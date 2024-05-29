/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Core.Serialization
{

    /// <summary>
    /// Represents an object that can be (de)serialized to/from a Furball asset stream.
    /// </summary>
    public interface IFurballSerializable
    {

        /// <summary>
        /// Save the object to an asset stream.
        /// </summary>
        void Serialize(IFurballContentWriter outstream);

        /// <summary>
        /// Load the object from an asset stream, overwriting existing instance data.
        /// </summary>
        void Deserialize(IFurballContentReader instream);

    }

}
