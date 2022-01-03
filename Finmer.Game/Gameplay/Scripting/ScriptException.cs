/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Runtime.Serialization;

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// The exception that is thrown when an unrecoverable error occurs in a ScriptContext.
    /// </summary>
    [Serializable]
    public sealed class ScriptException : ApplicationException
    {

        private ScriptException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        public ScriptException(string message) : base(message) {}

        public ScriptException(string message, Exception innerException) : base(message, innerException) {}

    }

}
