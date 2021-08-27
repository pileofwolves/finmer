/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using Finmer.Core;
using Finmer.Core.Assets;
using Finmer.Gameplay.Scripting;
using Finmer.Models;
using Finmer.Utility;
using Finmer.ViewModels;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents an inventory item.
    /// </summary>
    public class Item : GameObject
    {

        public AssetItem Asset { get; }

        public List<Buff> Buffs { get; } = new List<Buff>();

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
                    src.UriSource = new Uri("pack://application:,,,/Finmer;component/Resources/UI/MissingItemIcon.png", UriKind.Absolute);
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
        }

        public override PropertyBag SerializeProperties()
        {
            // Item is a bit of a special case; because all its properties are read-only from script, we can safely discard the entire
            // instance and just save the Asset ID, then look it up in the Furball when we deserialize to re-create the same object.
            var serialized = new PropertyBag();
            serialized.SetBytes("guid", ID.ToByteArray()); // stack ID
            serialized.SetBytes("asset", Asset.ID.ToByteArray()); // asset ID
            return serialized;
        }

        protected override GameObjectViewModel CreateViewModel()
        {
            throw new NotImplementedException();
        }

        public static Item FromAsset(ScriptContext context, string assetName)
        {
            try
            {
                // Find the AssetItem represented by the specified file name
                AssetBase asset = GameController.Content.GetAssetByName(assetName);
                if (!(asset is AssetItem item))
                    throw new ArgumentException($"The specified asset ('{assetName ?? "[null]"}') does not exist or is not an Item.", nameof(assetName));

                // Initialize the item with empty save data
                PropertyBag template = new PropertyBag();
                return new Item(context, template, item);
            }
            catch (Exception ex)
            {
                GameUI.Instance.Log($"ERROR: Failed to create item '{assetName}': {ex}", Theme.LogColorError);
                return null;
            }
        }

        public static Item FromSaveGame(ScriptContext context, PropertyBag savedata)
        {
            try
            {
                // Read the asset file ID
                byte[] asset_id_bytes = savedata.GetBytes("asset")
                    ?? throw new ArgumentException("A saved item is missing its asset GUID. This save file is probably corrupt.", nameof(savedata));

                // Get the asset associated with that ID
                var asset_guid = new Guid(asset_id_bytes);
                AssetItem item = GameController.Content.GetAssetByID(asset_guid) as AssetItem
                    ?? throw new ArgumentException($"A saved item has Asset ID {asset_guid}, but no such Item was found in loaded modules. Was it unloaded or edited in the mean time?");

                // Save data used for deserialization only needs to contain the ScriptableObject ID, everything else is taken
                // from the AssetItem directly since it is read-only and can be treated as the single source of truth for items.
                var template = new PropertyBag();
                template.SetBytes("guid", savedata.GetBytes("guid"));

                return new Item(context, template, item);
            }
            catch (Exception ex)
            {
                GameUI.Instance.Log($"ERROR: Failed to load item from save: {ex}", Theme.LogColorError);
                return null;
            }
        }

    }

}
