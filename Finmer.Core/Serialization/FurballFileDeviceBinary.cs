/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.IO;
using System.IO.Compression;
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

        /// <inheritdoc />
        public override Furball ReadModule(FileInfo file)
        {
            try
            {
                using (var file_stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                {
                    // Read the file header and version from the raw file stream
                    uint version = ReadHeaderVersionFromStream(file_stream);

                    // Then wrap the rest of the file stream in a reader that may be decompressing, based on file version
                    using (var instream = CreateModuleReader(file_stream, version))
                    {
                        // Read the rest of the metadata from the file header
                        var furball = new Furball
                        {
                            Metadata = ReadMetadataFromStream(instream, version)
                        };

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

                        return furball;
                    }
                }
            }
            catch (IOException ex)
            {
                throw new FurballIOException(ex.Message, ex);
            }
        }

        /// <inheritdoc />
        public override FurballMetadata ReadMetadata(FileInfo file)
        {
            try
            {
                using (var file_stream = new FileStream(file.FullName, FileMode.Open))
                {
                    uint version = ReadHeaderVersionFromStream(file_stream);

                    using (var instream = CreateModuleReader(file_stream, version))
                        return ReadMetadataFromStream(instream, version);
                }
            }
            catch (IOException ex)
            {
                throw new FurballIOException(ex.Message, ex);
            }
        }

        /// <inheritdoc />
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

                    // Module contents are GZIP-compressed
                    using (var outstream = new BinaryWriter(new GZipStream(stream, CompressionMode.Compress, true), Encoding.UTF8, false))
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
        /// Validates that the input stream represents a Furball, and reads its format version number.
        /// </summary>
        /// <exception cref="FurballInvalidHeaderException">Throws if the stream does not represent a Furball, or if the version is incompatible.</exception>
        private static uint ReadHeaderVersionFromStream(Stream instream)
        {
            using (var reader = new BinaryReader(instream, Encoding.UTF8, true))
            {
                // Verify file magic
                char[] header = reader.ReadChars(k_FurballHeader.Length);
                if (!header.SequenceEqual(k_FurballHeader))
                    throw new FurballInvalidHeaderException("Invalid module header");

                // Verify file version
                byte version = reader.ReadByte();
                if (version < k_MinimumVersion)
                    throw new FurballInvalidHeaderException($"Incompatible module version {version} (minimum version {k_MinimumVersion}, latest version {k_LatestVersion}). The module is from an outdated version of the game; please ask the module author to update it.");
                if (version > k_LatestVersion)
                    throw new FurballInvalidHeaderException($"Incompatible module version {version} (expected version {k_LatestVersion}). The module is from a newer version of the game; please download the latest version of Finmer to play it.");

                return version;
            }
        }

        /// <summary>
        /// Reads module metadata from the input stream.
        /// </summary>
        /// <param name="instream">The stream to read from.</param>
        /// <param name="version">The format version number that was read from the file header.</param>
        private static FurballMetadata ReadMetadataFromStream(BinaryReader instream, uint version)
        {
            return new FurballMetadata
            {
                ID = new Guid(instream.ReadBytes(16)),
                Title = instream.ReadString(),
                Author = instream.ReadString(),
                FormatVersion = version
            };
        }

        /// <summary>
        /// Reads an asset from the input stream.
        /// </summary>
        /// <param name="instream">The stream to read from.</param>
        /// <param name="version">The format version number that was read from the file header.</param>
        /// <returns>A deserialized asset.</returns>
        /// <exception cref="FurballInvalidAssetException">Throws if the asset cannot be deserialized.</exception>
        /// <exception cref="FurballException">Throws if an I/O error occurs.</exception>
        /// <exception cref="IOException">Throws if an I/O error occurs.</exception>
        private static AssetBase ReadAssetFromStream(BinaryReader instream, uint version)
        {
            // Deserialize an asset of the appropriate type
            IFurballContentReader content_reader = new FurballContentReaderBinary(instream, version);
            AssetBase asset = AssetSerializer.DeserializeAsset(content_reader) as AssetBase;
            if (asset == null)
                throw new FurballInvalidAssetException("Could not parse asset in stream");

            return asset;
        }

        /// <summary>
        /// Returns a BinaryReader suitable for the target format version, that wraps the specified base stream.
        /// </summary>
        private static BinaryReader CreateModuleReader(Stream base_stream, uint version)
        {
            // From format versions 21 onward, modules are GZIP compressed
            return version >= 21
                ? new BinaryReader(new GZipStream(base_stream, CompressionMode.Decompress, true), Encoding.UTF8, false)
                : new BinaryReader(base_stream, Encoding.UTF8, true);
        }

    }

}
