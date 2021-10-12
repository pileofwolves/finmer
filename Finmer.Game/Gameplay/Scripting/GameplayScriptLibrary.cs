﻿/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
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

            // Misc gameplay
            context.RegisterGlobalFunction("GetTime", ExportedGetTime);
            context.RegisterGlobalFunction("AdvanceTime", ExportedAddTime);
            context.RegisterGlobalFunction("SetScene", ExportedSetScene);

            // Journal table
            lua_createtable(state, 0, 2);
            context.RegisterFunction("Update", ExportedJournalSet);
            context.RegisterFunction("Close", ExportedJournalClose);
            lua_setglobal(state, "Journal");
        }

        private static int ExportedNewCharacter(IntPtr state)
        {
            // try to load the specified character object
            string name = luaL_checkstring(state, 1);
            Character ch = Character.FromAsset(ScriptContext.FromLua(state), name);
            if (ch == null)
                return luaL_error(state, $"failed to load creature '{name}'");

            // save a reference to it and return it to lua
            ch.PushToLua(state);
            return 1;
        }

        private static int ExportedNewItem(IntPtr state)
        {
            // try to load the specified character object
            string name = luaL_checkstring(state, 1);
            Item obj = Item.FromAsset(ScriptContext.FromLua(state), name);
            if (obj == null)
                return luaL_error(state, $"failed to load item '{name}'");

            // save a reference to it and return it to lua
            obj.PushToLua(state);
            return 1;
        }

        private static int ExportedNewShop(IntPtr state)
        {
            // try to load the specified character object
            string name = luaL_checkstring(state, 1);
            ShopState shopstate = ShopState.Load(ScriptContext.FromLua(state), name);

            // save a reference to it and return it to lua
            shopstate.PushToLua(state);
            return 1;
        }

        private static int ExportedGetTime(IntPtr L)
        {
            lua_pushnumber(L, GameController.Session.Player.TimeDay);
            lua_pushnumber(L, GameController.Session.Player.TimeHour);
            return 2;
        }

        private static int ExportedAddTime(IntPtr L)
        {
            int hours = (int)luaL_checknumber(L, 1);
            GameController.Session.AdvanceTime(hours);
            return 0;
        }

        private static int ExportedSetScene(IntPtr L)
        {
            // change the scene to the specified string
            GameController.Session.SetScene(new SceneScripted(ScriptContext.FromLua(L), luaL_checkstring(L, 1)));
            // throw a faux error to terminate the old script
            // TODO: error has a lot of overhead, maybe we can instead yield and not register the thread for resuming
            return luaL_error(L, "ScriptStopSignal");
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

    }

}
