/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
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
            return Delta >= 0 ? EBuffIcon.AttackUp : EBuffIcon.AttackDown;
        }

        public override string GetDescription()
        {
            return $"{Delta:+#;-#;0} Attack Dice";
        }

    }

    /// <summary>
    /// Modifies the number of defense dice a character has in combat.
    /// </summary>
    public sealed class BuffDefenseDice : SingleDeltaBuff
    {

        public override EBuffIcon GetIcon()
        {
            return Delta >= 0 ? EBuffIcon.DefenseUp : EBuffIcon.DefenseDown;
        }

        public override string GetDescription()
        {
            return $"{Delta:+#;-#;0} Defense Dice";
        }

    }

    /// <summary>
    /// Modifies the number of grapple dice a character has in combat.
    /// </summary>
    public sealed class BuffGrappleDice : SingleDeltaBuff
    {

        public override EBuffIcon GetIcon()
        {
            return Delta >= 0 ? EBuffIcon.GrappleUp : EBuffIcon.GrappleDown;
        }

        public override string GetDescription()
        {
            return $"{Delta:+#;-#;0} Grapple Dice";
        }

    }

    /// <summary>
    /// Modifies the number of swallow dice a character has in combat.
    /// </summary>
    public sealed class BuffSwallowDice : SingleDeltaBuff
    {

        public override EBuffIcon GetIcon()
        {
            return Delta >= 0 ? EBuffIcon.SwallowUp : EBuffIcon.SwallowDown;
        }

        public override string GetDescription()
        {
            return $"{Delta:+#;-#;0} Swallow Dice";
        }

    }

    /// <summary>
    /// Modifies the number of struggle dice a character has in combat.
    /// </summary>
    public sealed class BuffStruggleDice : SingleDeltaBuff
    {

        public override EBuffIcon GetIcon()
        {
            return Delta >= 0 ? EBuffIcon.StruggleUp : EBuffIcon.StruggleDown;
        }

        public override string GetDescription()
        {
            return $"{Delta:+#;-#;0} Struggle Dice";
        }

    }

}
