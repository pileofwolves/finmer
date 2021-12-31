/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;

namespace Finmer.Core.Serialization
{

    /// <summary>
    /// Represents a forward stream-like interface for writing asset data in module files.
    /// </summary>
    public interface IFurballContentWriter
    {

        /// <summary>
        /// Write a key/value-pair with the specified value.
        /// </summary>
        void WriteBooleanProperty(string key, bool value);

        /// <summary>
        /// Write a key/value-pair with the specified value.
        /// </summary>
        void WriteByteProperty(string key, byte value);

        /// <summary>
        /// Write a key/value-pair with the specified value.
        /// </summary>
        void WriteInt32Property(string key, int value);

        /// <summary>
        /// Write a key/value-pair with the specified value.
        /// </summary>
        void WriteFloatProperty(string key, float value);

        /// <summary>
        /// Write a key/value-pair with the specified value.
        /// </summary>
        void WriteEnumProperty<TEnum>(string key, TEnum value) where TEnum : struct, Enum;

        /// <summary>
        /// Write a key/value-pair with the specified value.
        /// </summary>
        void WriteGuidProperty(string key, Guid value);

        /// <summary>
        /// Write a key/value-pair with the specified value.
        /// </summary>
        void WriteStringProperty(string key, string value);

        /// <summary>
        /// Write a key/value-pair with the specified value.
        /// </summary>
        void WriteByteArrayProperty(string key, byte[] value);

        /// <summary>
        /// Write a raw string value, such as an array element.
        /// </summary>
        void WriteStringValue(string value);

        /// <summary>
        /// Writes a loose file that may be stored separately in the project.
        /// </summary>
        /// <param name="key">Identifier for this attachment. It must be unique throughout the entire module; not just this asset.</param>
        /// <param name="value">The attachment contents; or null to erase it.</param>
        void WriteAttachment(string key, byte[] value);

        /// <summary>
        /// Write the header of a nested object defined by the specified key.
        /// </summary>
        /// <param name="key">The key of the key/value pair, to be used in content formats that support named keys.</param>
        void BeginObject(string key = null);

        /// <summary>
        /// Write the header of a nested array defined by the specified key.
        /// </summary>
        /// <param name="key">The key of the key/value pair, to be used in content formats that support named keys.</param>
        /// <param name="numElements">The number of elements contained in this array.</param>
        void BeginArray(string key, int numElements);

        /// <summary>
        /// Close an object that was opened with BeginObject(). This function must be called an equal number of times as BeginObject().
        /// </summary>
        void EndObject();

        /// <summary>
        /// Close an array that was opened with BeginArray(). This function must be called an equal number of times as BeginObject().
        /// </summary>
        void EndArray();

    }

}
