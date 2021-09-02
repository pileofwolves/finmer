/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Finmer.Core;
using Finmer.Core.Assets;
using Finmer.Core.Compilers;
using Finmer.Core.Serialization;
using Finmer.Gameplay.Scripting;
using Finmer.Utility;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents an exception thrown when loading of game content fails.
    /// </summary>
    [Serializable]
    public sealed class LoaderException : ApplicationException
    {

        private LoaderException(SerializationInfo info, StreamingContext context) : base(info, context) {}

        public LoaderException(string message) : base(message) {}

        public LoaderException(string message, Exception inner) : base(message, inner) {}

    }

    /// <summary>
    /// Represents a utility for loading and preparing all game content.
    /// </summary>
    public static class ContentLoader
    {

        /// <summary>
        /// Load all content.
        /// </summary>
        public static void LoadAll()
        {
            // Load modules from disk
            LoadModules();

            // Preload all save slots, to cache their header information
            SaveManager.CacheSlots();

            // Prepare content
            LoadAllStrings();
            CompileGlobalScripts();
            CompileScenePatches();
            CompileScenes();
        }

        /// <summary>
        /// Load asset packages from disk.
        /// </summary>
        private static void LoadModules()
        {
            GameController.Content = new Furball();

            try
            {
                // Open the Modules folder
                var dir = new DirectoryInfo("Modules");
                if (!dir.Exists)
                    throw new LoaderException("The Modules folder is missing from the game installation. Did you forget to extract all the files? Try reinstalling the game.");

                // Find all module files in the Modules folder
                FileInfo[] files = dir.GetFiles("*.furball");
                if (files.Length == 0)
                    throw new LoaderException("No game modules (furballs) were found. Did you forget to extract all the files? Try reinstalling the game.");

                List<FurballDependency> required_dependencies = new List<FurballDependency>();
                FurballFileDevice device = new FurballFileDeviceBinary();

                // Load all module files
                foreach (FileInfo file in files)
                {
                    Furball furball;
                    try
                    {
                        furball = device.ReadModule(file);
                    }
                    catch (Exception ex)
                    {
                        throw new LoaderException($"Failed to load module '{file.Name}': {ex.Message}", ex);
                    }

                    // IDs should be unique
                    var duplicate = GameController.LoadedModules.FirstOrDefault(loaded => loaded.ID == furball.Metadata.ID);
                    if (duplicate.ID == furball.Metadata.ID)
                        throw new LoaderException($"There are two modules using the same ID {furball.Metadata.ID}: '{furball.Metadata.Title}' by '{furball.Metadata.Author}', and '{duplicate.Title}' by '{duplicate.Author}'");

                    // Collect the dependencies of this module
                    required_dependencies.AddRange(furball.Dependencies);

                    // Record the furball's metadata (used for crash reports)
                    GameController.LoadedModules.Add(furball.Metadata);

                    // Merge the furball with the central asset repository
                    GameController.Content.Merge(furball);
                }

                // Verify that all dependencies are present
                foreach (var dependency in required_dependencies)
                {
                    if (GameController.LoadedModules.All(module => module.ID != dependency.ID))
                        throw new LoaderException($"Missing dependency: {dependency.ID}, possibly named '{dependency.FileNameHint}'");
                }
            }
            catch (IOException ex)
            {
                throw new LoaderException("A disk read error occurred: " + ex.Message, ex);
            }
        }

        private static void LoadAllStrings()
        {
            // Merge all strings from all content packages into a central table
            GameController.Content.Assets
                .OfType<AssetStringTable>()
                .Select(asset => asset.Table)
                .ForEach(table =>
                {
                    // Merge into the central table
                    GameController.MergedStrings.Merge(table);

                    // Get rid of the contents of the old table, to avoid duplicate memory usage
                    table.Clear();
                });
        }

        /// <summary>
        /// Inject all scene patches.
        /// </summary>
        private static void CompileScenePatches()
        {
            try
            {
                GameController.Content.Assets
                    .OfType<AssetScene>()
                    .Where(scene => scene.Inject)
                    .ForEach(InjectScenePatch);
            }
            catch (SceneCompilerException ex)
            {
                throw new LoaderException("Error while applying scene patches:\r\n" + ex.Message, ex);
            }
        }

        /// <summary>
        /// Process an individual scene patch.
        /// </summary>
        /// <param name="patch">The patch to process.</param>
        private static void InjectScenePatch(AssetScene patch)
        {
            // find the target scene
            var target_scene = GameController.Content.GetAssetByID(patch.InjectScene) as AssetScene;
            if (target_scene == null)
                throw new SceneCompilerException(
                    $"Scene '{patch.Name}' requested injection into target scene with GUID {patch.InjectScene}, but no such scene was found.");

            AssetScene.SceneNode target_node = target_scene.GetNodeByKey(patch.InjectNode);
            if (target_node == null)
                throw new SceneCompilerException(
                    $"Scene '{patch.Name}' requested injection into target Scene '{target_scene.Name}' at node '{patch.InjectNode}', but no such node was found.");

            // find the injection point (target parent node) and the index at which to insert our patch
            AssetScene.SceneNode insert_parent;
            int insert_index;
            switch (patch.InjectMode)
            {
                case AssetScene.ESceneInjectMode.BeforeTarget:
                    insert_parent = target_node.Parent;
                    insert_index = insert_parent.Children.IndexOf(target_node);
                    break;
                case AssetScene.ESceneInjectMode.AfterTarget:
                    insert_parent = target_node.Parent;
                    insert_index = insert_parent.Children.IndexOf(target_node) + 1;
                    break;
                case AssetScene.ESceneInjectMode.InsideAtStart:
                    insert_parent = target_node;
                    insert_index = 0;
                    break;
                case AssetScene.ESceneInjectMode.InsideAtEnd:
                    insert_parent = target_node;
                    insert_index = insert_parent.Children.Count;
                    break;
                default:
                    throw new SceneCompilerException($"Scene '{patch.Name}' requested invalid injection mode. Corrupt file or editor bug maybe?");
            }

            // insert the patch nodes now
            foreach (AssetScene.SceneNode patch_node in patch.Root.Children)
                insert_parent.Children.Insert(insert_index++, patch_node);

            // remove the scene from the main repository, so it can't be travelled to
            GameController.Content.Assets.Remove(patch);
        }

        /// <summary>
        /// Compiles and runs all global script assets.
        /// </summary>
        private static void CompileGlobalScripts()
        {
            using (var script_compiler = new ScriptCompiler())
            {
                // Get all global scripts, and all item use scripts
                var scripts = GameController.Content.Assets
                    .OfType<AssetScript>()
                    .Concat(GameController.Content.Assets
                        .OfType<AssetItem>()
                        .Where(item => item.ItemType == AssetItem.EItemType.Usable)
                        .Select(item => item.UseScript));

                // Precompile all of them
                foreach (var script in scripts)
                {
                    try
                    {
                        // Compile the script, so we can cache the binary form (which is much faster to load in the future)
                        script.PrecompiledScript = script_compiler.Compile(script.ScriptText, script.Name);

                        // Discard the original script source, to save a little bit of memory
                        script.ScriptText = null;
                    }
                    catch (Exception ex)
                    {
                        throw new LoaderException($"Failed to compile script '{script.Name}': {ex.Message}", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Compiles and caches all scene assets into Lua scripts.
        /// </summary>
        private static void CompileScenes()
        {
            // build Lua scripts from all scenes
            try
            {
                using (var script_compiler = new ScriptCompiler())
                {
                    GameController.Content.Assets
                        .OfType<AssetScene>()
                        .ForEach(asset =>
                        {
                            // convert the scene graph into a script
                            SceneCompiler.Compile(script_compiler, asset);

                            // discard the scene graph as we no longer need it
                            asset.Root = null;
                            asset.ScriptEnter = null;
                            asset.ScriptLeave = null;
                            asset.ScriptCustom = null;
                        });
                }
            }
            catch (InvalidDataException ex)
            {
                throw new LoaderException("Script compiler error: " + ex.Message, ex);
            }
            catch (SceneCompilerException ex)
            {
                throw new LoaderException("Scene compiler error: " + ex.Message, ex);
            }
        }

    }

}
