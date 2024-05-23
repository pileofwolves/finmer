/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
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
                {
                    return ReadModuleFromStream(file_stream, true);
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
                {
                    return ReadModuleFromStream(file_stream, false).Metadata;
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
                {
                    // File header
                    using (var outstream = new BinaryWriter(stream, Encoding.UTF8, true))
                    {
                        outstream.Write(k_FurballHeader);
                        outstream.Write(k_LatestVersion);
                    }

                    // In format versions 21 and onwards, furballs are GZIP compressed
                    using (var outstream = new GZipBinaryWriter(stream, Encoding.UTF8, true))
                    {
                        // Leftover metadata chunk
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
            }
            catch (Exception ex)
            {
                throw new FurballIOException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Reads a Furball from the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read from. The stream will not be closed by this method; the caller must do this.</param>
        /// <param name="read_fully">Whether or not to read the dependencies and assets of the furball. If false, reads only the metadata.</param>
        private Furball ReadModuleFromStream(Stream stream, bool read_fully)
        {
            var format_version = ReadHeaderFromStream(stream, out var instream);

            Furball furball;

            using (instream)
            {
                furball = new Furball
                {
                    Metadata = new FurballMetadata
                    {
                        ID = new Guid(instream.ReadBytes(16)),
                        Title = instream.ReadString(),
                        Author = instream.ReadString(),
                        FormatVersion = format_version
                    }
                };

                if (read_fully)
                {
                    // Read dependencies list
                    for (int num_dependencies = instream.ReadInt32(); num_dependencies > 0; num_dependencies--)
                    {
                        furball.Dependencies.Add(new FurballDependency
                        {
                            ID = new Guid(instream.ReadBytes(16)),
                            FileNameHint = instream.ReadString()
                        });
                    }

                    // Read asset blobs
                    for (int num_assets = instream.ReadInt32(); num_assets > 0; num_assets--)
                    {
                        var asset = ReadAssetFromStream(instream, furball.Metadata.FormatVersion);
                        asset.Module = furball.Metadata;
                        furball.Assets.Add(asset);
                    }
                }
            }

            return furball;
        }

        private byte ReadHeaderFromStream(Stream instream, out BinaryReader new_stream)
        {
            byte version;

            using (var reader = new BinaryReader(instream, Encoding.UTF8, true))
            {
                // Verify file magic
                char[] header = reader.ReadChars(k_FurballHeader.Length);
                if (!header.SequenceEqual(k_FurballHeader))
                    throw new FurballInvalidHeaderException("Invalid module header");

                // Verify file version
                version = reader.ReadByte();
                if (version < k_MinimumVersion)
                    throw new FurballInvalidHeaderException($"Incompatible module version {version} (minimum version {k_MinimumVersion}, latest version {k_LatestVersion}). The module is from an outdated version of the game; please ask the module author to update it.");
                if (version > k_LatestVersion)
                    throw new FurballInvalidHeaderException($"Incompatible module version {version} (expected version {k_LatestVersion}). The module is from a newer version of the game; please download the latest version of Finmer to play it.");
            }

            // In format versions 21 and onwards, furballs are GZIP compressed
            if (version >= 21)
            {
                new_stream = new GZipBinaryReader(instream, Encoding.UTF8, true);
            }
            else
            {
                new_stream = new BinaryReader(instream, Encoding.UTF8, true);
            }

            return version;
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
