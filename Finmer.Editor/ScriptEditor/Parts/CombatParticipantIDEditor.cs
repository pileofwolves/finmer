/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows.Forms;
using Finmer.Core;
using Finmer.Core.VisualScripting;

namespace Finmer.Editor
{

    public partial class CombatParticipantIDEditor : UserControl
    {

        public CombatParticipantIDEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns the internal ID of the configured participant.
        /// </summary>
        public string GetParticipantID()
        {
            return optModePlayer.Checked
                ? CombatUtilities.k_PlayerParticipantID
                : txtNPC.Text.MakeSafeIdentifier();
        }

        /// <summary>
        /// Sets up the UI for the specified participant ID.
        /// </summary>
        public void SetParticipantID(string id)
        {
            if (id.Equals(CombatUtilities.k_PlayerParticipantID, StringComparison.InvariantCultureIgnoreCase))
            {
                optModePlayer.Checked = true;
            }
            else
            {
                optModeNPC.Checked = true;
                txtNPC.Text = id;
            }
        }

        private void optModeNPC_CheckedChanged(object sender, EventArgs e)
        {
            txtNPC.Enabled = optModeNPC.Checked;
            lblNPC.Enabled = optModeNPC.Checked;
        }

    }

}
