/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Runtime.Serialization;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents the exception that is thrown when loading of game content fails.
    /// </summary>
    [Serializable]
    public sealed class LoaderException : ApplicationException
    {

        private LoaderException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        public LoaderException(string message) : base(message) {}

        public LoaderException(string message, Exception inner) : base(message, inner) {}

    }

    /// <summary>
    /// Represents the exception that is thrown when duplicate assets are found.
    /// </summary>
    [Serializable]
    public sealed class DuplicateContentException : ApplicationException
    {

        private DuplicateContentException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        public DuplicateContentException(string message) : base(message) {}

        public DuplicateContentException(string message, Exception inner) : base(message, inner) {}

    }

    /// <summary>
    /// Represents the exception that is thrown when a request to load an asset cannot be fulfilled because the asset does not exist.
    /// </summary>
    [Serializable]
    public sealed class MissingContentException : ApplicationException
    {

        private MissingContentException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        public MissingContentException(string message) : base(message) {}

        public MissingContentException(string message, Exception inner) : base(message, inner) {}

    }

    /// <summary>
    /// Represents the exception that is thrown when a set of dependency constraints is unsolvable, i.e. cannot be topologically sorted.
    /// </summary>
    [Serializable]
    public sealed class UnsolvableConstraintException : ApplicationException
    {

        private UnsolvableConstraintException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        public UnsolvableConstraintException(string message) : base(message) {}

        public UnsolvableConstraintException(string message, Exception inner) : base(message, inner) {}

    }

}
