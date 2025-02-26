/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Linq;
using System.Windows.Forms;
using Finmer.Core.Serialization;
using Finmer.Core.VisualScripting;

namespace Finmer.Editor
{

    /// <summary>
    /// Editor control for ScriptConditionGroup.
    /// </summary>
    public partial class ConditionGroupEditor : UserControl
    {

        /// <summary>
        /// The group object that is represented in this editor.
        /// </summary>
        public ScriptConditionGroup Group { get; private set; }

        /// <summary>
        /// Occurs when the validity of the condition group is reassessed.
        /// </summary>
        public event Action<bool> GroupValidityChanged;

        public ConditionGroupEditor()
        {
            InitializeComponent();
        }

        public void SetGroup(ScriptConditionGroup source)
        {
            // Make a copy of the node so we can freely edit the conditions array
            Group = AssetSerializer.DuplicateAsset(source);

            // Populate UI
            optModeAll.Checked = Group.Mode == ScriptConditionGroup.EConditionMode.All;
            optModeAny.Checked = Group.Mode == ScriptConditionGroup.EConditionMode.Any;
            optOperandTrue.Checked = Group.Operand;
            optOperandFalse.Checked = !Group.Operand;
            RebuildConditionList();
        }

        private void cmdConditionAdd_Click(object sender, EventArgs e)
        {
            using (var picker = new FormVisualScriptConditionPalette())
            {
                if (picker.ShowDialog() != DialogResult.OK)
                    return;

                Group.Tests.Add(picker.NewNode);
                AddConditionToView(picker.NewNode);
            }
        }

        private void cmdConditionRemove_Click(object sender, EventArgs e)
        {
            // As a fail-safe, validate selection again
            int index = lsvConditions.SelectedIndex;
            if (index == -1)
                return;

            // Delete selection
            lsvConditions.Items.RemoveAt(index);
            Group.Tests.RemoveAt(index);
            cmdConditionRemove.Enabled = false;

            // The group is valid as long as we have at least one condition in it
            GroupValidityChanged?.Invoke(Group.Tests.Any());
        }

        private void lsvConditions_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool has_selection = lsvConditions.SelectedIndex != -1;
            cmdConditionRemove.Enabled = has_selection;
        }

        private void lsvConditions_ItemDoubleClick(object sender, EventArgs e)
        {
            // Must have a selection to open
            var selected_item = lsvConditions.SelectedItem;
            if (selected_item == null)
                return;

            // Check what actions we can perform with this tree item
            var tag = selected_item.Tag as ScriptNode;
            if (tag != null)
            {
                using (FormScriptNode editor_form = ScriptNodeFormMapper.CreateEditorForm(tag))
                {
                    // If this node cannot be edited, there's nothing more to do
                    if (editor_form == null)
                        return;

                    // Display the node edit dialog
                    if (editor_form.ShowDialog(this) == DialogResult.OK)
                        RebuildConditionList();
                }
            }
        }

        private void optModeAll_CheckedChanged(object sender, EventArgs e)
        {
            Group.Mode = optModeAll.Checked ? ScriptConditionGroup.EConditionMode.All : ScriptConditionGroup.EConditionMode.Any;
        }

        private void optOperandTrue_CheckedChanged(object sender, EventArgs e)
        {
            Group.Operand = optOperandTrue.Checked;
        }

        private void RebuildConditionList()
        {
            lsvConditions.Items.Clear();

            foreach (var condition in Group.Tests)
                AddConditionToView(condition);
        }

        private void AddConditionToView(ScriptCondition condition)
        {
            lsvConditions.Items.Add(new ListViewItem
            {
                Text = condition.GetEditorDescription(Program.LoadedContent),
                ForeColor = BandedListView.ConvertColor(condition.GetEditorColor()),
                Tag = condition
            });

            // If any condition is added to an empty group, then the group becomes valid
            if (lsvConditions.Items.Count == 1)
                GroupValidityChanged?.Invoke(true);
        }

    }

}
