/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Globalization;
using System.Windows.Data;
using Finmer.Core.Assets;
using Finmer.Gameplay;

namespace Finmer.Converters
{

    /// <summary>
    /// Converter that takes an Item and returns a boolean indicating whether this Item can be 'used' from the character sheet.
    /// </summary>
    internal sealed class CharSheetItemUseConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Item item = value as Item;

            // Value may be null in the XAML designer
            if (item == null)
                return false;

            // Usable-type items as well as equipable items can be 'used' from the character sheet
            switch (item.Asset.ItemType)
            {
                case AssetItem.EItemType.Usable:
                    return item.Asset.CanUseInField;

                default:
                    return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }

}
