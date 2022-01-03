/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using Finmer.Gameplay;

namespace Finmer.Converters
{

    /// <summary>
    /// Given an Item, returns an integer describing the sale price of the item when sold back to a shop.
    /// </summary>
    public sealed class ShopItemSellValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null);

            // Calculate sale price
            int purchase_value = (int)value;
            return ShopState.GetSalePrice(purchase_value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }

}
