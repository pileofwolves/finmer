/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core.Assets;

namespace Finmer.Core.Serialization
{

    /// <summary>
    /// Represents an interface for locating assets linked by other assets.
    /// </summary>
    public interface IContentStore
    {

        /// <summary>
        /// Look up an asset by its unique GUID, as assigned by the editor. Returns the cast asset if found, or null if not found.
        /// </summary>
        TAsset GetAssetByID<TAsset>(Guid id) where TAsset : AssetBase;

        /// <summary>
        /// Returns the name of the asset identified by its GUID, or an approximation if not found.
        /// </summary>
        string GetAssetName(Guid id);

    }

}
