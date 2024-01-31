/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
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

        private void FormScriptCondCombatParGrp_Load(object sender, EventArgs e)
        {
            var node = (ConditionCombatParGrappling)Node;
            participant.SetParticipantID(node.ParticipantName);
            partner.SetParticipantID(node.TargetName);

            chkWithPartner.Checked = !String.IsNullOrEmpty(node.TargetName);
            partner.Enabled = chkWithPartner.Checked;
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            var node = (ConditionCombatParGrappling)Node;
            node.ParticipantName = participant.GetParticipantID();
            node.TargetName = chkWithPartner.Checked ? partner.GetParticipantID() : String.Empty;
        }

        private void chkWithPartner_CheckedChanged(object sender, EventArgs e)
        {
            partner.Enabled = chkWithPartner.Checked;
        }

    }

}
