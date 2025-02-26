/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
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

    /// <summary>
    /// Represents a dockable window that is responsible for editing one asset.
    /// </summary>
    public class AssetWindow : EditorWindow
    {

        /// <summary>
        /// Gets or sets the asset that this window is editing.
        /// </summary>
        public AssetBase Asset { get; set; }

        protected AssetWindow()
        {
            FormClosed += AssetWindow_FormClosed;
        }

        protected override string GetWindowTitle()
        {
            return Asset?.Name ?? String.Empty;
        }

        public override void Flush()
        {
            base.Flush();

            // Find the asset represented by this editor window
            var new_asset = Asset;
            var old_asset = Program.ActiveFurball.GetAssetByID(new_asset.ID);
            Debug.Assert(old_asset != null, "Asset ID changes are not supported");
            Debug.Assert(old_asset.Name.Equals(new_asset.Name, StringComparison.InvariantCulture), "Asset name changes must be copied by window");

            // If the window still points to the same asset object, we do not need to replace anything
            if (!ReferenceEquals(new_asset, old_asset))
            {
                // Otherwise, the old asset must be removed, so it can be replaced with the editor window's newly created asset
                var assets = Program.ActiveFurball.Assets;
                assets.Remove(old_asset);
                assets.Add(new_asset);

                // Replace the reference on the asset tree node, so we won't re-open the old asset later
                Program.MainForm.ReplaceAssetTreeNode(old_asset, new_asset);
            }
        }
        private void AssetWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Ensure the asset icon is up-to-date - discarding changes may have changed the asset contents
            Program.MainForm.UpdateAssetIcon(Asset);
        }

    }

}
