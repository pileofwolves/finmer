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
    /// Command that sets a number variable.
    /// </summary>
    public sealed class CommandVarSetNumber : ScriptCommand
    {

        /// <summary>
        /// Describes what to do with the value.
        /// </summary>
        public enum EOperation
        {
            Add,
            Multiply,
            Divide,
            Set,
            Random,
            SetTimeDay,
            SetTimeHour,
            SetTimeHourTotal,
            SetPlayerStrength,
            SetPlayerAgility,
            SetPlayerBody,
            SetPlayerWits,
            SetPlayerMoney,
            SetPlayerLevel,
            SetPlayerHealth,
            SetPlayerHealthMax,
        }

        /// <summary>
        /// The name of the variable to change.
        /// </summary>
        public string VariableName { get; set; } = String.Empty;

        /// <summary>
        /// The operation to apply to the value.
        /// </summary>
        public EOperation ValueOperation { get; set; } = EOperation.Set;

        /// <summary>
        /// The numeric value of the change.
        /// </summary>
        public ValueWrapperFloat Value { get; set; } = new ValueWrapperFloat();

        public override string GetEditorDescription(IContentStore content)
        {
            var culture = CultureInfo.InvariantCulture;

            switch (ValueOperation)
            {
                case EOperation.Add:                    return String.Format(culture, "Modify Number Variable {0} by {1}", VariableName, Value.GetOperandDescription());
                case EOperation.Multiply:               return String.Format(culture, "Multiply Number Variable {0} with {1}", VariableName, Value.GetOperandDescription());
                case EOperation.Divide:                 return String.Format(culture, "Divide Number Variable {0} by {1}", VariableName, Value.GetOperandDescription());
                case EOperation.Set:                    return String.Format(culture, "Set Number Variable {0} to {1}", VariableName, Value.GetOperandDescription());
                case EOperation.Random:                 return String.Format(culture, "Randomize Number Variable {0} between 0 and {1}", VariableName, Value.GetOperandDescription());
                case EOperation.SetTimeDay:             return String.Format(culture, "Set Number Variable {0} to Current Day", VariableName);
                case EOperation.SetTimeHour:            return String.Format(culture, "Set Number Variable {0} to Current Hour", VariableName);
                case EOperation.SetTimeHourTotal:       return String.Format(culture, "Set Number Variable {0} to Total Hours Passed", VariableName);
                case EOperation.SetPlayerStrength:      return String.Format(culture, "Set Number Variable {0} to Player Strength", VariableName);
                case EOperation.SetPlayerAgility:       return String.Format(culture, "Set Number Variable {0} to Player Agility", VariableName);
                case EOperation.SetPlayerBody:          return String.Format(culture, "Set Number Variable {0} to Player Body", VariableName);
                case EOperation.SetPlayerWits:          return String.Format(culture, "Set Number Variable {0} to Player Wits", VariableName);
                case EOperation.SetPlayerMoney:         return String.Format(culture, "Set Number Variable {0} to Player Money", VariableName);
                case EOperation.SetPlayerLevel:         return String.Format(culture, "Set Number Variable {0} to Player Level", VariableName);
                case EOperation.SetPlayerHealth:        return String.Format(culture, "Set Number Variable {0} to Player Health", VariableName);
                case EOperation.SetPlayerHealthMax:     return String.Format(culture, "Set Number Variable {0} to Player Max Health", VariableName);
                default:                                throw new InvalidScriptNodeException("Invalid operation mode");
            }
        }

        public override EColor GetEditorColor()
        {
            return EColor.Variable;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            switch (ValueOperation)
            {
                case EOperation.Add:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.ModifyNumber(\"{0}\", {1})", VariableName, Value.GetOperandLuaSnippet());
                    break;
                case EOperation.Multiply:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", Storage.GetNumber(\"{0}\") * {1})", VariableName, Value.GetOperandLuaSnippet());
                    break;
                case EOperation.Divide:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", Storage.GetNumber(\"{0}\") / {1})", VariableName, Value.GetOperandLuaSnippet());
                    break;
                case EOperation.Set:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", {1})", VariableName, Value.GetOperandLuaSnippet());
                    break;
                case EOperation.Random:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", math.random(0, {1}))", VariableName, Value.GetOperandLuaSnippet());
                    break;
                case EOperation.SetTimeDay:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", GetTimeDay())", VariableName);
                    break;
                case EOperation.SetTimeHour:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", GetTimeHour())", VariableName);
                    break;
                case EOperation.SetTimeHourTotal:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", GetTimeHourTotal())", VariableName);
                    break;
                case EOperation.SetPlayerStrength:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", Player.Strength)", VariableName);
                    break;
                case EOperation.SetPlayerAgility:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", Player.Agility)", VariableName);
                    break;
                case EOperation.SetPlayerBody:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", Player.Body)", VariableName);
                    break;
                case EOperation.SetPlayerWits:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", Player.Wits)", VariableName);
                    break;
                case EOperation.SetPlayerMoney:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", Player.Money)", VariableName);
                    break;
                case EOperation.SetPlayerLevel:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", Player.Level)", VariableName);
                    break;
                case EOperation.SetPlayerHealth:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", Player.Health)", VariableName);
                    break;
                case EOperation.SetPlayerHealthMax:
                    output.AppendFormat(CultureInfo.InvariantCulture, "Storage.SetNumber(\"{0}\", Player.HealthMax)", VariableName);
                    break;
                default:
                    throw new InvalidScriptNodeException("Invalid value of ValueOperation in CommandVarSetNumber");
            }

            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty(nameof(VariableName), VariableName);
            outstream.WriteEnumProperty(nameof(ValueOperation), ValueOperation);

            if (HasRightOperand())
                Value.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            VariableName = instream.ReadStringProperty(nameof(VariableName));
            ValueOperation = instream.ReadEnumProperty<EOperation>(nameof(ValueOperation));

            if (HasRightOperand() || version < 20)
                Value.Deserialize(instream, version);
        }

        /// <summary>
        /// Indicates whether the currently configured ValueOperation requires a second (right-hand-side) operand.
        /// </summary>
        public bool HasRightOperand()
        {
            switch (ValueOperation)
            {
                case EOperation.SetTimeDay:
                case EOperation.SetTimeHour:
                case EOperation.SetTimeHourTotal:
                case EOperation.SetPlayerStrength:
                case EOperation.SetPlayerAgility:
                case EOperation.SetPlayerBody:
                case EOperation.SetPlayerWits:
                case EOperation.SetPlayerMoney:
                case EOperation.SetPlayerLevel:
                case EOperation.SetPlayerHealth:
                case EOperation.SetPlayerHealthMax:
                    // These specific set operations do not require a second operand
                    return false;

                default:
                    // All others do
                    return true;
            }
        }

    }

}
