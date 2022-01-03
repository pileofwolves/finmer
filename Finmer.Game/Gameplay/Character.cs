/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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

        /// <summary>
        /// Gets or sets whether this character is friendly to the player during combat.
        /// </summary>
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public virtual bool IsAlly { get; set; }

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
        public int NumSwallowDice => Math.Max(Body, 1);

        [ScriptableProperty(EScriptAccess.Read)]
        public int NumStruggleDice => Math.Max(Agility, 1);

        public AssetCreature Asset { get; }

        private int m_Strength;
        private int m_Agility;
        private int m_Body;
        private int m_Wits;
        private int m_Health;

        protected Character(ScriptContext context, PropertyBag template) : base(context, template)
        {
            // Asset ID
            byte[] asset_id = template.GetBytes(SaveData.k_AssetID);
            if (asset_id != null)
                Asset = (AssetCreature)GameController.Content.GetAssetByID(new Guid(asset_id));

            // Core stats
            Strength = template.GetInt(SaveData.k_Character_Strength);
            Agility = template.GetInt(SaveData.k_Character_Agility);
            Body = template.GetInt(SaveData.k_Character_Body);
            Wits = template.GetInt(SaveData.k_Character_Wits);
            Flags = (ECharacterFlags)template.GetInt(SaveData.k_Character_Flags);
            Level = template.GetInt(SaveData.k_Character_Level);
            Size = (AssetCreature.ESize)template.GetInt(SaveData.k_Character_Size, (int)AssetCreature.ESize.Medium);
            Health = template.GetInt(SaveData.k_Character_Health, HealthMax);

            // Combat settings
            IsPredator = template.GetBool(SaveData.k_Character_IsPredator);
            PredatorDigests = template.GetBool(SaveData.k_Character_PredatorDigest);
            PredatorFullness = template.GetFloat(SaveData.k_Character_PredatorFullness);

            // Load equipment
            for (var i = 0; i < k_EquipSlotCount; i++)
            {
                PropertyBag nested = template.GetNestedPropertyBag(SaveData.CombineBase(SaveData.k_Character_EquipBase, i));
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
                props.SetBytes(SaveData.k_AssetID, asset.ID.ToByteArray());
                props.SetString(SaveData.k_Object_Name, creature.ObjectName);
                props.SetString(SaveData.k_Object_Alias, creature.ObjectAlias);
                props.SetInt(SaveData.k_Object_Gender, (int)creature.Gender);
                props.SetInt(SaveData.k_Character_Strength, creature.Strength);
                props.SetInt(SaveData.k_Character_Agility, creature.Agility);
                props.SetInt(SaveData.k_Character_Body, creature.Body);
                props.SetInt(SaveData.k_Character_Wits, creature.Wits);
                props.SetInt(SaveData.k_Character_Flags, creature.Flags);
                props.SetInt(SaveData.k_Character_Level, creature.Level);
                props.SetInt(SaveData.k_Character_Size, (int)creature.Size);
                props.SetBool(SaveData.k_Character_IsPredator, creature.PredatorEnabled);
                props.SetBool(SaveData.k_Character_PredatorDigest, creature.PredatorDigests);
                for (var i = 0; i < k_EquipSlotCount; i++)
                    props.SetNestedPropertyBag(SaveData.CombineBase(SaveData.k_Character_EquipBase, i), Item.FromAsset(context, creature.Equipment[i])?.SerializeProperties());

                return new Character(context, props);
            }
            catch (Exception ex)
            {
                GameUI.Instance.Log($"ERROR: Failed to create character '{assetName}': {ex}", Theme.LogColorError);
                return null;
            }
        }

        public override PropertyBag SerializeProperties()
        {
            PropertyBag props = base.SerializeProperties();

            // Asset ID
            if (Asset != null)
                props.SetBytes(SaveData.k_AssetID, Asset.ID.ToByteArray());

            // Core stats
            props.SetInt(SaveData.k_Character_Strength, Strength);
            props.SetInt(SaveData.k_Character_Agility, Agility);
            props.SetInt(SaveData.k_Character_Body, Body);
            props.SetInt(SaveData.k_Character_Wits, Wits);
            props.SetInt(SaveData.k_Character_Flags, (int)Flags);
            props.SetInt(SaveData.k_Character_Level, Level);
            props.SetInt(SaveData.k_Character_Size, (int)Size);
            props.SetInt(SaveData.k_Character_Health, Health);

            // Vore stats
            props.SetBool(SaveData.k_Character_IsPredator, IsPredator);
            props.SetBool(SaveData.k_Character_PredatorDigest, PredatorDigests);
            props.SetFloat(SaveData.k_Character_PredatorFullness, PredatorFullness);

            // Equipment
            for (var i = 0; i < k_EquipSlotCount; i++)
                props.SetNestedPropertyBag(SaveData.CombineBase(SaveData.k_Character_EquipBase, i), Equipment[i]?.SerializeProperties());

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
        /// Returns a value indicating whether this character has zero HP or less.
        /// </summary>
        public bool IsDead()
        {
            return Health <= 0;
        }

        /// <summary>
        /// Returns a value indicating whether a certain prey can be swallowed.
        /// </summary>
        /// <param name="prey">The hypothetical prey character.</param>
        public bool CanSwallow(Character prey)
        {
            return prey.Size <= Size;
        }

        /// <summary>
        /// Returns a value indicating whether a certain character can be grappled in a fight.
        /// </summary>
        /// <param name="victim">The character who may or may not be grabbed.</param>
        public bool CanGrapple(Character victim) 
        {
            return victim.Size <= Size;
        }
    }

}
