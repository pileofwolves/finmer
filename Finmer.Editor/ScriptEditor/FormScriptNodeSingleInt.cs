/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.VisualScripting;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for editing one integer parameter.
    /// </summary>
    public partial class FormScriptNodeSingleInt : FormScriptNode
    {

        public FormScriptNodeSingleInt()
        {
            InitializeComponent();
        }

        private void FormScriptNodeSingleInt_Load(object sender, System.EventArgs e)
        {
            var command = (ScriptCommandSingleInt)Node;
            nudValue.Value = command.Value;

            Text = command.GetEditorWindowTitle();
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            ((ScriptCommandSingleInt)Node).Value = (int)nudValue.Value;
        }

    }

}
