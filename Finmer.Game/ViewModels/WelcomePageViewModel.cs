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
using Finmer.Models;
using Finmer.Utility;
using Finmer.Views;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model for the first-time player welcome screen, and base for the title page view model.
    /// </summary>
    [SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded")]
    public class WelcomePageViewModel : BaseProp
    {

        public ICommand VisitFaCommand => m_CommandVisitFa ?? (m_CommandVisitFa = new RelayCommand(OnClickFA));

        public ICommand VisitGhCommand => m_CommandVisitGh ?? (m_CommandVisitGh = new RelayCommand(OnClickGH));

        public ICommand DonateCommand => m_CommandDonate ?? (m_CommandDonate = new RelayCommand(OnClickDonate));

        public ICommand EmailCommand => m_CommandEmail ?? (m_CommandEmail = new RelayCommand(OnClickEmail));

        public ICommand ExitCommand => m_CommandExit ?? (m_CommandExit = new RelayCommand(OnExit));

        public ICommand GoTitleCommand => m_CommandGoToTitle ?? (m_CommandGoToTitle = new RelayCommand(OnGoTitle));

        private ICommand m_CommandVisitFa;
        private ICommand m_CommandVisitGh;
        private ICommand m_CommandDonate;
        private ICommand m_CommandEmail;
        private ICommand m_CommandExit;
        private ICommand m_CommandGoToTitle;

        private static void OnClickFA(object args)
        {
            Process.Start(@"https://www.furaffinity.net/user/nuntis/");
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
            Process.Start(@"mailto:nuntiswolf@gmail.com");
        }

        private static void OnExit(object args)
        {
            Application.Current.MainWindow?.Close();
        }

        private static void OnGoTitle(object args)
        {
            // Don't show the welcome page again
            UserConfig.FirstStart = false;

            // Navigate to the title screen
            GameController.Window.Navigate(new TitlePage(), ENavigatorAnimation.SlideUp);
        }
    }

}
