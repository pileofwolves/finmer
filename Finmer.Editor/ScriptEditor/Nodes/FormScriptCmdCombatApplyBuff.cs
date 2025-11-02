/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Finmer.Core;
using Finmer.Core.Buffs;
using Finmer.Core.Serialization;
using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for CommandCombatApplyBuff.
    /// </summary>
    public partial class FormScriptCmdCombatApplyBuff : FormScriptNode
    {

        private CommandCombatApplyBuff m_Node;

        public FormScriptCmdCombatApplyBuff()
        {
            InitializeComponent();
        }

        private void FormScriptCmdCombatApplyBuff_Load(object sender, EventArgs e)
        {
            // Duplicate the asset so we can safely edit the buff properties
            m_Node = AssetSerializer.DuplicateAsset((CommandCombatApplyBuff)Node);

            // Populate UI
            optTargetPlayer.Checked = m_Node.Target == CommandCombatApplyBuff.ETarget.Player;
            optTargetNPC.Checked = m_Node.Target == CommandCombatApplyBuff.ETarget.NPC;
            txtTargetNPC.Text = m_Node.ParticipantID;
            nudDuration.Value = m_Node.Duration;
            UpdateBuffEditor();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            // Copy settings to replacement instance
            m_Node.Target = optTargetPlayer.Checked ? CommandCombatApplyBuff.ETarget.Player : CommandCombatApplyBuff.ETarget.NPC;
            m_Node.ParticipantID = txtTargetNPC.Text.MakeSafeIdentifier();
            m_Node.Duration = (int)nudDuration.Value;

            // Replace output node with the edited one
            Node = m_Node;
        }

        private void optTargetPlayer_CheckedChanged(object sender, EventArgs e)
        {
            lblTargetNPC.Enabled = optTargetNPC.Checked;
            txtTargetNPC.Enabled = optTargetNPC.Checked;
        }

        private void ReplaceBuff(Buff buff)
        {
            m_Node.Effect = buff;
            UpdateBuffEditor();
            OpenBuffEditor();
        }

        private void UpdateBuffEditor()
        {
            cmdBuffDelete.Enabled = m_Node.Effect != null;
            lblBuffText.Text = m_Node.Effect?.GetDescription() ?? "[No effect configured]";
        }

        private void OpenBuffEditor()
        {
            Debug.Assert(m_Node.Effect != null);

            // Try creating a form for editing the contained buff
            using (var form = BaseEffectEditor.CreateBuffEditor(m_Node.Effect))
            {
                // Some buffs do not have editable properties
                if (form == null)
                    return;

                // Allow user to edit the buff, and apply any changes
                if (form.ShowDialog() == DialogResult.OK)
                {
                    m_Node.Effect = form.BuffInstance;
                    UpdateBuffEditor();
                }
            }
        }

        private void cmdBuffDelete_Click(object sender, EventArgs e)
        {
            // Reset the contained buff, so it can be replaced
            m_Node.Effect = null;
            UpdateBuffEditor();
        }

        private void cmdBuffEdit_Click(object sender, EventArgs e)
        {
            if (m_Node.Effect == null)
            {
                // Open a menu for selecting a buff to insert
                mnuBuffAdd.Show(cmdBuffEdit, new Point(0, cmdBuffEdit.Height));
            }
            else
            {
                // Edit the contained buff
                OpenBuffEditor();
            }
        }

        private void mnuEffectDiceAttack_Click(object sender, EventArgs e)
        {
            ReplaceBuff(new BuffAttackDice());
        }

        private void mnuEffectDiceDefense_Click(object sender, EventArgs e)
        {
            ReplaceBuff(new BuffDefenseDice());
        }

        private void mnuEffectDiceGrapple_Click(object sender, EventArgs e)
        {
            ReplaceBuff(new BuffGrappleDice());
        }

        private void mnuEffectDiceSwallow_Click(object sender, EventArgs e)
        {
            ReplaceBuff(new BuffSwallowDice());
        }

        private void mnuEffectDiceStruggle_Click(object sender, EventArgs e)
        {
            ReplaceBuff(new BuffStruggleDice());
        }

        private void mnuEffectStatHPOverTime_Click(object sender, EventArgs e)
        {
            ReplaceBuff(new BuffHealthOverTime());
        }

        private void mnuEffectStun_Click(object sender, EventArgs e)
        {
            ReplaceBuff(new BuffStun());
        }

    }

}
