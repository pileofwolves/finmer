/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Finmer.Core;
using JetBrains.Annotations;

namespace Finmer.Views.Base
{

    /// <summary>
    /// Base class for character creation section pages.
    /// </summary>
    public abstract class CharCreateViewBase : UserControl, INotifyPropertyChanged
    {

        private int m_TotalPages;

        /// <summary>
        /// Indicates whether the user may proceed to the next page in the character creation wizard.
        /// </summary>
        public abstract bool CanGoNext { get; }

        /// <summary>
        /// Returns the total number of steps (pages) in the character creation process.
        /// </summary>
        public int TotalPages
        {
            get => m_TotalPages;
            set
            {
                m_TotalPages = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Contains the initial save data block that will be used to generate a new game.
        /// </summary>
        public PropertyBag InitialSaveData { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
