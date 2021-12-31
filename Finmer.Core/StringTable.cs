/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using System.Linq;
using Table = System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>;

namespace Finmer.Core
{

    /// <summary>
    /// Represents a table of strings indexed with string keys.
    /// </summary>
    public sealed class StringTable
    {

        private readonly Table m_Table;

        /// <summary>
        /// Constructs an empty StringTable.
        /// </summary>
        public StringTable()
        {
            m_Table = new Table();
        }

        /// <summary>
        /// Constructs a StringTable that uses a pre-existing dictionary.
        /// </summary>
        public StringTable(Table table)
        {
            m_Table = table;
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
        public Table GetTable()
        {
            return m_Table;
        }

        /// <summary>
        /// Returns a deep copy of the internal dictionary, meaning that changes to the returned copy do not affect the original.
        /// </summary>
        public Table GetTableDeepCopy()
        {
            Table copy = new Table(m_Table.Count);
            foreach (KeyValuePair<string, List<string>> pair in m_Table)
            {
                // Create a deep copy of the List and add it to the new dictionary
                List<string> listcopy = new List<string>(pair.Value.Count);
                pair.Value.ForEach(listcopy.Add);
                copy.Add(pair.Key, listcopy);
            }

            return copy;
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
        /// Removes all records from the StringTable.
        /// </summary>
        public void Clear()
        {
            m_Table.Clear();
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
