/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Finmer.Core.Buffs;
using Finmer.Core.Serialization;

namespace Finmer.Editor
{

    /// <summary>
    /// Editor form for an EquipEffectGroup.
    /// </summary>
    public partial class FormEquipEffectGroupEditor : Form
    {

        /// <summary>
        /// The effect group being edited.
        /// </summary>
        public EquipEffectGroup Group { get; set; }

        public FormEquipEffectGroupEditor()
        {
            InitializeComponent();
        }
        private void FormEquipEffectGroupEditor_Load(object sender, EventArgs e)
        {
            // Copy basic settings to UI
            cmbProcMode.SelectedIndex = (int)Group.ProcStyle;
            cmbProcTarget.SelectedIndex = (int)Group.ProcTarget;
            nudProcChance.Value = (decimal)Math.Min(Math.Max(Group.ProcChance * 100.0f, 0.0f), 100.0f);
            nudProcDuration.Value = Group.Duration;
            txtProcString.Text = Group.ProcStringTableKey;

            // Copy buff list
            foreach (var buff in Group.Buffs)
                AddBuff(buff);
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            // Commit basic settings
            Group.ProcStyle = (EquipEffectGroup.EProcStyle)cmbProcMode.SelectedIndex;
            Group.ProcTarget = (EquipEffectGroup.EProcTarget)cmbProcTarget.SelectedIndex;
            Group.ProcChance = (float)nudProcChance.Value / 100.0f;
            Group.Duration = (int)nudProcDuration.Value;
            Group.ProcStringTableKey = txtProcString.Text;

            // Commit buff list
            Group.Buffs.Clear();
            foreach (ListViewItem item in lsvBuffs.Items)
                Group.Buffs.Add((Buff)item.Tag);
        }

        private void cmbProcMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Proc settings only make sense when the proc mode is not 'Always'
            grpProcSettings.Enabled = cmbProcMode.SelectedIndex != 0;
        }

        private void AddBuff(Buff buff)
        {
            // Create a new list entry that represents this effect
            var buff_item = new ListViewItem
            {
                Text = buff.GetEditorDescription(),
                Tag = buff
            };
            lsvBuffs.Items.Add(buff_item);
        }

        private void mnuEffectDiceAttack_Click(object sender, EventArgs e)
        {
            AddBuff(new BuffAttackDice());
        }

        private void mnuEffectDiceDefense_Click(object sender, EventArgs e)
        {
            AddBuff(new BuffDefenseDice());
        }

        private void mnuEffectDiceGrapple_Click(object sender, EventArgs e)
        {
            AddBuff(new BuffGrappleDice());
        }

        private void mnuEffectDiceSwallow_Click(object sender, EventArgs e)
        {
            AddBuff(new BuffSwallowDice());
        }

        private void mnuEffectDiceStruggle_Click(object sender, EventArgs e)
        {
            AddBuff(new BuffStruggleDice());
        }

        private void mnuEffectStatHP_Click(object sender, EventArgs e)
        {
            AddBuff(new BuffHealth());
        }

        private void mnuEffectStatHPOverTime_Click(object sender, EventArgs e)
        {
            AddBuff(new BuffHealthOverTime());
        }

        private void mnuEffectCustomText_Click(object sender, EventArgs e)
        {
            AddBuff(new BuffCustomTooltipText());
        }

        private void stunSkipTurnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBuff(new BuffStun());
        }

        private static BaseEffectEditor CreateEquipEffectEditor(Buff buff)
        {
            switch (buff)
            {
                case SingleDeltaBuff _:
                    return new FormEffectEditorSingleDelta();

                case BuffCustomTooltipText _:
                    return new FormEffectEditorCustomText();

                default:
                    throw new ArgumentException(nameof(buff));
            }
        }

        private void EditSelectedBuff()
        {
            // Validate that there actually is a selection to edit
            if (lsvBuffs.SelectedItems.Count == 0)
                return;

            // Edit the Buff object associated with the selected row
            var item = lsvBuffs.SelectedItems[0];
            var buff = (Buff)item.Tag;
            using (var form = CreateEquipEffectEditor(buff))
            {
                // Make a copy of the buff, so the editor form can edit it safely
                form.BuffInstance = AssetSerializer.DuplicateAsset(buff);

                // Save result only if user clicked OK
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                // Update list item
                item.Text = form.BuffInstance.GetEditorDescription();
                item.Tag = form.BuffInstance;
            }
        }

        private void cmdBuffAdd_Click(object sender, EventArgs e)
        {
            mnuBuffAdd.Show(cmdBuffAdd, new Point(0, cmdBuffAdd.Height));
        }

        private void cmdBuffRemove_Click(object sender, EventArgs e)
        {
            // Delete all selected rows
            foreach (var selection in lsvBuffs.SelectedItems)
                lsvBuffs.Items.Remove((ListViewItem)selection);
        }

        private void cmdBuffEdit_Click(object sender, EventArgs e)
        {
            EditSelectedBuff();
        }

        private void lsvBuffs_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedBuff();
        }

        private void lsvBuffs_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool has_selection = lsvBuffs.SelectedItems.Count != 0;
            cmdBuffRemove.Enabled = has_selection;
            cmdBuffEdit.Enabled = has_selection;
        }

    }

}
