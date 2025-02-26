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
    /// Node editor form for ConditionCombatParDead.
    /// </summary>
    public partial class FormScriptCondCombatParDead : FormScriptNode
    {

        public FormScriptCondCombatParDead()
        {
            InitializeComponent();
        }

        private void FormScriptCondCombatParDead_Load(object sender, EventArgs e)
        {
            var node = (ConditionCombatParDead)Node;
            participant.SetParticipantID(node.ParticipantName);
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            var node = (ConditionCombatParDead)Node;
            node.ParticipantName = participant.GetParticipantID();
        }

    }

}
