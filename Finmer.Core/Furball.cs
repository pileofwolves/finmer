/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Finmer.Core.Assets;

namespace Finmer.Core
{

    /// <summary>
    /// Represents a collection of assets that can be loaded by the game.
    /// </summary>
    public sealed class Furball
    {

        /// <summary>
        /// Contains the metadata that describes this package.
        /// </summary>
        public FurballMetadata Metadata { get; set; }

        /// <summary>
        /// Gets a collection of assets contained by this asset package.
        /// </summary>
        public List<AssetBase> Assets { get; } = new List<AssetBase>();

        /// <summary>
        /// Gets a collection of asset package IDs required by this asset package.
        /// </summary>
        public List<FurballDependency> Dependencies { get; } = new List<FurballDependency>();

        /// <summary>
        /// Copies the assets of another Furball into this one.
        /// </summary>
        /// <param name="other">The asset package to merge with the current instance.</param>
        public void Merge(Furball other)
        {
            other.Assets.ForEach(Assets.Add);
        }

        /// <summary>
        /// Searches for an asset with the given name and returns it.
        /// </summary>
        public AssetBase GetAssetByName(string name)
        {
            return Assets.FirstOrDefault(asset => asset.Name.Equals(name));
        }

        /// <summary>
        /// Searches for an asset with the given unique ID and returns it.
        /// </summary>
        public AssetBase GetAssetByID(Guid id)
        {
            return Assets.FirstOrDefault(asset => asset.ID.Equals(id));
        }

    }

}
