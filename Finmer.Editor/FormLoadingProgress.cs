/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows.Forms;

namespace Finmer.Editor
{

    public partial class FormLoadingProgress : Form
    {

        private bool m_AllowClose  ;

        public FormLoadingProgress()
        {
            InitializeComponent();
        }

        private void frmLoadingProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_AllowClose) e.Cancel = true;
        }

        public void RequestClose()
        {
            m_AllowClose = true;
            Close();
        }

        public void SetLabel(string label)
        {
            lblCaption.Text = label;
            Application.DoEvents();
        }

        public void SetupProgress(int max)
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = max;
            progressBar.Value = 0;
        }

        public void AddProgress()
        {
            progressBar.Value++;
        }

    }

}
