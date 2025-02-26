﻿/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model for a journal item.
    /// </summary>
    internal sealed class JournalItemViewModel : BaseProp
    {

        private string m_JournalTitle = String.Empty;
        private string m_JournalEntry = String.Empty;

        /// <summary>
        /// Specifies the journal title.
        /// </summary>
        public string JournalTitle
        {
            get => m_JournalTitle;
            set
            {
                m_JournalTitle = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Specifies the text contents of the journal entry.
        /// </summary>
        public string JournalEntry
        {
            get => m_JournalEntry;
            set
            {
                m_JournalEntry = value;
                OnPropertyChanged();
            }
        }

    }

}
