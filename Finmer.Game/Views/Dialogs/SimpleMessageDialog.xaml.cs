/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for SimpleMessageDialog.xaml
    /// </summary>
    public partial class SimpleMessageDialog
    {

        private string m_Message = String.Empty;

        /// <summary>
        /// The message to display in the dialog.
        /// </summary>
        public string Message
        {
            get => m_Message;
            set
            {
                m_Message = value;
                OnPropertyChanged();
            }
        }

        public SimpleMessageDialog()
        {
            InitializeComponent();
        }

    }

}
