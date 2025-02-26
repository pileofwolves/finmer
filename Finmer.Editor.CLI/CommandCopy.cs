/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
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
    /// Generic command for copying a module.
    /// </summary>
    public abstract class CommandCopy : Command
    {

        /// <summary>
        /// Copy an input module from one device to another.
        /// </summary>
        /// <param name="file_list">List of file paths to operate on.</param>
        /// <param name="options">List of command line options.</param>
        /// <param name="in_device">Device to read the input module with.</param>
        /// <param name="out_device">Device to write the output module with.</param>
        /// <returns>Program exit code.</returns>
        protected int CopyInternal(IReadOnlyList<FileInfo> file_list, IReadOnlyList<string> options, FurballFileDevice in_device, FurballFileDevice out_device)
        {
            if (file_list.Count != 2)
                return ShowCommandUsage();

            // Interpret the file list as an input and output path
            var in_file = file_list[0];
            var out_file = file_list[1];
            if (!in_file.Exists)
                return Utilities.DisplayError($"Input {in_file.FullName} could not be found.");

            // Overwrite only if a flag is provided
            if (out_file.Exists && !Utilities.GetCommandLineFlag(options, "-y"))
                return Utilities.DisplayError($"Output {out_file.FullName} already exists. Specify -y to overwrite.");

            // Read the module from disk
            Console.WriteLine($"Reading module {in_file.Name}");
            var module = in_device.ReadModule(in_file);

            // Write it back to the requested output path using the other device
            Console.WriteLine($"Writing module {out_file.Name}");
            out_device.WriteModule(module, out_file);

            return Program.k_ExitCode_Success;
        }

    }

}
