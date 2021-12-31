/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Finmer.Gameplay;
using Finmer.Utility;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model for the shop system.
    /// </summary>
    public sealed class ShopViewModel : BaseProp
    {

        /// <summary>
        /// Command for purchasing a selected ShopItemStack and transferring it to the player's inventory.
        /// </summary>
        public ICommand BuyCommand => m_BuyCommand ?? (m_BuyCommand = new RelayCommand(BuyItem, CanBuyItem));

        /// <summary>
        /// Command for selling a selected Item and transferring it to the shop's stock.
        /// </summary>
        public ICommand SellCommand => m_SellCommand ?? (m_SellCommand = new RelayCommand(SellItem, CanSellItem));

        /// <summary>
        /// Mirror of the ShopState's title setting.
        /// </summary>
        public string ShopTitle => m_Shop.Title;

        /// <summary>
        /// Mirror of the player's inventory.
        /// </summary>
        public IReadOnlyCollection<ShopItemStack> ShopInventory { get; private set; }

        /// <summary>
        /// Mirror of the player's name.
        /// </summary>
        public string PlayerName => m_Player.Name;

        /// <summary>
        /// Mirror of the player's inventory.
        /// </summary>
        public IReadOnlyCollection<Item> PlayerInventory { get; private set; }

        /// <summary>
        /// Mirror of the player's current money total.
        /// </summary>
        public int PlayerMoney => m_Player.Money;

        private readonly ShopState m_Shop;
        private readonly Player m_Player;

        private ICommand m_BuyCommand;
        private ICommand m_SellCommand;

        public ShopViewModel(ShopState state, Player player)
        {
            m_Shop = state;
            m_Player = player;

            // Populate UI with initial data
            UpdateUI();
        }

        private void BuyItem(object arg)
        {
            var stack = (ShopItemStack)arg;
            Debug.Assert(stack != null);

            // Remove one such item from the shop's own stock
            m_Shop.RemoveItem(stack.Item);

            // Add the item to the player's inventory and take away money
            m_Player.Money -= stack.Item.Asset.PurchaseValue;
            m_Player.Inventory.Add(stack.Item);

            UpdateUI();
        }

        private void SellItem(object arg)
        {
            var item = (Item)arg;
            Debug.Assert(item != null);

            // Remove the item from the player's inventory and add money
            m_Player.Money += ShopState.GetSalePrice(item.Asset.PurchaseValue);
            m_Player.Inventory.Remove(item);

            // Add the item to the shop's stock as replaceable stock
            m_Shop.AddItem(item, 1, ShopItemStack.EStackType.Regular);

            UpdateUI();
        }

        private bool CanBuyItem(object arg)
        {
            var stack = (ShopItemStack)arg;
            if (stack == null)
                return false;

            // Return true if the player has sufficient money to buy this item
            return m_Player.Money >= stack.Item.Asset.PurchaseValue;
        }

        private bool CanSellItem(object arg)
        {
            var item = (Item)arg;
            if (item == null)
                return false;

            // All items except quest items can be sold
            return !item.Asset.IsQuestItem;
        }

        private void UpdateUI()
        {
            // Update cached data
            PlayerInventory = new List<Item>(m_Player.Inventory);
            ShopInventory = new List<ShopItemStack>(m_Shop.Stock);

            // Submit change notifications to the UI
            OnPropertyChanged(nameof(PlayerMoney));
            OnPropertyChanged(nameof(PlayerInventory));
            OnPropertyChanged(nameof(ShopInventory));
        }

    }

}
