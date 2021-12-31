/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core.Serialization;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Represents a single asset that can be loaded by the game.
    /// </summary>
    public abstract class AssetBase
    {

        /// <summary>
        /// Gets this asset's unique ID.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets this asset's file name.
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the name of the module this asset was loaded from.
        /// </summary>
        public string SourceModuleName { get; set; } = String.Empty;

        /// <summary>
        /// Save the asset to a stream.
        /// </summary>
        public virtual void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteGuidProperty("ID", ID);
            outstream.WriteStringProperty("Name", Name);
        }

        /// <summary>
        /// Load the asset from a stream, overwriting existing data.
        /// </summary>
        public virtual void Deserialize(IFurballContentReader instream, int version)
        {
            ID = instream.ReadGuidProperty("ID");
            Name = instream.ReadStringProperty("Name");
        }

    }

}
