﻿/*
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
    /// Node editor form for configuring a number comparison.
    /// </summary>
    public partial class FormScriptCondNumberComp : FormScriptNode
    {

        public FormScriptCondNumberComp()
        {
            InitializeComponent();
        }

        private void FormScriptCondNumberComp_Load(object sender, System.EventArgs e)
        {
            var node = (ScriptConditionNumberComparison)Node;
            cmbOperator.SelectedIndex = (int)node.Operator;
            nudOperand.Value = (decimal)node.Operand;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (ScriptConditionNumberComparison)Node;
            node.Operator = (ScriptConditionNumberComparison.ECompareMode)cmbOperator.SelectedIndex;
            node.Operand = (float)nudOperand.Value;
        }

    }

}