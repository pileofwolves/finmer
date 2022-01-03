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
using Finmer.Core.Serialization;
using Newtonsoft.Json;

namespace Finmer.Editor
{

    /// <summary>
    /// Represents a content stream that outputs a JSON document plus one or more attachment files.
    /// </summary>
    public sealed class FurballContentWriterText : IFurballContentWriter
    {

        private readonly JsonWriter m_Stream;
        private readonly DirectoryInfo m_AttachmentsFolder;

        public FurballContentWriterText(JsonWriter outstream, DirectoryInfo attachmentsFolder)
        {
            m_Stream = outstream;
            m_AttachmentsFolder = attachmentsFolder;
        }

        public void WriteBooleanProperty(string key, bool value)
        {
            m_Stream.WritePropertyName(key);
            m_Stream.WriteValue(value);
        }

        public void WriteByteProperty(string key, byte value)
        {
            m_Stream.WritePropertyName(key);
            m_Stream.WriteValue(value);
        }

        public void WriteInt32Property(string key, int value)
        {
            m_Stream.WritePropertyName(key);
            m_Stream.WriteValue(value);
        }

        public void WriteFloatProperty(string key, float value)
        {
            m_Stream.WritePropertyName(key);
            m_Stream.WriteValue(value);
        }

        public void WriteEnumProperty<TEnum>(string key, TEnum value) where TEnum : struct, Enum
        {
            var enum_type = typeof(TEnum);
            Debug.Assert(enum_type.IsEnum);

            // Validate that the specified value is a valid value for the input enum type
            if (!Enum.IsDefined(enum_type, value))
                throw new FurballInvalidAssetException($"Property '{key}' has value {value} which is not valid for enum type '{enum_type.Name}'");

            // Write the name of the enum value to the output document
            string name = Enum.GetName(enum_type, value);
            m_Stream.WritePropertyName(key);
            m_Stream.WriteValue(name);
        }

        public void WriteGuidProperty(string key, Guid value)
        {
            m_Stream.WritePropertyName(key);
            m_Stream.WriteValue(value.ToString());
        }

        public void WriteStringProperty(string key, string value)
        {
            // Skip writing empty values, to help reduce file size. They will be interpreted as empty strings by the reader.
            if (String.IsNullOrEmpty(value))
                return;

            m_Stream.WritePropertyName(key);
            m_Stream.WriteValue(value);
        }

        public void WriteByteArrayProperty(string key, byte[] value)
        {
            m_Stream.WritePropertyName(key);
            m_Stream.WriteValue(value);
        }

        public void WriteStringValue(string value)
        {
            m_Stream.WriteValue(value);
        }

        public void WriteAttachment(string key, byte[] value)
        {
            // Avoid doing disk access with weird file names
            if (String.IsNullOrWhiteSpace(key))
                throw new FurballInvalidAssetException("Invalid attachment key");

            // To avoid accidentally overwriting asset files, prevent writing attachments with the .json extension
            if (key.EndsWith(".json", StringComparison.InvariantCultureIgnoreCase))
                throw new FurballInvalidAssetException($"Cannot use attachment key '{key}'");

            // If the specified value is null, erase the attachment, otherwise dump it to the file
            string attachment_path = Path.Combine(m_AttachmentsFolder.FullName, key);
            if (value == null)
                File.Delete(attachment_path);
            else
                File.WriteAllBytes(attachment_path, value);
        }

        public void BeginObject(string key = null)
        {
            if (key != null)
                m_Stream.WritePropertyName(key);

            m_Stream.WriteStartObject();
        }

        public void BeginArray(string key, int numElements)
        {
            if (key != null)
                m_Stream.WritePropertyName(key);

            m_Stream.WriteStartArray();
        }

        public void EndObject()
        {
            m_Stream.WriteEndObject();
        }

        public void EndArray()
        {
            m_Stream.WriteEndArray();
        }

    }

}
