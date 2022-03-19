/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Finmer.Core.Assets;

namespace Finmer.Core.Serialization
{

    /// <summary>
    /// Provides utilities for (de)serializing asset objects from filesystem-agnostic streams.
    /// </summary>
    public static class AssetSerializer
    {

        private static readonly Dictionary<string, Func<IFurballSerializable>> s_NameToObjectMap;

        static AssetSerializer()
        {
            s_NameToObjectMap = new Dictionary<string, Func<IFurballSerializable>>();

            // Discover all IFurballSerializable types in this assembly
            var assembly = Assembly.GetCallingAssembly();
            var furball_types = assembly.GetExportedTypes()
                .Where(t => !t.IsAbstract && typeof(IFurballSerializable).IsAssignableFrom(t))
                .Where(t => t != typeof(ScriptDataWrapper)); // Explicitly excluded, not meant to be serialized

            // Register those types
            foreach (var type in furball_types)
            {
                // Advanced wizardry: dynamically compile a lambda that will produce a new instance of the input type when called.
                // This is much, much faster than using Activator.CreateInstance() to produce instances.
                var ctor = Expression.Lambda<Func<IFurballSerializable>>(
                    Expression.New(type.GetConstructor(Type.EmptyTypes))).Compile();

                // Store the type
                s_NameToObjectMap.Add(type.Name, ctor);
            }
        }

        /// <summary>
        /// Instantiates and deserializes an asset from the input stream.
        /// </summary>
        public static IFurballSerializable DeserializeAsset(IFurballContentReader instream, int version)
        {
            // Instantiate the asset itself
            var type = instream.ReadStringProperty(@"!Type");
            var asset = InstantiateAsset(type);

            // Read its data from stream
            asset.Deserialize(instream, version);

            return asset;
        }

        /// <summary>
        /// Serializes an asset to an output stream.
        /// </summary>
        public static void SerializeAsset(IFurballContentWriter outstream, IFurballSerializable asset)
        {
            // Write the type identifier to the stream
            outstream.WriteStringProperty(@"!Type", IdentifyAsset(asset));

            // Write asset contents
            asset.Serialize(outstream);
        }

        /// <summary>
        /// Factory function that instantiates an asset object based on its type ID.
        /// </summary>
        private static IFurballSerializable InstantiateAsset(string typeName)
        {
            // Find the constructor that will produce an instance of the desired asset
            if (s_NameToObjectMap.TryGetValue(typeName, out var ctor))
                return ctor();

            throw new FurballUnknownAssetException("Unknown asset type ID");
        }

        /// <summary>
        /// Given an asset instance, returns the value that must be passed to InstantiateAsset() to return a new instance of the same type.
        /// </summary>
        private static string IdentifyAsset(IFurballSerializable asset)
        {
            // Unwrap script wrappers
            if (asset is ScriptDataWrapper wrapper)
                asset = wrapper.Wrapped;

            // Simply return the name of the object's type. We don't perform any further checks or lookups here since we assume
            // that, as it is derived from IFurballSerializable, at deserialization time the type is present in the ctor lookup table.
            return asset.GetType().Name;
        }

    }

}
