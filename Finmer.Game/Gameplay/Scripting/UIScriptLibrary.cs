/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows.Media;
using Finmer.Models;
using Finmer.Utility;
using static Finmer.Gameplay.Scripting.LuaApi;

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// Script library containing functions related to the user interface.
    /// </summary>
    internal static class UIScriptLibrary
    {

        /// <summary>
        /// Register the library contents in the ScriptContext.
        /// </summary>
        /// <param name="context"></param>
        public static void Inject(ScriptContext context)
        {
            context.RegisterGlobalFunction("SetInstruction", ExportedSetInstruction);
            context.RegisterGlobalFunction("SetLocation", ExportedSetLocation);
            context.RegisterGlobalFunction("SetInventoryEnabled", ExportedSetInvEnabled);
            context.RegisterGlobalFunction("AddButton", ExportedAddButton);
            context.RegisterGlobalFunction("AddLink", ExportedAddLink);
            context.RegisterGlobalFunction("Log", ExportedLog);
            context.RegisterGlobalFunction("LogRaw", ExportedLogRaw);
            context.RegisterGlobalFunction("LogSplit", ExportedLogSplit);
            context.RegisterGlobalFunction("ClearLog", ExportedClearLog);
        }

        private static int ExportedSetInstruction(IntPtr L)
        {
            GameUI.Instance.Instruction = luaL_checkstring(L, 1);
            return 0;
        }

        private static int ExportedSetLocation(IntPtr L)
        {
            GameUI.Instance.Location = luaL_checkstring(L, 1);
            return 0;
        }

        private static int ExportedSetInvEnabled(IntPtr L)
        {
            luaL_checktype(L, 1, ELuaType.Boolean);
            GameUI.Instance.InventoryEnabled = lua_toboolean(L, 1);
            return 0;
        }

        private static int ExportedAddButton(IntPtr L)
        {
            // Compose all info provided by script into a model
            var settings = new ChoiceButtonModel
            {
                Choice = (int)luaL_checknumber(L, 1),
                Label = luaL_checkstring(L, 2),
                Tooltip = lua_tostring(L, 3),
                Highlight = lua_toboolean(L, 4),
                Width = (lua_type(L, 5) == ELuaType.Number) ? (float)lua_tonumber(L, 5) : 1.0f
            };

            // Add the button to the UI
            GameUI.Instance.AddButton(settings);

            // The first time a script shows a highlighted button, show a tip about it
            var save_data = GameController.Session.Player.AdditionalSaveData;
            if (settings.Highlight && !save_data.GetBool("tip_shown_highlight"))
            {
                save_data.SetBool("tip_shown_highlight", true);
                GameUI.Instance.Log(GameController.Content.GetAndParseString("tip_highlight_button"), Theme.LogColorHighlight);
            }

            return 0;
        }

        private static int ExportedAddLink(IntPtr L)
        {
            // Retrieve and validate the input compass direction
            var direction = (ECompassDirection)(int)luaL_checknumber(L, 1);
            if (direction < ECompassDirection.North || direction > ECompassDirection.East)
                return luaL_argerror(L, 1, "invalid compass direction");

            if (lua_type(L, 2) == ELuaType.Function)
            {
                // This is a script link, store the function for later use
                lua_pushvalue(L, 2);
                GameController.Session.Compass.AddScriptLink(direction, L);
            }
            else
            {
                // Otherwise, this should be a direct link to a scene
                string target = luaL_checkstring(L, 2);
                GameController.Session.Compass.AddDirectLink(direction, target);
            }

            return 0;
        }

        private static int ExportedLog(IntPtr L)
        {
            Color color = lua_type(L, 2) == ELuaType.Table ? LuaUtils.lua_tocolor(L, 2) : Theme.LogColorDefault;
            GameUI.Instance.Log(GameController.Content.GetAndParseString(luaL_checkstring(L, 1)), color);
            return 0;
        }

        private static int ExportedLogRaw(IntPtr L)
        {
            Color color = lua_type(L, 2) == ELuaType.Table ? LuaUtils.lua_tocolor(L, 2) : Theme.LogColorDefault;
            GameUI.Instance.Log(luaL_checkstring(L, 1), color);
            return 0;
        }

        private static int ExportedLogSplit(IntPtr L)
        {
            GameUI.Instance.LogSplit();
            return 0;
        }

        private static int ExportedClearLog(IntPtr L)
        {
            GameUI.Instance.ClearLog();
            return 0;
        }

    }

}
