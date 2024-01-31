/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that does not emit any code, and serves only as an administrative tool for the user.
    /// </summary>
    public sealed class CommandComment : ScriptCommand
    {

        /// <summary>
        /// The body text of the comment.
        /// </summary>
        public string Comment { get; set; } = String.Empty;

        public override string GetEditorDescription(IContentStore content)
        {
            return "-- " + Comment;
        }

        public override EColor GetEditorColor()
        {
            return EColor.Comment;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // No code to emit
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty("Comment", Comment);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Comment = instream.ReadStringProperty("Comment");
        }

    }

}
