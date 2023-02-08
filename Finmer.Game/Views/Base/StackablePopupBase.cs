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

        private ICommand m_CloseCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Close the dialog.
        /// </summary>
        private void Close()
        {
            Host.Remove(this);
        }

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
