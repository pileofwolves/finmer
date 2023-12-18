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
    /// Node editor form for CommandTimeSetHour.
    /// </summary>
    public partial class FormScriptCmdTimeSetHour : FormScriptNode
    {

        public FormScriptCmdTimeSetHour()
        {
            InitializeComponent();
        }

        private void FormScriptCmdTimeAdvance_Load(object sender, System.EventArgs e)
        {
            var node = (CommandTimeSetHour)Node;
            valHours.SetValue(node.Hour);
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandTimeSetHour)Node;
            node.Hour = valHours.GetValue();
        }

    }

}
