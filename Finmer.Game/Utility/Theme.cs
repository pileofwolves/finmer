/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows;
using System.Windows.Media;

namespace Finmer.Utility
{

    /// <summary>
    /// A collection of theme settings for easy access.
    /// </summary>
    public static class Theme
    {

        // ReSharper disable PossibleNullReferenceException
        public static readonly Color LogColorDefault = (Color)Application.Current.FindResource("LogColorDefault");
        public static readonly Color LogColorPositive = (Color)Application.Current.FindResource("LogColorPositive");
        public static readonly Color LogColorNeutral = (Color)Application.Current.FindResource("LogColorNeutral");
        public static readonly Color LogColorNegative = (Color)Application.Current.FindResource("LogColorNegative");
        public static readonly Color LogColorError = (Color)Application.Current.FindResource("LogColorError");
        public static readonly Color LogColorGray = (Color)Application.Current.FindResource("LogColorGray");
        public static readonly Color LogColorLightGray = (Color)Application.Current.FindResource("LogColorLightGray");
        public static readonly Color LogColorOrange = (Color)Application.Current.FindResource("LogColorOrange");
        public static readonly Color LogColorDarkGray = (Color)Application.Current.FindResource("LogColorDarkGray");
        public static readonly Color LogColorLevelup = (Color)Application.Current.FindResource("LogColorLightBlue");
        public static readonly Color LogColorHighlight = (Color)Application.Current.FindResource("LogColorHighlight");
        public static readonly Color LogColorDarkCyan = (Color)Application.Current.FindResource("LogColorDarkCyan");

        public static readonly Style TextBlockDefault = (Style)Application.Current.FindResource("TextBlockDefault");

        public static readonly Style TextBlockCombatLog = (Style)Application.Current.FindResource("TextBlockCombatLog");
        // ReSharper restore PossibleNullReferenceException

    }

}
