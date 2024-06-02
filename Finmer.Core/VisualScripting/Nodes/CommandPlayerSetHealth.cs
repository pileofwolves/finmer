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
    /// Command that modifies the player's health.
    /// </summary>
    public sealed class CommandPlayerSetHealth : ScriptCommand
    {

        /// <summary>
        /// Describes what to do with the value.
        /// </summary>
        public enum EOperation
        {
            Add,
            Set
        }

        /// <summary>
        /// The operation to apply to the stat.
        /// </summary>
        public EOperation ValueOperation { get; set; }

        /// <summary>
        /// The numeric value of the stat change.
        /// </summary>
        public ValueWrapperInt Value { get; set; } = new ValueWrapperInt();

        public override string GetEditorDescription(IContentStore content)
        {
            return ValueOperation == EOperation.Add
                ? String.Format(CultureInfo.InvariantCulture, "Add {0} to Player Health", Value.GetOperandDescription())
                : String.Format(CultureInfo.InvariantCulture, "Set Player Health to {0}", Value.GetOperandDescription());
        }

        public override EColor GetEditorColor()
        {
            return EColor.Player;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.Append("Player.Health = ");

            if (ValueOperation == EOperation.Add)
                output.Append("Player.Health + ");

            output.AppendFormat(CultureInfo.InvariantCulture, "{0}", Value.GetOperandLuaSnippet());
            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteEnumProperty(nameof(ValueOperation), ValueOperation);
            Value.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            ValueOperation = instream.ReadEnumProperty<EOperation>(nameof(ValueOperation));
            Value.Deserialize(instream);
        }

    }

}
