/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows;
using System.Windows.Controls;
using Finmer.Gameplay;
using Finmer.Models;
using Finmer.Utility;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for SaveGamePage.xaml
    /// </summary>
    public partial class SaveGamePage
    {

        public SaveGamePage()
        {
            InitializeComponent();

            btnSlot1.Click += (sender, args) => SaveGamePrompt(btnSlot1, 0);
            btnSlot2.Click += (sender, args) => SaveGamePrompt(btnSlot2, 1);
            btnSlot3.Click += (sender, args) => SaveGamePrompt(btnSlot3, 2);
            btnSlot1.Content = SaveManager.GetSaveInfo(0).Info;
            btnSlot2.Content = SaveManager.GetSaveInfo(1).Info;
            btnSlot3.Content = SaveManager.GetSaveInfo(2).Info;
        }

        private void SaveGamePrompt(Button context, int slot)
        {
            if (SaveManager.GetSaveInfo(slot).IsLoadable)
            {
                PopupOverwriteConfirm.PlacementTarget = context;
                PopupOverwriteConfirm.Tag = slot;
                PopupOverwriteConfirm.IsOpen = true;
                return;
            }

            SaveGameNow(slot);
        }

        private void SaveGameNow(int slot)
        {
            SaveManager.Save(slot, GameController.Session.Player.SerializeProperties());

            btnSlot1.IsEnabled = false;
            btnSlot2.IsEnabled = false;
            btnSlot3.IsEnabled = false;

            GameUI.Instance.Log("Game saved!", Theme.LogColorLightGray);
            GameController.Window.Navigate(new MainPage(), ENavigatorAnimation.SlideRight);
            GameController.Session.ResumeScript();
        }

        private void OverwriteConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            PopupOverwriteConfirm.IsOpen = false;
            SaveGameNow((int) PopupOverwriteConfirm.Tag); // hack, oh well :(
        }

        private void OverwriteCancelButton_Click(object sender, RoutedEventArgs e)
        {
            PopupOverwriteConfirm.IsOpen = false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            GameController.Window.Navigate(new MainPage(), ENavigatorAnimation.SlideRight);
            GameController.Session.ResumeScript();
        }

    }

}
