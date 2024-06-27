/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Finmer.Core.Serialization
{

    /// <summary>
    /// Implementation of an asset data writer that generates compact, binary package files.
    /// </summary>
    public sealed class FurballContentWriterBinary : IFurballContentWriter
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

        public void WriteInt32Property(string key, int value)
        {
            m_Stream.Write(value);
        }

        public void WriteCompressedInt32Property(string key, int value)
        {
            Write7BitEncodedInt(value);
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

            // Find the size in bytes of the underlying type
            var underlying_type = Enum.GetUnderlyingType(enum_type);
            int underlying_size = Marshal.SizeOf(underlying_type);

            // Serialize the smallest type that will fit the enum
            switch (underlying_size)
            {
                case 1:     m_Stream.Write(Convert.ToByte(value));      break;
                case 2:     m_Stream.Write(Convert.ToUInt16(value));    break;
                case 4:     m_Stream.Write(Convert.ToUInt32(value));    break;
                default:    throw new NotSupportedException();
            }
        }

        public void WriteGuidProperty(string key, Guid value)
        {
            m_Stream.Write(value.ToByteArray());
        }

        public void WriteStringProperty(string key, string value)
        {
            m_Stream.Write(value);
        }

        public void WriteObjectProperty(string key, IFurballSerializable value, EFurballObjectMode mode)
        {
            // Input object may be absent
            if (value == null)
            {
                // If the object is mandatory, throw
                if (mode == EFurballObjectMode.Required)
                    throw new FurballInvalidAssetException($"Property {key} cannot be null");

                // Write prefix indicating an optional object is absent
                m_Stream.Write(false);
                return;
            }

            // Write a prefix indicating that the optional object is present
            if (mode == EFurballObjectMode.Optional)
                m_Stream.Write(true);

            // Recursively serialize the asset
            AssetSerializer.SerializeAsset(this, value);
        }

        public void WriteStringValue(string value)
        {
            m_Stream.Write(value);
        }

        public void WriteAttachment(string key, byte[] value)
        {
            // Implemented as in-place byte array
            if (value == null)
            {
                Write7BitEncodedInt(0);
            }
            else
            {
                if (value.Length == 0)
                    throw new ArgumentException("Attempt to write zero-length byte array", nameof(value));

                Write7BitEncodedInt(value.Length);
                m_Stream.Write(value);
            }
        }

        public void BeginObject(string key = null)
        {
            // Irrelevant for binary objects
        }

        public void BeginArray(string key, int numElements)
        {
            Write7BitEncodedInt(numElements);
        }

        public void EndObject()
        {
            // Irrelevant for binary objects
        }

        public void EndArray()
        {
            // Irrelevant for binary objects
        }

        private void Write7BitEncodedInt(int value)
        {
            // The following is a re-implementation of Write7BitEncodedInt() from BinaryWriter

            uint unsigned_value = (uint)value;

            while (unsigned_value >= 0x80u) {
                m_Stream.Write((byte) (unsigned_value | 0x80u));
                unsigned_value >>= 7;
            }

            m_Stream.Write((byte)unsigned_value);
        }

    }

}
