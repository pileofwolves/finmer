/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Finmer.Gameplay;

namespace Finmer.Converters
{

    /// <summary>
    /// Given a ShopItemStack, returns the appropriate Visibility of the quantity indicator.
    /// </summary>
    public class ShopStackQuantityVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ShopItemStack stack = value as ShopItemStack;

            // Value may be null in the XAML designer (return Visible in this case so the item is shown in the designer)
            if (stack == null)
                return Visibility.Visible;

            return stack.Quantity > -1 ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }

}
