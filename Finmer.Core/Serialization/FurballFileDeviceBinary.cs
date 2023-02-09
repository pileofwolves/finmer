/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.IO;
using System.Linq;
using System.Text;
using Finmer.Core.Assets;

namespace Finmer.Core.Serialization
{

    /// <summary>
    /// Implementation of a file device that reads or writes compact binary packages.
    /// </summary>
    public sealed class FurballFileDeviceBinary : FurballFileDevice
    {

        private static readonly char[] k_FurballHeader = { 'F', 'U', 'R', 'B', 'A', 'L', 'L' };

        public override Furball ReadModule(FileInfo file)
        {
            try
            {
                using (var file_stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                using (var instream = new BinaryReader(file_stream, Encoding.UTF8, true))
                {
                    // Read basic configuration
                    var output = new Furball
                    {
                        Metadata = ReadHeaderFromStream(instream, out int file_version)
                    };

                    // Read dependencies list
                    for (int num_dependencies = instream.ReadInt32(); num_dependencies > 0; num_dependencies--)
                    {
                        output.Dependencies.Add(new FurballDependency
                        {
                            ID = new Guid(instream.ReadBytes(16)),
                            FileNameHint = instream.ReadString()
                        });
                    }

                    // Read asset blobs
                    for (int num_assets = instream.ReadInt32(); num_assets > 0; num_assets--)
                    {
                        var asset = ReadAssetFromStream(instream, file_version);
                        asset.SourceModuleName = file.Name;
                        output.Assets.Add(asset);
                    }

                    return output;
                }
            }
            catch (IOException ex)
            {
                throw new FurballIOException(ex.Message, ex);
            }
        }

        public override FurballMetadata ReadMetadata(FileInfo file)
        {
            try
            {
                using (var file_stream = new FileStream(file.FullName, FileMode.Open))
                using (var instream = new BinaryReader(file_stream, Encoding.UTF8, true))
                {
                    return ReadHeaderFromStream(instream, out int _);
                }
            }
            catch (IOException ex)
            {
                throw new FurballIOException(ex.Message, ex);
            }
        }

        public override void WriteModule(Furball furball, FileInfo file)
        {
            try
            {
                using (var stream = new FileStream(file.FullName, FileMode.Create))
                using (var outstream = new BinaryWriter(stream, Encoding.UTF8, true))
                {
                    // File header
                    outstream.Write(k_FurballHeader);
                    outstream.Write(k_LatestVersion);
                    outstream.Write(furball.Metadata.ID.ToByteArray());
                    outstream.Write(furball.Metadata.Title);
                    outstream.Write(furball.Metadata.Author);

                    // Dependency table
                    outstream.Write(furball.Dependencies.Count);
                    foreach (FurballDependency dependency in furball.Dependencies)
                    {
                        outstream.Write(dependency.ID.ToByteArray());
                        outstream.Write(dependency.FileNameHint);
                    }

                    // Asset table
                    outstream.Write(furball.Assets.Count);
                    var content_writer = new FurballContentWriterBinary(outstream);
                    foreach (AssetBase asset in furball.Assets)
                    {
                        AssetSerializer.SerializeAsset(content_writer, asset);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FurballIOException(ex.Message, ex);
            }
        }

        private FurballMetadata ReadHeaderFromStream(BinaryReader instream, out int fileVersion)
        {
            // Verify file magic
            char[] header = instream.ReadChars(k_FurballHeader.Length);
            if (!header.SequenceEqual(k_FurballHeader))
                throw new FurballInvalidHeaderException("Invalid asset package header");

            // Verify file version
            fileVersion = instream.ReadByte();
            if (fileVersion != k_LatestVersion)
                throw new FurballInvalidHeaderException($"Incompatible asset package file version (got version {fileVersion}, expected version {k_LatestVersion})");

            return new FurballMetadata
            {
                ID = new Guid(instream.ReadBytes(16)),
                Title = instream.ReadString(),
                Author = instream.ReadString(),
            };
        }

        private AssetBase ReadAssetFromStream(BinaryReader instream, int version)
        {
            // Deserialize an asset of the appropriate type
            IFurballContentReader content_reader = new FurballContentReaderBinary(instream);
            AssetBase asset = AssetSerializer.DeserializeAsset(content_reader, version) as AssetBase;
            if (asset == null)
                throw new FurballInvalidAssetException("Could not parse asset in stream");

            return asset;
        }

    }

}
