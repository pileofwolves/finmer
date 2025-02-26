/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows.Forms;
using Finmer.Core;
using Finmer.Core.Assets;

namespace Finmer.Editor
{

    /// <summary>
    /// Form for configuring load order dependencies from one source asset to one or more target assets.
    /// </summary>
    public partial class FormLoadOrder : Form
    {

        /// <summary>
        /// The source asset whose load order settings are being edited by this form.
        /// </summary>
        public AssetScript Source { get; set; }

        private int m_SelectedIndex = -1;

        public FormLoadOrder()
        {
            InitializeComponent();
        }

        private void FormLoadOrder_Load(object sender, System.EventArgs e)
        {
            lblRelation.Text = $"Load {Source.Name} in this order:";

            // Add list items for all dependencies registered in the asset
            foreach (var dep in Source.LoadOrder)
            {
                lsvDependencies.Items.Add(new ListViewItem
                {
                    Text = $"{dep.Relation} {Program.LoadedContent.GetAssetName(dep.TargetAsset)}",
                    Tag = dep
                });
            }
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            // Save current settings if we have any being edited
            if (m_SelectedIndex != -1)
                SaveSelectedItem();

            // Commit to the asset
            Source.LoadOrder.Clear();
            foreach (ListViewItem item in lsvDependencies.Items)
                Source.LoadOrder.Add((LoadOrderDependency)item.Tag);
        }

        private void lsvDependencies_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Save current settings if we have any being edited
            if (m_SelectedIndex != -1)
                SaveSelectedItem();

            // Clear UI if deselecting items
            if (lsvDependencies.SelectedIndices.Count != 1)
            {
                m_SelectedIndex = -1;
                cmdDepRemove.Enabled = false;
                grpElement.Enabled = false;
                return;
            }

            // Enable editing controls
            m_SelectedIndex = lsvDependencies.SelectedIndices[0];
            cmdDepRemove.Enabled = true;
            grpElement.Enabled = true;

            // Populate with selected item
            var dep = (LoadOrderDependency)lsvDependencies.Items[m_SelectedIndex].Tag;
            cmbRelation.SelectedIndex = (int)dep.Relation;
            apcOther.SelectedGuid = dep.TargetAsset;
        }

        private void SaveSelectedItem()
        {
            var item = lsvDependencies.Items[m_SelectedIndex];
            item.Text = $"{cmbRelation.Text} {Program.LoadedContent.GetAssetName(apcOther.SelectedGuid)}";
            item.Tag = new LoadOrderDependency
            {
                Relation = (LoadOrderDependency.ERelation)cmbRelation.SelectedIndex,
                TargetAsset = apcOther.SelectedGuid
            };
        }

        private void cmdDepAdd_Click(object sender, System.EventArgs e)
        {
            lsvDependencies.Items.Add(new ListViewItem
            {
                Text = "New dependency",
                Tag = new LoadOrderDependency(),
                Selected = true
            });
        }

        private void cmdDepRemove_Click(object sender, System.EventArgs e)
        {
            // Remove the listing
            lsvDependencies.Items.RemoveAt(m_SelectedIndex);

            // Disable editing UI now that the selection is gone
            m_SelectedIndex = -1;
            cmdDepRemove.Enabled = false;
            grpElement.Enabled = false;
        }

    }

}
