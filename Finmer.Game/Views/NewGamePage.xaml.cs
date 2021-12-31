/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Finmer.Core;
using Finmer.Gameplay;
using Finmer.Models;
using Finmer.Utility;
using Finmer.Views.Base;
using JetBrains.Annotations;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for NewGamePage.xaml
    /// </summary>
    public partial class NewGamePage : INotifyPropertyChanged
    {

        private readonly PropertyBag m_Player;
        private CharCreateViewBase m_CurrentPage;
        private int m_Page = 1;

        public NewGamePage()
        {
            InitializeComponent();

            // Restore the last character the user created, or create a new one if there is none
            m_Player = UserConfig.NewGamePreset ?? new PropertyBag();

            // Initialize data binding
            DataContext = this;

            NewGameCarousel.NavigationComplete += (sender, args) =>
            {
                BackButton.IsHitTestVisible = true;
                NextButton.IsHitTestVisible = true;
            };
            GoPage(ENavigatorAnimation.Instant);
        }

        public bool CanGoNext => m_CurrentPage != null && m_CurrentPage.CanGoNext;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            m_Page++;
            GoPage(ENavigatorAnimation.SlideLeft);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            m_Page--;
            GoPage(ENavigatorAnimation.SlideRight);
        }

        private void GoPage(ENavigatorAnimation anim)
        {
            if (m_CurrentPage != null)
                m_CurrentPage.PropertyChanged -= CurrentPage_PropertyChanged;

            BackButton.IsHitTestVisible = false;
            NextButton.IsHitTestVisible = false;

            switch (m_Page)
            {
                case 0:
                    GameController.Window.Navigate(new TitlePage(), ENavigatorAnimation.SlideRight);
                    return;
                case 1:
                    m_CurrentPage = new CharCreateBasic();
                    break;
                case 2:
                    m_CurrentPage = new CharCreateSpecies();
                    break;
                case 3:
                    m_CurrentPage = new CharCreateAbility();
                    break;
                case 4:
                    DoBeginGame();
                    return;
                default:
                    return;
            }

            m_CurrentPage.InitialSaveData = m_Player;
            m_CurrentPage.PropertyChanged += CurrentPage_PropertyChanged;
            NewGameCarousel.Navigate(m_CurrentPage, anim, true);
        }

        private void CurrentPage_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName.Equals(nameof(CharCreateViewBase.CanGoNext)))
                OnPropertyChanged(nameof(CanGoNext));
        }

        private void DoBeginGame()
        {
            // Store this character in the user config, so the next time this menu is opened, the same settings are restored
            UserConfig.NewGamePreset = m_Player;

            // Initialize the savegame with basic defaults.
            // The scripts in content can make any other adjustments if needed (such as starting equipment).
            m_Player.SetInt(SaveData.k_Character_Level, 1);
            m_Player.SetInt(SaveData.k_Player_TimeDay, 1);
            m_Player.SetInt(SaveData.k_Player_TimeHour, 9);

            // Launch the session
            GameSnapshot save_data = new GameSnapshot(m_Player, new PropertyBag(), new PropertyBag());
            GameController.BeginNewSession(save_data);
            GameController.Window.Navigate(new MainPage(), ENavigatorAnimation.SlideLeft);
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
