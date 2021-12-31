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
    /// Describes a dependency on another asset package.
    /// </summary>
    public struct FurballDependency
    {

        /// <summary>
        /// The GUID of the dependency package.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// The package file name under which the GUID was last seen. May not be accurate; this is primarily used for UI display.
        /// </summary>
        public string FileNameHint { get; set; }

    }

}
