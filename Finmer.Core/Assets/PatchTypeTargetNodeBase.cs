/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Serialization;
using System;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Base class for patch subtypes that target a specific scene node.
    /// </summary>
    public abstract class PatchTypeTargetNodeBase : PatchType
    {

        /// <summary>
        /// The name of the scene node in the target scene onto which the patch applies.
        /// </summary>
        public string TargetNode { get; set; } = String.Empty;

        /// <inheritdoc />
        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty(nameof(TargetNode), TargetNode);
        }

        /// <inheritdoc />
        public override void Deserialize(IFurballContentReader instream)
        {
            TargetNode = instream.ReadStringProperty(nameof(TargetNode));
        }

    }

}
