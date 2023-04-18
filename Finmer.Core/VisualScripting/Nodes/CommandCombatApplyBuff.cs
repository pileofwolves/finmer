/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Text;
using Finmer.Core.Buffs;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that adds a buff to a combat participant, or to the player's pending buff list.
    /// </summary>
    public sealed class CommandCombatApplyBuff : ScriptCommand
    {

        /// <summary>
        /// Specifies the target to apply a buff to.
        /// </summary>
        public enum ETarget : byte
        {
            Player,
            NPC
        }

        /// <summary>
        /// The target to apply the buff to.
        /// </summary>
        public ETarget Target { get; set; }

        /// <summary>
        /// ID of the NPC participant to apply the buff to, if Target is set to NPC.
        /// </summary>
        public string ParticipantID { get; set; } = String.Empty;

        /// <summary>
        /// The contained buff.
        /// </summary>
        public Buff Effect { get; set; }

        /// <summary>
        /// Participant ID of the prey involved.
        /// </summary>
        public int Duration { get; set; } = 1;

        public override string GetEditorDescription(IContentStore content)
        {
            string buff_text = Effect?.GetDescription() ?? "(Unset)";
            return Target == ETarget.Player
                ? $"Apply '{buff_text}' to Player for {Duration} round(s)"
                : $"Apply '{buff_text}' to NPC {ParticipantID} for {Duration} round(s)";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Combat;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // Contained buff must be valid at this point
            if (Effect == null)
                throw new InvalidScriptNodeException("No buff configured");

            // Check if we require a combat context or not
            if (Target == ETarget.Player)
            {
                // Add the buff to the player object, which will either add it to the pending buff set, or to an active combat
                output.Append("Player:ApplyBuff(");
                output.Append(GetLuaBuffCtor());
                output.AppendLine(")");
            }
            else
            {
                if (String.IsNullOrWhiteSpace(ParticipantID))
                    throw new InvalidScriptNodeException("Buff target is NPC, but no participant ID is set");

                // Add the buff directly to a combat participant
                string participant_variable = CommandCombatBegin.GetParticipantVariableName(ParticipantID);
                output.AppendLine("assert(_combat ~= nil, \"Apply Buff to NPC command can only be used in combat context\")");
                output.AppendLine($"assert({participant_variable} ~= nil, \"No NPC participant with name '{ParticipantID}'\")");
                output.AppendLine($"_combat:ApplyBuff({participant_variable}, {GetLuaBuffCtor()})");
            }
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteEnumProperty(nameof(Target), Target);

            if (Target == ETarget.NPC)
                outstream.WriteStringProperty(nameof(ParticipantID), ParticipantID);

            outstream.WriteNestedObjectProperty(nameof(Effect), Effect);
            outstream.WriteInt32Property(nameof(Duration), Duration);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Target = instream.ReadEnumProperty<ETarget>(nameof(Target));

            if (Target == ETarget.NPC)
                ParticipantID = instream.ReadStringProperty(nameof(ParticipantID));

            Effect = instream.ReadNestedObjectProperty<Buff>(nameof(Effect), version);
            Duration = instream.ReadInt32Property(nameof(Duration));
        }

        private string GetLuaBuffCtor()
        {
            switch (Effect)
            {
                case BuffAttackDice ba:         return $"Buff.AttackDice({ba.Delta}, {Duration})";
                case BuffDefenseDice ba:        return $"Buff.DefenseDice({ba.Delta}, {Duration})";
                case BuffGrappleDice ba:        return $"Buff.GrappleDice({ba.Delta}, {Duration})";
                case BuffSwallowDice ba:        return $"Buff.SwallowDice({ba.Delta}, {Duration})";
                case BuffStruggleDice ba:       return $"Buff.StruggleDice({ba.Delta}, {Duration})";
                case BuffHealthOverTime ba:     return $"Buff.HealthOverTime({ba.Delta}, {Duration})";
                case BuffStun _:                return $"Buff.Stun({Duration})";
                default:                        throw new InvalidScriptNodeException("Unknown buff type");
            }
        }

    }

}
