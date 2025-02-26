﻿/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core.Assets;
using Finmer.Core.Serialization;

namespace Finmer.Editor
{

    /// <summary>
    /// Editor-specific content store implementation that uses the loaded Furball and its dependencies.
    /// </summary>
    public class EditorContentStore : IContentStore
    {

        public TAsset GetAssetByID<TAsset>(Guid id) where TAsset : AssetBase
        {
            return (Program.ActiveFurball.GetAssetByID(id) ?? Program.ActiveDependencies.GetAssetByID(id)) as TAsset;
        }

        public string GetAssetName(Guid id)
        {
            // Return a human-readable string for the common case of an unset link
            if (id == Guid.Empty)
                return "[None]";

            // Otherwise, try to resolve the asset name
            var asset = GetAssetByID<AssetBase>(id);
            return asset == null
                ? id.ToString()
                : asset.Name;
        }

    }

}
