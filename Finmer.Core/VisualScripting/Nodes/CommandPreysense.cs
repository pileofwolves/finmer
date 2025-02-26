
/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Text;
using Finmer.Core.Assets;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that implements the PreySense system.
    /// </summary>
    public sealed class CommandPreysense : ScriptCommand
    {

        /// <summary>
        /// Describes a type of preysense warning.
        /// </summary>
        [Flags]
        public enum ESenseType : ushort
        {
            None                = 0,
            OralVore            = 1 << 0,
            AnalVore            = 1 << 1,
            CockVore            = 1 << 2,
            Unbirth             = 1 << 3,
            Endo                = 1 << 4,
            EndoOrFatal         = 1 << 5,
            DigestionReform     = 1 << 6,
            DigestionFatal      = 1 << 7,
        }

        /// <summary>
        /// Bitfield of activated preysense warnings.
        /// </summary>
        public ESenseType Mode { get; set; } = ESenseType.None;

        /// <summary>
        /// Asset of the creature to display in the preysense warning.
        /// </summary>
        public Guid CreatureGuid { get; set; } = Guid.Empty;

        public override string GetEditorDescription(IContentStore content)
        {
            return $"Preysense '{content.GetAssetName(CreatureGuid)}' for '{Mode}'";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Combat;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // Find the asset for this participant
            var creature = content.GetAssetByID<AssetCreature>(CreatureGuid);
            if (creature == null)
                throw new InvalidScriptNodeException($"Could not find a Creature asset with ID {CreatureGuid}");

            // Emit function call
            output.Append("PreySense(Creature(\"");
            output.Append(creature.Name);
            output.Append("\")");

            // Emit warning types
            for (ESenseType type = ESenseType.OralVore; type <= ESenseType.DigestionFatal; type = (ESenseType)((int)type << 1))
            {
                if (Mode.HasFlag(type))
                {
                    output.Append(",EPreySenseType.");
                    output.Append(type);
                }
            }

            output.Append(')');
            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteCompressedInt32Property(nameof(Mode), (int)Mode);
            outstream.WriteGuidProperty(nameof(CreatureGuid), CreatureGuid);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            Mode = (ESenseType)instream.ReadCompressedInt32Property(nameof(Mode));
            CreatureGuid = instream.ReadGuidProperty(nameof(CreatureGuid));
        }

    }

}
