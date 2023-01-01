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
    /// Node editor form for CommandPlayerSetStat.
    /// </summary>
    public partial class FormScriptCmdPlayerSetStat : FormScriptNode
    {

        public FormScriptCmdPlayerSetStat()
        {
            InitializeComponent();
        }

        private void FormScriptCmdSetLocation_Load(object sender, System.EventArgs e)
        {
            var node = (CommandPlayerSetStat)Node;
            optModeAdd.Checked = node.StatOperation == CommandPlayerSetStat.EOperation.Add;
            optModeSet.Checked = node.StatOperation == CommandPlayerSetStat.EOperation.Set;
            cmbStat.SelectedIndex = (int)node.Stat;
            sveValue.SetValue(node.Value);
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandPlayerSetStat)Node;
            node.StatOperation = optModeAdd.Checked ? CommandPlayerSetStat.EOperation.Add : CommandPlayerSetStat.EOperation.Set;
            node.Stat = (CommandPlayerSetStat.EStat)cmbStat.SelectedIndex;
            node.Value = sveValue.GetValue();
        }

    }

}
