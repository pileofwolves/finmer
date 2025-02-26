/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for CommandSetInstruction.
    /// </summary>
    public partial class FormScriptCmdSetLocation : FormScriptNode
    {

        public FormScriptCmdSetLocation()
        {
            InitializeComponent();
        }

        private void FormScriptCmdSetLocation_Load(object sender, System.EventArgs e)
        {
            var node = (CommandSetLocation)Node;
            sveValue.SetValue(node.Value);
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandSetLocation)Node;
            node.Value = sveValue.GetValue();
        }

    }

}
