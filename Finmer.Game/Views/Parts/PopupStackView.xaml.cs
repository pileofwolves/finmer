/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows;
using Finmer.ViewModels;
using Finmer.Views.Base;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for PopupStackView.xaml
    /// </summary>
    public partial class PopupStackView
    {

        /// <summary>
        /// Routed event for a popup that has been dismissed by the user.
        /// </summary>
        public static readonly RoutedEvent PopupClosingEvent = EventManager.RegisterRoutedEvent(
            "PopupClosing", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PopupStackView));

        public PopupStackView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Push a new popup onto the stack.
        /// </summary>
        public void Push(StackablePopupBase popup)
        {
            popup.Host = this;

            var context = (PopupStackViewModel)DataContext;
            context.Elements.Add(popup);
        }

        /// <summary>
        /// Remove a specific popup from the stack.
        /// </summary>
        public void Remove(StackablePopupBase popup)
        {
            popup.IsHitTestVisible = false;
            popup.Host = null;

            popup.RaiseEvent(new RoutedEventArgs(PopupClosingEvent, popup));
        }

        private void FadeOutAnimation_OnCompleted(object sender, EventArgs e)
        {
            // Popup fade-out animation is done, so we can now remove the item from the visual tree entirely.
            // A bit of a cheeky hack: this callback receives no context for which dialog actually finished animating out,
            // since the 'sender' argument is an internal animation clock that has no direct bearing on the actual popup.
            // So we resort to some trickery here to just delete all dialogs that are animating out - it's good enough.
            var elements = ((PopupStackViewModel)DataContext).Elements;
            for (int i = elements.Count - 1; i >= 0; i--)
                if (elements[i].Host == null)
                    elements.RemoveAt(i);
        }

    }

}
