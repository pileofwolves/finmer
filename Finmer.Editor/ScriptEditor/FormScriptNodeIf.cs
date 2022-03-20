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
    /// Node editor form for a branch node.
    /// </summary>
    public partial class FormScriptNodeIf : FormScriptNode
    {

        public FormScriptNodeIf()
        {
            InitializeComponent();
        }

        private void FormScriptNodeIf_Load(object sender, System.EventArgs e)
        {
            var branch = (CommandIf)Node;
            condition.Value = branch.Condition;
            chkElse.Checked = branch.HasElseBranch;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var branch = (CommandIf)Node;
            branch.Condition = condition.Value;
            branch.HasElseBranch = chkElse.Checked;
        }

    }

}
