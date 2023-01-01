/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
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
            chkAnnounce.Checked = !node.Quiet;
            chkAnnounce.Visible = optModeAdd.Checked;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandPlayerSetItem)Node;
            node.Add = optModeAdd.Checked;
            node.Quiet = !chkAnnounce.Checked;
            node.ItemGuid = apcItem.SelectedGuid;
        }

        private void optModeAdd_CheckedChanged(object sender, System.EventArgs e)
        {
            // Quiet flag is only relevant when the mode is to add items
            chkAnnounce.Visible = optModeAdd.Checked;
        }

    }

}
