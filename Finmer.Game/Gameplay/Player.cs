/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Finmer.Core;
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

        public const int k_AbilityScorePointsAllowed = 8;

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

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        [TextProperty(@"speciesplural")]
        public string SpeciesPlural { get; set; }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        [TextProperty(@"fur")]
        public string CoatNoun { get; set; }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        [TextProperty(@"furry")]
        public string CoatAdjective { get; set; }

        [ScriptableProperty(EScriptAccess.Read)]
        public bool PreferEndo { get; private set; }

        [ScriptableProperty(EScriptAccess.Read)]
        public int XP { get; private set; }

        [ScriptableProperty(EScriptAccess.Read)]
        public int RequiredXP => 100 + (Level - 1) * 20;

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public int FeatPoints
        {
            get => m_FeatPoints;
            set
            {
                m_FeatPoints = value;
                OnPropertyChanged();
            }
        }

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
        public string Location { get; set; }

        [ScriptableProperty(EScriptAccess.Read)]
        public bool PreferScat => UserConfig.PreferScat;

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
        public Journal Journal { get; }

        /// <summary>
        /// Returns a PropertyBag that is included in save data, that other code can write arbitrary data to.
        /// </summary>
        public PropertyBag AdditionalSaveData { get; }

        [ScriptableProperty(EScriptAccess.Read)]
        public sealed override bool IsAlly => true;

        private int m_AbilityPoints;
        private int m_FeatPoints;
        private int m_Money;

        private string m_Species;
        private int m_TimeDay;
        private int m_TimeHour;
        private int m_TotalPreyDigested;
        private int m_TotalPreySwallowed;

        public Player(ScriptContext context, PropertyBag template) : base(context, template)
        {
            // Player stats
            m_Species = template.GetString("species");
            SpeciesPlural = template.GetString(@"species_plural");
            CoatNoun = template.GetString(@"species_coat_n");
            CoatAdjective = template.GetString(@"species_coat_a");
            Location = template.GetString("location");
            PreferEndo = template.GetBool("endo");

            // Progression
            XP = template.GetInt("xp");
            m_FeatPoints = template.GetInt("featpoints");
            m_AbilityPoints = template.GetInt("abilitypoints");
            m_Money = template.GetInt("money");
            m_TotalPreySwallowed = template.GetInt("totalpreyswallowed");
            m_TotalPreyDigested = template.GetInt("totalpreydigested");
            m_TimeDay = template.GetInt("timeday");
            m_TimeHour = template.GetInt("timehour");

            // Additional data
            AdditionalSaveData = template.GetNestedPropertyBag("extdata") ?? new PropertyBag();

            // Inventory
            int num_items = template.GetInt("invcount");
            for (var i = 0; i < num_items; i++)
            {
                PropertyBag value = template.GetNestedPropertyBag("inv_" + i);
                if (value != null)
                    Inventory.Add(Item.FromSaveGame(context, value));
            }

            // Journal
            Journal = new Journal(template.GetNestedPropertyBag("journal"));
        }

        /// <summary>
        /// Adds XP to the player's total and performs levelups if necessary.
        /// </summary>
        /// <param name="amount">The amount of XP to add.</param>
        /// <param name="announce">Whether or not to print a 'you gain ... xp' message.</param>
        public void AwardXP(int amount, bool announce = true)
        {
            // removed, message is now printed by CombatManager.CheckKill()
            if (announce)
                GameUI.Instance.Log($"You gain {amount} XP.", Theme.LogColorLightGray);
            XP += amount;

            while (XP >= RequiredXP)
            {
                // subtract xp needed for levelup
                XP -= RequiredXP;
                Level++;

                // award points
                FeatPoints++;
                AbilityPoints++;

                // notify player
                GameUI.Instance.Log(
                    $"Congratulations, you have reached level {Level}! You gain 1 ability point.",
                    Theme.LogColorNotification);

                OnPropertyChanged(nameof(Level));
                OnPropertyChanged(nameof(RequiredXP));
            }

            OnPropertyChanged(nameof(XP));
        }

        public override PropertyBag SerializeProperties()
        {
            PropertyBag props = base.SerializeProperties();

            props.SetString("species", m_Species);
            props.SetString(@"species_plural", SpeciesPlural);
            props.SetString(@"species_coat_n", CoatNoun);
            props.SetString(@"species_coat_a", CoatAdjective);
            props.SetBool("endo", PreferEndo);
            props.SetInt("level", Level);
            props.SetInt("xp", XP);
            props.SetNestedPropertyBag("extdata", AdditionalSaveData);

            props.SetInt("featpoints", m_FeatPoints);
            props.SetInt("abilitypoints", m_AbilityPoints);
            props.SetInt("money", m_Money);
            props.SetInt("timeday", m_TimeDay);
            props.SetInt("timehour", m_TimeHour);
            props.SetString("location", Location);
            props.SetString("location_nice", GameUI.Instance.Location);
            props.SetInt("totalpreyswallowed", m_TotalPreySwallowed);
            props.SetInt("totalpreydigested", m_TotalPreyDigested);

            props.SetString("game_version", CompileConstants.k_VersionString);
            props.SetInt("game_rev", CompileConstants.k_VersionRevision);

            // quest journal entries
            props.SetNestedPropertyBag("journal", Journal.SerializeProperties());

            // equipment and inventory
            props.SetInt("invcount", Inventory.Count);
            for (var i = 0; i < Inventory.Count; i++)
                props.SetNestedPropertyBag("inv_" + i, Inventory[i].SerializeProperties());

            return props;
        }

        protected override void ReloadPronouns()
        {
            base.ReloadPronouns();

            // Hardcode the player's 'pronouns' to be first-person
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
            LuaApi.lua_pushboolean(L, found ? 1 : 0);

            return 1;
        }

        [ScriptableFunction]
        protected static int ExportedGiveItem(IntPtr L)
        {
            var self = FromLuaNonOptional<Player>(L, 1);
            var item_name = LuaApi.luaL_checkstring(L, 2);

            // Instantiate the item and give it to the player
            Item item = Item.FromAsset(ScriptContext.FromLua(L), item_name);
            self.AddItem(item);

            // Display announcement
            GameUI.Instance.Log($"{item.Name} added to your backpack.", Theme.LogColorNotification);

            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedTakeItem(IntPtr L)
        {
            var self = FromLuaNonOptional<Player>(L, 1);
            var item_name = LuaApi.luaL_checkstring(L, 2);

            // Attempt to remove the first matching item from the player inventory
            LuaApi.lua_pushboolean(L, self.TakeItem(item_name) ? 1 : 0);

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

    }

}
