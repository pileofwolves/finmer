---
sidebar_position: 1
---

# About Modules

The game uses _assets_ to store information about the game world. Things such as items, creatures and places are _assets_. These are what you build in the Finmer Editor.

A group of assets together forms a _module_. In the Editor, you can open and edit one _module_ at a time. For example, the 'Core' _module_ contains all the content I made for the game.

The module system is designed to be **plug-and-play**. To 'install' custom content, all you need to do is drop the packaged module in the game's `Modules` folder, and the game will automatically load it on startup. This makes it easy to distribute your custom content and modifications to others, should you choose to do so.

## Flavors of Asset

:::tip
The 'Your First Quest' tutorial will walk you through the important info about all of these assets one at a time, so feel free to just move on to the next page. The links below take you to detailed documentation, in case you'd like to browse anyway.
:::

An asset can be any of the following types. Click the name of any asset to view the detailed documentation.

- **[Scenes](/asset-types/scenes)** describe an in-game location, event or dialogue tree. They state what happens when, and the options the player can click.
- **[Items](/asset-types/items)** describe anything a Creature can hold or equip. This includes consumables, quest items and equipment.
- **[Creatures](/asset-types/creatures)** describe a template for something that can participate in combat. These are only used for the battle system, 'real NPCs' just exist in Scenes.
- **[Journals](/asset-types/journals)** describe quest logs that can be shown in the player's in-game journal.
- **[String Tables](/asset-types/string-tables)** contain game text. Groups of strings are assigned to a _key_, and support built-in randomization.
- **[Scripts](/asset-types/scripts)** are raw Lua scripts. They are primarily useful for defining global tables and common functions.
