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
    /// Command for converting an Editor project into a Furball.
    /// </summary>
    public sealed class CommandPack : CommandCopy
    {

        /// <inheritdoc />
        public override IEnumerable<string> GetNames()
        {
            yield return "pack";
        }

        /// <inheritdoc />
        public override Help GetHelp()
        {
            return new Help
            {
                Usage = "<in-project> <out-furball>",
                Description = "Loads the Editor project at the specified path, and saves it as a Furball at the specified output path.",
                Parameters = new List<Help.Parameter>
                {
                    new Help.Parameter
                    {
                        Name = "-y",
                        Description = "Overwrite output file if it already exists."
                    }
                }
            };
        }

        /// <inheritdoc />
        public override int Run(IReadOnlyList<FileInfo> file_list, IReadOnlyList<string> options)
        {
            // Read module as editor project, and write back as furball
            return CopyInternal(file_list, options, new FurballFileDeviceText(), new FurballFileDeviceBinary());
        }

    }

}
