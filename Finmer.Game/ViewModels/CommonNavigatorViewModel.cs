/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;
using Finmer.Gameplay;
using Finmer.Utility;
using Finmer.Views;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model for common navigation commands.
    /// </summary>
    [SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded")]
    public class CommonNavigatorViewModel : BaseProp
    {

        /// <summary>
        /// Command for opening a FA link in an external browser.
        /// </summary>
        public ICommand VisitWebsiteCommand => m_CommandVisitWebsite ?? (m_CommandVisitWebsite = new RelayCommand(OnClickWebsite));

        /// <summary>
        /// Command for opening the GitHub repository in an external browser.
        /// </summary>
        public ICommand VisitGitHubCommand => m_CommandVisitGitHub ?? (m_CommandVisitGitHub = new RelayCommand(OnClickGH));

        /// <summary>
        /// Command for opening the donation page in an external browser.
        /// </summary>
        public ICommand DonateCommand => m_CommandDonate ?? (m_CommandDonate = new RelayCommand(OnClickDonate));

        /// <summary>
        /// Command for composing a new email.
        /// </summary>
        public ICommand EmailCommand => m_CommandEmail ?? (m_CommandEmail = new RelayCommand(OnClickEmail));

        /// <summary>
        /// Command for launching the game, after a GameSession has been initialized.
        /// </summary>
        public ICommand NavigateToGameCommand => m_CommandNavGame ?? (m_CommandNavGame = new RelayCommand(OnNavigateGame));

        /// <summary>
        /// Command for exiting the game.
        /// </summary>
        public ICommand ExitCommand => m_CommandExit ?? (m_CommandExit = new RelayCommand(OnExit));

        private ICommand m_CommandVisitWebsite;
        private ICommand m_CommandVisitGitHub;
        private ICommand m_CommandDonate;
        private ICommand m_CommandEmail;
        private ICommand m_CommandExit;
        private ICommand m_CommandNavGame;

        private static void OnClickWebsite(object args)
        {
            Process.Start(@"https://finmer.dev");
        }

        private static void OnClickGH(object args)
        {
            Process.Start(@"https://github.com/pileofwolves/finmer");
        }

        private static void OnClickDonate(object args)
        {
            Process.Start(@"https://ko-fi.com/nuntiswolf");
        }

        private static void OnClickEmail(object args)
        {
            // TODO: Replace
            Process.Start(@"mailto:nuntis@finmer.dev");
        }

        private static void OnNavigateGame(object args)
        {
            GameController.Window.Navigate(new MainPage(), ENavigatorAnimation.SlideLeft);
        }

        private static void OnExit(object args)
        {
            Application.Current.MainWindow?.Close();
        }

    }

}
