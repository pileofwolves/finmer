/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using System.Linq;
using TDictionary = System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>;

namespace Finmer.Core
{

    /// <summary>
    /// Represents a table of strings indexed with string keys.
    /// </summary>
    public sealed class StringTable
    {

        private readonly TDictionary m_Table;

        /// <summary>
        /// Constructs an empty StringTable.
        /// </summary>
        public StringTable()
        {
            m_Table = new TDictionary();
        }

        /// <summary>
        /// Adds an entry to the string table.
        /// </summary>
        /// <param name="key">The table key with which to access this entry.</param>
        /// <param name="text">The entry to add to the table.</param>
        public void Add(string key, string text)
        {
            key = key.ToUpperInvariant();

            if (m_Table.TryGetValue(key, out List<string> list))
                // Existing table - add entry
                list.Add(text);
            else
                // New table - create it
                m_Table.Add(key, new List<string> { text });
        }

        /// <summary>
        /// Returns a random entry from the given table.
        /// </summary>
        /// <param name="key">A table key. Not case-sensitive.</param>
        public string GetRandomEntry(string key)
        {
            key = key.ToUpperInvariant();

            if (m_Table.TryGetValue(key, out List<string> list))
                return list[CoreUtility.Rng.Next(list.Count)];

            return $"[missing text: '{key}']";
        }

        /// <summary>
        /// Returns a shallow copy of the internal dictionary.
        /// </summary>
        public TDictionary GetDictionary()
        {
            return m_Table;
        }

        /// <summary>
        /// Merges all entries in the given StringTable into this one.
        /// </summary>
        /// <param name="other">The StringTable to merge.</param>
        /// <returns>Returns self, to enable call chaining.</returns>
        public void Merge(StringTable other)
        {
            foreach (KeyValuePair<string, List<string>> pair in other.m_Table)
                foreach (string line in pair.Value)
                    Add(pair.Key, line);
        }

        /// <summary>
        /// Returns a string that gives an overview of this table.
        /// </summary>
        public override string ToString()
        {
            return $"{m_Table.Count} tables, {m_Table.Sum(item => item.Value.Count)} total entries";
        }

    }

}
