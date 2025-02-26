/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents serialized save data that can be reconstructed into a GameSession instance.
    /// </summary>
    public sealed class GameSnapshot
    {

        /// <summary>
        /// Snapshot of the player and world state.
        /// </summary>
        public PropertyBag PlayerData { get; }

        /// <summary>
        /// Data to be used for restoring the scene system.
        /// </summary>
        public PropertyBag SceneData { get; }

        /// <summary>
        /// Snapshot of user interface state.
        /// </summary>
        public PropertyBag InterfaceData { get; }

        public GameSnapshot(PropertyBag player, PropertyBag scene, PropertyBag ui)
        {
            PlayerData = player;
            SceneData = scene;
            InterfaceData = ui;
        }

        /// <summary>
        /// Initialize a new GameSnapshot using initial save data from the character creator.
        /// </summary>
        public static GameSnapshot FromInitialSaveData(PropertyBag initial_save)
        {
            // Initialize the save data with basic defaults.
            // The scripts in content can make any other adjustments if needed (such as starting equipment).
            initial_save.SetInt(SaveData.k_Character_Level, 1);
            initial_save.SetInt(SaveData.k_Player_TimeDay, 1);
            initial_save.SetInt(SaveData.k_Player_TimeHour, 9);

            // Move the start scene ID to a separate PropertyBag for scene data
            var scene_data = new PropertyBag();
            scene_data.SetBytes(SaveData.k_System_CurrentSceneID, initial_save.GetBytes(SaveData.k_System_StartSceneID));

            // Generate the session
            return new GameSnapshot(initial_save, scene_data, new PropertyBag());
        }

        /// <summary>
        /// Validates that this snapshot can be loaded. If a problem is found that would prevent the snapshot from being loaded, an exception is thrown.
        /// </summary>
        /// <exception cref="InvalidSaveDataException">Throws if validation did not pass.</exception>
        public void Validate()
        {
            var scene_guid_bytes = SceneData.GetBytes(SaveData.k_System_CurrentSceneID);
            if (scene_guid_bytes == null || scene_guid_bytes.Length != 16)
                throw new InvalidSaveDataException("Current scene ID missing from scene properties");

            var scene_guid = new Guid(scene_guid_bytes);
            var scene = GameController.Content.GetAssetByID(scene_guid);
            if (scene == null)
                throw new InvalidSaveDataException($"Current scene ID {scene_guid} is not a known Scene asset");
        }

    }

}
