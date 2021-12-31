/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

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
                        Text = asset.ID.ToString(),
                        ForeColor = SystemColors.GrayText
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

    }

}
