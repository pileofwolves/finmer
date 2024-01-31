/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;

namespace Finmer.Core.VisualScripting
{

    /// <summary>
    /// Utilities for generating Lua script code related to the combat system.
    /// </summary>
    public static class CombatUtilities
    {

        /// <summary>
        /// The fixed participant ID for the player character.
        /// </summary>
        public const string k_PlayerParticipantID = @"PLAYER";

        /// <summary>
        /// Returns a Lua local variable name identifying a participant, given a unique participant key.
        /// </summary>
        internal static string GetParticipantVariableName(string id)
        {
            return @"_par_" + id.MakeSafeIdentifier().ToLowerInvariant();
        }

        /// <summary>
        /// Returns a human-readable description of the specified participant ID, for editor display.
        /// </summary>
        internal static string GetEditorParticipantDescription(string id)
        {
            if (id.Equals(k_PlayerParticipantID, StringComparison.InvariantCultureIgnoreCase))
                return "Player";

            return "NPC " + id.MakeSafeIdentifier();
        }

    }

}
