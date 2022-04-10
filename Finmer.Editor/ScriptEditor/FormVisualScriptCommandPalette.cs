/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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

    }

}
