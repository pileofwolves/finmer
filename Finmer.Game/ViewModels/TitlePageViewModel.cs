/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows.Input;
using Finmer.Gameplay;
using Finmer.Models;
using Finmer.Utility;
using Finmer.Views;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model for the title screen page.
    /// </summary>
    public sealed class TitlePageViewModel : CommonNavigatorViewModel
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
        /// Save data description for the checkpoint save slot.
        /// </summary>
        public string SaveCheckpointText { get; }

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
            var checkpoint = SaveManager.GetSlotInfo(ESaveSlot.Checkpoint);
            SaveCheckpointText = checkpoint.IsLoadable ? checkpoint.Description : "Last Checkpoint";
            Save1Text = SaveManager.GetSlotInfo(ESaveSlot.Manual1).Description;
            Save2Text = SaveManager.GetSlotInfo(ESaveSlot.Manual2).Description;
            Save3Text = SaveManager.GetSlotInfo(ESaveSlot.Manual3).Description;
        }

        private static void OnNewGame(object args)
        {
            // Navigate to the options page
            GameController.Window.Navigate(new NewGamePage(), ENavigatorAnimation.SlideLeft);
        }

        private static void OnQuickStart(object args)
        {
            // Backwards compatibility: verify that the preset includes a scene ID, which was not present at launch
            var scene_id_bytes = UserConfig.NewGamePreset.GetBytes(SaveData.k_System_StartSceneID);
            if (scene_id_bytes == null)
            {
                // Present a popup dialog with the error message
                GameController.Window.OpenPopup(new SimpleMessageDialog {
                    Message = "Preset save data is outdated (missing initial scene ID). Please go through the character creation screen once to update it."
                });
                return;
            }

            // Verify that the quick start scene exists; it may have been unloaded
            var scene_id = new Guid(scene_id_bytes);
            if (GameController.Content.GetAssetByID(scene_id) == null)
            {
                // Present a popup dialog with the error message
                GameController.Window.OpenPopup(new SimpleMessageDialog {
                    Message = $"Preset save data has unknown start scene ID {scene_id} - it may have been in an uninstalled module. Please go through the character creation screen once to update it."
                });
                return;
            }

            // Start a new game using the last configured character creator preset
            GameSnapshot save_data = GameSnapshot.FromInitialSaveData(UserConfig.NewGamePreset);
            NavigationUtilities.BeginSessionAndNavigate(save_data);
        }

        private static bool CanQuickStart(object args)
        {
            return UserConfig.NewGamePreset != null;
        }

        private static void OnLoadGame(object args)
        {
            ESaveSlot slot = (ESaveSlot)args;
            GameSnapshot snapshot;

            try
            {
                // Read the stored save data
                snapshot = SaveManager.ReadSnapshot(slot);
            }
            catch (Exception ex)
            {
                // Handle I/O or data corruption errors by aborting the game load, and displaying the error
                GameController.Window.OpenPopup(new SimpleMessageDialog
                {
                    Message = $"Save data could not be loaded: {ex.GetType().Name}: {ex.Message}"
                });
                return;
            }

            // Launch the session using the save data
            NavigationUtilities.BeginSessionAndNavigate(snapshot);
        }

        private static bool CanLoadGame(object args)
        {
            // Game can be loaded if the slot contains valid save data
            ESaveSlot slot = (ESaveSlot)args;
            return SaveManager.GetSlotInfo(slot).IsLoadable;
        }

        private static void OnOptions(object args)
        {
            // Navigate to the options page
            GameController.Window.Navigate(new OptionsPage(), ENavigatorAnimation.SlideRight);
        }

    }

}
