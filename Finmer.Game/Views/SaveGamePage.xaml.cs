/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.ViewModels;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for SaveGamePage.xaml
    /// </summary>
    public partial class SaveGamePage
    {

        public SaveGamePage()
        {
            InitializeComponent();

            // Configure data context for UI binding
            DataContext = new SavePageViewModel();
        }

    }

}
