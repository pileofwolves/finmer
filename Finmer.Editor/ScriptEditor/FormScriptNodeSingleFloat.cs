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
    /// Node editor form for editing one floating-point parameter.
    /// </summary>
    public partial class FormScriptNodeSingleFloat : FormScriptNode
    {

        public FormScriptNodeSingleFloat()
        {
            InitializeComponent();
        }

        private void FormScriptNodeSingleFloat_Load(object sender, System.EventArgs e)
        {
            var command = (ScriptCommandSingleFloat)Node;
            nudValue.Value = (decimal)command.Value;

            Text = command.GetEditorWindowTitle();
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            ((ScriptCommandSingleFloat)Node).Value = (float)nudValue.Value;
        }

    }

}
