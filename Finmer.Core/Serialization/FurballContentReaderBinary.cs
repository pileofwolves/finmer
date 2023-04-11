/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
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

        public FurballContentReaderBinary(BinaryReader instream)
        {
            m_Stream = instream;
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
            int length = m_Stream.ReadInt32();
            if (length < 0)
                return null;

            return m_Stream.ReadBytes(length);
        }

        public TExpected ReadNestedObjectProperty<TExpected>( string key, int version) where TExpected : class, IFurballSerializable
        {
            // A single byte indicates whether the asset is null or not
            bool is_present = m_Stream.ReadBoolean();
            if (!is_present)
                return null;

            // It is present, so recursively deserialize it
            var asset = AssetSerializer.DeserializeAsset(this, version);
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
            return m_Stream.ReadInt32();
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
