/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Finmer.Converters
{

    /// <summary>
    /// Converts a <seealso cref="bool" /> to a <seealso cref="Visibility" /> state.
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, nameof(value) + " != null");
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, nameof(value) + " != null");
            return (Visibility)value == Visibility.Visible;
        }

    }

}
