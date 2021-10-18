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
using Finmer.Core.Assets;
using Finmer.Core.Buffs;
using Finmer.Gameplay.Scripting;
using Finmer.Models;
using Finmer.Utility;
using Finmer.ViewModels;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents a single living character.
    /// </summary>
    public class Character : GameObject
    {

        public const int k_AbilityScoreMinimum = 3;

        private const int k_EquipSlotCount = 4;

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public int Health
        {
            get => m_Health;
            set
            {
                m_Health = Math.Max(Math.Min(value, HealthMax), 0);
                OnPropertyChanged();
            }
        }

        [ScriptableProperty(EScriptAccess.Read)]
        public int HealthMax => Math.Max(1, Body + CumulativeBuffs.OfType<BuffHealth>().Sum(buff => buff.Delta));

        public Item[] Equipment { get; } = new Item[k_EquipSlotCount];

        [ScriptableProperty(EScriptAccess.Read)]
        public int Level { get; protected set; }

        [ScriptableProperty(EScriptAccess.Read)]
        public AssetCreature.ESize Size { get; set; }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public ECharacterFlags Flags { get; set; }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public int Strength
        {
            get => m_Strength;
            set
            {
                m_Strength = value;
                OnPropertyChanged();
            }
        }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public int Agility
        {
            get => m_Agility;
            set
            {
                m_Agility = value;
                OnPropertyChanged();
            }
        }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public int Body
        {
            get => m_Body;
            set
            {
                m_Body = value;
                OnPropertyChanged();
            }
        }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public int Wits
        {
            get => m_Wits;
            set
            {
                m_Wits = value;
                OnPropertyChanged();
            }
        }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public bool IsPredator { get; set; }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public bool PredatorDigests { get; set; }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public float PredatorFullness { get; set; }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public Item EquippedWeapon
        {
            get => Equipment[ItemUtilities.k_SlotIndex_Weapon];
            set => Equipment[ItemUtilities.k_SlotIndex_Weapon] = value;
        }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public Item EquippedArmor
        {
            get => Equipment[ItemUtilities.k_SlotIndex_Armor];
            set => Equipment[ItemUtilities.k_SlotIndex_Armor] = value;
        }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public Item EquippedAccessory1
        {
            get => Equipment[ItemUtilities.k_SlotIndex_Accessory1];
            set => Equipment[ItemUtilities.k_SlotIndex_Accessory1] = value;
        }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public Item EquippedAccessory2
        {
            get => Equipment[ItemUtilities.k_SlotIndex_Accessory2];
            set => Equipment[ItemUtilities.k_SlotIndex_Accessory2] = value;
        }

        /// <summary>
        /// Collection of buffs applied to this character specifically (e.g. excludes items).
        /// </summary>
        public List<Buff> LocalBuffs { get; } = new List<Buff>();

        /// <summary>
        /// Returns the cumulative collection of buffs applied to this Character and all its equipped items.
        /// </summary>
        public IEnumerable<Buff> CumulativeBuffs => LocalBuffs.Concat(Equipment.Where(item => item != null).SelectMany(item => item.Asset.EquipEffects));

        [ScriptableProperty(EScriptAccess.Read)]
        public int NumAttackDice => Math.Max(Strength + CumulativeBuffs.OfType<BuffAttackDice>().Sum(buff => buff.Delta), 1);

        [ScriptableProperty(EScriptAccess.Read)]
        public int NumDefenseDice => Math.Max(Agility + CumulativeBuffs.OfType<BuffDefenseDice>().Sum(buff => buff.Delta), 1);

        [ScriptableProperty(EScriptAccess.Read)]
        public int NumGrappleDice => Math.Max(Agility, 1);

        [ScriptableProperty(EScriptAccess.Read)]
        public int NumSwallowDice => Math.Max(Strength, 1);

        [ScriptableProperty(EScriptAccess.Read)]
        public int NumStruggleDice => Math.Max(Agility, 1);

        private readonly AssetCreature m_Asset;
        private int m_Strength;
        private int m_Agility;
        private int m_Body;
        private int m_Wits;
        private int m_Health;

        protected Character(ScriptContext context, PropertyBag template) : base(context, template)
        {
            // Asset ID
            byte[] asset_id = template.GetBytes("asset");
            if (asset_id != null)
                m_Asset = (AssetCreature)GameController.Content.GetAssetByID(new Guid(asset_id));

            // Core stats
            Strength = template.GetInt("str");
            Agility = template.GetInt("dex");
            Body = template.GetInt("con");
            Wits = template.GetInt("wis");
            Flags = (ECharacterFlags)template.GetInt("flags");
            Level = template.GetInt("level");
            Size = (AssetCreature.ESize)template.GetInt("size", (int)AssetCreature.ESize.Medium);
            Health = template.GetInt("health", HealthMax);

            // Vore stats
            IsPredator = template.GetBool("predator");
            PredatorDigests = template.GetBool("predator_digest", true);
            PredatorFullness = template.GetFloat("predator_fullness");

            // Load equipment
            for (var i = 0; i < k_EquipSlotCount; i++)
            {
                PropertyBag nested = template.GetNestedPropertyBag("eqp_" + i);
                if (nested != null)
                    Equipment[i] = Item.FromSaveGame(context, nested);
            }
        }

        public static Character FromAsset(ScriptContext context, string assetName)
        {
            try
            {
                // Find the asset with the given name
                AssetBase asset = GameController.Content.GetAssetByName(assetName);
                if (!(asset is AssetCreature creature))
                    throw new ArgumentException($"The specified asset ('{assetName ?? "[null]"}') does not exist or is not a Creature.", nameof(assetName));

                // Convert the asset to a savedata template with initial settings
                PropertyBag props = new PropertyBag();
                props.SetBytes("asset", asset.ID.ToByteArray());
                props.SetString("name", creature.ObjectName);
                props.SetString("alias", creature.ObjectAlias);
                props.SetInt("str", creature.Strength);
                props.SetInt("dex", creature.Agility);
                props.SetInt("con", creature.Body);
                props.SetInt("wis", creature.Wits);
                props.SetInt("flags", creature.Flags);
                props.SetInt("level", creature.Level);
                props.SetBool("predator", creature.PredatorEnabled);
                props.SetBool("predator_digest", creature.PredatorDigests);
                props.SetNestedPropertyBag("eqp_0", Item.FromAsset(context, creature.Equipment[0])?.SerializeProperties());
                props.SetNestedPropertyBag("eqp_1", Item.FromAsset(context, creature.Equipment[1])?.SerializeProperties());
                props.SetNestedPropertyBag("eqp_2", Item.FromAsset(context, creature.Equipment[2])?.SerializeProperties());
                props.SetNestedPropertyBag("eqp_3", Item.FromAsset(context, creature.Equipment[3])?.SerializeProperties());

                return new Character(context, props);
            }
            catch (Exception ex)
            {
                GameUI.Instance.Log($"ERROR: Failed to create character '{assetName}': {ex}", Theme.LogColorError);
                return null;
            }
        }

        /// <summary>
        /// Saves the <see cref="Character" />'s properties to the underlying <seealso cref="PropertyBag" />.
        /// </summary>
        public override PropertyBag SerializeProperties()
        {
            PropertyBag props = base.SerializeProperties();

            // Asset ID
            if (m_Asset != null)
                props.SetBytes("asset", m_Asset.ID.ToByteArray());

            // Core stats
            props.SetInt("str", Strength);
            props.SetInt("dex", Agility);
            props.SetInt("con", Body);
            props.SetInt("wis", Wits);
            props.SetInt("flags", (int)Flags);
            props.SetInt("level", Level);
            props.SetInt("size", (int)Size);
            props.SetInt("health", Health);

            // Vore stats
            props.SetBool("predator", IsPredator);
            props.SetBool("predator_digest", PredatorDigests);
            props.SetFloat("predator_fullness", PredatorFullness);

            // Equipment
            for (var i = 0; i < k_EquipSlotCount; i++)
                props.SetNestedPropertyBag("eqp_" + i, Equipment[i]?.SerializeProperties());

            return props;
        }

        /// <summary>
        /// Returns the degree to which an ability score is above-average, meaning how many points it's above the minimum.
        /// </summary>
        public static int GetAbilityModifier(int ability)
        {
            return ability - k_AbilityScoreMinimum;
        }

        /// <summary>
        /// Returns a random string from the string table.
        /// </summary>
        /// <param name="key">The key to look up in the table.</param>
        /// <param name="cause"></param>
        public string GetRandomString(string key, ScriptableObject cause)
        {
            // TODO: Reimplement m_Asset.StringMappings

            return GameController.Content.GetAndParseString(key);
        }

        /// <summary>
        /// Returns a value indicating whether this character has zero HP or less.
        /// </summary>
        public bool IsDead()
        {
            return Health <= 0;
        }

        /// <summary>
        /// Returns a value indicating whether this character is friendly to the player.
        /// </summary>
        public virtual bool IsAlly()
        {
            return false;
        }

        /// <summary>
        /// Returns a value indicating whether a certain prey can be swallowed.
        /// </summary>
        /// <param name="prey">The hypothetical prey character.</param>
        public bool CanSwallow(Character prey)
        {
            return prey.Size <= Size;
        }

        protected override GameObjectViewModel CreateViewModel()
        {
            return new CharacterViewModel(this);
        }

        [ScriptableFunction]
        protected static int ExportedGetString(IntPtr state)
        {
            Character self = FromLuaNonOptional<Character>(state, 1);
            Character cause = FromLuaOptional<Character>(state, 3) ?? self;
            LuaApi.lua_pushstring(state, self.GetRandomString(LuaApi.luaL_checkstring(state, 2), cause));
            return 1;
        }

    }

}
