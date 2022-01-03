/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Diagnostics;
using System.IO;

namespace Finmer.Core.Serialization
{

    /// <summary>
    /// Implementation of an asset data writer that generates compact, binary package files.
    /// </summary>
    internal sealed class FurballContentWriterBinary : IFurballContentWriter
    {

        private readonly BinaryWriter m_Stream;

        public FurballContentWriterBinary(BinaryWriter outstream)
        {
            m_Stream = outstream;
        }

        public void WriteBooleanProperty(string key, bool value)
        {
            m_Stream.Write(value);
        }

        public void WriteByteProperty(string key, byte value)
        {
            m_Stream.Write(value);
        }

        public void WriteInt32Property(string key, int value)
        {
            m_Stream.Write(value);
        }

        public void WriteFloatProperty(string key, float value)
        {
            m_Stream.Write(value);
        }

        public void WriteEnumProperty<TEnum>(string key, TEnum value) where TEnum : struct, Enum
        {
            var enum_type = typeof(TEnum);
            Debug.Assert(enum_type.IsEnum);

            // Validate that the specified value is a valid value for the input enum type
            if (!Enum.IsDefined(enum_type, value))
                throw new FurballInvalidAssetException($"Property '{key}' has value {value} which is not valid for enum type '{enum_type.Name}'");

            // We don't actually need any further info about the enumerator here, since the value is already provided
            m_Stream.Write(Convert.ToInt32(value));
        }

        public void WriteGuidProperty(string key, Guid value)
        {
            m_Stream.Write(value.ToByteArray());
        }

        public void WriteStringProperty(string key, string value)
        {
            m_Stream.Write(value);
        }

        public void WriteByteArrayProperty(string key, byte[] value)
        {
            if (value == null)
            {
                m_Stream.Write(-1);
            }
            else
            {
                m_Stream.Write(value.Length);
                m_Stream.Write(value);
            }
        }

        public void WriteStringValue(string value)
        {
            m_Stream.Write(value);
        }

        public void WriteAttachment(string key, byte[] value)
        {
            // Implemented as in-place byte array
            WriteByteArrayProperty(key, value);
        }

        public void BeginObject(string key = null)
        {
            // Irrelevant for binary objects
        }

        public void BeginArray(string key, int numElements)
        {
            m_Stream.Write(numElements);
        }

        public void EndObject()
        {
            // Irrelevant for binary objects
        }

        public void EndArray()
        {
            // Irrelevant for binary objects
        }

    }

}
