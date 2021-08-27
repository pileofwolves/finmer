/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
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

        private void FormDocumentProject_Load(object sender, EventArgs e)
        {
            txtTitle.Text = m_Furball.Metadata.Title;
            txtAuthor.Text = m_Furball.Metadata.Author;
            txtGUID.Text = m_Furball.Metadata.ID.ToString();
            lblStats.Text = GatherStats();

            foreach (FurballDependency dependency in m_Furball.Dependencies)
                AddDependencyEntry(dependency);

            MakeControlsDirty(this);

#if DEBUG
            cmdRandomGuid.Visible = true;
#endif
        }

        public override void Flush()
        {
            base.Flush();

            m_Furball.Metadata = new FurballMetadata
            {
                ID = m_Furball.Metadata.ID,
                Title = txtTitle.Text,
                Author = txtAuthor.Text
            };
        }

        private int ComputeWordCount(List<string> group)
        {
            return group.Sum(str => str?.Count(x => x == ' ') + 1 ?? 0);
        }

        private string GatherStats()
        {
            List<string> phrases_tables = new List<string>();
            List<string> phrases_scenes = new List<string>();
            List<string> phrases_journals = new List<string>();
            List<string> phrases_items = new List<string>();
            var sb = new StringBuilder();

            sb.AppendLine(m_Furball.Assets.Count.ToString());

            // count scene nodes
            sb.AppendLine(m_Furball.Assets.OfType<AssetScene>().Sum(scene =>
            {
                int ret = -1; // don't count root
                Stack<AssetScene.SceneNode> stack = new Stack<AssetScene.SceneNode>();
                stack.Push(scene.Root);
                while (stack.Count > 0)
                {
                    AssetScene.SceneNode node = stack.Pop();
                    node.Children.ForEach(stack.Push);
                    phrases_scenes.Add(node.Title);
                    phrases_scenes.Add(node.Tooltip);
                    ret++;
                }

                return ret;
            }).ToString());

            // ---- WORD COUNT ----
            var sets = 0;

            // count words in game text (string tables)
            m_Furball.Assets.OfType<AssetStringTable>().ForEach(table =>
            {
                Dictionary<string, List<string>> dict = table.Table.GetTable();
                sets += dict.Count;
                phrases_tables.AddRange(dict.SelectMany(pair => pair.Value.ToList()));
            });
            // count words in items
            m_Furball.Assets.OfType<AssetItem>().ForEach(item =>
            {
                phrases_items.Add(item.FlavorText);
                phrases_items.Add(item.UseDescription);
            });
            // count words in journals
            m_Furball.Assets.OfType<AssetJournal>().ForEach(journal => { phrases_journals.AddRange(journal.Stages.Select(entry => entry.Text)); });

            int words_tables = ComputeWordCount(phrases_tables);
            int words_scenes = ComputeWordCount(phrases_scenes);
            int words_items = ComputeWordCount(phrases_items);
            int words_journals = ComputeWordCount(phrases_journals);
            sb.AppendLine($"{sets:##,##0}");
            sb.AppendFormat("{0:##,##0}   ", words_tables + words_scenes + words_items + words_journals);
            sb.AppendFormat("({0:##,##0} in tables, {1:##,##0} in scenes, {2:##,##0} in items, {3:##,##0} in journals)", words_tables, words_scenes, words_items, words_journals);

            return sb.ToString();
        }

        private void cmdRandomGuid_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to regenerate the module ID? You should ONLY do this if you're making a NEW module that you COPIED from a template."
                    + "\r\n\r\nThis will break players' savegames if they ever used this module before, because savegames record which module IDs they were saved with (and therefore require)."
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
                if (m_Furball.Dependencies.Any(existing_dep => existing_dep.ID == dep_meta.ID))
                    throw new InvalidOperationException("The selected file is already registered as a dependency.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("The selected file could not be added as a dependency: " + ex.Message, "Finmer Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Register the dependency
            FurballDependency dependency = new FurballDependency
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

            lsvDeps.Items.Add(item);
        }

        private void lsvDeps_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdDepRemove.Enabled = lsvDeps.SelectedIndices.Count > 0;
        }

        private void cmdDepRemove_Click(object sender, EventArgs e)
        {
            Debug.Assert(lsvDeps.SelectedItems.Count == 1);

            // Remove the item from the list view
            ListViewItem selected = lsvDeps.SelectedItems[0];
            lsvDeps.Items.Remove(selected);

            // Unregister the dependency
            m_Furball.Dependencies.RemoveAll(dep => dep.ID == (Guid)selected.Tag);
            Dirty = true;
        }

    }

}
