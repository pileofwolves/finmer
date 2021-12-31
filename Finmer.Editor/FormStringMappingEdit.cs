/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows.Forms;
using Finmer.Core;

namespace Finmer.Editor
{

    public partial class FormStringMappingEdit : Form
    {

        /// <summary>
        /// Gets the StringMapping that is being edited in this dialog.
        /// </summary>
        public StringMapping Mapping { get; private set; }

        public FormStringMappingEdit(StringMapping mapping)
        {
            InitializeComponent();

            // Cache the input mapping object
            Mapping = mapping;

            // Populate UI
            txtOldKey.Text = mapping.Key;
            txtNewKey.Text = mapping.NewKey;
            cmbRule.SelectedIndex = (int)mapping.Rule;
        }

        private void cmdAccept_Click(object sender, System.EventArgs e)
        {
            // Save the mapping data, which may have been edited
            Mapping = new StringMapping
            {
                Key = txtOldKey.Text,
                NewKey = txtNewKey.Text,
                Rule = (StringMapping.ERule)cmbRule.SelectedIndex
            };

            // Close the dialog
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdCancel_Click(object sender, System.EventArgs e)
        {
            // Close the dialog
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }

}
