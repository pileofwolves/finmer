/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;

namespace Finmer.Core
{

    /// <summary>
    /// Represents state flags and behaviour overrides for this character.
    /// </summary>
    [Flags]
    public enum ECharacterFlags
    {
        None                = 0,
        NoGrapple           = 1,
        NoPrey              = 2,
        NoFight             = 4,
        NoXP                = 8,
        SkipTurns           = 16,
    }

}
