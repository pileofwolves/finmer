/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;

namespace Finmer.Core
{

    /// <summary>
    /// Contains simple metadata identifying a Furball.
    /// </summary>
    public struct FurballMetadata
    {

        /// <summary>
        /// Gets this asset package's unique ID.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the asset package's public title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the asset package's author name.
        /// </summary>
        public string Author { get; set; }

    }

}
