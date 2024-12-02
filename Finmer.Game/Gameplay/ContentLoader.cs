/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
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

            // Cache the headers of all save slots
            SaveManager.RefreshSlots();
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

                    // Record the module metadata (used for crash reports)
                    GameController.LoadedModules.Add(furball.Metadata);

                    // Merge the module with the central asset repository
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
        /// Apply all scene patches.
        /// </summary>
        private static void CompileScenePatches()
        {
            try
            {
                // Find all scene patches and apply them
                foreach (AssetScene scene in GameController.Content.GetAssetsByType<AssetScene>())
                    if (scene.IsPatchGroup)
                        ApplyPatchGroup(scene);
            }
            catch (InvalidScenePatchException ex)
            {
                throw new LoaderException("Error while applying scene patches:\r\n" + ex.Message, ex);
            }
        }

        /// <summary>
        /// Process an individual scene patch group.
        /// </summary>
        /// <param name="patch_group">The patch group to process.</param>
        private static void ApplyPatchGroup(AssetScene patch_group)
        {
            try
            {
                // Game start scenes cannot be patches
                if (patch_group.IsGameStart)
                    throw new InvalidScenePatchException("Patch group is also a game start; this is not allowed. Please either make the scene not a patch, or not a game start.");

                // Find the target scene
                var target_scene = GameController.Content.GetAssetByID(patch_group.PatchTargetScene) as AssetScene;
                if (target_scene == null)
                    throw new InvalidScenePatchException($"Target scene with GUID {patch_group.PatchTargetScene} could not be found.");

                // Validate that the patch isn't targeting itself, which would likely cause cycles in the scene node graph
                if (target_scene == patch_group)
                    throw new InvalidScenePatchException("Target scene is the patch group itself. This is not supported.");

                // We do not allow patching patches because the order in which patches are applied is undefined
                if (target_scene.IsPatchGroup)
                    throw new InvalidScenePatchException($"Target scene '{target_scene.Name}' is a patch group. This is not supported, because the order of operations (which patch applies first) is undefined, and so results would be unpredictable.");

                // Find all patches within this patch group
                foreach (var patch in patch_group.Root.Children)
                {
                    // Ensure that this node is actually a patch, so it includes a PatchData object
                    if (patch.NodeType != SceneNode.ENodeType.Patch)
                        throw new InvalidScenePatchException($"Patch '{patch.Key}' has invalid node type {patch.NodeType}. Module file may be corrupted.");

                    // Apply this patch onto the target scene
                    patch.Patch.Apply(target_scene, patch, GameController.Content);
                }
            }
            catch (InvalidScenePatchException ex)
            {
                // Add context of the patch group onto the exception, then rethrow as nested exception
                throw new InvalidScenePatchException($"Patch group '{patch_group.Name}' in module '{patch_group.Module.Title}' could not be applied: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Compiles and caches all global script assets.
        /// </summary>
        private static void CompileGlobalScripts()
        {
            using (var script_compiler = new ScriptCompiler())
            {
                // Find all script assets in content
                var content = GameController.Content;
                var global_scripts = content.GetAssetsByType<AssetScript>();
                var item_scripts = content.GetAssetsByType<AssetItem>()
                    .Where(item => item.ItemType == AssetItem.EItemType.Usable)
                    .Select(item => item.UseScript);

                // Get the merged collection of scripts
                var all_scripts = global_scripts.Concat(item_scripts);

                // Precompile all of them
                // Note: Load order does not matter here, because globals etc are not resolved until run-time
                foreach (var script in all_scripts)
                {
                    try
                    {
                        // Compile the script, so we can cache the binary form (which is much faster to load in the future)
                        var script_body = script.Contents?.GetScriptText(content) ?? String.Empty;
                        script.PrecompiledScript = script_compiler.Compile(script_body, script.Name);
                    }
                    catch (ScriptCompilationException ex)
                    {
                        throw new LoaderException($"Failed to compile script '{script.Name}' in module '{script.Module.Title}': {ex.Message}", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Compiles and caches all scene assets into Lua scripts.
        /// </summary>
        private static void CompileScenes()
        {
            var content = GameController.Content;

            // There must be at least one game start scene, else no new saves can be created
            if (content.GameStartCount == 0)
                throw new LoaderException("There are no game start locations in any loaded modules.\r\n\r\nIf using total conversion mods, please make sure there are modules that include game start locations.");

            // Precompile all loaded scenes
            using (var script_compiler = new ScriptCompiler())
            {
                var all_scenes = content.GetAssetsByType<AssetScene>();
                foreach (var scene in all_scenes)
                {
                    try
                    {
                        // Convert the scene graph into a Lua script, and precompile the Lua script as well
                        if (!scene.IsPatchGroup)
                            SceneCompiler.Compile(scene, script_compiler, content);

                        // Discard the scene graph since it is no longer needed and will just take up space
                        scene.Root = null;
                        scene.ScriptEnter = null;
                        scene.ScriptLeave = null;
                        scene.ScriptCustom = null;
                    }
                    catch (SceneCompilerException ex)
                    {
                        throw new LoaderException($"Failed to build scene '{scene.Name}' in module '{scene.Module.Title}':\r\n\r\n{ex.Message}", ex);
                    }
                }
            }
        }

    }

}
