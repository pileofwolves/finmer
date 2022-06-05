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
    /// Node editor form for CommandPlayerSetItem.
    /// </summary>
    public partial class FormScriptCmdPlayerSetItem : FormScriptNode
    {

        public FormScriptCmdPlayerSetItem()
        {
            InitializeComponent();
        }

        private void FormScriptCmdPlayerSetItem_Load(object sender, System.EventArgs e)
        {
            var node = (CommandPlayerSetItem)Node;
            optModeAdd.Checked = node.Add;
            optModeRemove.Checked = !node.Add;
            apcItem.SelectedGuid = node.ItemGuid;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandPlayerSetItem)Node;
            node.Add = optModeAdd.Checked;
            node.ItemGuid = apcItem.SelectedGuid;
            node.ItemName = apcItem.SelectedAsset?.Name ?? "[unknown]";
        }

    }

}
