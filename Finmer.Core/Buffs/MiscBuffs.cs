/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core.Serialization;

namespace Finmer.Core.Buffs
{

    /// <summary>
    /// Modifies the maximum HP of a character.
    /// </summary>
    public sealed class BuffHealth : SingleDeltaBuff
    {

        public override EBuffIcon GetIcon()
        {
            return EBuffIcon.HealingPerTurn;
        }

        public override string GetDescription()
        {
            return $"{Delta:+#;-#;0} Max Health";
        }

    }

    /// <summary>
    /// Modifies the current HP of a character each turn.
    /// </summary>
    public sealed class BuffHealthOverTime : SingleDeltaBuff
    {

        public override EBuffIcon GetIcon()
        {
            return Delta >= 0 ? EBuffIcon.HealingPerTurn : EBuffIcon.DamagePerTurn;
        }

        public override string GetDescription()
        {
            return $"{Delta:+#;-#;0} Health per Turn";
        }

    }

    /// <summary>
    /// Forces a character to skip combat turns.
    /// </summary>
    public sealed class BuffStun : Buff
    {

        public override EBuffIcon GetIcon()
        {
            return EBuffIcon.Stun;
        }

        public override EBuffImpact GetImpact()
        {
            return EBuffImpact.Negative;
        }

        public override string GetDescription()
        {
            return "Stunned (Skip Next Turn)";
        }

    }

    /// <summary>
    /// Adds an arbitrary text string to the in-game item tooltip.
    /// </summary>
    public sealed class BuffCustomTooltipText : Buff
    {

        /// <summary>
        /// The additional text to display on the tooltip.
        /// </summary>
        public string TooltipText { get; set; } = String.Empty;

        public override EBuffImpact GetImpact()
        {
            return EBuffImpact.Neutral;
        }

        public override string GetDescription()
        {
            return $"Custom Text: \"{TooltipText}\"";
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty(nameof(TooltipText), TooltipText);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            TooltipText = instream.ReadStringProperty(nameof(TooltipText));
        }

    }

}
