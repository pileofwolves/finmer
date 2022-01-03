/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows;
using System.Windows.Controls;
using Finmer.Core;
using Finmer.Gameplay;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for CharCreateBasic.xaml
    /// </summary>
    public partial class CharCreateBasic
    {

        private bool m_CanGoNext;

        public CharCreateBasic()
        {
            InitializeComponent();
        }

        public override bool CanGoNext => m_CanGoNext;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Copy data that was already cached in the save data
            NameInput.Text = InitialSaveData.GetString(SaveData.k_Object_Name);
            if (String.IsNullOrWhiteSpace(InitialSaveData.GetString(SaveData.k_Object_Name)))
            {
                // Randomly select one of the genders by default
                int gender_flip = CoreUtility.Rng.Next(2);
                GenderInputMale.IsChecked = gender_flip == 0;
                GenderInputFemale.IsChecked = !GenderInputMale.IsChecked;
            }
            else
            {
                GenderInputMale.IsChecked = InitialSaveData.GetString(SaveData.k_Object_Gender).Equals("Male");
                GenderInputFemale.IsChecked = !GenderInputMale.IsChecked;
            }

            // When debugging, apply default settings to enable quickly clicking through to the game
            if (GameController.DebugMode && String.IsNullOrWhiteSpace(NameInput.Text))
                NameInput.Text = "Snack";
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            InitialSaveData.SetString(SaveData.k_Object_Name, NameInput.Text);
            ValidateForm();
        }

        private void optGender_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton gender_option = (RadioButton)sender;

            // If this option was actually ticked (it might be toggled off instead) then update the selected gender
            if (gender_option.IsChecked ?? false)
                InitialSaveData.SetString(SaveData.k_Object_Gender, (string)gender_option.Tag);

            ValidateForm();
        }

        private void ValidateForm()
        {
            m_CanGoNext =
                !String.IsNullOrWhiteSpace(InitialSaveData.GetString(SaveData.k_Object_Name)) &&
                !String.IsNullOrWhiteSpace(InitialSaveData.GetString(SaveData.k_Object_Gender));
            OnPropertyChanged(nameof(CanGoNext));
        }

    }

}
