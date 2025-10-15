---
sidebar_position: 6
---

import LuaReference from '@site/src/components/LuaReference';
import CaptionedImage from '@site/src/components/CaptionedImage';
import ApiGroup from '@site/src/scriptref/Effect.json'

# Visual Effects

With these functions, you can display on-screen effects and animations.

Due to the number of available customization options, these are more complicated APIs. To help show how to use this library effectively, examples are provided at the bottom of the page.

<LuaReference group={ApiGroup} />

## Example

An example usage of `ShowOpposedDiceRoll`, demonstrating all possible parameters. Note that several parameters are optional; refer to the above layout documentation for details.

```lua
local my_instigator = Player -- Remember, Player is a Creature
local my_target = Creature("CR_SQ01_Alchemist")

Effect.ShowOpposedDiceRoll {
    instigator          = my_instigator,
    target              = my_target,
    label_instigator    = "Attack",
    label_target        = "Defense",

    -- For this string table key, the grammar contexts 'attacker', 'defender', 'predator' and
    -- 'prey' are available. 'attacker'/'predator' is the instigator, 'defender'/'prey' the target.
    log_key             = "ATTACK_HIT",

    -- At least 1 DieRound is required, but you can provide as many more as you want.
    rounds = {
        {
            -- List the individual D6 dice values as array elements, in the order they should be shown.
            -- Here, the instigator rolls six dice total, with each possible die face from 1 to 6.
            -- The target rolls two dice total, each with a value of 3.
            instigator_dice = { 1, 2, 3, 4, 5, 6 },
            target_dice = { 3, 3, hostile = true },
        },
        {
            -- Any DieList can be tagged with 'hostile = true' to change the die face colors.
            instigator_dice = { 6, 5, 4, 3, 2, 1, hostile = true },
            target_dice = { 1, 2, 3, 4, 5, 6 },
        }
    }
}
```

The first round of the above example might be displayed as follows:

<CaptionedImage
    src={require("/images/OpposedDiceRollExample.png")}
    caption="Note how Jar'la's dice are colored red, due to the 'hostile' flag in the die list." />
