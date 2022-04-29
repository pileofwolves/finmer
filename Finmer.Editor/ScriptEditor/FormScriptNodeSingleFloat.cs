/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.VisualScripting;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for editing one floating-point parameter.
    /// </summary>
    public partial class FormScriptNodeSingleFloat : FormScriptNode
    {

        public FormScriptNodeSingleFloat()
        {
            InitializeComponent();
        }

        private void FormScriptNodeSingleFloat_Load(object sender, System.EventArgs e)
        {
            var node = (ScriptCommandSingleFloat)Node;

            switch (node.OperandMode)
            {
                case ScriptCommandSingleFloat.EOperandMode.Literal:
                    optModeLiteral.Checked = true;
                    nudOperand.Value = (decimal)node.OperandLiteral;
                    break;

                case ScriptCommandSingleFloat.EOperandMode.Variable:
                    optModeNumberVar.Checked = true;
                    txtNumberVar.Text = node.OperandText;
                    break;

                case ScriptCommandSingleFloat.EOperandMode.Script:
                    optModeInlineLua.Checked = true;
                    txtLua.Text = node.OperandText;
                    break;
            }

            Text = node.GetEditorWindowTitle();
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (ScriptCommandSingleFloat)Node;

            if (optModeLiteral.Checked)
            {
                node.OperandMode = ScriptCommandSingleFloat.EOperandMode.Literal;
                node.OperandLiteral = (float)nudOperand.Value;
            }
            else if (optModeNumberVar.Checked)
            {
                node.OperandMode = ScriptCommandSingleFloat.EOperandMode.Variable;
                node.OperandText = txtNumberVar.Text.ToUpperInvariant();
            }
            else if (optModeInlineLua.Checked)
            {
                node.OperandMode = ScriptCommandSingleFloat.EOperandMode.Script;
                node.OperandText = txtLua.Text;
            }
        }

        private void optModeLiteral_CheckedChanged(object sender, System.EventArgs e)
        {
            nudOperand.Enabled = optModeLiteral.Checked;
        }

        private void optModeNumberVar_CheckedChanged(object sender, System.EventArgs e)
        {
            txtNumberVar.Enabled = optModeNumberVar.Checked;
        }

        private void optModeInlineLua_CheckedChanged(object sender, System.EventArgs e)
        {
            txtLua.Enabled = optModeInlineLua.Checked;
        }

    }

}
