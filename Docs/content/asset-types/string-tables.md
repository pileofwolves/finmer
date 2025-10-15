---
sidebar_position: 5
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

# String Tables

String Tables contain game text, for display to the player. A table consists of a list of _sets_, each with one or more pieces of text in it.

## Keys

Each _set_ has a unique ID called a _key_. You can reference the text elsewhere, such as when using the 'Show Message' script action, by specifying its _key_. This allows text to be reused in multiple places, and allows for neat features like randomization (see below).

Keys must be globally unique - that is, across all loaded modules. If any two sets have the same key, **they will be merged together**. They are case-insensitive, and there are no particular rules for what a key must look like.

:::tip
Try keeping keys short and recognizable. Consider prefixing them with a short context or 'topic', to clearly group related sets together: `TOWN_INN_ENTER`, `TOWN_INN_FOX_TALK`, `TOWN_INN_FOX_KISS` etc.
:::

## Editor Tools

The string table editor features the list of sets on the left, and the contents of the selected set on the right.

![String table editor layout](/images/StringEditor.png)

Click on a set in the left list to show its entries. Each new line of text is interpreted as a separate entry in the set. Double-click a set to change its name. Use the `Add Set` / `Remove Set` buttons to create/delete individual sets as needed.

The string table editor provides a couple of quality-of-life features to help implement bulk content quickly.

- Add Next Number in Set **(Ctrl+D)**: Adds a new set with the same key as the currently selected one, except any number at the end is incremented. So `TALK1` turns into `TALK2`, etc. If there is no number, one is added for you. Useful for quickly adding sequential chunks of text.
- Add Set with Same Topic **(Ctrl+T)**: Adds a new set with the currently selected key, stripped to the rightmost underscore. So `INN_TALK_FOX` turns into `INN_TALK_`, and when clicked again, that turns into `INN_`. Useful for adding closely related text, saving you some typing.

## Randomization

Whenever a string set is used, one of its entries will be chosen at random. Each new line in the Editor represents one such entry. In other words, if you have a string set that looks like this:

```
apple
banana
pear
```

... then whenever you use the 'Show Message' script action (or, in Lua, the `Log()` function), one of those three fruits will be chosen randomly. This automatic, built-in randomization is very useful to help easily keep things fresh. By writing variations of the same text, the player won't keep seeing the exact same thing over and over, which becomes boring very quickly.

:::note
You do not need to write variations if you do not want to - just one entry in a set is fine too. If a set contains only one entry, that one will just always get picked.
:::

As a concrete example, here is the string set from the Core module, used when the player approaches the fish vendor in the market. Any one of these four lines will be shown to the player.

```
You see the red fox from before just finishing up a deal with another customer, then he turns to you, smirk and all.
The red fox at the fish stall seems to be a bit bored and doesn't even notice you until you're right next to him. He yips but quickly composes himself.
The red fox grins as he spots you in the vicinity again. "You keep coming back, I see."
The fish merchant inspects the wares behind him, then turns back to the market street, apparently satisfied.
```

One more thing: note that because of this system, a regular line break will not show up correctly, since the two lines will be treated as separate entries in the set. If you really want a line break, write `\n` (backslash followed by lowercase n) instead - it will be replaced by the game.

## Grammar & Contexts

Finmer knows some simple English grammar: it supports conjugating verbs as well as substituting pronouns. When a string is picked from a set, such as when you use the 'Show Message' script action, various Contexts may be available to 'specialize' the string to the current situation. Here's how it works:

A _context_ is an object, like a Creature or Item, that is bound to a name. For example, in some combat-related text, the `attacker` context could refer to the Creature who is initiating an attack, and `defender` would refer to the Creature under attack. The grammar engine uses a context to determine how to write verbs and pronouns.

Specifically, the grammar engine's output **depends on the context's gender**. For Creatures, the gender is set by you in the editor. For the Player, they themselves set their gender in the game. For Items, the gender is always Ungendered (it/its).

You can invoke the grammar engine by placing a command of the form `{command}` in your text. (Curly braces, not round.) The engine will replace the command with the evaluated result. For example, `{attacker.possessive}` might be replaced with 'his' or 'her'. This system enables the **same text template to be reused for many different characters**, while still ensuring grammatical correctness.

The following commands are available for all objects:

- `{context}`, with no further commands, is a short version of, and identical to, `{context.alias}`.
- `{context verb}`, where verb is an English infinitive verb, like 'be' or 'eat'. The game will automatically conjugate the verb based on the gender of the context.
- `{context.name}` evaluates to the name of the `context`.
- `{context.alias}` evaluates to the alias of the `context`.
- `{context.object}` evaluates to `context`'s objective pronoun in the closest form possible. Examples: you, him, her, them
- `{context.subject}` evaluates to `context`'s nominative pronoun in the closest form possible. Examples: you, he, she, they
- `{context.possessive}` evaluates to `context`'s possessive pronoun in the closest form possible. Examples: your, his, her, their
- `{context.object3p}` evaluates to `context`'s objective pronoun in third person. Examples: him, her, them
- `{context.subject3p}` evaluates to `context`'s nominative pronoun in third person. Examples: he, she, they
- `{context.possessive3p}` evaluates to `context`'s possessive pronoun in third person. Examples: his, her, their
- `{context.aliaspossessive}` combines `alias` and `possessive`. Example: your, the dog's, Bob's
- `{context.namepossessive}` combines `name` and `possessive`. Example: Nuntis', Dog's, Bob's

Additionally, for Items, these properties are available:

- `{context.value}` evaluates to the value in coins of the item

Additionally, for the Player, these properties are available. You can always refer to the player anywhere, with `{player}`.

- `{player.species}` evaluates to the lowercase singular species name of the player ('dragon', 'tiger').
- `{player.speciesplural}` evaluates to the lowercase plural species name of the player ('dragons', 'tigers').
- `{player.fur}` evaluates to the noun describing the player's coat ('fur', 'scales').
- `{player.furry}` evaluates to the adjective describing the player's coat ('furry', 'scaly').

### Usage Example

In the combat system, the `ATTACK_MISS` string is used when an attack fails. The `attacker` and `defender` contexts are available. Consider this example string:

```
{defender} nimbly {defender dodge} {attacker.aliaspossessive} awkward punch, and {attacker.subject} {attacker hit} the wall instead.
```

which could evaluate to the following results:

```
You nimbly dodge Dave's awkward punch, and he hits the wall instead.
Dave nimbly dodges your awkward punch, and you hit the wall instead.
```

I'm sure you can see this feature being useful for avoiding duplicate text everywhere just to make the grammar work. :)

## Special Commands

### Uppercase Command

If the command starts with a caret (`^`), the first character of the evaluated text will be converted to uppercase. This is useful when the first word in a sentence is a grammar command - normally it would generate lowercase text, which would look like a mistake.

For example, these two strings:

```
You tap the merchant on the shoulder. {shopkeeper.subject} turns to face you.
You tap the merchant on the shoulder. {^shopkeeper.subject} turns to face you.
```

would be rendered as follows. Note how the first one, without caret, is grammatically incorrect.

```
You tap the merchant on the shoulder. she turns to face you.
You tap the merchant on the shoulder. She turns to face you.
```

### Randomizer Command

If the command starts with a question mark (`?`), the command is interpreted as a set of options, of which one will be chosen randomly. The next character must be a pipe (`|`), and from there, the different options are separated by a pipe (`|`).

You can have as many options as you like, and there is no limit to the length of each option. Nested grammar commands - e.g. having another command inside one of the random options - is currently not yet supported.

Like so:

```
My favorite fruit is {?|apple|orange|banana|pear}.
```

which could, randomly, be rendered as:

```
My favorite fruit is banana.
```

### Nested Key Command

If the command starts with an at-sign (`@`), the command is interpreted as a string table key. The key is looked up in string tables, resolved, and inserted in place of the command.

Any grammar tags and commands in the nested key are also resolved, including any additional nested key commands. Recursion up to 5 levels of depth is supported; deeper recursion will show an error message.

Example: If you have a string table key `EXAMPLE_INNER` with these entries:

```
always
never
```

then you can insert it into another string like so, randomly selecting one of the entries:

```
I {@EXAMPLE_INNER} put pineapple on my pizza.
```

### Variable Key Command

If the command starts with two at-signs (`@@`), the command is interpreted as a string variable name. The variable name is looked up (as if with `Storage.GetString`), and the resulting value is then interpreted as a string table key, like with the single-at-sign command.

Example: First set a string variable to `EXAMPLE_INNER` - the same key used in the above example - using a Lua script or the equivalent Visual Script command.

<Tabs groupId="script-lang">
<TabItem value="visual" label="Visual Script">
```finmervis
Set String Variable EXAMPLE_VARIABLE to "EXAMPLE_INNER"
```
</TabItem>
<TabItem value="lua" label="Lua Script">
```lua
Storage.SetString("EXAMPLE_VARIABLE", "EXAMPLE_INNER")
```
</TabItem>
</Tabs>

Afterward, it can be used like follows. The variable (`EXAMPLE_VARIABLE`) is substituted for its value (`EXAMPLE_INNER`) and that is then treated as a string table key to substitute into the text.

```
I {@@EXAMPLE_VARIABLE} put pineapple on my pizza.
```
