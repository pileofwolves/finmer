/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using Finmer.Gameplay;
using Finmer.Utility;
using JetBrains.Annotations;

namespace Finmer.Views.Base
{

    /// <summary>
    /// Base class for dialogs that can be displayed in a PopupStackView.
    /// </summary>
    public abstract class StackablePopupBase : UserControl, INotifyPropertyChanged
    {

        /// <summary>
        /// The stack containing this popup.
        /// </summary>
        public PopupStackView Host { get; set; }

        /// <summary>
        /// Wrapped command object for closing the dialog.
        /// </summary>
        public ICommand CloseCommand => m_CloseCommand ?? (m_CloseCommand = new RelayCommand(_ => Close()));

        /// <summary>
        /// Wrapped command object for closing the dialog, and resuming gameplay scripts.
        /// </summary>
        public ICommand CloseResumeCommand => m_CloseResumeCommand ?? (m_CloseResumeCommand = new RelayCommand(_ => CloseAndResume()));

        private ICommand m_CloseCommand, m_CloseResumeCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        protected StackablePopupBase()
        {
            Loaded += StackablePopupBase_Loaded;
        }

        private void StackablePopupBase_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // First remove logical focus and keyboard focus from the main window
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope(GameController.Window), null);
            Keyboard.ClearFocus();

            // Reapply focus to the newly opened popup
            Keyboard.Focus(this);
            Focus();
        }

        /// <summary>
        /// Close the dialog.
        /// </summary>
        private void Close()
        {
            Host.Remove(this);
        }

        /// <summary>
        /// Close the dialog and resume gameplay scripts.
        /// </summary>
        private void CloseAndResume()
        {
            Close();
            GameController.Session.ResumeScript();
        }

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
