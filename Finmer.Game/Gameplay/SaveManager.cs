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
    /// Manages the saving and loading of save files.
    /// </summary>
    public static class SaveManager
    {

        private const int k_SlotCount = 3;

        private static readonly SaveSlot[] s_Slots = new SaveSlot[k_SlotCount];
        private static readonly Guid k_CoreModuleID = new Guid("edcf99d2-6ced-40fa-87e9-86cda5e570ee");

        /// <summary>
        /// Indicates whether the game instance includes unofficial content.
        /// </summary>
        public static bool IsModdedGame =>
            // The game cannot start without at least one module loaded, so if any of them isn't Core, there are mods present
            GameController.LoadedModules.Any(meta => meta.ID != k_CoreModuleID);

        /// <summary>
        /// Preload the contents of all save slots, for display. This performs disk access and is therefore slow.
        /// </summary>
        public static void CacheSlots()
        {
            for (var index = 0; index < k_SlotCount; index++)
                s_Slots[index] = CacheSlot(index);
        }

        /// <summary>
        /// Returns the save data stored in the specified slot, or null if none exists.
        /// </summary>
        /// <param name="slot">The save slot index (zero-based).</param>
        public static GameSnapshot LoadSaveFile(int slot)
        {
            string filename = GetSaveFileName(slot);
            if (!File.Exists(filename))
                return null;

            // read PropertyBag from file stream
            using (var file = new FileStream(filename, FileMode.Open))
            {
                using (var reader = new BinaryReader(file, Encoding.UTF8, true))
                {
                    // Verify version
                    byte version = reader.ReadByte();
                    if (version != SaveData.k_FileVersion)
                        throw new InvalidDataException("Incompatible save version " + version);

                    // Skip the header, which will have been validated by CacheSlot().
                    reader.ReadString();
                    int num_modules = reader.ReadInt32();
                    reader.ReadBytes(num_modules * 16);

                    // Deserialize stream
                    var player_data = PropertyBag.FromStream(reader);
                    var scene_data = PropertyBag.FromStream(reader);
                    var ui_data = PropertyBag.FromStream(reader);
                    return new GameSnapshot(player_data, scene_data, ui_data);
                }
            }
        }

        /// <summary>
        /// Writes a save file to disk.
        /// </summary>
        /// <param name="slot">The zero-based save slot, range is [0, k_SlotCount).</param>
        /// <param name="snapshot">The <seealso cref="PropertyBag" /> containing the save data.</param>
        public static void Save(int slot, GameSnapshot snapshot)
        {
            // Create save file
            string filename = GetSaveFileName(slot);
            string description;
            using (var file = new FileStream(filename, FileMode.Create))
            {
                using (var writer = new BinaryWriter(file, Encoding.UTF8, true))
                {
                    // Write the save version, and the short save info, as header
                    description = DescribeSaveFile(snapshot);
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
            s_Slots[slot].Info = description;
            s_Slots[slot].IsLoadable = true;
        }

        /// <summary>
        /// Returns a string describing a saved game.
        /// </summary>
        /// <param name="index">The save slot index (zero-based).</param>
        public static SaveSlot GetSaveInfo(int index)
        {
            SaveSlot slot = s_Slots[index];
            return slot;
        }

        /// <summary>
        /// Preload the contents of a single save slot, so it can be displayed.
        /// </summary>
        private static SaveSlot CacheSlot(int index)
        {
            try
            {
                string filename = GetSaveFileName(index);

                // Slot may be empty
                if (!File.Exists(filename))
                    return new SaveSlot("Empty");

                // Read the save file header
                using (var file = new FileStream(filename, FileMode.Open))
                {
                    using (var reader = new BinaryReader(file, Encoding.UTF8, true))
                    {
                        // Check version number
                        byte version = reader.ReadByte();
                        if (version != SaveData.k_FileVersion)
                            return new SaveSlot($"Incompatible save version:\r\nfile is v{version}, but expected v{SaveData.k_FileVersion}");

                        // Obtain the info string
                        var slot = new SaveSlot
                        {
                            IsLoadable = true,
                            Info = reader.ReadString()
                        };

                        // Check if all modules used by this save are loaded
                        int num_modules = reader.ReadInt32();
                        while (num_modules > 0)
                        {
                            // Get the ID of the module and verify that it's loaded
                            var guid = new Guid(reader.ReadBytes(16));
                            if (GameController.LoadedModules.All(module => module.ID != guid))
                                return new SaveSlot("Missing required module:\r\n" + guid);

                            num_modules--;
                        }

                        return slot;
                    }
                }
            }
            catch (Exception ex)
            {
                // In case of any I/O issues, assume file is corrupted
                return new SaveSlot("Read error: " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a string describing a saved game.
        /// </summary>
        /// <param name="snapshot">The <seealso cref="Player" /> to check.</param>
        private static string DescribeSaveFile(GameSnapshot snapshot)
        {
            var text = new StringBuilder();

            // Describe the player data
            var player = snapshot.PlayerData;
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
        private static string GetSaveFileName(int slot)
        {
            return $"Slot{slot}.sav";
        }

        /// <summary>
        /// Represents a save slot.
        /// </summary>
        public struct SaveSlot
        {

            public SaveSlot(string errorMessage)
            {
                IsLoadable = false;
                Info = errorMessage;
            }

            /// <summary>
            /// Indicates whether this slot contains a valid save file.
            /// </summary>
            public bool IsLoadable { get; set; }

            /// <summary>
            /// A human-readable string describing the contents of this save slot, or a relevant read error.
            /// </summary>
            public string Info { get; set; }

        }

    }

}
