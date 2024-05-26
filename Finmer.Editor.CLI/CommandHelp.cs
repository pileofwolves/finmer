/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;

namespace Finmer.Editor.CLI
{

    /// <summary>
    /// Command for displaying CLI usage help text.
    /// </summary>
    internal static class CommandHelp
    {

        private const string k_HelpText = @"
help
    Displays this help text.

show <path>
    Loads the module at the specified path, and displays basic information
    about it. Both Editor projects (fnproj) and Furball files are accepted.

    Available options:
    -v          List all individual asset files in the module.

pack [options] <in-project> <out-furball>
    Loads the Editor project at the specified path, and saves it as a Furball
    at the specified output path.

    Available options:
    -y          Overwrite output file if it already exists.

unpack <in-furball> <out-project>
    Loads the Furball at the specified input path, and saves it as an Editor
    project. The .fnproj file will be at the specified output path. Caution:
    files for each asset in the module will be generated alongside the .fnproj
    file. Using an empty folder as the destination is recommended.

    Available options:
    -y          Overwrite output file if it already exists.

merge [options] <in1> <in2> <in...> <output>
    Merges the input modules into one. Both Editor projects (fnproj) and
    Furball files are accepted. The output path should be a Furball file. The
    IDs of the input modules will be merged in a deterministic manner, so that
    merging the same modules always results in the same merged module ID.

    Available options:
    -y          Overwrite output file if it already exists.
    -title=     Specifies the merged module's title. Defaults to ""Merged Module"".
    -author=    Specifies the merged module's author. Defaults to a list of all
                authors of input modules.
";

        /// <summary>
        /// .
        /// </summary>
        /// <returns>Program exit code.</returns>
        public static int Run()
        {
            // Write the help text
            Console.WriteLine(k_HelpText);

            // Return failure error code to indicate no valid command was run
            return Program.k_ExitCode_Failure;
        }

    }

}
