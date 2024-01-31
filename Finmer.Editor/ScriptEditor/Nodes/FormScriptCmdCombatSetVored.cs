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
    /// Node editor form for CommandCombatSetVored.
    /// </summary>
    public partial class FormScriptCmdCombatSetVored : FormScriptNode
    {

        public FormScriptCmdCombatSetVored()
        {
            InitializeComponent();
        }

        private void FormScriptCmdCombatSetVored_Load(object sender, EventArgs e)
        {
            var node = (CommandCombatSetVored)Node;
            instigator.SetParticipantID(node.PredatorName);
            target.SetParticipantID(node.PreyName);
            optModeSet.Checked = node.Mode == CommandCombatSetVored.EMode.Set;
            optModeUnset.Checked = node.Mode == CommandCombatSetVored.EMode.Unset;
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            // Copy settings to node
            var node = (CommandCombatSetVored)Node;
            node.PredatorName = instigator.GetParticipantID();
            node.PreyName = target.GetParticipantID();
            node.Mode = optModeSet.Checked ? CommandCombatSetVored.EMode.Set : CommandCombatSetVored.EMode.Unset;
        }

    }

}
