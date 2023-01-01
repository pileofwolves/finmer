/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Globalization;
using System.Text;
using Finmer.Core.Assets;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that sets a grammar context.
    /// </summary>
    public sealed class CommandGrammarSetContext : ScriptCommand
    {

        /// <summary>
        /// The name of the variable to change.
        /// </summary>
        public string VariableName { get; set; } = String.Empty;

        /// <summary>
        /// The creature asset to map to the grammar context.
        /// </summary>
        public Guid CreatureGuid { get; set; } = Guid.Empty;

        public override string GetEditorDescription(IContentStore content)
        {
            return $"Set Grammar Context {VariableName} to {CreatureGuid}";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Message;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // Resolve the asset GUID
            var creature = content.GetAssetByID<AssetCreature>(CreatureGuid);
            if (creature == null)
                throw new InvalidScriptNodeException($"Could not find Creature asset with ID {CreatureGuid}");

            // Emit command
            output.AppendFormat(CultureInfo.InvariantCulture, "Text.SetContext(\"{0}\", Creature(\"{1}\"))", VariableName, creature.Name);
            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty(nameof(VariableName), VariableName);
            outstream.WriteGuidProperty(nameof(CreatureGuid), CreatureGuid);
        }


        public override void Deserialize(IFurballContentReader instream, int version)
        {
            VariableName = instream.ReadStringProperty(nameof(VariableName));
            CreatureGuid = instream.ReadGuidProperty(nameof(CreatureGuid));
        }

    }

}
