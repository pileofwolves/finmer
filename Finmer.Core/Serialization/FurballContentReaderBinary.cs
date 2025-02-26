/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
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
        private readonly uint m_FormatVersion;

        public FurballContentReaderBinary(BinaryReader instream, uint format_version)
        {
            m_Stream = instream;
            m_FormatVersion = format_version;
        }

        public uint GetFormatVersion()
        {
            return m_FormatVersion;
        }

        public bool ReadBooleanProperty(string key)
        {
            return m_Stream.ReadBoolean();
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

        public TExpected ReadObjectProperty<TExpected>(string key, EFurballObjectMode mode) where TExpected : class, IFurballSerializable
        {
            // If the object is required, the presence prefix is omitted
            if (mode == EFurballObjectMode.Optional || GetFormatVersion() < 21)
            {
                // A single byte indicates whether the asset is null or not
                bool is_present = m_Stream.ReadBoolean();
                if (!is_present)
                    return null;
            }

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
            // Attachments are implemented as an in-place byte array.
            // Note tha in format 21 the length prefix for an omitted byte array changed from -1 to 0.
            int length = Read7BitEncodedInt();
            if (length <= 0)
                return null;

            return m_Stream.ReadBytes(length);
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
            if (m_FormatVersion < 21)
                return m_Stream.ReadInt32();

            // Compressed integers are encoded as a sequence of bytes where the lower 7 bits represent chunks of the integer,
            // and the upper bit is set if another byte follows. This allows for compact encoding of small integers.
            int output = 0;
            int shift = 0;
            byte next;
            do
            {
                // A compressed integer can be up to 5 bytes long; if we are shifting further than that, the stream is corrupt
                if (shift >= 5 * 7)
                    throw new FurballInvalidAssetException("Compressed integer is too long");

                // Read the next byte, and mask the lower 7 bits into 
                next = m_Stream.ReadByte();
                output |= (next & 0x7F) << shift;
                shift += 7;
            } while ((next & 0x80) != 0);

            return output;
        }

    }

}
