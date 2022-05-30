/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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
    /// Command that modifies the player's money.
    /// </summary>
    public sealed class CommandPlayerSetMoney : ScriptCommand
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
        /// The operation to apply to the value.
        /// </summary>
        public EOperation ValueOperation { get; set; }

        /// <summary>
        /// The numeric value of the change.
        /// </summary>
        public ValueWrapperInt Value { get; set; } = new ValueWrapperInt();

        public override string GetEditorDescription()
        {
            return ValueOperation == EOperation.Add
                ? String.Format(CultureInfo.InvariantCulture, "Modify Player Money by {0}", Value.GetOperandDescription())
                : String.Format(CultureInfo.InvariantCulture, "Set Player Money to {0}", Value.GetOperandDescription());
        }

        public override EColor GetEditorColor()
        {
            return EColor.Player;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.AppendFormat(CultureInfo.InvariantCulture,
                ValueOperation == EOperation.Add
                    ? "Player:ModifyMoney({0})"
                    : "Player.Money = {0}",
                Value.GetOperandLuaSnippet());

            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteEnumProperty(nameof(ValueOperation), ValueOperation);
            Value.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            ValueOperation = instream.ReadEnumProperty<EOperation>(nameof(ValueOperation));
            Value.Deserialize(instream, version);
        }

    }

}
