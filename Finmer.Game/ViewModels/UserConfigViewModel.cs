/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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

        public bool Hyphenation
        {
            get => UserConfig.Hyphenation;
            set
            {
                UserConfig.Hyphenation = value;
                OnPropertyChanged(nameof(Hyphenation));
            }
        }

        public bool PreferScat
        {
            get => UserConfig.PreferScat;
            set
            {
                UserConfig.PreferScat = value;
                OnPropertyChanged(nameof(PreferScat));
            }
        }

        public bool PreySense
        {
            get => UserConfig.PreySense;
            set
            {
                UserConfig.PreySense = value;
                OnPropertyChanged(nameof(PreySense));
            }
        }

        public float ZoomLevel => UserConfig.Zoom;

        public int ZoomIndex
        {
            get
            {
                float clamped = Math.Min(Math.Max(UserConfig.Zoom, UserConfig.k_Zoom_Min), UserConfig.k_Zoom_Max);
                float based = clamped - 1.0f;
                return (int)Math.Round(based / 0.1f);
            }
            set
            {
                UserConfig.Zoom = value * 0.1f + UserConfig.k_Zoom_Min;
                OnPropertyChanged(nameof(ZoomIndex));
                OnPropertyChanged(nameof(ZoomLevel));
            }
        }

        public int CombatAnimation
        {
            get => (int)UserConfig.CombatAnimation;
            set => UserConfig.CombatAnimation = (UserConfig.EAnimationLevel)value;
        }

        private UserConfigViewModel() { }

    }

}
