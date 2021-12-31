/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Finmer.Editor
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded", Justification = ":)")]
    public partial class FormAbout : Form
    {

        public FormAbout()
        {
            InitializeComponent();
            lblVersion.Text = $"Version {CompileConstants.k_VersionString}";
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lklFA_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"https://www.furaffinity.net/user/nuntis");
        }

        private void lklGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"https://github.com/pileofwolves/finmer");
        }

    }

}
