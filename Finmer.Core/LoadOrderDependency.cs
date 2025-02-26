/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;

namespace Finmer.Core
{

    /// <summary>
    /// Describes an ordering dependency of one asset on another.
    /// </summary>
    public struct LoadOrderDependency
    {

        /// <summary>
        /// Describes the order in which the assets must be resolved.
        /// </summary>
        public enum ERelation : byte
        {
            Before,
            After
        }

        /// <summary>
        /// ID of the target asset.
        /// </summary>
        public Guid TargetAsset { get; set; }

        /// <summary>
        /// The relation of the source asset to the target asset.
        /// </summary>
        public ERelation Relation { get; set; }

    }

}
