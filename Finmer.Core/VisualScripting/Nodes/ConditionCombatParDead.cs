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
    /// Script condition that tests whether the specified combat participant is dead.
    /// </summary>
    public sealed class ConditionCombatParDead : ScriptCondition
    {

        /// <summary>
        /// The name of the participant to inspect.
        /// </summary>
        public string ParticipantName { get; set; } = String.Empty;

        public override string GetEditorDescription()
        {
            return $"Participant '{ParticipantName}' Is Dead";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Combat;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // Emit condition
            output.AppendFormat(CultureInfo.InvariantCulture, "{0}:IsDead()", CommandCombatBegin.GetParticipantVariableName(ParticipantName));
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty(nameof(ParticipantName), ParticipantName.ToLowerInvariant());
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            ParticipantName = instream.ReadStringProperty(nameof(ParticipantName));
        }

    }

}
