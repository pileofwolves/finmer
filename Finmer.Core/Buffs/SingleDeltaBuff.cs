/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Serialization;

namespace Finmer.Core.Buffs
{

    /// <summary>
    /// Represents a buff that applies a simple delta value to a variable.
    /// </summary>
    public abstract class SingleDeltaBuff : Buff
    {

        /// <summary>
        /// Change to apply to the number of dice.
        /// </summary>
        public int Delta { get; set; }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteInt32Property("Delta", Delta);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            Delta = instream.ReadInt32Property("Delta");
        }

        public override EBuffImpact GetImpact()
        {
            if (Delta == 0)
                return EBuffImpact.Neutral;

            // Assuming positive delta is also actually positive for the player; if this is not the case
            // then derived classes can override this method to swap them around if needed.
            return Delta > 0 ? EBuffImpact.Positive : EBuffImpact.Negative;
        }

    }

}
