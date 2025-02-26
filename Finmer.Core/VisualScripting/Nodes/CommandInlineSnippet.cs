/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.IO;
using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that injects a user-specified raw and unfiltered block of Lua code into the output.
    /// </summary>
    public sealed class CommandInlineSnippet : ScriptCommand
    {

        /// <summary>
        /// The Lua code to add to the output script.
        /// </summary>
        public string Snippet { get; set; }

        public override string GetEditorDescription(IContentStore content)
        {
            // Return the first line of the script
            return "Lua: " + new StringReader(Snippet).ReadLine() + " ...";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Code;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.AppendLine(Snippet);
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty("Snippet", Snippet);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            Snippet = instream.ReadStringProperty("Snippet");
        }

    }

}
