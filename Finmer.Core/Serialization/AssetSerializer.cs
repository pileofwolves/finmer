/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Finmer.Core.Serialization
{

    /// <summary>
    /// Provides utilities for (de)serializing asset objects from filesystem-agnostic streams.
    /// </summary>
    public static class AssetSerializer
    {

        private static readonly Dictionary<string, Func<IFurballSerializable>> s_NameToCtorMap;
        private static readonly Dictionary<int, Func<IFurballSerializable>> s_HashToCtorMap;

        private const string k_FurballObjectTypeKey = @"!Type";

        static AssetSerializer()
        {
            s_NameToCtorMap = new Dictionary<string, Func<IFurballSerializable>>();
            s_HashToCtorMap = new Dictionary<int, Func<IFurballSerializable>>();

            // Discover all IFurballSerializable types in this assembly
            var assembly = Assembly.GetCallingAssembly();
            var furball_types = assembly.GetExportedTypes()
                .Where(t => !t.IsAbstract && typeof(IFurballSerializable).IsAssignableFrom(t))
                .Where(t => t.GetCustomAttribute<AbstractFurballSerializableAttribute>() == null);

            // Register those types
            foreach (var type in furball_types)
            {
                // Advanced wizardry: dynamically compile a lambda that will produce a new instance of the input type when called.
                // This is much, much faster than using Activator.CreateInstance() to produce instances.
                var ctor = type.GetConstructor(Type.EmptyTypes) ?? throw new InvalidOperationException();
                var lambda = Expression.Lambda<Func<IFurballSerializable>>(
                    Expression.New(ctor)).Compile();

                // Store the type
                s_NameToCtorMap.Add(type.Name, lambda);
                s_HashToCtorMap.Add(ComputeTypeHash(type), lambda);
            }
        }

        /// <summary>
        /// Instantiates and deserializes an asset from the input stream.
        /// </summary>
        public static IFurballSerializable DeserializeAsset(IFurballContentReader instream)
        {
            // Obtain the ctor for this object
            // Admittedly doing this explicit type check is a hack, but since the type ID is internal to AssetSerializer to begin
            // with, it shouldn't be a stretch to also have it decide how exactly the type ID ought to be serialized.
            Func<IFurballSerializable> constructor;
            if (instream is FurballContentReaderBinary binary)
                s_HashToCtorMap.TryGetValue(binary.ReadInt32Property(k_FurballObjectTypeKey), out constructor);
            else
                s_NameToCtorMap.TryGetValue(instream.ReadStringProperty(k_FurballObjectTypeKey), out constructor);

            // Validate that the type is known
            if (constructor == null)
                throw new FurballUnknownAssetException("Unknown asset type ID");

            // Instantiate the object
            var asset = constructor();

            // Read its data from stream
            asset.Deserialize(instream);

            return asset;
        }

        /// <summary>
        /// Serializes an asset to an output stream.
        /// </summary>
        public static void SerializeAsset(IFurballContentWriter outstream, IFurballSerializable asset)
        {
            // Forbid serialization of types that have been marked as abstract
            if (asset.GetType().GetCustomAttribute<AbstractFurballSerializableAttribute>() != null)
                throw new FurballInvalidAssetException($"Cannot serialize type {asset.GetType().Name} because it is marked as abstract");

            // Write the type identifier to the stream
            if (outstream is FurballContentWriterBinary binary)
                binary.WriteInt32Property(k_FurballObjectTypeKey, ComputeTypeHash(asset.GetType()));
            else
                outstream.WriteStringProperty(k_FurballObjectTypeKey, asset.GetType().Name);

            // Write asset contents
            asset.Serialize(outstream);
        }

        /// <summary>
        /// Create and return a deep copy of a serializable object.
        /// </summary>
        public static TAsset DuplicateAsset<TAsset>(TAsset input) where TAsset : IFurballSerializable
        {
            using (var ms = new MemoryStream())
            {
                // Write a serialized representation of the asset
                using (var writer = new BinaryWriter(ms, Encoding.UTF8, true))
                {
                    var serializer = new FurballContentWriterBinary(writer);
                    SerializeAsset(serializer, input);
                }

                // Seek back to the start of the stream, so we can read the data that was just written
                ms.Seek(0, SeekOrigin.Begin);

                // Read the serialized asset as if it's a new object, thus effectively duplicating it
                using (var reader = new BinaryReader(ms, Encoding.UTF8, true))
                {
                    var deserializer = new FurballContentReaderBinary(reader, FurballFileDevice.k_LatestVersion);
                    return (TAsset)DeserializeAsset(deserializer);
                }
            }
        }

        /// <summary>
        /// Returns a stable (deterministic) 32-bit hash given an input Type.
        /// </summary>
        private static int ComputeTypeHash(Type type)
        {
            // Managed hash implementation taken mostly from .NET's String.GetHashCode (but stable across versions)
            unchecked
            {
                var input = type.Name;
                int hash1 = 5381;
                int hash2 = hash1;

                for (int i = 0; i < input.Length && input[i] != '\0'; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ input[i];
                    if (i == input.Length - 1 || input[i + 1] == '\0')
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ input[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }

    }

}
