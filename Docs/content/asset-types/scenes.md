---
sidebar_position: 1
---

import CaptionedImage from '@site/src/components/CaptionedImage';

# Scenes

The Scene system is the heart of the game. Scenes dictate what happens in the game, and so all other content comes together here.

## Nodes & Scene Tree

In Finmer, Scenes are made up of _nodes_. You can think of each _node_ as a single decision made in, or by, the game world.

Each node can contain scripts that determine what happens if that node is picked, and whether or not that node will be shown in-game. For reference, the Scene editor is heavily inspired by the dialogue editor from the old Neverwinter Nights game.

There are four types of nodes:

- 💬 **Choices** are options available to the player. In the game, these are the buttons at the bottom of the screen. A Choice will generally lead to another State. In the editor, Choices have a speech bubble as icon.
- 📃 **States** are groups of Choice nodes. Think of them as the game's response to a player's Choice. Each State may contain any number of Choices. In the editor, States have a paper sheet as icon.
- 🧭 **Compasses** add a compass button to the left side of the screen. They can either link directly to a Scene, or can have a custom script that gets run when the player clicks the compass button. A Compass can only be on a State node.
- 🔗 **Links** are 'teleports' to other nodes. These are useful to avoid duplicating the same nodes over and over, and to make it possible to return to an earlier point in the tree. Links may replace States, Choices or Compasses anywhere in the tree, and they will function as if the link's target was actually there. In the game, these are invisible to the player. In the editor, Links have a chain as icon.

<CaptionedImage
  src={require("/images/SceneNodeExample.png")}
  alt="Example scene layout"
  caption="A simple scene. All four node types are shown. Note how States lead to Choices, and Choices lead to States." />

States and Choices alternate each other in the Scene tree. Each State may contain a number of Choices, and each Choice may contain a number of States.

The idea is that the player can click on any Choice they like, and the game subsequently picks the next State in response. Then the new set of Choices are shown, and we begin again. It is not possible for States to contain States, or for Choices to contain Choices, since that would break this 'call and response' flow style.

:::tip
To quickly create a Link, **right-click** the link target node, then drag-and-drop on the node where you want the Link to be.
:::

## Unique Keys

All nodes have a unique key. This key is used by the game internally to refer to the nodes, and the keys are what Links use to know where they lead. For example, if you have a State named 'HelloWorld', then a Link that wants to go there must specify 'HelloWorld' as its target. All keys in a scene must be unique within that scene; the game will give you an error if you have two nodes with the same key.

:::tip
**Keys are optional.** You may leave the unique key field empty; in this case the game will automatically generate one for you behind the scenes. While this simplifies the editing process, keep in mind that a Link can only point to a node that has a proper key assigned.
:::

You are free to name them whatever you want, but I recommend keeping them at least somewhat organized. For example, you might want to group nodes related to the same topic together using a common prefix.

## Special Types of Scenes

When selecting the Root node of the scene node tree, two options appear that specialize the role of the scene. Only up to one of these can be selected at a time.

### 🛠️ Patches

Patches can be used to inject new scene nodes into a scene asset from a different module. For instance, you can use this to patch in new areas or dialogue trees into the Core module, adding them on to the main game. Or you can patch scenes from other people's mods.

To use this feature, tick the 'Is Patch' check box on the Root node. Select the scene you wish to add nodes on to, and then choose the injection point, which tells Finmer how exactly to inject your custom nodes. The target node, where your new nodes will be added, is identified with its Unique Key. It may be beneficial to open the module you're patching in the Editor, so you can see more clearly what a good target node would be for your edit.

Injection Mode                      | Effect
---                                 | ---
Before (as States)                  | The patch will add new States _before_ the target injection point, forcing the game to select them (per their Appears When script) before the target.
After (as States)                   | The patch will add new States _after_ the target injection point. They can only be selected if the Appears When script of the target evaluates to false.
Inside, at the start (as Choices)   | The patch will add new Choices before all existing ones.
Inside, at the end (as Choices)     | The patch will add new Choices at the end of the existing ones.

It is currently not possible to _replace_ Actions Taken or Appears When scripts on existing scene nodes. However, you could add new nodes with scripts that replace the old ones, and inject your custom nodes such that they appear before the base module has a chance to show the originals.

### 🏁 Game Start Locations

Game Starts represent locations where the player can start a new story. You can use this to allow players to start a new game entirely within your custom mod (such as a total conversion mod) without ever having to go through the main story from the Core module.

If there are multiple Game Starts in loaded modules, the game will allow the player to choose which one they want to play. Set the 'Game Start Description' to a brief description of the story or the start location; this is shown to the player in the dialog where they choose which story they wish to start.

## Scripting with Scenes

### Node Scripts

Each node may optionally contain either or both of two special scripts. In short:

- **Actions Taken** is run whenever that Choice is selected, or State is activated. This is where you usually put actions such as adding text to the window, opening a shop, starting combat, etc.
- **Appears When** is run whenever the game evaluates whether or not to use this node. This script determines whether (for Choices) to show the button or not, or (for States) to select this State or not.

The **Actions Taken** script is simply a chunk of logic that is run whenever the node is 'selected': when a Choice is clicked, or a State is activated.

The **Appears When** script determines whether the node may be 'shown'. For Choices, that means whether the button will be shown at the bottom of the screen. For States, that means whether this State may become active (see below). Its output should be `true` to show or activate the node, `false` to hide it. This makes it possible to have the scene change dynamically. You can easily enable or disable entire dialogue paths.

<CaptionedImage
  src={require("/images/SceneNodeScripts.png")}
  caption="A node may have a script on it that does things like showing text, or removing inventory items." />

Links cannot have scripts themselves. Instead, the scripts of their respective link targets are reused, if any.

:::note
It often doesn't matter whether you put your script in the Choice or in the State, as both will be executed. Because a State represents the game's 'response' to your choice, I prefer putting actions in the State. However, if you Link back to that State such as in a dialogue tree, putting the script in the preceding Choice prevents it from being run again when the Link is followed.
:::

After a Choice is selected, the game will go over all child States, in order from top to bottom (as seen in editor). The first State whose Appears When function evaluates to `true`, or has no Appears When function at all, will become the new active State.

For Choices, it's a bit simpler: every child Choice whose Appears When function evaluates to `true` (or doesn't have one at all) will appear at the bottom of the screen.

### Scene-Wide Scripts

A Scene may optionally have these scripts:

- **Enter Script** runs whenever the player enters this scene. It runs after the previous scene's Leave Script, and before this scene's first State. Think of it like an 'Actions Taken' for the Root node.
- **Leave Script** runs whenever the player leaves the scene. It'll run right before the next scene's Enter Script. You probably rarely need to use Leave Scripts, but if you need to clean something up, here's the place to do it.

<CaptionedImage
	src={require("/images/SceneScriptButtons.png")}
	caption="Click these buttons at the top of the Scene Editor to edit these scene-wide scripts." />

Additionally, for advanced users, it is also possible to have custom Lua code in the scene scope. To create a Custom Script, you must first enable the 'Show custom scene script header editor' option in the Editor Preferences, for the button to show up in the Scene Editor. (It is not shown by default to help simplify the editor UI for newcomers.)

- **Custom Script** will be inserted in the outermost scope of the scene script. It's useful for declaring variables and common functions. Stuff you declare here is accessible in any other part of the scene. Avoid interacting with gameplay and UI systems in the main body of this script; their state is undefined at the time the script runs. Using the `Storage` API is fine.
