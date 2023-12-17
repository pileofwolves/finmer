---
sidebar_position: 12
---

import LuaReference from '@site/src/components/LuaReference';
import ApiGroup from '@site/src/scriptref/Encounter.json'

# Encounter System

Encounters allow for creating easily-moddable randomized events, such as a walk in the woods that could lead to any number of events occurring. You could add a new encounter with a wandering merchant, or perhaps a new monster encounter, or a new quest.

The encounter system works the same way as the [rumor system](/script-reference/rumor) - refer to that document for details on weights and the optional check functions.

:::note
When adding custom encounters, do so in a Script asset that calls `Encounter.Add`. Make sure your Script asset loads **after** the `Script_EncounterSystem` and `Script_EncounterList` scripts from the Core module, so that the encounter system is actually loaded and the built-in encounter groups (listed below) are loaded. You can do this by clicking the Load Order button in the top right of the script editor, and configuring it accordingly.
:::

## Reference

<LuaReference group={ApiGroup} />

## Built-in encounter groups

These encounter groups are used by the Core module. If you want to add encounters to the main game's locations, simply use one of the variable names listed below as the value for the `group` argument to `Encounter.Add`.

Of course, you do not have to use one of these groups; you can also create a new one if you like.

Variable Name                  | Description
---                            | ---
`k_EncounterGroup_Forest`      | Forests east of North Finmer.
