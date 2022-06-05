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
    /// Node editor form for a log message.
    /// </summary>
    public partial class FormScriptCmdLog : FormScriptNode
    {

        public FormScriptCmdLog()
        {
            InitializeComponent();
        }

        private void FormScriptNodeLog_Load(object sender, System.EventArgs e)
        {
            var node = (CommandLog)Node;
            txtMessage.Text = node.Text;
            chkRaw.Checked = node.IsRaw;

            // Force text header and multiline state to update
            chkRaw_CheckedChanged(sender, e);
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            // String table keys are uppercase-only by convention
            if (!chkRaw.Checked)
                txtMessage.Text = txtMessage.Text.ToUpperInvariant();

            // Copy settings to node
            var node = (CommandLog)Node;
            node.Text = txtMessage.Text;
            node.IsRaw = chkRaw.Checked;
        }

        private void chkRaw_CheckedChanged(object sender, System.EventArgs e)
        {
            lblHeader.Text = chkRaw.Checked ? "Message Text:" : "String Table Key:";
            txtMessage.Multiline = chkRaw.Checked;
        }

    }

}
