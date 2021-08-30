/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Diagnostics;
using System.Windows;
using Finmer.Gameplay;
using Finmer.Utility;
using Finmer.ViewModels;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for CharSheetPage.xaml
    /// </summary>
    public partial class CharSheetPage
    {

        private readonly CharacterSheetViewModel m_ViewModel;

        public CharSheetPage()
        {
            InitializeComponent();

            // Populate the main view
            var player = GameController.Session.Player;
            m_ViewModel = new CharacterSheetViewModel(player);
            DataContext = m_ViewModel;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            GameController.Window.Navigate(new MainPage(), ENavigatorAnimation.SlideRight);
        }

        private void EquipmentBox_SelectedChanged(object sender, RoutedEventArgs e)
        {
            ItemBoxView box = sender as ItemBoxView;
            if (box == null || !box.IsSelected)
                return;

            // Update which box is currently selected
            Debug.WriteLine(box.DisplayedItem?.Asset.ObjectName ?? "Nothing");
        }

    }

}
