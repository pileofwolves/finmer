/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for CommandPlayerSetName.
    /// </summary>
    public partial class FormScriptCmdPlayerSetName : FormScriptNode
    {

        public FormScriptCmdPlayerSetName()
        {
            InitializeComponent();
        }

        private void FormScriptCmdSetPlayerName_Load(object sender, System.EventArgs e)
        {
            var node = (CommandPlayerSetName)Node;
            sveValue.SetValue(node.Value);
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandPlayerSetName)Node;
            node.Value = sveValue.GetValue();
        }

    }

}
