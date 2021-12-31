/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Runtime.Serialization;

namespace Finmer.Core.Serialization
{

    /// <summary>
    /// Represents an exception that occurs during (de)serialization of game content module files.
    /// </summary>
    [Serializable]
    public abstract class FurballException : ApplicationException
    {

        protected FurballException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        protected FurballException(string message) : base(message) {}

        protected FurballException(string message, Exception innerException) : base(message, innerException) {}

    }

    /// <summary>
    /// The exception that is thrown when a general I/O failure occurs during module (de)serialization.
    /// </summary>
    [Serializable]
    public sealed class FurballIOException : FurballException
    {

        private FurballIOException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        public FurballIOException(string message) : base(message) {}

        public FurballIOException(string message, Exception innerException) : base(message, innerException) {}

    }

    /// <summary>
    /// The exception that is thrown when a file is corrupt, or is not a module file at all.
    /// </summary>
    [Serializable]
    public sealed class FurballInvalidHeaderException : FurballException
    {

        private FurballInvalidHeaderException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        public FurballInvalidHeaderException(string message) : base(message) {}

    }

    /// <summary>
    /// The exception that is thrown when an asset contained by a module file is corrupt.
    /// </summary>
    [Serializable]
    public sealed class FurballInvalidAssetException : FurballException
    {

        private FurballInvalidAssetException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        public FurballInvalidAssetException(string message) : base(message) {}

        public FurballInvalidAssetException(string message, Exception innerException) : base(message, innerException) {}

    }

    /// <summary>
    /// The exception that is thrown when a module file contains an unknown asset type.
    /// </summary>
    [Serializable]
    public sealed class FurballUnknownAssetException : FurballException
    {

        private FurballUnknownAssetException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        public FurballUnknownAssetException(string message) : base(message) {}

    }

}
