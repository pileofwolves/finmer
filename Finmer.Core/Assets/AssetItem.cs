/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using Finmer.Core.Buffs;
using Finmer.Core.Serialization;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Represents a template for an inventory item or equipable item.
    /// </summary>
    public class AssetItem : AssetBase
    {

        /// <summary>
        /// Describes the in-game functionality of an item.
        /// </summary>
        public enum EItemType : byte
        {
            Generic,
            Equipable,
            Usable
        }

        /// <summary>
        /// Describes the type of equipment slot that fits an Equipable item.
        /// </summary>
        public enum EEquipSlot : byte
        {
            Weapon,
            Armor,
            Accessory
        }

        /// <summary>
        /// The name of the object, used as value for GameObject.Name.
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// The alternate name of the object, used as value for GameObject.Alias.
        /// </summary>
        public string ObjectAlias { get; set; }

        /// <summary>
        /// Arbitrary narrative or informative text displayed at the bottom of the item tooltip.
        /// </summary>
        public string FlavorText { get; set; }

        /// <summary>
        /// The in-game functionality group of the item.
        /// </summary>
        public EItemType ItemType { get; set; }

        /// <summary>
        /// The in-game functionality group of the item.
        /// </summary>
        public EEquipSlot EquipSlot { get; set; }

        /// <summary>
        /// Collection of buffs applied when the item is equipped.
        /// </summary>
        public List<EquipEffectGroup> EquipEffects { get; } = new List<EquipEffectGroup>();

        /// <summary>
        /// The economic value of this item. Set to zero to prevent the item from being traded.
        /// </summary>
        public int PurchaseValue { get; set; }

        /// <summary>
        /// Whether this represents a quest item. Quest items cannot be deleted by the player.
        /// </summary>
        public bool IsQuestItem { get; set; }

        /// <summary>
        /// If ItemType is Usable, indicates whether this item is deleted when it is used.
        /// </summary>
        public bool IsConsumable { get; set; }

        /// <summary>
        /// If ItemType is Usable, indicates whether this item can be used from the Inventory screen.
        /// </summary>
        public bool CanUseInField { get; set; }

        /// <summary>
        /// If ItemType is Usable, indicates whether this item can be used during the player's turn in combat.
        /// </summary>
        public bool CanUseInBattle { get; set; }

        /// <summary>
        /// If ItemType is Usable, specifies a string to display on the item tooltip that describes this item's effect.
        /// </summary>
        public string UseDescription { get; set; } = String.Empty;

        /// <summary>
        /// A 32x32 icon to display in UI to represent this item. May be null, in which case a default icon is used.
        /// </summary>
        public byte[] InventoryIcon { get; set; }

        /// <summary>
        /// The script that is invoked when the item is Used from the character sheet.
        /// </summary>
        public AssetScript UseScript { get; set; }

        public override void Serialize(IFurballContentWriter outstream)
        {
            base.Serialize(outstream);

            // Core stats
            outstream.WriteStringProperty(nameof(ObjectName), ObjectName);
            outstream.WriteStringProperty(nameof(ObjectAlias), ObjectAlias);
            outstream.WriteStringProperty(nameof(FlavorText), FlavorText);
            outstream.WriteEnumProperty(nameof(ItemType), ItemType);
            if (ItemType == EItemType.Equipable)
            {
                // Save equip slot setting only for equipable items
                outstream.WriteEnumProperty(nameof(EquipSlot), EquipSlot);
                outstream.BeginArray(nameof(EquipEffects), EquipEffects.Count);
                foreach (var effect in EquipEffects)
                {
                    // Serialize each equip effect
                    outstream.WriteNestedObjectProperty(null, effect);
                }
                outstream.EndArray();
            }
            outstream.WriteInt32Property(nameof(PurchaseValue), PurchaseValue);
            outstream.WriteBooleanProperty(nameof(IsQuestItem), IsQuestItem);

            // Usable item data
            if (ItemType == EItemType.Usable)
            {
                outstream.WriteBooleanProperty(nameof(IsConsumable), IsConsumable);
                outstream.WriteBooleanProperty(nameof(CanUseInField), CanUseInField);
                outstream.WriteBooleanProperty(nameof(CanUseInBattle), CanUseInBattle);
                outstream.WriteStringProperty(nameof(UseDescription), UseDescription);
                outstream.WriteNestedObjectProperty(nameof(UseScript), UseScript);
            }

            // Icon data
            outstream.WriteAttachment(GetIconAttachmentName(), InventoryIcon);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            base.Deserialize(instream, version);

            // Core stats
            ObjectName = instream.ReadStringProperty(nameof(ObjectName));
            ObjectAlias = instream.ReadStringProperty(nameof(ObjectAlias));
            FlavorText = instream.ReadStringProperty(nameof(FlavorText));
            ItemType = instream.ReadEnumProperty<EItemType>(nameof(ItemType));
            if (ItemType == EItemType.Equipable)
            {
                EquipSlot = instream.ReadEnumProperty<EEquipSlot>(nameof(EquipSlot));

                // Read equipment effect groups
                if (version >= 19)
                {
                    // V19 onwards: equip effect groups with nested buffs and proc settings
                    for (int count = instream.BeginArray(nameof(EquipEffects)); count > 0; count--)
                        EquipEffects.Add(instream.ReadNestedObjectProperty<EquipEffectGroup>(null, version));
                    instream.EndArray();
                }
                else
                {
                    // V18 backwards compatibility: proc data didn't exist yet. If item had buffs, wrap them in a group with the Always proc mode.
                    var implicit_group = new EquipEffectGroup();
                    for (int count = instream.BeginArray(nameof(EquipEffects)); count > 0; count--)
                        implicit_group.Buffs.Add(instream.ReadNestedObjectProperty<Buff>(null, version));
                    instream.EndArray();
                    if (implicit_group.Buffs.Count != 0)
                        EquipEffects.Add(implicit_group);
                }
            }
            PurchaseValue = instream.ReadInt32Property(nameof(PurchaseValue));
            IsQuestItem = instream.ReadBooleanProperty(nameof(IsQuestItem));

            // Usable item data
            if (ItemType == EItemType.Usable)
            {
                IsConsumable = instream.ReadBooleanProperty(nameof(IsConsumable));
                CanUseInField = instream.ReadBooleanProperty(nameof(CanUseInField));
                CanUseInBattle = instream.ReadBooleanProperty(nameof(CanUseInBattle));
                UseDescription = instream.ReadStringProperty(nameof(UseDescription));

                // Attached scripts
                if (version >= 16)
                {
                    UseScript = instream.ReadNestedObjectProperty<AssetScript>(nameof(UseScript), version);
                }
                else
                {
                    // V15 backwards compatibility
                    instream.BeginObject("UseScript");
                    UseScript = new AssetScript();
                    UseScript.Deserialize(instream, version);
                    instream.EndObject();
                }

                // If there is no UseScript object, instantiate one now
                if (UseScript == null)
                    UseScript = new AssetScript { ID = Guid.NewGuid() };

                // Ensure the script is named
                UseScript.Name = GetUseScriptName();
            }

            // Icon data
            InventoryIcon = instream.ReadAttachment(GetIconAttachmentName());
        }

        /// <summary>
        /// Returns the name that the UseScript should have. Used to reduce code duplication.
        /// </summary>
        /// <returns></returns>
        public string GetUseScriptName()
        {
            return Name + "_Use";
        }

        /// <summary>
        /// Returns an attachment key suitable for the item icon image.
        /// </summary>
        private string GetIconAttachmentName()
        {
            return Name + ".png";
        }

    }

}
