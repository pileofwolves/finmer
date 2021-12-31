/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;

namespace Finmer.Core.Buffs
{

    /// <summary>
    /// Factory utility for instantiating derived Buff objects.
    /// </summary>
    public static class BuffFactory
    {

        /// <summary>
        /// Instantiates a new Buff given a type name.
        /// </summary>
        public static Buff CreateInstance(string typeName)
        {
            switch (typeName)
            {
                case nameof(BuffAttackDice):
                    return new BuffAttackDice();

                case nameof(BuffDefenseDice):
                    return new BuffDefenseDice();

                case nameof(BuffHealth):
                    return new BuffHealth();

                default:
                    throw new ArgumentException(nameof(typeName));
            }
        }

    }

}
