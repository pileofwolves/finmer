/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using Finmer.Core.Serialization;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Represents a Lua script.
    /// </summary>
    public sealed class AssetScript : AssetBase
    {

        /// <summary>
        /// Container for the script data represented by this standalone asset.
        /// </summary>
        public ScriptData Contents { get; set; }

        /// <summary>
        /// Load order configuration of this script.
        /// </summary>
        public List<LoadOrderDependency> LoadOrder { get; } = new List<LoadOrderDependency>();

        /// <summary>
        /// Binary precompiled version of the script, or null if unavailable.
        /// </summary>
        public CompiledScript PrecompiledScript { get; set; }

        public override void Serialize(IFurballContentWriter outstream)
        {
            base.Serialize(outstream);

            // Write script object
            outstream.WriteNestedScriptProperty(nameof(Contents), Contents);

            // Write load order table
            outstream.BeginArray(nameof(LoadOrder), LoadOrder.Count);
            foreach (var dependency in LoadOrder)
            {
                outstream.BeginObject();
                outstream.WriteGuidProperty(nameof(LoadOrderDependency.TargetAsset), dependency.TargetAsset);
                outstream.WriteEnumProperty(nameof(LoadOrderDependency.Relation), dependency.Relation);
                outstream.EndObject();
            }
            outstream.EndArray();
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            base.Deserialize(instream, version);
            // Read script data
            Contents = instream.ReadNestedObjectProperty<ScriptData>(nameof(Contents), version);
            if (Contents != null)
                Contents.Name = Name;

            // Read load order
            LoadOrder.Clear();
            for (int i = 0, c = instream.BeginArray(nameof(LoadOrder)); i < c; i++)
            {
                instream.BeginObject();
                LoadOrder.Add(new LoadOrderDependency
                {
                    TargetAsset = instream.ReadGuidProperty(nameof(LoadOrderDependency.TargetAsset)),
                    Relation = instream.ReadEnumProperty<LoadOrderDependency.ERelation>(nameof(LoadOrderDependency.Relation)),
                });
                instream.EndObject();
            }
            instream.EndArray();
        }
    }
}
