/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for ConditionVarString.
    /// </summary>
    public partial class FormScriptCondVarString : FormScriptNode
    {

        public FormScriptCondVarString()
        {
            InitializeComponent();
        }

        private void FormScriptCondVarString_Load(object sender, System.EventArgs e)
        {
            var node = (ConditionVarString)Node;
            txtVarName.Text = node.VariableName;
            sveRhs.SetValue(node.Operand);
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (ConditionVarString)Node;
            node.VariableName = txtVarName.Text.ToUpperInvariant();
            node.Operand = sveRhs.GetValue();
        }

    }

}
