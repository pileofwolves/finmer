/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Finmer.Core
{

    /// <summary>
    /// Represents a collection of key/value pairs of various value types.
    /// </summary>
    public sealed class PropertyBag : ICloneable
    {

        private readonly HashSet<string> m_PropBool;
        private readonly Dictionary<string, byte[]> m_PropBytes;
        private readonly Dictionary<string, float> m_PropFloat;
        private readonly Dictionary<string, int> m_PropInt;
        private readonly Dictionary<string, string> m_PropString;

        /// <summary>
        /// Constructs an empty property bag.
        /// </summary>
        public PropertyBag()
        {
            m_PropBool = new HashSet<string>();
            m_PropBytes = new Dictionary<string, byte[]>();
            m_PropInt = new Dictionary<string, int>();
            m_PropFloat = new Dictionary<string, float>();
            m_PropString = new Dictionary<string, string>();
        }

        public object Clone()
        {
            // Create a deep copy by serializing and deserializing the properties
            using (var ms = new MemoryStream())
            {
                // Serialize the PropertyBag
                using (var writer = new BinaryWriter(ms, Encoding.UTF8, true))
                    Serialize(writer);

                // Read a new PropertyBag from the generated stream
                ms.Seek(0, SeekOrigin.Begin);
                using (var reader = new BinaryReader(ms, Encoding.UTF8, true))
                    return FromStream(reader);
            }
        }

        /// <summary>
        /// Removes all properties from the collection.
        /// </summary>
        public void Clear()
        {
            m_PropBool.Clear();
            m_PropBytes.Clear();
            m_PropInt.Clear();
            m_PropFloat.Clear();
            m_PropString.Clear();
        }

        /// <summary>
        /// Returns an integer value associated with a specified key.
        /// </summary>
        /// <param name="key">The key to look up.</param>
        /// <param name="defaultValue">The default value to return if the key is absent.</param>
        public int GetInt(string key, int defaultValue = 0)
        {
            if (!m_PropInt.TryGetValue(key.ToUpperInvariant(), out var value))
                value = defaultValue;

            return value;
        }

        /// <summary>
        /// Sets an integer as the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key to set or change.</param>
        /// <param name="value">The new value to associate with the key.</param>
        public void SetInt(string key, int value)
        {
            m_PropInt[key.ToUpperInvariant()] = value;
        }

        /// <summary>
        /// Returns a float associated with a specified key.
        /// </summary>
        /// <param name="key">The key to look up.</param>
        /// <param name="defaultValue">The default value to return if the key is absent.</param>
        public float GetFloat(string key, float defaultValue = 0.0f)
        {
            if (!m_PropFloat.TryGetValue(key.ToUpperInvariant(), out var value))
                value = defaultValue;

            return value;
        }

        /// <summary>
        /// Sets a float as the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key to set or change.</param>
        /// <param name="value">The new value to associate with the key.</param>
        public void SetFloat(string key, float value)
        {
            m_PropFloat[key.ToUpperInvariant()] = value;
        }

        /// <summary>
        /// Returns a boolean value associated with a specified key.
        /// </summary>
        /// <param name="key">The key to look up.</param>
        public bool GetBool(string key)
        {
            return m_PropBool.Contains(key.ToUpperInvariant());
        }

        /// <summary>
        /// Sets a boolean as the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key to set or change.</param>
        /// <param name="value">The new value to associate with the key.</param>
        public void SetBool(string key, bool value)
        {
            // Boolean values are implemented slightly differently: since the value of an unspecified key defaults to false,
            // we can store only the keys whose values are set to 'true', and discard all others.
            key = key.ToUpperInvariant();
            if (value)
                m_PropBool.Add(key);
            else
                m_PropBool.Remove(key);
        }

        /// <summary>
        /// Returns the string associated with a specified key.
        /// </summary>
        /// <param name="key">The key to look up.</param>
        /// <param name="defaultValue">The default value to return if the key is absent.</param>
        public string GetString(string key, string defaultValue = "")
        {
            if (!m_PropString.TryGetValue(key.ToUpperInvariant(), out var value))
                value = defaultValue;

            return value;
        }

        /// <summary>
        /// Sets the string associated with the specified key.
        /// </summary>
        /// <param name="key">The key to set or change.</param>
        /// <param name="value">The new value to associate with the key.</param>
        public void SetString(string key, string value)
        {
            m_PropString[key.ToUpperInvariant()] = value;
        }

        /// <summary>
        /// Returns a byte array associated with a specified key. Returns null if key does not exist.
        /// </summary>
        /// <param name="key">The key to look up.</param>
        public byte[] GetBytes(string key)
        {
            if (m_PropBytes.TryGetValue(key.ToUpperInvariant(), out var value))
                return value;

            return null;
        }

        /// <summary>
        /// Sets the byte array associated with the specified key.
        /// </summary>
        /// <param name="key">The key to set or change.</param>
        /// <param name="value">The new value to associate with the key.</param>
        public void SetBytes(string key, byte[] value)
        {
            key = key.ToUpperInvariant();
            if (value == null)
            {
                // can't save null arrays, so when setting value to null, just delete the key/value pair
                m_PropBytes.Remove(key);
                return;
            }

            m_PropBytes[key] = value;
        }

        /// <summary>
        /// Deserializes a nested PropertyBag instance. Returns null if key does not exist.
        /// </summary>
        /// <param name="key">The key to look up.</param>
        public PropertyBag GetNestedPropertyBag(string key)
        {
            byte[] rawbytes = GetBytes("__nestedpb_" + key);
            if (rawbytes == null)
                return null;

            using (var ms = new MemoryStream(rawbytes))
            {
                using (var reader = new BinaryReader(ms, Encoding.UTF8, true))
                {
                    return FromStream(reader);
                }
            }
        }

        /// <summary>
        /// Serializes a nested PropertyBag instance into a byte array.
        /// </summary>
        /// <param name="key">The key to set or change.</param>
        /// <param name="child">The new value to associate with the key. Specify null to delete the key/value pair.</param>
        public void SetNestedPropertyBag(string key, PropertyBag child)
        {
            // A null value indicates that the key/value pair should be erased
            if (child == null)
            {
                SetBytes(key, null);
                return;
            }

            // Store the nested PropertyBag as a byte array
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms, Encoding.UTF8, true))
                {
                    child.Serialize(writer);
                }

                SetBytes("__nestedpb_" + key, ms.ToArray());
            }
        }

        /// <summary>
        /// Returns a list of table keys that have the specified prefix.
        /// </summary>
        /// <param name="prefix">The prefix to look up.</param>
        public IEnumerable<KeyValuePair<string, string>> GetKeysByPrefix(string prefix)
        {
            prefix = prefix.ToUpperInvariant();
            return m_PropString
                .Where(prop => prop.Key.StartsWith(prefix));
        }

        /// <summary>
        /// Writes the contents of this PropertyBag to a stream.
        /// </summary>
        /// <param name="outstream">A wrapper around the stream to write to.</param>
        public void Serialize(BinaryWriter outstream)
        {
            outstream.Write(m_PropBool.Count);
            m_PropBool.ForEach(outstream.Write);

            outstream.Write(m_PropBytes.Count);
            foreach (var pair in m_PropBytes)
            {
                outstream.Write(pair.Key);
                outstream.Write(pair.Value.Length);
                outstream.Write(pair.Value);
            }

            outstream.Write(m_PropInt.Count);
            foreach (var pair in m_PropInt)
            {
                outstream.Write(pair.Key);
                outstream.Write(pair.Value);
            }

            outstream.Write(m_PropFloat.Count);
            foreach (var pair in m_PropFloat)
            {
                outstream.Write(pair.Key);
                outstream.Write(pair.Value);
            }

            outstream.Write(m_PropString.Count);
            foreach (var pair in m_PropString)
            {
                outstream.Write(pair.Key);
                outstream.Write(pair.Value);
            }
        }

        /// <summary>
        /// Parses a PropertyBag from a stream, and returns it.
        /// </summary>
        /// <param name="instream">The stream to read from.</param>
        public static PropertyBag FromStream(BinaryReader instream)
        {
            PropertyBag output = new PropertyBag();

            // Flags
            int count;
            for (count = instream.ReadInt32(); count > 0; count--)
                output.SetBool(instream.ReadString(), true);

            // Byte arrays
            for (count = instream.ReadInt32(); count > 0; count--)
                output.SetBytes(instream.ReadString(), instream.ReadBytes(instream.ReadInt32()));

            // Integers
            for (count = instream.ReadInt32(); count > 0; count--)
                output.SetInt(instream.ReadString(), instream.ReadInt32());

            // Floats
            for (count = instream.ReadInt32(); count > 0; count--)
                output.SetFloat(instream.ReadString(), instream.ReadSingle());

            // Strings
            for (count = instream.ReadInt32(); count > 0; count--)
                output.SetString(instream.ReadString(), instream.ReadString());

            return output;
        }

    }

}
