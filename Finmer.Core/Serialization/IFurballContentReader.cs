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
    /// Represents a stream-like interface for reading asset data from module files.
    /// </summary>
    /// <remarks>
    /// In general, values must be read in the exact same order as they were written. In other words, each call to a
    /// Write function in IFurballContentWriter must be symmetrically matched with a Read call when using the Reader.
    /// </remarks>
    public interface IFurballContentReader
    {

        /// <summary>
        /// Read the value of a boolean property.
        /// </summary>
        /// <param name="key">The key of the key/value pair, to be used in content formats that support named keys.</param>
        bool ReadBooleanProperty(string key);

        /// <summary>
        /// Read the value of an 8-bit integer property.
        /// </summary>
        /// <param name="key">The key of the key/value pair, to be used in content formats that support named keys.</param>
        byte ReadByteProperty(string key);

        /// <summary>
        /// Read the value of a 32-bit integer property.
        /// </summary>
        /// <param name="key">The key of the key/value pair, to be used in content formats that support named keys.</param>
        int ReadInt32Property(string key);

        /// <summary>
        /// Read the value of a 32-bit float property.
        /// </summary>
        /// <param name="key">The key of the key/value pair, to be used in content formats that support named keys.</param>
        float ReadFloatProperty(string key);

        /// <summary>
        /// Read the value of a 32-bit enum integer property.
        /// </summary>
        /// <param name="key">The key of the key/value pair, to be used in content formats that support named keys.</param>
        TEnum ReadEnumProperty<TEnum>(string key) where TEnum : struct, Enum;

        /// <summary>
        /// Read the value of a GUID property.
        /// </summary>
        /// <param name="key">The key of the key/value pair, to be used in content formats that support named keys.</param>
        Guid ReadGuidProperty(string key);

        /// <summary>
        /// Read the value of a string property.
        /// </summary>
        /// <param name="key">The key of the key/value pair, to be used in content formats that support named keys.</param>
        string ReadStringProperty(string key);

        /// <summary>
        /// Read the value of a byte array property.
        /// </summary>
        /// <param name="key">The key of the key/value pair, to be used in content formats that support named keys.</param>
        byte[] ReadByteArrayProperty(string key);

        /// <summary>
        /// Reads a raw string token from the stream, such as an array element.
        /// </summary>
        string ReadStringValue();

        /// <summary>
        /// Reads a file attachment.
        /// </summary>
        /// <param name="key">Identifier for this attachment. It must be unique throughout the entire module; not just this asset.</param>
        byte[] ReadAttachment(string key);

        /// <summary>
        /// Begin reading from the nested object defined by the specified key.
        /// </summary>
        /// <param name="key">The key of the key/value pair, to be used in content formats that support named keys.</param>
        void BeginObject(string key = null);

        /// <summary>
        /// Begin reading from the nested array defined by the specified key.
        /// </summary>
        /// <param name="key">The key of the key/value pair, to be used in content formats that support named keys.</param>
        int BeginArray(string key);

        /// <summary>
        /// Exit out of an object that was opened with BeginObject(). This function must be called an equal number of times as BeginObject().
        /// </summary>
        void EndObject();

        /// <summary>
        /// Exit out of an array that was opened with BeginObject(). This function must be called an equal number of times as BeginObject().
        /// </summary>
        void EndArray();

    }

}
