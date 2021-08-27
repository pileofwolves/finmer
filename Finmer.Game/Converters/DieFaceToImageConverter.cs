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
using System.Windows.Data;
using Finmer.Gameplay.Combat;

namespace Finmer.Converters
{

    /// <summary>
    /// Converts an EDieFace to an equivalent image source URI.
    /// </summary>
    public class DieFaceToImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null);
            EDieFace face = (EDieFace)value;

            string face_name;
            switch (face)
            {
                case EDieFace.Empty:
                    face_name = "Dice/Empty.png";
                    break;
                case EDieFace.Attack:
                    face_name = "Dice/Attack1.png";
                    break;
                case EDieFace.AttackCritical:
                    face_name = "Dice/Attack2.png";
                    break;
                case EDieFace.Defense:
                    face_name = "Dice/Defense1.png";
                    break;
                case EDieFace.DefenseCritical:
                    face_name = "Dice/Defense2.png";
                    break;
                case EDieFace.Grapple:
                    face_name = "Dice/Grapple.png";
                    break;
                case EDieFace.VoreSwallow:
                    face_name = "Dice/VoreSwallow.png";
                    break;
                case EDieFace.VoreStruggle:
                    face_name = "Dice/VoreStruggle.png";
                    break;
                default:
                    face_name = "UI/MissingItemIcon.png";
                    break;
            }

            return new Uri("pack://application:,,,/Finmer;component/Resources/" + face_name, UriKind.Absolute);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }

}
