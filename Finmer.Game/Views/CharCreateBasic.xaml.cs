/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
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
        private bool m_Setup = true;

        public CharCreateBasic()
        {
            InitializeComponent();
        }

        public override bool CanGoNext => m_CanGoNext;

        private void CharCreateViewBase_Loaded(object sender, RoutedEventArgs e)
        {
            txtName.Text = InitialSaveData.GetString("name");
            cmbSpecies.Text = InitialSaveData.GetString("species");
            //check if there was some initial value saved in the gender field, if not we go without anything checked.
            if (!String.IsNullOrWhiteSpace(InitialSaveData.GetString("gender")))
            {
                optGenderMale.IsChecked = InitialSaveData.GetString("gender").Equals("Male");
                optGenderFemale.IsChecked = !optGenderMale.IsChecked;
            }
            m_Setup = false;

            // Apply some default settings to enable quickly clicking through to the game
            //if (GameController.DebugMode)
            {
                txtName.Text = "Snack";
                cmbSpecies.SelectedIndex = CoreUtility.Rng.Next(cmbSpecies.Items.Count);
                ValidateForm();
            }
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (m_Setup)
                return;

            InitialSaveData.SetString("name", txtName.Text);
            ValidateForm();
        }

        private void cmbSpecies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (m_Setup) 
                return;

            InitialSaveData.SetString("species", (string)cmbSpecies.SelectedValue);
            ValidateForm();
        }

        private void optGender_Checked(object sender, RoutedEventArgs e)
        {
            if (m_Setup) 
                return;

            InitialSaveData.SetString("gender", (optGenderMale.IsChecked ?? false) ? "Male" : "Female");
            ValidateForm();
        }

        private void ValidateForm()
        {
            //add additional check to make sure radio button has been selected.
            m_CanGoNext =
                !String.IsNullOrWhiteSpace(InitialSaveData.GetString("name")) && 
                !String.IsNullOrWhiteSpace(InitialSaveData.GetString("species")) &&
                !String.IsNullOrWhiteSpace(InitialSaveData.GetString("gender"));
            OnPropertyChanged(nameof(CanGoNext));
        }

    }

}
