/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using Finmer.Core;
using Finmer.Gameplay.Scripting;
using Finmer.Utility;
using Finmer.Views;

namespace Finmer.Gameplay
{

    public sealed class ShopState : ScriptableObject
    {

        public ShopState(ScriptContext context) : base(context)
        {
            Stock = new List<ShopEntry>();
        }

        [ScriptableProperty(EScriptAccess.Read)]
        public string ShopKey { get; set; }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public string Title { get; set; }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public string StringGroup { get; set; }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public bool NeedsRestock { get; set; }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public int RestockInterval { get; set; }

        public List<ShopEntry> Stock { get; }

        public void AddItem(Item item, int quantity, bool unique = false)
        {
            int index = Stock.FindIndex(other => item.Asset.Name.Equals(other.Item.Asset.Name));
            if (index != -1)
            {
                // add quantity to existing stack
                ShopEntry entry = Stock[index];

                // if quantity is -1, it's an infinite stack, so don't touch the magic -1 number
                if (entry.Quantity != -1)
                    entry.Quantity += quantity;
            }
            else
            {
                // create new stack
                Stock.Add(new ShopEntry(item, quantity, unique));
            }
        }

        public void DeductItem(int index)
        {
            ShopEntry entry = Stock[index];

            // quantity of -1 indicates infinite stock
            if (entry.Quantity == -1) return;

            // decrease stack size
            entry.Quantity--;

            // remove empty stacks
            if (entry.Quantity == 0)
                Stock.RemoveAt(index);
        }

        [ScriptableFunction]
        private int LuaD_RemoveDefaultStock(IntPtr L)
        {
            Stock.RemoveAll(entry => !entry.Unique);
            return 0;
        }

        [ScriptableFunction]
        private int LuaD_RemoveAll(IntPtr L)
        {
            Stock.Clear();
            return 0;
        }

        private int Lua_AddItemHelper(IntPtr L, bool unique)
        {
            var item = ScriptableObject.FromLua(L, 2) as Item;
            if (item == null) return LuaApi.luaL_error(L, "bad argument 1: expected Item");

            var quantity = 1;
            if (LuaApi.lua_type(L, 3) == LuaApi.ELuaType.Number)
                quantity = (int)LuaApi.lua_tonumber(L, 3);

            AddItem(item, quantity, unique);
            return 0;
        }

        [ScriptableFunction]
        private int LuaD_AddItem(IntPtr L)
        {
            return Lua_AddItemHelper(L, false);
        }

        [ScriptableFunction]
        private int LuaD_AddUniqueItem(IntPtr L)
        {
            return Lua_AddItemHelper(L, true);
        }

        [ScriptableFunction]
        private int LuaD_Save(IntPtr L)
        {
            Save();
            return 0;
        }

        [ScriptableFunction]
        private int LuaD_Show(IntPtr L)
        {
            // sanity check
            var self = ScriptableObject.FromLua(L, 1) as ShopState;
            if (self == null || self != this)
                return LuaApi.luaL_error(L, "bad self to ShopState:Show()");

            // navigate to the thingy
            GameController.Window.Dispatcher.Invoke(delegate { GameController.Window.Navigate(new ShopPage(this), ENavigatorAnimation.SlideLeft); });
            return LuaApi.lua_yield(L, 0);
        }

        public static ShopState Load(ScriptContext context, string id)
        {
            // we save shop state as a nested propertybag in the player's main save, so it's all nicely contained
            // and doesn't clutter up the "global namespace" of the save file
            var ret = new ShopState(context)
            {
                Title = "Shop_" + id,
                StringGroup = "shop_generic_",
                ShopKey = id,
                NeedsRestock = true,
                RestockInterval = 24
            };

            // Look for the shop in the player's save data
            PropertyBag saved = GameController.Session.Player.AdditionalSaveData.GetNestedPropertyBag("shop_" + id);

            // If found, deserialize it. Otherwise, we leave the properties at defaults (as above).
            if (saved != null)
            {
                // Read basic metadata
                ret.Title = saved.GetString("title");
                ret.StringGroup = saved.GetString("strings");
                ret.NeedsRestock = saved.GetBool("restock_needed");
                ret.RestockInterval = saved.GetInt("restock_interval");

                // Read stock
                int num_stock = saved.GetInt("stock_count");
                for (var i = 0; i < num_stock; i++)
                {
                    int quantity = saved.GetInt($"stock_{i}_qty");
                    bool unique = saved.GetBool($"stock_{i}_unique");
                    PropertyBag data = saved.GetNestedPropertyBag($"stock_{i}_data");
                    ret.AddItem(Item.FromSaveGame(context, data), quantity, unique);
                }
            }

            return ret;
        }

        public void Save()
        {
            GameController.Session.Player.AdditionalSaveData.SetNestedPropertyBag("shop_" + ShopKey, SerializeProperties());
        }

        public override PropertyBag SerializeProperties()
        {
            var props = base.SerializeProperties();

            // Store metadata
            props.SetString("title", Title);
            props.SetString("strings", StringGroup);
            props.SetBool("restock_needed", NeedsRestock);
            props.SetInt("restock_interval", RestockInterval);

            // Serialize stock
            props.SetInt("stock_count", Stock.Count);
            for (var i = 0; i < Stock.Count; i++)
            {
                ShopEntry entry = Stock[i];
                props.SetInt($"stock_{i}_qty", entry.Quantity);
                props.SetBool($"stock_{i}_unique", entry.Unique);
                props.SetNestedPropertyBag($"stock_{i}_data", entry.Item.SerializeProperties());
            }

            return base.SerializeProperties();
        }

        public sealed class ShopEntry
        {

            public ShopEntry(Item item, int quantity, bool unique = false)
            {
                Item = item;
                Quantity = quantity;
                Unique = unique;
            }

            public Item Item { get; set; }
            public int Quantity { get; set; }
            public bool Unique { get; set; }

        }

    }

}
