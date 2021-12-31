/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Describes a feat, with its text resources and prerequisites.
    /// </summary>
    public class AssetFeat : AssetBase
    {

        /// <summary>
        /// A collection of key/value pairs used to reconstruct the asset.
        /// </summary>
        [Obsolete("To be replaced by dedicated properties")]
        public PropertyBag Properties { get; private set; } = new PropertyBag();

    }

}
