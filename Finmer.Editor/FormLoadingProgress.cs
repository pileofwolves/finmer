/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows.Forms;

namespace Finmer.Editor
{

    public partial class FormLoadingProgress : Form
    {

        public FormLoadingProgress()
        {
            InitializeComponent();
        }

        private void FormLoadingProgress_Load(object sender, System.EventArgs e)
        {
            // Move dialog to owner center - must be done manually because FormStartPosition.CenterParent does not work with non-modal forms
            CenterToParent();
        }

        private void FormLoadingProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ignore requests from the user to close the form; this is handled programmatically
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
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
