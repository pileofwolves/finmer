/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows;
using System.Windows.Controls;
using Finmer.Gameplay;
using Finmer.Utility;
using Finmer.Views.Base;

namespace Finmer
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public MainWindow()
        {
            InitializeComponent();

            // For the regular player experience, maximize the window. We avoid this in debug mode because it
            // can get in the way a little bit when rapidly iterating.
            if (!GameController.DebugMode && WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// Change the content of the carousel.
        /// </summary>
        /// <param name="target">The new root control to display in the carousel.</param>
        /// <param name="anim">The change-over animation style to use.</param>
        public void Navigate(Control target, ENavigatorAnimation anim)
        {
            MainWindowCarousel.Navigate(target, anim);
        }

        /// <summary>
        /// Push a new dialog popup onto the popup stack.
        /// </summary>
        public void OpenPopup(StackablePopupBase popup)
        {
            MainWindowPopupStack.Push(popup);
        }

    }

}
