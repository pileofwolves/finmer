/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using Finmer.Gameplay.Combat;
using Finmer.Models;
using Finmer.ViewModels;
using static Finmer.Gameplay.Scripting.LuaApi;

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// Script library containing functions related to visual effects and animations.
    /// </summary>
    internal static class EffectScriptLibrary
    {

        /// <summary>
        /// Register the library contents in the ScriptContext.
        /// </summary>
        public static void Inject(ScriptContext context)
        {
            IntPtr state = context.LuaState;

            // Effect table
            lua_createtable(state, 0, 1);
            context.RegisterFunction("ShowOpposedDiceRoll", ExportedEffectOpposedDiceRoll);
            lua_setglobal(state, "Effect");
        }

        /// <summary>
        /// Convert a Lua field in the table at the specified stack index into a boolean. If missing, false is returned.
        /// </summary>
        private static bool GetParamBool(IntPtr state, string key, int stack_index)
        {
            lua_getfield(state, stack_index, key);
            bool output = lua_toboolean(state, -1);
            lua_pop(state, 1);
            return output;
        }

        /// <summary>
        /// Convert a Lua field in the table at the specified stack index into a string. If missing, the specified default is returned.
        /// </summary>
        private static string GetParamString(IntPtr state, string key, int stack_index, string default_value = "")
        {
            string output = default_value;
            lua_getfield(state, stack_index, key);
            if (lua_isstring(state, -1))
                output = lua_tostring(state, -1);
            lua_pop(state, 1);
            return output;
        }

        /// <summary>
        /// Parse a nested Lua table into a list of die faces, and count the sum of all die faces along the way.
        /// </summary>
        /// <param name="state">Lua state. The target table must be at the top of the stack.</param>
        /// <param name="field">Name of the field to retrieve in target table.</param>
        /// <param name="total">Contains the sum of the dice.</param>
        private static List<EDieFace> DecodeDieFaceList(IntPtr state, string field, out int total)
        {
            total = 0;

            // Retrieve the nested table by name
            lua_getfield(state, -1, field);
            if (!lua_istable(state, -1))
                luaL_error(state, $"rounds list '{field}' is not a table");

            // Use optional 'hostile' flag to determine offset in the die face list
            EDieFace face_offset = GetParamBool(state, "hostile", -1) ? EDieFace.HostileGeneric1 : EDieFace.AlliedGeneric1;

            // Get number of dice in the table, and preallocate memory for them
            int die_count = (int)lua_objlen(state, -1);
            List<EDieFace> faces = new List<EDieFace>(die_count);

            // Parse each die face
            for (int i = 1; i <= die_count; i++)
            {
                // Die must be presented as a number from 1 to 6
                lua_rawgeti(state, -1, i);
                if (!lua_isnumber(state, -1))
                    luaL_error(state, $"in rounds list {field}: element at index {i}: expected number, got {lua_typename(state, lua_type(state, -1))}");

                // Add to total
                int face_num = (int)lua_tonumber(state, -1);
                total += face_num;

                // Convert number to a die face and add to list for presentation
                EDieFace face = face_offset + (face_num - 1);
                faces.Add(face);

                lua_pop(state, 1);
            }

            lua_pop(state, 1);

            return faces;
        }

        /// <summary>
        /// Exported function for displaying an opposing dice roll animation between two characters.
        /// </summary>
        private static int ExportedEffectOpposedDiceRoll(IntPtr state)
        {
            // Input is expected to be a table, because positional parameters are confusing for a large function like this
            luaL_checktype(state, 1, ELuaType.Table);

            // Push function arguments on the stack in a consistent order
            lua_getfield(state, 1, "instigator");
            lua_getfield(state, 1, "target");
            lua_getfield(state, 1, "rounds");
            if (!lua_isuserdata(state, -3))
                return luaL_error(state, "instigator is not a Creature");
            if (!lua_isuserdata(state, -2))
                return luaL_error(state, "target is not a Creature");
            if (!lua_istable(state, -1))
                return luaL_error(state, "rounds list is not a table");

            // Generate a new combat session, which we can use as backing context for the dice animation
            var context = ScriptContext.FromLua(state);
            var session = CombatSession.GetActiveSession() ?? new CombatSession(context);
            var instigator = new Participant(ScriptableObject.FromLuaNonOptional<Character>(state, -3), session);
            var target = new Participant(ScriptableObject.FromLuaNonOptional<Character>(state, -2), session);

            // Cache game state
            var is_in_combat = GameUI.Instance.IsInCombat;
            var round_count = (int)lua_objlen(state, -1);

            // Parse animation metadata
            var info = new CombatDisplay.ResolveInfo
            {
                Instigator = instigator,
                Target = target,
                ActionLabelInstigator = GetParamString(state, "label_instigator", 1, "Total"),
                ActionLabelTarget = GetParamString(state, "label_target", 1, "Total"),
                LogKey = GetParamString(state, "log_key", 1),
                Rounds = new List<CombatDisplay.ResolveRound>(round_count)
            };

            // Parse round configuration
            for (int i = 1; i <= round_count; i++)
            {
                lua_rawgeti(state, -1, i);

                info.Rounds.Add(new CombatDisplay.ResolveRound
                {
                    OffenseDice = DecodeDieFaceList(state, "instigator_dice", out var offense_total),
                    DefenseDice = DecodeDieFaceList(state, "target_dice", out var defense_total),
                    OffenseTotal = offense_total,
                    DefenseTotal = defense_total
                });

                lua_pop(state, 1);
            }

            // If this function is called while another combat is in progress, reuse the same view model, to avoid trampling combat state
            if (!is_in_combat)
                GameUI.Instance.CombatStateViewModel = new CombatStateViewModel();

            // With the installed view model, initiate the animation
            CombatDisplay.ShowRoundResolve(info);

            // Restore previous state (i.e. no state) only if we overrode it in the first place
            if (!is_in_combat)
                GameUI.Instance.CombatStateViewModel = null;

            return 0;
        }

    }

}
