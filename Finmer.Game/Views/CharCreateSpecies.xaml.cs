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
using Finmer.ViewModels;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for CharCreateSpecies.xaml
    /// </summary>
    public partial class CharCreateSpecies
    {

        private bool m_CanGoNext;

        public CharCreateSpecies()
        {
            InitializeComponent();
        }

        public override bool CanGoNext => m_CanGoNext;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Populate the form
            if (String.IsNullOrWhiteSpace(InitialSaveData.GetString(SaveData.k_Player_SpeciesSingular)))
            {
                // If no species has been selected yet, pick a random one
                if (GameController.DebugMode)
                    PresetInput.SelectedIndex = CoreUtility.Rng.Next(PresetInput.Items.Count);
            }
            else
            {
                // Otherwise, restore from save data
                SpeciesSingularInput.Text = InitialSaveData.GetString(SaveData.k_Player_SpeciesSingular);
                SpeciesPluralInput.Text = InitialSaveData.GetString(SaveData.k_Player_SpeciesPlural);
                CoatNounInput.Text = InitialSaveData.GetString(SaveData.k_Player_SpeciesCoatNoun);
                CoatAdjectiveInput.Text = InitialSaveData.GetString(SaveData.k_Player_SpeciesCoatAdj);
            }

            ValidateForm();
        }

        private void ValidateForm()
        {
            m_CanGoNext =
                !String.IsNullOrWhiteSpace(InitialSaveData.GetString(SaveData.k_Player_SpeciesSingular)) &&
                !String.IsNullOrWhiteSpace(InitialSaveData.GetString(SaveData.k_Player_SpeciesPlural)) &&
                !String.IsNullOrWhiteSpace(InitialSaveData.GetString(SaveData.k_Player_SpeciesCoatNoun)) &&
                !String.IsNullOrWhiteSpace(InitialSaveData.GetString(SaveData.k_Player_SpeciesCoatAdj));
            OnPropertyChanged(nameof(CanGoNext));
        }

        private void Preset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected preset
            var combobox = (ComboBox)e.Source;
            var species = combobox.SelectedItem as SpeciesPresetViewModel;
            if (species == null)
                return;

            // Apply the preset
            SpeciesSingularInput.Text = species.SingularNoun;
            SpeciesPluralInput.Text = species.PluralNoun;
            CoatNounInput.Text = species.CoatNoun;
            CoatAdjectiveInput.Text = species.CoatAdjective;

            // Update validation status
            ValidateForm();
        }

        private void SpeciesSingularInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            InitialSaveData.SetString(SaveData.k_Player_SpeciesSingular, SpeciesSingularInput.Text);
            ValidateForm();
        }

        private void SpeciesPluralInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            InitialSaveData.SetString(SaveData.k_Player_SpeciesPlural, SpeciesPluralInput.Text);
            ValidateForm();
        }

        private void CoatNounInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            InitialSaveData.SetString(SaveData.k_Player_SpeciesCoatNoun, CoatNounInput.Text);
            ValidateForm();
        }

        private void CoatAdjectiveInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            InitialSaveData.SetString(SaveData.k_Player_SpeciesCoatAdj, CoatAdjectiveInput.Text);
            ValidateForm();
        }

    }

}
