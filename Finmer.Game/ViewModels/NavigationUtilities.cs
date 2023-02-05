/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Gameplay;
using Finmer.Models;
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
            // Number of times the player must start a game session to get the invitation dialog.
            // This is based on one initial load (for a new game), and then two reloads (such as due to game-overs).
            const int k_CommunityInviteThreshold = 3;

            // If the user has been playing for some time, show a community info dialog first instead of navigating directly to the game
            if (UserConfig.SessionLoadCount >= k_CommunityInviteThreshold && !UserConfig.CommunityInviteShown)
            {
                // Show info dialog
                UserConfig.CommunityInviteShown = true;
                GameController.Window.Navigate(new CommunityInvitePage(), ENavigatorAnimation.SlideLeft);
            }
            else
            {
                // Show game
                GameController.Window.Navigate(new MainPage(), ENavigatorAnimation.SlideLeft);
            }
        }

    }

}
