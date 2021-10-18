/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows;
using System.Windows.Controls;
using Finmer.Gameplay;
using Finmer.Utility;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for ShopPage.xaml
    /// </summary>
    public partial class ShopPage
    {

        private readonly ShopState m_State;
        private Item m_SelectedItem;

        public ShopPage(ShopState state)
        {
            InitializeComponent();

            DataContext = GameController.Session.Player.GetOrCreateViewModel();

            // TODO: MVVMify this
            m_State = state;
            ShopTitle.Text = m_State.Title;
            NpcPanel.DataContext = m_State;
            ValuePanel.Visibility = Visibility.Collapsed;
            SetShopkeepText("hello");

            ButtonSell.IsEnabled = false;
            ButtonBuy.IsEnabled = false;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            GameController.Window.Navigate(new MainPage(), ENavigatorAnimation.SlideRight);
            GameController.Session.ResumeScript();
        }

        private void ItemsShop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemsShop.SelectedIndex == -1) return;

            // deselect other list item
            ItemsPlayer.SelectedIndex = -1;
            m_SelectedItem = m_State.Stock[ItemsShop.SelectedIndex].Item;

            // UI updates
            bool can_afford = GameController.Session.Player.Money >= m_SelectedItem.Asset.PurchaseValue;
            SetShopkeepText(can_afford ? "buy" : "cannot_afford");
            ValueText.Text = m_SelectedItem.Asset.PurchaseValue.ToString();
            ValuePanel.Visibility = Visibility.Visible;
            ButtonSell.IsEnabled = false;
            ButtonBuy.IsEnabled = can_afford;
        }

        private void ItemsPlayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemsPlayer.SelectedIndex == -1) return;

            // deselect other list item
            ItemsShop.SelectedIndex = -1;
            m_SelectedItem = GameController.Session.Player.Inventory[ItemsPlayer.SelectedIndex];

            // cannot sell quest items or worthless items
            if (m_SelectedItem.Asset.IsQuestItem || m_SelectedItem.Asset.PurchaseValue < 1)
            {
                ButtonSell.IsEnabled = false;
                ButtonBuy.IsEnabled = false;
                ValuePanel.Visibility = Visibility.Collapsed;
                SetShopkeepText("cannot_sell");
                return;
            }

            // UI updates
            SetShopkeepText("sell");
            ValuePanel.Visibility = Visibility.Visible;
            ValueText.Text = GetSalePrice(m_SelectedItem.Asset.PurchaseValue).ToString();
            ButtonSell.IsEnabled = true;
            ButtonBuy.IsEnabled = false;
        }

        private void ButtonBuy_Click(object sender, RoutedEventArgs e)
        {
            SetShopkeepText("bought");
            ButtonSell.IsEnabled = false;
            ButtonBuy.IsEnabled = false;

            // take money and add item
            GameController.Session.Player.Money -= m_SelectedItem.Asset.PurchaseValue;
            GameController.Session.Player.AddItem(m_SelectedItem);
            m_State.DeductItem(ItemsShop.SelectedIndex);

            // deselect items in the lists
            ItemsShop.SelectedIndex = -1;
            ItemsPlayer.SelectedIndex = -1;

            ItemsShop.Items.Refresh();
            ItemsPlayer.Items.Refresh();
        }

        private void ButtonSell_Click(object sender, RoutedEventArgs e)
        {
            SetShopkeepText("sold");
            ButtonSell.IsEnabled = false;
            ButtonBuy.IsEnabled = false;

            // deselect items in the lists
            ItemsShop.SelectedIndex = -1;
            ItemsPlayer.SelectedIndex = -1;

            // give half the item's value
            GameController.Session.Player.Money += GetSalePrice(m_SelectedItem.Asset.PurchaseValue);

            // add the item to the shop's inventory
            m_State.AddItem(m_SelectedItem, 1);
            GameController.Session.Player.Inventory.Remove(m_SelectedItem);

            ItemsShop.Items.Refresh();
            ItemsPlayer.Items.Refresh();
        }

        private void SetShopkeepText(string key)
        {
            TextParser.SetContext("item", m_SelectedItem, false);
            ShopkeepText.Text = GameController.Content.GetAndParseString(m_State.StringGroup + key);
        }

        private static int GetSalePrice(int buyPrice)
        {
            return (buyPrice + 1) / 2;
        }

    }

}
