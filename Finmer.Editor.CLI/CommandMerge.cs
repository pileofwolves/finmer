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
using Finmer.Core;
using System.Security.Cryptography;
using Finmer.Core.Serialization;

namespace Finmer.Editor.CLI
{

    /// <summary>
    /// Command for merging multiple modules into one.
    /// </summary>
    public sealed class CommandMerge : Command
    {

        /// <inheritdoc />
        public override IEnumerable<string> GetNames()
        {
            yield return "merge";
        }

        /// <inheritdoc />
        public override Help GetHelp()
        {
            return new Help
            {
                Usage = "<in1> <in2> <in...> <output>",
                Description = "Merges the input modules into one. Both Editor projects (fnproj) and Furball files are accepted. " +
                    "The last path specified is assumed to be the output, and should be a Furball file. The IDs of the input modules " +
                    "will be merged in a deterministic manner, so that merging the same modules always results in the same merged module ID.",
                Parameters = new List<Help.Parameter>
                {
                    new Help.Parameter
                    {
                        Name = "-y",
                        Description = "Overwrite output file if it already exists."
                    },
                    new Help.Parameter
                    {
                        Name = "-title=",
                        Description = "Set output module title. Default is \"Merged Module\"."
                    },
                    new Help.Parameter
                    {
                        Name = "-author=",
                        Description = "Set output module author. Default is a list of all authors of input modules."
                    }
                }
            };
        }

        /// <inheritdoc />
        public override int Run(IReadOnlyList<FileInfo> file_list, IReadOnlyList<string> options)
        {
            if (file_list.Count < 2)
                return ShowCommandUsage();

            // Parse the file list such that the last path is the output path, and all other paths are input modules
            var num_in_files = file_list.Count - 1;
            var in_files = new List<FileInfo>(num_in_files);
            var out_file = file_list[num_in_files];
            for (int i = 0; i < num_in_files; i++)
                in_files.Add(file_list[i]);

            // Read all inputs
            Console.WriteLine($"Merging {in_files.Count} modules into {out_file.Name}");
            var input_metadata = new List<FurballMetadata>();
            var input_dependencies = new List<FurballDependency>();
            var output_furball = new Furball();
            foreach (var input in in_files)
            {
                Console.WriteLine($"Reading {input.Name}");
                if (!input.Exists)
                    return Utilities.DisplayError($"Input {input.FullName} could not be found.");

                // Read this input module from disk and merge it into the output
                var device = Utilities.CreateFileDevice(input);
                var submodule = device.ReadModule(input);
                output_furball.Merge(submodule);

                // Keep track of metadata so we can
                input_metadata.Add(submodule.Metadata);
                input_dependencies.AddRange(submodule.Dependencies);
            }

            // Sort metadata by ID, to guarantee determinism if the input modules are provided in a different order
            input_metadata.Sort((lhs, rhs) => Comparer<Guid>.Default.Compare(lhs.ID, rhs.ID));

            // Generate a deterministic ID for the merged module, using the IDs of the input module
            byte[] id_hash = new byte[16];
            using (var sha1 = SHA1.Create())
            {
                for (int i = 0; i < input_metadata.Count; i++)
                {
                    var guid_bytes = input_metadata[i].ID.ToByteArray();
                    if (i == input_metadata.Count - 1)
                        sha1.TransformFinalBlock(guid_bytes, 0, guid_bytes.Length);
                    else
                        sha1.TransformBlock(guid_bytes, 0, guid_bytes.Length, null, 0);
                }

                // Copy the first 16 bytes of the SHA-1 hash
                Buffer.BlockCopy(sha1.Hash, 0, id_hash, 0, 16);
            }

            // Set the version number of the GUID to 5, as required by standard, to indicate a SHA-1-based GUID
            id_hash[6] = (byte)((id_hash[6] & 0x0F) | (5 << 4));
            id_hash[8] = (byte)((id_hash[8] & 0x3F) | 0x80);

            // Combine and filter the dependencies of all input modules, such that dependencies on merged modules are discarded
            foreach (var dependency in input_dependencies.Distinct())
            {
                // If one of the merged dependencies is a dependency on a module that was merged, discard it, since they are now the same module
                if (input_metadata.Any(meta => meta.ID == dependency.ID))
                    continue;

                // Copy the dependency to the output
                output_furball.Dependencies.Add(dependency);
            }

            // Configure output module metadata
            output_furball.Metadata = new FurballMetadata
            {
                FormatVersion = FurballFileDevice.k_LatestVersion,
                ID = new Guid(id_hash),
                Title = Utilities.GetCommandLineString(options, "-title", "Merged Module"),
                Author = Utilities.GetCommandLineString(options, "-author", String.Join(", ", input_metadata.Select(meta => meta.Author).Distinct())),
            };

            // Write the final merged module to disk
            Console.WriteLine($"Writing merged module with {output_furball.Assets.Count} assets, {output_furball.Dependencies.Count} dependencies");
            new FurballFileDeviceBinary().WriteModule(output_furball, out_file);

            Console.WriteLine($"Done. Finished writing {output_furball.Metadata.Title} ({output_furball.Metadata.ID})");
            return Program.k_ExitCode_Success;
        }

    }

}
