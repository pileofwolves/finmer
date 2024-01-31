/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting
{

    /// <summary>
    /// Represents a ScriptCommand that contains groups of nested nodes.
    /// </summary>
    public abstract class ScriptCommandContainer : ScriptCommand
    {

        /// <summary>
        /// Describes a nested group of nodes.
        /// </summary>
        public struct Subgroup
        {

            /// <summary>
            /// String to display in front of the group in the editor, or null if it should be omitted.
            /// </summary>
            public string EditorPrefix { get; set; }

            /// <summary>
            /// String to display after the group in the editor, or null if it should be omitted.
            /// </summary>
            public string EditorSuffix { get; set; }

            /// <summary>
            /// The collection of nodes comprising this group.
            /// </summary>
            public List<ScriptNode> Nodes { get; set; }

        }

        /// <summary>
        /// Returns all valid subgroups for this node.
        /// </summary>
        public abstract IEnumerable<Subgroup> GetSubgroups();

        /// <summary>
        /// Helper method for writing a subgroup to an output stream.
        /// </summary>
        protected void SerializeSubgroup(IFurballContentWriter outstream, string key, List<ScriptNode> nodes)
        {
            outstream.BeginArray(key, nodes.Count);
            foreach (var node in nodes)
                outstream.WriteNestedObjectProperty(null, node);
            outstream.EndArray();
        }

        /// <summary>
        /// Helper method for reading a subgroup from an input stream that was written using SerializeSubgroup().
        /// </summary>
        protected List<ScriptNode> DeserializeSubgroup(IFurballContentReader instream, int version, string key)
        {
            var count = instream.BeginArray(key);
            var output = new List<ScriptNode>(count);

            // Read the nested nodes
            for (int i = 0; i < count; i++)
                output.Add(instream.ReadNestedObjectProperty<ScriptNode>(null, version));
            instream.EndArray();

            return output;
        }

    }

}
