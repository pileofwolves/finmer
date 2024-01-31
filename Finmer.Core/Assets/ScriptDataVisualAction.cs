/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using System.Text;
using Finmer.Core.Compilers;
using Finmer.Core.Serialization;
using Finmer.Core.VisualScripting;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Contains a Lua script that is built using modular, visually arranged nodes.
    /// </summary>
    public sealed class ScriptDataVisualAction : ScriptData
    {

        /// <summary>
        /// The collection of nodes that make up this script.
        /// </summary>
        public List<ScriptNode> Nodes { get; } = new List<ScriptNode>();

        public override string GetScriptText(IContentStore content)
        {
            var output = new StringBuilder();

            try
            {
                // Recursively allow each node to emit Lua code into the output
                foreach (var node in Nodes)
                    node.EmitLua(output, content);
            }
            catch (InvalidScriptNodeException ex)
            {
                // Add more information to the exception, then rethrow it
                throw new ScriptCompilationException($"In script '{Name}': {ex.Message}", ex);
            }

            return output.ToString();
        }

        public override bool HasContent()
        {
            return Nodes.Count > 0;
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            // Write node array
            outstream.BeginArray("Nodes", Nodes.Count);
            foreach (var node in Nodes)
                outstream.WriteNestedObjectProperty(null, node);
            outstream.EndArray();
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            // Recursively deserialize each of the nodes in the node array
            for (int i = 0, count = instream.BeginArray("Nodes"); i < count; i++)
                Nodes.Add(instream.ReadNestedObjectProperty<ScriptNode>(null, version));
            instream.EndArray();
        }

    }

}
