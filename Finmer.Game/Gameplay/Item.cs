/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.IO;
using System.Windows.Media.Imaging;
using Finmer.Core;
using Finmer.Core.Assets;
using Finmer.Gameplay.Scripting;
using Finmer.Utility;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents an inventory item.
    /// </summary>
    public class Item : GameObject
    {

        private AssetItem m_Asset;

        /// <summary>
        /// Returns the item asset that describes the behavior of this item instance.
        /// </summary>
        public AssetItem Asset
        {
            get => m_Asset;
            private set
            {
                m_Asset = value;

                // Copy name and alias from the asset
                Gender = EGender.Ungendered;
                Name = m_Asset.ObjectName;
                Alias = m_Asset.ObjectAlias;
            }
        }

        /// <summary>
        /// Returns the item asset's assigned name. Enables scripts to test equality between Item objects.
        /// </summary>
        [ScriptableProperty(EScriptAccess.Read)]
        public string AssetName => Asset.Name;

        /// <summary>
        /// Returns the inventory icon that was configured for this item.
        /// </summary>
        public BitmapImage Image
        {
            get
            {
                var src = new BitmapImage();
                src.BeginInit();
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.DecodePixelWidth = 32;
                src.DecodePixelHeight = 32;
                if (Asset.InventoryIcon == null)
                {
                    // no icon specified in editor, so use a default one included in resources
                    src.UriSource = PackUriGenerator.GetGameResource("UI/MissingItemIcon.png");
                    src.EndInit();
                    src.Freeze(); // prevents unnecessary CPU->GPU copies
                }
                else
                {
                    // parse image from the raw byte array
                    using (var ms = new MemoryStream(Asset.InventoryIcon))
                    {
                        src.StreamSource = ms;
                        src.EndInit();
                        src.Freeze(); // prevents unnecessary CPU->GPU copies
                    }
                }

                return src;
            }
        }

        private Item(ScriptContext context) : base(context) {}

        public override PropertyBag SaveState()
        {
            // Item is a bit of a special case; because all its properties are read-only from script, we can safely discard the entire
            // instance and just save the Asset ID, then look it up in the Furball when we deserialize to re-create the same object.
            var serialized = new PropertyBag();
            serialized.SetBytes(SaveData.k_AssetID, Asset.ID.ToByteArray()); // asset ID
            return serialized;
        }

        public override void LoadState(PropertyBag input)
        {
            // Read the asset file ID
            byte[] asset_id_bytes = input.GetBytes(SaveData.k_AssetID);
            if (asset_id_bytes == null || asset_id_bytes.Length != 16)
                throw new InvalidSaveDataException("Missing asset ID in Item");

            // Get the asset associated with that ID
            var asset_guid = new Guid(asset_id_bytes);
            var item = GameController.Content.GetAssetByID<AssetItem>(asset_guid);
            if (item == null)
                throw new InvalidSaveDataException($"Could not find Item {asset_guid} in loaded modules");

            // Assign reference to internal asset object
            Asset = item;
        }

        public static Item FromAsset(ScriptContext context, string asset_name)
        {
            if (asset_name == null)
                throw new ArgumentNullException(nameof(asset_name));

            // Find the AssetItem represented by the specified file name
            AssetItem item = GameController.Content.GetAssetByName(asset_name) as AssetItem;
            if (item == null)
                throw new MissingContentException($"Could not find Item '{asset_name}' in loaded modules");

            // Initialize the item
            return new Item(context)
            {
                Asset = item
            };
        }

        public static Item FromAsset(ScriptContext context, Guid guid)
        {
            // Empty Guid is well-defined to return a null object
            if (guid == Guid.Empty)
                return null;

            // Find the AssetItem represented by the specified file name
            AssetItem item = GameController.Content.GetAssetByID(guid) as AssetItem;
            if (item == null)
                return null;

            // Initialize the item
            return new Item(context)
            {
                Asset = item
            };
        }

        public static Item FromSaveData(ScriptContext context, PropertyBag save_data)
        {
            var instance = new Item(context);
            instance.LoadState(save_data);
            return instance;
        }

    }

}
