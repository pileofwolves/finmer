﻿/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Globalization;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting
{

    /// <summary>
    /// Represents a value container for a boolean.
    /// </summary>
    public sealed class ValueWrapperBool : IFurballSerializable
    {

        /// <summary>
        /// Describes the type of the right operand.
        /// </summary>
        public enum EOperandMode : byte
        {
            Literal,
            Variable,
            Script
        }

        /// <summary>
        /// The type of right operand used in the comparison.
        /// </summary>
        public EOperandMode OperandMode { get; set; } = EOperandMode.Literal;

        /// <summary>
        /// The right-hand operand of the comparison. Valid if OperandMode is Literal.
        /// </summary>
        public bool OperandLiteral { get; set; }

        /// <summary>
        /// The right-hand operand of the comparison. Valid if OperandMode is Variable or Script.
        /// </summary>
        public string OperandText { get; set; } = String.Empty;

        public void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteEnumProperty(nameof(OperandMode), OperandMode);

            if (OperandMode == EOperandMode.Literal)
                outstream.WriteBooleanProperty(nameof(OperandLiteral), OperandLiteral);
            else
                outstream.WriteStringProperty(nameof(OperandText), OperandText);
        }

        public void Deserialize(IFurballContentReader instream)
        {
            OperandMode = instream.ReadEnumProperty<EOperandMode>(nameof(OperandMode));

            if (OperandMode == EOperandMode.Literal)
                OperandLiteral = instream.ReadBooleanProperty(nameof(OperandLiteral));
            else
                OperandText = instream.ReadStringProperty(nameof(OperandText));
        }

        public string GetOperandDescription()
        {
            switch (OperandMode)
            {
                case EOperandMode.Literal:
                    return OperandLiteral ? "True" : "False";

                case EOperandMode.Variable:
                case EOperandMode.Script:
                    return OperandText;

                default:
                    throw new InvalidScriptNodeException("Invalid operand mode");
            }
        }

        public string GetOperandLuaSnippet()
        {
            switch (OperandMode)
            {
                case EOperandMode.Literal:
                    return OperandLiteral ? "true" : "false";

                case EOperandMode.Variable:
                    return String.Format(CultureInfo.InvariantCulture, "Storage.GetFlag(\"{0}\")", OperandText);

                case EOperandMode.Script:
                    return OperandText;

                default:
                    throw new InvalidScriptNodeException("Invalid operand mode");
            }
        }

    }

}
