/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows.Forms;

namespace Finmer.Editor
{

    /// <summary>
    /// Window that enables editing of the editor's user preferences.
    /// </summary>
    public partial class FormPreferences : Form
    {

        public FormPreferences()
        {
            InitializeComponent();

            txtExePath.Text = EditorPreferences.ExecutablePath;
            txtExeWorkDir.Text = EditorPreferences.ExecutableWorkingDirectory;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            // Update the global preferences store
            EditorPreferences.ExecutablePath = txtExePath.Text;
            EditorPreferences.ExecutableWorkingDirectory = txtExeWorkDir.Text;
            EditorPreferences.Save();

            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdBrowseExePath_Click(object sender, EventArgs e)
        {
            if (dlgBrowseExe.ShowDialog() == DialogResult.OK)
                txtExePath.Text = dlgBrowseExe.FileName;
        }

        private void cmdBrowseExeWorkDir_Click(object sender, EventArgs e)
        {
            // Preselect the current value, so the user doesn't have to navigate their entire PC to make small changes
            dlgBrowseWorkDir.SelectedPath = txtExeWorkDir.Text;

            if (dlgBrowseWorkDir.ShowDialog() == DialogResult.OK)
                txtExeWorkDir.Text = dlgBrowseWorkDir.SelectedPath;
        }

    }

}
