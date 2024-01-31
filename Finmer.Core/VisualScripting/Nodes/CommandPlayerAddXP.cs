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
    /// Command that grants the player XP.
    /// </summary>
    public sealed class CommandPlayerAddXP : ScriptCommand
    {

        /// <summary>
        /// The numeric value of the change.
        /// </summary>
        public ValueWrapperInt Value { get; set; } = new ValueWrapperInt();

        public override string GetEditorDescription(IContentStore content)
        {
            return String.Format(CultureInfo.InvariantCulture, "Grant {0} XP to Player", Value.GetOperandDescription());
        }

        public override EColor GetEditorColor()
        {
            return EColor.Player;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.Append("Player:AwardXP(");
            output.AppendFormat(CultureInfo.InvariantCulture, "{0}", Value.GetOperandLuaSnippet());
            output.AppendLine(")");
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            Value.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Value.Deserialize(instream, version);
        }

    }

}
