/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
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
    /// Converts an EBuffImpact value to an equivalent ImageSource.
    /// </summary>
    public class BuffImpactToImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null);

            // Convert the enum value to an image
            return PackImageUtilities.GetImage(GetFileNameForEnumValue((EBuffImpact)value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Given an impact enum value, returns the image file name that can be used to represent it.
        /// </summary>
        private static string GetFileNameForEnumValue(EBuffImpact face)
        {
            switch (face)
            {
                case EBuffImpact.Positive:              return "Buffs/BackPositive.png";
                case EBuffImpact.Neutral:               return "Buffs/BackNeutral.png";
                case EBuffImpact.Negative:              return "Buffs/BackNegative.png";
                default:                                throw new ArgumentOutOfRangeException(nameof(face));
            }
        }

    }

}
