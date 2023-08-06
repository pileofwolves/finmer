/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Finmer.Core;
using Finmer.Gameplay.Combat;
using Finmer.Gameplay.Scripting;
using Finmer.Models;
using Finmer.Utility;
using JetBrains.Annotations;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents the player character.
    /// </summary>
    public class Player : Character
    {

        /// <summary>
        /// Number of ability points a new player character can allocate to start with.
        /// </summary>
        public const int k_AbilityScorePointsAllowed = 8;

        /// <summary>
        /// Lowercase singular noun for player character species.
        /// </summary>
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        [TextProperty(@"species")]
        public string Species
        {
            get => m_Species;
            set
            {
                m_Species = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Lowercase plural noun for player character species.
        /// </summary>
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        [TextProperty(@"speciesplural")]
        public string SpeciesPlural { get; set; }

        /// <summary>
        /// Lowercase noun describing the player character's fur coat.
        /// </summary>
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        [TextProperty(@"fur")]
        public string CoatNoun { get; set; }

        /// <summary>
        /// Lowercase adjective describing the player character's fur coat.
        /// </summary>
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        [TextProperty(@"furry")]
        public string CoatAdjective { get; set; }

        /// <summary>
        /// XP acquired since last level.
        /// </summary>
        [ScriptableProperty(EScriptAccess.Read)]
        public int XP { get; private set; }

        /// <summary>
        /// Total XP required to reach the next experience level.
        /// </summary>
        [ScriptableProperty(EScriptAccess.Read)]
        public int RequiredXP => 100 + (Level - 1) * 20;

        /// <summary>
        /// Number of available, unallocated ability points.
        /// </summary>
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public int AbilityPoints
        {
            get => m_AbilityPoints;
            set
            {
                m_AbilityPoints = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Total money currently owned by the player.
        /// </summary>
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public int Money
        {
            get => m_Money;
            set
            {
                m_Money = value;
                OnPropertyChanged();
            }
        }

        [ScriptableProperty(EScriptAccess.Read)]
        [Obsolete("Backwards compatibility only; use 'IsExplicitDisposalEnabled' instead")]
        public bool PreferScat => IsExplicitDisposalEnabled;

        /// <summary>
        /// Wraps disposal setting for scripts.
        /// </summary>
        [ScriptableProperty(EScriptAccess.Read)]
        public bool IsExplicitDisposalEnabled => UserConfig.AllowExplicitDisposal;

        /// <summary>
        /// Wraps prey-sense setting for scripts.
        /// </summary>
        [ScriptableProperty(EScriptAccess.Read)]
        public bool IsPreySenseEnabled => UserConfig.AllowPreySense;

        /// <summary>
        /// Wraps explorer mode setting for scripts.
        /// </summary>
        [ScriptableProperty(EScriptAccess.Read)]
        public bool IsExplorerModeEnabled => UserConfig.ExplorerMode;

        /// <summary>
        /// Total number of characters the player has swallowed in combat since creation.
        /// </summary>
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public int TotalPreySwallowed
        {
            get => m_TotalPreySwallowed;
            set
            {
                m_TotalPreySwallowed = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Total number of characters the player has digested in swallowed and combat since creation.
        /// </summary>
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public int TotalPreyDigested
        {
            get => m_TotalPreyDigested;
            set
            {
                m_TotalPreyDigested = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Current day number of the world clock, starting at 1.
        /// </summary>
        public int TimeDay
        {
            get => m_TimeDay;
            set
            {
                m_TimeDay = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TimeHourCumulative));
            }
        }

        /// <summary>
        /// Current hour number of the world clock, ranging 0-23 inclusive.
        /// </summary>
        public int TimeHour
        {
            get => m_TimeHour;
            set
            {
                m_TimeHour = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TimeHourCumulative));
            }
        }

        /// <summary>
        /// Total number of hours passed in the game.
        /// </summary>
        public int TimeHourCumulative => TimeDay * 24 + TimeHour;

        /// <summary>
        /// Collection of Items held by the player.
        /// </summary>
        public List<Item> Inventory { get; } = new List<Item>();

        /// <summary>
        /// Returns the player's Journal state.
        /// </summary>
        public Journal Journal { get; private set; }

        /// <summary>
        /// Collection of buffs that have been applied in script, and will take effect in the next combat.
        /// </summary>
        public List<PendingBuff> PendingBuffs { get; } = new List<PendingBuff>();

        /// <summary>
        /// Returns a PropertyBag that is included in save data, that other code can write arbitrary data to.
        /// </summary>
        public PropertyBag AdditionalSaveData { get; private set; }

        /// <inheritdoc />
        [ScriptableProperty(EScriptAccess.Read)]
        public sealed override bool IsAlly => true;

        private string m_Species;
        private int m_AbilityPoints;
        private int m_FeatPoints;
        private int m_Money;
        private int m_TimeDay;
        private int m_TimeHour;
        private int m_TotalPreyDigested;
        private int m_TotalPreySwallowed;

        public Player(ScriptContext context) : base(context) {}

        /// <summary>
        /// Adds XP to the player's total and performs level-ups if necessary.
        /// </summary>
        /// <param name="amount">The amount of XP to add.</param>
        public void AwardXP(int amount)
        {
            GameUI.Instance.Log($"Gained {amount} XP.", Theme.LogColorLightGray);
            XP += amount;

            while (XP >= RequiredXP)
            {
                // Subtract XP required for level-up
                XP -= RequiredXP;
                Level++;

                // Award ability point
                AbilityPoints++;

                // Show a notification message
                GameUI.Instance.Log(
                    $"Congratulations, you have reached level {Level}! You gain 1 ability point.",
                    Theme.LogColorNotification);

                OnPropertyChanged(nameof(Level));
                OnPropertyChanged(nameof(RequiredXP));
            }

            OnPropertyChanged(nameof(XP));
        }

        public override PropertyBag SaveState()
        {
            PropertyBag output = base.SaveState();

            // Customization data
            output.SetString(SaveData.k_Player_SpeciesSingular, m_Species);
            output.SetString(SaveData.k_Player_SpeciesPlural, SpeciesPlural);
            output.SetString(SaveData.k_Player_SpeciesCoatNoun, CoatNoun);
            output.SetString(SaveData.k_Player_SpeciesCoatAdj, CoatAdjective);

            // Script data
            output.SetNestedPropertyBag(SaveData.k_Player_ExtData, AdditionalSaveData);

            // Gameplay data
            output.SetInt(SaveData.k_Player_XP, XP);
            output.SetInt(SaveData.k_Player_FeatPoints, m_FeatPoints);
            output.SetInt(SaveData.k_Player_AbilityPoints, m_AbilityPoints);
            output.SetInt(SaveData.k_Player_Money, m_Money);
            output.SetInt(SaveData.k_Player_TimeDay, m_TimeDay);
            output.SetInt(SaveData.k_Player_TimeHour, m_TimeHour);
            output.SetInt(SaveData.k_Player_NumPreySwallowed, m_TotalPreySwallowed);
            output.SetInt(SaveData.k_Player_NumPreyDigested, m_TotalPreyDigested);

            // Game version
            output.SetString(SaveData.k_GameVersion, CompileConstants.k_VersionString);
            output.SetInt(SaveData.k_GameRevision, CompileConstants.k_VersionRevision);

            // Journal
            output.SetNestedPropertyBag(SaveData.k_Player_Journal, Journal.SaveState());

            // Inventory
            output.SetInt(SaveData.k_Player_InventoryCount, Inventory.Count);
            for (var i = 0; i < Inventory.Count; i++)
            {
                var key = SaveData.CombineBase(SaveData.k_Player_InventoryBase, i);
                output.SetNestedPropertyBag(key, Inventory[i].SaveState());
            }

            // Pending buffs
            output.SetInt(SaveData.k_Player_PendingBuffCount, PendingBuffs.Count);
            for (var i = 0; i < PendingBuffs.Count; i++)
            {
                var key = SaveData.CombineBase(SaveData.k_Player_PendingBuffBase, i);
                output.SetNestedPropertyBag(key, PendingBuffs[i].SaveState());
            }

            return output;
        }

        public override void LoadState(PropertyBag input)
        {
            base.LoadState(input);

            // Customization data
            m_Species = input.GetString(SaveData.k_Player_SpeciesSingular);
            SpeciesPlural = input.GetString(SaveData.k_Player_SpeciesPlural);
            CoatNoun = input.GetString(SaveData.k_Player_SpeciesCoatNoun);
            CoatAdjective = input.GetString(SaveData.k_Player_SpeciesCoatAdj);

            // Gameplay data
            XP = input.GetInt(SaveData.k_Player_XP);
            m_FeatPoints = input.GetInt(SaveData.k_Player_FeatPoints);
            m_AbilityPoints = input.GetInt(SaveData.k_Player_AbilityPoints);
            m_Money = input.GetInt(SaveData.k_Player_Money);
            m_TimeDay = input.GetInt(SaveData.k_Player_TimeDay);
            m_TimeHour = input.GetInt(SaveData.k_Player_TimeHour);
            m_TotalPreySwallowed = input.GetInt(SaveData.k_Player_NumPreySwallowed);
            m_TotalPreyDigested = input.GetInt(SaveData.k_Player_NumPreyDigested);

            // Player character always digests prey
            IsPredator = true;
            PredatorDigests = true;

            // Script data
            AdditionalSaveData = input.GetNestedPropertyBag(SaveData.k_Player_ExtData) ?? new PropertyBag();

            // Inventory
            int num_items = input.GetInt(SaveData.k_Player_InventoryCount);
            for (var i = 0; i < num_items; i++)
            {
                PropertyBag value = input.GetNestedPropertyBag(SaveData.CombineBase(SaveData.k_Player_InventoryBase, i));
                if (value != null)
                    Inventory.Add(Item.FromSaveData(ScriptContext, value));
            }

            // Pending buffs
            num_items = input.GetInt(SaveData.k_Player_PendingBuffCount);
            for (var i = 0; i < num_items; i++)
            {
                var key = SaveData.CombineBase(SaveData.k_Player_PendingBuffBase, i);
                var instance = new PendingBuff(ScriptContext);
                instance.LoadState(input.GetNestedPropertyBag(key));
                PendingBuffs.Add(instance);
            }

            // Journal
            Journal = new Journal(input.GetNestedPropertyBag(SaveData.k_Player_Journal));
        }

        protected override void ReloadPronouns()
        {
            base.ReloadPronouns();

            // Force the player's 'pronouns' to be first-person
            Alias = "you";
            PronounObjective = "you";
            PronounSubjective = "you";
            PronounPossessive = "your";
            AliasPossessive = "your";
            NamePossessive = "your";
        }

        /// <summary>
        /// Adds an item to the player's inventory.
        /// </summary>
        public void AddItem([NotNull] Item item)
        {
            Inventory.Add(item);
        }

        /// <summary>
        /// Finds the first item in the player's inventory with a matching file name and deletes it.
        /// </summary>
        /// <param name="itemname">The item file name to destroy.</param>
        /// <returns>Returns true on success, false if the item could not be found.</returns>
        public bool TakeItem(string itemname)
        {
            for (var i = 0; i < Inventory.Count; i++)
                // Find matching item in inventory
                if (Inventory[i].Asset.Name.Equals(itemname, StringComparison.InvariantCultureIgnoreCase))
                {
                    // If matching item is found, destroy the item
                    Inventory.RemoveAt(i);
                    return true;
                }

            return false;
        }

        [ScriptableFunction]
        protected static int ExportedHasItem(IntPtr L)
        {
            // Get the file name to locate in the inventory
            var self = FromLuaNonOptional<Player>(L, 1);
            var item_name = LuaApi.luaL_checkstring(L, 2);

            // Look for this item in the player's inventory
            bool found = self.Inventory.Any(item => item.Asset.Name.Equals(item_name, StringComparison.InvariantCultureIgnoreCase));
            LuaApi.lua_pushboolean(L, found);

            return 1;
        }

        [ScriptableFunction]
        protected static int ExportedGiveItem(IntPtr L)
        {
            // Unmarshal input arguments
            var self = FromLuaNonOptional<Player>(L, 1);
            var item_name = LuaApi.luaL_checkstring(L, 2);
            var quiet = LuaApi.lua_type(L, 3) == LuaApi.ELuaType.Boolean && LuaApi.lua_toboolean(L, 3);

            // Instantiate the item
            Item instance;
            try
            {
                instance = Item.FromAsset(self.ScriptContext, item_name);
            }
            catch (Exception ex)
            {
                return LuaApi.luaL_error(L, $"Failed to load Item '{item_name}': {ex.Message}");
            }

            // Give the item to the player
            self.AddItem(instance);

            // Display announcement, unless explicitly silenced
            if (!quiet)
                GameUI.Instance.Log($"{instance.Name} added to your backpack.", Theme.LogColorNotification);

            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedTakeItem(IntPtr L)
        {
            var self = FromLuaNonOptional<Player>(L, 1);
            var item_name = LuaApi.luaL_checkstring(L, 2);

            // Attempt to remove the first matching item from the player inventory
            LuaApi.lua_pushboolean(L, self.TakeItem(item_name));

            return 1;
        }

        [ScriptableFunction]
        protected static int ExportedAwardXP(IntPtr L)
        {
            var self = FromLuaNonOptional<Player>(L, 1);
            var amount = (int)LuaApi.luaL_checknumber(L, 2);
            self.AwardXP(amount);

            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedModifyMoney(IntPtr L)
        {
            var self = FromLuaNonOptional<Player>(L, 1);
            var delta = (int)LuaApi.luaL_checknumber(L, 2);

            // Ignore same-value assignments
            if (delta == 0)
                return 0;

            // Show message describing the change
            var suffix = (Math.Abs(delta) == 1) ? String.Empty : "s";
            GameUI.Instance.Log($"{(delta > 0 ? "Gained" : "Lost")} {Math.Abs(delta)} coin{suffix}.", Theme.LogColorLightGray);

            // Perform assignment, clamp to zero
            self.Money = Math.Max(self.Money + delta, 0);

            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedApplyBuff(IntPtr L)
        {
            var self = FromLuaNonOptional<Player>(L, 1);
            var buff = FromLuaNonOptional<PendingBuff>(L, 2);

            // If there is an active combat, prefer applying the buff immediately. This simplifies user script code.
            var active_combat = CombatSession.GetActiveSession();
            if (active_combat != null)
            {
                // Find the player in this combat - could be absent too
                var player_participant = active_combat.Participants.FirstOrDefault(p => p.IsPlayer());
                if (player_participant != null)
                {
                    // If found, apply buff directly
                    player_participant.ApplyPendingBuff(buff);
                    return 0;
                }
            }

            // Otherwise, add buff to list of effects to apply on next combat start
            self.PendingBuffs.Add(buff);

            return 0;
        }

    }

}
