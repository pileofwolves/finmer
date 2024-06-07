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
    /// Script library containing functions related to modules and assets.
    /// </summary>
    internal static class ModuleScriptLibrary
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
            context.RegisterFunction("IsModuleLoaded", ExportedModulesIsModuleLoaded);
            context.RegisterFunction("HasAssetByID", ExportedModulesHasAssetByID);
            lua_setglobal(state, "Modules");
        }

        private static int ExportedModulesIsModuleLoaded(IntPtr state)
        {
            var string_id = luaL_checkstring(state, 1);
            var id = Guid.Parse(string_id);

            lua_pushboolean(state, GameController.LoadedModules.Any(metadata => id == metadata.ID));

            return 1;
        }

        private static int ExportedModulesHasAssetByID(IntPtr state)
        {
            var string_id = luaL_checkstring(state, 1);
            var id = Guid.Parse(string_id);

            lua_pushboolean(state, GameController.Content.GetAssetByID(id) != null);

            return 1;
        }

    }

}
