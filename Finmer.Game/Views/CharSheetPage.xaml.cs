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
    /// Interaction logic for CharSheetPage.xaml
    /// </summary>
    public partial class CharSheetPage
    {

        public CharSheetPage()
        {
            InitializeComponent();

            // Populate the main view
            var player = GameController.Session.Player;
            DataContext = new CharacterSheetViewModel(player);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            GameController.Window.Navigate(new MainPage(), ENavigatorAnimation.SlideRight);
        }

    }

}
