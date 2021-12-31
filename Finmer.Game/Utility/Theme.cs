/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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

        public static readonly Color LogColorDefault;
        public static readonly Color LogColorPositive;
        public static readonly Color LogColorNeutral;
        public static readonly Color LogColorNegative;
        public static readonly Color LogColorError;
        public static readonly Color LogColorNotification;
        public static readonly Color LogColorGray;
        public static readonly Color LogColorLightGray;
        public static readonly Color LogColorDarkGray;
        public static readonly Color LogColorHighlight;
        public static readonly Color LogColorDarkCyan;
        public static readonly Style TextBlockDefault;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S3963:\"static\" fields should be initialized inline",
            Justification = "More readable, and not concerned about minor performance overhead")]
        static Theme()
        {
            Application app = Application.Current;

            // ReSharper disable PossibleNullReferenceException
            LogColorDefault = (Color)app.FindResource("LogColorDefault");
            LogColorPositive = (Color)app.FindResource("LogColorPositive");
            LogColorNeutral = (Color)app.FindResource("LogColorNeutral");
            LogColorNegative = (Color)app.FindResource("LogColorNegative");
            LogColorError = (Color)app.FindResource("LogColorError");
            LogColorNotification = (Color)app.FindResource("LogColorNotification");
            LogColorGray = (Color)app.FindResource("LogColorGray");
            LogColorLightGray = (Color)app.FindResource("LogColorLightGray");
            LogColorDarkGray = (Color)app.FindResource("LogColorDarkGray");
            LogColorHighlight = (Color)app.FindResource("LogColorHighlight");
            LogColorDarkCyan = (Color)app.FindResource("LogColorDarkCyan");
            TextBlockDefault = (Style)app.FindResource("TextBlockDefault");
        }

    }

}
