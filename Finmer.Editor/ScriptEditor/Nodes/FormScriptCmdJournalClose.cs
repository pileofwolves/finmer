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
    /// Node editor form for CommandJournalClose.
    /// </summary>
    public partial class FormScriptCmdJournalClose : FormScriptNode
    {

        public FormScriptCmdJournalClose()
        {
            InitializeComponent();
        }

        private void FormScriptCmdPlayerSetItem_Load(object sender, System.EventArgs e)
        {
            var node = (CommandJournalClose)Node;
            apcJournal.SelectedGuid = node.JournalGuid;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandJournalClose)Node;
            node.JournalGuid = apcJournal.SelectedGuid;
        }

    }

}
