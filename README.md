# FINMER

[![Build](https://github.com/pileofwolves/finmer/actions/workflows/ci.yml/badge.svg?branch=master)](https://github.com/pileofwolves/finmer/actions/workflows/ci.yml)

Welcome to Finmer, an 18+ interactive text adventure.

## :warning: CAUTION

This repository contains adult fiction and fantasy content. Proceed at your own risk. If you stumbled across this project by accident, chances are you're not interested in the subject matter and should move on. :)

## LATEST RELEASES

**If you simply wish to play the latest stable version**, please check the Releases tab, or visit my [FurAffinity page](https://www.furaffinity.net/user/nuntis/) for downloads.

## LICENSE

The program source code is licensed under the [GNU General Public License v3 (GPL3)](LICENSE.md).

Game content files and assets included in this repository (including files in the Modules folder, text, scripts, images, audio, video) are licensed under Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License ([CC BY-NC-SA 4.0](https://creativecommons.org/licenses/by-nc-sa/4.0/)).

In short, this means modifying and redistributing the project (with source included) as you like is generally fine, however you are *not* allowed to redistribute *for commercial purposes* the game content files (as defined above) included in this repository.

Some fictional characters appearing in this project belong to others, and have been implemented with written permission from their respective owners.

## COMPILING

### Prerequisites

Finmer currently only officially supports Windows. To build the game from source, the following prerequisites are needed:

- Visual Studio 2022 or newer
- .NET Framework 4.8 SDK and targeting pack
- C++11 compiler or newer
- C# 7.3 compiler or newer

### Building from source

To generate executables, open the `Finmer.sln` solution file in Visual Studio and build all projects. The output executable files will be placed in their respective projects' `bin` folder, and can either be run directly or launched using Visual Studio as desired.

To run the game, you'll need to prepare content package files (furballs) for the game to load:

1. Run the **Finmer.Editor** program and open the Core module (located at `/Modules/Projects/Core/Core.fnproj`).
2. Once the project has loaded, click the Preferences button (on the Play section on the toolbar) and set the Working Directory to the repository root. This will ensure the editor knows where to find the Modules folder. You only need to do this once; the editor will save this info for the next run.
3. Click the 'Save Module' button on the toolbar to generate furball files suitable for the game engine. They'll be placed in the Modules folder.
4. Close the editor, and launch **Finmer.Game** to play.

### Getting started

Check [CONTRIBUTING.md](.github/CONTRIBUTING.md) for more info on running the project and making changes.

## PROJECT OVERVIEW

The project structure is as follows:

- **Finmer.Core** is a library containing functionality shared by the game and editor, or objects that can/should be abstracted away from applications.
- **Finmer.Game** is the main game (a WPF application), and contains all UI and gameplay code.
- **Finmer.Editor** is a WinForms application that enables editing of modules (game content files).
- **External** contains third-party libraries.
- **Modules** contains all game content files, as well as the editor documentation and example modules.

## CONTRIBUTING

Issue reports and code pull requests are welcome! Please check [the contribution guidelines](.github/CONTRIBUTING.md) for more info and tips on getting started.

PLEASE NOTE: This repository is *not* the right place to make suggestions or requests for entirely new content. Pull requests that add entirely new content (other than fixes or tweaks, of course) are not accepted at this time. If you wish to make such suggestions, please get in touch by creating an issue here, or on FurAffinity or email (nuntis@finmer.dev) to discuss.

## MAKING CONTENT / USING THE EDITOR

Please refer to the [editor documentation](https://docs.finmer.dev) for detailed information on the workings of the Editor, as well as a Lua script reference.

Your custom modules and/or game content files belong to you; you are free to copy and distribute them as you see fit.
