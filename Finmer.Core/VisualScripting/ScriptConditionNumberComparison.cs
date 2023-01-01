/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
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
        /// The comparison mode to be used for this node.
        /// </summary>
        public EOperator Operator { get; set; } = EOperator.Equal;

        /// <summary>
        /// The right-hand side of the comparison.
        /// </summary>
        public ValueWrapperFloat RightOperand { get; set; } = new ValueWrapperFloat();

        public override string GetEditorDescription(IContentStore content)
        {
            string right = RightOperand.GetOperandDescription();

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
                default:                                throw new InvalidScriptNodeException("Invalid operator mode");
            }

            // Emit right-hand operand
            output.Append(RightOperand.GetOperandLuaSnippet());
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteEnumProperty(nameof(Operator), Operator);
            RightOperand.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Operator = instream.ReadEnumProperty<EOperator>(nameof(Operator));
            RightOperand.Deserialize(instream, version);
        }

        /// <summary>
        /// Returns the left-hand expression for the comparison operator.
        /// </summary>
        protected abstract string GetLeftOperandExpression();

    }

}
