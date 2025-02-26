/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
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

        private void FormScriptCondCombatParSwl_Load(object sender, EventArgs e)
        {
            var node = (ConditionCombatParSwallowed)Node;
            prey.SetParticipantID(node.ParticipantName);
            predator.SetParticipantID(node.PredatorName);

            chkWithPredator.Checked = !String.IsNullOrEmpty(node.PredatorName);
            predator.Enabled = chkWithPredator.Checked;
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            var node = (ConditionCombatParSwallowed)Node;
            node.ParticipantName = prey.GetParticipantID();
            node.PredatorName = chkWithPredator.Checked ? predator.GetParticipantID() : String.Empty;
        }

        private void chkWithPredator_CheckedChanged(object sender, EventArgs e)
        {
            predator.Enabled = chkWithPredator.Checked;
        }

    }

}
