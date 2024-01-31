/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Gameplay;
using Finmer.Gameplay.Scripting;
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
        /// Initialize a new session with save data, and navigate the main window to the game page. On error, show a dialog box.
        /// </summary>
        public static void BeginSessionAndNavigate(GameSnapshot save_data)
        {
            try
            {
                // Attempt to initiate a new game with save data
                GameController.BeginNewSession(save_data);

                // Present the game screen
                NavigateToGameInternal();
            }
            catch (InvalidSaveDataException ex)
            {
                HandleGameStartupError("Save data is corrupted: " + ex.Message);
            }
            catch (UnsolvableConstraintException ex)
            {
                HandleGameStartupError("There is a configuration error in loaded modules: " + ex.Message);
            }
            catch (ScriptException ex)
            {
                HandleGameStartupError("Could not initialize game: " + ex.Message);
            }
        }

        /// <summary>
        /// Navigate the main window to the game page.
        /// </summary>
        private static void NavigateToGameInternal()
        {
            // Number of times the player must start a game session to get the invitation dialog.
            // This is based on one initial load (for a new game), and then two reloads (such as due to game-overs).
            const int k_CommunityInviteThreshold = 3;

            // Increment play counter
            UserConfig.SessionLoadCount++;

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

        /// <summary>
        /// Present an error dialog.
        /// </summary>
        private static void HandleGameStartupError(string message)
        {
            // Present a popup dialog with the error message
            GameController.Window.OpenPopup(new SimpleMessageDialog
            {
                Message = message
            });

            // Navigate to the title screen
            GameController.Window.Navigate(new TitlePage(), ENavigatorAnimation.Instant);
        }

    }

}
