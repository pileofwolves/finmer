/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Linq;
using static Finmer.Gameplay.Scripting.LuaApi;

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// Script library containing functions related to the content repository, mainly for inspecting modules and assets.
    /// </summary>
    internal static class ContentScriptLibrary
    {

        /// <summary>
        /// Register the library contents in the ScriptContext.
        /// </summary>
        /// <param name="context"></param>
        public static void Inject(ScriptContext context)
        {
            IntPtr state = context.LuaState;

            // Modules table
            lua_createtable(state, 0, 2);
            context.RegisterFunction("IsModuleLoadedByID", ExportedContentIsModuleLoadedByID);
            context.RegisterFunction("IsAssetLoadedByID", ExportedContentIsAssetLoadedByID);
            lua_setglobal(state, "Content");
        }

        private static int ExportedContentIsModuleLoadedByID(IntPtr state)
        {
            var string_id = luaL_checkstring(state, 1);
            if (!Guid.TryParse(string_id, out var id))
                return luaL_argerror(state, 1, "invalid id format");

            lua_pushboolean(state, GameController.LoadedModules.Any(metadata => id == metadata.ID));

            return 1;
        }

        private static int ExportedContentIsAssetLoadedByID(IntPtr state)
        {
            var string_id = luaL_checkstring(state, 1);
            if (!Guid.TryParse(string_id, out var id))
                return luaL_argerror(state, 1, "invalid id format");

            lua_pushboolean(state, GameController.Content.GetAssetByID(id) != null);

            return 1;
        }

    }

}
