---
slug: /category/script-reference
---

import DocCardList from '@theme/DocCardList';

# Script Reference

This is reference documentation for all public functions and properties exposed by the Finmer game engine. When writing Lua code for your own scenes and scripts, refer to this documentation for details on function names, signatures and the like.

If you intend to use the **visual scripting feature, you do not need to consult this script reference**. Create a Visual Script instead of a Raw Script when the Editor asks which one of the two you wish to pick.

**Feature requests are welcome!** If you'd like to implement behaviors that Finmer does not (fully) support, please feel free to reach out in the community - maybe a new system could be built to help support your use case!

:::caution Compatibility Warning
The game engine may or may not expose additional functions or properties that are not included in this script reference. Usage of such undocumented features is not supported; they may change or break without notice in future updates.
:::

## About Lua

Lua is a script language commonly used in games. If you would like to learn it, there are lots of resources available online for you to peruse. You can find the official [Lua 5.1 reference manual here](https://www.lua.org/manual/5.1/manual.html), and a large directory of [user-made tutorials here](http://lua-users.org/wiki/TutorialDirectory).

The game makes available for you most standard Lua libraries. You can use the basic standard library, as well as the `coroutine`, `table`, `string`, `math` and `bit32` libraries. Note that for security reasons, the `io`, `os`, `package` and `debug` libraries are unavailable.

## Reference pages

The API is split up into these categories:

<DocCardList />
