---
sidebar_position: 11
---

import LuaReference from '@site/src/components/LuaReference';
import ApiGroup from '@site/src/scriptref/Rumor.json'

# Rumor System

Rumors are pieces of game text, usually dialogue snippets, that can be spoken by various innkeepers or shopkeepers around the game world. They have a number of uses: they help steer players toward new content they may not have explored yet, they provide an easy way for the world to 'react' to player choices, and (especially more general quips) provide a little context for the game world.

Mods can add or present rumors as needed. Please note that the entirety of the rumor system is part of the Core module, meaning that if you are trying to make a 'total conversion'-style mod that does not depend on Core at all, these functions will be unavailable.

## Usage Notes

Rumors are shuffled into a random order from which `Rumor.Get` and `Rumor.Present` will make selections. Once a particular rumor is shown, it will not be shown again until all other rumors have been exhausted; this helps keep the variety up.

Rumors have a relative weight factor, which represents the probability that that rumor will be chosen. It is relative to the weights of all other rumors, e.g. it represents a 'slice of the pie'. As an example, consider two rumors, one with a weight of 1 and another with a weight of 2, then the second rumor has a 66% probability of being selected. If you add a third rumor with a weight of 1, then the second rumor's selection probability drops to 50%, and the other two have 25% each.

:::note
When adding your own rumors, do so in a Script asset that calls `Rumor.Add`. Make sure your Script asset loads **after** the `Script_RumorSystem` script from the Core module; this will guarantee that the `Rumor.Add` function is actually available. You can do this by clicking the Load Order button in the top right of the script editor, and configuring it accordingly.
:::

## Reference

<LuaReference group={ApiGroup} />

## Example

As a quick example, add a `RUMOR_EXAMPLE_TEXT` string table set to any string table, and enter this text. Note the use of the `{mill}` grammar context; please consult the section on [Grammar & Contexts](/asset-types/string-tables#grammar--contexts) for information on how to use the grammar engine.

> "I heard something incredible last week," {mill} {mill exclaim} eagerly. "I forgot what it was, though."

Then hook it up in script, like so. Don't forget to add a Load Order dependency to load _after_ `Script_RumorSystem`, else the `Rumor.Add` function may not yet be loaded, leading to errors.

```lua
Rumor.Add {
	id		= "MyCustomRumor",			-- Internal ID; used only for Rumor.Remove
	text	= "RUMOR_EXAMPLE_TEXT",		-- The string table key that you just added
	weight	= 1,						-- Relative weight: higher number = more likely to get picked
	
	-- Optional! You may add a 'check' function if you wish to restrict when the rumor may be shown.
	-- This is useful to, for instance, show a rumor only before, or after, a certain quest was completed.
	check	= function()
		return Storage.GetFlag("MY_QUEST_COMPLETED")
	end
}
```

This custom rumor will now be available at various shopkeeps and bartenders throughout the Finmer world. For total conversion mods, be sure to add a rumor mill somewhere, e.g. an NPC or similar that calls `Rumor.Present` (or `Rumor.Get` for advanced uses) to display the rumors.
