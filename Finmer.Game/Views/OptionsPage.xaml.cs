/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows;
using Finmer.Gameplay;
using Finmer.Models;
using Finmer.Utility;
using Finmer.ViewModels;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for OptionsPage.xaml
    /// </summary>
    public partial class OptionsPage
    {

        public OptionsPage()
        {
            InitializeComponent();
            DataContext = UserConfigViewModel.Instance;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserConfig.Save();
            GameController.Window.Navigate(new TitlePage(), ENavigatorAnimation.SlideLeft);
        }

    }

}
