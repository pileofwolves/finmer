/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Globalization;
using System.Windows.Data;
using Finmer.Gameplay;

namespace Finmer.Converters
{

    /// <summary>
    /// Converter that takes an Item and returns a boolean indicating whether this Item may be freely deleted from the inventory.
    /// </summary>
    internal sealed class CharSheetItemDropConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Item item = value as Item;

            // Value may be null in the XAML designer
            if (item == null)
                return false;

            // Any item except quest items may be deleted if the player wishes
            return !item.Asset.IsQuestItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }

}
