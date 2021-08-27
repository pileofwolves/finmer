/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Finmer.Core.Assets;

namespace Finmer.Editor
{

    /// <summary>
    /// Control that allows selection of a game asset of a particular type.
    /// </summary>
    public partial class AssetPickerControl : UserControl
    {

        /// <summary>
        /// Describes which AssetBase derived type can be selected.
        /// </summary>
        public enum EPickerType
        {
            Scene,
            Creature,
            Item,
            Feat,
            Journal,
            StringTable,
            Script
        }

        private static readonly Dictionary<EPickerType, Type> s_AssetTypeLookup = new Dictionary<EPickerType, Type>
        {
            { EPickerType.Scene,        typeof(AssetScene) },
            { EPickerType.Creature,     typeof(AssetCreature) },
            { EPickerType.Item,         typeof(AssetItem) },
            { EPickerType.Feat,         typeof(AssetFeat) },
            { EPickerType.Journal,      typeof(AssetJournal) },
            { EPickerType.StringTable,  typeof(AssetStringTable) },
            { EPickerType.Script,       typeof(AssetScript) }
        };

        /// <summary>
        /// Sets the filter for this picker's possible target assets.
        /// </summary>
        [Browsable(true)]
        public EPickerType AssetType { get; set; }

        /// <summary>
        /// Returns the Guid of the selected asset. Works even if the target asset is unloaded.
        /// </summary>
        public Guid SelectedGuid
        {
            get => m_SelectedAssetGuid;
            set
            {
                m_SelectedAssetGuid = value;
                UpdateLabel();
            }
        }

        /// <summary>
        /// Returns the currently selected asset object, or null if it is unset or not loaded.
        /// </summary>
        public AssetBase SelectedAsset =>
            Program.ActiveFurball.GetAssetByID(m_SelectedAssetGuid) ??
            Program.ActiveDependencies.GetAssetByID(m_SelectedAssetGuid);

        private Guid m_SelectedAssetGuid;

        public AssetPickerControl()
        {
            InitializeComponent();
        }

        private void UpdateLabel()
        {
            // Handle unset links
            if (m_SelectedAssetGuid == Guid.Empty)
            {
                lblAssetName.Enabled = false;
                lblAssetName.LinkColor = SystemColors.GrayText;
                lblAssetName.Text = "Not set";
                return;
            }

            // Otherwise, try resolving the Guid to an AssetBase
            AssetBase selected_asset = SelectedAsset;
            if (selected_asset == null)
            {
                // Link could not be resolved; asset was deleted or unloaded
                lblAssetName.Enabled = false;
                lblAssetName.LinkColor = SystemColors.GrayText;
                lblAssetName.Text = m_SelectedAssetGuid.ToString();
            }
            else
            {
                // Link resolved successfully; display its metadata
                lblAssetName.Enabled = true;
                lblAssetName.LinkColor = Color.Blue;
                lblAssetName.Text = selected_asset.Name;
            }
        }

        private void lblAssetName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Safety check: nothing to open if there's no set asset (but somehow this link was clicked anyway)
            var selected_asset = SelectedAsset;
            if (selected_asset == null)
                return;

            // Open the specified asset in the editor
            Program.MainForm.OpenAssetEditor(selected_asset);
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            // Open a dialog box where the user can select a new target asset
            using (AssetPickerBrowser browser = new AssetPickerBrowser())
            {
                // Add all acceptable assets to the dialog from both the current project and all loaded dependencies
                Type target_type = s_AssetTypeLookup[AssetType];
                foreach (var asset in Program.ActiveFurball.Assets.Where(asset => asset.GetType() == target_type))
                    browser.AddSelectableAsset(asset, asset.ID == m_SelectedAssetGuid, false);
                foreach (var asset in Program.ActiveDependencies.Assets.Where(asset => asset.GetType() == target_type))
                    browser.AddSelectableAsset(asset, asset.ID == m_SelectedAssetGuid, true);

                // Present the dialog, and only use results if the user confirmed
                if (browser.ShowDialog() != DialogResult.OK)
                    return;

                // Update the selected asset
                SelectedGuid = browser.SelectedAsset?.ID ?? Guid.Empty;
            }
        }

    }

}
