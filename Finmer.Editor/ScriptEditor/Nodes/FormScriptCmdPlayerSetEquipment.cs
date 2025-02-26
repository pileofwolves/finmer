/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Assets;
using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for CommandPlayerSetEquipment.
    /// </summary>
    public partial class FormScriptCmdPlayerSetEquipment : FormScriptNode
    {

        public FormScriptCmdPlayerSetEquipment()
        {
            InitializeComponent();
        }

        private void FormScriptCmdPlayerSetEquipment_Load(object sender, System.EventArgs e)
        {
            var node = (CommandPlayerSetEquipment)Node;
            cmbSlot.SelectedIndex = (int)node.EquipSlot;
            apcItem.SelectedGuid = node.ItemGuid;
            apcItem.SelectorPredicate = candidate => candidate is AssetItem candidate_item && candidate_item.ItemType == AssetItem.EItemType.Equipable;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandPlayerSetEquipment)Node;
            node.EquipSlot = (CommandPlayerSetEquipment.ESlot)cmbSlot.SelectedIndex;
            node.ItemGuid = apcItem.SelectedGuid;
        }

    }

}
