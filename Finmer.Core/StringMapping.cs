/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Core
{

    /// <summary>
    /// Describes a conditional mapping of one text string onto another.
    /// </summary>
    public struct StringMapping
    {

        /// <summary>
        /// Describes the condition that must be met for a string mapping to be activated.
        /// </summary>
        public enum ERule
        {
            Always,
            NpcToPlayer,
            NpcToNpc,
            PlayerToNpc
        }

        /// <summary>
        /// The key of the string to replace.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The condition that must be met for the string to be replaced.
        /// </summary>
        public ERule Rule { get; set; }

        /// <summary>
        /// The key of the new string to use.
        /// </summary>
        public string NewKey { get; set; }

    }

}
