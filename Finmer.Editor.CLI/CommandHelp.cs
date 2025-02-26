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
using System.Text;

namespace Finmer.Editor.CLI
{

    /// <summary>
    /// Command for displaying CLI usage help text.
    /// </summary>
    public sealed class CommandHelp : Command
    {

        /// <inheritdoc />
        public override IEnumerable<string> GetNames()
        {
            // For convenience, we accept a few variations of 'help' switches that are common in other CLI programs
            yield return "help";
            yield return "-help";
            yield return "/help";
            yield return "?";
            yield return "-?";
            yield return "/?";
        }

        /// <inheritdoc />
        public override Help GetHelp()
        {
            return new Help
            {
                Usage = String.Empty,
                Description = "Display a list of available commands."
            };
        }

        public override int Run(IReadOnlyList<FileInfo> file_list, IReadOnlyList<string> options)
        {
            // Build a string describing all supported commands
            var builder = new StringBuilder();
            builder.AppendLine();
            builder.AppendLine("Available commands:");
            builder.AppendLine();
            foreach (var command in Program.Commands)
            {
                builder.Append(' ', 4);
                builder.AppendLine(command.GetHelp().GetSynopsis(command));
            }

            builder.AppendLine();
            builder.AppendLine("Run a command without arguments to view additional help.");

            // Display it
            Console.WriteLine(builder.ToString());

            // Return failure error code to indicate no meaningful command was run
            return Program.k_ExitCode_Failure;
        }

    }

}
