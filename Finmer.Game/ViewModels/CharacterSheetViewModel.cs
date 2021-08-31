﻿/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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

        public ICommand IncreaseAbilityCommand => m_IncreaseAbilityCommand ?? (m_IncreaseAbilityCommand = new RelayCommand(SpendAbilityPoint));

        public Visibility AbilityPointVisibility => AbilityPoints > 0 ? Visibility.Visible : Visibility.Hidden;

        public Visibility PreyVisibility => Player.StomachFullness > 0f ? Visibility.Visible : Visibility.Collapsed;

        public string Name => Player.Name;

        public string Species => Player.Species;

        public int Strength => Player.Strength;

        public int Agility => Player.Agility;

        public int Body => Player.Body;

        public int Wits => Player.Wits;

        public int Level => Player.Level;

        public int XP => Player.XP;

        public int RequiredXP => Player.RequiredXP;

        public int Money => Player.Money;

        public int AbilityPoints => Player.AbilityPoints;

        public string PreyList => String.Join(", ", Player.Stomach.Select(prey => prey.Name));

        public ObservableCollection<Item> Inventory { get; }

        public Item[] Equipment => Player.Equipment;

        private ICommand m_UseItemCommand;
        private ICommand m_DropItemCommand;
        private ICommand m_IncreaseAbilityCommand;
        private JournalViewModel m_JournalViewModel;

        public CharacterSheetViewModel(Player player)
        {
            Player = player;
            Inventory = new ObservableCollection<Item>(Player.Inventory);
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

            // TODO: Invoke item use script / swap equipment
            Debug.WriteLine(item);
        }

        private void DropItem(object arg)
        {
            // Remove the item from the character sheet. This will notify the view to update.
            Item item = (Item)arg;
            Inventory.Remove(item);

            // Remove the item from the real player inventory. This should be safe to do since modifications are made 1:1.
            Player.Inventory.Remove(item);
        }

    }

}