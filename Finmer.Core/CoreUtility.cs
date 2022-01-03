/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;

namespace Finmer.Core
{

    public static class CoreUtility
    {

        /// <summary>
        /// A public RNG instance. Use for whatever desired purpose.
        /// </summary>
        public static readonly Random Rng = new Random(Guid.NewGuid().GetHashCode());

        /// <summary>
        /// Capitalizes the first letter in a string.
        /// </summary>
        public static string CapFirst(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return String.Empty;

            // Capitalize the first character in the string's character array, then build a new string with that change
            char[] ca = value.ToCharArray();
            ca[0] = char.ToUpper(value[0]);
            return new string(ca);
        }

    }

}
