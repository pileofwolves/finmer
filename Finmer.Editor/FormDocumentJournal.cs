/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Diagnostics;
using System.Windows.Forms;
using Finmer.Core.Assets;

namespace Finmer.Editor
{

    public partial class FormDocumentJournal : AssetWindow
    {

        private AssetJournal m_Journal;
        private ListViewItem m_SelectedEntry;

        public FormDocumentJournal()
        {
            InitializeComponent();
        }

        private void FormDocumentJournal_Load(object sender, EventArgs e)
        {
            Debug.Assert(Asset != null);
            Debug.Assert(Asset is AssetJournal);

            m_Journal = (AssetJournal)Asset;
            txtName.Text = m_Journal.Title;
            txtGuid.Text = Asset.ID.ToString();

            foreach (var stage in m_Journal.Stages)
            {
                var lsi = new ListViewItem();
                lsi.Text = $"{stage.Key}: {stage.Text}";
                lsi.Tag = stage;
                lstEntries.Items.Add(lsi);
            }

            // mark the asset as dirty if a control on the form is changed
            MakeControlsDirty(this);
            nudEntryKey.ValueChanged += UpdateEntryLabel;
            txtEntryText.TextChanged += UpdateEntryLabel;
        }

        public override void Flush()
        {
            base.Flush();

            // Apply fields to the previously selected entry, if there was one, to ensure changes are committed
            if (m_SelectedEntry != null)
            {
                m_SelectedEntry.Tag = new AssetJournal.QuestStage
                {
                    Key = (int)nudEntryKey.Value,
                    Text = txtEntryText.Text
                };
                m_SelectedEntry = null;
            }

            // Copy new settings and quest stages back to the AssetJournal instance
            m_Journal.Title = txtName.Text;
            m_Journal.Stages.Clear();
            foreach (ListViewItem item in lstEntries.Items)
            {
                var stage = (AssetJournal.QuestStage)item.Tag;
                m_Journal.Stages.Add(stage);
            }

            // Sort the entries so the key IDs are in ascending order
            m_Journal.Stages.Sort((a, b) => a.Key.CompareTo(b.Key));
        }

        private void UpdateEntryLabel(object sender, EventArgs e)
        {
            // wanted to use null-coalescing operator here but apparently that's not allowed when accessing property setters
            if (m_SelectedEntry != null)
                m_SelectedEntry.Text = $"{(int)nudEntryKey.Value}: {txtEntryText.Text}";
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            // Generate a new list item
            var list_item = new ListViewItem
            {
                Text = "New Entry", 
                Tag = new AssetJournal.QuestStage
                {
                    Text = String.Empty
                }
            };

            // Add it to UI and select it so user can edit it immediately
            lstEntries.Items.Add(list_item);
            list_item.Selected = true;
        }

        private void lstEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Commit any previous changes
            if (m_SelectedEntry != null)
            {
                m_SelectedEntry.Tag = new AssetJournal.QuestStage
                {
                    Key = (int)nudEntryKey.Value,
                    Text = txtEntryText.Text
                };
                m_SelectedEntry = null;
            }

            // set up UI for this new selection
            bool mode = lstEntries.SelectedItems.Count == 1;
            lblEntryKey.Enabled = mode;
            lblEntryText.Enabled = mode;
            txtEntryText.Enabled = mode;
            nudEntryKey.Enabled = mode;
            cmdRemove.Enabled = mode;

            // apply the newly selected entry to the UI controls
            if (mode)
            {
                m_SelectedEntry = lstEntries.SelectedItems[0];

                var entry = (AssetJournal.QuestStage)m_SelectedEntry.Tag;
                nudEntryKey.Value = entry.Key;
                txtEntryText.Text = entry.Text;
            }
            else
            {
                m_SelectedEntry = null;
            }
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            lstEntries.Items.Remove(m_SelectedEntry);
            m_SelectedEntry = null;
        }

    }

}
