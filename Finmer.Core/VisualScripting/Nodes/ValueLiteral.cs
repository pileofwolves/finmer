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
    /// Script value that has a literal value embedded.
    /// </summary>
    public sealed class ValueLiteral : ScriptValue
    {

        /// <summary>
        /// Describes the type of literal.
        /// </summary>
        public enum ELiteralType
        {
            Number,
            String,
            Boolean,
            Nil
        }

        /// <summary>
        /// Returns a new ValueLiteral set to the boolean value 'true'.
        /// </summary>
        public static ValueLiteral TrueValue => new ValueLiteral { LiteralType = ELiteralType.Boolean, BooleanValue = true };

        /// <summary>
        /// The type of literal value.
        /// </summary>
        public ELiteralType LiteralType { get; set; } = ELiteralType.Number;

        /// <summary>
        /// Value of the literal as a number.
        /// </summary>
        public float NumberValue { get; set; }

        /// <summary>
        /// Value of the literal as a string.
        /// </summary>
        public string StringValue { get; set; } = String.Empty;

        /// <summary>
        /// Value of the literal as a boolean.
        /// </summary>
        public bool BooleanValue { get; set; }

        public override string GetEditorDescription()
        {
            switch (LiteralType)
            {
                case ELiteralType.Number:       return $"{NumberValue:F1}";
                case ELiteralType.String:       return $"\"{StringValue}\"";
                case ELiteralType.Boolean:      return BooleanValue ? "true" : "false";
                case ELiteralType.Nil:          return "nil";
                default:                        throw new ArgumentOutOfRangeException();
            }
        }

        public override void EmitLua(StringBuilder output)
        {
            switch (LiteralType)
            {
                case ELiteralType.Number:
                    output.AppendFormat("{0:F5}", NumberValue);
                    break;
                case ELiteralType.String:
                    output.AppendFormat("\"{0}\"", StringValue);
                    break;
                case ELiteralType.Boolean:
                    output.Append(BooleanValue ? "true" : "false");
                    break;
                case ELiteralType.Nil:
                    output.Append("nil");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteEnumProperty("LiteralType", LiteralType);
            switch (LiteralType)
            {
                case ELiteralType.Number:
                    outstream.WriteFloatProperty("Value", NumberValue);
                    break;
                case ELiteralType.String:
                    outstream.WriteStringProperty("Value", StringValue);
                    break;
                case ELiteralType.Boolean:
                    outstream.WriteBooleanProperty("Value", BooleanValue);
                    break;
            }
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            LiteralType = instream.ReadEnumProperty<ELiteralType>("LiteralType");
            switch (LiteralType)
            {
                case ELiteralType.Number:
                    NumberValue = instream.ReadFloatProperty("Value");
                    break;
                case ELiteralType.String:
                    StringValue = instream.ReadStringProperty("Value");
                    break;
                case ELiteralType.Boolean:
                    BooleanValue = instream.ReadBooleanProperty("Value");
                    break;
            }
        }

    }

}
