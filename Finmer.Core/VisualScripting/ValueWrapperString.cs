/*
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
    /// Represents a value container for a string.
    /// </summary>
    public sealed class ValueWrapperString : IFurballSerializable
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
        /// The right-hand operand of the comparison. Context of the value depends on OperandMode.
        /// </summary>
        public string OperandText { get; set; } = String.Empty;

        public void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteEnumProperty(nameof(OperandMode), OperandMode);
            outstream.WriteStringProperty(nameof(OperandText), OperandText);
        }

        public void Deserialize(IFurballContentReader instream)
        {
            OperandMode = instream.ReadEnumProperty<EOperandMode>(nameof(OperandMode));
            OperandText = instream.ReadStringProperty(nameof(OperandText));
        }

        public string GetOperandDescription()
        {
            return OperandMode == EOperandMode.Literal ? $"\"{OperandText}\"" : OperandText;
        }

        public string GetOperandLuaSnippet()
        {
            switch (OperandMode)
            {
                case EOperandMode.Literal:
                    return $"\"{CoreUtility.EscapeLuaString(OperandText)}\"";

                case EOperandMode.Variable:
                    return String.Format(CultureInfo.InvariantCulture, "Storage.GetString(\"{0}\")", OperandText);

                case EOperandMode.Script:
                    return OperandText;

                default:
                    throw new InvalidScriptNodeException("Invalid operand mode");
            }
        }

    }

}
