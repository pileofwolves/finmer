/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
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
    /// Script condition that tests whether the specified combat participant is swallowed.
    /// </summary>
    public sealed class ConditionCombatParSwallowed : ScriptCondition
    {

        /// <summary>
        /// The name of the participant to inspect.
        /// </summary>
        public string ParticipantName { get; set; } = CombatUtilities.k_PlayerParticipantID;

        /// <summary>
        /// The name of the predator. Optional, may be empty string.
        /// </summary>
        public string PredatorName { get; set; } = String.Empty;

        public override string GetEditorDescription(IContentStore content)
        {
            return String.IsNullOrWhiteSpace(PredatorName)
                ? $"Participant {CombatUtilities.GetEditorParticipantDescription(ParticipantName)} Is Swallowed"
                : $"Participant {CombatUtilities.GetEditorParticipantDescription(ParticipantName)} Is Swallowed By {CombatUtilities.GetEditorParticipantDescription(PredatorName)}";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Combat;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // Emit condition
            if (String.IsNullOrWhiteSpace(PredatorName))
                output.AppendFormat(CultureInfo.InvariantCulture, "_combat:IsSwallowed({0})",
                    CombatUtilities.GetParticipantVariableName(ParticipantName));
            else
                output.AppendFormat(CultureInfo.InvariantCulture, "_combat:GetPredator({0}) == {1}",
                    CombatUtilities.GetParticipantVariableName(ParticipantName),
                    CombatUtilities.GetParticipantVariableName(PredatorName));
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty(nameof(ParticipantName), ParticipantName);
            outstream.WriteStringProperty(nameof(PredatorName), PredatorName);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            ParticipantName = instream.ReadStringProperty(nameof(ParticipantName));
            PredatorName = instream.ReadStringProperty(nameof(PredatorName));
        }

    }

}
