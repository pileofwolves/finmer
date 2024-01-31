/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Gameplay;
using Finmer.ViewModels;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for ShopDialog.xaml
    /// </summary>
    public partial class ShopDialog
    {

        public ShopDialog(ShopState state)
        {
            InitializeComponent();

            // Configure view model for data binding
            DataContext = new ShopViewModel(state, GameController.Session.Player);
        }

    }

}
