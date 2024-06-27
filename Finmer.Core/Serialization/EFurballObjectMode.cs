/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Core.Serialization
{

    /// <summary>
    /// Describes whether a nested object in a furball asset stream is mandatory.
    /// </summary>
    public enum EFurballObjectMode : byte
    {
        Required,
        Optional
    }

}
