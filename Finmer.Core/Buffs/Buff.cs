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
    /// Represents a special condition that affects a Character, positive or negative.
    /// </summary>
    public abstract class Buff
    {

        /// <summary>
        /// Describes this buff.
        /// </summary>
        public abstract EBuffIcon GetIcon();

        /// <summary>
        /// Instantiates a new copy of this buff object.
        /// </summary>
        public abstract Buff Clone();

        /// <summary>
        /// Gets an unlocalized human-readable description of the buff, for editor display.
        /// </summary>
        public abstract string GetDescription();

    }

    /// <summary>
    /// Represents a Buff that lasts a limited number of combat turns.
    /// </summary>
    public abstract class VolatileBuff : Buff
    {

        /// <summary>
        /// Number of combat turns remaining before this buff disappears.
        /// </summary>
        public int TurnsRemaining { get; set; }

    }

    /// <summary>
    /// Represents a buff that applies a simple delta value to a variable.
    /// </summary>
    public abstract class SimpleDeltaBuff : Buff
    {

        /// <summary>
        /// Change to apply to the number of dice.
        /// </summary>
        public int Delta { get; set; }

    }

}
