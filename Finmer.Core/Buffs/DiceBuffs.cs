/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Core.Buffs
{

    /// <summary>
    /// Modifies the number of attack dice a character has in combat.
    /// </summary>
    public sealed class BuffAttackDice : SingleDeltaBuff
    {

        public override EBuffIcon GetIcon()
        {
            return Delta >= 0 ? EBuffIcon.IncreasedAttack : EBuffIcon.DecreasedAttack;
        }

        public override string GetDescription()
        {
            return $"{Delta:+#;-#;0} Dice";
        }

        public override Buff Clone()
        {
            return new BuffAttackDice
            {
                Delta = Delta
            };
        }

    }

    /// <summary>
    /// Modifies the number of defense dice a character has in combat.
    /// </summary>
    public sealed class BuffDefenseDice : SingleDeltaBuff
    {

        public override EBuffIcon GetIcon()
        {
            return Delta >= 0 ? EBuffIcon.IncreasedDefense : EBuffIcon.DecreasedDefense;
        }

        public override string GetDescription()
        {
            return $"{Delta:+#;-#;0} Dice";
        }

        public override Buff Clone()
        {
            return new BuffDefenseDice
            {
                Delta = Delta
            };
        }

    }

}
