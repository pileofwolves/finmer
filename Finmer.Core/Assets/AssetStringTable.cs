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
    /// Contains a <seealso cref="StringTable" />.
    /// </summary>
    public class AssetStringTable : AssetBase
    {

        public StringTable Table { get; set; } = new StringTable();

        public override void Serialize(IFurballContentWriter outstream)
        {
            base.Serialize(outstream);

            // Get the collection of string table entries
            Dictionary<string, List<string>> entries = Table.GetTable();

            // Alphabetically sort the keys, to ensure they are laid out deterministically. This is useful for source control,
            // since this will ensure that different memory layouts will not cause unnecessarily big file deltas.
            List<string> sorted_keys = entries.Keys.ToList();
            sorted_keys.Sort();

            // Write the table to the output stream
            outstream.BeginArray("Entries", entries.Count);
            foreach (var key in sorted_keys)
            {
                // Search for the string table entry matching this key (remember, the table is a hashmap, but we want sorted keys)
                var entry = entries[key];

                // Write each individual text collection
                outstream.BeginObject();
                {
                    outstream.WriteStringProperty("Key", key);
                    outstream.BeginArray("Text", entry.Count);
                    foreach (var text in entry)
                        outstream.WriteStringValue(text);
                    outstream.EndArray(); // Text
                }
                outstream.EndObject(); // array element
            }
            outstream.EndArray(); // Entries
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            base.Deserialize(instream, version);

            Table = new StringTable();

            for (int count = instream.BeginArray("Entries"); count > 0; count--)
            {
                instream.BeginObject();

                // Read entry key and number of subentries
                string key = instream.ReadStringProperty("Key");

                // Read all strings into the table
                for (int subentries = instream.BeginArray("Text"); subentries > 0; subentries--)
                {
                    Table.Add(key, instream.ReadStringValue());
                }

                instream.EndArray();
                instream.EndObject();
            }
            instream.EndArray();
        }

    }

}
