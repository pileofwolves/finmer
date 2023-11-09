/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Globalization;
using System.IO;
using System.Text;
using Finmer.Core;
using Finmer.Core.Serialization;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Centralized collection of PropertyBag keys used in save data, for improved maintainability.
    /// </summary>
    public static class SaveData
    {

        public const byte k_FileVersion = 5;

        public const string k_AssetID = @"asset";

        public const string k_System_CurrentSceneID = @"scene_guid";
        public const string k_System_CurrentSceneState = @"scene_state";

        public const string k_UI_Location = @"ui_location";
        public const string k_UI_Instruction = @"ui_instruction";
        public const string k_UI_InventoryEnabled = @"ui_inv";
        public const string k_UI_LogSnippet_Count = @"log_count";
        public const string k_UI_LogSnippet_Base = @"log_";

        public const string k_Object_Guid = @"guid";
        public const string k_Object_Name = @"name";
        public const string k_Object_Alias = @"alias";
        public const string k_Object_Gender = @"gender";
        public const string k_Object_TagCount = @"tag_count";
        public const string k_Object_TagBase = @"tag_";

        public const string k_Character_Strength = @"str";
        public const string k_Character_Agility = @"agi";
        public const string k_Character_Body = @"body";
        public const string k_Character_Wits = @"wits";
        public const string k_Character_Flags = @"flags";
        public const string k_Character_Level = @"level";
        public const string k_Character_Size = @"size";
        public const string k_Character_Health = @"health";
        public const string k_Character_IsPredator = @"predator";
        public const string k_Character_PredatorDigest = @"predator_digest";
        public const string k_Character_EquipBase = @"equip_";

        public const string k_Player_SpeciesSingular = @"species";
        public const string k_Player_SpeciesPlural = @"species_plural";
        public const string k_Player_SpeciesCoatNoun = @"species_coat_n";
        public const string k_Player_SpeciesCoatAdj = @"species_coat_a";
        public const string k_Player_XP = @"xp";
        public const string k_Player_ExtData = @"extdata";
        public const string k_Player_FeatPoints = @"feat_points";
        public const string k_Player_AbilityPoints = @"ability_points";
        public const string k_Player_Money = @"money";
        public const string k_Player_TimeDay = @"time_day";
        public const string k_Player_TimeHour = @"time_hour";
        public const string k_Player_NumPreySwallowed = @"num_prey_swallowed";
        public const string k_Player_NumPreyDigested = @"num_prey_digested";
        public const string k_Player_InventoryCount = @"inv_count";
        public const string k_Player_InventoryBase = @"inv_";
        public const string k_Player_PendingBuffCount = @"pending_buff_count";
        public const string k_Player_PendingBuffBase = @"pending_buff_";
        public const string k_Player_Journal = @"journal";

        public const string k_Shop_Title = @"title";
        public const string k_Shop_RestockRequired = @"restock_required";
        public const string k_Shop_RestockInterval = @"restock_interval";
        public const string k_Shop_RestockTimestamp = @"restock_timestamp";
        public const string k_Shop_StockCount = @"stock_count";
        public const string k_Shop_StockQuantityBase = @"stock_qty_";
        public const string k_Shop_StockItemBase = @"stock_data_";
        public const string k_Shop_StockUniqueBase = @"stock_unique_";

        public const string k_PendingBuff_Effect = @"buff";
        public const string k_PendingBuff_Duration = @"duration";

        public const string k_Journal_Count = @"count";
        public const string k_Journal_EntryGuid = @"guid_";
        public const string k_Journal_EntryStage = @"stage_";

        public const string k_GameVersion = @"game_version";
        public const string k_GameVersionMajor = @"version_major";
        public const string k_GameVersionMinor = @"version_minor";
        public const string k_GameVersionRevision = @"version_rev";

        /// <summary>
        /// Returns a culture-independent namespaced array index.
        /// </summary>
        public static string CombineBase(string group, int suffix)
        {
            return group + suffix.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Game-specific save data extension: Writes a furball asset to save data.
        /// </summary>
        /// <param name="output">The output save data container.</param>
        /// <param name="asset">The asset to serialize.</param>
        /// <param name="key">The unique key that identifies the asset in save data.</param>
        public static void SetSavedAsset(this PropertyBag output, IFurballSerializable asset, string key)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms, Encoding.UTF8, true))
            {
                // Serialize asset to a byte array
                var writer = new FurballContentWriterBinary(bw);
                AssetSerializer.SerializeAsset(writer, asset);

                // Store the byte array
                output.SetBytes(key, ms.ToArray());
            }
        }

        /// <summary>
        /// Game-specific save data extension: Reads a furball asset from save data.
        /// </summary>
        /// <param name="input">The input save data container to read from.</param>
        /// <param name="key">The unique key that was used to identify this asset when it was saved.</param>
        /// <exception cref="InvalidSaveDataException">Throws if asset cannot be deserialized.</exception>
        public static TAsset GetSavedAsset<TAsset>(this PropertyBag input, string key) where TAsset : class, IFurballSerializable
        {
            // Retrieve the raw byte array that represents the asset
            byte[] serialized = input.GetBytes(key);
            if (serialized == null)
                throw new InvalidSaveDataException($"Saved asset '{key}' is missing from save data");

            // Wrap a stream around it
            using (var ms = new MemoryStream(serialized))
            using (var br = new BinaryReader(ms, Encoding.UTF8, true))
            {
                try
                {
                    // Reconstruct and deserialize the asset
                    var reader = new FurballContentReaderBinary(br);
                    var result = AssetSerializer.DeserializeAsset(reader, FurballFileDevice.k_LatestVersion) as TAsset;

                    // If casting failed, the asset was of some unexpected type
                    if (result == null)
                        throw new InvalidSaveDataException($"Saved asset '{key}' had unexpected type");

                    return result;
                }
                catch (FurballException ex)
                {
                    // Forward all other deserialization errors to the caller as a save data problem
                    throw new InvalidSaveDataException($"Failed to deserialize saved asset '{key}': " + ex.Message, ex);
                }
            }
        }

    }

}
