/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Text;
using Finmer.Core.Assets;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Script value that returns an equipped item.
    /// </summary>
    public sealed class ConditionPlayerEquipment : ScriptCondition
    {

        /// <summary>
        /// The item to check for.
        /// </summary>
        public Guid ItemGuid { get; set; } = Guid.Empty;

        public override string GetEditorDescription(IContentStore content)
        {
            return "Player Has Equipped " + content.GetAssetName(ItemGuid);
        }

        public override EColor GetEditorColor()
        {
            return EColor.Player;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // Locate and validate the linked item asset
            var item = content.GetAssetByID<AssetItem>(ItemGuid);
            if (item == null)
                throw new InvalidScriptNodeException($"Could not find an Item asset with ID {ItemGuid}");
            if (item.ItemType != AssetItem.EItemType.Equipable)
                throw new InvalidScriptNodeException($"Item '{item.Name}' is used in an ConditionPlayerEquipment, but is not Equipable");

            // Emit code to check the slot fields as appropriate for the target item
            switch (item.EquipSlot)
            {
                case AssetItem.EEquipSlot.Weapon:
                    output.Append("(Player.EquippedWeapon and Player.EquippedWeapon.AssetName == \"");
                    output.Append(item.Name);
                    output.Append("\")");
                    break;

                case AssetItem.EEquipSlot.Armor:
                    output.Append("(Player.EquippedArmor and Player.EquippedArmor.AssetName == \"");
                    output.Append(item.Name);
                    output.Append("\")");
                    break;

                case AssetItem.EEquipSlot.Accessory:
                    // Need to check both equipment slots, so join them with a logical OR
                    output.Append("((Player.EquippedAccessory1 and Player.EquippedAccessory1.AssetName == \"");
                    output.Append(item.Name);
                    output.Append("\") or ");
                    output.Append("(Player.EquippedAccessory2 and Player.EquippedAccessory2.AssetName == \"");
                    output.Append(item.Name);
                    output.Append("\"))");
                    break;

                default:
                    throw new FurballInvalidAssetException($"Item '{item.Name}' has invalid equip slot {(int)item.EquipSlot}");
            }
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteGuidProperty(nameof(ItemGuid), ItemGuid);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            ItemGuid = instream.ReadGuidProperty(nameof(ItemGuid));
        }

    }

}
