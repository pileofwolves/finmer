/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.IO;
using System.Linq;
using System.Text;
using Finmer.Core;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Identifies a slot for storing save data.
    /// </summary>
    public enum ESaveSlot : byte
    {
        Checkpoint = 0,
        Manual1 = 1,
        Manual2 = 2,
        Manual3 = 3,
        _Count
    }

    /// <summary>
    /// Manages the saving and loading of save files.
    /// </summary>
    public static class SaveManager
    {

        private const int k_SlotCount = (int)ESaveSlot._Count;

        private static readonly SlotInfo[] s_Slots = new SlotInfo[k_SlotCount];
        private static readonly Guid k_CoreModuleID = new Guid("edcf99d2-6ced-40fa-87e9-86cda5e570ee");

        /// <summary>
        /// Indicates whether the game instance includes unofficial content.
        /// </summary>
        public static bool IsModdedGame =>
            // The game cannot start without at least one module loaded, so if any of them isn't Core, there are mods present
            GameController.LoadedModules.Any(meta => meta.ID != k_CoreModuleID);

        /// <summary>
        /// Refresh the cached save slot info by reading save data headers from disk.
        /// </summary>
        public static void RefreshSlots()
        {
            for (var index = 0; index < k_SlotCount; index++)
                s_Slots[index] = ReadSlotInfo((ESaveSlot)index);
        }

        /// <summary>
        /// Returns the save data stored in the specified slot.
        /// </summary>
        /// <param name="slot">The save slot index.</param>
        /// <exception cref="IOException">Throws if a filesystem error occurs.</exception>
        /// <exception cref="InvalidSaveDataException">Throws if save file is unreadable.</exception>
        public static GameSnapshot ReadSnapshot(ESaveSlot slot)
        {
            string filename = GetSlotFileName(slot);
            using (var file = new FileStream(filename, FileMode.Open))
            {
                using (var reader = new BinaryReader(file, Encoding.UTF8, true))
                {
                    // Verify version
                    byte version = reader.ReadByte();
                    if (version != SaveData.k_FileVersion)
                        throw new InvalidSaveDataException("Incompatible save version " + version);

                    // Skip the header, which will have been validated by ReadSlotInfo().
                    reader.ReadString();
                    int num_modules = reader.ReadInt32();
                    reader.ReadBytes(num_modules * 16);

                    // Deserialize stream
                    PropertyBag player_data = PropertyBag.FromStream(reader);
                    PropertyBag scene_data = PropertyBag.FromStream(reader);
                    PropertyBag ui_data = PropertyBag.FromStream(reader);
                    return new GameSnapshot(player_data, scene_data, ui_data);
                }
            }
        }

        /// <summary>
        /// Writes a save file to disk.
        /// </summary>
        /// <param name="slot">The zero-based save slot, range is [0, k_SlotCount).</param>
        /// <param name="snapshot">The <seealso cref="PropertyBag" /> containing the save data.</param>
        /// <exception cref="IOException">Throws if file writing fails.</exception>
        /// <exception cref="UnauthorizedAccessException">Throws if filesystem write access was denied.</exception>
        public static void WriteSnapshot(ESaveSlot slot, GameSnapshot snapshot)
        {
            // Create save file
            string filename = GetSlotFileName(slot);
            string description;
            using (var file = new FileStream(filename, FileMode.Create))
            {
                using (var writer = new BinaryWriter(file, Encoding.UTF8, true))
                {
                    // Write the save version, and the short save info, as header
                    description = GetSnapshotDescription(snapshot);
                    writer.Write(SaveData.k_FileVersion);
                    writer.Write(description);

                    // Write dependencies list
                    writer.Write(GameController.LoadedModules.Count);
                    foreach (FurballMetadata module in GameController.LoadedModules)
                        writer.Write(module.ID.ToByteArray());

                    // Write the actual save data
                    snapshot.PlayerData.Serialize(writer);
                    snapshot.SceneData.Serialize(writer);
                    snapshot.InterfaceData.Serialize(writer);
                }
            }

            // Overwrite the cached version of the slot so it's displayed correctly in UI
            s_Slots[(int)slot] = new SlotInfo
            {
                Description = description,
                IsLoadable = true
            };
        }

        /// <summary>
        /// Returns a string describing a saved game.
        /// </summary>
        public static SlotInfo GetSlotInfo(ESaveSlot slot)
        {
            return s_Slots[(int)slot];
        }

        /// <summary>
        /// Read the header of a save slot, or returns an invalid slot if empty/corrupt.
        /// </summary>
        private static SlotInfo ReadSlotInfo(ESaveSlot slot)
        {
            try
            {
                string filename = GetSlotFileName(slot);

                // Slot may be empty
                if (!File.Exists(filename))
                    return new SlotInfo("Empty Slot");

                // Read the save file header
                using (var file = new FileStream(filename, FileMode.Open))
                {
                    using (var reader = new BinaryReader(file, Encoding.UTF8, true))
                    {
                        // Check version number
                        byte version = reader.ReadByte();
                        if (version != SaveData.k_FileVersion)
                            return new SlotInfo($"Incompatible save version:\r\nfile is v{version}, but expected v{SaveData.k_FileVersion}");

                        // Obtain the info string
                        var info = new SlotInfo
                        {
                            IsLoadable = true,
                            Description = reader.ReadString()
                        };

                        // Check if all modules used by this save are loaded
                        int num_modules = reader.ReadInt32();
                        while (num_modules > 0)
                        {
                            // Get the ID of the module and verify that it's loaded
                            var guid = new Guid(reader.ReadBytes(16));
                            if (GameController.LoadedModules.All(module => module.ID != guid))
                                return new SlotInfo($"Missing required module:\r\nID {guid.ToString().Substring(0, 8)}");

                            num_modules--;
                        }

                        return info;
                    }
                }
            }
            catch (Exception ex)
            {
                // In case of any I/O issues, assume file is corrupted
                return new SlotInfo("Read error: " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a string describing a saved game.
        /// </summary>
        private static string GetSnapshotDescription(GameSnapshot snapshot)
        {
            var text = new StringBuilder();

            // Describe the player data
            PropertyBag player = snapshot.PlayerData;
            text.AppendFormat("{0}{1}  -  Lv {2} {3}",
                IsModdedGame ? "[M] " : String.Empty,
                player.GetString(SaveData.k_Object_Name),
                player.GetInt(SaveData.k_Character_Level),
                player.GetString(SaveData.k_Player_SpeciesSingular).CapFirst());

            // Describe the scene data
            text.AppendLine();
            text.Append(snapshot.InterfaceData.GetString(SaveData.k_UI_Location));

            return text.ToString();
        }

        /// <summary>
        /// Generate a file name for the specified save slot index.
        /// </summary>
        private static string GetSlotFileName(ESaveSlot slot)
        {
            return $"Slot{(int)slot}.sav";
        }

        /// <summary>
        /// Represents a save slot.
        /// </summary>
        public struct SlotInfo
        {

            public SlotInfo(string errorMessage)
            {
                IsLoadable = false;
                Description = errorMessage;
            }

            /// <summary>
            /// Indicates whether this slot contains a valid save file.
            /// </summary>
            public bool IsLoadable { get; set; }

            /// <summary>
            /// A human-readable string describing the contents of this save slot, or a relevant read error.
            /// </summary>
            public string Description { get; set; }

        }
    }

}
