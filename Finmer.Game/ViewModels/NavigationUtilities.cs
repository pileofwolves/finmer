/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Gameplay;
using Finmer.Utility;
using Finmer.Views;

namespace Finmer.ViewModels
{

    /// <summary>
    /// Provides utility functions for common main window navigation tasks.
    /// </summary>
    public static class NavigationUtilities
    {

        /// <summary>
        /// Navigate the main window to the game page.
        /// </summary>
        public static void NavigateToGame()
        {
            GameController.Window.Navigate(new MainPage(), ENavigatorAnimation.SlideLeft);
        }

    }

}
