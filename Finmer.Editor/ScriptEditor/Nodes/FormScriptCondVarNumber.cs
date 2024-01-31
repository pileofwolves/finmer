/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core;
using Finmer.Core.VisualScripting;
using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for ConditionVarNumber.
    /// </summary>
    public partial class FormScriptCondVarNumber : FormScriptNode
    {

        public FormScriptCondVarNumber()
        {
            InitializeComponent();
        }

        private void FormScriptCondVarNumber_Load(object sender, System.EventArgs e)
        {
            var node = (ConditionVarNumber)Node;
            txtVarName.Text = node.VariableName;
            cmbOperator.SelectedIndex = (int)node.Operator;
            sveRhs.SetValue(node.RightOperand);
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (ConditionVarNumber)Node;
            node.VariableName = txtVarName.Text.MakeSafeIdentifier();
            node.Operator = (ScriptConditionNumberComparison.EOperator)cmbOperator.SelectedIndex;
            node.RightOperand = sveRhs.GetValue();
        }

    }

}
