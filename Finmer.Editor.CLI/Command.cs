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
using Finmer.Core.Serialization;

namespace Finmer.Editor.CLI
{

    /// <summary>
    /// Represents a CLI command.
    /// </summary>
    public abstract class Command
    {

        /// <summary>
        /// Returns each of the names under which this command is known, including aliases. The first is assumed to be 'primary'.
        /// </summary>
        public abstract IEnumerable<string> GetNames();

        /// <summary>
        /// Returns user-oriented usage information for this command.
        /// </summary>
        public abstract Help GetHelp();

        /// <summary>
        /// Run the command.
        /// </summary>
        /// <param name="file_list">List of file paths to operate on.</param>
        /// <param name="options">List of command line options.</param>
        /// <exception cref="FurballException">Thrown if any input or output module operations result in I/O errors.</exception>
        /// <returns>Program exit code.</returns>
        public abstract int Run(IReadOnlyList<FileInfo> file_list, IReadOnlyList<string> options);

        /// <summary>
        /// Prints text to the console describing the command and how to use it.
        /// </summary>
        /// <returns>Program exit code.</returns>
        protected int ShowCommandUsage()
        {
            // Write command usage and detailed description
            var help = GetHelp();
            Console.WriteLine("Usage:");
            Console.WriteLine(help.GetSynopsis(this));
            Console.WriteLine();
            Console.WriteLine(help.GetDetails(this));

            // Showing command help means no meaningful action was performed, hence we return an error
            return Program.k_ExitCode_Failure;
        }

    }

}
