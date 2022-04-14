/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.VisualScripting;
using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for selecting a player primary stat.
    /// </summary>
    public partial class FormScriptCondPlayerStat : FormScriptNode
    {

        public FormScriptCondPlayerStat()
        {
            InitializeComponent();
        }

        private void FormScriptCondPlayerStat_Load(object sender, System.EventArgs e)
        {
            var node = (ConditionPlayerStat)Node;
            cmbStat.SelectedIndex = (int)node.Stat;
            cmbOperator.SelectedIndex = (int)node.Operator;
            switch (node.OperandMode)
            {
                case ScriptConditionNumberComparison.EOperandMode.Literal:
                    optModeLiteral.Checked = true;
                    nudOperand.Value = (decimal)node.OperandLiteral;
                    break;

                case ScriptConditionNumberComparison.EOperandMode.Variable:
                    optModeNumberVar.Checked = true;
                    txtNumberVar.Text = node.OperandText;
                    break;

                case ScriptConditionNumberComparison.EOperandMode.Script:
                    optModeInlineLua.Checked = true;
                    txtLua.Text = node.OperandText;
                    break;
            }
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (ConditionPlayerStat)Node;
            node.Stat = (ConditionPlayerStat.EStat)cmbStat.SelectedIndex;
            node.Operator = (ScriptConditionNumberComparison.EOperator)cmbOperator.SelectedIndex;

            if (optModeLiteral.Checked)
            {
                node.OperandMode = ScriptConditionNumberComparison.EOperandMode.Literal;
                node.OperandLiteral = (float)nudOperand.Value;
            }
            else if (optModeNumberVar.Checked)
            {
                node.OperandMode = ScriptConditionNumberComparison.EOperandMode.Variable;
                node.OperandText = txtNumberVar.Text.ToUpperInvariant();
            }
            else if (optModeInlineLua.Checked)
            {
                node.OperandMode = ScriptConditionNumberComparison.EOperandMode.Script;
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
