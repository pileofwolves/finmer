/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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

        private static Random s_RNG;

        /// <summary>
        /// Register the library contents in the ScriptContext.
        /// </summary>
        /// <param name="context"></param>
        public static void Inject(ScriptContext context)
        {
            IntPtr state = context.LuaState;

            // Re-seed the RNG
            s_RNG = new Random();

            // Replace the built-in RNG with our custom one
            lua_getglobal(state, "math");
            lua_pushnil(state); // math.randomseed is not supported
            lua_setfield(state, -2, "randomseed");
            context.RegisterFunction("random", ExportedRandom);
            lua_pop(state, 1);

            // Override some default functions
            context.RegisterGlobalFunction("require", ExportedRequire);
            context.RegisterGlobalFunction("print", ExportedPrint);

            // Misc global utilities
            context.RegisterGlobalFunction("IsDebugMode", ExportedIsDebugMode);
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

        private static int ExportedIsDebugMode(IntPtr L)
        {
            lua_pushboolean(L, GameController.DebugMode);
            return 1;
        }

        private static int ExportedRandom(IntPtr L)
        {
            // Re-implementation of Lua's default math.random function, but with a much higher-quality underlying PRNG
            double rand = s_RNG.NextDouble();
            switch (lua_gettop(L))
            {
                case 0:
                    lua_pushnumber(L, rand);
                    break;
                case 1:
                {
                    var upper = (int)luaL_checknumber(L, 1);
                    if (upper < 1) luaL_error(L, "interval is empty");
                    lua_pushnumber(L, Math.Floor(rand * upper) + 1);
                    break;
                }

                case 2:
                {
                    var lower = (int)luaL_checknumber(L, 1);
                    var upper = (int)luaL_checknumber(L, 2);
                    if (lower > upper) luaL_error(L, "interval is empty");
                    lua_pushnumber(L, Math.Floor(rand * (upper - lower + 1)) + lower);
                    break;
                }
            }

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
