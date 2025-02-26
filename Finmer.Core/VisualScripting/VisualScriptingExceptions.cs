/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Runtime.Serialization;

namespace Finmer.Core.VisualScripting
{

    /// <summary>
    /// The exception that is thrown when a visual script is broken.
    /// </summary>
    [Serializable]
    public sealed class InvalidScriptNodeException : Exception
    {

        private InvalidScriptNodeException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        public InvalidScriptNodeException(string message) : base(message) {}

    }

}
