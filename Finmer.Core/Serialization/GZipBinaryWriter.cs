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
    /// A version of BinaryWriter that wraps the output stream in a GZipStream.
    /// </summary>
    public class GZipBinaryWriter : BinaryWriter
    {

        public GZipBinaryWriter(Stream outstream, Encoding encoding, bool leaveOpen)
            : base(new GZipStream(outstream, CompressionMode.Compress, leaveOpen), encoding, false)
        {
        }

    }

}