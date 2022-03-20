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
    /// Node editor form for an object comparison.
    /// </summary>
    public partial class FormScriptValueCompare : FormScriptNode
    {

        public FormScriptValueCompare()
        {
            InitializeComponent();
        }

        private void FormScriptValueCompare_Load(object sender, System.EventArgs e)
        {
            var node = (ValueCompare)Node;
            lhs.Value = node.Left;
            rhs.Value = node.Right;
            cmbOperator.SelectedIndex = (int)node.Operator;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (ValueCompare)Node;
            node.Left = lhs.Value;
            node.Right = rhs.Value;
            node.Operator = (ValueCompare.ECompareMode)cmbOperator.SelectedIndex;
        }

    }

}
