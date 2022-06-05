/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Script condition that runs an arbitrary user-specified Lua code snippet.
    /// </summary>
    public sealed class ConditionInlineSnippet : ScriptCondition
    {

        /// <summary>
        /// The Lua code to add to the output script.
        /// </summary>
        public string Snippet { get; set; }

        public override string GetEditorDescription()
        {
            return Snippet;
        }

        public override EColor GetEditorColor()
        {
            return EColor.Code;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.Append(Snippet);
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty("Snippet", Snippet);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Snippet = instream.ReadStringProperty("Snippet");
        }

    }

}
