---
sidebar_position: 16
---

import LuaReference from '@site/src/components/LuaReference';
import ApiGroup from '@site/src/scriptref/Storage.json'

# Variables & Persistent Storage

These functions can be used to store data and keep it around between scenes. The key/value pairs are saved with the player's savegame. Because all Scenes each have their own self-contained script environment, this is the one and only way to communicate data between two Scenes, or to remember data the next time the same Scene is visited.

Keys must be unique (as they identify the thing you want to save or load), but are not case-sensitive (e.g. 'MyNumber' and 'mYnUmBeR' are the same).

<LuaReference group={ApiGroup} />
