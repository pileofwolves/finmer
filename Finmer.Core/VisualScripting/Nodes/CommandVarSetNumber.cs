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
    /// Command that sets a number variable.
    /// </summary>
    public sealed class CommandVarSetNumber : ScriptCommand
    {

        /// <summary>
        /// Describes what to do with the value.
        /// </summary>
        public enum EOperation
        {
            Add,
            Multiply,
            Divide,
            Set
        }

        /// <summary>
        /// The name of the variable to change.
        /// </summary>
        public string VariableName { get; set; } = String.Empty;

        /// <summary>
        /// The operation to apply to the value.
        /// </summary>
        public EOperation ValueOperation { get; set; } = EOperation.Set;

        /// <summary>
        /// The numeric value of the change.
        /// </summary>
        public ValueWrapperFloat Value { get; set; } = new ValueWrapperFloat();

        public override string GetEditorDescription(IContentStore content)
        {
            switch (ValueOperation)
            {
                case EOperation.Add:
                    return String.Format(CultureInfo.InvariantCulture, "Modify Variable {0} by {1}", VariableName, Value.GetOperandDescription());
                case EOperation.Multiply:
                    return String.Format(CultureInfo.InvariantCulture, "Multiply Variable {0} with {1}", VariableName, Value.GetOperandDescription());
                case EOperation.Divide:
                    return String.Format(CultureInfo.InvariantCulture, "Divide Variable {0} by {1}", VariableName, Value.GetOperandDescription());
                case EOperation.Set:
                    return String.Format(CultureInfo.InvariantCulture, "Set Variable {0} to {1}", VariableName, Value.GetOperandDescription());
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override EColor GetEditorColor()
        {
            return EColor.Variable;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            switch (ValueOperation)
            {
                case EOperation.Add:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.ModifyNumber(\"{0}\", {1})",
                        VariableName, Value.GetOperandLuaSnippet());
                    break;
                case EOperation.Multiply:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", Storage.GetNumber(\"{0}\") * {1})",
                        VariableName, Value.GetOperandLuaSnippet());
                    break;
                case EOperation.Divide:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", Storage.GetNumber(\"{0}\") / {1})",
                        VariableName, Value.GetOperandLuaSnippet());
                    break;
                case EOperation.Set:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", {1})",
                        VariableName, Value.GetOperandLuaSnippet());
                    break;
                default:
                    throw new InvalidScriptNodeException("Invalid value of ValueOperation in CommandVarSetNumber");
            }

            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty(nameof(VariableName), VariableName);
            outstream.WriteEnumProperty(nameof(ValueOperation), ValueOperation);
            Value.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            VariableName = instream.ReadStringProperty(nameof(VariableName));
            ValueOperation = instream.ReadEnumProperty<EOperation>(nameof(ValueOperation));
            Value.Deserialize(instream, version);
        }

    }

}
