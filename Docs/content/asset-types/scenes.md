---
sidebar_position: 1
---

import CaptionedImage from '@site/src/components/CaptionedImage';

# Scenes

The Scene system is the heart of the game. Scenes dictate what happens in the game, and so all other content comes together here.

## About Nodes

In Finmer, Scenes are made up of _nodes_. They represent actions, branches and gameplay logic. You can think of each _node_ as a single decision made in, or by, the game world. Nodes often contain other nodes ('children'), forming a tree-like structure. Nodes may have scripts attached to them that make things happen in-game.

There are several types of nodes; these four are what you will use most often:

- 💬 **Choices** are options available to the player. In the game, these are the buttons at the bottom of the screen. Players may click any one of the provided Choices. In the editor, Choices have a speech bubble as icon.
- 📃 **States** are groups of Choice nodes. They represent a response, or outcome, of the player's Choice. In the editor, States have a paper sheet as icon.
- 🧭 **Compasses** add a compass button to the left side of the screen. They can either link directly to a Scene, or can have a custom script that gets run when the player clicks the compass button. A Compass can only be on a State node.
- 🔗 **Links** are 'teleports' to other nodes, allowing you to jump to a different place in the node tree. This is useful for looping scenes, like dialogue trees, or reusing the same content multiple times. Links may replace States, Choices or Compasses anywhere in the tree, and they will function as if the link's target was actually there. In the editor, Links have a chain as icon.

<CaptionedImage
  src={require("/images/SceneNodeExample.png")}
  alt="Example scene layout"
  caption="An example scene. All four basic node types are shown. Note how 📃 States lead to 💬 Choices, and 💬 Choices lead to 📃 States." />

In addition, there are two special types of nodes that you will use less commonly:

- ⚓ **Roots** are the top-most node in every scene. There is only ever one Root in any Scene. It contains all the possible initial States that the Scene can start with. The Root also contains the Patch and Game Start configuration, if relevant.
- 🛠️ **Patches** are special nodes that can add, remove or rearrange nodes in other scenes. These are mostly useful for mods that 'patch' content in other modules, like adding content to the official game. For more info, check the Patches section below.

States and Choices alternate each other in the Scene tree: States contain Choices, and Choices contain States. Players can pick a Choice node that they like, and the game subsequently picks the next State in response. Then the children of that State, which will be Choice nodes, are in turn presented on screen.

States may not contain States, and Choices may not contain Choices, since that would break this 'call and response' flow style.

:::tip
To quickly create a Link, **right-click** the link target node, then drag-and-drop on the node where you want the Link to be.
:::

## Unique Keys

All nodes may optionally have a short name, called a **unique key**.

This key serves as an ID within the game engine itself. They are also important for Links and Patches: to target a node to link back to or edit with a patch, you use its unique key to tell Finmer where to look. For example, if you have a State named 'HelloWorld', then a Link that wants to go there must specify 'HelloWorld' as its target.

All keys in a scene must be unique within that scene; the game will raise an error if you have two nodes with the same key.

:::tip Keys are optional
To help simplify content creation, you may leave the unique key field empty. In this case, the game will auto-generate a key behind the scenes. However, keep in mind that a node without a unique key cannot be targeted by a Link or a Patch.
:::

You can name nodes whatever you like using alphanumeric characters (A-Z, 0-9) and underscores. Consider, however, keeping them organized, to help future you maintain your work. For example, you might want to group nodes related to the same topic together using a common prefix.

## 🛠️ Patch Groups

**Patches** can be used to inject new scene nodes into a scene asset from a different module. You can use this to add, delete, or replace chunks of content with modified versions. Patches are organized into **Patch Groups** that all modify the same scene in some way.

To create a Patch, select the Root node of an empty Scene, and tick the **Is Patch Group** check box. Click on the **Target Scene** link that now appears, and select the Scene that you will be modifying with this Patch Group. Usually, this would be a Scene in another module - don't forget to configure a dependency in your Module Settings, so that its Scenes show up in the list.

In your new Patch Group, while you have the Root node selected, the **Add Node** button on the toolbar changes into **New Patch**. Click it to add any of the below types of patch to this group. All such patches in the group will modify the same **Target Scene** that you selected earlier; this allows you to keep them grouped and organized.

![Types of patches](/images/ScenePatchTypes.png)

Patch Type              | Description
---                     | ---
Add/Insert Nodes        | This patch inserts new node into the target scene, relative to a target node. You can add things before, or after, or inside (as a child node).
Replace Node            | This patch completely replaces a node in the target scene with your version. You may choose if you want to keep the target node's children (if any), or delete them.
Remove Nodes            | This patch deletes a node, and all its child nodes, from the target scene. Note that if there are any Link nodes anywhere that reference the deleted nodes, they will break and the game will not start.

Future versions of Finmer may add new types of patches.

:::tip Referencing your patch target
While working on patches, it may be useful to have the module you're patching open in a second Editor window. That way, you can more easily see what a good target node would be for your patch, as well as how it will influence the nodes around it.
:::

## 🏁 Game Start Locations

**Game Starts** represent locations where the player can start a new story. You can use this to allow players to start a new game entirely within your custom mod (such as a total conversion mod) without ever having to go through the main story from the Core module.

To create a new Game Start, select the Root node in a scene. In the options for the Root node, tick the **Is Game Start** check box.

If there are multiple Game Starts in loaded modules, the game will allow the player to choose which one they want to play. Set the **Game Start Description** to a brief description of the story or the start location; this is shown to the player in the dialog where they choose which story they wish to start.

![Game Start setup](/images/SceneGameStart.png)

## Scripting with Scenes

### Node Scripts

Each node may optionally contain either or both of two special scripts. In short:

- **Actions Taken** is run whenever that Choice is selected, or State is activated. This is where you usually put actions such as adding text to the window, opening a shop, starting combat, etc.
- **Appears When** is run whenever the game evaluates whether or not to use this node. This script determines whether (for Choices) to show the button or not, or (for States) to select this State or not.

The **Actions Taken** script is a chunk of logic that is run whenever the node is 'selected': when a Choice is clicked, or a State is activated.

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
