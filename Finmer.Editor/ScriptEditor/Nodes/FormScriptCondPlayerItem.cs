/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for a player inventory item selector.
    /// </summary>
    public partial class FormScriptCondPlayerItem : FormScriptNode
    {

        public FormScriptCondPlayerItem()
        {
            InitializeComponent();
        }

        private void FormScriptCondPlayerItem_Load(object sender, System.EventArgs e)
        {
            var node = (ConditionPlayerHasItem)Node;
            asset.SelectedGuid = node.ItemGuid;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (ConditionPlayerHasItem)Node;
            node.ItemGuid = asset.SelectedGuid;
            node.ItemDisplayName = asset.SelectedAsset?.Name ?? "[Unknown]";
        }

    }

}
