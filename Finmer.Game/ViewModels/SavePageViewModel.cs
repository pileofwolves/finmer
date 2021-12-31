/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.IO;
using System.Windows.Input;
using Finmer.Core;
using Finmer.Gameplay;
using Finmer.Models;
using Finmer.Utility;
using Finmer.Views;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model for the save game creation page.
    /// </summary>
    public sealed class SavePageViewModel : BaseProp
    {

        /// <summary>
        /// Command for triggering an unconfirmed save.
        /// </summary>
        public ICommand SaveCommand => m_SaveCommand ?? (m_SaveCommand = new RelayCommand(OnSaveCommand));

        /// <summary>
        /// Command for triggering a confirmed save.
        /// </summary>
        public ICommand PopupConfirmCommand => m_PopupConfirmCommand ?? (m_PopupConfirmCommand = new RelayCommand(OnSaveConfirm));

        /// <summary>
        /// Command for closing the dialog.
        /// </summary>
        public ICommand CloseCommand => m_CloseCommand ?? (m_CloseCommand = new RelayCommand(OnCloseDialog));

        /// <summary>
        /// Gets or sets whether the overwrite confirmation popup should be shown.
        /// </summary>
        public bool IsConfirmPopupOpen
        {
            get => m_IsConfirmPopupOpen;
            set
            {
                m_IsConfirmPopupOpen = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Description of save slot 1.
        /// </summary>
        public string Save1Text { get; }

        /// <summary>
        /// Description of save slot 2.
        /// </summary>
        public string Save2Text { get; }

        /// <summary>
        /// Description of save slot 3.
        /// </summary>
        public string Save3Text { get; }

        private bool m_IsConfirmPopupOpen;
        private int m_SelectedSlot;
        private ICommand m_PopupConfirmCommand;
        private ICommand m_SaveCommand;
        private ICommand m_CloseCommand;

        public SavePageViewModel()
        {
            // Cache headings for all save slots
            var slot = SaveManager.GetSaveInfo(0);
            Save1Text = slot.IsLoadable ? slot.Info : "Empty";

            slot = SaveManager.GetSaveInfo(1);
            Save2Text = slot.IsLoadable ? slot.Info : "Empty";

            slot = SaveManager.GetSaveInfo(2);
            Save3Text = slot.IsLoadable ? slot.Info : "Empty";
        }

        private void OnSaveCommand(object arg)
        {
            m_SelectedSlot = (int)arg;

            // Find the save slot associated with the index
            SaveManager.SaveSlot slot = SaveManager.GetSaveInfo(m_SelectedSlot);

            // If no save data exists in this slot yet, there's nothing to overwrite, so just save
            if (!slot.IsLoadable)
            {
                OnSaveConfirm(arg);
                return;
            }

            // Otherwise, we should display an overwrite confirmation prompt
            IsConfirmPopupOpen = true;
        }

        private void OnSaveConfirm(object arg)
        {
            // Close the popup
            IsConfirmPopupOpen = false;

            // Get a snapshot of the player
            var snapshot = GameController.Session.CaptureSnapshot();
            try
            {
                // Save it to disk
                SaveManager.Save(m_SelectedSlot, snapshot);
                GameUI.Instance.Log("Game saved!", Theme.LogColorLightGray);
            }
            catch (UnauthorizedAccessException)
            {
                // If an error occurs, report it to the user and ignore since there's nothing we can do about it at this point
                GameUI.Instance.Log("Could not save game data: Access was denied. Make sure the game can write files to the game folder.", Theme.LogColorError);
            }
            catch (IOException ex)
            {
                // If an error occurs, report it to the user and ignore since there's nothing we can do about it at this point
                GameUI.Instance.Log("Could not save game data: " + ex.Message, Theme.LogColorError);
            }

            // Close this dialog
            OnCloseDialog(arg);
        }

        private void OnCloseDialog(object arg)
        {
            GameController.Window.Navigate(new MainPage(), ENavigatorAnimation.SlideRight);
            GameController.Session.ResumeScript();
        }

    }

}
