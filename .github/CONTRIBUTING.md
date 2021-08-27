# Contributing

Thanks for your interest in helping improve Finmer!

**This file is still a work-in-progress**, since there is a *lot* of stuff that still needs to be done on the project as well before it can be considered to be somewhat stable. Please bear with me while I try to untangle this mess. :heart:

## Issue reports

Issue reports are welcome! If you have any trouble with the game - both in terms of technical issues or content problems - or if you have any questions about navigating the project or using the editor, please feel free to use the issue system for that. Alternative contact methods are listed below if you'd like to get in touch with me.

## Contact

If you have any questions or issues, or if you'd like to make large changes to the project, please create an issue here on GitHub, or get in touch with me via e-mail (nuntiswolf@gmail.com), or alternatively via [@nuntis on FurAffinity](https://www.furaffinity.net/user/nuntis/).

## Getting started

Please check the project's main README file for building and launching instructions.

Pull requests are accepted for both code and content (though not entirely _new_ content, see README). Be sure to use the same general style and flow of the code surrounding your changes (see below for guidelines).

## General Guidelines

- All code must compile using C# 7.3 and run on .NET Framework 4.8. Currently, Finmer does not use later versions of .NET (such as .NET Core).
- If at all possible, please avoid introducing third-party dependencies such as new NuGet packages. The project - and especially the main game - should remain as lean as reasonably possible.

## C# Style Guidelines

I recommend using JetBrains' [ReSharper](https://www.jetbrains.com/resharper/) extension while working with this codebase. Almost all code style settings listed below have been codified in the DotSettings file in the repository root, so you'll get everything set up automagically.

- All classes and all public members of classes should have xmldoc comments with at least a `<summary>` section.
- Classes should be laid out in the following order: public enums, static fields, constants, properties, private fields, constructors, destructors, public methods, everything else.
- Use four spaces for indentation, not tabs.
- Braces on new lines, except for empty declarations, these may be placed together.

### Naming conventions

- Class names use `CamelCase`
- Interface names should be prefixed with `I`, as in `IScriptCompiler`
- Enum names should be prefixed with `E`, as in `ECharacterFlags`
- Properties, regardless of access level, use `CamelCase`
- Class fields use `m_CamelCase`
- Static class fields use `s_CamelCase`
- Constants use `k_CamelCase`
- Local variables use `snake_case`

## Editor Documentation

The Editor documentation is kept in a HTML file in `/Modules/Projects/Documentation.html`. When making large-scale changes to the editor, or when modifying Lua script APIs, please be sure to also update the documentation file so module creators always have the most up-to-date and correct info available.

## XAML and MVVM notes

The main game is a WPF project, and therefore all UI systems are (sometimes very loosely) based on the MVVM pattern.

- Models are data containers or 'business logic' that do not directly interact with the UI. This encompasses most gameplay code.
- Views are the user-facing UI components. They contain all visual styling and handle user interactions.
- Viewmodels act as glue between models and views. Generally speaking, viewmodels are responsible for taking the data in a model, and transforming it into something a view can display.

Currently, in many places this pattern is adhered to quite loosely and/or messily, especially in old code, but things are being cleaned up slowly.
