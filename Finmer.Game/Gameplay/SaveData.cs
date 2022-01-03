/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Globalization;

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
        public const string k_Character_PredatorFullness = @"predator_fullness";
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
        public const string k_Player_Journal = @"journal";

        public const string k_Shop_Title = @"title";
        public const string k_Shop_RestockRequired = @"restock_required";
        public const string k_Shop_RestockInterval = @"restock_interval";
        public const string k_Shop_RestockTimestamp = @"restock_timestamp";
        public const string k_Shop_StockCount = @"stock_count";
        public const string k_Shop_StockQuantityBase = @"stock_qty_";
        public const string k_Shop_StockItemBase = @"stock_data_";
        public const string k_Shop_StockUniqueBase = @"stock_unique_";

        public const string k_Journal_Count = @"count";
        public const string k_Journal_EntryGuid = @"guid_";
        public const string k_Journal_EntryStage = @"stage_";

        public const string k_GameVersion = @"game_version";
        public const string k_GameRevision = @"game_revision";

        /// <summary>
        /// Returns a culture-independent namespaced array index.
        /// </summary>
        public static string CombineBase(string group, int suffix)
        {
            return group + suffix.ToString(CultureInfo.InvariantCulture);
        }

    }

}
