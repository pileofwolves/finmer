/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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
        /// Indicates whether the player character participates in this battle.
        /// </summary>
        public bool IncludePlayer { get; set; } = true;

        /// <summary>
        /// The collection of participants in the battle, expressed as a pair of variable name and Creature asset GUID.
        /// </summary>
        public Dictionary<string, Guid> Participants { get; set; } = new Dictionary<string, Guid>();

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

        public override string GetEditorDescription()
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
            foreach (var pair in Participants)
            {
                // Find the asset for this participant
                var creature = content.GetAssetByID<AssetCreature>(pair.Value);
                if (creature == null)
                    throw new InvalidScriptNodeException($"Could not find a Creature asset with ID {pair.Value} (participant \"{pair.Key}\")");

                // Add them as a local variable and participant
                var variable_name = GetParticipantVariableName(pair.Key);
                output.AppendFormat(CultureInfo.InvariantCulture, "local {0} = Creature(\"{1}\")", variable_name, creature.Name);
                output.AppendLine();
                output.AppendFormat(CultureInfo.InvariantCulture, "_combat:AddParticipant({0})", variable_name);
                output.AppendLine();
            }

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
                outstream.WriteStringProperty(@"Variable", participant.Key);
                outstream.WriteGuidProperty(@"Creature", participant.Value);
                outstream.EndObject();
            }
            outstream.EndArray();

            // Serialize the callbacks
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
                Participants.Add(instream.ReadStringProperty(@"Variable"), instream.ReadGuidProperty(@"Creature"));
                instream.EndObject();
            }
            instream.EndArray();

            // Callback bodies
            CallbackRoundEnd = DeserializeOptionalSubgroup(instream, version, nameof(CallbackRoundEnd));
            CallbackPlayerKilled = DeserializeOptionalSubgroup(instream, version, nameof(CallbackPlayerKilled));
            CallbackCreatureKilled = DeserializeOptionalSubgroup(instream, version, nameof(CallbackCreatureKilled));
            CallbackCreatureVored = DeserializeOptionalSubgroup(instream, version, nameof(CallbackCreatureVored));
            CallbackCreatureReleased = DeserializeOptionalSubgroup(instream, version, nameof(CallbackCreatureReleased));
        }

        public override IEnumerable<Subgroup> GetSubgroups()
        {
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
