/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows;
using System.Windows.Controls;
using Finmer.Gameplay;
using Finmer.Utility;

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

        internal void Navigate(Control target, ENavigatorAnimation anim)
        {
            MainWindowCarousel.Navigate(target, anim);
        }

    }

}
