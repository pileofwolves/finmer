/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core;
using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for CommandVarSetFlag.
    /// </summary>
    public partial class FormScriptCmdVarSetFlag : FormScriptNode
    {

        public FormScriptCmdVarSetFlag()
        {
            InitializeComponent();
        }

        private void FormScriptCmdSetPlayerName_Load(object sender, System.EventArgs e)
        {
            var node = (CommandVarSetFlag)Node;
            txtContextName.Text = node.VariableName;
            sveValue.SetValue(node.Value);
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandVarSetFlag)Node;
            node.VariableName = txtContextName.Text.MakeSafeIdentifier();
            node.Value = sveValue.GetValue();
        }

    }

}
