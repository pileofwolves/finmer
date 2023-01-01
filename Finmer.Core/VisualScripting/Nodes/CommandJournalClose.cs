/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
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
    /// Command that removes a quest from the journal.
    /// </summary>
    public sealed class CommandJournalClose : ScriptCommand
    {

        /// <summary>
        /// Describes the journal group to modify.
        /// </summary>
        public Guid JournalGuid { get; set; } = Guid.Empty;

        public override string GetEditorDescription(IContentStore content)
        {
            return "Remove Quest " + JournalGuid;
        }

        public override EColor GetEditorColor()
        {
            return EColor.Journal;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // Resolve the journal asset GUID
            var journal = content.GetAssetByID<AssetJournal>(JournalGuid);
            if (journal == null)
                throw new InvalidScriptNodeException($"Could not find Journal asset with ID {JournalGuid}");

            // Emit command
            output.Append("Journal.Close(\"");
            output.Append(journal.Name);
            output.Append("\")");
            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteGuidProperty(nameof(JournalGuid), JournalGuid);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            JournalGuid = instream.ReadGuidProperty(nameof(JournalGuid));
        }

    }

}
