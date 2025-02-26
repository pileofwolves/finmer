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
    /// Node editor form for CommandCombatSetGrappled.
    /// </summary>
    public partial class FormScriptCmdCombatSetGrappled : FormScriptNode
    {

        public FormScriptCmdCombatSetGrappled()
        {
            InitializeComponent();
        }

        private void FormScriptCmdCombatSetGrappled_Load(object sender, EventArgs e)
        {
            var node = (CommandCombatSetGrappled)Node;
            instigator.SetParticipantID(node.InstigatorName);
            target.SetParticipantID(node.TargetName);
            optModeSet.Checked = node.Mode == CommandCombatSetGrappled.EMode.Set;
            optModeUnset.Checked = node.Mode == CommandCombatSetGrappled.EMode.Unset;
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            // Copy settings to node
            var node = (CommandCombatSetGrappled)Node;
            node.InstigatorName = instigator.GetParticipantID();
            node.TargetName = target.GetParticipantID();
            node.Mode = optModeSet.Checked ? CommandCombatSetGrappled.EMode.Set : CommandCombatSetGrappled.EMode.Unset;
        }

    }

}
