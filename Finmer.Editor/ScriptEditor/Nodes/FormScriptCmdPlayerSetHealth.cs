/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for CommandPlayerSetHealth.
    /// </summary>
    public partial class FormScriptCmdPlayerSetHealth : FormScriptNode
    {

        public FormScriptCmdPlayerSetHealth()
        {
            InitializeComponent();
        }

        private void FormScriptCmdPlayerSetHealth_Load(object sender, System.EventArgs e)
        {
            var node = (CommandPlayerSetHealth)Node;
            sveValue.SetValue(node.Value);
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandPlayerSetHealth)Node;
            node.Value = sveValue.GetValue();
        }

    }

}
