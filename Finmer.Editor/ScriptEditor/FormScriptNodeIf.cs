/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Linq;
using System.Windows.Forms;
using Finmer.Core.VisualScripting;
using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for a branch node.
    /// </summary>
    public partial class FormScriptNodeIf : FormScriptNode
    {

        private CommandIf m_Node;

        public FormScriptNodeIf()
        {
            InitializeComponent();
        }

        private void FormScriptNodeIf_Load(object sender, System.EventArgs e)
        {
            m_Node = (CommandIf)Node;

            optModeAll.Checked = m_Node.Mode == CommandIf.EConditionMode.All;
            optModeAny.Checked = m_Node.Mode == CommandIf.EConditionMode.Any;
            optOperandTrue.Checked = m_Node.Operand;
            optOperandFalse.Checked = !m_Node.Operand;
            chkElse.Checked = m_Node.HasElseBranch;

            RebuildConditionList();
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            m_Node.Mode = optModeAll.Checked ? CommandIf.EConditionMode.All : CommandIf.EConditionMode.Any;
            m_Node.Operand = optOperandTrue.Checked;
            m_Node.HasElseBranch = chkElse.Checked;
        }

        private void cmdConditionAdd_Click(object sender, System.EventArgs e)
        {
            using (var picker = new FormVisualScriptConditionPalette())
            {
                if (picker.ShowDialog() != DialogResult.OK)
                    return;

                m_Node.Conditions.Add(picker.NewNode);
                AddConditionToView(picker.NewNode);
            }
        }

        private void cmdConditionRemove_Click(object sender, System.EventArgs e)
        {
            // As a fail-safe, validate selection again
            if (lsvConditions.SelectedIndices.Count != 1)
                return;

            // Delete selection
            int index = lsvConditions.SelectedIndices[0];
            lsvConditions.Items.RemoveAt(index);
            m_Node.Conditions.RemoveAt(index);

            // Update button state
            cmdConditionRemove.Enabled = false;
            cmdAccept.Enabled = m_Node.Conditions.Any();
        }

        private void lsvConditions_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            bool has_selection = lsvConditions.SelectedItems.Count == 1;
            cmdConditionRemove.Enabled = has_selection;
        }

        private void lsvConditions_DoubleClick(object sender, System.EventArgs e)
        {
            // Must have a selection to open
            if (lsvConditions.SelectedItems.Count != 1)
                return;

            // Check what actions we can perform with this tree item
            var selected_item = lsvConditions.SelectedItems[0];
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

        private void RebuildConditionList()
        {
            lsvConditions.Items.Clear();

            foreach (var condition in m_Node.Conditions)
                AddConditionToView(condition);
        }

        private void AddConditionToView(ScriptCondition condition)
        {
            lsvConditions.Items.Add(new ListViewItem
            {
                Text = condition.GetEditorDescription(),
                Tag = condition
            });

            // If any condition is added to the view, then the branch must also have a condition, and is therefore valid
            cmdAccept.Enabled = true;
        }

    }

}
