/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core;
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
            cmbOperation.SelectedIndex = (int)node.ValueOperation;

            sveValue.SetValue(node.Value);
            sveValue.Enabled = node.HasRightOperand();
            lblOperandContext.Enabled = sveValue.Enabled;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandVarSetNumber)Node;
            node.VariableName = txtContextName.Text.MakeSafeIdentifier();
            node.ValueOperation = (CommandVarSetNumber.EOperation)cmbOperation.SelectedIndex;
            node.Value = sveValue.GetValue();
        }

        private void cmbOperation_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Update whether value wrapper editor is enabled, based on new operation mode
            var node = (CommandVarSetNumber)Node;
            node.ValueOperation = (CommandVarSetNumber.EOperation)cmbOperation.SelectedIndex;
            sveValue.Enabled = node.HasRightOperand();
            lblOperandContext.Enabled = sveValue.Enabled;

            // Change right operand hint text accordingly
            switch (node.ValueOperation)
            {
                case CommandVarSetNumber.EOperation.Add:
                    lblOperandContext.Text = "Add this value:";
                    break;
                case CommandVarSetNumber.EOperation.Multiply:
                    lblOperandContext.Text = "Multiply by this value:";
                    break;
                case CommandVarSetNumber.EOperation.Divide:
                    lblOperandContext.Text = "Divide by this value:";
                    break;
                case CommandVarSetNumber.EOperation.Set:
                    lblOperandContext.Text = "Overwrite variable with this new value:";
                    break;
                case CommandVarSetNumber.EOperation.Random:
                    lblOperandContext.Text = "Pick random number between 0 and this value, inclusive:";
                    break;
                default:
                    lblOperandContext.Text = "n/a";
                    break;
            }
        }

    }

}
