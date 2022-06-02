/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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
    /// Script condition that tests whether the specified combat participant is engaged in a grapple.
    /// </summary>
    public sealed class ConditionCombatParGrappling : ScriptCondition
    {

        /// <summary>
        /// The name of the participant to inspect.
        /// </summary>
        public string ParticipantName { get; set; } = String.Empty;

        /// <summary>
        /// The name of the grapple partner. Optional, may be empty string.
        /// </summary>
        public string TargetName { get; set; } = String.Empty;

        public override string GetEditorDescription()
        {
            return String.IsNullOrWhiteSpace(TargetName)
                ? $"Participant '{ParticipantName}' Is Grappling"
                : $"Participant '{ParticipantName}' Is Grappling With '{TargetName}'";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Combat;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // Emit condition
            if (String.IsNullOrWhiteSpace(TargetName))
                output.AppendFormat(CultureInfo.InvariantCulture, "_combat:IsGrappling({0})",
                    CommandCombatBegin.GetParticipantVariableName(ParticipantName));
            else
                output.AppendFormat(CultureInfo.InvariantCulture, "_combat:GetGrapplingWith({0}) == {1}",
                    CommandCombatBegin.GetParticipantVariableName(ParticipantName),
                    CommandCombatBegin.GetParticipantVariableName(TargetName));
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty(nameof(ParticipantName), ParticipantName.ToLowerInvariant());
            outstream.WriteStringProperty(nameof(TargetName), TargetName.ToLowerInvariant());
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            ParticipantName = instream.ReadStringProperty(nameof(ParticipantName));
            TargetName = instream.ReadStringProperty(nameof(TargetName));
        }

    }

}
