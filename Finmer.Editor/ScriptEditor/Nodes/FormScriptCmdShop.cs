/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Globalization;
using System.Windows.Forms;
using Finmer.Core.Assets;
using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for CommandShop.
    /// </summary>
    public partial class FormScriptCmdShop : FormScriptNode
    {

        private ListViewItem m_SelectedItem;

        public FormScriptCmdShop()
        {
            InitializeComponent();
        }

        private void FormScriptCmdPlayerSetItem_Load(object sender, EventArgs e)
        {
            var node = (CommandShop)Node;

            // Configuration
            txtKey.Text = node.Key;
            txtTitle.Text = node.Title;
            nudRestockInterval.Value = node.RestockInterval;

            // Merchandise
            foreach (var pair in node.Merchandise)
            {
                var item = new ListViewItem
                {
                    Text = DescribeItemLink(pair.Key),
                    Tag = pair.Key,
                    SubItems =
                    {
                        new ListViewItem.ListViewSubItem
                        {
                            Text = DescribeQuantity(pair.Value),
                            Tag = pair.Value
                        }
                    }
                };
                lsvMerch.Items.Add(item);
            }
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            var node = (CommandShop)Node;

            // Configuration
            node.Key = txtKey.Text;
            node.Title = txtTitle.Text;
            node.RestockInterval = (int)nudRestockInterval.Value;

            // Merchandise
            node.Merchandise.Clear();
            foreach (ListViewItem item in lsvMerch.Items)
                node.Merchandise.Add((Guid)item.Tag, (int)item.SubItems[1].Tag);
        }

        private void nudRestockInterval_ValueChanged(object sender, EventArgs e)
        {
            lblRestockNote.Text = nudRestockInterval.Value == 0 ? "(never)" : "in-game hours";
        }

        private void lsvMerch_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool has_selection = lsvMerch.SelectedItems.Count == 1;
            pnlMerchEdit.Enabled = has_selection;
            cmdItemRemove.Enabled = has_selection;
            m_SelectedItem = null;

            if (has_selection)
            {
                var item = lsvMerch.SelectedItems[0];
                apcMerchAsset.SelectedGuid = (Guid)item.Tag;
                nudMerchQty.Value = (int)item.SubItems[1].Tag;
                m_SelectedItem = item;
            }
        }

        private void cmdItemAdd_Click(object sender, EventArgs e)
        {
            var item = new ListViewItem
            {
                Text = DescribeItemLink(Guid.Empty),
                Tag = Guid.Empty,
                Selected = true,
                SubItems =
                {
                    new ListViewItem.ListViewSubItem
                    {
                        Text = DescribeQuantity(0),
                        Tag = 0
                    }
                }
            };
            lsvMerch.Items.Add(item);
        }

        private void cmdItemRemove_Click(object sender, EventArgs e)
        {
            if (lsvMerch.SelectedItems.Count == 1)
                lsvMerch.Items.Remove(lsvMerch.SelectedItems[0]);
        }

        private void apcMerchAsset_SelectedAssetChanged(object sender, EventArgs e)
        {
            if (m_SelectedItem == null)
                return;

            m_SelectedItem.Tag = apcMerchAsset.SelectedGuid;
            m_SelectedItem.Text = DescribeItemLink(apcMerchAsset.SelectedGuid);
        }

        private void nudMerchQty_ValueChanged(object sender, EventArgs e)
        {
            if (m_SelectedItem == null)
                return;

            int selected_qty = (int)nudMerchQty.Value;
            var sub_item = m_SelectedItem.SubItems[1];
            sub_item.Tag = selected_qty;
            sub_item.Text = DescribeQuantity(selected_qty);
        }

        private static string DescribeQuantity(int quantity)
        {
            return quantity == 0 ? "\u221E" : quantity.ToString(CultureInfo.InvariantCulture);
        }

        private static string DescribeItemLink(Guid guid)
        {
            // Return a descriptive label for unset links
            if (guid == Guid.Empty)
                return "[empty]";

            // Use the item name if found, otherwise use the UUID
            return Program.LoadedContent.GetAssetByID<AssetItem>(guid)?.Name
                ?? guid.ToString();
        }

    }

}
