/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Core.Buffs
{

    /// <summary>
    /// Modifies the maximum HP of a character.
    /// </summary>
    public sealed class BuffHealth : SingleDeltaBuff
    {

        public override EBuffIcon GetIcon()
        {
            return Delta >= 0 ? EBuffIcon.IncreasedHealth : EBuffIcon.DecreasedHealth;
        }

        public override string GetDescription()
        {
            return $"{Delta:+#;-#;0} Max Health";
        }

        public override Buff Clone()
        {
            return new BuffHealth
            {
                Delta = Delta
            };
        }

    }

}
