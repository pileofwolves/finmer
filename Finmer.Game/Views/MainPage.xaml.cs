/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage
    {

        public MainPage()
        {
            InitializeComponent();

            // Set up the view model
            DataContext = new MainPageViewModel();

            // hook up the log viewers to the correct sources
            // TODO: MVVM-ify this
            LogPrimary.LogSource = GameUI.Instance.Messages;
        }

        private void SheetButton_Click(object sender, RoutedEventArgs e)
        {
            GameController.Window.Navigate(new CharSheetPage(), ENavigatorAnimation.SlideLeft);
        }

        private void ConsoleButton_Click(object sender, RoutedEventArgs e)
        {
            GameUI.Instance.IsScriptConsoleOpened = !GameUI.Instance.IsScriptConsoleOpened;
        }

    }

}
