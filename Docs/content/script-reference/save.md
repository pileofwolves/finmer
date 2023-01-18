---
sidebar_position: 6
---

import LuaReference from '@site/src/components/LuaReference';
import ApiGroup from '@site/src/scriptref/Save.json'

# Save Data

These functions allow you to create save data. When save data is loaded, the game will load the scene that was active when the save was created, and resume the scene from the specific State or Choice node that created the save data. In other words, the script that caused save data to be created, will run again from the top. (The save dialog will not be presented again.)

If you're looking for the functions for changing the actual save data contents, check [Variables & Persistent Storage](/script-reference/storage).

<LuaReference group={ApiGroup} />
