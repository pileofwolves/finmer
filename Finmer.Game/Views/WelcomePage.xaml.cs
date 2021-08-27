/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Finmer.Gameplay;
using Finmer.Utility;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage
    {

        private ICommand m_VisitMeCmd;
        private ICommand m_LoadSlotCmd;

        public WelcomePage()
        {
            DataContext = this;
            InitializeComponent();

            VersionLabel.Text = CompileConstants.k_VersionString;
        }

        public SaveManager.SaveSlot Save1 => SaveManager.GetSaveInfo(0);
        public SaveManager.SaveSlot Save2 => SaveManager.GetSaveInfo(1);
        public SaveManager.SaveSlot Save3 => SaveManager.GetSaveInfo(2);

        public ICommand VisitMeCommand => m_VisitMeCmd ?? (m_VisitMeCmd = new RelayCommand(Hyperlink_OnRequestNavigate));
        public ICommand LoadSlotCommand => m_LoadSlotCmd ?? (m_LoadSlotCmd = new RelayCommand(LoadSlotButton_OnClick));

        private void Hyperlink_OnRequestNavigate(object sender)
        {
            Process.Start("https://www.furaffinity.net/user/nuntis");
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow?.Close();
        }

        private void OptionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            GameController.Window.Navigate(new OptionsPage(), ENavigatorAnimation.SlideLeft);
        }

        private void NewGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            GameController.Window.Navigate(new NewGamePage(), ENavigatorAnimation.SlideLeft);
        }

        private void LoadSlotButton_OnClick(object param)
        {
            DoLoadSlot(int.Parse((string)param));
        }

        private void DoLoadSlot(int slot)
        {
            var player = SaveManager.LoadSaveFile(slot);
            GameController.BeginNewSession(player);
            GameController.Window.Navigate(new MainPage(), ENavigatorAnimation.SlideLeft);
        }

    }

}
