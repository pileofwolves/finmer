/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Script value transformer that performs a comparison between two operands.
    /// </summary>
    public sealed class ValueCompare : ScriptValueDualOperand
    {

        /// <summary>
        /// Describes how two operands should be compared.
        /// </summary>
        public enum ECompareMode
        {
            Equal,
            NotEqual,
            Lesser,
            LesserOrEqual,
            Greater,
            GreaterOrEqual
        }

        /// <summary>
        /// The comparison mode to be used for this node.
        /// </summary>
        public ECompareMode Operator { get; set; } = ECompareMode.Equal;

        public override string GetEditorDescription()
        {
            string left = Left?.GetEditorDescription() ?? "[ Not configured ]";
            string right = Right?.GetEditorDescription() ?? "[ Not configured ]";

            switch (Operator)
            {
                case ECompareMode.Equal:
                    return $"{left} Equals {right}";
                case ECompareMode.NotEqual:
                    return $"{left} Not Equals {right}";
                case ECompareMode.Lesser:
                    return $"{left} < {right}";
                case ECompareMode.LesserOrEqual:
                    return $"{left} <= {right}";
                case ECompareMode.Greater:
                    return $"{left} > {right}";
                case ECompareMode.GreaterOrEqual:
                    return $"{left} >= {right}";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void EmitLua(StringBuilder output)
        {
            base.EmitLua(output);

            // Emit left-hand operand
            output.Append('(');
            Left.EmitLua(output);
            output.Append(')');

            // Emit operator
            switch (Operator)
            {
                case ECompareMode.Equal:
                    output.Append(" == ");
                    break;
                case ECompareMode.NotEqual:
                    output.Append(" ~= ");
                    break;
                case ECompareMode.Lesser:
                    output.Append(" < ");
                    break;
                case ECompareMode.LesserOrEqual:
                    output.Append(" <= ");
                    break;
                case ECompareMode.Greater:
                    output.Append(" > ");
                    break;
                case ECompareMode.GreaterOrEqual:
                    output.Append(" >= ");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Emit right-hand operand
            output.Append('(');
            Right.EmitLua(output);
            output.Append(')');
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            base.Serialize(outstream);
            outstream.WriteEnumProperty("Operator", Operator);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            base.Deserialize(instream, version);
            Operator = instream.ReadEnumProperty<ECompareMode>("Operator");
        }

    }

}
