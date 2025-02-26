/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows.Input;
using Finmer.Gameplay;
using Finmer.Utility;
using Finmer.Views;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model for the GameOverPanelView.
    /// </summary>
    public sealed class GameOverPanelViewModel : BaseProp
    {

        /// <summary>
        /// Command for reloading the last stored checkpoint (auto-save).
        /// </summary>
        public ICommand ReloadCheckpointCommand => m_CommandReload ?? (m_CommandReload = new RelayCommand(OnReloadCheckpoint, CanReloadCheckpoint));

        /// <summary>
        /// Command for closing the game and returning to the main menu.
        /// </summary>
        public ICommand NavigateToMenuCommand => m_CommandExit ?? (m_CommandExit = new RelayCommand(OnReturnToMenu));

        private ICommand m_CommandReload;
        private ICommand m_CommandExit;

        private void OnReloadCheckpoint(object args)
        {
            // Load the checkpoint from disk
            GameSnapshot checkpoint;
            try
            {
                checkpoint = SaveManager.ReadSnapshot(ESaveSlot.Checkpoint);
            }
            catch (Exception ex)
            {
                GameController.Window.OpenPopup(new SimpleMessageDialog
                {
                    Message = $"Could not load checkpoint save data: {ex.Message} ({ex.GetType().Name})"
                });
                return;
            }

            // Navigate to a new copy of the main page, to ensure a clean visual break and to easily reset all UI
            NavigationUtilities.BeginSessionAndNavigate(checkpoint);
        }

        private bool CanReloadCheckpoint(object args)
        {
            return SaveManager.GetSlotInfo(ESaveSlot.Checkpoint).IsLoadable;
        }

        private void OnReturnToMenu(object args)
        {
            // Destroy the current game session so it can be GCed
            GameController.ExitSession();

            // Go to the main menu
            GameController.Window.Navigate(new TitlePage(), ENavigatorAnimation.SlideRight);
        }

    }

}
