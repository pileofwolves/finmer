/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Finmer.Core.Serialization
{

    /// <summary>
    /// A version of BinaryReader that wraps the input stream in a GZipStream.
    /// </summary>
    public class GZipBinaryReader : BinaryReader
    {

        public GZipBinaryReader(Stream instream, Encoding encoding, bool leaveOpen)
            : base(new GZipStream(instream, CompressionMode.Decompress, leaveOpen), encoding, false)
        {
        }

    }

}