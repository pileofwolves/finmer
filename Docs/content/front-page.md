---
slug: /
sidebar_position: 1
---

# Welcome

Hi, and welcome to this guide. You're about to make some content for Finmer, and that's awesome!

## What am I looking at?

Finmer is a **furry text adventure game** built with adult vore content and mod support in mind. If that doesn't sound like it's quite your cup of tea, or if you don't know what some of those labels mean, you should probably close this tab.

I have spent a sizable portion of my free time over the last several years building this game for your enjoyment, and I am humbled and honored by the fact that you're interested in adding some content of your own. I hope that this document provides all the information needed to get your paws dirty.

If you ever have any questions, concerns, suggestions or bug reports, you can contact me as ~nuntis on FurAffinity, or create an [Issue on GitHub](https://github.com/pileofwolves/finmer/issues), or via email at [nuntiswolf@gmail.com](mailto:nuntiswolf@gmail.com). I won't bite (but might swallow)!

## How to use this document

:::tip Want to get started quickly?
Start with the ['Getting Started' help pages (click here!)](/getting-started/about-modules) for an introduction to the basics of the Editor. You'll then be taken to a step-by-step guide to creating your first custom quest.
:::

The editor should be reasonably easy to get the hang of using this documentation. While some technical knowledge is **helpful**, you do not need to know the Lua scripting language in order to create content. A **visual-scripting feature is available**, allowing you to put together scripts using small building blocks.

In these pages, we will discuss each element of the Editor and the game. There's also **a step-by-step guide to creating an example module**.

Feel free to look at the small example projects bundled with this editor, or the Core module (which contains my own stuff), to see how some things are set up! 

### Notes for Lua Scripting

:::info There's a visual scripting feature, too!
**You do not need to know Lua to create content for Finmer.** If, however, you'd like to use the engine to its fullest potential, some scripting knowledge will be useful.
:::

Lua is a simple, elegant script language. If you would like to learn it, there are lots of resources available online for you to peruse. You can find the official [Lua 5.1 reference manual here](https://www.lua.org/manual/5.1/manual.html), and a large directory of [user-made tutorials here](http://lua-users.org/wiki/TutorialDirectory). Check the [Script Reference](/category/script-reference) to see what you can do with the game engine.

The game makes available for you most standard Lua libraries. You can use the `base`, `coroutine`, `table`, `string` and `math` libraries. Note that for security reasons, the `io`, `os`, `package` and `debug` libraries are unavailable. 
