/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Assets;

namespace Finmer.Core.Serialization
{

    /// <summary>
    /// Provides utilities for (de)serializing asset objects from filesystem-agnostic streams.
    /// </summary>
    public static class AssetSerializer
    {

        /// <summary>
        /// Identifies the derived type of an asset.
        /// </summary>
        private enum EAssetType
        {
            AssetScene,
            AssetItem,
            AssetCreature,
            AssetStringTable,
            AssetScript,
            AssetFeat,
            AssetJournal,
            ScriptDataExternal,
            ScriptDataInline,
            ScriptDataVisual,
        }

        /// <summary>
        /// Instantiates and deserializes an asset from the input stream.
        /// </summary>
        public static IFurballSerializable DeserializeAsset(IFurballContentReader instream, int version)
        {
            // Instantiate the asset itself
            var type = instream.ReadEnumProperty<EAssetType>(@"!Type");
            var asset = InstantiateAsset(type);

            // Read its data from stream
            asset.Deserialize(instream, version);

            return asset;
        }

        /// <summary>
        /// Serializes an asset to an output stream.
        /// </summary>
        public static void SerializeAsset(IFurballContentWriter outstream, IFurballSerializable asset)
        {
            // Write the type identifier to the stream
            outstream.WriteEnumProperty(@"!Type", IdentifyAsset(asset));

            // Write asset contents
            asset.Serialize(outstream);
        }

        /// <summary>
        /// Factory function that instantiates an asset object based on its type ID.
        /// </summary>
        private static IFurballSerializable InstantiateAsset(EAssetType type)
        {
            switch (type)
            {
                case EAssetType.AssetScene:         return new AssetScene();
                case EAssetType.AssetItem:          return new AssetItem();
                case EAssetType.AssetCreature:      return new AssetCreature();
                case EAssetType.AssetStringTable:   return new AssetStringTable();
                case EAssetType.AssetScript:        return new AssetScript();
                case EAssetType.ScriptDataExternal: return new ScriptDataExternal();
                case EAssetType.ScriptDataInline:   return new ScriptDataInline();
                case EAssetType.ScriptDataVisual:   return new ScriptDataVisual();
                case EAssetType.AssetFeat:          return new AssetFeat();
                case EAssetType.AssetJournal:       return new AssetJournal();
                default:                            throw new FurballUnknownAssetException("Unknown asset type ID");
            }
        }

        /// <summary>
        /// Given an asset instance, returns the value that must be passed to InstantiateAsset() to return a new instance of the same type.
        /// </summary>
        private static EAssetType IdentifyAsset(IFurballSerializable asset)
        {
            switch (asset)
            {
                case AssetScene _:                  return EAssetType.AssetScene;
                case AssetItem _:                   return EAssetType.AssetItem;
                case AssetCreature _:               return EAssetType.AssetCreature;
                case AssetStringTable _:            return EAssetType.AssetStringTable;
                case AssetScript _:                 return EAssetType.AssetScript;
                case ScriptDataExternal _:          return EAssetType.ScriptDataExternal;
                case ScriptDataInline _:            return EAssetType.ScriptDataInline;
                case ScriptDataVisual _:            return EAssetType.ScriptDataVisual;
                case AssetFeat _:                   return EAssetType.AssetFeat;
                case AssetJournal _:                return EAssetType.AssetJournal;
                default:                            throw new FurballUnknownAssetException("Unknown asset class");
            }
        }

    }

}
