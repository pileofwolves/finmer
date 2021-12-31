/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using System.Linq;
using Finmer.Core.Serialization;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Describes a quest journal, with all entries pertaining to a single questline.
    /// </summary>
    public class AssetJournal : AssetBase
    {

        /// <summary>
        /// Represents an entry as part of a quest journal.
        /// </summary>
        public struct QuestStage
        {

            public int Key { get; set; }

            public string Text { get; set; }

        }

        /// <summary>
        /// Gets or sets the summary of the journal collection.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A collection of key/value pairs used to reconstruct the asset.
        /// </summary>
        public List<QuestStage> Stages { get; } = new List<QuestStage>();

        public override void Serialize(IFurballContentWriter outstream)
        {
            base.Serialize(outstream);

            // Write metadata
            outstream.WriteStringProperty("Title", Title);

            // Write all quest stages
            outstream.BeginArray("Stages", Stages.Count);
            foreach (QuestStage stage in Stages)
            {
                outstream.BeginObject();
                outstream.WriteInt32Property("Key", stage.Key);
                outstream.WriteStringProperty("Text", stage.Text);
                outstream.EndObject();
            }
            outstream.EndArray();
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            base.Deserialize(instream, version);

            // Read metadata
            Title = instream.ReadStringProperty("Title");

            // Read stages
            Stages.Clear();
            for (int count = instream.BeginArray("Stages"); count > 0; count--)
            {
                instream.BeginObject();
                QuestStage stage = new QuestStage
                {
                    Key = instream.ReadInt32Property("Key"),
                    Text = instream.ReadStringProperty("Text")
                };
                Stages.Add(stage);
                instream.EndObject();
            }
            instream.EndArray();
        }

        public string GetEntryForStage(int stage)
        {
            return Stages.FirstOrDefault(entry => entry.Key == stage).Text ?? "<MISSING ENTRY>";
        }

    }

}
