/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Serialization;

namespace Finmer.Core.Buffs
{

    /// <summary>
    /// Represents a special condition that affects a Character, positive or negative.
    /// </summary>
    public abstract class Buff : IFurballSerializable
    {

        /// <summary>
        /// Describes how a buff affects the player.
        /// </summary>
        public enum EImpact
        {
            Neutral,
            Positive,
            Negative
        }

        /// <summary>
        /// Describes how this buff affects the player.
        /// </summary>
        public abstract EImpact GetImpact();

        /// <summary>
        /// Gets an unlocalized human-readable description of the buff, for editor display.
        /// </summary>
        public abstract string GetEditorDescription();

        public virtual void Serialize(IFurballContentWriter outstream) {}

        public virtual void Deserialize(IFurballContentReader instream, int version) {}

    }

}
