/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Utility;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model for the round pips display.
    /// </summary>
    public class CombatRoundPipsViewModel : BaseProp
    {

        private const string k_PipIconPath_Ally = "UI/RoundPipAlly.png";
        private const string k_PipIconPath_Enemy = "UI/RoundPipEnemy.png";

        private int m_NumLeftPips;
        private int m_NumRightPips;
        private bool m_IsLeftAlly;

        /// <summary>
        /// Specifies the number of empty round indicators to show.
        /// </summary>
        public int NumLeftPips
        {
            get => m_NumLeftPips;
            set
            {
                m_NumLeftPips = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Specifies the number of filled round indicators to show.
        /// </summary>
        public int NumRightPips
        {
            get => m_NumRightPips;
            set
            {
                m_NumRightPips = value;
                OnPropertyChanged();
            }
        }

        public bool IsLeftAlly
        {
            get => m_IsLeftAlly;
            set
            {
                m_IsLeftAlly = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LeftPipImagePath));
                OnPropertyChanged(nameof(RightPipImagePath));
            }
        }

        public Uri LeftPipImagePath => PackUriGenerator.GetGameResource(IsLeftAlly ? k_PipIconPath_Ally : k_PipIconPath_Enemy);

        public Uri RightPipImagePath => PackUriGenerator.GetGameResource(IsLeftAlly ? k_PipIconPath_Enemy : k_PipIconPath_Ally);

    }

}
