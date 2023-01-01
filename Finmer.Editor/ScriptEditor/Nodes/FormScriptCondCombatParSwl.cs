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
    /// Node editor form for ConditionCombatParSwallowed.
    /// </summary>
    public partial class FormScriptCondCombatParSwl : FormScriptNode
    {

        public FormScriptCondCombatParSwl()
        {
            InitializeComponent();
        }

        private void FormScriptCondCombatParSwl_Load(object sender, System.EventArgs e)
        {
            var node = (ConditionCombatParSwallowed)Node;
            txtPreyName.Text = node.ParticipantName;
            txtPredatorName.Text = node.PredatorName;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (ConditionCombatParSwallowed)Node;
            node.ParticipantName = txtPreyName.Text.ToLowerInvariant();
            node.PredatorName = txtPredatorName.Text.ToLowerInvariant();
        }

    }

}
