/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
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
    /// Converts an ESaveSlot to a label string that identifies the slot.
    /// </summary>
    public class SaveSlotLabelConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null);
            switch ((ESaveSlot)value)
            {
                case ESaveSlot.Checkpoint:      return "CP";
                case ESaveSlot.Manual1:         return "#1";
                case ESaveSlot.Manual2:         return "#2";
                case ESaveSlot.Manual3:         return "#3";
                default:                        throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }

}
