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
using Finmer.Core.Assets;
using Finmer.Core.Serialization;

namespace Finmer.Editor.CLI
{

    /// <summary>
    /// Command for displaying the contents and metadata of a module.
    /// </summary>
    internal static class CommandShow
    {

        /// <summary>
        /// Run the command.
        /// </summary>
        /// <param name="file_list">List of file paths to operate on.</param>
        /// <param name="options">List of command line options.</param>
        /// <returns>Program exit code.</returns>
        public static int Run(IReadOnlyList<FileInfo> file_list, IReadOnlyList<string> options)
        {
            if (file_list.Count != 1)
                return CommandHelp.Run();

            // Interpret the file list as a single input path
            var in_file = file_list[0];
            if (!in_file.Exists)
                return Utilities.DisplayError($"Input {in_file.FullName} could not be found.");

            // Read the module from disk
            var device = Utilities.CreateFileDevice(in_file);
            var module = device.ReadModule(in_file);
            var meta = module.Metadata;

            // Display some information about it
            Console.WriteLine("=== File Information ===");
            Console.WriteLine($"File Size:        {Utilities.DescribeByteCount(in_file.Length)}");
            Console.WriteLine($"Last Modified:    {in_file.LastWriteTimeUtc.ToShortDateString()} {in_file.LastWriteTimeUtc.ToShortTimeString()}");
            Console.WriteLine($"Format Version:   {meta.FormatVersion} / {FurballFileDevice.k_LatestVersion}");
            Console.WriteLine($"Format Mode:      {(device is FurballFileDeviceText ? "Editor Project" : "Furball")}");

            Console.WriteLine();
            Console.WriteLine("=== Module Metadata ===");
            Console.WriteLine($"Module ID:        {meta.ID}");
            Console.WriteLine($"Title:            {meta.Title}");
            Console.WriteLine($"Author:           {meta.Author}");

            Console.WriteLine();
            Console.WriteLine("=== Dependencies ===");
            if (module.Dependencies.Count == 0)
            {
                Console.WriteLine("No dependencies.");
            }
            else
            {
                foreach (var dependency in module.Dependencies)
                    Console.WriteLine($"- {dependency.FileNameHint} ({dependency.ID})");
            }

            Console.WriteLine();
            Console.WriteLine("=== Asset Summary ===");
            Console.WriteLine($"Total:            {module.Assets.Count}");
            Console.WriteLine($"Scenes:           {module.Assets.OfType<AssetScene>().Count()} ({module.Assets.OfType<AssetScene>().Count(scene => scene.IsPatch)} patches)");
            Console.WriteLine($"Creatures:        {module.Assets.OfType<AssetCreature>().Count()}");
            Console.WriteLine($"Items:            {module.Assets.OfType<AssetItem>().Count()}");
            Console.WriteLine($"String Tables:    {module.Assets.OfType<AssetStringTable>().Count()}");
            Console.WriteLine($"Journals:         {module.Assets.OfType<AssetJournal>().Count()}");
            Console.WriteLine($"Scripts:          {module.Assets.OfType<AssetScript>().Count()}");

            // If verbose flag is added, also display individual files
            if (Utilities.GetCommandLineFlag(options, "-v"))
            {
                Console.WriteLine();
                Console.WriteLine("=== Asset List ===");

                if (module.Assets.Count == 0)
                    Console.WriteLine("No assets.");

                foreach (var asset in module.Assets)
                    Console.WriteLine($"- {asset.GetType().Name.Substring(5),-16}{asset.Name,-32}{asset.ID}");
            }

            return Program.k_ExitCode_Success;
        }

    }

}
