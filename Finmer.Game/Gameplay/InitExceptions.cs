/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Runtime.Serialization;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents an exception that prevents the application from starting up.
    /// </summary>
    [Serializable]
    public abstract class ApplicationInitException : ApplicationException
    {

        protected ApplicationInitException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected ApplicationInitException(string message) : base(message) { }

        protected ApplicationInitException(string message, Exception innerException) : base(message, innerException) { }

    }

    /// <summary>
    /// Represents the exception that is thrown when initialization of the Lua script runtime fails.
    /// </summary>
    [Serializable]
    public sealed class ScriptRuntimeLoadException : ApplicationInitException
    {

        private ScriptRuntimeLoadException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ScriptRuntimeLoadException(string message) : base(message) { }

        public ScriptRuntimeLoadException(string message, Exception inner) : base(message, inner) { }

    }

}
