---
sidebar_position: 3
---

# Creatures

Creatures are things that can participate in the combat system. Each Creature has Health, stats determining combat effectiveness, and a set of flags that may override combat behavior. While Creatures are generally AI-controlled NPCs, note that the Player is also a Creature.

:::note
Creatures are only relevant for the combat system. Dialogue trees with NPCs are built with Scenes instead.
:::

## Properties

The following properties are available when editing a Creature: 

Property|Description
---|---
Name|The canonical name of the object, as it appears in the combat sidepanel, e.g. "Bob".
Alias|Alternate name of the object, as it is used in dialogue and combat, e.g. "the dingo".
Gender|Determines which pronouns are used. Cosmetic only.
Level|Base level. Determines the XP value of the Creature when defeated.
Size Class|Creatures can only grapple or swallow Creatures of equal or smaller size. In vore checks, if the pred is larger than the prey, they need to win fewer rounds. Player is Medium-sized.
Strength|Determines number of Attack Dice and Grapple Dice.
Agility|Determines number of Defense Dice and Struggle Dice.
Body|Determines number of Health Points and Swallow Dice.
Wits|Determines turn order. Highest Wits goes first.
Behavior Overrides|Change the Creature's behavior in combat. See below for details.
Vore Settings|Determine which vore-related actions this Creature may take. See below for details.
Script Tags|Default set of tags, one per line. For advanced users - to be used with the HasTag() function.
Equipment|Sets a number of items the Creature has equipped in combat. The alias of the item in Slot 1 is used to describe the NPC's weapon in combat text; the order does not matter otherwise.
String Mappings|Rules for replacing string table keys with different ones. This is what you use to implement creature-specific combat text, like for hitting, missing, vore, etc. More on this below.
Asset GUID|Unique ID number of the asset. For advanced users.

## Behavior overrides

The following options are available to change a participant's general behavior in combat:

Option|Description
---|---
Skip All Turns|Never take any actions in combat.
Cannot be Attacked|Forbids other Creatures from directly attacking this Creature.
Cannot be Grappled|Forbids other Creatures from initiating a Grapple with this Creature.
Cannot be Vored|Forbids other Creatures from swallowing this Creature.
Does Not Give XP|When defeated, does not award XP.

## Predator settings

The following options are available to customize a predator's behavior in combat.

:::caution
If you turn off 'Digests Prey', swallowed prey will be stuck forever, because in the standard combat system prey cannot escape once swallowed. Therefore, you must manually script a different method for combat to end.
:::

Option|Description
---|---
Is Predator|Whether this Creature will attempt to swallow enemies.
Digests Prey|Whether swallowed prey will take digestion damage.
Has Disposal Scene|If the player is digested by this Creature, and the player has disposal enabled in the game settings, prints the `VORE_DISPOSAL` string. (Use String Mappings to customize the text.)
Swallow Defeated Player|If player dies and is not swallowed, this Creature immediately swallows the player.
Swallowed When Defeated|If killed while not swallowed, the player immediately swallows this Creature.
