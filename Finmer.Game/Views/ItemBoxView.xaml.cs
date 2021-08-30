/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows;
using System.Windows.Input;
using Finmer.Gameplay;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for ItemBoxView.xaml
    /// </summary>
    public partial class ItemBoxView
    {

        /// <summary>
        /// Currently selected item box.
        /// </summary>
        /// <remarks>
        /// Note that this is a static because that's a much simpler solution than implementing some kind of overarching viewmodel for
        /// simulating radio buttons etc. Also, it's a weak reference to ensure that the field does not keep closed UI trees alive.
        /// </remarks>
        private static readonly WeakReference<ItemBoxView> s_Selected = new WeakReference<ItemBoxView>(null);

        /// <summary>
        /// Dependency property for DisplayedItem.
        /// </summary>
        public static readonly DependencyProperty DisplayedItemProperty = DependencyProperty.Register(
            "DisplayedItem", typeof(Item), typeof(ItemBoxView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property for IsSelected.
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected", typeof(bool), typeof(ItemBoxView), new PropertyMetadata(false));

        /// <summary>
        /// Routed event for SelectedChanged.
        /// </summary>
        public static readonly RoutedEvent SelectedChangedEvent = EventManager.RegisterRoutedEvent(
            "SelectedChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(ItemBoxView));

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
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public ItemBoxView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fired when the selection status of this UI element changes.
        /// </summary>
        public event RoutedEventHandler SelectedChanged
        {
            add => AddHandler(SelectedChangedEvent, value);
            remove => RemoveHandler(SelectedChangedEvent, value);
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            // Deselect the previous selected box, if any.
            if (s_Selected.TryGetTarget(out ItemBoxView previous))
            {
                previous.IsSelected = false;
                previous.RaiseEvent(new RoutedEventArgs(SelectedChangedEvent, previous));
            }

            // Can only select a box if it has an item in it
            if (DisplayedItem == null)
                return;

            // Assign the new box
            s_Selected.SetTarget(this);
            IsSelected = true;

            // Trigger event
            RaiseEvent(new RoutedEventArgs(SelectedChangedEvent, this));
        }

    }

}
