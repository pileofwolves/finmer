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
using Finmer.Gameplay.Scripting;
using Finmer.Utility;
using Finmer.Views;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents a serializable item shop.
    /// </summary>
    public class ShopState : ScriptableObject
    {

        /// <summary>
        /// The unique identifier of this shop, used for serializing the shop to and from save data.
        /// </summary>
        [ScriptableProperty(EScriptAccess.Read)]
        public string Key { get; set; }

        /// <summary>
        /// The title of the shop, as presented in the UI.
        /// </summary>
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public string Title { get; set; }

        /// <summary>
        /// Time in world clock hours until the RestockRequired property will be flipped to 'true'.
        /// </summary>
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public int RestockInterval { get; set; }

        /// <summary>
        /// Indicates whether this shop's RestockInterval has elapsed.
        /// </summary>
        [ScriptableProperty(EScriptAccess.Read)]
        public bool RestockRequired { get; private set; }

        /// <summary>
        /// The timestamp in world clock hours when this stock was last restocked.
        /// </summary>
        [ScriptableProperty(EScriptAccess.Read)]
        public int RestockLastTime { get; private set; }

        /// <summary>
        /// Contains the stock of items available in this shop.
        /// </summary>
        public List<ShopItemStack> Stock { get; } = new List<ShopItemStack>();

        public ShopState(ScriptContext context) : base(context) {}

        public void AddItem(Item item, int quantity, ShopItemStack.EStackType type)
        {
            // If the item is not unique, we can try merging it with an existing stack
            if (type != ShopItemStack.EStackType.Unique)
            {
                // Find an existing stack of the same item type
                int index = Stock.FindIndex(other => item.Asset.Name.Equals(other.Item.Asset.Name));
                if (index != -1)
                {
                    ShopItemStack entry = Stock[index];

                    // Add the quantity to the stack (unless it already has infinite stock)
                    if (entry.Quantity != -1)
                        entry.Quantity += quantity;

                    return;
                }
            }

            // The item cannot be merged with another stack, so create a new stack
            Stock.Add(new ShopItemStack(item, type)
            {
                Quantity = quantity
            });
        }

        /// <summary>
        /// Deduct the quantity of an item.
        /// </summary>
        public void RemoveItem(Item item)
        {
            int index = Stock.FindIndex(other => item.Asset.Name.Equals(other.Item.Asset.Name));
            if (index == -1)
            {
                Debug.Fail($"Failed to find item {item.Asset.Name} in shop stock");
                return;
            }

            ShopItemStack entry = Stock[index];

            // If the item has infinite stock, don't change the quantity value
            if (entry.Quantity == -1)
                return;

            // Decrease stack size
            entry.Quantity--;

            // Remove empty stacks
            if (entry.Quantity == 0)
                Stock.RemoveAt(index);
        }

        /// <summary>
        /// Given an item's base purchase value, returns the sale price the item goes for when sold back to a shop.
        /// </summary>
        public static int GetSalePrice(int buyPrice)
        {
            return (buyPrice + 1) / 2;
        }

        /// <summary>
        /// Returns the shop with the specified unique ID, either by retrieving it from save data or by generating a new one.
        /// </summary>
        /// <param name="context">The script context this shop object must be bound to.</param>
        /// <param name="id">The unique ID of the shop to load.</param>
        public static ShopState LoadOrCreate(ScriptContext context, string id)
        {
            // Generate a default shop
            var ret = new ShopState(context)
            {
                Key = id,
                Title = "Shop",
                RestockRequired = true,
                RestockInterval = 24
            };

            // Look for the shop in the player's save data
            Player player = GameController.Session.Player;
            PropertyBag saved = player.AdditionalSaveData.GetNestedPropertyBag(GetShopID(id));

            // If found, deserialize it. Otherwise, we leave the properties at defaults (as above).
            if (saved != null)
            {
                // Overwrite the shop's settings with things retrieved from the save file
                ret.Title = saved.GetString(SaveData.k_Shop_Title);
                ret.RestockInterval = saved.GetInt(SaveData.k_Shop_RestockInterval);
                ret.RestockLastTime = saved.GetInt(SaveData.k_Shop_RestockTimestamp);

                // Read stock
                int num_stock = saved.GetInt(SaveData.k_Shop_StockCount);
                for (var i = 0; i < num_stock; i++)
                {
                    var quantity = saved.GetInt(SaveData.CombineBase(SaveData.k_Shop_StockQuantityBase, i));
                    var type = saved.GetBool(SaveData.CombineBase(SaveData.k_Shop_StockUniqueBase, i)) ? ShopItemStack.EStackType.Unique : ShopItemStack.EStackType.Regular;
                    var item_data = saved.GetNestedPropertyBag(SaveData.CombineBase(SaveData.k_Shop_StockItemBase, i));
                    ret.AddItem(Item.FromSaveGame(context, item_data), quantity, type);
                }

                // If the restock interval has elapsed, set the RestockRequired flag
                int total_world_hours = player.TimeHourCumulative;
                ret.RestockRequired = ret.RestockInterval != 0 && total_world_hours >= ret.RestockLastTime + ret.RestockInterval;
            }

            return ret;
        }

        /// <summary>
        /// Serializes a snapshot of the shop to the player's save data.
        /// </summary>
        public void Save()
        {
            PropertyBag save_data = SerializeProperties();
            GameController.Session.Player.AdditionalSaveData.SetNestedPropertyBag(GetShopID(Key), save_data);
        }

        public override PropertyBag SerializeProperties()
        {
            PropertyBag props = base.SerializeProperties();

            // Serialize metadata
            props.SetString(SaveData.k_Shop_Title, Title);
            props.SetBool(SaveData.k_Shop_RestockRequired, RestockRequired);
            props.SetInt(SaveData.k_Shop_RestockInterval, RestockInterval);
            props.SetInt(SaveData.k_Shop_RestockTimestamp, RestockLastTime);

            // Serialize stock
            props.SetInt(SaveData.k_Shop_StockCount, Stock.Count);
            for (var i = 0; i < Stock.Count; i++)
            {
                ShopItemStack entry = Stock[i];
                props.SetInt(SaveData.CombineBase(SaveData.k_Shop_StockQuantityBase, i), entry.Quantity);
                props.SetBool(SaveData.CombineBase(SaveData.k_Shop_StockUniqueBase, i), entry.Type == ShopItemStack.EStackType.Unique);
                props.SetNestedPropertyBag(SaveData.CombineBase(SaveData.k_Shop_StockItemBase, i), entry.Item.SerializeProperties());
            }

            return props;
        }

        private int AddItemInternal(IntPtr state, ShopItemStack.EStackType type)
        {
            // Retrieve arguments from script
            var item = FromLuaNonOptional<Item>(state, 2);
            var quantity = (int)LuaApi.luaL_optnumber(state, 3, 1);

            // Add the item to the shop
            AddItem(item, quantity, type);

            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedRemoveDefaultStock(IntPtr state)
        {
            var shop = FromLuaNonOptional<ShopState>(state, 1);
            shop.Stock.RemoveAll(entry => entry.Type == ShopItemStack.EStackType.Regular);
            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedRemoveAll(IntPtr state)
        {
            var shop = FromLuaNonOptional<ShopState>(state, 1);
            shop.Stock.Clear();
            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedAddItem(IntPtr state)
        {
            var shop = FromLuaNonOptional<ShopState>(state, 1);
            return shop.AddItemInternal(state, ShopItemStack.EStackType.Regular);
        }

        [ScriptableFunction]
        protected static int ExportedAddUniqueItem(IntPtr state)
        {
            var shop = FromLuaNonOptional<ShopState>(state, 1);
            return shop.AddItemInternal(state, ShopItemStack.EStackType.Unique);
        }

        [ScriptableFunction]
        protected static int ExportedSave(IntPtr state)
        {
            var shop = FromLuaNonOptional<ShopState>(state, 1);
            shop.Save();
            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedMarkRestocked(IntPtr state)
        {
            // Update the shop's restock timestamp, and remove the restock-required flag
            var shop = FromLuaNonOptional<ShopState>(state, 1);
            shop.RestockRequired = false;
            shop.RestockLastTime = GameController.Session.Player.TimeHourCumulative;

            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedShow(IntPtr state)
        {
            // Queue a delegate on the main thread to navigate to the shop page
            var shop = FromLuaNonOptional<ShopState>(state, 1);
            GameController.Window.Dispatcher.Invoke(delegate { GameController.Window.Navigate(new ShopPage(shop), ENavigatorAnimation.SlideLeft); });

            // Pause the script until the shop page closes
            return LuaApi.lua_yield(state, 0);
        }

        /// <summary>
        /// Returns a deterministic namespace to use for the shop in save data.
        /// </summary>
        private static string GetShopID(string shopName)
        {
            return "shop_" + shopName;
        }

    }

}
