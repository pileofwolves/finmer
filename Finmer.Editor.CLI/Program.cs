/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Finmer.Core.Serialization;

namespace Finmer.Editor.CLI
{

    /// <summary>
    /// Contains the main application logic for the editor CLI.
    /// </summary>
    public static class Program
    {

        public const int k_ExitCode_Success = 0;
        public const int k_ExitCode_Failure = 1;

        /// <summary>
        /// Main entry point.
        /// </summary>
        public static int Main(string[] args)
        {
            // If run without arguments, display the help text
            if (args == null || args.Length == 0)
            {
                WriteLogo(Array.Empty<string>());
                return CommandHelp.Run();
            }

            // Parse the command line; all command line options after the first one (the main command) are additional info or switches
            var file_list = new List<FileInfo>();
            var options = new List<string>();
            foreach (var command_param in args.Skip(1))
            {
                // Command line elements starting with a dash are interpreted as options (switches), otherwise they're file paths
                if (command_param.StartsWith("-"))
                    options.Add(command_param);
                else
                    file_list.Add(new FileInfo(command_param));
            }

            // Write version information
            WriteLogo(options);

            try
            {
                // Run the main command
                var command = args[0].ToLowerInvariant();
                switch (command)
                {
                    case @"help":
                    case @"?": // A few common variants of the help switch, for ease of use
                    case @"/?":
                    case @"-?":
                        return CommandHelp.Run();

                    case @"show":
                        return CommandShow.Run(file_list, options);

                    case @"pack":
                        return CommandPack.Run(file_list, options);

                    case @"unpack":
                        return CommandUnpack.Run(file_list, options);

                    case @"merge":
                        return CommandMerge.Run(file_list, options);

                    default:
                        return Utilities.DisplayError($"Unknown command: {command}. Run 'help' for usage details.");
                }
            }
            catch (FurballException ex)
            {
                return Utilities.DisplayError("An I/O error occurred: " + ex.Message);
            }
        }

        /// <summary>
        /// Display the CLI version banner.
        /// </summary>
        private static void WriteLogo(IReadOnlyList<string> options)
        {
            // Display a banner with version information
            if (!Utilities.GetCommandLineFlag(options, @"-nologo"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Finmer Editor CLI version {CompileConstants.k_VersionString} (format {FurballFileDevice.k_LatestVersion})");
                Console.WriteLine("(C) Nuntis the Wolf, 2019-2024. Licensed under GNU GPL v3.");
            }

            // Reset text color
            Console.ForegroundColor = ConsoleColor.White;
        }

    }

}
