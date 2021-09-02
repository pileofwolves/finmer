/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Core.Buffs
{

    /// <summary>
    /// Modifies the number of attack dice a character has in combat.
    /// </summary>
    public sealed class BuffAttackDice : Buff
    {

        /// <summary>
        /// Change to apply to the number of dice.
        /// </summary>
        public int Delta { get; set; }

        public override EBuffIcon GetIcon()
        {
            return Delta >= 0 ? EBuffIcon.IncreasedAttack : EBuffIcon.DecreasedAttack;
        }

    }

    /// <summary>
    /// Modifies the number of defense dice a character has in combat.
    /// </summary>
    public sealed class BuffDefenseDice : Buff
    {

        /// <summary>
        /// Change to apply to the number of dice.
        /// </summary>
        public int Delta { get; set; }

        public override EBuffIcon GetIcon()
        {
            return Delta >= 0 ? EBuffIcon.IncreasedDefense : EBuffIcon.DecreasedDefense;
        }

    }

}
