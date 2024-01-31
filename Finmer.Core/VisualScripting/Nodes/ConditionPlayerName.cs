/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Globalization;
using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Script condition that tests the player character's name.
    /// </summary>
    public sealed class ConditionPlayerName : ScriptCondition
    {

        /// <summary>
        /// The right-hand operand to test the player name against.
        /// </summary>
        public ValueWrapperString Comparison { get; set; } = new ValueWrapperString();

        /// <summary>
        /// Whether the comparison should be case-sensitive.
        /// </summary>
        public bool IsCaseSensitive { get; set; }

        public override string GetEditorDescription(IContentStore content)
        {
            return $"Player Name Equals {Comparison.GetOperandDescription()}";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Player;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            string ci_wrap = IsCaseSensitive ? String.Empty : "string.lower";
            output.AppendFormat(CultureInfo.InvariantCulture, "{0}(Player.Name) == {0}({1})", ci_wrap, Comparison.GetOperandLuaSnippet());
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            Comparison.Serialize(outstream);
            outstream.WriteBooleanProperty(nameof(IsCaseSensitive), IsCaseSensitive);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Comparison.Deserialize(instream, version);
            IsCaseSensitive = instream.ReadBooleanProperty(nameof(IsCaseSensitive));
        }

    }

}
