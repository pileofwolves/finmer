/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
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
    /// Script condition that tests the value of a persistent flag variable.
    /// </summary>
    public sealed class ConditionVarFlag : ScriptCondition
    {

        /// <summary>
        /// The name of the variable to inspect.
        /// </summary>
        public string VariableName { get; set; } = String.Empty;

        /// <summary>
        /// The value to test the variable against.
        /// </summary>
        public ValueWrapperBool Operand { get; set; } = new ValueWrapperBool();

        public override string GetEditorDescription(IContentStore content)
        {
            return $"Flag {VariableName} Equals {Operand.GetOperandDescription()}";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Variable;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.AppendFormat(CultureInfo.InvariantCulture, "Storage.GetFlag(\"{0}\") == {1}", VariableName, Operand.GetOperandLuaSnippet());
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty(nameof(VariableName), VariableName.MakeSafeIdentifier());
            Operand.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            VariableName = instream.ReadStringProperty(nameof(VariableName));
            Operand.Deserialize(instream, version);
        }

    }

}
