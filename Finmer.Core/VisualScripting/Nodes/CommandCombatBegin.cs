/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Finmer.Core.Assets;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that encapsulates the combat system.
    /// </summary>
    public sealed class CommandCombatBegin : ScriptCommandContainer
    {

        /// <summary>
        /// Describes an NPC participant.
        /// </summary>
        public struct Participant
        {

            /// <summary>
            /// Participant ID, used for code generation.
            /// </summary>
            public string ID { get; set; }

            /// <summary>
            /// Template creature asset.
            /// </summary>
            public Guid Creature { get; set; }

            /// <summary>
            /// Whether the participant is allied with the player's faction.
            /// </summary>
            public bool IsAlly { get; set; }

        }

        /// <summary>
        /// Indicates whether the player character participates in this battle.
        /// </summary>
        public bool IncludePlayer { get; set; } = true;

        /// <summary>
        /// The collection of participants in the battle, expressed as a pair of variable name and Creature asset GUID.
        /// </summary>
        public List<Participant> Participants { get; set; } = new List<Participant>();

        /// <summary>
        /// User script function. May be null if callback is not configured.
        /// </summary>
        public List<ScriptNode> CallbackCombatStart { get; set; }

        /// <summary>
        /// User script function. May be null if callback is not configured.
        /// </summary>
        public List<ScriptNode> CallbackRoundEnd { get; set; }

        /// <summary>
        /// User script function. May be null if callback is not configured.
        /// </summary>
        public List<ScriptNode> CallbackPlayerKilled { get; set; }

        /// <summary>
        /// User script function. May be null if callback is not configured.
        /// </summary>
        public List<ScriptNode> CallbackCreatureKilled { get; set; }

        /// <summary>
        /// User script function. May be null if callback is not configured.
        /// </summary>
        public List<ScriptNode> CallbackCreatureVored { get; set; }

        /// <summary>
        /// User script function. May be null if callback is not configured.
        /// </summary>
        public List<ScriptNode> CallbackCreatureReleased { get; set; }

        public override string GetEditorDescription(IContentStore content)
        {
            return String.Format(CultureInfo.InvariantCulture, "Start Combat with {0} Participants", Participants.Count + (IncludePlayer ? 1 : 0));
        }

        public override EColor GetEditorColor()
        {
            return EColor.Combat;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // Start enclosing scope, so we can define local variables
            output.AppendLine("do");
            output.AppendLine("local _combat = Combat2()");

            // Add player
            if (IncludePlayer)
            {
                // Emit a local variable that creators can use to easily refer to the PC
                output.AppendFormat(CultureInfo.InvariantCulture, "local {0} = Player", GetParticipantVariableName("player"));
                output.AppendLine();
                output.AppendLine("_combat:AddParticipant(Player)");
            }

            // Add AI participants
            foreach (var participant in Participants)
            {
                // Validate participant name
                if (String.IsNullOrWhiteSpace(participant.ID))
                    throw new InvalidScriptNodeException($"Combat participant has no name (creature asset {content.GetAssetName(participant.Creature)})");

                // Find the asset for this participant
                var creature = content.GetAssetByID<AssetCreature>(participant.Creature);
                if (creature == null)
                    throw new InvalidScriptNodeException($"Could not find a Creature asset with ID {participant.Creature} (participant \"{participant.ID}\")");

                // Add them as a local variable and participant
                var variable_name = GetParticipantVariableName(participant.ID);
                output.AppendFormat(CultureInfo.InvariantCulture, "local {0} = Creature(\"{1}\")", variable_name, creature.Name);
                output.AppendLine();
                output.AppendFormat(CultureInfo.InvariantCulture, "_combat:AddParticipant({0})", variable_name);
                output.AppendLine();

                // If marked as ally, emit code to flip the 'allied' flag
                if (participant.IsAlly)
                {
                    output.AppendFormat(CultureInfo.InvariantCulture, "{0}.IsAlly = true", variable_name);
                    output.AppendLine();
                }
            }

            // Emit start callback (which is just some custom script in between setup and the Begin call)
            if (CallbackCombatStart != null)
                foreach (var node in CallbackCombatStart)
                    node.EmitLua(output, content);

            // Attach user callbacks
            EmitCallbackFunction(output, content, "OnRoundEnd", CallbackRoundEnd);
            EmitCallbackFunction(output, content, "OnPlayerKilled", CallbackPlayerKilled);
            EmitCallbackFunction(output, content, "OnCreatureKilled", CallbackCreatureKilled);
            EmitCallbackFunction(output, content, "OnCreatureVored", CallbackCreatureVored);
            EmitCallbackFunction(output, content, "OnCreatureReleased", CallbackCreatureReleased);

            output.AppendLine("_combat:Begin()");
            output.AppendLine("end");
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            // Basic configuration
            outstream.WriteBooleanProperty(nameof(IncludePlayer), IncludePlayer);
            outstream.WriteBooleanProperty(@"IncludeAllies", IncludePlayer); // Reserved

            // Participant list
            outstream.BeginArray(nameof(Participants), Participants.Count);
            foreach (var participant in Participants)
            {
                outstream.BeginObject();
                outstream.WriteStringProperty(@"Variable", participant.ID);
                outstream.WriteGuidProperty(@"Creature", participant.Creature);
                outstream.WriteBooleanProperty(@"IsAlly", participant.IsAlly);
                outstream.EndObject();
            }
            outstream.EndArray();

            // Serialize the callbacks
            SerializeOptionalSubgroup(outstream, nameof(CallbackCombatStart), CallbackCombatStart);
            SerializeOptionalSubgroup(outstream, nameof(CallbackRoundEnd), CallbackRoundEnd);
            SerializeOptionalSubgroup(outstream, nameof(CallbackPlayerKilled), CallbackPlayerKilled);
            SerializeOptionalSubgroup(outstream, nameof(CallbackCreatureKilled), CallbackCreatureKilled);
            SerializeOptionalSubgroup(outstream, nameof(CallbackCreatureVored), CallbackCreatureVored);
            SerializeOptionalSubgroup(outstream, nameof(CallbackCreatureReleased), CallbackCreatureReleased);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            // Basic configuration
            IncludePlayer = instream.ReadBooleanProperty(nameof(IncludePlayer));
            instream.ReadBooleanProperty(@"IncludeAllies"); // Reserved

            // Participant list
            for (int i = 0, c = instream.BeginArray(nameof(Participants)); i < c; i++)
            {
                instream.BeginObject();
                Participants.Add(new Participant
                {
                    ID = instream.ReadStringProperty(@"Variable"),
                    Creature = instream.ReadGuidProperty(@"Creature"),
                    IsAlly = version >= 18 && instream.ReadBooleanProperty(@"IsAlly")
                });
                instream.EndObject();
            }
            instream.EndArray();

            // Callback bodies
            if (version >= 17)
                CallbackCombatStart = DeserializeOptionalSubgroup(instream, version, nameof(CallbackCombatStart));
            CallbackRoundEnd = DeserializeOptionalSubgroup(instream, version, nameof(CallbackRoundEnd));
            CallbackPlayerKilled = DeserializeOptionalSubgroup(instream, version, nameof(CallbackPlayerKilled));
            CallbackCreatureKilled = DeserializeOptionalSubgroup(instream, version, nameof(CallbackCreatureKilled));
            CallbackCreatureVored = DeserializeOptionalSubgroup(instream, version, nameof(CallbackCreatureVored));
            CallbackCreatureReleased = DeserializeOptionalSubgroup(instream, version, nameof(CallbackCreatureReleased));
        }

        public override IEnumerable<Subgroup> GetSubgroups()
        {
            if (CallbackCombatStart != null)
            {
                yield return new Subgroup
                {
                    EditorPrefix = "Before Combat Starts:",
                    Nodes = CallbackCombatStart
                };
            }
            if (CallbackRoundEnd != null)
            {
                yield return new Subgroup
                {
                    EditorPrefix = "When Any Round Ends:",
                    Nodes = CallbackRoundEnd
                };
            }
            if (CallbackPlayerKilled != null)
            {
                yield return new Subgroup
                {
                    EditorPrefix = "When Player Dies:",
                    Nodes = CallbackPlayerKilled
                };
            }
            if (CallbackCreatureKilled != null)
            {
                yield return new Subgroup
                {
                    EditorPrefix = "When Any NPC Dies:",
                    Nodes = CallbackCreatureKilled
                };
            }
            if (CallbackCreatureVored != null)
            {
                yield return new Subgroup
                {
                    EditorPrefix = "When Any Creature is Swallowed:",
                    Nodes = CallbackCreatureVored
                };
            }
            if (CallbackCreatureReleased != null)
            {
                yield return new Subgroup
                {
                    EditorPrefix = "When Any Creature is Released:",
                    Nodes = CallbackCreatureReleased
                };
            }
        }

        /// <summary>
        /// Returns a Lua local variable name identifying a participant, given a unique participant key.
        /// </summary>
        internal static string GetParticipantVariableName(string id)
        {
            return "_par_" + id.ToLowerInvariant();
        }

        private void SerializeOptionalSubgroup(IFurballContentWriter outstream, string name, List<ScriptNode> nodes)
        {
            // Write a flag indicating whether the callback is specified
            bool has_callback = nodes != null;
            outstream.WriteBooleanProperty("Has" + name, has_callback);

            // If callback exists, serialize the nodes as a subgroup
            if (has_callback)
                SerializeSubgroup(outstream, name, nodes);
        }

        private List<ScriptNode> DeserializeOptionalSubgroup(IFurballContentReader instream, int version, string name)
        {
            // Read the presence flag
            bool has_callback = instream.ReadBooleanProperty("Has" + name);
            if (!has_callback)
                return null;

            // If callback exists, deserialize the subgroup
            return DeserializeSubgroup(instream, version, name);
        }

        private void EmitCallbackFunction(StringBuilder output, IContentStore content, string name, List<ScriptNode> body)
        {
            // Don't register callbacks for empty functions
            if (body == null || body.Count == 0)
                return;

            // Outer boilerplate
            // Note: We do not yet provide a way to pass arguments to the callback function because this is tricky to encode
            // in the visual scripting framework. If strictly necessary, the user will have to fall back to plain Lua.
            output.Append("_combat:");
            output.Append(name);
            output.AppendLine("(function()");

            // Emit function body
            foreach (var node in body)
                node.EmitLua(output, content);

            // Close function
            output.AppendLine("end)");
        }

    }

}
