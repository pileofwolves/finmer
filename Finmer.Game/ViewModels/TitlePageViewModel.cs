/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows.Input;
using Finmer.Core;
using Finmer.Gameplay;
using Finmer.Models;
using Finmer.Utility;
using Finmer.Views;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model for the title screen page.
    /// </summary>
    public sealed class TitlePageViewModel : WelcomePageViewModel
    {

        /// <summary>
        /// Command for navigating to the game setup page.
        /// </summary>
        public ICommand NewGameCommand => m_CommandNewGame ?? (m_CommandNewGame = new RelayCommand(OnNewGame));

        /// <summary>
        /// Command for loading stored save data.
        /// </summary>
        public ICommand LoadGameCommand => m_CommandLoadGame ?? (m_CommandLoadGame = new RelayCommand(OnLoadGame, CanLoadGame));

        /// <summary>
        /// Command for navigating to the options page.
        /// </summary>
        public ICommand OptionsCommand => m_CommandOptions ?? (m_CommandOptions = new RelayCommand(OnOptions));

        /// <summary>
        /// Command for navigating to the game page using cached save data.
        /// </summary>
        public ICommand QuickStartCommand => m_CommandQuickStart ?? (m_CommandQuickStart = new RelayCommand(OnQuickStart, CanQuickStart));

        /// <summary>
        /// Returns the version number of this build.
        /// </summary>
        public string VersionNumber => CompileConstants.k_VersionString;

        /// <summary>
        /// Save data description for save slot 1.
        /// </summary>
        public string Save1Text { get; }

        /// <summary>
        /// Save data description for save slot 2.
        /// </summary>
        public string Save2Text { get; }

        /// <summary>
        /// Save data description for save slot 3.
        /// </summary>
        public string Save3Text { get; }

        private ICommand m_CommandNewGame;
        private ICommand m_CommandLoadGame;
        private ICommand m_CommandOptions;
        private ICommand m_CommandQuickStart;

        public TitlePageViewModel()
        {
            // Cache descriptions for all save data slots
            Save1Text = SaveManager.GetSaveInfo(0).Info;
            Save2Text = SaveManager.GetSaveInfo(1).Info;
            Save3Text = SaveManager.GetSaveInfo(2).Info;
        }

        private static void OnNewGame(object args)
        {
            // Navigate to the options page
            GameController.Window.Navigate(new NewGamePage(), ENavigatorAnimation.SlideLeft);
        }

        private static void OnQuickStart(object args)
        {
            // Start a new game using the last configured character creator preset
            GameSnapshot save_data = new GameSnapshot(UserConfig.NewGamePreset, new PropertyBag(), new PropertyBag());
            GameController.BeginNewSession(save_data);
            GameController.Window.Navigate(new MainPage(), ENavigatorAnimation.SlideLeft);
        }

        private static bool CanQuickStart(object args)
        {
            return UserConfig.NewGamePreset != null;
        }

        private static void OnLoadGame(object args)
        {
            int slot_index = (int)args;
            GameSnapshot snapshot;

            try
            {
                // Read the stored save data
                snapshot = SaveManager.LoadSaveFile(slot_index);
            }
            catch
            {
                // Handle I/O or data corruption errors by ignoring them
                // TODO: Once the popup system is in place, use that to notify the user of the error
                return;
            }

            // Launch the session using the save data
            GameController.BeginNewSession(snapshot);
            GameController.Window.Navigate(new MainPage(), ENavigatorAnimation.SlideLeft);
        }

        private static bool CanLoadGame(object args)
        {
            // Game can be loaded if the slot contains valid save data
            int slot_index = (int)args;
            return SaveManager.GetSaveInfo(slot_index).IsLoadable;
        }

        private static void OnOptions(object args)
        {
            // Navigate to the options page
            GameController.Window.Navigate(new OptionsPage(), ENavigatorAnimation.SlideRight);
        }

    }

}
