---
sidebar_position: 5
---

import LuaReference from '@site/src/components/LuaReference';
import ApiGroupBasic from '@site/src/scriptref/CombatBasic.json'
import ApiGroupState from '@site/src/scriptref/CombatState.json'
import ApiGroupCallback from '@site/src/scriptref/CombatCallback.json'

# Combat System

Functions relating to the combat system. You can start and stop fights, and manipulate and inspect their state.

## Basics

The basic flow is that you create a new session using the `Combat2` function, add some participants, and then start it with the `Begin` method.

A newly created session does not contain any participants at all, not even the player character (whom you can add with the global variable `Player`). You could, if you want to, forego adding the player to a combat session, and have only AI-controlled participants fight it out.

NPC participants can be created by using the `Creature` function, described in the [Constructors](script-reference/ctors.md) section.

<LuaReference group={ApiGroupBasic} />

## Participant State

Use these functions to force characters into specific states, or to inspect their current state. This is useful to, for example, force a character to already be swallowed by some predator before the combat starts.

Note that each Combat Session is completely separate and self-contained. A participant could, for example, be grappling with another participant in one session, and be swallowed by that same creature in another session. In other words, combat-related states like grappling, swallowing, stunning and the like are 'forgotten' as soon as the combat session ends. It is up to your scene scripts to figure out how participants have interacted with each other, so that the scene can respond to the combat's outcome appropriately.

:::caution
Be sure that all Creatures you pass to these functions, are already registered to this combat session using `AddParticipant`. If this is not done, the functions will raise an error.
:::

<LuaReference group={ApiGroupState} />

## Callbacks

You can use these functions to have the combat system inform you of certain events. For example, you can have a snippet of Lua code run every time a round ends, or whenever someone is swallowed by a predator. This is useful for scripting combat sequences beyond what the vanilla combat system normally allows.

<LuaReference group={ApiGroupCallback} />
