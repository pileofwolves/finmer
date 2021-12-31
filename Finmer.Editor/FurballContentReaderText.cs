/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Finmer.Core.Serialization;
using Newtonsoft.Json.Linq;

namespace Finmer.Editor
{

    /// <summary>
    /// Represents a content stream that consumes a JSON file, and optionally additional loose files (attachments) on disk.
    /// </summary>
    public sealed class FurballContentReaderText : IFurballContentReader
    {

        private JToken CurrentToken => m_TokenStack.Peek();

        private readonly DirectoryInfo m_SearchPath;
        private readonly Stack<JToken> m_TokenStack = new Stack<JToken>();
        private readonly Stack<JToken> m_ArrayStack = new Stack<JToken>();
        private JToken m_CurrentArrayElement;

        public FurballContentReaderText(JObject root, DirectoryInfo searchPath)
        {
            m_SearchPath = searchPath;
            m_TokenStack.Push(root);
        }

        public bool ReadBooleanProperty(string key)
        {
            try
            {
                Debug.Assert(CurrentToken.Type == JTokenType.Object);
                return (bool)CurrentToken[key];
            }
            catch (Exception ex)
            {
                throw new FurballInvalidAssetException($"Cannot read boolean {key} at path {CurrentToken.Path}", ex);
            }
        }

        public byte ReadByteProperty(string key)
        {
            try
            {
                Debug.Assert(CurrentToken.Type == JTokenType.Object);
                return (byte)CurrentToken[key];
            }
            catch (Exception ex)
            {
                throw new FurballInvalidAssetException($"Cannot read byte {key} at path {CurrentToken.Path}", ex);
            }
        }

        public int ReadInt32Property(string key)
        {
            try
            {
                Debug.Assert(CurrentToken.Type == JTokenType.Object);
                return (int)CurrentToken[key];
            }
            catch (Exception ex)
            {
                throw new FurballInvalidAssetException($"Cannot read int {key} at path {CurrentToken.Path}", ex);
            }
        }

        public float ReadFloatProperty(string key)
        {
            try
            {
                Debug.Assert(CurrentToken.Type == JTokenType.Object);
                return (float)CurrentToken[key];
            }
            catch (Exception ex)
            {
                throw new FurballInvalidAssetException($"Cannot read float {key} at path {CurrentToken.Path}", ex);
            }
        }

        public TEnum ReadEnumProperty<TEnum>(string key) where TEnum : struct, Enum
        {
            var enum_type = typeof(TEnum);
            Debug.Assert(enum_type.IsEnum);

            // Read the raw value as a string
            string name = ReadStringProperty(key);

            // Convert it to a member of the generic enum
            if (!Enum.TryParse(name, out TEnum enum_value))
                throw new FurballInvalidAssetException($"Property '{key}' has value '{name}' which is not a member of enum type '{enum_type.Name}'");

            return enum_value;
        }

        public Guid ReadGuidProperty(string key)
        {
            return Guid.Parse(ReadStringProperty(key));
        }

        public string ReadStringProperty(string key)
        {
            try
            {
                Debug.Assert(CurrentToken.Type == JTokenType.Object);

                // String properties may be omitted from the document, to help reduce file size.
                // In that case, they are interpreted as empty strings.
                JToken element = CurrentToken[key];
                if (element == null)
                    return String.Empty;

                // Property is present; convert it to a string
                return (string)element;
            }
            catch (Exception ex)
            {
                throw new FurballInvalidAssetException($"Cannot read string {key} at path {CurrentToken.Path}", ex);
            }
        }

        public byte[] ReadByteArrayProperty(string key)
        {
            try
            {
                Debug.Assert(CurrentToken.Type == JTokenType.Object);
                JToken value = CurrentToken[key];

                // Handle null values properly
                if (value == null || value.Type == JTokenType.Null)
                    return null;

                // Otherwise, deserialize the byte array
                return (byte[])value;
            }
            catch (Exception ex)
            {
                throw new FurballInvalidAssetException($"Cannot read byte array {key} at path {CurrentToken.Path}", ex);
            }
        }

        public string ReadStringValue()
        {
            // Read the value of the next array element as a string
            string value = (string)m_CurrentArrayElement;

            // Move the array pointer to the next sibling
            m_CurrentArrayElement = m_CurrentArrayElement.Next;

            return value;
        }

        public byte[] ReadAttachment(string key)
        {
            // The file should be in the project directory
            string attachment_path = Path.Combine(m_SearchPath.FullName, key);

            try
            {
                // If it doesn't exist at all, just return null
                if (!File.Exists(attachment_path))
                    return null;

                // Otherwise, read and return the file contents
                return File.ReadAllBytes(attachment_path);
            }
            catch (Exception ex)
            {
                throw new FurballIOException($"Failed to read the attachment file '{key}'", ex);
            }
        }

        public void BeginObject(string key = null)
        {
            // Named property in object
            if (key != null)
            {
                // Look for the JToken with the specified key
                JToken child = CurrentToken[key];
                if (child == null || child.Type != JTokenType.Object)
                    throw new FurballInvalidAssetException($"Object property '{key}' not found at path {CurrentToken.Path}");

                // Enter this object
                m_TokenStack.Push(child);
            }
            // Unnamed object
            else
            {
                // Unnamed objects can occur only inside an array
                Debug.Assert(CurrentToken.Type == JTokenType.Array);

                // Make sure there is another object to enter into
                if (m_CurrentArrayElement == null || m_CurrentArrayElement.Type != JTokenType.Object)
                    throw new FurballInvalidAssetException($"Attempted to read past end of array {CurrentToken.Path}");

                // Enter the object
                m_TokenStack.Push(m_CurrentArrayElement);

                // Update array element pointer so it references the element that comes after the one we just pushed
                m_CurrentArrayElement = m_CurrentArrayElement.Next;
            }
        }

        public int BeginArray(string key)
        {
            // Nested arrays are currently not supported, there shouldn't be a use case for them
            Debug.Assert(CurrentToken.Type != JTokenType.Array);

            // Look for the JToken with the specified key
            JToken child = CurrentToken[key];
            if (child == null || child.Type != JTokenType.Array)
                throw new FurballInvalidAssetException($"Array property '{key}' not found at path {CurrentToken.Path}");

            // Enter this object. The current array element pointer is stored, so it can be restored later when exiting out of this new array.
            m_TokenStack.Push(child);
            m_ArrayStack.Push(m_CurrentArrayElement);
            m_CurrentArrayElement = child.First;

            // Return the number of elements in this array
            return child.Children().Count();
        }

        public void EndObject()
        {
            // Remove the topmost token from the stack, which should be an object
            Debug.Assert(CurrentToken.Type == JTokenType.Object);
            m_TokenStack.Pop();
        }

        public void EndArray()
        {
            // Remove the topmost token from the stack, which should be an array
            Debug.Assert(CurrentToken.Type == JTokenType.Array);
            m_TokenStack.Pop();
            m_CurrentArrayElement = m_ArrayStack.Pop();
        }

    }

}
