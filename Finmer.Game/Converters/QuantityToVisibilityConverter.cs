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
    /// Converts a quantity to a <seealso cref="Visibility" /> state. If the quantity is positive or zero, the element is
    /// visible.
    /// Otherwise, the element is hidden.
    /// </summary>
    public class QuantityToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, nameof(value) + " != null");
            return (int)value > -1 ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }

}
