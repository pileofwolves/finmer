---
sidebar_position: 9
---

import LuaReference from '@site/src/components/LuaReference';
import ApiGroup from '@site/src/scriptref/Shop.json'

# Shop System

These functions and properties are members of Shop instances. Create a Shop instance using the `Shop` function.

The general procedure to display an interactive shop is as follows:

1. Load a shop's data using the `Shop` function
2. Check the `RestockRequired` property. If it returns true, you should:
	- Call `RemoveDefaultStock()` to erase the previous stock,
	- Add fresh items using `AddItem()`,
	- Finally, call `MarkRestocked()`.
3. Call `Show()` to display to the player
4. Call `Save()` to save the shop

You do not need to call `Show()` per se. It is fine to load, modify and save a shop if you only wish to modify stock (such as when adding a unique item from someplace else).

:::caution
If you `Save()` the shop before you `Show()` it, the player's changes will not be committed to save data, since they will modify it _after_ you saved it. This may or may not be what you want.
:::

<LuaReference group={ApiGroup} />
