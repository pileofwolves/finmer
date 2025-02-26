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
            return PackImageUtilities.GetImage(face_name);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Given a die face, returns the image file name that can be used to represent it.
        /// </summary>
        private static string GetFileNameForDieFace(EDieFace face)
        {
            switch (face)
            {
                case EDieFace.Empty:                    return "Dice/Empty.png";
                case EDieFace.AlliedAttack:             return "Dice/AlliedAttack1.png";
                case EDieFace.AlliedAttackCritical:     return "Dice/AlliedAttack2.png";
                case EDieFace.AlliedDefense:            return "Dice/AlliedDefense1.png";
                case EDieFace.AlliedDefenseCritical:    return "Dice/AlliedDefense2.png";
                case EDieFace.AlliedGeneric1:           return "Dice/AlliedGeneric1.png";
                case EDieFace.AlliedGeneric2:           return "Dice/AlliedGeneric2.png";
                case EDieFace.AlliedGeneric3:           return "Dice/AlliedGeneric3.png";
                case EDieFace.AlliedGeneric4:           return "Dice/AlliedGeneric4.png";
                case EDieFace.AlliedGeneric5:           return "Dice/AlliedGeneric5.png";
                case EDieFace.AlliedGeneric6:           return "Dice/AlliedGeneric6.png";
                case EDieFace.HostileAttack:            return "Dice/EnemyAttack1.png";
                case EDieFace.HostileAttackCritical:    return "Dice/EnemyAttack2.png";
                case EDieFace.HostileDefense:           return "Dice/EnemyDefense1.png";
                case EDieFace.HostileDefenseCritical:   return "Dice/EnemyDefense2.png";
                case EDieFace.HostileGeneric1:          return "Dice/EnemyGeneric1.png";
                case EDieFace.HostileGeneric2:          return "Dice/EnemyGeneric2.png";
                case EDieFace.HostileGeneric3:          return "Dice/EnemyGeneric3.png";
                case EDieFace.HostileGeneric4:          return "Dice/EnemyGeneric4.png";
                case EDieFace.HostileGeneric5:          return "Dice/EnemyGeneric5.png";
                case EDieFace.HostileGeneric6:          return "Dice/EnemyGeneric6.png";
                default:                                throw new ArgumentOutOfRangeException(nameof(face));
            }
        }

    }

}
