---
sidebar_position: 90
---

# Packaging Modules

When you've finished working on your content and you would like to publish it so that others can give it a try, it is time to package your module into what's called a _furball_.

:::tip
Feel free to skip this page and come back later if you're not yet looking to publish your work.
:::

## Flavors of modules

As [described earlier](/getting-started/about-modules), a _module_ is a collection of _assets_. A module can be stored in one of two ways: as a _project_ or as a _furball_. The two formats have different purposes, so let's look at each of them.

### What is a _project_?

When you work on a module in the Editor, you work with a _project_ most of the time. When you save the project file, the Editor creates many small text files alongside the project file itself - one for each asset you created. These text files contain all your work as human-readable text.

This method of storage makes it very easy to back up and partially restore your project from said backup, or, for advanced users, hand-editing files. This also works well with source control systems like Git, should you choose to use that.

The downside of this format is that it is rather bulky. Because it consists of so many loose files, it is tricky to copy around, and very inefficient for the game to load.

### What is a _furball_?

To solve this, there is the _furball_ format. A _furball_ is a compact single file that contains an entire module. Think of it like **a zip file that contains your project**. It is optimized to load quickly, and because it's just one small file, it's super easy to distribute to other players.

To let others enjoy your custom content, simply post the furball file in the community forum, or your favorite Discord chat or file sharing site, etc. All the player needs to do to 'install' your content, is dropping the furball file in their Finmer's `Modules` folder. The game will then load it automatically.

:::info In short
The _project_ is what you make in the Editor, and it is then used by the Editor to create the _furball_ file that the game works with.
:::

## Saving your module

When you save your project in the Editor, it will also automatically save such a furball file in the game's Modules folder. This enables quick testing: when you save your work, you can immediately click the Play button and get into the game without further steps.

You can also manually export your project as a furball by clicking the `Publish Furball` button on the Toolbar. Simply choose a location on your PC and the Editor will save a furball there, ready for distribution.

### Importing furballs

You can also extract the contents of a furball back into a project. Doing so will allow you to open it in the Editor and see how everything was set up by the author.

To extract a furball, click the `Extract Furball` button on the Toolbar. Note that this will automatically close any other project you might have open already.
