/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Finmer.Core.Serialization
{

    /// <summary>
    /// Implementation of an asset data reader that operates on compact, binary package files.
    /// </summary>
    public sealed class FurballContentReaderBinary : IFurballContentReader
    {

        private readonly BinaryReader m_Stream;
        private readonly int m_Version;

        public FurballContentReaderBinary(BinaryReader instream, int version)
        {
            m_Stream = instream;
            m_Version = version;
        }

        public int GetVersion()
        {
            return m_Version;
        }

        public bool ReadBooleanProperty(string key)
        {
            return m_Stream.ReadBoolean();
        }

        public byte ReadByteProperty(string key)
        {
            return m_Stream.ReadByte();
        }

        public int ReadInt32Property(string key)
        {
            return m_Stream.ReadInt32();
        }

        public int ReadCompressedInt32Property(string key)
        {
            return Read7BitEncodedInt();
        }

        public float ReadFloatProperty(string key)
        {
            return m_Stream.ReadSingle();
        }

        public TEnum ReadEnumProperty<TEnum>(string key) where TEnum : struct, Enum
        {
            // Find the size in bytes of the underlying type
            var underlying_type = Enum.GetUnderlyingType(typeof(TEnum));
            int underlying_size = Marshal.SizeOf(underlying_type);

            // Deserialize the same underlying type
            // Key and enum type are ignored, since the file stores only the raw integer value.
            int value;
            switch (underlying_size)
            {
                case 1:     value = m_Stream.ReadByte();            break;
                case 2:     value = m_Stream.ReadUInt16();          break;
                case 4:     value = (int)m_Stream.ReadUInt32();     break;
                default:    throw new NotSupportedException();
            }

            // Cast the deserialized integer value to the enum type.
            // There doesn't seem to be an easy way to cast an int to a generic enum, so we kinda have to force the compiler's paw here.
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        }

        public Guid ReadGuidProperty(string key)
        {
            return new Guid(m_Stream.ReadBytes(16));
        }

        public string ReadStringProperty(string key)
        {
            return m_Stream.ReadString();
        }

        public byte[] ReadByteArrayProperty(string key)
        {
            // In versions <= 20, we used to write -1 for null byte arrays (we now write 0)
            // This method will handle that fine, since it delegates to ReadInt32Property in those versions
            int length = Read7BitEncodedInt();
            if (length <= 0)
                return null;

            return m_Stream.ReadBytes(length);
        }

        public TExpected ReadNestedObjectProperty<TExpected>(string key) where TExpected : class, IFurballSerializable
        {
            // A single byte indicates whether the asset is null or not
            bool is_present = m_Stream.ReadBoolean();
            if (!is_present)
                return null;

            // It is present, so recursively deserialize it
            var asset = AssetSerializer.DeserializeAsset(this);
            if (!(asset is TExpected expected))
                // Error handling here to remove boilerplate from callers
                throw new FurballInvalidAssetException($"Unexpected nested asset type in property '{key}'");

            return expected;
        }

        public string ReadStringValue()
        {
            return m_Stream.ReadString();
        }

        public byte[] ReadAttachment(string key)
        {
            // Implemented as in-place byte array
            return ReadByteArrayProperty(key);
        }

        public void BeginObject(string key = null)
        {
            // Irrelevant for binary objects
        }

        public int BeginArray(string key)
        {
            return Read7BitEncodedInt();
        }

        public void EndObject()
        {
            // Irrelevant for binary objects
        }

        public void EndArray()
        {
            // Irrelevant for binary objects
        }

        private int Read7BitEncodedInt()
        {
            // In versions 20 and below, these were plain uncompressed integers
            if (m_Version < 21)
                return m_Stream.ReadInt32();

            // The following is a re-implementation of Read7BitEncodedInt() from BinaryReader

            int read_value = 0; // The value being read
            int shift = 0;      // The position of the next seven bits
            byte next;          // The next byte to be read

            do {
                if (shift == 5 * 7)
                    throw new FormatException("Int too long");

                next = m_Stream.ReadByte();

                read_value |= (next & 0x7F) << shift;
                shift += 7;
            } while ((next & 0x80u) != 0);

            return read_value;
        }

    }

}
