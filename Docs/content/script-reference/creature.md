---
sidebar_position: 31
---

import LuaReference from '@site/src/components/LuaReference';
import ApiGroup from '@site/src/scriptref/Creature.json'

# Creature Interface

These functions and properties are members of Creature instances. You can create an instance of a Creature using the `Creature()` function (see [Asset Instantiation](/script-reference/ctors)). The player character - obtained using the `Player` global variable - is also a Creature, so you can use the same properties.

Note that a Creature object is also a GameObject, so you can use the [GameObject interface](/script-reference/gameobject) as well.

<LuaReference group={ApiGroup} />
