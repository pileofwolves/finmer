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
    public partial class FormScriptCondLuaSnippet : FormScriptNode
    {

        public FormScriptCondLuaSnippet()
        {
            InitializeComponent();
        }

        private void FormScriptNodeLuaSnippet_Load(object sender, System.EventArgs e)
        {
            ScintillaHelper.Setup(scintilla, ScintillaHelper.EScintillaStyle.Lua);
            scintilla.Text = ((ConditionInlineSnippet)Node).Snippet;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            ((ConditionInlineSnippet)Node).Snippet = scintilla.Text;
        }

    }

}
