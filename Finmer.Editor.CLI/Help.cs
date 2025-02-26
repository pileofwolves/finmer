/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Finmer.Editor.CLI
{

    /// <summary>
    /// Represents usage information and tips associated with a console command.
    /// </summary>
    public class Help
    {

        /// <summary>
        /// Describes a command switch or parameter.
        /// </summary>
        public struct Parameter
        {

            /// <summary>
            /// The expression the user should place on the command line to invoke this switch or parameter.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Description of the function of the switch or parameter.
            /// </summary>
            public string Description { get; set; }

        }

        /// <summary>
        /// Overview of the required command line options to use with this command.
        /// </summary>
        public string Usage { get; set; }

        /// <summary>
        /// Detailed description of the command, with associated operations, tools and tips.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Collection of optional switches and parameters the user may provide to this command.
        /// </summary>
        public List<Parameter> Parameters { get; set; }

        /// <summary>
        /// Returns a brief, single-line overview of the command's usage.
        /// </summary>
        /// <param name="command">The command this help object is associated with.</param>
        public string GetSynopsis(Command command)
        {
            var builder = new StringBuilder();

            // First name specified by the command is the 'primary' one
            builder.Append(command.GetNames().First());

            // If the command has switches, indicate that in the usage example
            if (Parameters != null && Parameters.Count != 0)
                builder.Append(" [options]");

            // If a usage example is provided, include it
            if (!String.IsNullOrEmpty(Usage))
            {
                builder.Append(' ');
                builder.Append(Usage);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Returns a detailed, multi-line description of the command.
        /// </summary>
        /// <param name="command">The command this help object is associated with.</param>
        public string GetDetails(Command command)
        {
            var builder = new StringBuilder();

            // Write the main description
            const int k_LineWidth = 80;
            const int k_DescOffset = 4;
            const int k_ParamColumn = 16;
            WriteWrappedText(builder, Description, k_LineWidth, k_DescOffset, k_DescOffset);
            builder.AppendLine();

            // Write parameters
            if (Parameters.Count != 0)
            {
                builder.AppendLine();
                builder.AppendLine("Available options:");

                foreach (var param in Parameters)
                {
                    var padded_name = param.Name.PadRight(k_ParamColumn - k_DescOffset);
                    builder.Append(' ', k_DescOffset);
                    builder.Append(padded_name);
                    WriteWrappedText(builder, param.Description, k_LineWidth, 0, k_ParamColumn);
                    builder.AppendLine();
                }
            }

            // If the command has additional names besides the primary one, print them too
            var aliases = command.GetNames().Skip(1).ToList();
            if (aliases.Count != 0)
            {
                builder.AppendLine();
                builder.Append("Aliases: ");
                builder.AppendLine(String.Join(", ", aliases));
            }

            return builder.ToString();
        }

        /// <summary>
        /// Utility that word-wraps an input string.
        /// </summary>
        /// <param name="builder">The StringBuilder to write the output wrapped text to.</param>
        /// <param name="phrase">The source text.</param>
        /// <param name="width">Width in characters of the desired output text block.</param>
        /// <param name="offset_first">The starting offset of the first line.</param>
        /// <param name="offset_newline">The offset of each subsequent line.</param>
        private static void WriteWrappedText(StringBuilder builder, string phrase, int width, int offset_first, int offset_newline)
        {
            var words = phrase.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Add starting indentation
            var pos = offset_newline;
            var first_word = true;
            builder.Append(' ', offset_first);

            foreach (var word in words)
            {
                // Add one character for the space at the end
                int word_length = word.Length + 1;

                if (pos + word_length > width)
                {
                    // Move to new line
                    pos = offset_newline;
                    first_word = true;
                    builder.AppendLine();
                    builder.Append(' ', offset_newline);
                }

                // If there is a word preceding this one, add a space in front
                if (!first_word)
                    builder.Append(' ');
                first_word = false;

                // Add the word
                pos += word_length;
                builder.Append(word);
            }
        }

    }

}
