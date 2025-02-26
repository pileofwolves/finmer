/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
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
        public string ObjectName { get; set; } = String.Empty;

        /// <summary>
        /// The alternate name of the object, used as value for GameObject.Alias.
        /// </summary>
        public string ObjectAlias { get; set; } = String.Empty;

        /// <summary>
        /// Arbitrary narrative or informative text displayed at the bottom of the item tooltip.
        /// </summary>
        public string FlavorText { get; set; } = String.Empty;

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
                    outstream.WriteObjectProperty(null, effect, EFurballObjectMode.Required);
                }
                outstream.EndArray();
            }
            outstream.WriteCompressedInt32Property(nameof(PurchaseValue), PurchaseValue);
            outstream.WriteBooleanProperty(nameof(IsQuestItem), IsQuestItem);

            // Usable item data
            if (ItemType == EItemType.Usable)
            {
                outstream.WriteBooleanProperty(nameof(IsConsumable), IsConsumable);
                outstream.WriteBooleanProperty(nameof(CanUseInField), CanUseInField);
                outstream.WriteBooleanProperty(nameof(CanUseInBattle), CanUseInBattle);
                outstream.WriteStringProperty(nameof(UseDescription), UseDescription);
                outstream.WriteObjectProperty(nameof(UseScript), UseScript, EFurballObjectMode.Optional);
            }

            // Icon data
            outstream.WriteAttachment(GetIconAttachmentName(), InventoryIcon);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            base.Deserialize(instream);

            // Core stats
            ObjectName = instream.ReadStringProperty(nameof(ObjectName));
            ObjectAlias = instream.ReadStringProperty(nameof(ObjectAlias));
            FlavorText = instream.ReadStringProperty(nameof(FlavorText));
            ItemType = instream.ReadEnumProperty<EItemType>(nameof(ItemType));
            if (ItemType == EItemType.Equipable)
            {
                EquipSlot = instream.ReadEnumProperty<EEquipSlot>(nameof(EquipSlot));

                // V19 onwards: equip effect groups with nested buffs and proc settings
                for (int count = instream.BeginArray(nameof(EquipEffects)); count > 0; count--)
                    EquipEffects.Add(instream.ReadObjectProperty<EquipEffectGroup>(null, EFurballObjectMode.Required));
                instream.EndArray();
            }
            PurchaseValue = instream.ReadCompressedInt32Property(nameof(PurchaseValue));
            IsQuestItem = instream.ReadBooleanProperty(nameof(IsQuestItem));

            // Usable item data
            if (ItemType == EItemType.Usable)
            {
                IsConsumable = instream.ReadBooleanProperty(nameof(IsConsumable));
                CanUseInField = instream.ReadBooleanProperty(nameof(CanUseInField));
                CanUseInBattle = instream.ReadBooleanProperty(nameof(CanUseInBattle));
                UseDescription = instream.ReadStringProperty(nameof(UseDescription));

                // Read attached UseScript, or if there is none, instantiate one now
                UseScript = instream.ReadObjectProperty<AssetScript>(nameof(UseScript), EFurballObjectMode.Optional)
                    ?? new AssetScript { ID = Guid.NewGuid() };

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
