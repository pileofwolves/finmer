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
        /// Describes the type of the right operand.
        /// </summary>
        public enum EOperandMode
        {
            Literal,
            Variable,
            Script
        }

        /// <summary>
        /// Describes how two operands should be compared.
        /// </summary>
        public enum EOperator
        {
            Equal,
            NotEqual,
            Lesser,
            LesserOrEqual,
            Greater,
            GreaterOrEqual
        }

        /// <summary>
        /// The type of right operand used in the comparison.
        /// </summary>
        public EOperandMode OperandMode { get; set; } = EOperandMode.Literal;

        /// <summary>
        /// The comparison mode to be used for this node.
        /// </summary>
        public EOperator Operator { get; set; } = EOperator.Equal;

        /// <summary>
        /// The right-hand operand of the comparison. Valid if OperandMode is Literal.
        /// </summary>
        public float OperandLiteral { get; set; }

        /// <summary>
        /// The right-hand operand of the comparison. Valid if OperandMode is Variable or Script.
        /// </summary>
        public string OperandText { get; set; } = String.Empty;

        public override string GetEditorDescription()
        {
            string right;
            switch (OperandMode)
            {
                case EOperandMode.Literal:
                    right = String.Format(CultureInfo.InvariantCulture, "{0:F0}", OperandLiteral);
                    break;
                case EOperandMode.Variable:
                case EOperandMode.Script:
                    right = OperandText;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            switch (Operator)
            {
                case EOperator.Equal:                   return $"Equals {right}";
                case EOperator.NotEqual:                return $"Not Equals {right}";
                case EOperator.Lesser:                  return $"< {right}";
                case EOperator.LesserOrEqual:           return $"<= {right}";
                case EOperator.Greater:                 return $"> {right}";
                case EOperator.GreaterOrEqual:          return $">= {right}";
                default:                                throw new InvalidOperationException();
            }
        }

        public sealed override void EmitLua(StringBuilder output, IContentStore content)
        {
            // Emit left-hand operand
            output.Append(GetLeftOperandExpression());

            // Emit operator
            switch (Operator)
            {
                case EOperator.Equal:                   output.Append(" == ");      break;
                case EOperator.NotEqual:                output.Append(" ~= ");      break;
                case EOperator.Lesser:                  output.Append(" < ");       break;
                case EOperator.LesserOrEqual:           output.Append(" <= ");      break;
                case EOperator.Greater:                 output.Append(" > ");       break;
                case EOperator.GreaterOrEqual:          output.Append(" >= ");      break;
                default:                                throw new ArgumentOutOfRangeException();
            }

            // Emit right-hand operand
            switch (OperandMode)
            {
                case EOperandMode.Literal:
                    output.AppendFormat(CultureInfo.InvariantCulture, "{0:F5}", OperandLiteral);
                    break;

                case EOperandMode.Variable:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.GetNumber(\"{0}\")", OperandText);
                    break;

                case EOperandMode.Script:
                    output.Append(OperandText);
                    break;
            }
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteEnumProperty("OperandMode", OperandMode);
            outstream.WriteEnumProperty("Operator", Operator);

            if (OperandMode == EOperandMode.Literal)
                outstream.WriteFloatProperty("OperandLiteral", OperandLiteral);
            else
                outstream.WriteStringProperty("OperandText", OperandText);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            OperandMode = instream.ReadEnumProperty<EOperandMode>("OperandMode");
            Operator = instream.ReadEnumProperty<EOperator>("Operator");

            if (OperandMode == EOperandMode.Literal)
                OperandLiteral = instream.ReadFloatProperty("OperandLiteral");
            else
                OperandText = instream.ReadStringProperty("OperandText");
        }

        /// <summary>
        /// Returns the left-hand expression for the comparison operator.
        /// </summary>
        protected abstract string GetLeftOperandExpression();

    }

}
