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
    /// Node editor form for selecting a player primary stat.
    /// </summary>
    public partial class FormScriptValuePlayerStat : FormScriptNode
    {

        public FormScriptValuePlayerStat()
        {
            InitializeComponent();
        }

        private void FormScriptValuePlayerStat_Load(object sender, System.EventArgs e)
        {
            cmbStat.SelectedIndex = (int)((ConditionPlayerStat)Node).Stat;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            ((ConditionPlayerStat)Node).Stat = (ConditionPlayerStat.EStat)cmbStat.SelectedIndex;
        }

    }

}
