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
    /// Command that sets a flag variable.
    /// </summary>
    public sealed class CommandVarSetFlag : ScriptCommand
    {

        /// <summary>
        /// The name of the variable to change.
        /// </summary>
        public string VariableName { get; set; } = String.Empty;

        /// <summary>
        /// The new value of the flag.
        /// </summary>
        public ValueWrapperBool Value { get; set; } = new ValueWrapperBool();

        public override string GetEditorDescription(IContentStore content)
        {
            return String.Format(CultureInfo.InvariantCulture, "Set Flag {0} to {1}", VariableName, Value.GetOperandDescription());
        }

        public override EColor GetEditorColor()
        {
            return EColor.Variable;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetFlag(\"{0}\", {1})", VariableName, Value.GetOperandLuaSnippet());
            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty(nameof(VariableName), VariableName);
            Value.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            VariableName = instream.ReadStringProperty(nameof(VariableName));
            Value.Deserialize(instream, version);
        }

    }

}
