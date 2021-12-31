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
    /// Interaction logic for TitlePage.xaml
    /// </summary>
    public partial class TitlePage
    {

        public TitlePage()
        {
            InitializeComponent();

            // Set up data context
            DataContext = new TitlePageViewModel();
        }

    }

}
