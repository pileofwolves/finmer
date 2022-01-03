/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows;
using Finmer.Gameplay;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for ItemBoxView.xaml
    /// </summary>
    public partial class ItemEquipmentBoxView
    {

        /// <summary>
        /// Dependency property for DisplayedItem.
        /// </summary>
        public static readonly DependencyProperty DisplayedItemProperty = DependencyProperty.Register(
            "DisplayedItem", typeof(Item), typeof(ItemEquipmentBoxView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property for EquipmentIndex.
        /// </summary>
        public static readonly DependencyProperty EquipmentIndexProperty = DependencyProperty.Register(
            "EquipmentIndex", typeof(int), typeof(ItemEquipmentBoxView), new PropertyMetadata(0));

        /// <summary>
        /// Indicates whether the user has activated this UI element.
        /// </summary>
        public Item DisplayedItem
        {
            get => (Item)GetValue(DisplayedItemProperty);
            set => SetValue(DisplayedItemProperty, value);
        }

        /// <summary>
        /// Indicates whether the user has activated this UI element.
        /// </summary>
        public int EquipmentIndex
        {
            get => (int)GetValue(EquipmentIndexProperty);
            set => SetValue(EquipmentIndexProperty, value);
        }

        public ItemEquipmentBoxView()
        {
            InitializeComponent();
        }

        private void OpenActionsButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the actions popup if there is actually an item in this box
            if (DisplayedItem != null)
                EquipmentActionsPopup.IsOpen = true;
        }

        private void UnequipButton_Click(object sender, RoutedEventArgs e)
        {
            // Ensure the popup closes
            EquipmentActionsPopup.IsOpen = false;
        }

    }

}
