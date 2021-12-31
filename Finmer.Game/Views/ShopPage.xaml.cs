/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows;
using Finmer.Gameplay;
using Finmer.Utility;
using Finmer.ViewModels;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for ShopPage.xaml
    /// </summary>
    public partial class ShopPage
    {

        public ShopPage(ShopState state)
        {
            InitializeComponent();

            // Configure view model for data binding
            DataContext = new ShopViewModel(state, GameController.Session.Player);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            GameController.Window.Navigate(new MainPage(), ENavigatorAnimation.SlideRight);
            GameController.Session.ResumeScript();
        }

    }

}
