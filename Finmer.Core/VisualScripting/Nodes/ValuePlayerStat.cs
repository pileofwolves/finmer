/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Script value that returns one of the player's primary stats.
    /// </summary>
    public sealed class ValuePlayerStat : ScriptValue
    {

        /// <summary>
        /// Identifies a primary stat.
        /// </summary>
        public enum EStat
        {
            Strength,
            Agility,
            Body,
            Wits
        }

        /// <summary>
        /// Describes which stat to obtain.
        /// </summary>
        public EStat Stat { get; set; }

        public override string GetEditorDescription()
        {
            return "Player " + Stat;
        }

        public override void EmitLua(StringBuilder output)
        {
            output.Append("Player.");
            output.Append(Stat.ToString());
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteEnumProperty("Stat", Stat);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Stat = instream.ReadEnumProperty<EStat>("Stat");
        }

    }

}
