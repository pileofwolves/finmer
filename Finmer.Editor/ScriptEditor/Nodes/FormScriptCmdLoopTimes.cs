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
    /// Node editor form for CommandLoopTimes.
    /// </summary>
    public partial class FormScriptCmdLoopTimes : FormScriptNode
    {

        public FormScriptCmdLoopTimes()
        {
            InitializeComponent();
        }

        private void FormScriptCmdLoopTimes_Load(object sender, System.EventArgs e)
        {
            var node = (CommandLoopTimes)Node;
            sveValue.SetValue(node.RepeatCount);
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandLoopTimes)Node;
            node.RepeatCount = sveValue.GetValue();
        }

    }

}
