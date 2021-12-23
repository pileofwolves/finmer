/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core;
using Finmer.Utility;
using Finmer.Views;
using static Finmer.Gameplay.Scripting.LuaApi;

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// Script library containing functions related to persistent data storage.
    /// </summary>
    internal static class SaveDataScriptLibrary
    {

        /// <summary>
        /// Register the library contents in the ScriptContext.
        /// </summary>
        /// <param name="context"></param>
        public static void Inject(ScriptContext context)
        {
            IntPtr state = context.LuaState;

            // SaveData table
            lua_createtable(state, 0, 1);
            context.RegisterFunction("ShowSaveDialog", ExportedSaveGame);
            lua_setglobal(state, "SaveData");

            // Storage table
            lua_createtable(state, 0, 7);
            context.RegisterFunction("SetFlag", ExportedStorageSetFlag);
            context.RegisterFunction("SetNumber", ExportedStorageSetNumber);
            context.RegisterFunction("SetString", ExportedStorageSetString);
            context.RegisterFunction("ModifyNumber", ExportedStorageModifyNumber);
            context.RegisterFunction("GetFlag", ExportedStorageGetFlag);
            context.RegisterFunction("GetNumber", ExportedStorageGetNumber);
            context.RegisterFunction("GetString", ExportedStorageGetString);
            lua_setglobal(state, "Storage");
        }

        private static int ExportedSaveGame(IntPtr state)
        {
            GameController.Window.Dispatcher.Invoke(delegate
            {
                GameController.Window.Navigate(new SaveGamePage(), ENavigatorAnimation.SlideLeft);
            });

            // Pause the script so it can be resumed once the user closes the dialog
            return lua_yield(state, 0);
        }

        private static PropertyBag GetAdditionalSaveData()
        {
            return GameController.Session.Player.AdditionalSaveData;
        }

        private static string GetNamespacedPropertyPath(string property)
        {
            // Namespace the properties set through scripts, so that they do not clash with save data set by game code
            return @"LUA_" + property;
        }

        private static int ExportedStorageSetFlag(IntPtr L)
        {
            string key = GetNamespacedPropertyPath(luaL_checkstring(L, 1));
            luaL_checktype(L, 2, ELuaType.Boolean);
            GetAdditionalSaveData().SetBool(key, lua_toboolean(L, 2));
            return 0;
        }

        private static int ExportedStorageSetNumber(IntPtr L)
        {
            string key = GetNamespacedPropertyPath(luaL_checkstring(L, 1));
            GetAdditionalSaveData().SetFloat(key, (float)luaL_checknumber(L, 2));
            return 0;
        }

        private static int ExportedStorageSetString(IntPtr L)
        {
            string key = GetNamespacedPropertyPath(luaL_checkstring(L, 1));
            GetAdditionalSaveData().SetString(key, luaL_checkstring(L, 2));
            return 0;
        }

        private static int ExportedStorageModifyNumber(IntPtr L)
        {
            string key = GetNamespacedPropertyPath(luaL_checkstring(L, 1));
            PropertyBag props = GetAdditionalSaveData();
            props.SetFloat(key, props.GetFloat(key) + (float)luaL_checknumber(L, 2));
            return 0;
        }

        private static int ExportedStorageGetNumber(IntPtr L)
        {
            lua_pushnumber(L, GetAdditionalSaveData().GetFloat(GetNamespacedPropertyPath(luaL_checkstring(L, 1))));
            return 1;
        }

        private static int ExportedStorageGetFlag(IntPtr L)
        {
            lua_pushboolean(L, GetAdditionalSaveData().GetBool(GetNamespacedPropertyPath(luaL_checkstring(L, 1))));
            return 1;
        }

        private static int ExportedStorageGetString(IntPtr L)
        {
            lua_pushstring(L, GetAdditionalSaveData().GetString(GetNamespacedPropertyPath(luaL_checkstring(L, 1))));
            return 1;
        }

    }

}
