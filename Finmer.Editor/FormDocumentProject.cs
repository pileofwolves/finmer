/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Finmer.Core;
using Finmer.Core.Assets;
using Finmer.Core.Serialization;

namespace Finmer.Editor
{

    public partial class FormDocumentProject : AssetWindow
    {

        private readonly Furball m_Furball;

        public FormDocumentProject(Furball furball)
        {
            InitializeComponent();
            m_Furball = furball;
        }

        public override void Flush()
        {
            base.Flush();

            // Save changed data back to the module
            m_Furball.Metadata = new FurballMetadata
            {
                ID = m_Furball.Metadata.ID,
                Title = txtTitle.Text,
                Author = txtAuthor.Text
            };
        }

        private void FormDocumentProject_Load(object sender, EventArgs e)
        {
            // Populate UI
            txtTitle.Text = m_Furball.Metadata.Title;
            txtAuthor.Text = m_Furball.Metadata.Author;
            txtGUID.Text = m_Furball.Metadata.ID.ToString();
            lblStats.Text = GatherStats();

            // Populate dependencies table
            foreach (FurballDependency dependency in m_Furball.Dependencies)
                AddDependencyEntry(dependency);

            // Allow saving when an input field is changed
            MakeControlsDirty(this);
        }

        private string GatherStats()
        {
            var sb = new StringBuilder();

            // Total asset count
            sb.AppendLine($"{m_Furball.Assets.Count:##,##0}");

            // Scene node count
            sb.AppendLine($"{CountAllSceneNodes():##,##0}");

            // Word count
            sb.AppendLine($"{CountAllWords():##,##0}");

            return sb.ToString();
        }

        private int CountAllSceneNodes()
        {
            return m_Furball.Assets
                .OfType<AssetScene>()
                .Select(scene => scene.Root)
                .Traverse(node => node.Children)
                .Count();
        }

        private int CountAllWords()
        {
            var total = 0;

            // Find text in string tables
            foreach (AssetStringTable table in m_Furball.Assets.OfType<AssetStringTable>())
            {
                Dictionary<string, List<string>> dictionary = table.Table.GetTable();
                IEnumerable<string> text_entries = dictionary.SelectMany(pair => pair.Value);
                total += CountWords(text_entries);
            }

            // Find text in items
            foreach (AssetItem item in m_Furball.Assets.OfType<AssetItem>())
            {
                total += CountWords(item.FlavorText);
                total += CountWords(item.UseDescription);
            }

            // Find text in journals
            foreach (AssetJournal journal in m_Furball.Assets.OfType<AssetJournal>())
            {
                IEnumerable<string> stages = journal.Stages.Select(entry => entry.Text);
                total += CountWords(stages);
            }

            return total;
        }

        private static int CountWords(string phrase)
        {
            return phrase.Count(x => x == ' ') + 1;
        }

        private static int CountWords(IEnumerable<string> group)
        {
            return group.Sum(CountWords);
        }

        private void cmdRandomGuid_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to regenerate the module ID? You should ONLY do this if you're making a NEW module that you COPIED from a template."
                    + "\r\n\r\nThis will break players' save data if they ever used this module before, because save data records which module IDs it requires."
                    + "\r\n\r\nAre you sure you know what you're doing?",
                    "Finmer Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                == DialogResult.No) return;

            // Generate and save a new random GUID
            m_Furball.Metadata = new FurballMetadata
            {
                ID = Guid.NewGuid(),
                Title = txtTitle.Text,
                Author = txtAuthor.Text
            };

            // Update UI
            txtGUID.Text = m_Furball.Metadata.ID.ToString();
            Dirty = true;
        }

        private void cmdDepAdd_Click(object sender, EventArgs e)
        {
            if (dlgSelectDep.ShowDialog() != DialogResult.OK) return;

            // Try to load the selected module, in order to obtain its ID number
            FurballMetadata dep_meta;
            try
            {
                FurballFileDevice device = new FurballFileDeviceBinary();
                dep_meta = device.ReadMetadata(new FileInfo(dlgSelectDep.FileName));

                // Cannot be the same module
                if (dep_meta.ID == m_Furball.Metadata.ID)
                    throw new InvalidOperationException("You cannot add a module as a dependency to itself.");

                // Cannot be added more than once
                if (m_Furball.Dependencies.Any(existingDependency => existingDependency.ID == dep_meta.ID))
                    throw new InvalidOperationException("The selected file is already registered as a dependency.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("The selected file could not be added as a dependency: " + ex.Message, "Finmer Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Register the dependency
            var dependency = new FurballDependency
            {
                ID = dep_meta.ID,
                FileNameHint = dlgSelectDep.SafeFileName
            };
            m_Furball.Dependencies.Add(dependency);
            AddDependencyEntry(dependency);

            // Module has been edited
            Dirty = true;
        }

        private void AddDependencyEntry(FurballDependency dependency)
        {
            var item = new ListViewItem
            {
                Text = dependency.FileNameHint,
                SubItems =
                {
                    dependency.ID.ToString()
                },
                Tag = dependency.ID
            };

            lsvDependencies.Items.Add(item);
        }

        private void lsvDependencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdDepRemove.Enabled = lsvDependencies.SelectedIndices.Count > 0;
        }

        private void cmdDepRemove_Click(object sender, EventArgs e)
        {
            Debug.Assert(lsvDependencies.SelectedItems.Count == 1);

            // Remove the item from the list view
            ListViewItem selected = lsvDependencies.SelectedItems[0];
            lsvDependencies.Items.Remove(selected);

            // Unregister the dependency
            m_Furball.Dependencies.RemoveAll(dep => dep.ID == (Guid)selected.Tag);
            Dirty = true;
        }

    }

}
