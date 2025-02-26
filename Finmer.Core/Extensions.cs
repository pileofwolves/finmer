/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Core
{

    /// <summary>
    /// A container for extension methods.
    /// </summary>
    public static class Extensions
    {

        /// <summary>
        /// Formats the input user string to be suitable for unique key usage in the game engine.
        /// </summary>
        public static string MakeSafeIdentifier(this string str)
        {
            return str.Trim().ToUpperInvariant().Replace(' ', '_');
        }

    }

}
