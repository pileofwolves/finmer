/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Gameplay.Combat
{

    /// <summary>
    /// Describes the face of a combat die.
    /// </summary>
    public enum EDieFace
    {
        Empty,
        AlliedAttack,
        AlliedAttackCritical,
        AlliedDefense,
        AlliedDefenseCritical,
        AlliedGeneric1,
        AlliedGeneric2,
        AlliedGeneric3,
        AlliedGeneric4,
        AlliedGeneric5,
        AlliedGeneric6,
        HostileAttack,
        HostileAttackCritical,
        HostileDefense,
        HostileDefenseCritical,
        HostileGeneric1,
        HostileGeneric2,
        HostileGeneric3,
        HostileGeneric4,
        HostileGeneric5,
        HostileGeneric6,
    }

}
