﻿/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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

        public override string GetEditorDescription()
        {
            return $"{Delta:+#;-#;0} Max Health";
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

        public override EImpact GetImpact()
        {
            return EImpact.Neutral;
        }

        public override string GetEditorDescription()
        {
            return $"Custom Text: \"{TooltipText}\"";
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty(nameof(TooltipText), TooltipText);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            TooltipText = instream.ReadStringProperty(nameof(TooltipText));
        }

    }

}