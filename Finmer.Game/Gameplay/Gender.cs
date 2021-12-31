/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Text utilities regarding gender.
    /// </summary>
    public static class GenderUtil
    {

        public static string GetObjectivePronoun(EGender gender)
        {
            switch (gender)
            {
                case EGender.Male:          return "him";
                case EGender.Female:        return "her";
                case EGender.Neutral:       return "them";
                case EGender.Ungendered:    return "it";
                default:                    throw new ArgumentOutOfRangeException(nameof(gender));
            }
        }

        public static string GetSubjectivePronoun(EGender gender)
        {
            switch (gender)
            {
                case EGender.Male:          return "he";
                case EGender.Female:        return "she";
                case EGender.Neutral:       return "they";
                case EGender.Ungendered:    return "it";
                default:                    throw new ArgumentOutOfRangeException(nameof(gender));
            }
        }

        public static string GetPossessivePronoun(EGender gender)
        {
            switch (gender)
            {
                case EGender.Male:          return "his";
                case EGender.Female:        return "her";
                case EGender.Neutral:       return "their";
                case EGender.Ungendered:    return "its";
                default:                    throw new ArgumentOutOfRangeException(nameof(gender));
            }
        }

    }

}
