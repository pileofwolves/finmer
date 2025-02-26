/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Finmer.Editor.CLI
{

    /// <summary>
    /// Contains display-related utilities for the CLI tool.
    /// </summary>
    internal static class Utilities
    {

        private static readonly string[] k_ByteCountSuffixes = { "KiB", "MiB", "GiB", "TiB" };

        /// <summary>
        /// Returns a FurballFileDevice suitable for reading the target module file.
        /// If the file extension is not recognized, reading behavior is undefined.
        /// </summary>
        /// <param name="target_file">The module file to read from disk.</param>
        public static FurballFileDevice CreateFileDevice(FileInfo target_file)
        {
            if (target_file.Extension.Equals(@".fnproj"))
                return new FurballFileDeviceText();
            else
                return new FurballFileDeviceBinary();
        }

        /// <summary>
        /// Checks whether the specified command line option is specified among the list of command line options.
        /// </summary>
        /// <param name="options">Command line options provided by the user.</param>
        /// <param name="flag">The flag to check for.</param>
        public static bool GetCommandLineFlag(IReadOnlyList<string> options, string flag)
        {
            return options.Contains(flag);
        }

        /// <summary>
        /// Searches for a command line option with the specified name, and returns the associated value (e.g. -option=value).
        /// </summary>
        /// <param name="options">Command line options provided by the user.</param>
        /// <param name="key">The option name to check for.</param>
        /// <param name="default_value">The default value to return if the option is not specified by the user.</param>
        public static string GetCommandLineString(IReadOnlyList<string> options, string key, string default_value)
        {
            foreach (var option in options)
            {
                var pair = option.Split('=');
                if (pair.Length != 2)
                    continue;

                if (!pair[0].Equals(key, StringComparison.InvariantCulture))
                    continue;

                return pair[1];
            }

            return default_value;
        }

        /// <summary>
        /// Given a byte count, returns a formatted string displaying the byte count in an appropriately-sized unit.
        /// </summary>
        public static string DescribeByteCount(long count)
        {
            int suffix_index = 0;
            double scaled_count = count / 1024.0;
            while (scaled_count > 1024.0 && suffix_index < 3)
            {
                scaled_count /= 1024.0;
                suffix_index++;
            }

            return String.Format(CultureInfo.InvariantCulture, "{0:F2} {1}", scaled_count, k_ByteCountSuffixes[suffix_index]);
        }

        /// <summary>
        /// Displays an error and returns the error exit code. Meant to be used in a return statement, as a convenience.
        /// </summary>
        /// <param name="message">The error message to display to the user.</param>
        public static int DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + message);
            Console.ForegroundColor = ConsoleColor.White;
            return Program.k_ExitCode_Failure;
        }

    }

}
