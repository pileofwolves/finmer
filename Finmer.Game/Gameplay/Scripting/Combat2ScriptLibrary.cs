/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Gameplay.Combat;

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// Script library containing functions related to the V2 combat system.
    /// </summary>
    internal static class Combat2ScriptLibrary
    {

        /// <summary>
        /// Register the library contents in the ScriptContext.
        /// </summary>
        /// <param name="context"></param>
        public static void Inject(ScriptContext context)
        {
            // CombatV2 table
            context.RegisterGlobalFunction("Combat2", ExportedNewCombat2State);
        }

        private static int ExportedNewCombat2State(IntPtr state)
        {
            // Create a new CombatState on the caller stack (not the main thread stack!) and return it to the caller
            var combat = new CombatSession(ScriptContext.FromLua(state));
            combat.PushToLua(state);
            return 1;
        }

    }

}
