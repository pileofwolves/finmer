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
    /// Node editor form for CommandTimeAdvance.
    /// </summary>
    public partial class FormScriptCmdTimeAdvance : FormScriptNode
    {

        public FormScriptCmdTimeAdvance()
        {
            InitializeComponent();
        }

        private void FormScriptCmdTimeAdvance_Load(object sender, System.EventArgs e)
        {
            var node = (CommandTimeAdvance)Node;
            valHours.SetValue(node.Hours);
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandTimeAdvance)Node;
            node.Hours = valHours.GetValue();
        }

    }

}
