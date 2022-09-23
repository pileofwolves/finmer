/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
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

            base.Flush();
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

        private void lstKeys_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            // Always cancel, because we manually override the text to an uppercase-converted version
            e.CancelEdit = true;

            // Ignore unchanged or invalid input
            if (String.IsNullOrWhiteSpace(e.Label))
                return;

            // Check for uniqueness of the key
            string new_key = e.Label.ToUpperInvariant();
            if (m_RawDictionary.ContainsKey(new_key))
            {
                MessageBox.Show("The table key must be unique, please try something else.", "Finmer Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

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
            // Find an unused name
            string key;
            var name_number = 0;
            do
            {
                name_number++;
                key = "UNTITLED" + name_number;
            } while (m_RawDictionary.ContainsKey(key));

            // Add a new table entry for the new key
            m_RawDictionary.Add(key, new List<string>());

            // Add a new list item and immediately mark it for editing so the user can type a better name
            ListViewItem item = lstKeys.Items.Add(key);
            lstKeys.Sort();
            item.BeginEdit();

            Dirty = true;
        }

        private void tsbRemove_Click(object sender, EventArgs e)
        {
            // Has selection? (failsafe)
            if (lstKeys.SelectedItems.Count == 0) 
                return;

            // User confirmed?
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

    }

}
