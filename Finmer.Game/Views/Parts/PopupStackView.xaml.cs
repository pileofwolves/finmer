/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using Finmer.ViewModels;
using Finmer.Views.Base;
using JetBrains.Annotations;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for PopupStackView.xaml
    /// </summary>
    public partial class PopupStackView : INotifyPropertyChanged
    {

        /// <summary>
        /// Routed event for a popup that has been dismissed by the user.
        /// </summary>
        public static readonly RoutedEvent PopupClosingEvent = EventManager.RegisterRoutedEvent(
            "PopupClosing", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PopupStackView));

        /// <summary>
        /// Indicates whether the stack contains a non-zero number of open popups.
        /// </summary>
        public bool HasAnyOpenPopups => ((PopupStackViewModel)DataContext).HasAnyOpenPopups;

        /// <summary>
        /// Wrapper accessor for view model.
        /// </summary>
        private PopupStackViewModel ViewModel => (PopupStackViewModel)DataContext;

        public PopupStackView()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Push a new popup onto the stack.
        /// </summary>
        public void Push(StackablePopupBase popup)
        {
            // Tell popup it is being displayed by this view
            Debug.Assert(popup.Host == null);
            popup.Host = this;

            // Display the popup
            ViewModel.Elements.Add(popup);
        }

        /// <summary>
        /// Remove a specific popup from the stack.
        /// </summary>
        public void Remove(StackablePopupBase popup)
        {
            popup.IsHitTestVisible = false;
            popup.Host = null;

            popup.RaiseEvent(new RoutedEventArgs(PopupClosingEvent, popup));

            ViewModel.OnPopupClosing();
        }

        private void FadeOutAnimation_OnCompleted(object sender, EventArgs e)
        {
            // Popup fade-out animation is done, so we can now remove the item from the visual tree entirely.
            // A bit of a cheeky hack: this callback receives no context for which dialog actually finished animating out,
            // since the 'sender' argument is an internal animation clock that has no direct bearing on the actual popup.
            // So we resort to some trickery here to just delete all dialogs that are animating out - it's good enough.
            ObservableCollection<StackablePopupBase> elements = ViewModel.Elements;
            for (int i = elements.Count - 1; i >= 0; i--)
                if (elements[i].Host == null)
                    elements.RemoveAt(i);
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PopupStackView_OnLoaded(object sender, RoutedEventArgs e)
        {
            PropertyChangedEventManager.AddHandler(ViewModel, PopupStackViewModel_OnPropertyChanged,
                nameof(PopupStackViewModel.HasAnyOpenPopups));
        }

        private void PopupStackView_OnUnloaded(object sender, RoutedEventArgs e)
        {
            PropertyChangedEventManager.RemoveHandler(ViewModel, PopupStackViewModel_OnPropertyChanged,
                nameof(PopupStackViewModel.HasAnyOpenPopups));
        }

        private void PopupStackViewModel_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasAnyOpenPopups));
        }
    }

}
