---
sidebar_position: 15
---

import LuaReference from '@site/src/components/LuaReference';
import ApiGroup from '@site/src/scriptref/Save.json'

# Save Data

These functions allow you to create save data. When save data is loaded, the game will load the scene that was active when the save was created, and resume the scene from the specific State or Choice node that created the save data. In other words, the script that caused save data to be created, will run again from the top. (The save dialog will not be presented again.)

If you're looking for the functions for changing the actual save data contents, check [Variables & Persistent Storage](/script-reference/storage).

Note that if the user creates a manual save using the save menu presented by `SaveData.ShowSaveDialog()`, any checkpoint save data created using `SaveData.TakeCheckpoint()` is deleted. This is done to avoid the potentially confusing scenario where the user could be led to believe that their checkpoint is more recent than their last manual save.

<LuaReference group={ApiGroup} />
