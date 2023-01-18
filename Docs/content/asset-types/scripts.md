---
sidebar_position: 6
---

# Scripts

Scripts are blocks of Lua code that are loaded and run by the game engine as-is.

Whenever the player starts a new game - whether by creating a new character or loading save data - all Script assets are executed immediately in the global context.

Advanced users may find them useful for declaring global variables and functions. Anything that is defined in a Script asset, such as a function, will be available anywhere else in the game, such as in a Scene node. This makes them perfect for defining snippets of custom logic in one place, so that you do not need to duplicate them every time you need them.

:::tip
You can also embed custom code at the start of a specific scene, if you want.

To do this, go to the Editor Preferences, and tick the 'Show custom scene script header editor' option. The 'Custom Script' button will then show up in the Scene Editor. You may need to close and reopen the Scene asset. Anything written in this script can be referenced by scene nodes as globals or upvalues (if you declare them as `local`).
:::

## Example

For example, you might have a Script asset that exports a function, like so:

```lua title="MyScriptAsset" showLineNumbers
-- Roll 'count' D6 dice, and return the sum.
function RollSomeDice(count)
	local total = 0
	for i = 1, count do
		local roll = math.random(1, 6)
		total = total + roll
	end
	
	return total
end
```

You could then call this function from anywhere, like in an 'Actions Taken' node script in a scene:

```lua
-- Roll and sum the dice
local my_roll = RollSomeDice(5)

-- Show the result on screen
LogRaw("I'm rolling 5 dice... the total is: " .. my_roll)
```