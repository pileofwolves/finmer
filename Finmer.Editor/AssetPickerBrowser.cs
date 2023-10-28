/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using Finmer.Core.Assets;

namespace Finmer.Editor
{

    public partial class AssetPickerBrowser : Form
    {

        public AssetBase SelectedAsset { get; private set; }

        public AssetPickerBrowser()
        {
            InitializeComponent();
        }

        public void AddSelectableAsset(AssetBase asset, bool isSelected, bool isFromDependency)
        {
            // Generate a new list item that describes this asset
            ListViewItem item = new ListViewItem
            {
                Text = asset.Name,
                Tag = asset,
                SubItems =
                {
                    new ListViewItem.ListViewSubItem
                    {
                        Text = DescribeAsset(asset)
                    }
                },
                Selected = isSelected,
                Group = isFromDependency ? lsvAssets.Groups[1] : lsvAssets.Groups[0]
            };

            lsvAssets.Items.Add(item);
        }

        private void lsvAssets_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var selection = lsvAssets.SelectedItems;
            if (selection.Count == 0)
                SelectedAsset = null;
            else
                SelectedAsset = (AssetBase)selection[0].Tag;
        }

        private void lsvAssets_DoubleClick(object sender, EventArgs e)
        {
            // If the user has made a selection, allow double-click as a convenience for confirming
            if (SelectedAsset != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// Tries to return a user-defined name for an asset, or an empty string if unavailable.
        /// </summary>
        private static string DescribeAsset(AssetBase asset)
        {
            switch (asset)
            {
                case AssetCreature creature:
                    return creature.ObjectName;

                case AssetItem item:
                    return item.ObjectName;

                case AssetJournal journal:
                    return journal.Title;

                default:
                    return String.Empty;
            }
        }

    }

}
