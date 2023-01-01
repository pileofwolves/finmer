/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
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
    /// Represents a palette form from which the user can select a script command.
    /// </summary>
    public partial class FormVisualScriptCommandPalette : Form
    {

        /// <summary>
        /// The node that was newly created through this palette form, or null if not (yet) set.
        /// </summary>
        public ScriptNode NewNode { get; private set; }

        public FormVisualScriptCommandPalette()
        {
            InitializeComponent();
        }

        private void AddScriptNode(ScriptNode node)
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

                // Keep track of the created node
                NewNode = editor_form?.Node ?? node;
            }

            // All done, close and notify the script editor that we're done
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmdAdvLuaScript_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandInlineSnippet());
        }

        private void cmdFlowIf_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandIf());
        }

        private void cmdFlowLoop_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandLoop());
        }

        private void cmdFlowLoopBreak_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandLoopBreak());
        }

        private void cmdFlowExit_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandExitScript());
        }

        private void cmdFlowSleep_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandSleep());
        }

        private void cmdFlowComment_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandComment());
        }

        private void cmdUILog_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandLog());
        }

        private void cmdUILogSplit_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandLogSplit());
        }

        private void cmdUIClearLog_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandLogClear());
        }

        private void cmdUIAddLink_Click(object sender, System.EventArgs e)
        {
            // TODO: Larger refactor to make compass links part of the scene graph
        }

        private void cmdUISetInstruction_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandSetInstruction());
        }

        private void cmdUISetInventoryEnabled_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandSetInventoryEnabled());
        }

        private void cmdUISetLocation_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandSetLocation());
        }

        private void cmdPlayerSetName_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandPlayerSetName());
        }

        private void cmdPlayerSetSpecies_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandPlayerSetSpecies());
        }

        private void cmdPlayerSetStat_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandPlayerSetStat());
        }

        private void cmdPlayerSetHealth_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandPlayerSetHealth());
        }

        private void cmdPlayerRestoreHealth_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandPlayerHealAll());
        }

        private void cmdPlayerSetMoney_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandPlayerSetMoney());
        }

        private void cmdPlayerSetEquipment_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandPlayerSetEquipment());
        }

        private void cmdPlayerAddItem_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandPlayerSetItem());
        }

        private void cmdPlayerAddXP_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandPlayerAddXP());
        }

        private void cmdPlayerAddAbilityPoints_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandPlayerAddAP());
        }

        private void cmdSceneSetScene_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandSetScene());
        }

        private void cmdSceneCombat_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandCombatBegin());
        }

        private void cmdSceneCombatStop_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandCombatEnd());
        }

        private void cmdSceneShop_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandShop());
        }

        private void cmdSceneEndGame_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandEndGame());
        }

        private void cmdJournalUpdate_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandJournalUpdate());
        }

        private void cmdJournalClose_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandJournalClose());
        }

        private void cmdSaveDialog_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandSaveDialog());
        }

        private void cmdSaveCheckpoint_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandSaveCheckpoint());
        }

        private void cmdDataSetFlag_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandVarSetFlag());
        }

        private void cmdDataSetNumber_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandVarSetNumber());
        }

        private void cmdDataSetString_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandVarSetString());
        }

        private void cmdTextSetContext_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandGrammarSetContext());
        }

        private void cmdTextSetVariable_Click(object sender, System.EventArgs e)
        {
            AddScriptNode(new CommandGrammarSetVariable());
        }

    }

}
