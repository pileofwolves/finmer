/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Finmer.Core.Assets;
using Finmer.Gameplay;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for CharCreateScene.xaml
    /// </summary>
    public partial class CharCreateScene
    {

        /// <summary>
        /// Represents details about a game start location that is relevant for the view.
        /// </summary>
        public class GameStartInfo
        {

            /// <summary>
            /// User-friendly description of the game start.
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// Author of the module the game start is contained in.
            /// </summary>
            public string Author { get; set; }

            /// <summary>
            /// Asset ID of the scene represented by this game start.
            /// </summary>
            public Guid Scene { get; set; }

        }

        /// <summary>
        /// Collection of game start locations in loaded modules.
        /// </summary>
        public static List<GameStartInfo> ValidStarts { get; } = new List<GameStartInfo>();

        private Guid m_SelectedScene = Guid.Empty;

        public CharCreateScene()
        {
            InitializeComponent();

            // Lazy-initialize the starts list on first usage
            if (ValidStarts.Count == 0)
            {
                foreach (var scene in GameController.Content.GetAssetsByType<AssetScene>())
                {
                    // Only interested in scenes that are marked as game starts
                    if (!scene.IsGameStart)
                        continue;

                    // Collect UI-relevant info about this game start
                    ValidStarts.Add(new GameStartInfo
                    {
                        Title = scene.GameStartDescription,
                        Author = scene.Module.Author,
                        Scene = scene.ID
                    });
                }
            }
        }

        public override bool CanGoNext => m_SelectedScene != Guid.Empty;

        private void Page_OnLoaded(object sender, RoutedEventArgs e)
        {
            // Check if the user previously selected a scene
            byte[] scene_id_bytes = InitialSaveData.GetBytes(SaveData.k_System_StartSceneID);
            if (scene_id_bytes != null)
            {
                // If this scene is the one that was previously selected, restore the selection
                var saved_scene_id = new Guid(scene_id_bytes);
                var start_index = ValidStarts.FindIndex(start => start.Scene == saved_scene_id);
                if (start_index != -1)
                    GameStartList.SelectedIndex = start_index;
            }

            // Update Next button
            OnPropertyChanged(nameof(CanGoNext));
        }

        private void GameStartList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GameStartList.SelectedIndex == -1)
            {
                // If the scene was deselected, disable the Next button
                m_SelectedScene = Guid.Empty;
            }
            else
            {
                // A scene was selected; record the user's choice
                var scene_id = ValidStarts[GameStartList.SelectedIndex].Scene;
                m_SelectedScene = scene_id;
                InitialSaveData.SetBytes(SaveData.k_System_StartSceneID, scene_id.ToByteArray());
            }

            // Update Next button
            OnPropertyChanged(nameof(CanGoNext));
        }

    }

}
