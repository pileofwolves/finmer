/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting
{

    /// <summary>
    /// Represents a ScriptCommand that contains up to two groups of nested nodes.
    /// </summary>
    public abstract class ScriptCommandContainer : ScriptCommand
    {

        /// <summary>
        /// The first set of nested nodes.
        /// </summary>
        public List<ScriptNode> Subgroup1 { get; } = new List<ScriptNode>();

        /// <summary>
        /// The second set of nested nodes.
        /// </summary>
        public List<ScriptNode> Subgroup2 { get; } = new List<ScriptNode>();

        public override void Serialize(IFurballContentWriter outstream)
        {
            // Write the nested nodes
            outstream.BeginArray("Subgroup", Subgroup1.Count);
            foreach (var node in Subgroup1)
                outstream.WriteNestedObjectProperty(null, node);
            outstream.EndArray();

            // Write the optional second group of nested nodes
            if (IsSubgroup2Enabled())
            {
                outstream.BeginArray("Subgroup2", Subgroup2.Count);
                foreach (var node in Subgroup2)
                    outstream.WriteNestedObjectProperty(null, node);
                outstream.EndArray();
            }
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            // Reset state, in case it was populated already
            Subgroup1.Clear();
            Subgroup2.Clear();

            // Read the nested nodes
            for (int i = 0, count = instream.BeginArray("Subgroup"); i < count; i++)
                Subgroup1.Add(instream.ReadNestedObjectProperty<ScriptNode>(null, version));
            instream.EndArray();

            // Read the optional second group of nested nodes
            if (IsSubgroup2Enabled())
            {
                for (int i = 0, count = instream.BeginArray("Subgroup2"); i < count; i++)
                    Subgroup2.Add(instream.ReadNestedObjectProperty<ScriptNode>(null, version));
                instream.EndArray();
            }
        }

        /// <summary>
        /// Returns the label to insert in between the two subgroups in the editor.
        /// </summary>
        public abstract string GetEditorSubgroup1Suffix();

        /// <summary>
        /// Returns the label to insert after subgroup 2 in the editor.
        /// </summary>
        public abstract string GetEditorSubgroup2Suffix();

        /// <summary>
        /// Indicates whether subgroup 2 is in use at all.
        /// </summary>
        public abstract bool IsSubgroup2Enabled();

    }

}
