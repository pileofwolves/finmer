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
    /// Command that modifies one of the player's primary stats.
    /// </summary>
    public sealed class CommandPlayerSetStat : ScriptCommand
    {

        /// <summary>
        /// Describes the stat to influence.
        /// </summary>
        public enum EStat : byte
        {
            Strength,
            Agility,
            Body,
            Wits
        }

        /// <summary>
        /// Describes what to do with the stat.
        /// </summary>
        public enum EOperation : byte
        {
            Add,
            Set
        }

        /// <summary>
        /// The stat to influence.
        /// </summary>
        public EStat Stat { get; set; }

        /// <summary>
        /// The operation to apply to the stat.
        /// </summary>
        public EOperation StatOperation { get; set; }

        /// <summary>
        /// The numeric value of the stat change.
        /// </summary>
        public ValueWrapperInt Value { get; set; } = new ValueWrapperInt();

        public override string GetEditorDescription(IContentStore content)
        {
            return StatOperation == EOperation.Add
                ? String.Format(CultureInfo.InvariantCulture, "Add {0} to Player {1}", Value.GetOperandDescription(), Stat)
                : String.Format(CultureInfo.InvariantCulture, "Set Player {0} to {1}", Stat, Value.GetOperandDescription());
        }

        public override EColor GetEditorColor()
        {
            return EColor.Player;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.AppendFormat(CultureInfo.InvariantCulture, "Player.{0} = ", Stat);

            if (StatOperation == EOperation.Add)
                output.AppendFormat(CultureInfo.InvariantCulture, "Player.{0} + ", Stat);

            output.AppendFormat(CultureInfo.InvariantCulture, "{0}", Value.GetOperandLuaSnippet());
            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteEnumProperty(nameof(Stat), Stat);
            outstream.WriteEnumProperty(nameof(StatOperation), StatOperation);
            Value.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            Stat = instream.GetFormatVersion() >= 21 ? instream.ReadEnumProperty<EStat>(nameof(Stat)) : (EStat)instream.ReadInt32Property(nameof(Stat));
            StatOperation = instream.GetFormatVersion() >= 21 ? instream.ReadEnumProperty<EOperation>(nameof(StatOperation)) : (EOperation)instream.ReadInt32Property(nameof(StatOperation));
            Value.Deserialize(instream);
        }

    }

}
