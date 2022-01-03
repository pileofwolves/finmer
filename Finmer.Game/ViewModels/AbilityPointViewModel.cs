/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows.Input;
using Finmer.Gameplay;
using Finmer.Utility;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model that represents one ability score row in the character creation screen.
    /// </summary>
    internal sealed class AbilityPointViewModel : BaseProp
    {

        private RelayCommand m_CmdInc, m_CmdDec;
        private int m_AbilityValue;

        /// <summary>
        /// Short name of the ability score.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Brief description of the ability score.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The current value of the ability score, as assigned by the user.
        /// </summary>
        public int Value
        {
            get => m_AbilityValue;
            private set
            {
                m_AbilityValue = Math.Max(value, Character.k_AbilityScoreMinimum);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Button command for incrementing the score value.
        /// </summary>
        public ICommand IncrementCommand => m_CmdInc ?? (m_CmdInc = new RelayCommand(Increment));

        /// <summary>
        /// Button command for decrementing the score value.
        /// </summary>
        public ICommand DecrementCommand => m_CmdDec ?? (m_CmdDec = new RelayCommand(Decrement, CanDecrement));

        public void Increment(object args = null)
        {
            Value++;
        }

        public void Decrement(object args = null)
        {
            Value--;
        }

        public void Reset(int value = Character.k_AbilityScoreMinimum)
        {
            Value = value;
        }

        private bool CanDecrement(object args)
        {
            return Value > Character.k_AbilityScoreMinimum;
        }

    }

}
