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
    /// Node editor form for CommandPlayerSetSpecies.
    /// </summary>
    public partial class FormScriptCmdPlayerSetSpecies : FormScriptNode
    {

        public FormScriptCmdPlayerSetSpecies()
        {
            InitializeComponent();
        }

        private void FormScriptCmdSetPlayerSpecies_Load(object sender, System.EventArgs e)
        {
            var node = (CommandPlayerSetSpecies)Node;
            txtSingular.Text = node.Singular;
            txtPlural.Text = node.Plural;
            txtCoatNoun.Text = node.CoatNoun;
            txtCoatAdjective.Text = node.CoatAdjective;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            var node = (CommandPlayerSetSpecies)Node;
            node.Singular = txtSingular.Text;
            node.Plural = txtPlural.Text;
            node.CoatNoun = txtCoatNoun.Text;
            node.CoatAdjective = txtCoatAdjective.Text;
        }

    }

}
