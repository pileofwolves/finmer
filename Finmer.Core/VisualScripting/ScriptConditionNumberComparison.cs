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

namespace Finmer.Core.VisualScripting
{

    /// <summary>
    /// Base class for script value transformers that operate on two operands.
    /// </summary>
    public abstract class ScriptConditionNumberComparison : ScriptCondition
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

        /// <summary>
        /// The right-hand operand of the comparison.
        /// </summary>
        public float Operand { get; set; }

        public override string GetEditorDescription()
        {
            string right = String.Format(CultureInfo.InvariantCulture, "{0:F0}", Operand);
            switch (Operator)
            {
                case ECompareMode.Equal:                return $"Equals {right}";
                case ECompareMode.NotEqual:             return $"Not Equals {right}";
                case ECompareMode.Lesser:               return $"< {right}";
                case ECompareMode.LesserOrEqual:        return $"<= {right}";
                case ECompareMode.Greater:              return $"> {right}";
                case ECompareMode.GreaterOrEqual:       return $">= {right}";
                default:                                throw new ArgumentOutOfRangeException();
            }
        }

        public sealed override void EmitLua(StringBuilder output)
        {
            // Emit left-hand operand
            output.Append(GetLeftOperandExpression());

            // Emit operator
            switch (Operator)
            {
                case ECompareMode.Equal:                output.Append(" == ");      break;
                case ECompareMode.NotEqual:             output.Append(" ~= ");      break;
                case ECompareMode.Lesser:               output.Append(" < ");       break;
                case ECompareMode.LesserOrEqual:        output.Append(" <= ");      break;
                case ECompareMode.Greater:              output.Append(" > ");       break;
                case ECompareMode.GreaterOrEqual:       output.Append(" >= ");      break;
                default:                                throw new ArgumentOutOfRangeException();
            }

            // Emit right-hand operand
            output.AppendFormat(CultureInfo.InvariantCulture, "{0:F5}", Operand);
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteEnumProperty("Operator", Operator);
            outstream.WriteFloatProperty("Operand", Operand);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Operator = instream.ReadEnumProperty<ECompareMode>("Operator");
            Operand = instream.ReadFloatProperty("Operand");
        }

        /// <summary>
        /// Returns the left-hand expression for the comparison operator.
        /// </summary>
        protected abstract string GetLeftOperandExpression();

    }

}
