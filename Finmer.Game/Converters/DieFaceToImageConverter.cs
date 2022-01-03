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
using System.Windows.Media.Imaging;
using Finmer.Gameplay.Combat;
using Finmer.Utility;

namespace Finmer.Converters
{

    /// <summary>
    /// Converts an EDieFace to an equivalent ImageSource.
    /// </summary>
    public class DieFaceToImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null);

            // Convert the face to a file name
            EDieFace face = (EDieFace)value;
            string face_name = GetFileNameForDieFace(face);

            // Compose the image file name into a full path for display
            var source = new BitmapImage();
            source.BeginInit();
            source.UriSource = PackUriGenerator.GetGameResource(face_name);
            source.EndInit();
            source.Freeze();
            return source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Given a die face, returns the image file name that can be used to represent it.
        /// </summary>
        private string GetFileNameForDieFace(EDieFace face)
        {
            switch (face)
            {
                case EDieFace.Empty:                return "Dice/Empty.png";
                case EDieFace.Attack:               return "Dice/Attack1.png";
                case EDieFace.AttackCritical:       return "Dice/Attack2.png";
                case EDieFace.Defense:              return "Dice/Defense1.png";
                case EDieFace.DefenseCritical:      return "Dice/Defense2.png";
                case EDieFace.Grapple:              return "Dice/Grapple.png";
                case EDieFace.Generic1:             return "Dice/Generic1.png";
                case EDieFace.Generic2:             return "Dice/Generic2.png";
                case EDieFace.Generic3:             return "Dice/Generic3.png";
                case EDieFace.Generic4:             return "Dice/Generic4.png";
                case EDieFace.Generic5:             return "Dice/Generic5.png";
                case EDieFace.Generic6:             return "Dice/Generic6.png";
                default:                            throw new ArgumentOutOfRangeException(nameof(face));
            }
        }

    }

}
