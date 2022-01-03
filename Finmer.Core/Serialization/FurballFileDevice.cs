/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.IO;

namespace Finmer.Core.Serialization
{

    /// <summary>
    /// Represents an I/O device for reading and writing Furballs to and from disk.
    /// </summary>
    public abstract class FurballFileDevice
    {

        /// <summary>
        /// The latest version number of the furball file format. Used for migrating old content to new formats.
        /// </summary>
        public const byte k_LatestVersion = 15;

        /// <summary>
        /// Reads a full module file from disk.
        /// </summary>
        /// <param name="file">The main file to read.</param>
        /// <exception cref="FurballException">Thrown on any disk or integrity error.</exception>
        public abstract Furball ReadModule(FileInfo file);

        /// <summary>
        /// Reads only module metadata from the specified file.
        /// </summary>
        /// <param name="file">The main file to read.</param>
        /// <exception cref="FurballException">Thrown on any disk or integrity error.</exception>
        public abstract FurballMetadata ReadMetadata(FileInfo file);

        /// <summary>
        /// Write a module to disk.
        /// </summary>
        /// <param name="furball">The module to serialize.</param>
        /// <param name="file">The main file to write to.</param>
        /// <exception cref="FurballException">Thrown on any disk error.</exception>
        public abstract void WriteModule(Furball furball, FileInfo file);

    }

}
