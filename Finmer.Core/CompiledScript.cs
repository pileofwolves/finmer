/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Core
{

    /// <summary>
    /// Represents a precompiled script, stored in a binary format.
    /// </summary>
    public class CompiledScript
    {

        /// <summary>
        /// The raw script data.
        /// </summary>
        public byte[] Data { get; set; }

#if DEBUG
        /// <summary>
        /// The original source code used to generate the binary script data.
        /// </summary>
        public string Source { get; set; }
#endif

    }

}
