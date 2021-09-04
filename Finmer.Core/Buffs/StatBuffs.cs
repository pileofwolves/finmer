/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Core.Buffs
{

    /// <summary>
    /// Modifies the maximum HP of a character.
    /// </summary>
    public sealed class BuffHealth : SimpleDeltaBuff
    {

        public override EBuffIcon GetIcon()
        {
            return Delta >= 0 ? EBuffIcon.IncreasedHealth : EBuffIcon.DecreasedHealth;
        }

    }

}
