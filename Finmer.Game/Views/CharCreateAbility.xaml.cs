/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.ComponentModel;
using System.Linq;
using System.Windows;
using Finmer.Core;
using Finmer.Gameplay;
using Finmer.ViewModels;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for CharCreateAbility.xaml
    /// </summary>
    public partial class CharCreateAbility
    {

        public string PointsLeftText => $"Points Left: {Player.k_AbilityScorePointsAllowed - GetUsedPoints()}";

        public override bool CanGoNext => m_CanGoNext;

        private readonly AbilityPointCollectionViewModel m_Abilities;
        private bool m_CanGoNext;
        private bool m_Setup = true;

        public CharCreateAbility()
        {
            InitializeComponent();

            // This view does not have a viewmodel, so bind directly to the view
            DataContext = this;

            // Attach property changed event listeners to each of the ability score rows
            m_Abilities = (AbilityPointCollectionViewModel)FindResource("AbilityCollection");
            foreach (AbilityPointViewModel ability in m_Abilities)
                ability.PropertyChanged += Ability_PropertyChanged;
        }

        private void Ability_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (m_Setup || !e.PropertyName.Equals(nameof(AbilityPointViewModel.Value)))
                return;

            ValidateForm();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            m_Abilities[0].Reset(InitialSaveData.GetInt(SaveData.k_Character_Strength));
            m_Abilities[1].Reset(InitialSaveData.GetInt(SaveData.k_Character_Agility));
            m_Abilities[2].Reset(InitialSaveData.GetInt(SaveData.k_Character_Body));
            m_Abilities[3].Reset(InitialSaveData.GetInt(SaveData.k_Character_Wits));

            // In debug mode, roll random stats when the page opens, to allow quickly clicking through to the game
            if (GameController.DebugMode)
                ButtonRandomize_Click(sender, e);

            m_Setup = false;
            ValidateForm();
        }

        private void ValidateForm()
        {
            // Update the player save data with the new settings
            InitialSaveData.SetInt(SaveData.k_Character_Strength, m_Abilities[0].Value);
            InitialSaveData.SetInt(SaveData.k_Character_Agility, m_Abilities[1].Value);
            InitialSaveData.SetInt(SaveData.k_Character_Body, m_Abilities[2].Value);
            InitialSaveData.SetInt(SaveData.k_Character_Wits, m_Abilities[3].Value);

            // Update points total
            OnPropertyChanged(nameof(PointsLeftText));

            // If the player spent exactly the right number of points, they may proceed
            m_CanGoNext = GetUsedPoints() == Player.k_AbilityScorePointsAllowed;
            OnPropertyChanged(nameof(CanGoNext));
        }

        private void ButtonRandomize_Click(object sender, RoutedEventArgs e)
        {
            // Clear existing values
            foreach (AbilityPointViewModel ability in m_Abilities)
                ability.Reset();

            // Randomly distribute the points
            for (var i = 0; i < Player.k_AbilityScorePointsAllowed; i++)
            {
                int random_index = CoreUtility.Rng.Next(0, m_Abilities.Count);
                m_Abilities[random_index].Increment();
            }
        }

        private int GetUsedPoints()
        {
            // Design mode support
            if (m_Abilities == null)
                return 0;

            // Total the points used so far above the minimum value
            return m_Abilities.Sum(ability => ability.Value - Character.k_AbilityScoreMinimum);
        }

    }

}
