/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.IO;

namespace Finmer.Core.Serialization
{

    /// <summary>
    /// Implementation of an asset data reader that operates on compact, binary package files.
    /// </summary>
    internal sealed class FurballContentReaderBinary : IFurballContentReader
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
            // Key and enum type are ignored, since the file stores only the raw integer value.
            // There doesn't seem to be an easy way to cast an int to a generic enum, so we kinda have to force the compiler's paw here.
            var value = m_Stream.ReadInt32();
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
