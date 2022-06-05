/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Finmer.Core.Assets;
using Finmer.Core.VisualScripting;
using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for CommandCombatBegin.
    /// </summary>
    public partial class FormScriptCmdCombatBegin : FormScriptNode
    {

        private ListViewItem m_SelectedParticipant;

        public FormScriptCmdCombatBegin()
        {
            InitializeComponent();
        }

        private void FormScriptCmdCombatBegin_Load(object sender, EventArgs e)
        {
            var node = (CommandCombatBegin)Node;

            // Basic configuration
            chkIncludePlayer.Checked = node.IncludePlayer;

            // NPC participants
            foreach (var npc in node.Participants)
            {
                var item = new ListViewItem
                {
                    Text = npc.Key,
                    Tag = npc.Value,
                    SubItems =
                    {
                        new ListViewItem.ListViewSubItem
                        {
                            Text = npc.Value.ToString()
                        }
                    }
                };
                lsvNpcs.Items.Add(item);
            }

            // Callbacks
            chkCallbackRE.Checked = node.CallbackRoundEnd != null;
            chkCallbackPK.Checked = node.CallbackPlayerKilled != null;
            chkCallbackCK.Checked = node.CallbackCreatureKilled != null;
            chkCallbackCV.Checked = node.CallbackCreatureVored != null;
            chkCallbackCR.Checked = node.CallbackCreatureReleased != null;
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            var node = (CommandCombatBegin)Node;

            // Basic settings
            node.IncludePlayer = chkIncludePlayer.Checked;

            // NPC participants
            node.Participants.Clear();
            foreach (ListViewItem item in lsvNpcs.Items)
                node.Participants[item.Text.ToLowerInvariant()] = (Guid)item.Tag;

            // Callbacks
            node.CallbackRoundEnd = chkCallbackRE.Checked ? (node.CallbackRoundEnd ?? new List<ScriptNode>()) : null;
            node.CallbackPlayerKilled = chkCallbackPK.Checked ? (node.CallbackPlayerKilled ?? new List<ScriptNode>()) : null;
            node.CallbackCreatureKilled = chkCallbackCK.Checked ? (node.CallbackCreatureKilled ?? new List<ScriptNode>()) : null;
            node.CallbackCreatureVored = chkCallbackCV.Checked ? (node.CallbackCreatureVored ?? new List<ScriptNode>()) : null;
            node.CallbackCreatureReleased = chkCallbackCR.Checked ? (node.CallbackCreatureReleased ?? new List<ScriptNode>()) : null;
        }

        private void lsvNpcs_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If item is deselected, reset the participant editing UI
            if (lsvNpcs.SelectedItems.Count != 1)
            {
                m_SelectedParticipant = null;
                grpParticipantSettings.Enabled = false;
                cmdNpcRemove.Enabled = false;
                return;
            }

            // Re-enable UI
            m_SelectedParticipant = null;
            grpParticipantSettings.Enabled = true;
            cmdNpcRemove.Enabled = true;

            // Show state of the selected participant
            var item = lsvNpcs.SelectedItems[0];
            txtNpcName.Text = item.Text;
            apcNpcAsset.SelectedGuid = (Guid)item.Tag;

            // Assign selected participant only after UI is configured, so that all content-changed callbacks early-out
            m_SelectedParticipant = lsvNpcs.SelectedItems[0];
        }

        private void txtNpcName_TextChanged(object sender, EventArgs e)
        {
            if (m_SelectedParticipant == null)
                return;

            // Store the new NPC variable name
            m_SelectedParticipant.Text = txtNpcName.Text;
        }

        private void apcNpcAsset_SelectedAssetChanged(object sender, EventArgs e)
        {
            if (m_SelectedParticipant == null)
                return;

            // Store the new Creature asset ID
            var creature = (AssetCreature)apcNpcAsset.SelectedAsset;
            m_SelectedParticipant.Tag = apcNpcAsset.SelectedGuid;
            m_SelectedParticipant.SubItems[1].Text = creature?.Name ?? "(not set)";
        }

        private void cmdNpcAdd_Click(object sender, EventArgs e)
        {
            var item = new ListViewItem
            {
                Text = String.Empty,
                Tag = Guid.Empty,
                Selected = true,
                SubItems =
                {
                    new ListViewItem.ListViewSubItem
                    {
                        Text = "(not set)"
                    }
                }
            };
            lsvNpcs.Items.Add(item);
        }

        private void cmdNpcRemove_Click(object sender, EventArgs e)
        {
            // Button should be disabled, but best to check again
            if (lsvNpcs.SelectedItems.Count == 0)
                return;

            // Delete the selected row
            lsvNpcs.Items.Remove(lsvNpcs.SelectedItems[0]);
        }

    }

}
