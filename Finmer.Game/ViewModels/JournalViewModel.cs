﻿/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using System.Linq;
using Finmer.Core.Assets;
using Finmer.Gameplay;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model that wraps a Journal.
    /// </summary>
    internal sealed class JournalViewModel : BaseProp
    {

        private readonly Journal m_Journal;

        public JournalViewModel(Journal journal)
        {
            m_Journal = journal;
        }

        /// <summary>
        /// Enumerates the journal as a collection of JournalItemViewModels.
        /// </summary>
        public IEnumerable<JournalItemViewModel> Entries
        {
            get
            {
                foreach (AssetJournal quest in m_Journal.GetOpenQuests())
                {
                    int stage = m_Journal.GetQuestStage(quest);
                    string text_stage = quest.GetEntryForStage(stage);

                    yield return new JournalItemViewModel
                    {
                        JournalTitle = quest.Title,
                        JournalEntry = text_stage
                    };
                }
            }
        }

        /// <summary>
        /// Indicates whether the journal contains no entries.
        /// </summary>
        public bool IsEmpty => !m_Journal.GetOpenQuests().Any();

    }

}
