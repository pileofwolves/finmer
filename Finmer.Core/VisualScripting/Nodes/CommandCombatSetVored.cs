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
    /// Command that forces two combat participants in or out of a vore relationship.
    /// </summary>
    public sealed class CommandCombatSetVored : ScriptCommand
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
        /// Participant ID of the predator involved.
        /// </summary>
        public string PredatorName { get; set; } = CombatUtilities.k_PlayerParticipantID;

        /// <summary>
        /// Participant ID of the prey involved.
        /// </summary>
        public string PreyName { get; set; } = String.Empty;

        public override string GetEditorDescription(IContentStore content)
        {
            return Mode == EMode.Set
                ? $"Force '{PredatorName}' to Swallow Prey '{PreyName}'"
                : $"Force '{PredatorName}' to Release Prey '{PreyName}'";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Combat;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.Append("_combat:");
            output.Append(Mode == EMode.Set ? "SetVored" : "UnsetVored");
            output.Append('(');

            output.Append(CombatUtilities.GetParticipantVariableName(PredatorName));
            output.Append(',');
            output.Append(CombatUtilities.GetParticipantVariableName(PreyName));
            output.Append(')');

            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteEnumProperty(nameof(Mode), Mode);
            outstream.WriteStringProperty(nameof(PredatorName), PredatorName);
            outstream.WriteStringProperty(nameof(PreyName), PreyName);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Mode = instream.ReadEnumProperty<EMode>(nameof(Mode));
            PredatorName = instream.ReadStringProperty(nameof(PredatorName));
            PreyName = instream.ReadStringProperty(nameof(PreyName));
        }

    }

}
