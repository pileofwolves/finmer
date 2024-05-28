/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.IO;
using System.Linq;
using System.Text;
using Finmer.Core;
using Finmer.Core.Assets;
using Finmer.Core.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Finmer.Editor
{

    /// <summary>
    /// Implementation of a file device that manages a collection of text files on disk, suitable for version control.
    /// </summary>
    public sealed class FurballFileDeviceText : FurballFileDevice
    {

        public override Furball ReadModule(FileInfo file)
        {
            try
            {
                using (var file_stream = new FileStream(file.FullName, FileMode.Open))
                using (var read_stream = new StreamReader(file_stream, Encoding.UTF8))
                using (var json_stream = new JsonTextReader(read_stream))
                {
                    // Read the project version number
                    var header_reader = FurballContentReaderText.ForProjectMetadata(JObject.Load(json_stream));
                    var project_version = header_reader.GetFormatVersion();

                    // Validate that the version is within supported range
                    if (project_version < k_MinimumVersion)
                        throw new FurballInvalidHeaderException($"Unsupported project version {project_version} (minimum supported is {k_MinimumVersion}). Please use an older version of the Editor to upgrade your project.");
                    if (project_version > k_LatestVersion)
                        throw new FurballInvalidHeaderException($"Unsupported project version {project_version} (latest version is {k_LatestVersion}). Please use a newer version of the Editor to open your project.");

                    // Read basic metadata
                    var output = new Furball
                    {
                        Metadata = new FurballMetadata
                        {
                            ID = header_reader.ReadGuidProperty("ID"),
                            Title = header_reader.ReadStringProperty("Title"),
                            Author = header_reader.ReadStringProperty("Author"),
                            FormatVersion = project_version
                        }
                    };

                    // Read the collection of dependencies
                    for (int num_dependencies = header_reader.BeginArray("Dependencies"); num_dependencies > 0; num_dependencies--)
                    {
                        header_reader.BeginObject();
                        output.Dependencies.Add(new FurballDependency
                        {
                            ID = header_reader.ReadGuidProperty("ID"),
                            FileNameHint = header_reader.ReadStringProperty("FileNameHint")
                        });
                        header_reader.EndObject();
                    }

                    header_reader.EndArray();

                    // Gather a list of all .json files in the same directory as the main module file
                    DirectoryInfo project_dir = file.Directory;
                    if (project_dir == null)
                        throw new FurballInvalidHeaderException("Could not determine project directory for input file");
                    FileInfo[] asset_files = project_dir.GetFiles("*.json", SearchOption.TopDirectoryOnly);

                    // Read all asset files
                    foreach (FileInfo asset_file in asset_files)
                    {
                        var asset = ReadAssetFromFile(asset_file, project_version);
                        asset.Module = output.Metadata;
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
                using (var read_stream = new StreamReader(file_stream, Encoding.UTF8))
                using (var json_stream = new JsonTextReader(read_stream))
                {
                    var header_reader = FurballContentReaderText.ForProjectMetadata(JObject.Load(json_stream));
                    return new FurballMetadata
                    {
                        ID = header_reader.ReadGuidProperty("ID"),
                        Title = header_reader.ReadStringProperty("Title"),
                        Author = header_reader.ReadStringProperty("Author"),
                        FormatVersion = header_reader.GetFormatVersion()
                    };
                }
            }
            catch (IOException ex)
            {
                throw new FurballIOException(ex.Message, ex);
            }
        }

        private AssetBase ReadAssetFromFile(FileInfo file, int version)
        {
            using (var file_stream = new FileStream(file.FullName, FileMode.Open))
            using (var read_stream = new StreamReader(file_stream, Encoding.UTF8))
            using (var json_stream = new JsonTextReader(read_stream))
            {
                // Wrap a filesystem-agnostic reader around the json object
                var content_reader = new FurballContentReaderText(JObject.Load(json_stream), GetProjectDirectory(file), version);

                // Deserialize the asset
                AssetBase asset = AssetSerializer.DeserializeAsset(content_reader) as AssetBase;
                if (asset == null)
                    throw new FurballInvalidAssetException("Could not parse asset in stream");

                return asset;
            }
        }

        public override void WriteModule(Furball furball, FileInfo file)
        {
            try
            {
                var project_dir = file.Directory;
                if (project_dir == null)
                    throw new FurballInvalidHeaderException("Could not determine project directory for input file");

                // Create the directory if it doesn't exist
                if (!project_dir.Exists)
                    project_dir.Create();

                // Delete all .json files in the target folder that do not have matching assets
                var unused_files = project_dir.GetFiles("*.json")
                    .Where(existingFile => furball.GetAssetByName(Path.GetFileNameWithoutExtension(existingFile.Name)) == null);
                foreach (var unused_file in unused_files)
                    unused_file.Delete();

                // Write project metadata
                WriteProjectFile(furball, file);

                // Write all individual asset files
                foreach (var asset in furball.Assets)
                    WriteAssetFile(asset, project_dir);
            }
            catch (IOException ex)
            {
                throw new FurballIOException(ex.Message, ex);
            }
        }

        private void WriteProjectFile(Furball furball, FileInfo projectFile)
        {
            using (var file_stream = new FileStream(projectFile.FullName, FileMode.Create))
            using (var write_stream = new StreamWriter(file_stream, Encoding.UTF8))
            using (var json_stream = new JsonTextWriter(write_stream))
            {
                // Pretty-print the document. We use tabs for indentation (despite that being inconsistent with spaces in code)
                // because it saves quite a lot of disk space in files that are extremely indentation-heavy, like scene trees.
                json_stream.Formatting = Formatting.Indented;
                json_stream.IndentChar = '\t';
                json_stream.Indentation = 1;

                // Wrap a filesystem-agnostic reader around the json object
                var content_writer = new FurballContentWriterText(json_stream, GetProjectDirectory(projectFile));
                content_writer.BeginObject();

                // Write the project summary
                content_writer.WriteInt32Property("FormatVersion", k_LatestVersion);
                content_writer.WriteGuidProperty("ID", furball.Metadata.ID);
                content_writer.WriteStringProperty("Title", furball.Metadata.Title);
                content_writer.WriteStringProperty("Author", furball.Metadata.Author);

                // Write dependency table
                content_writer.BeginArray("Dependencies", furball.Dependencies.Count);
                foreach (var dependency in furball.Dependencies)
                {
                    content_writer.BeginObject();
                    content_writer.WriteGuidProperty("ID", dependency.ID);
                    content_writer.WriteStringProperty("FileNameHint", dependency.FileNameHint);
                    content_writer.EndObject();
                }
                content_writer.EndArray();

                // All done
                content_writer.EndObject();
            }
        }

        private void WriteAssetFile(AssetBase asset, DirectoryInfo projectDir)
        {
            string file_name = Path.ChangeExtension(Path.Combine(projectDir.FullName, asset.Name), ".json");
            using (var file_stream = new FileStream(file_name, FileMode.Create))
            using (var write_stream = new StreamWriter(file_stream, Encoding.UTF8))
            using (var json_stream = new JsonTextWriter(write_stream))
            {
                // Pretty-print the document. We use tabs for indentation (despite that being inconsistent with spaces in code)
                // because it saves quite a lot of disk space in files that are extremely indentation-heavy, like scene trees.
                json_stream.Formatting = Formatting.Indented;
                json_stream.IndentChar = '\t';
                json_stream.Indentation = 1;

                // Wrap a filesystem-agnostic writer around the json object
                var content_writer = new FurballContentWriterText(json_stream, projectDir);
                content_writer.BeginObject();

                // Write the asset data itself
                AssetSerializer.SerializeAsset(content_writer, asset);

                // All done
                content_writer.EndObject();
            }
        }

        private static DirectoryInfo GetProjectDirectory(FileInfo file)
        {
            // Get the project directory, which will be used as search path for attachments
            var project_dir = file.Directory;
            if (project_dir == null)
                throw new FurballInvalidAssetException($"Could not retrieve project directory for '{file.FullName}'");

            return project_dir;
        }

    }

}
