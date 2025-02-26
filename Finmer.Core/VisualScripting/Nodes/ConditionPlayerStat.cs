/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Script condition that tests one of the player's primary stats.
    /// </summary>
    public sealed class ConditionPlayerStat : ScriptConditionNumberComparison
    {

        /// <summary>
        /// Identifies a primary stat.
        /// </summary>
        public enum EStat
        {
            Strength,
            Agility,
            Body,
            Wits
        }

        /// <summary>
        /// Describes which stat to obtain.
        /// </summary>
        public EStat Stat { get; set; }

        public override string GetEditorDescription(IContentStore content)
        {
            return $"Player {Stat} Stat {base.GetEditorDescription(content)}";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Player;
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            base.Serialize(outstream);
            outstream.WriteEnumProperty("Stat", Stat);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            base.Deserialize(instream);
            Stat = instream.ReadEnumProperty<EStat>("Stat");
        }

        protected override string GetLeftOperandExpression()
        {
            return "Player." + Stat;
        }

    }

}
