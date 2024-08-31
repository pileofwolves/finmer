/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Runtime.Serialization;

namespace Finmer.Core.Compilers
{

    /// <summary>
    /// The exception that is thrown by the SceneCompiler if an AssetScene cannot be compiled.
    /// </summary>
    [Serializable]
    public class SceneCompilerException : ApplicationException
    {

        protected SceneCompilerException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        public SceneCompilerException(string message) : base(message) { }

        public SceneCompilerException(string message, Exception innerException) : base(message, innerException) { }

    }

    /// <summary>
    /// Represents the exception that is thrown when reading or applying of a scene patch fails.
    /// </summary>
    [Serializable]
    public sealed class InvalidScenePatchException : SceneCompilerException
    {

        private InvalidScenePatchException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public InvalidScenePatchException(string message) : base(message) { }

        public InvalidScenePatchException(string message, Exception inner) : base(message, inner) { }

    }

    /// <summary>
    /// The exception that is thrown if a script is invalid.
    /// </summary>
    [Serializable]
    public sealed class ScriptCompilationException : ApplicationException
    {

        private ScriptCompilationException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        public ScriptCompilationException(string message) : base(message) {}

        public ScriptCompilationException(string message, Exception innerException) : base(message, innerException) {}

    }

}
