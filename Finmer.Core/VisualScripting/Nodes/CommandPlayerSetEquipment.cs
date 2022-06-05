/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Globalization;
using System.Text;
using Finmer.Core.Assets;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that modifies the player's equipment.
    /// </summary>
    public sealed class CommandPlayerSetEquipment : ScriptCommand
    {

        /// <summary>
        /// Describes an equipment slot.
        /// </summary>
        public enum ESlot
        {
            Weapon,
            Armor,
            Accessory1,
            Accessory2
        }

        /// <summary>
        /// The equip slot to change.
        /// </summary>
        public ESlot EquipSlot { get; set; } = ESlot.Weapon;

        /// <summary>
        /// The new value of the slot.
        /// </summary>
        public Guid ItemGuid { get; set; } = Guid.Empty;

        /// <summary>
        /// The cached name of the item (for display purposes).
        /// </summary>
        public string ItemName { get; set; } = String.Empty;

        public override string GetEditorDescription()
        {
            return ItemGuid == Guid.Empty
                ? String.Format(CultureInfo.InvariantCulture, "Remove Equipped {0}", EquipSlot)
                : String.Format(CultureInfo.InvariantCulture, "Set Equipped {0} to {1}", EquipSlot, ItemName);
        }

        public override EColor GetEditorColor()
        {
            return EColor.Player;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // If an item was specified for the slot, look it up in content
            AssetItem item = null;
            if (ItemGuid != Guid.Empty)
                item = content.GetAssetByID<AssetItem>(ItemGuid) ?? throw new InvalidScriptNodeException($"Could not find an Item asset with ID {ItemGuid}");

            // Emit assignment operation
            output.AppendFormat("Player.Equipped{0} = ", EquipSlot);
            if (item == null)
                output.Append("nil");
            else
                output.AppendFormat(CultureInfo.InvariantCulture, "Item(\"{0}\")", item.Name);
            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteEnumProperty(nameof(EquipSlot), EquipSlot);
            outstream.WriteGuidProperty(nameof(ItemGuid), ItemGuid);
            outstream.WriteStringProperty(nameof(ItemName), ItemName);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            EquipSlot = instream.ReadEnumProperty<ESlot>(nameof(EquipSlot));
            ItemGuid = instream.ReadGuidProperty(nameof(ItemGuid));
            ItemName = instream.ReadStringProperty(nameof(ItemName));
        }

    }

}
