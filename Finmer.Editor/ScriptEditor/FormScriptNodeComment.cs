/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Node editor form for a raw Lua code snippet.
    /// </summary>
    public partial class FormScriptNodeComment : FormScriptNode
    {

        public FormScriptNodeComment()
        {
            InitializeComponent();
        }

        private void FormScriptNodeComment_Load(object sender, System.EventArgs e)
        {
            txtComment.Text = ((CommandComment)Node).Comment;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            ((CommandComment)Node).Comment = txtComment.Text;
        }

    }

}
