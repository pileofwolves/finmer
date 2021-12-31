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
using Finmer.Core;
using Finmer.Core.Assets;
using Finmer.Core.Compilers;
using Finmer.Core.Serialization;
using Finmer.Gameplay.Scripting;

namespace Finmer.Gameplay
{

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

            // Prepare content
            CompileGlobalScripts();
            CompileScenePatches();
            CompileScenes();

            // Touch all save slots, to cache their header information
            SaveManager.CacheSlots();
        }

        /// <summary>
        /// Load asset packages from disk.
        /// </summary>
        private static void LoadModules()
        {
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
                    // Read the module file from disk
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
                    GameController.Content.Add(furball.Assets);
                }

                // Verify that all dependencies are present
                foreach (var dependency in required_dependencies)
                {
                    if (GameController.LoadedModules.All(module => module.ID != dependency.ID))
                        throw new LoaderException($"Missing dependency: {dependency.ID}, possibly named '{dependency.FileNameHint}'");
                }
            }
            catch (DuplicateContentException ex)
            {
                throw new LoaderException("Some modules are incompatible with each other:\r\n" + ex.Message, ex);
            }
            catch (IOException ex)
            {
                throw new LoaderException("A disk read error occurred:\r\n" + ex.Message, ex);
            }
        }

        /// <summary>
        /// Inject all scene patches.
        /// </summary>
        private static void CompileScenePatches()
        {
            try
            {
                // Find all scene patches and inject them
                foreach (AssetScene scene in GameController.Content.GetAssetsByType<AssetScene>())
                    if (scene.Inject)
                        InjectScenePatch(scene);
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
            // Find the target scene
            var target_scene = GameController.Content.GetAssetByID(patch.InjectScene) as AssetScene;
            if (target_scene == null)
                throw new SceneCompilerException(
                    $"Patch '{patch.Name}' in module '{patch.SourceModuleName}' requested injection into target scene with GUID {patch.InjectScene}, but no such scene was found.");

            // Validate that the patch isn't targeting itself, which would likely cause cycles in the scene node graph
            if (target_scene == patch)
                throw new SceneCompilerException(
                    $"Patch '{patch.Name}' in module '{patch.SourceModuleName}' requested injection into itself. This is not supported.");

            // Find the anchor node the patch should be added to
            AssetScene.SceneNode target_node = target_scene.GetNodeByKey(patch.InjectNode);
            if (target_node == null)
                throw new SceneCompilerException(
                    $"Patch '{patch.Name}' in module '{patch.SourceModuleName}' requested injection into target Scene '{target_scene.Name}' at node '{patch.InjectNode}', but no such node was found.");

            // Find the injection point (target parent node) and the index at which to insert our patch
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
                    throw new SceneCompilerException($"Patch '{patch.Name}' in module '{patch.SourceModuleName}' requested invalid injection mode. Module file is likely corrupt.");
            }

            // Insert the patch nodes into the target scene
            foreach (AssetScene.SceneNode patch_node in patch.Root.Children)
                insert_parent.Children.Insert(insert_index++, patch_node);
        }

        /// <summary>
        /// Compiles and caches all global script assets.
        /// </summary>
        private static void CompileGlobalScripts()
        {
            using (var script_compiler = new ScriptCompiler())
            {
                // Find all script assets in content
                var global_scripts = GameController.Content.GetAssetsByType<AssetScript>();
                var item_scripts = GameController.Content.GetAssetsByType<AssetItem>()
                    .Where(item => item.ItemType == AssetItem.EItemType.Usable)
                    .Select(item => item.UseScript);

                // Get the merged collection of scripts
                var all_scripts = global_scripts.Concat(item_scripts);

                // Precompile all of them
                foreach (var script in all_scripts)
                {
                    try
                    {
                        // Compile the script, so we can cache the binary form (which is much faster to load in the future)
                        script.PrecompiledScript = script_compiler.Compile(script.ScriptText, script.Name);

                        // Discard the original script source, to save a little bit of memory
                        script.ScriptText = null;
                    }
                    catch (InvalidDataException ex)
                    {
                        throw new LoaderException($"Failed to compile script '{script.Name}' in module '{script.SourceModuleName}': {ex.Message}", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Compiles and caches all scene assets into Lua scripts.
        /// </summary>
        private static void CompileScenes()
        {
            using (var script_compiler = new ScriptCompiler())
            {
                var all_scenes = GameController.Content.GetAssetsByType<AssetScene>();
                foreach (var scene in all_scenes)
                {
                    try
                    {
                        // Convert the scene graph into a Lua script, and precompile the Lua script as well
                        if (!scene.Inject)
                            SceneCompiler.Compile(script_compiler, scene);

                        // Discard the scene graph since it is no longer needed and will just take up space
                        scene.Root = null;
                        scene.ScriptEnter = null;
                        scene.ScriptLeave = null;
                        scene.ScriptCustom = null;
                    }
                    catch (InvalidDataException ex)
                    {
                        throw new LoaderException($"Failed to compile scene '{scene.Name}' in module '{scene.SourceModuleName}':\r\n\r\n{ex.Message}", ex);
                    }
                    catch (SceneCompilerException ex)
                    {
                        throw new LoaderException($"Failed to build scene '{scene.Name}' in module '{scene.SourceModuleName}':\r\n\r\n{ex.Message}", ex);
                    }
                }
            }
        }

    }

}
