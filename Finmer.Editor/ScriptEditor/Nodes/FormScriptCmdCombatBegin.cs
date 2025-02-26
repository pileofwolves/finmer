/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
                AddParticipant(npc);

            // Callbacks
            chkCallbackCS.Checked = node.CallbackCombatStart != null;
            chkCallbackRS.Checked = node.CallbackRoundStart != null;
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
                node.Participants.Add((CommandCombatBegin.Participant)item.Tag);

            // Callbacks
            node.CallbackCombatStart = chkCallbackCS.Checked ? (node.CallbackCombatStart ?? new List<ScriptNode>()) : null;
            node.CallbackRoundStart = chkCallbackRS.Checked ? (node.CallbackRoundStart ?? new List<ScriptNode>()) : null;
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
                cmdNpcEdit.Enabled = false;
                cmdNpcRemove.Enabled = false;
                return;
            }

            // Re-enable UI
            cmdNpcEdit.Enabled = true;
            cmdNpcRemove.Enabled = true;
            m_SelectedParticipant = lsvNpcs.SelectedItems[0];
        }

        private void cmdNpcAdd_Click(object sender, EventArgs e)
        {
            // Open participant editor dialog with a default-initialized (and thus empty) participant
            using (var window = new FormScriptCmdCombatParticipant())
            {
                // Abort editing if the dialog was dismissed
                if (window.ShowDialog() != DialogResult.OK)
                    return;

                // Add the newly configured participant to the tree
                AddParticipant(window.Participant);
            }
        }

        private void cmdNpcEdit_Click(object sender, EventArgs e)
        {
            EditSelectedParticipant();
        }

        private void cmdNpcRemove_Click(object sender, EventArgs e)
        {
            // Button should be disabled, but best to check again
            if (lsvNpcs.SelectedItems.Count == 0)
                return;

            // Delete the selected row
            lsvNpcs.Items.Remove(lsvNpcs.SelectedItems[0]);
        }

        private void lsvNpcs_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedParticipant();
        }

        private void AddParticipant(CommandCombatBegin.Participant npc)
        {
            var item = new ListViewItem
            {
                Text = npc.ID,
                Tag = npc,
                SubItems =
                {
                    new ListViewItem.ListViewSubItem
                    {
                        Text = Program.LoadedContent.GetAssetName(npc.Creature)
                    },
                    new ListViewItem.ListViewSubItem
                    {
                        Text = npc.IsAlly ? "Yes" : "No"
                    }
                }
            };
            lsvNpcs.Items.Add(item);
        }

        private void EditSelectedParticipant()
        {
            // Safeguard
            if (m_SelectedParticipant == null)
                return;

            // Open editor window for participant
            CommandCombatBegin.Participant npc = (CommandCombatBegin.Participant)m_SelectedParticipant.Tag;
            using (var window = new FormScriptCmdCombatParticipant { Participant = npc })
            {
                // Abort editing if the dialog was dismissed
                if (window.ShowDialog() != DialogResult.OK)
                    return;

                // Update tree with new participant state
                npc = window.Participant;
                m_SelectedParticipant.Tag = npc;
                m_SelectedParticipant.Text = npc.ID;
                m_SelectedParticipant.SubItems[1].Text = Program.LoadedContent.GetAssetName(npc.Creature);
                m_SelectedParticipant.SubItems[2].Text = npc.IsAlly ? "Yes" : "No";
            }
        }

    }

}
