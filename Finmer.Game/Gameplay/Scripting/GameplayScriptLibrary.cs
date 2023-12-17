/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core.Assets;
using static Finmer.Gameplay.Scripting.LuaApi;

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// Script library containing functions related to general gameplay or asset functions.
    /// </summary>
    internal static class GameplayScriptLibrary
    {

        /// <summary>
        /// Register the library contents in the ScriptContext.
        /// </summary>
        /// <param name="context"></param>
        public static void Inject(ScriptContext context)
        {
            IntPtr state = context.LuaState;

            // ScriptableObject constructors
            context.RegisterGlobalFunction("Creature", ExportedNewCharacter);
            context.RegisterGlobalFunction("Item", ExportedNewItem);
            context.RegisterGlobalFunction("Shop", ExportedNewShop);

            // Time and clock
            context.RegisterGlobalFunction("GetTime", ExportedGetTime);
            context.RegisterGlobalFunction("GetTimeHour", ExportedGetTimeHour);
            context.RegisterGlobalFunction("GetTimeHourTotal", ExportedGetTimeHourTotal);
            context.RegisterGlobalFunction("GetTimeDay", ExportedGetTimeDay);
            context.RegisterGlobalFunction("GetIsNight", ExportedGetIsNight);
            context.RegisterGlobalFunction("SetTimeHour", ExportedSetTimeHour);
            context.RegisterGlobalFunction("AdvanceTime", ExportedAddTime);

            // Misc gameplay
            context.RegisterGlobalFunction("SetScene", ExportedSetScene);
            context.RegisterGlobalFunction("EndGame", ExportedEndGame);

            // Journal table
            lua_createtable(state, 0, 2);
            context.RegisterFunction("Update", ExportedJournalSet);
            context.RegisterFunction("Close", ExportedJournalClose);
            lua_setglobal(state, "Journal");
        }

        private static int ExportedNewCharacter(IntPtr state)
        {
            // Try creating an instance of a Character with the specified asset name
            var name = luaL_checkstring(state, 1);
            var context = ScriptContext.FromLua(state);
            Character instance;
            try
            {
                instance = Character.FromAsset(context, name);
            }
            catch (Exception ex)
            {
                return luaL_error(state, $"Failed to load Creature '{name}': {ex.Message}");
            }

            // Return it to script
            instance.PushToLua(state);
            return 1;
        }

        private static int ExportedNewItem(IntPtr state)
        {
            // Try creating an instance of an Item with the specified asset name
            var name = luaL_checkstring(state, 1);
            var context = ScriptContext.FromLua(state);
            Item instance;
            try
            {
                instance = Item.FromAsset(context, name);
            }
            catch (Exception ex)
            {
                return luaL_error(state, $"Failed to load Item '{name}': {ex.Message}");
            }

            // Return it to script
            instance.PushToLua(state);
            return 1;
        }

        private static int ExportedNewShop(IntPtr state)
        {
            // Get or create the shop with the specified unique key
            var key = luaL_checkstring(state, 1);
            var context = ScriptContext.FromLua(state);
            ShopState shop;
            try
            {
                shop = ShopState.LoadOrCreate(context, key);
            }
            catch (Exception ex)
            {
                return luaL_error(state, $"Failed to load Shop '{key}': {ex.Message}");
            }

            // Return it to script
            shop.PushToLua(state);
            return 1;
        }

        private static int ExportedGetTime(IntPtr L)
        {
            lua_pushnumber(L, GameController.Session.Player.TimeDay);
            lua_pushnumber(L, GameController.Session.Player.TimeHour);
            return 2;
        }

        private static int ExportedGetTimeDay(IntPtr L)
        {
            lua_pushnumber(L, GameController.Session.Player.TimeDay);
            return 1;
        }

        private static int ExportedGetTimeHour(IntPtr L)
        {
            lua_pushnumber(L, GameController.Session.Player.TimeHour);
            return 1;
        }

        private static int ExportedGetTimeHourTotal(IntPtr L)
        {
            lua_pushnumber(L, GameController.Session.Player.TimeHourCumulative);
            return 1;
        }

        private static int ExportedGetIsNight(IntPtr L)
        {
            int hour = GameController.Session.Player.TimeHour;
            lua_pushboolean(L, hour < 6 || hour > 20);
            return 1;
        }

        private static int ExportedSetTimeHour(IntPtr L)
        {
            var session = GameController.Session;
            var player = session.Player;

            // Find easiest way to reach the target time
            int diff = (int)luaL_checknumber(L, 1) - player.TimeHour;
            if (diff < 0)
            {
                // Move clock "backwards" (a full day cycle) to reach an earlier hour timestamp
                session.AdvanceTime(24 + diff);
            }
            else
            {
                // Move clock forward by difference to target time
                session.AdvanceTime(diff);
            }

            return 0;
        }

        private static int ExportedAddTime(IntPtr L)
        {
            int hours = (int)luaL_checknumber(L, 1);
            GameController.Session.AdvanceTime(hours);
            return 0;
        }

        private static int ExportedSetScene(IntPtr L)
        {
            // Load the specified scene
            try
            {
                GameController.Session.SetScene(new SceneScripted(ScriptContext.FromLua(L), luaL_checkstring(L, 1)));
            }
            catch (ArgumentException ex)
            {
                return luaL_error(L, ex.Message);
            }
            catch (ScriptException ex)
            {
                return luaL_error(L, ex.Message);
            }

            // Interrupt the current scene so control returns to the script wrapper that invoked Lua. While throwing an error
            // would have the same effect, yielding is much faster even though we do not intend to ever return control to this thread.
            return lua_yield(L, 0);
        }

        private static int ExportedJournalSet(IntPtr L)
        {
            string quest_name = luaL_checkstring(L, 1);
            var quest_stage = (int)luaL_checknumber(L, 2);

            var quest = GameController.Content.GetAssetByName(quest_name) as AssetJournal;
            if (quest == null) return luaL_error(L, "No Journal asset with name '" + quest_name + "' exists.");

            GameController.Session.Player.Journal.SetQuestStage(quest, quest_stage);
            return 0;
        }

        private static int ExportedJournalClose(IntPtr L)
        {
            string quest_name = luaL_checkstring(L, 1);
            var quest = GameController.Content.GetAssetByName(quest_name) as AssetJournal;
            if (quest == null) return luaL_error(L, "No Journal asset with name '" + quest_name + "' exists.");

            GameController.Session.Player.Journal.CloseQuest(quest);
            return 0;
        }

        private static int ExportedEndGame(IntPtr L) 
        {
            GameController.Session.RequestGameOver();
            return 0;
        }
    }

}
