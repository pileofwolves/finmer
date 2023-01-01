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
    /// Node editor form for CommandJournalUpdate.
    /// </summary>
    public partial class FormScriptCmdJournalUpdate : FormScriptNode
    {

        public FormScriptCmdJournalUpdate()
        {
            InitializeComponent();
        }

        private void FormScriptCmdPlayerSetItem_Load(object sender, System.EventArgs e)
        {
            var node = (CommandJournalUpdate)Node;
            apcJournal.SelectedGuid = node.JournalGuid;
            nudStage.Value = node.Stage;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandJournalUpdate)Node;
            node.JournalGuid = apcJournal.SelectedGuid;
            node.Stage = (int)nudStage.Value;
        }

    }

}
