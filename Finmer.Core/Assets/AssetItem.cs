/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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
        public enum EItemType
        {
            Generic,
            Equipable,
            Usable
        }

        /// <summary>
        /// Describes the type of equipment slot that fits an Equipable item.
        /// </summary>
        public enum EEquipSlot
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
        public List<Buff> EquipEffects { get; } = new List<Buff>();

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
            outstream.WriteStringProperty("ObjectName", ObjectName);
            outstream.WriteStringProperty("ObjectAlias", ObjectAlias);
            outstream.WriteStringProperty("FlavorText", FlavorText);
            outstream.WriteEnumProperty("ItemType", ItemType);
            if (ItemType == EItemType.Equipable)
            {
                // Save equip slot setting only for equipable items
                outstream.WriteEnumProperty("EquipSlot", EquipSlot);
                outstream.BeginArray("EquipEffects", EquipEffects.Count);
                foreach (var effect in EquipEffects)
                {
                    // Serialize each equip effect
                    outstream.BeginObject();
                    effect.Serialize(outstream);
                    outstream.EndObject();
                }
                outstream.EndArray();
            }
            outstream.WriteInt32Property("PurchaseValue", PurchaseValue);
            outstream.WriteBooleanProperty("IsQuestItem", IsQuestItem);

            // Usable item data
            if (ItemType == EItemType.Usable)
            {
                outstream.WriteBooleanProperty("IsConsumable", IsConsumable);
                outstream.WriteBooleanProperty("CanUseInField", CanUseInField);
                outstream.WriteBooleanProperty("CanUseInBattle", CanUseInBattle);
                outstream.WriteStringProperty("UseDescription", UseDescription);

                outstream.BeginObject("UseScript");
                UseScript.Serialize(outstream);
                outstream.EndObject();
            }

            // Icon data
            outstream.WriteAttachment(GetIconAttachmentName(), InventoryIcon);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            base.Deserialize(instream, version);

            // Core stats
            ObjectName = instream.ReadStringProperty("ObjectName");
            ObjectAlias = instream.ReadStringProperty("ObjectAlias");
            FlavorText = instream.ReadStringProperty("FlavorText");
            ItemType = instream.ReadEnumProperty<EItemType>("ItemType");
            if (ItemType == EItemType.Equipable)
            {
                EquipSlot = instream.ReadEnumProperty<EEquipSlot>("EquipSlot");
                for (int count = instream.BeginArray("EquipEffects"); count > 0; count--)
                {
                    // Read each equip effect
                    instream.BeginObject();
                    var effect_type_name = instream.ReadStringProperty("!Type");
                    var effect_instance = BuffFactory.CreateInstance(effect_type_name);
                    effect_instance.Deserialize(instream);
                    EquipEffects.Add(effect_instance);
                    instream.EndObject();
                }
                instream.EndArray();
            }
            PurchaseValue = instream.ReadInt32Property("PurchaseValue");
            IsQuestItem = instream.ReadBooleanProperty("IsQuestItem");

            // Usable item data
            if (ItemType == EItemType.Usable)
            {
                IsConsumable = instream.ReadBooleanProperty("IsConsumable");
                CanUseInField = instream.ReadBooleanProperty("CanUseInField");
                CanUseInBattle = instream.ReadBooleanProperty("CanUseInBattle");
                UseDescription = instream.ReadStringProperty("UseDescription");

                // Attached scripts
                instream.BeginObject("UseScript");
                UseScript = new AssetScript();
                UseScript.Deserialize(instream, version);
                instream.EndObject();
            }

            // Icon data
            InventoryIcon = instream.ReadAttachment(GetIconAttachmentName());
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
