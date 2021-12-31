/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Gameplay.Combat;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model for the combat resolve screen.
    /// </summary>
    public sealed class CombatResolveViewModel : BaseProp
    {

        public enum EPanelState
        {
            FadeIn,
            FadeOut
        }

        private EPanelState m_State = EPanelState.FadeIn;
        private CombatResolveParticipantViewModel m_Participant1ViewModel;
        private CombatResolveParticipantViewModel m_Participant2ViewModel;
        private CombatRoundPipsViewModel m_PipsViewModel = new CombatRoundPipsViewModel();

        public CombatResolveParticipantViewModel Participant1ViewModel
        {
            get => m_Participant1ViewModel;
            private set
            {
                m_Participant1ViewModel = value;
                OnPropertyChanged();
            }
        }

        public CombatResolveParticipantViewModel Participant2ViewModel
        {
            get => m_Participant2ViewModel;
            private set
            {
                m_Participant2ViewModel = value;
                OnPropertyChanged();
            }
        }

        public CombatRoundPipsViewModel PipsViewModel
        {
            get => m_PipsViewModel;
            set
            {
                m_PipsViewModel = value;
                OnPropertyChanged();
            }
        }

        public EPanelState AnimationState
        {
            get => m_State;
            set
            {
                m_State = value;
                OnPropertyChanged();
            }
        }

        public void SetRound(CombatDisplay.ResolveInfo info, int roundIndex)
        {
            CombatDisplay.ResolveRound round = info.Rounds[roundIndex];

            // Update the participant viewmodels to show the new sets of dice
            Participant1ViewModel = new CombatResolveParticipantViewModel(info.Instigator, info.Action, round.OffenseTotal, round.OffenseDice);
            Participant2ViewModel = new CombatResolveParticipantViewModel(info.Target, info.Action, round.DefenseTotal, round.DefenseDice);

            // Update the display of round victory pips
            m_PipsViewModel.NumLeftPips = round.OffenseRoundsWon;
            m_PipsViewModel.NumRightPips = round.DefenseRoundsWon;
            m_PipsViewModel.IsLeftAlly = info.Instigator.Character.IsAlly;
        }

    }

}
