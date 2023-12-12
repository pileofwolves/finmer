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

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that forces two combat participants in or out of a grapple relationship.
    /// </summary>
    public sealed class CommandCombatSetGrappled : ScriptCommand
    {

        /// <summary>
        /// Describes the attachment mode.
        /// </summary>
        public enum EMode : byte
        {
            Set,
            Unset
        }

        /// <summary>
        /// Describes the attachment mode.
        /// </summary>
        public EMode Mode { get; set; } = EMode.Set;

        /// <summary>
        /// Participant ID of the grapple initiator involved.
        /// </summary>
        public string InstigatorName { get; set; } = CombatUtilities.k_PlayerParticipantID;

        /// <summary>
        /// Participant ID of the grapple target involved.
        /// </summary>
        public string TargetName { get; set; } = String.Empty;

        public override string GetEditorDescription(IContentStore content)
        {
            return $"Force Participant {CombatUtilities.GetEditorParticipantDescription(InstigatorName)} to {(Mode == EMode.Set ? "Grapple" : "Release Grappled")} {CombatUtilities.GetEditorParticipantDescription(TargetName)}";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Combat;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.Append("_combat:");
            output.Append(Mode == EMode.Set ? "SetGrappling" : "UnsetGrappling");
            output.Append('(');

            output.Append(CombatUtilities.GetParticipantVariableName(InstigatorName));
            output.Append(',');
            output.Append(CombatUtilities.GetParticipantVariableName(TargetName));
            output.Append(')');

            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteEnumProperty(nameof(Mode), Mode);
            outstream.WriteStringProperty(nameof(InstigatorName), InstigatorName);
            outstream.WriteStringProperty(nameof(TargetName), TargetName);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Mode = instream.ReadEnumProperty<EMode>(nameof(Mode));
            InstigatorName = instream.ReadStringProperty(nameof(InstigatorName));
            TargetName = instream.ReadStringProperty(nameof(TargetName));
        }

    }

}
