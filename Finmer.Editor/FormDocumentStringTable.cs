/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Finmer.Core;
using Finmer.Core.Assets;
using Finmer.Core.Serialization;

namespace Finmer.Editor
{

    public partial class FormDocumentStringTable : AssetWindow
    {

        private string m_EditingKey;
        private bool m_IgnoreDirtiedText;

        private AssetStringTable m_AssetStringTable;
        private Dictionary<string, List<string>> m_RawDictionary;

        private static readonly Regex s_IncrementingKeyRegex = new Regex(@"^(.+?)(\d+)$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public FormDocumentStringTable()
        {
            InitializeComponent();
        }

        private void FormDocumentStringTable_Load(object sender, EventArgs e)
        {
            ScintillaHelper.Setup(scintilla, ScintillaHelper.EScintillaStyle.PlainText);

            // Duplicate the source asset so we can safely modify it in-memory
            m_AssetStringTable = AssetSerializer.DuplicateAsset((AssetStringTable)Asset);
            m_RawDictionary = m_AssetStringTable.Table.GetDictionary();

            // Populate the list control with the table's entries
            m_RawDictionary.Keys.ForEach(key => lstKeys.Items.Add(key));
            lstKeys.Sort();

            // Populate stats label
            UpdateStats();
        }

        public override void Flush()
        {
            // Ensure the cached copy is updated with the last user edits
            FlushSelected();

            // Force the new asset to take on the old asset's name - this can only be changed outside the editor window, so if the user
            // changed this after the editor window made a copy of the source asset, the name would be overwritten with the old one.
            m_AssetStringTable.Name = Asset.Name;

            // Commit the changes by updating the asset represented by this editor window with a new snapshot
            Asset = AssetSerializer.DuplicateAsset(m_AssetStringTable);

            // Refresh stats label
            UpdateStats();

            base.Flush();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.D))
            {
                if (tsbAddIncrement.Enabled)
                    tsbAddIncrement_Click(this, EventArgs.Empty);
                return true;
            }

            if (keyData == (Keys.Control | Keys.T))
            {
                if (tsbAddTopic.Enabled)
                    tsbAddTopic_Click(this, EventArgs.Empty);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FlushSelected()
        {
            if (m_EditingKey == null)
                return;

            // Replace the contents of the list with data from the form
            var list = m_RawDictionary[m_EditingKey];
            list.Clear();
            list.AddRange(scintilla.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
        }

        private void UpdateStats()
        {
            // First calculate number of entries, to avoid a division by zero if the table is empty
            int num_entries = m_RawDictionary.Sum(pair => pair.Value.Count);
            if (m_RawDictionary.Count == 0 || num_entries == 0)
            {
                tslStats.Text = "0 words";
                return;
            }

            // Compute the total word count in all string sets, and divide by the set entry count (not set count) to get average
            int total_words = m_RawDictionary.SelectMany(pair => pair.Value).Sum(phrase => phrase.CountWords());
            int avg_words = total_words / num_entries;

            // Show stats on-screen
            tslStats.Text = $"{total_words:##,##0} words, {avg_words:##,##0} avg";
        }

        private void AddSet(string key)
        {
            // If a duplicate set is requested, select that node instead
            if (m_RawDictionary.ContainsKey(key))
            {
                // Find and select a matching node
                for (int i = 0; i < lstKeys.Items.Count; i++)
                {
                    if (lstKeys.Items[i].Text.Equals(key))
                    {
                        lstKeys.Items[i].BeginEdit();
                        break;
                    }
                }

                // Regardless of whether we found the list node (and we should have), we can't add a new one, so bail
                return;
            }

            // Add a new table entry for the new key
            m_RawDictionary.Add(key, new List<string>());

            // Add a new list item and immediately put the label in edit mode so the user can easily customize the key
            ListViewItem item = lstKeys.Items.Add(key);
            lstKeys.Sort();
            item.BeginEdit();

            Dirty = true;
        }

        private string GetUnusedKey(string prefix, int start_suffix = 1, int suffix_width = 1)
        {
            // Find an unused name
            string key;
            do
            {
                key = prefix + String.Format(CultureInfo.InvariantCulture, $"{{0:D{suffix_width}}}", start_suffix);
                start_suffix++;
            } while (m_RawDictionary.ContainsKey(key));

            return key;
        }

        private void lstKeys_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            // Always cancel, because we manually override the text to an uppercase-converted version
            e.CancelEdit = true;

            // Ignore unchanged or invalid input
            if (String.IsNullOrWhiteSpace(e.Label))
                return;

            // Check for uniqueness of the key
            string new_key = e.Label.MakeSafeIdentifier();
            if (m_RawDictionary.ContainsKey(new_key))
                return;

            // Update the label
            var list_item = lstKeys.Items[e.Item];
            var old_key = list_item.Text;
            list_item.Text = new_key;
            lstKeys.Sort();

            // Move the list to the new key
            m_RawDictionary.Add(new_key, m_RawDictionary[old_key]);
            m_RawDictionary.Remove(old_key);

            // Update the editing key to match, if we happen to be editing that same key
            if (old_key.Equals(m_EditingKey))
                m_EditingKey = new_key;

            Dirty = true;
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            // Create a new set with no name
            AddSet(GetUnusedKey("UNTITLED"));
        }

        private void tsbRemove_Click(object sender, EventArgs e)
        {
            // Ensure there is actually a selection to remove
            if (lstKeys.SelectedItems.Count == 0)
                return;

            // Ask for confirmation
            if (MessageBox.Show($"Are you sure you want to delete the set '{m_EditingKey}'? This operation cannot be undone.", "Finmer Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return;

            lstKeys.Items.Remove(lstKeys.SelectedItems[0]);
            m_RawDictionary.Remove(m_EditingKey);

            m_EditingKey = null;
            scintilla.Visible = false;

            Dirty = true;
        }

        private void lstKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Show editing UI only if a set is selected
            bool has_selection = lstKeys.SelectedIndices.Count != 0;
            scintilla.Visible = has_selection;
            tsbRemove.Enabled = has_selection;
            tsbAddIncrement.Enabled = has_selection;
            tsbAddTopic.Enabled = has_selection;

            // Save any previously selected set
            FlushSelected();

            // Update editing UI if a new set is selected
            if (!has_selection)
                return;

            // Show new set (and avoid marking asset as dirty while we're updating the text shown in the text control)
            m_IgnoreDirtiedText = true;
            m_EditingKey = lstKeys.SelectedItems[0].Text;
            scintilla.Text = String.Join(Environment.NewLine, m_RawDictionary[m_EditingKey]);

            // Wipe the undo/redo buffer to avoid awkward UX where the user can "undo" to the previous string set
            scintilla.EmptyUndoBuffer();

            m_IgnoreDirtiedText = false;
        }

        private void scintilla_TextChanged(object sender, EventArgs e)
        {
            if (m_IgnoreDirtiedText)
                return;

            Dirty = true;
        }

        private void lstKeys_Resize(object sender, EventArgs e)
        {
            // Quick hack to make the column resize along with the control
            lstKeys.Columns[0].Width = lstKeys.ClientSize.Width - 1;
        }

        private void tsbAddIncrement_Click(object sender, EventArgs e)
        {
            Debug.Assert(lstKeys.SelectedItems.Count == 1);

            // Match the auto-increment regex against the selected key
            string base_key = lstKeys.SelectedItems[0].Text;
            var match = s_IncrementingKeyRegex.Match(base_key);
            if (match.Success)
            {
                // We have a number suffix; increment it
                string number_text = match.Groups[2].Value;
                base_key = match.Groups[1].Value;
                AddSet(GetUnusedKey(base_key, Int32.Parse(number_text), number_text.Length));
            }
            else
            {
                // If we can't find a number suffix, just slap on a new one
                AddSet(GetUnusedKey(base_key));
            }
        }

        private void tsbAddTopic_Click(object sender, EventArgs e)
        {
            Debug.Assert(lstKeys.SelectedItems.Count == 1);

            // Check if there is any text at all in this key, e.g. more than just an underscore
            var base_key = lstKeys.SelectedItems[0].Text;
            if (String.IsNullOrWhiteSpace(base_key) || base_key.Length <= 1)
                return;

            // Find the last underscore so we can strip it off
            var underscore_index = base_key.LastIndexOf('_', base_key.Length - 2);
            if (underscore_index != -1)
            {
                string topic_key = base_key.Substring(0, underscore_index + 1);

                // Create or select a set with that stripped key
                if (!String.IsNullOrEmpty(topic_key))
                    AddSet(topic_key);
            }
            else if (!base_key.EndsWith("_"))
            {
                // This key does not contain underscores, so add a new one
                AddSet(base_key + "_");
            }
            else
            {
                // This key already has an underscore and no further topics, so the best we can do is let the user edit this one
                lstKeys.SelectedItems[0].BeginEdit();
            }
        }

    }

}
