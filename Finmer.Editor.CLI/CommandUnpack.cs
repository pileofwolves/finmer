/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using System.IO;
using Finmer.Core.Serialization;

namespace Finmer.Editor.CLI
{

    /// <summary>
    /// Command for converting a Furball into an Editor project.
    /// </summary>
    internal static class CommandUnpack
    {

        /// <summary>
        /// Run the command.
        /// </summary>
        /// <param name="file_list">List of file paths to operate on.</param>
        /// <param name="options">List of command line options.</param>
        /// <returns>Program exit code.</returns>
        public static int Run(IReadOnlyList<FileInfo> file_list, IReadOnlyList<string> options)
        {
            // Read module as furball, and write back as editor project
            return CommandCopy.Run(file_list, options, new FurballFileDeviceBinary(), new FurballFileDeviceText());
        }

    }

}
