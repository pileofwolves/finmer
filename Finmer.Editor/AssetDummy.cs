/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core.Serialization;

namespace Finmer.Editor
{

    /// <summary>
    /// Placeholder asset object.
    /// </summary>
    public sealed class AssetDummy : IFurballSerializable
    {

        public void Serialize(IFurballContentWriter outstream)
        {
            throw new NotSupportedException();
        }

        public void Deserialize(IFurballContentReader instream)
        {
            throw new NotSupportedException();
        }

    }

}
