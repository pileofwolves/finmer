/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core.Buffs;
using Finmer.Gameplay.Combat;
using static Finmer.Gameplay.Scripting.LuaApi;

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// Script library containing functions related to the V2 combat system.
    /// </summary>
    internal static class Combat2ScriptLibrary
    {

        /// <summary>
        /// Register the library contents in the ScriptContext.
        /// </summary>
        /// <param name="context"></param>
        public static void Inject(ScriptContext context)
        {
            IntPtr state = context.LuaState;

            // Combat globals
            context.RegisterGlobalFunction("Combat2", ExportedNewCombatSession); // Backwards compatibility
            context.RegisterGlobalFunction("CombatSession", ExportedNewCombatSession);
            context.RegisterGlobalFunction("GetActiveCombat", ExportedGetActiveCombat);

            // Buff table
            lua_createtable(state, 0, 2);
            context.RegisterFunction("AttackDice", ExportedNewBuffAttackDice);
            context.RegisterFunction("DefenseDice", ExportedNewBuffDefenseDice);
            context.RegisterFunction("GrappleDice", ExportedNewBuffGrappleDice);
            context.RegisterFunction("SwallowDice", ExportedNewBuffSwallowDice);
            context.RegisterFunction("StruggleDice", ExportedNewBuffStruggleDice);
            context.RegisterFunction("HealthOverTime", ExportedNewBuffHealthOverTime);
            context.RegisterFunction("Stun", ExportedNewBuffStun);
            lua_setglobal(state, "Buff");
        }

        private static int ExportedNewCombatSession(IntPtr state)
        {
            // Create a new CombatState on the caller stack (not the main thread stack!) and return it to the caller
            var combat = new CombatSession(ScriptContext.FromLua(state));
            combat.PushToLua(state);
            return 1;
        }

        private static int ExportedGetActiveCombat(IntPtr state)
        {
            // Get the CombatSession of the currently active scene, if any
            var session = CombatSession.GetActiveSession();
            if (session == null)
            {
                // There is no active combat
                lua_pushnil(state);
                return 1;
            }

            // Return the session to script
            session.PushToLua(state);
            return 1;
        }

        private static int ExportedNewBuffAttackDice(IntPtr state)
        {
            return InternalNewPendingBuff(state, new BuffAttackDice { Delta = (int)luaL_checknumber(state, 1) }, (int)luaL_checknumber(state, 2));
        }

        private static int ExportedNewBuffDefenseDice(IntPtr state)
        {
            return InternalNewPendingBuff(state, new BuffDefenseDice { Delta = (int)luaL_checknumber(state, 1) }, (int)luaL_checknumber(state, 2));
        }

        private static int ExportedNewBuffGrappleDice(IntPtr state)
        {
            return InternalNewPendingBuff(state, new BuffGrappleDice { Delta = (int)luaL_checknumber(state, 1) }, (int)luaL_checknumber(state, 2));
        }

        private static int ExportedNewBuffStruggleDice(IntPtr state)
        {
            return InternalNewPendingBuff(state, new BuffStruggleDice { Delta = (int)luaL_checknumber(state, 1) }, (int)luaL_checknumber(state, 2));
        }

        private static int ExportedNewBuffSwallowDice(IntPtr state)
        {
            return InternalNewPendingBuff(state, new BuffSwallowDice { Delta = (int)luaL_checknumber(state, 1) }, (int)luaL_checknumber(state, 2));
        }

        private static int ExportedNewBuffHealthOverTime(IntPtr state)
        {
            return InternalNewPendingBuff(state, new BuffHealthOverTime { Delta = (int)luaL_checknumber(state, 1) }, (int)luaL_checknumber(state, 2));
        }

        private static int ExportedNewBuffStun(IntPtr state)
        {
            return InternalNewPendingBuff(state, new BuffStun(), (int)luaL_checknumber(state, 1));
        }

        private static int InternalNewPendingBuff(IntPtr state, Buff effect, int duration)
        {
            var buff = new PendingBuff(ScriptContext.FromLua(state))
            {
                Effect = effect,
                Duration = duration
            };

            buff.PushToLua(state);
            return 1;
        }

    }

}
