# Contributing

Hey! Thanks a lot for your interest in helping improve Finmer! :heart:

I hope this document helps you get started with the project. Issue reports are welcome! If you find anything missing here, or if you have any trouble with the game - both in terms of technical issues or content problems - or if you have any questions about navigating the project or using the editor, please get in touch!

I can be reached by creating an issue here on GitHub, or via e-mail (nuntis@finmer.dev), or via [@nuntis on FurAffinity](https://www.furaffinity.net/user/nuntis/).

## :bulb: Getting started

Please check the project's main README file for building instructions.

- **Want to create content?** Check out the Editor Documentation (`/Modules/Projects/Documentation.html`) for info on how content modules work, what kind of assets there are, and how they interact with the game application.
- **Want to add/change features?** Check below for project structure notes and code style guidelines.

Pull requests are accepted for both code and content (though not entirely _new_ content, see README).

### Running the game with developer tools

Finmer has a **debug mode** that enables various diagnostics, as well as a script console that can be used to inspect, cheat or modify things as desired.

To activate debug mode, do either of the following:

- Compile and run a build in the Debug configuration. Debug builds have debug mode enabled by default.
- Alternatively, pass the `-debug` command line option to a Release build.

When in-game, a new Script Console button will appear in the panel on the left. Click it to reveal an input field. You can enter any arbitrary Lua code in this field. Refer to the Script Documentation for a list of functions and properties that you can use here. Press Enter to run your code.

## Working with the solution

- All code must compile using C# 7.3 and run on .NET Framework 4.8. Currently, Finmer does not use later versions of .NET (such as .NET Core).
- If at all possible, please avoid introducing third-party dependencies such as new NuGet packages. The project - and especially the main game - should remain as lean as reasonably possible.

## C# Style Guidelines

I recommend using JetBrains' [ReSharper](https://www.jetbrains.com/resharper/) extension while working with this codebase. Almost all code style settings listed below have been codified in the DotSettings file in the repository root, so you'll get everything set up automagically.

### General suggestions

- All classes and all public members of classes should have xmldoc comments with at least a `<summary>` section.
- Add comments not just for yourself, but future us, too. Add context, notes and thought processes that aren't already there: avoid repeating in plain English what the code already says.
- Don't commit commented-out code; use Git to retrieve old code instead if needed.
- Adhere to the style and formatting of surrounding code where possible.
- Prefer newer C# language features (string interpolation, null coalescing, pattern matching, etc) where it makes code more readable.

### Formatting

- Use four spaces for indentation, not tabs.
- Braces on new lines, except for empty declarations, these may be placed together.
- Classes should be laid out in the following order: public enums, static fields, constants, properties, private fields, constructors, destructors, public methods, everything else.
- Comments should be (reasonably) complete English sentences: `// They should look like this`

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

The main game is a WPF project, and therefore all UI systems are generally based on the MVVM pattern.

- Models are data containers or 'business logic' that do not directly interact with the UI. This encompasses most gameplay code.
- Views are the user-facing UI components. They contain all visual styling and handle user interactions.
- Viewmodels act as glue between models and views. Generally speaking, viewmodels are responsible for taking the data in a model, and transforming it into something a view can display.

When adding new views, prefer adhering to MVVM where possible. This means for example creating viewmodels to bind controls to context. For example:

- Binding a Button to a RelayCommand is preferred over adding a Click event handler in the code-behind.
- Binding a TextBlock to a property with INotifyPropertyChanged is preferred over setting `TextBlock.Text` directly in code.

Binding to a model directly (as opposed to putting a viewmodel in between) is fine if all the viewmodel would do is blindly relay everything between the model and the view. If more substantial logic is needed for the UI, then use a viewmodel.

Especially in older code this pattern is not always followed. This is being cleaned up over time.

**If you struggle to get a view working - MVVM can certainly be very annoying sometimes - don't fret!** Open an issue and let me know; I'm here to help.
