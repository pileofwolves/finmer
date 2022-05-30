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
    /// Node editor form for CommandVarSetNumber.
    /// </summary>
    public partial class FormScriptCmdVarSetNum : FormScriptNode
    {

        public FormScriptCmdVarSetNum()
        {
            InitializeComponent();
        }

        private void FormScriptCmdSetPlayerName_Load(object sender, System.EventArgs e)
        {
            var node = (CommandVarSetNumber)Node;
            txtContextName.Text = node.VariableName;
            optModeAdd.Checked = node.ValueOperation == CommandVarSetNumber.EOperation.Add;
            optModeMul.Checked = node.ValueOperation == CommandVarSetNumber.EOperation.Multiply;
            optModeDiv.Checked = node.ValueOperation == CommandVarSetNumber.EOperation.Divide;
            optModeSet.Checked = node.ValueOperation == CommandVarSetNumber.EOperation.Set;
            sveValue.SetValue(node.Value);
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandVarSetNumber)Node;
            node.VariableName = txtContextName.Text.ToUpperInvariant();
            node.Value = sveValue.GetValue();

            if (optModeAdd.Checked)
                node.ValueOperation = CommandVarSetNumber.EOperation.Add;
            else if (optModeMul.Checked)
                node.ValueOperation = CommandVarSetNumber.EOperation.Multiply;
            else if (optModeDiv.Checked)
                node.ValueOperation = CommandVarSetNumber.EOperation.Divide;
            else
                node.ValueOperation = CommandVarSetNumber.EOperation.Set;
        }

    }

}
