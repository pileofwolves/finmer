/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Finmer.Gameplay;
using Finmer.Models;
using Finmer.Utility;
using Finmer.Views;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model for the main game page.
    /// </summary>
    internal class MainPageViewModel : BaseProp
    {

        private ICommand m_CommandExit;
        private ICommand m_CommandConsole;
        private ICommand m_CommandCharSheet;
        private ICommand m_CommandJournal;

        /// <summary>
        /// Command for exiting the game session and returning to the title screen.
        /// </summary>
        public ICommand ExitToMenuCommand => m_CommandExit ?? (m_CommandExit = new RelayCommand(OnExitToMenu));

        /// <summary>
        /// Command for toggling the visibility of the debug script console.
        /// </summary>
        public ICommand ToggleScriptConsoleCommand => m_CommandConsole ?? (m_CommandConsole = new RelayCommand(OnToggleConsole, CanToggleConsole));

        /// <summary>
        /// Command for navigating to the character sheet page.
        /// </summary>
        public ICommand ViewCharacterSheetCommand => m_CommandCharSheet ?? (m_CommandCharSheet = new RelayCommand(OnViewCharSheet));

        /// <summary>
        /// Command for navigating to the journal page.
        /// </summary>
        public ICommand ViewJournalCommand => m_CommandJournal ?? (m_CommandJournal = new RelayCommand(OnViewJournal));

        /// <summary>
        /// Downstream viewmodel representing the player character.
        /// </summary>
        public Player Player => GameController.Session.Player;

        /// <summary>
        /// Downstream viewmodel representing the game UI state.
        /// </summary>
        public GameUI UI => GameUI.Instance;

        private void OnExitToMenu(object args)
        {
            // As a bit of a hack, use the CommandParameter to pass the popup to this function so we can close it
            Popup confirm_popup = (Popup)args;
            confirm_popup.IsOpen = false;

            // Destroy the gameplay session
            GameController.ExitSession();

            // Return to the title screen
            GameController.Window.Navigate(new TitlePage(), ENavigatorAnimation.SlideRight);
        }

        private void OnToggleConsole(object args)
        {
            GameUI.Instance.IsScriptConsoleOpened = !GameUI.Instance.IsScriptConsoleOpened;
        }

        private bool CanToggleConsole(object args)
        {
            return GameUI.Instance.IsScriptConsoleEnabled && GameUI.Instance.ControlsEnabled;
        }

        private void OnViewCharSheet(object args)
        {
            GameController.Window.OpenPopup(new CharSheetDialog());
        }

        private void OnViewJournal(object args)
        {
            GameController.Window.OpenPopup(new JournalDialog());
        }

    }

}
