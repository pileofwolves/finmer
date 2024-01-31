/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.IO;
using System.Windows.Input;
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
        private ESaveSlot m_SelectedSlot;
        private ICommand m_PopupConfirmCommand;
        private ICommand m_SaveCommand;
        private ICommand m_CloseCommand;

        public SavePageViewModel()
        {
            // Cache headings for all save slots
            Save1Text = SaveManager.GetSlotInfo(ESaveSlot.Manual1).Description;
            Save2Text = SaveManager.GetSlotInfo(ESaveSlot.Manual2).Description;
            Save3Text = SaveManager.GetSlotInfo(ESaveSlot.Manual3).Description;
        }

        private void OnSaveCommand(object arg)
        {
            m_SelectedSlot = (ESaveSlot)arg;

            // Find the save slot associated with the index
            SaveManager.SlotInfo info = SaveManager.GetSlotInfo(m_SelectedSlot);

            // If no save data exists in this slot yet, there's nothing to overwrite, so just save
            if (!info.IsLoadable)
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
                SaveManager.WriteSnapshot(m_SelectedSlot, snapshot);

                // Erase the auto-save, to help prevent confusing UI: the manual save is now the most recent save data
                SaveManager.DeleteSlot(ESaveSlot.Checkpoint);

                GameUI.Instance.Log("Game saved!", Theme.LogColorLightGray);
            }
            catch (UnauthorizedAccessException)
            {
                // We cannot solve I/O errors without user intervention, so show the error to the user
                GameController.Window.OpenPopup(new SimpleMessageDialog
                {
                    Message = "Could not write save data, because access to the filesystem was denied. " +
                        "Please make sure the game is permitted to write files to the game directory, and then try again."
                });
            }
            catch (IOException ex)
            {
                // We cannot solve I/O errors without user intervention, so show the error to the user
                GameController.Window.OpenPopup(new SimpleMessageDialog
                {
                    Message = $"Could not write save data due to a filesystem error: {ex.GetType().Name}: {ex.Message}"
                });
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
