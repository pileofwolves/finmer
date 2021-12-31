/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Runtime.Serialization;

namespace Finmer.Core.Compilers
{

    /// <summary>
    /// The exception that is thrown by the <seealso cref="SceneCompiler" /> if an <seealso cref="Assets.AssetScene" /> cannot
    /// be compiled.
    /// </summary>
    [Serializable]
    public sealed class SceneCompilerException : ApplicationException
    {

        private SceneCompilerException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        public SceneCompilerException(string message) : base(message) {}

    }

}
