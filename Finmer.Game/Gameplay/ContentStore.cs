/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Finmer.Core;
using Finmer.Core.Assets;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents a storage for game assets, optimized for fast lookups.
    /// </summary>
    public class ContentStore
    {

        private const int k_DefaultCapacity = 128;

        private readonly Dictionary<Guid, AssetBase> m_GuidMap = new Dictionary<Guid, AssetBase>(k_DefaultCapacity);
        private readonly Dictionary<string, AssetBase> m_NameMap = new Dictionary<string, AssetBase>(k_DefaultCapacity);
        private readonly Dictionary<Type, List<AssetBase>> m_TypeMap = new Dictionary<Type, List<AssetBase>>();
        private readonly StringTable m_MergedStrings = new StringTable();

        /// <summary>
        /// Adds a new asset to the content store.
        /// </summary>
        public void Add(AssetBase asset)
        {
            Debug.Assert(asset.ID != Guid.Empty, "Asset has no Guid");
            Debug.Assert(!String.IsNullOrEmpty(asset.Name), "Asset has no name");

            // Validate that the UUID is unique
            if (m_GuidMap.TryGetValue(asset.ID, out var duplicate))
                throw new DuplicateContentException($"Duplicate asset ID '{asset.ID}' found. All asset IDs must be unique.\r\n\r\nA: {duplicate.Name} (in {duplicate.SourceModuleName})\r\nB: {asset.Name} (in {asset.SourceModuleName})");

            // Validate that the name is unique
            if (m_NameMap.TryGetValue(asset.Name, out duplicate))
                throw new DuplicateContentException($"Duplicate asset name '{asset.Name}' found. All asset names must be unique.\r\n\r\nA: {duplicate.ID} (in {duplicate.SourceModuleName})\r\nB: {asset.ID} (in {asset.SourceModuleName})");

            // Special handling for string tables: merge them into the main table instead of registering them,
            // since we don't need the individual table at runtime, only the full, merged collection.
            if (asset is AssetStringTable string_table)
            {
                m_MergedStrings.Merge(string_table.Table);
                return;
            }

            // Map the asset to its GUID and name, for fast lookup
            m_GuidMap.Add(asset.ID, asset);
            m_NameMap.Add(asset.Name, asset);

            // Find the asset-by-type list for this type, or create it if it doesn't exist yet
            if (!m_TypeMap.TryGetValue(asset.GetType(), out List<AssetBase> by_type_list))
            {
                by_type_list = new List<AssetBase>();
                m_TypeMap.Add(asset.GetType(), by_type_list);
            }

            // Add the asset to the list of assets of its type
            by_type_list.Add(asset);
        }

        /// <summary>
        /// Adds a collection of assets to the content store.
        /// </summary>
        public void Add(IEnumerable<AssetBase> assets)
        {
            // Add each individual item
            foreach (var asset in assets)
                Add(asset);
        }

        /// <summary>
        /// Look up an asset by its unique GUID, as assigned by the editor. Returns the asset if found, or null if not found.
        /// </summary>
        public AssetBase GetAssetByID(Guid guid)
        {
            if (m_GuidMap.TryGetValue(guid, out AssetBase output))
                return output;

            return null;
        }

        /// <summary>
        /// Look up an asset by its unique name. Returns the asset if found, or null if not found.
        /// </summary>
        public AssetBase GetAssetByName(string name)
        {
            if (m_NameMap.TryGetValue(name, out AssetBase output))
                return output;

            return null;
        }

        /// <summary>
        /// Returns a collection of all registered assets of a particular type.
        /// </summary>
        public IEnumerable<TAsset> GetAssetsByType<TAsset>() where TAsset : AssetBase
        {
            // Return all assets of the matching type
            if (m_TypeMap.TryGetValue(typeof(TAsset), out var by_type_list))
                foreach (var asset in by_type_list)
                    yield return (TAsset)asset;
        }

        /// <summary>
        /// Retrieves a string from the game string table and automatically substitutes text parser tags.
        /// </summary>
        /// <param name="key">The table key to look up.</param>
        public string GetAndParseString(string key)
        {
            return TextParser.Parse(m_MergedStrings.GetRandomEntry(key));
        }

    }

}
