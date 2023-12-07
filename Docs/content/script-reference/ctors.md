---
sidebar_position: 2
---

import LuaReference from '@site/src/components/LuaReference';
import ApiGroup1 from '@site/src/scriptref/CtorsAsset.json'
import ApiGroup2 from '@site/src/scriptref/CtorsBuff.json'

# Constructors

With these constructors, you can create instances of objects. These are used in various other functions; for example adding a participant to a combat session requires a Creature.

## Assets

These functions create instances of assets that you created in the Editor. The name specified in these constructors must be the unique asset name, as it appears in the editor's asset list.

<LuaReference group={ApiGroup1} />

## Buffs

These functions create instances of PendingBuffs, describing temporary effects that can be applied to combat participants. Check the Combat or Player documentation to see how to apply them.

<LuaReference group={ApiGroup2} />
