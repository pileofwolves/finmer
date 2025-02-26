/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Threading;
using Finmer.Models;
using Finmer.Utility;
using static Finmer.Gameplay.Scripting.LuaApi;

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// Script library that contains various global utilities.
    /// </summary>
    internal static class GlobalScriptLibrary
    {

        /// <summary>
        /// Register the library contents in the ScriptContext.
        /// </summary>
        /// <param name="context"></param>
        public static void Inject(ScriptContext context)
        {
            // Override some default functions
            context.RegisterGlobalFunction("require", ExportedRequire);
            context.RegisterGlobalFunction("print", ExportedPrint);

            // Misc global utilities
            context.RegisterGlobalFunction("IsDevModeEnabled", ExportedIsDevModeEnabled);
            context.RegisterGlobalFunction("Sleep", ExportedSleep);
        }

        private static int ExportedRequire(IntPtr state)
        {
            return luaL_error(state, "'require' is not supported in Finmer. All Script Assets are automatically compiled and executed on game start.");
        }

        private static int ExportedPrint(IntPtr state)
        {
            int num_args = lua_gettop(state);

            // Use the global 'tostring' function for this
            lua_getglobal(state, "tostring");

            // Prefix the output string with the name of the caller, so it's easier to track down stray print statements
            luaL_where(state, 1);

            // Stringify each individual argument
            for (var i = 1; i <= num_args; i++)
            {
                lua_pushvalue(state, num_args + 1); // tostring function
                lua_pushvalue(state, i); // argument
                lua_call(state, 1, 1);
            }

            // Concatenate all the strings together and output the text
            lua_concat(state, num_args + 1);
            string merged_text = lua_tostring(state, -1);
            GameUI.Instance.Log(merged_text, Theme.LogColorDarkGray);

            return 0;
        }

        private static int ExportedIsDevModeEnabled(IntPtr L)
        {
            lua_pushboolean(L, GameController.IsDevModeEnabled);
            return 1;
        }

        private static int ExportedSleep(IntPtr L)
        {
            var milli = (int)(luaL_checknumber(L, 1) * 1000.0);
            if (milli < 1) luaL_error(L, "sleep timeout must be at least 1 ms");
            Thread.Sleep(milli);
            return 0;
        }

    }

}
