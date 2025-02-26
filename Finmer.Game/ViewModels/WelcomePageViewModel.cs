/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows.Input;
using Finmer.Gameplay;
using Finmer.Models;
using Finmer.Utility;
using Finmer.Views;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model for the first-time player welcome screen
    /// </summary>
    public sealed class WelcomePageViewModel : CommonNavigatorViewModel
    {

        private ICommand m_CommandNavTitle;

        public ICommand NavigateToTitleCommand => m_CommandNavTitle ?? (m_CommandNavTitle = new RelayCommand(OnNavigateTitle));

        private static void OnNavigateTitle(object args)
        {
            // Don't show the welcome page again
            UserConfig.FirstStart = false;

            // Navigate to the title screen
            GameController.Window.Navigate(new TitlePage(), ENavigatorAnimation.SlideUp);
        }

    }

}
