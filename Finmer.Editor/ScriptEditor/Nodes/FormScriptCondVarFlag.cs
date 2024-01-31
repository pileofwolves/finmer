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
    /// Node editor form for ConditionVarFlag.
    /// </summary>
    public partial class FormScriptCondVarFlag : FormScriptNode
    {

        public FormScriptCondVarFlag()
        {
            InitializeComponent();
        }

        private void FormScriptCondVarFlag_Load(object sender, System.EventArgs e)
        {
            var node = (ConditionVarFlag)Node;
            txtVarName.Text = node.VariableName;
            sveRhs.SetValue(node.Operand);
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (ConditionVarFlag)Node;
            node.VariableName = txtVarName.Text.MakeSafeIdentifier();
            node.Operand = sveRhs.GetValue();
        }

    }

}
