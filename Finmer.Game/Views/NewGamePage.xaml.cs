/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Finmer.Core;
using Finmer.Core.Assets;
using Finmer.Gameplay;
using Finmer.Models;
using Finmer.Utility;
using Finmer.ViewModels;
using Finmer.Views.Base;
using JetBrains.Annotations;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for NewGamePage.xaml
    /// </summary>
    public partial class NewGamePage : INotifyPropertyChanged
    {

        private readonly PropertyBag m_InitialSaveData;
        private readonly List<CharCreateViewBase> m_Pages = new List<CharCreateViewBase>(4);

        private CharCreateViewBase m_CurrentPage;
        private int m_CurrentPageIndex;

        public NewGamePage()
        {
            InitializeComponent();

            // Restore the last character the user created, or create a new one if there is none
            m_InitialSaveData = (PropertyBag)UserConfig.NewGamePreset?.Clone() ?? new PropertyBag();

            // Set up the page list
            m_Pages.Add(new CharCreateBasic());
            m_Pages.Add(new CharCreateSpecies());
            m_Pages.Add(new CharCreateAbility());

            // Optionally, mods may add additional game starts. If there are any, we let the user choose; if not, grab the first one.
            if (GameController.Content.GameStartCount > 1)
                m_Pages.Add(new CharCreateScene());
            else
                m_InitialSaveData.SetBytes(SaveData.k_System_StartSceneID, GetDefaultGameStart().ToByteArray());

            // Initialize data binding
            DataContext = this;

            // Whenever navigation completes, re-enable the navigation buttons
            NewGameCarousel.NavigationComplete += (sender, args) =>
            {
                BackButton.IsHitTestVisible = true;
                NextButton.IsHitTestVisible = true;
            };

            // Navigate to the first page
            GoPage(ENavigatorAnimation.Instant);
        }

        public bool CanGoNext => m_CurrentPage != null && m_CurrentPage.CanGoNext;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            m_CurrentPageIndex++;
            GoPage(ENavigatorAnimation.SlideLeft);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            m_CurrentPageIndex--;
            GoPage(ENavigatorAnimation.SlideRight);
        }

        private void GoPage(ENavigatorAnimation anim)
        {
            // Unsubscribe property event handler
            if (m_CurrentPage != null)
                PropertyChangedEventManager.RemoveHandler(m_CurrentPage, CurrentPage_PropertyChanged, nameof(CharCreateViewBase.CanGoNext));

            // Disable user input
            BackButton.IsHitTestVisible = false;
            NextButton.IsHitTestVisible = false;

            // If the user backed out all the way, return to main menu
            if (m_CurrentPageIndex < 0)
            {
                GameController.Window.Navigate(new TitlePage(), ENavigatorAnimation.SlideRight);
                return;
            }

            // If the user completed all pages, begin the game
            if (m_CurrentPageIndex >= m_Pages.Count)
            {
                DoBeginGame();
                return;
            }

            // Initialize next page
            m_CurrentPage = m_Pages[m_CurrentPageIndex];
            m_CurrentPage.InitialSaveData = m_InitialSaveData;
            m_CurrentPage.TotalPages = m_Pages.Count;
            PropertyChangedEventManager.AddHandler(m_CurrentPage, CurrentPage_PropertyChanged, nameof(CharCreateViewBase.CanGoNext));

            // Navigate to next page
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
            UserConfig.NewGamePreset = (PropertyBag)m_InitialSaveData.Clone();

            // Initialize new save data and start the game
            GameSnapshot save_data = GameSnapshot.FromInitialSaveData(m_InitialSaveData);
            NavigationUtilities.BeginSessionAndNavigate(save_data);
        }

        private static Guid GetDefaultGameStart()
        {
            return GameController.Content.GetAssetsByType<AssetScene>().First(scene => scene.IsGameStart).ID;
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
