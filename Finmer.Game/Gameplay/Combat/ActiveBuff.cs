/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Buffs;

namespace Finmer.Gameplay.Combat
{

    /// <summary>
    /// Contains state for a buff that was applied mid-combat and has a limited duration.
    /// </summary>
    public class ActiveBuff
    {

        /// <summary>
        /// The underlying effect.
        /// </summary>
        public Buff Effect { get; }

        /// <summary>
        /// Time left before the buff is removed, in combat rounds.
        /// </summary>
        public int RoundsLeft { get; set; }

    }

}
