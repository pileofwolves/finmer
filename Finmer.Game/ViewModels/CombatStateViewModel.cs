/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.ObjectModel;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model for the overall combat system.
    /// </summary>
    public sealed class CombatStateViewModel : BaseProp
    {

        private CombatResolveViewModel m_CombatResolveViewModel;

        public ObservableCollection<CombatParticipantViewModel> Participants { get; } = new ObservableCollection<CombatParticipantViewModel>();

        public CombatResolveViewModel CombatResolveViewModel
        {
            get => m_CombatResolveViewModel;
            set
            {
                m_CombatResolveViewModel = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsResolveVisible));
            }
        }

        public bool IsResolveVisible => CombatResolveViewModel != null;

    }

}
