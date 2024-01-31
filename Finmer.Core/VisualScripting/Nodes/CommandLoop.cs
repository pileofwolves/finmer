/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that loops a body of commands.
    /// </summary>
    public sealed class CommandLoop : ScriptCommandContainer
    {

        /// <summary>
        /// The nested nodes that form the loop body.
        /// </summary>

        public List<ScriptNode> LoopBody { get; set; } = new List<ScriptNode>();

        public override string GetEditorDescription(IContentStore content)
        {
            return "Loop:";
        }

        public override EColor GetEditorColor()
        {
            return EColor.FlowControl;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // Emit loop header
            output.AppendLine("while true do");

            // Emit loop body
            foreach (var node in LoopBody)
                node.EmitLua(output, content);

            // Emit end
            output.AppendLine("end");
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            SerializeSubgroup(outstream, nameof(LoopBody), LoopBody);
            base.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            LoopBody = DeserializeSubgroup(instream, version, nameof(LoopBody));
            base.Deserialize(instream, version);
        }

        public override IEnumerable<Subgroup> GetSubgroups()
        {
            yield return new Subgroup
            {
                EditorSuffix = "Repeat Above",
                Nodes = LoopBody
            };
        }

    }

}
