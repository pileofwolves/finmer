/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents a special condition that affects a Character.
    /// </summary>
    public abstract class Buff
    {

        /// <summary>
        /// Number of combat turns remaining before this buff disappears. If -1, the buff does not disappear.
        /// </summary>
        public int TurnsRemaining { get; set; }

        /// <summary>
        /// Whether the buff is removed automatically when combat ends.
        /// </summary>
        public bool IsVolatile { get; set; }

        /// <summary>
        /// Returns the display-friendly name of the buff.
        /// </summary>
        public abstract string GetName();

    }

    public class BuffAttackDice : Buff
    {

        /// <summary>
        /// Change to apply to the number of dice.
        /// </summary>
        public int Delta { get; set; }

        public override string GetName()
        {
            return (Delta >= 0) ? "Extra Attack Dice" : "Reduced Attack Dice";
        }

    }

    public class BuffDefenseDice : Buff
    {

        /// <summary>
        /// Change to apply to the number of dice.
        /// </summary>
        public int Delta { get; set; }

        public override string GetName()
        {
            return (Delta >= 0) ? "Extra Defense Dice" : "Reduced Defense Dice";
        }

    }

    public class BuffHealth : Buff
    {

        /// <summary>
        /// Change to apply to the health maximum.
        /// </summary>
        public int Delta { get; set; }

        public override string GetName()
        {
            return (Delta >= 0) ? "Increased Health" : "Decreased Health";
        }

    }

}
