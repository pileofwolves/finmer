/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Diagnostics;
using Finmer.Core.Assets;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Container for functions related to using Items in a GameSession.
    /// </summary>
    public static class ItemUtilities
    {

        private const int k_SlotIndex_Weapon = 0;
        private const int k_SlotIndex_Armor = 1;
        private const int k_SlotIndex_Accessory1 = 2;
        private const int k_SlotIndex_Accessory2 = 3;

        /// <summary>
        /// Invoke an item's UseScript and handle side effects such as consumable status.
        /// </summary>
        public static void UseItem(GameSession session, Item item)
        {
            Debug.Assert(item.Asset.ItemType == AssetItem.EItemType.Usable, "Trying to use a non-Usable item");

            // Load the item's UseScript onto the script stack
            var asset = item.Asset;
            var script_context = session.ScriptContext;
            if (!script_context.LoadScript(asset.UseScript.PrecompiledScript, asset.UseScript.Name))
                return;

            // Execute the script
            if (!script_context.RunProtectedCall(0, 0))
                return;

            // If the item is consumable, delete it from the inventory now
            if (asset.IsConsumable)
            {
                var inventory = session.Player.Inventory;
                inventory.Remove(item);
            }
        }

        /// <summary>
        /// Place an item in a player's equipment slots, swapping out another item if necessary.
        /// </summary>
        public static void EquipItem(Player player, Item equippable)
        {
            // Determine an appropriate slot index for the item
            if (!GetSuitableEquipSlot(player, equippable.Asset, out int slot))
            {
                // The slot is occupied; remove the old item first
                UnequipItem(player, slot);
            }

            // Remove the new item from the player's inventory
            Debug.Assert(player.Inventory.Contains(equippable));
            player.Inventory.Remove(equippable);

            // Place it in the equipment slot
            Debug.Assert(player.Equipment[slot] == null);
            player.Equipment[slot] = equippable;
        }

        /// <summary>
        /// Remove an item from a player's equipment slot, returning it to the inventory.
        /// </summary>
        public static void UnequipItem(Player player, int equipSlotIndex)
        {
            Item equipped = player.Equipment[equipSlotIndex];
            if (equipped == null)
                return;

            // Remove it from the slot
            player.Equipment[equipSlotIndex] = null;

            // Append the item to the end of the inventory
            player.Inventory.Add(equipped);
        }

        /// <summary>
        /// Returns an equipment slot index suitable for the specified equippable item.
        /// </summary>
        /// <returns>
        /// Always writes the slot index in the slot output parameter (regardless of whether there is space). Returns true
        /// if the slot is empty, false otherwise (meaning the item would fit, but needs to be swapped with the slot).
        /// </returns>
        private static bool GetSuitableEquipSlot(Player player, AssetItem asset, out int slot)
        {
            Debug.Assert(asset.ItemType == AssetItem.EItemType.Equipable, "Trying to equip non-equipable item");

            switch (asset.EquipSlot)
            {
                case AssetItem.EEquipSlot.Weapon:
                    // Only one slot for weapons
                    slot = k_SlotIndex_Weapon;
                    return player.Equipment[k_SlotIndex_Weapon] == null;

                case AssetItem.EEquipSlot.Armor:
                    // Only one slot for armor
                    slot = k_SlotIndex_Armor;
                    return player.Equipment[k_SlotIndex_Armor] == null;

                case AssetItem.EEquipSlot.Accessory:
                    // Accessories can go in multiple slots, so try the first one to see if it's empty
                    if (player.Equipment[k_SlotIndex_Accessory1] == null)
                    {
                        slot = k_SlotIndex_Accessory1;
                        return true;
                    }

                    // Otherwise, try the second slot
                    slot = k_SlotIndex_Accessory2;
                    return player.Equipment[k_SlotIndex_Accessory2] == null;

                default:
                    throw new ArgumentOutOfRangeException(nameof(asset), "Invalid EquipSlot value");
            }
        }

    }

}
