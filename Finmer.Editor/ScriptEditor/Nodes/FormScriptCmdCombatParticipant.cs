/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows.Forms;
using Finmer.Core;
using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Form for configuring an NPC participant as part of CommandCombatBegin.
    /// </summary>
    public partial class FormScriptCmdCombatParticipant : Form
    {

        /// <summary>
        /// The participant data being edited by this editor window.
        /// </summary>
        public CommandCombatBegin.Participant Participant { get; set; }

        public FormScriptCmdCombatParticipant()
        {
            InitializeComponent();
        }

        private void FormScriptCmdCombatParticipant_Load(object sender, EventArgs e)
        {
            txtNpcName.Text = Participant.ID;
            apcNpcAsset.SelectedGuid = Participant.Creature;
            chkIsAlly.Checked = Participant.IsAlly;
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            // Update the participant with UI state
            Participant = new CommandCombatBegin.Participant
            {
                ID = txtNpcName.Text.MakeSafeIdentifier(),
                Creature = apcNpcAsset.SelectedGuid,
                IsAlly = chkIsAlly.Checked
            };

            // The dialog will be closed due to the DialogResult property on the button
        }

    }

}
