/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows.Forms;
using Finmer.Core.VisualScripting;
using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Represents a palette form from which the user can select a script value.
    /// </summary>
    public partial class FormVisualScriptConditionPalette : Form
    {

        /// <summary>
        /// The node that was newly created through this palette form, or null if not (yet) set.
        /// </summary>
        public ScriptCondition NewNode { get; private set; }

        public FormVisualScriptConditionPalette()
        {
            InitializeComponent();
        }

        private void AddScriptNode(ScriptCondition node)
        {
            // Open the edit dialog so this node can be initially configured
            using (var editor_form = ScriptNodeFormMapper.CreateEditorForm(node))
            {
                // If the node does not require an editor form, then don't display one
                if (editor_form != null)
                {
                    // Present the dialog. If the user exited out, then drop this node.
                    var dr = editor_form.ShowDialog();
                    if (dr != DialogResult.OK)
                        return;
                }
            }

            // Otherwise, the user finished setting up the new node, and we can add it to the tree
            NewNode = node;
            DialogResult = DialogResult.OK;

            // All done, close and notify the script editor that we're done
            Close();
        }

        private void cmdCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmdPlayerGetStat_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionPlayerStat());
        }

        private void cmdPlayerGetHealth_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionPlayerHealth());
        }

        private void cmdPlayerGetMaxHealth_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionPlayerHealthMax());
        }

        private void cmdPlayerGetMoney_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionPlayerMoney());
        }

        private void cmdPlayerGetEquipment_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionPlayerEquipment());
        }

        private void cmdPlayerHasItem_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionPlayerHasItem());
        }

        private void cmdPlayerGetLevel_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionPlayerLevel());
        }

        private void cmdAdvLuaScript_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionInlineSnippet());
        }

        private void cmdVarFlag_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionVarFlag());
        }

        private void cmdVarNum_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionVarNumber());
        }

        private void cmdVarStr_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionVarString());
        }

        private void cmdCombatActive_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionCombatActive());
        }

        private void cmdCombatDead_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionCombatParDead());
        }

        private void cmdCombatGrappling_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionCombatParGrappling());
        }

        private void cmdCombatSwallowed_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionCombatParSwallowed());
        }

        private void cmdSystemExplorer_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionIsExplorerEnabled());
        }

        private void cmdSystemDisposal_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionIsDisposalEnabled());
        }

        private void cmdSystemDevMode_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionIsDevModeEnabled());
        }

        private void cmdTimeDay_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionTimeDay());
        }

        private void cmdTimeHour_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionTimeHour());
        }

        private void cmdTimeHourTotal_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionTimeHourTotal());
        }

        private void cmdTimeNight_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new ConditionTimeIsNight());
        }

    }

}
