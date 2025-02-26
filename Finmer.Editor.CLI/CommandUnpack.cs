/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
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
    public sealed class CommandUnpack : CommandCopy
    {

        /// <inheritdoc />
        public override IEnumerable<string> GetNames()
        {
            yield return "unpack";
        }

        /// <inheritdoc />
        public override Help GetHelp()
        {
            return new Help
            {
                Usage = "<in-furball> <out-project>",
                Description = "Loads the Furball at the specified input path, and saves it as an Editor project. The .fnproj file will be at the specified output path. " +
                    "Caution: files for each asset in the module will be generated alongside the .fnproj file. Using an empty folder as the destination is recommended.",
                Parameters = new List<Help.Parameter>
                {
                    new Help.Parameter
                    {
                        Name = "-y",
                        Description = "Overwrite project file if it already exists."
                    }
                }
            };
        }

        /// <inheritdoc />
        public override int Run(IReadOnlyList<FileInfo> file_list, IReadOnlyList<string> options)
        {
            // Read module as furball, and write back as editor project
            return CopyInternal(file_list, options, new FurballFileDeviceBinary(), new FurballFileDeviceText());
        }

    }

}
