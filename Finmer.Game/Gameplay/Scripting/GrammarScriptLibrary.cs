/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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
            context.RegisterFunction("SetVariable", ExportedTextSetVariable);
            context.RegisterFunction("SetContext", ExportedTextSetContext);
            context.RegisterFunction("GetString", ExportedTextGetString);
            lua_setglobal(state, "Text");
        }

        private static int ExportedTextSetVariable(IntPtr state)
        {
            // Parse arguments
            var key = luaL_checkstring(state, 1);
            var value = luaL_checkstring(state, 2);

            // Register the variable
            TextParser.SetVariable(key, value);

            return 0;
        }

        private static int ExportedTextSetContext(IntPtr state)
        {
            // Parse arguments
            var key = luaL_checkstring(state, 1);
            var context = ScriptableObject.FromLuaNonOptional<GameObject>(state, 2);
            var persist = lua_toboolean(state, 3); // Optional

            // Register the grammar context
            TextParser.SetContext(key, context, persist);

            return 0;
        }

        private static int ExportedTextGetString(IntPtr L)
        {
            lua_pushstring(L, GameController.Content.GetAndParseString(luaL_checkstring(L, 1)));
            return 1;
        }

    }

}
