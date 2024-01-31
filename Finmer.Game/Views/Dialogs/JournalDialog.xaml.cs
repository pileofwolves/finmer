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
    /// Interaction logic for JournalDialog.xaml
    /// </summary>
    public partial class JournalDialog
    {

        public JournalDialog()
        {
            InitializeComponent();

            // Populate the data context
            var player = GameController.Session.Player;
            DataContext = new CharacterSheetViewModel(player);
        }

    }

}
