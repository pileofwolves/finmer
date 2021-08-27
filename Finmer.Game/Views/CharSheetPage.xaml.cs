/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Finmer.Core.Assets;
using Finmer.Gameplay;
using Finmer.Utility;
using JetBrains.Annotations;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for CharSheetPage.xaml
    /// </summary>
    public partial class CharSheetPage : INotifyPropertyChanged
    {

        private ItemBoxView m_EquipSelBox;

        private int m_EquipSelIndex = -1;

        private ICommand m_IncreaseAbilityCmd;

        public CharSheetPage()
        {
            InitializeComponent();

            DataContext = GameController.Session.Player.GetOrCreateViewModel();

            // Populate the journal list
            // TODO: MVVMify this
            Journal journal = GameController.Session.Player.Journal;
            foreach (AssetJournal quest in journal.GetAllQuests())
            {
                int stage = journal.GetQuestStage(quest);
                string text_stage = quest.GetEntryForStage(stage);

                var view = new JournalListItemView();
                view.TextEntry.Text = text_stage;
                JournalList.Children.Add(view);
            }
        }

        public ICommand IncreaseAbilityCommand => m_IncreaseAbilityCmd ?? (m_IncreaseAbilityCmd = new RelayCommand(IncreaseAbilityLink_Click));

        public Visibility ShowIncreaseAbilityPanel => GameController.Session.Player.AbilityPoints > 0 ? Visibility.Visible : Visibility.Hidden;

        public Visibility ShowDigestingPreyPanel => GameController.Session.Player.StomachFullness > 0f ? Visibility.Visible : Visibility.Collapsed;
        public string DigestingPreyList => String.Join(", ", GameController.Session.Player.Stomach.Select(prey => prey.Name));

        public event PropertyChangedEventHandler PropertyChanged;

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            GameController.Window.Navigate(new MainPage(), ENavigatorAnimation.SlideRight);
        }

        private void ItemUseButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.Assert(ItemList.SelectedIndex >= 0 || m_EquipSelIndex >= 0);

            // deselect slot and update sheet
            Deselect();
        }

        private void ItemDropButton_Click(object sender, RoutedEventArgs e)
        {
            string name = GameController.Session.Player.Inventory[ItemList.SelectedIndex].Name;
            PopupDropText.Text = $"Are you sure you want to get rid of {name}?";
            DropConfirmButton.Content = "Drop " + name;
            PopupDropConfirm.PlacementTarget = ItemDropButton;
            PopupDropConfirm.IsOpen = true;
        }

        private void Deselect()
        {
            if (m_EquipSelIndex >= 0)
                m_EquipSelBox.IsEnabled = true;
            ItemList.SelectedIndex = -1;
            m_EquipSelIndex = -1;
            CheckItemButtons();

            // update listings
            ItemList.Items.Refresh();
        }

        private void CheckItemButtons()
        {
            // determine which item is selected
            Item item = null;
            if (ItemList.SelectedIndex >= 0)
                item = GameController.Session.Player.Inventory[ItemList.SelectedIndex];
            else if (m_EquipSelIndex >= 0)
                item = GameController.Session.Player.Equipment[m_EquipSelIndex];

            if (item == null)
            {
                ItemUseButton.IsEnabled = false;
                ItemDropButton.IsEnabled = false;
                return;
            }

            // can only drop inventory non-quest items
            ItemDropButton.IsEnabled = ItemList.SelectedIndex >= 0 && !item.Asset.IsQuestItem;

            // set up the Use button
            switch (item.Asset.ItemType)
            {
                case AssetItem.EItemType.Armor:
                case AssetItem.EItemType.Weapon:
                    ItemUseButton.IsEnabled = true;
                    ItemUseButton.Content = m_EquipSelIndex >= 0 ? "Unequip" : "Equip";
                    break;

                case AssetItem.EItemType.Usable:
                    ItemUseButton.IsEnabled = item.Asset.CanUseInField;
                    ItemUseButton.Content = item.Asset.CanUseInField ? "Use" : "Can't Use";
                    break;

                default:
                    ItemUseButton.IsEnabled = false;
                    ItemUseButton.Content = "Can't Use";
                    break;
            }
        }

        private void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemList.SelectedIndex >= 0 && m_EquipSelIndex >= 0)
            {
                m_EquipSelBox.IsEnabled = true;
                m_EquipSelIndex = -1;
            }

            CheckItemButtons();
        }

        private void ItemBoxView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // get slot index from tag
            int index = int.Parse((string)((ItemBoxView)sender).Tag);
            // cannot select empty equipment slots
            if (GameController.Session.Player.Equipment[index] == null)
                return;
            // deselect previous box
            if (m_EquipSelIndex >= 0)
                m_EquipSelBox.IsEnabled = true;
            // make the equip box selected
            m_EquipSelIndex = index;
            m_EquipSelBox = (ItemBoxView)sender;
            m_EquipSelBox.IsEnabled = false;
            ItemList.SelectedIndex = -1;
            CheckItemButtons();
        }

        private void DropCancelButton_Click(object sender, RoutedEventArgs e)
        {
            PopupDropConfirm.IsOpen = false;
        }

        private void DropConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            PopupDropConfirm.IsOpen = false;
            GameController.Session.Player.Inventory.RemoveAt(ItemList.SelectedIndex);
            Deselect();
        }

        private void IncreaseAbilityLink_Click(object args)
        {
            Player plr = GameController.Session.Player;
            int stat_idx = Int32.Parse((string)args);
            if (plr.AbilityPoints < 1) return;

            plr.AbilityPoints--;

            switch (stat_idx)
            {
                case 0:
                    plr.Strength++;
                    break;
                case 1:
                    plr.Agility++;
                    break;
                case 2:
                    plr.Body++;
                    break;
                case 3:
                    plr.Wits++;
                    break;
            }

            OnBindPropertyChanged(nameof(ShowIncreaseAbilityPanel));
        }

        [NotifyPropertyChangedInvocator]
        private void OnBindPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
