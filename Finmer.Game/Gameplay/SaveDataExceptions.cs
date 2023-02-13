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
    /// Represents the exception that is thrown when save data cannot be reconstructed into a game session.
    /// </summary>
    [Serializable]
    public sealed class InvalidSaveDataException : ApplicationException
    {

        private InvalidSaveDataException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        public InvalidSaveDataException(string message) : base(message) {}

        public InvalidSaveDataException(string message, Exception inner) : base(message, inner) {}

    }

}
