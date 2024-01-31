/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Models;

namespace Finmer.ViewModels
{

    /// <summary>
    /// Represents an MVVM wrapper for the user config collection.
    /// </summary>
    internal sealed class UserConfigViewModel : BaseProp
    {

        private static UserConfigViewModel s_Inst;

        public static UserConfigViewModel Instance => s_Inst ?? (s_Inst = new UserConfigViewModel());

        public bool PreferScat
        {
            get => UserConfig.AllowExplicitDisposal;
            set
            {
                UserConfig.AllowExplicitDisposal = value;
                OnPropertyChanged();
            }
        }

        public bool PreySense
        {
            get => UserConfig.AllowPreySense;
            set
            {
                UserConfig.AllowPreySense = value;
                OnPropertyChanged();
            }
        }

        public bool ExplorerMode
        {
            get => UserConfig.ExplorerMode;
            set
            {
                UserConfig.ExplorerMode = value;
                OnPropertyChanged();
            }
        }

        public float ZoomLevel => UserConfig.ZoomFactor;

        public int ZoomIndex
        {
            get
            {
                float clamped = Math.Min(Math.Max(UserConfig.ZoomFactor, UserConfig.k_Zoom_Min), UserConfig.k_Zoom_Max);
                float based = clamped - 1.0f;
                return (int)Math.Round(based / 0.1f);
            }
            set
            {
                UserConfig.ZoomFactor = value * 0.1f + UserConfig.k_Zoom_Min;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ZoomLevel));
            }
        }

        public int CombatAnimation
        {
            get => (int)UserConfig.CombatSpeed;
            set => UserConfig.CombatSpeed = (UserConfig.EAnimationLevel)value;
        }

        private UserConfigViewModel() { }

    }

}
