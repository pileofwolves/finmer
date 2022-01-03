/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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

        public AssetItem Asset { get; }

        public BitmapImage Image
        {
            get
            {
                var src = new BitmapImage();
                src.BeginInit();
                src.CacheOption = BitmapCacheOption.OnLoad;
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

        private Item(ScriptContext context, PropertyBag template, AssetItem asset) : base(context, template)
        {
            Asset = asset;

            // Copy name and alias from the asset
            Name = Asset.ObjectName;
            Alias = asset.ObjectAlias;
        }

        public override PropertyBag SerializeProperties()
        {
            // Item is a bit of a special case; because all its properties are read-only from script, we can safely discard the entire
            // instance and just save the Asset ID, then look it up in the Furball when we deserialize to re-create the same object.
            var serialized = new PropertyBag();
            serialized.SetBytes(SaveData.k_AssetID, Asset.ID.ToByteArray()); // asset ID
            return serialized;
        }

        public static Item FromAsset(ScriptContext context, string assetName)
        {
            // Find the AssetItem represented by the specified file name
            AssetItem item = GameController.Content.GetAssetByName(assetName) as AssetItem;
            if (item == null)
                return null;

            // Initialize the item with empty save data
            PropertyBag template = new PropertyBag();
            return new Item(context, template, item);
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

            // Initialize the item with empty save data
            PropertyBag template = new PropertyBag();
            return new Item(context, template, item);
        }

        public static Item FromSaveGame(ScriptContext context, PropertyBag savedata)
        {
            // Read the asset file ID
            byte[] asset_id_bytes = savedata.GetBytes(SaveData.k_AssetID);
            if (asset_id_bytes == null || asset_id_bytes.Length != 16)
                return null;

            // Get the asset associated with that ID
            var asset_guid = new Guid(asset_id_bytes);
            AssetItem item = GameController.Content.GetAssetByID(asset_guid) as AssetItem;
            if (item == null)
                return null;

            // We don't need to actually restore any state from the save data because Items are immutable
            var template = new PropertyBag();
            return new Item(context, template, item);
        }

    }

}
