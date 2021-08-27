/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows;
using Finmer.Gameplay;
using Finmer.Utility;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for GameOverPanelView.xaml
    /// </summary>
    public partial class GameOverPanelView
    {

        public GameOverPanelView()
        {
            InitializeComponent();
        }

        private void ReturnToMenu_OnClick(object sender, RoutedEventArgs e)
        {
            // Destroy the current game session so it can be GCed
            GameController.ExitSession();

            // Go to the main menu
            GameController.Window.Navigate(new WelcomePage(), ENavigatorAnimation.SlideRight);
        }

    }

}
