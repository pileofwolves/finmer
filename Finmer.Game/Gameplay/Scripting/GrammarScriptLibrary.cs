/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using static Finmer.Gameplay.Scripting.LuaApi;

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// Script library containing functions related to text rendering and grammar.
    /// </summary>
    internal static class GrammarScriptLibrary
    {

        /// <summary>
        /// Register the library contents in the ScriptContext.
        /// </summary>
        /// <param name="context"></param>
        public static void Inject(ScriptContext context)
        {
            IntPtr state = context.LuaState;

            // Text table
            lua_createtable(state, 0, 2);
            context.RegisterFunction("SetContext", ExportedTextSetContext);
            context.RegisterFunction("GetString", ExportedTextGetString);
            lua_setglobal(state, "Text");
        }

        private static int ExportedTextSetContext(IntPtr state)
        {
            // Parse arguments
            string key = luaL_checkstring(state, 1);
            if (!(ScriptableObject.FromLua(state, 2) is GameObject context))
                return luaL_error(state, "GameObject expected as argument #2");
            bool persist = lua_toboolean(state, 3) == 1; // Optional

            TextParser.SetContext(key, context, persist);
            return 0;
        }

        private static int ExportedTextGetString(IntPtr L)
        {
            lua_pushstring(L, GameController.GetString(luaL_checkstring(L, 1)));
            return 1;
        }

    }

}
