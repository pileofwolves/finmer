/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Finmer.Core.Assets;
using Finmer.Gameplay;
using Finmer.Utility;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model for the character sheet page.
    /// </summary>
    internal sealed class CharacterSheetViewModel : BaseProp
    {

        private Player Player { get; }

        public JournalViewModel JournalViewModel => m_JournalViewModel ?? (m_JournalViewModel = new JournalViewModel(Player.Journal));

        public ICommand UseItemCommand => m_UseItemCommand ?? (m_UseItemCommand = new RelayCommand(UseItem));

        public ICommand DropItemCommand => m_DropItemCommand ?? (m_DropItemCommand = new RelayCommand(DropItem));

        public ICommand UnequipItemCommand => m_UnequipItemCommand ?? (m_UnequipItemCommand = new RelayCommand(UnequipItem));

        public ICommand IncreaseAbilityCommand => m_IncreaseAbilityCommand ?? (m_IncreaseAbilityCommand = new RelayCommand(SpendAbilityPoint));

        public Visibility AbilityPointVisibility => AbilityPoints > 0 ? Visibility.Visible : Visibility.Hidden;

        public string Name => Player.Name;

        public string Species => Player.Species;

        public int Strength => Player.Strength;

        public int Agility => Player.Agility;

        public int Body => Player.Body;

        public int Wits => Player.Wits;

        public int NumAttackDice => Player.NumAttackDice;

        public int NumDefenseDice => Player.NumDefenseDice;

        public int Level => Player.Level;

        public int XP => Player.XP;

        public int RequiredXP => Player.RequiredXP;

        public int Money => Player.Money;

        public int AbilityPoints => Player.AbilityPoints;

        public IReadOnlyCollection<Item> Inventory { get; private set; }

        public Item[] Equipment => Player.Equipment;

        private ICommand m_UseItemCommand;
        private ICommand m_DropItemCommand;
        private ICommand m_UnequipItemCommand;
        private ICommand m_IncreaseAbilityCommand;
        private JournalViewModel m_JournalViewModel;

        public CharacterSheetViewModel(Player player)
        {
            Player = player;

            RefreshInventory();
        }

        private void SpendAbilityPoint(object args)
        {
            int stat_index = Int32.Parse((string)args);

            // Validate that there is actually an ability point to spend
            if (Player.AbilityPoints < 1)
                return;

            // Take away the point
            Player.AbilityPoints--;

            // Increase the stat of choice
            switch (stat_index)
            {
                case 0:     Player.Strength++;      OnPropertyChanged(nameof(Strength));        break;
                case 1:     Player.Agility++;       OnPropertyChanged(nameof(Agility));         break;
                case 2:     Player.Body++;          OnPropertyChanged(nameof(Body));            break;
                case 3:     Player.Wits++;          OnPropertyChanged(nameof(Wits));            break;
            }

            OnPropertyChanged(nameof(AbilityPoints));
            OnPropertyChanged(nameof(AbilityPointVisibility));
        }

        private void UseItem(object arg)
        {
            Item item = (Item)arg;

            // Apply this item
            switch (item.Asset.ItemType)
            {
                case AssetItem.EItemType.Equipable:
                    // Equip the item, potentially swapping it with another
                    ItemUtilities.EquipItem(Player, item);
                    OnPropertyChanged(nameof(Equipment));
                    OnPropertyChanged(nameof(NumAttackDice));
                    OnPropertyChanged(nameof(NumDefenseDice));
                    break;

                case AssetItem.EItemType.Usable:
                    // Use and consume the item
                    ItemUtilities.UseItem(GameController.Session, item);
                    break;

                default:
                    throw new ArgumentException("Item cannot be used from the character sheet", nameof(arg));
            }

            // Refresh the cached copy for the view
            RefreshInventory();
        }

        private void DropItem(object arg)
        {
            // Remove the item from the character sheet
            Item item = (Item)arg;
            Player.Inventory.Remove(item);

            // Refresh the cached copy for the view
            RefreshInventory();
        }

        private void UnequipItem(object arg)
        {
            // Find the equipped item; validate that the slot actually contains something as a failsafe
            int equipment_index = (int)arg;
            Item equipped = Equipment[equipment_index];
            if (equipped == null)
                return;

            // Unequip it
            ItemUtilities.UnequipItem(Player, equipment_index);

            // Make sure to update the equipment readouts
            OnPropertyChanged(nameof(Equipment));
            OnPropertyChanged(nameof(NumAttackDice));
            OnPropertyChanged(nameof(NumDefenseDice));

            // Refresh the cached copy for the view
            RefreshInventory();
        }

        private void RefreshInventory()
        {
            // ListBox will not update the items collection if the collection changes but the collection itself remains reference-equal.
            // So instead, we'll just allocate a new container entirely, copying the player's inventory into it.
            Inventory = new List<Item>(Player.Inventory);
            OnPropertyChanged(nameof(Inventory));
        }

    }

}
