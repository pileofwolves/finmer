/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Assets;
using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for a player equipment slot selector.
    /// </summary>
    public partial class FormScriptCondPlayerEquip : FormScriptNode
    {

        public FormScriptCondPlayerEquip()
        {
            InitializeComponent();
        }

        private void FormScriptCondPlayerEquip_Load(object sender, System.EventArgs e)
        {
            var node = (ConditionPlayerEquipment)Node;
            asset.SelectedGuid = node.ItemGuid;
            asset.SelectorPredicate = candidate => candidate is AssetItem candidate_item && candidate_item.ItemType == AssetItem.EItemType.Equipable;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (ConditionPlayerEquipment)Node;
            node.ItemGuid = asset.SelectedGuid;
        }

    }

}
