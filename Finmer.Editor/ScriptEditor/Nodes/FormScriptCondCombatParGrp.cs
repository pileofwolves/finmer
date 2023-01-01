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
    /// Node editor form for ConditionCombatParGrappling.
    /// </summary>
    public partial class FormScriptCondCombatParGrp : FormScriptNode
    {

        public FormScriptCondCombatParGrp()
        {
            InitializeComponent();
        }

        private void FormScriptCondCombatParGrp_Load(object sender, System.EventArgs e)
        {
            var node = (ConditionCombatParGrappling)Node;
            txtNpcName.Text = node.ParticipantName;
            txtPartnerName.Text = node.TargetName;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (ConditionCombatParGrappling)Node;
            node.ParticipantName = txtNpcName.Text.ToLowerInvariant();
            node.TargetName = txtPartnerName.Text.ToLowerInvariant();
        }

    }

}
