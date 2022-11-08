/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Globalization;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Script condition that tests the value of a persistent number variable.
    /// </summary>
    public sealed class ConditionVarNumber : ScriptConditionNumberComparison
    {

        /// <summary>
        /// The name of the variable to inspect.
        /// </summary>
        public string VariableName { get; set; } = String.Empty;

        public override string GetEditorDescription(IContentStore content)
        {
            return String.Format(CultureInfo.InvariantCulture, "Variable {0} {1}", VariableName, base.GetEditorDescription(content));
        }

        public override EColor GetEditorColor()
        {
            return EColor.Variable;
        }

        protected override string GetLeftOperandExpression()
        {
            return String.Format(CultureInfo.InvariantCulture, "Storage.GetNumber(\"{0}\")", VariableName);
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty(nameof(VariableName), VariableName.ToUpperInvariant());
            base.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            VariableName = instream.ReadStringProperty(nameof(VariableName));
            base.Deserialize(instream, version);
        }

    }

}
