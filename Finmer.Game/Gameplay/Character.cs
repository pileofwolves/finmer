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
using Finmer.Core.Assets;
using Finmer.Core.Buffs;
using Finmer.Gameplay.Scripting;
using JetBrains.Annotations;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents a single living character.
    /// </summary>
    public class Character : GameObject
    {

        public const int k_AbilityScoreMinimum = 3;

        private const int k_EquipSlotCount = 4;

        /// <summary>
        /// Current number of hit points.
        /// </summary>
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

        /// <summary>
        /// Maximum number of hit points the character can have.
        /// </summary>
        [ScriptableProperty(EScriptAccess.Read)]
        public int HealthMax => Math.Max(1, Body + AlwaysActiveBuffs.OfType<BuffHealth>().Sum(buff => buff.Delta));

        /// <summary>
        /// Set of equipment slots.
        /// </summary>
        public Item[] Equipment { get; } = new Item[k_EquipSlotCount];

        /// <summary>
        /// Current level of experience. Used to determine XP value.
        /// </summary>
        [ScriptableProperty(EScriptAccess.Read)]
        public int Level { get; protected set; }

        /// <summary>
        /// Relative size factor in combat. Used to determine relative strength between characters.
        /// </summary>
        [ScriptableProperty(EScriptAccess.Read)]
        public AssetCreature.ESize Size { get; set; }

        /// <summary>
        /// Combat-related behavioral override flags.
        /// </summary>
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public ECharacterFlags Flags { get; set; }

        /// <summary>
        /// Primary strength stat.
        /// </summary>
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

        /// <summary>
        /// Primary agility stat.
        /// </summary>
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

        /// <summary>
        /// Primary body stat.
        /// </summary>
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

        /// <summary>
        /// Primary wits stat.
        /// </summary>
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

        /// <summary>
        /// Gets or sets whether the character can take predator-related actions during combat.
        /// </summary>
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public bool IsPredator { get; set; }

        /// <summary>
        /// Gets or sets whether swallowed prey take digestion damage during combat.
        /// </summary>
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public bool PredatorDigests { get; set; }

        /// <summary>
        /// Currently unused.
        /// </summary>
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public float PredatorFullness { get; set; }

        /// <summary>
        /// Equipment slot 1.
        /// </summary>
        [CanBeNull]
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public Item EquippedWeapon
        {
            get => Equipment[ItemUtilities.k_SlotIndex_Weapon];
            set => Equipment[ItemUtilities.k_SlotIndex_Weapon] = value;
        }

        /// <summary>
        /// Equipment slot 2.
        /// </summary>
        [CanBeNull]
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public Item EquippedArmor
        {
            get => Equipment[ItemUtilities.k_SlotIndex_Armor];
            set => Equipment[ItemUtilities.k_SlotIndex_Armor] = value;
        }

        /// <summary>
        /// Equipment slot 3.
        /// </summary>
        [CanBeNull]
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public Item EquippedAccessory1
        {
            get => Equipment[ItemUtilities.k_SlotIndex_Accessory1];
            set => Equipment[ItemUtilities.k_SlotIndex_Accessory1] = value;
        }

        /// <summary>
        /// Equipment slot 4.
        /// </summary>
        [CanBeNull]
        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public Item EquippedAccessory2
        {
            get => Equipment[ItemUtilities.k_SlotIndex_Accessory2];
            set => Equipment[ItemUtilities.k_SlotIndex_Accessory2] = value;
        }

        /// <summary>
        /// Returns the collection of always-active buffs from equipped items.
        /// </summary>
        public IEnumerable<Buff> AlwaysActiveBuffs => Equipment
            .Where(item => item != null)
            .SelectMany(item => item.Asset.EquipEffects)
            .Where(group => group.ProcStyle == EquipEffectGroup.EProcStyle.Always)
            .SelectMany(group => group.Buffs);

        /// <summary>
        /// The asset this instance was loaded from. May be null if instance was not loaded from content.
        /// </summary>
        [CanBeNull]
        public AssetCreature Asset { get; private set; }

        private int m_Strength;
        private int m_Agility;
        private int m_Body;
        private int m_Wits;
        private int m_Health;

        protected Character(ScriptContext context) : base(context) {}

        public static Character FromAsset(ScriptContext context, string asset_name)
        {
            if (asset_name == null)
                throw new ArgumentNullException(nameof(asset_name));

            // Find the asset with the given name
            AssetBase asset = GameController.Content.GetAssetByName(asset_name);
            if (!(asset is AssetCreature creature))
                throw new MissingContentException($"Could not find Creature '{asset_name}' in loaded modules.");

            // Convert the asset to a save data template with initial settings
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
            {
                var key = SaveData.CombineBase(SaveData.k_Character_EquipBase, i);
                props.SetNestedPropertyBag(key, Item.FromAsset(context, creature.Equipment[i])?.SaveState());
            }

            // Convert the template to an instance
            var output = new Character(context);
            output.LoadState(props);
            return output;
        }

        public override PropertyBag SaveState()
        {
            PropertyBag props = base.SaveState();

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
            {
                var key = SaveData.CombineBase(SaveData.k_Character_EquipBase, i);
                props.SetNestedPropertyBag(key, Equipment[i]?.SaveState());
            }

            return props;
        }

        public override void LoadState(PropertyBag input)
        {
            base.LoadState(input);

            // Asset ID
            byte[] asset_id = input.GetBytes(SaveData.k_AssetID);
            if (asset_id != null)
                Asset = (AssetCreature)GameController.Content.GetAssetByID(new Guid(asset_id));

            // Core stats
            Strength = input.GetInt(SaveData.k_Character_Strength);
            Agility = input.GetInt(SaveData.k_Character_Agility);
            Body = input.GetInt(SaveData.k_Character_Body);
            Wits = input.GetInt(SaveData.k_Character_Wits);
            Flags = (ECharacterFlags)input.GetInt(SaveData.k_Character_Flags);
            Level = input.GetInt(SaveData.k_Character_Level);
            Size = (AssetCreature.ESize)input.GetInt(SaveData.k_Character_Size, (int)AssetCreature.ESize.Medium);
            Health = input.GetInt(SaveData.k_Character_Health, HealthMax);

            // Combat settings
            IsPredator = input.GetBool(SaveData.k_Character_IsPredator);
            PredatorDigests = input.GetBool(SaveData.k_Character_PredatorDigest);
            PredatorFullness = input.GetFloat(SaveData.k_Character_PredatorFullness);

            // Load equipment
            for (var i = 0; i < k_EquipSlotCount; i++)
            {
                PropertyBag nested = input.GetNestedPropertyBag(SaveData.CombineBase(SaveData.k_Character_EquipBase, i));
                if (nested != null)
                    Equipment[i] = Item.FromSaveData(ScriptContext, nested);
            }
        }

        /// <summary>
        /// Notifies the character that max health was changed, and updates current health accordingly.
        /// </summary>
        /// <param name="old_max">The value of HealthMax before the change was applied.</param>
        public void ApplyMaxHealthChange(int old_max)
        {
            int new_max = HealthMax;
            if (new_max == old_max)
                return;

            // Add or take away the difference in HP, so adding max HP doesn't leave the player with
            // injuries, and removing max HP doesn't allow for free healing (by equipping and unequipping)
            m_Health += new_max - old_max;

            // Ensure health is clamped, and prevent killing player with this
            m_Health = Math.Max(Math.Min(m_Health, new_max), 1);

            // Notify UI
            OnPropertyChanged(nameof(HealthMax));
            OnPropertyChanged(nameof(Health));
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
            return prey.Size <= Size && !prey.Flags.HasFlag(ECharacterFlags.NoPrey);
        }

        /// <summary>
        /// Returns a value indicating whether a certain character can be grappled in a fight.
        /// </summary>
        /// <param name="victim">The character who may or may not be grabbed.</param>
        public bool CanGrapple(Character victim)
        {
            return victim.Size <= Size && !victim.Flags.HasFlag(ECharacterFlags.NoGrapple);
        }

        /// <summary>
        /// Returns all equip effect groups in equipped items that are configured to proc with the specified style.
        /// </summary>
        public IEnumerable<EquipEffectGroup> GetEquipEffectsOfType(EquipEffectGroup.EProcStyle type)
        {
            return Equipment
                .Where(item => item != null)
                .SelectMany(item => item.Asset.EquipEffects)
                .Where(group => group.ProcStyle == type);
        }

        [ScriptableFunction]
        protected static int ExportedIsDead(IntPtr L)
        {
            var self = FromLuaNonOptional<Character>(L, 1);
            LuaApi.lua_pushboolean(L, self.IsDead());
            return 1;
        }

        [ScriptableFunction]
        protected static int ExportedCanSwallow(IntPtr L)
        {
            var lhs = FromLuaNonOptional<Character>(L, 1);
            var rhs = FromLuaNonOptional<Character>(L, 2);
            LuaApi.lua_pushboolean(L, lhs.CanSwallow(rhs));
            return 1;
        }

        [ScriptableFunction]
        protected static int ExportedCanGrapple(IntPtr L)
        {
            var lhs = FromLuaNonOptional<Character>(L, 1);
            var rhs = FromLuaNonOptional<Character>(L, 2);
            LuaApi.lua_pushboolean(L, lhs.CanGrapple(rhs));
            return 1;
        }

    }

}
