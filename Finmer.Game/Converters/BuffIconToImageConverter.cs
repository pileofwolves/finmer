/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using Finmer.Core.Buffs;
using Finmer.Utility;

namespace Finmer.Converters
{

    /// <summary>
    /// Converts an EBuffIcon to an equivalent ImageSource.
    /// </summary>
    public class BuffIconToImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null);

            // Convert the enum value to an image
            return PackImageUtilities.GetImage(GetFileNameForEnumValue((EBuffIcon)value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Given an icon enum value, returns the image file name that can be used to represent it.
        /// </summary>
        private static string GetFileNameForEnumValue(EBuffIcon face)
        {
            switch (face)
            {
                case EBuffIcon.Generic:             return "Buffs/IconGeneric.png";
                default:                            throw new ArgumentOutOfRangeException(nameof(face));
            }
        }

    }

}
