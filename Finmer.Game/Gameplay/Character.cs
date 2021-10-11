/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        /// <summary>
        /// Type of a serialized object in the save data for a Character's stomach.
        /// </summary>
        private enum ESerializedPreyType
        {
            Creature,
            Item
        }

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
        public bool IsPredator { get; set; }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public float Predatoriness { get; set; }

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public float Digestedness { get; set; }

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

        public List<GameObject> Stomach { get; } = new List<GameObject>();

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public bool StomachDigest { get; set; }

        [ScriptableProperty(EScriptAccess.Read)]
        public int StomachCount => Stomach.Count;

        [ScriptableProperty(EScriptAccess.ReadWrite)]
        public float BowelFullness { get; set; }

        [ScriptableProperty(EScriptAccess.Read)]
        public float StomachFullness => Stomach.OfType<Character>().Sum(ch => GetBellySizeForPrey(ch));

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
            StomachDigest = template.GetBool("predator_digest", true);
            Predatoriness = template.GetFloat("predness");
            Digestedness = template.GetFloat("digested");
            BowelFullness = template.GetFloat("bowelfullness");

            // Load equipment
            for (var i = 0; i < k_EquipSlotCount; i++)
            {
                PropertyBag nested = template.GetNestedPropertyBag("eqp_" + i);
                if (nested != null)
                    Equipment[i] = Item.FromSaveGame(context, nested);
            }

            // Stomach contents
            int preycount = template.GetInt("stomach_count");
            for (var i = 0; i < preycount; i++)
            {
                PropertyBag nested = template.GetNestedPropertyBag("stomach_" + i);
                if (nested != null)
                {
                    // Deserialize the prey object as an instance of the appropriate class
                    GameObject prey;
                    switch ((ESerializedPreyType)template.GetInt("stomach_type_" + i))
                    {
                        case ESerializedPreyType.Creature:
                            prey = FromSaveGame(context, nested);
                            break;

                        case ESerializedPreyType.Item:
                            prey = Item.FromSaveGame(context, nested);
                            break;

                        default:
                            throw new InvalidDataException("Unknown serialized prey type");
                    }

                    Stomach.Add(prey);
                }
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

        public static Character FromSaveGame(ScriptContext context, PropertyBag savedata)
        {
            return new Character(context, savedata);
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
            props.SetBool("predator_digest", StomachDigest);
            props.SetFloat("predness", Predatoriness);
            props.SetFloat("digested", Digestedness);
            props.SetFloat("bowelfullness", BowelFullness);

            // Equipment
            for (var i = 0; i < k_EquipSlotCount; i++)
                props.SetNestedPropertyBag("eqp_" + i, Equipment[i]?.SerializeProperties());

            // Stomach contents
            props.SetInt("stomach_count", StomachCount);
            for (var i = 0; i < StomachCount; i++)
            {
                props.SetInt("stomach_type_" + i, (int)(Stomach[i] is Character ? ESerializedPreyType.Creature : ESerializedPreyType.Item));
                props.SetNestedPropertyBag("stomach_" + i, Stomach[i].SerializeProperties());
            }

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

            return GameController.GetString(key);
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
        /// Returns a <see cref="List&lt;Character&gt;" /> of swallowed prey that are still alive.
        /// </summary>
        public List<Character> GetLivePrey()
        {
            return Stomach.OfType<Character>().Where(prey => !prey.IsDead()).ToList();
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
        /// Simulate time spent digesting swallowed prey.
        /// </summary>
        /// <param name="hours">The number of hours passed.</param>
        public void DigestPrey(float hours)
        {
            // TODO: Clean up this method (leftover from V1 combat) and move it somewhere other than the Character class

            // if the player has live prey, select a random one and show a message that theyre still being digested
            List<Character> live_prey = Stomach.OfType<Character>().Where(ch => ch.Digestedness < 1f).ToList();
            if (this is Player && StomachDigest && live_prey.Count >= 1)
            {
                Character random_prey = live_prey[CoreUtility.Rng.Next(0, live_prey.Count)];
                TextParser.SetContext("predator", this, false);
                TextParser.SetContext("prey", random_prey, false);
                GameUI.Instance.Log(random_prey.GetRandomString("predpov_clock_digesting", this), Theme.LogColorDefault);

                // belly capacity tip... this is a decent place to put this code, I guess?
                if (!GameController.Session.Player.AdditionalSaveData.GetBool("tip_shown_bellycapacity"))
                {
                    GameController.Session.Player.AdditionalSaveData.SetBool("tip_shown_bellycapacity", true);
                    GameUI.Instance.Log(GameController.GetString("tip_belly_capacity"), Theme.LogColorHighlight);
                }
            }

            for (int i = Stomach.Count - 1; i >= 0; i--)
            {
                GameObject obj = Stomach[i];
                if (obj is Character prey)
                {
                    // same-size prey will take 15 hours to fully digest, smaller prey is faster
                    if (StomachDigest)
                    {
                        float digest_speed = (float)Size / (float)prey.Size;
                        prey.Digestedness += hours * (0.06666f * digest_speed);
                    }

                    // fully digested prey is removed entirely
                    if (prey.Digestedness >= 1f)
                    {
                        float meat = (float)prey.Size / (float)Size;
                        TextParser.SetContext("predator", this, false);
                        TextParser.SetContext("prey", prey, false);
                        GameUI.Instance.Log(prey.GetRandomString("predpov_clock_digested", this), Theme.LogColorDefault);
                        Debug.Assert(prey.StomachCount == 0); // will be lost, should've been removed by CombatManager.CheckKill
                        BowelFullness += meat * meat;
                        Stomach.RemoveAt(i);
                    }
                    else
                    {
                        // recurse so prey also digest their own prey, if that happens to be a thing
                        prey.DigestPrey(hours);
                    }
                }
                else if (obj is Item item)
                {
                    // object vore, I guess? I don't really want to bother tracking digestedness for items, so just instant gurgles
                    TextParser.SetContext("predator", this, false);
                    TextParser.SetContext("item", item, false);
                    GameUI.Instance.Log(GetRandomString("predpov_clock_item_digested", this), Theme.LogColorDefault);
                    BowelFullness += 0.5f;
                    Stomach.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Returns the relative amount of space a given prey character would take up when swallowed.
        /// </summary>
        /// <param name="prey">The hypothetical prey.</param>
        private float GetBellySizeForPrey(Character prey)
        {
            return (float)Math.Pow(0.5, Size - prey.Size) * (1f - prey.Digestedness);
        }

        protected override GameObjectViewModel CreateViewModel()
        {
            return new CharacterViewModel(this);
        }

        [ScriptableFunction]
        protected static int ExportedClearStomach(IntPtr state)
        {
            Character self = FromLuaNonOptional<Character>(state, 1);
            self.Stomach.Clear();
            return 0;
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
