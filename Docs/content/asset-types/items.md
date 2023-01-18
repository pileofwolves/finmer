---
sidebar_position: 2
---

# Items

Items are anything the player or NPCs can equip, use, or carry in an inventory. They are used for story-relevant quest items, combat-influencing equipment, and shop merchandise.

## Properties

The following properties are available when editing an Item: 

Property|Description
---|---
Item Name|The canonical name of the object, as it appears in the inventory screen and shops.
Alias|Alternate name of the object, as it is used in dialogue and combat.
Type|**Generic:** Not interactive. Useful for quest items and the like.<br/>**Equipable:** Can be placed in an equip slot and may provide equip effects.<br/>**Usable:** Can be activated by the player, executing a custom script.
Value|Purchase value of the item. When selling, sale price is half the purchase value. Set to zero to forbid selling.
Icon|Image representing the item. Must be 32 x 32 pixels, PNG format.
Quest Item|If ticked, item cannot be discarded or sold.
Flavor Text|Additional lore text shown on item tooltip. Optional, but recommended.
Asset GUID|Unique ID number of the asset. For advanced users.

## Equipable items

When the item's **Type** is set to **Equipable**, the following additional properties are available:

Property|Description
---|---
Equip Slot|Determines in which slot the player must put the item. The player has 1 Weapon slot, 1 Armor slot, and 2 Accessory slots. NPC Creatures do not observe equip slots.
Equip Effects|List of effects that will be applied to the Creature while this item is equipped.

## Usable items

When the item's **Type** is set to **Usable**, the following additional properties are available:

Property|Description
---|---
Item Script|The script that is run when the player uses this item.
Use Description|Informational text shown in the item tooltip, describing what the item's effects are.
Consumable|If ticked, item is removed from player inventory after use.
Allow Use in Field|Allow item to be used from the Character Sheet.
Allow Use in Battle|Allow item to be used in combat.
