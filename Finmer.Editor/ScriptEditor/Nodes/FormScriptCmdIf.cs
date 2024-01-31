/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for a branch node.
    /// </summary>
    public partial class FormScriptCmdIf : FormScriptNode
    {

        private CommandIf m_Node;

        public FormScriptCmdIf()
        {
            InitializeComponent();
        }

        private void FormScriptNodeIf_Load(object sender, EventArgs e)
        {
            // Populate UI
            m_Node = (CommandIf)Node;
            cgeBranch.SetGroup(m_Node.Condition);
            chkElse.Checked = m_Node.HasElseBranch;
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            // Copy UI state back to the node
            m_Node.Condition = cgeBranch.Group;
            m_Node.HasElseBranch = chkElse.Checked;

            // Return the edited node to the caller
            Node = m_Node;
        }

        private void cgeBranch_GroupValidityChanged(bool valid)
        {
            cmdAccept.Enabled = valid;
        }

    }

}
