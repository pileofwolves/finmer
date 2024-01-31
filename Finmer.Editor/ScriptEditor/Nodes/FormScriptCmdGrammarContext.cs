/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for CommandGrammarSetContext.
    /// </summary>
    public partial class FormScriptCmdGrammarContext : FormScriptNode
    {

        public FormScriptCmdGrammarContext()
        {
            InitializeComponent();
        }

        private void FormScriptCmdPlayerSetItem_Load(object sender, System.EventArgs e)
        {
            var node = (CommandGrammarSetContext)Node;
            txtContextName.Text = node.VariableName;
            apcCreature.SelectedGuid = node.CreatureGuid;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandGrammarSetContext)Node;
            node.VariableName = txtContextName.Text;
            node.CreatureGuid = apcCreature.SelectedGuid;
        }

    }

}
