﻿/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using Finmer.Core.Serialization;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Represents a node-driven interactive dialogue.
    /// </summary>
    public class AssetScene : AssetBase
    {

        /// <summary>
        /// The root node of the scene node tree.
        /// </summary>
        public SceneNode Root { get; set; }

        /// <summary>
        /// A custom script header that will be inserted at the top of the generated scene script.
        /// </summary>
        public ScriptData ScriptCustom { get; set; }

        /// <summary>
        /// Script that will run when the player enters this scene.
        /// </summary>
        public ScriptData ScriptEnter { get; set; }

        /// <summary>
        /// Script that will run when the player leaves this scene, before entering another scene.
        /// </summary>
        public ScriptData ScriptLeave { get; set; }

        /// <summary>
        /// Precompiled script that represents the entire scene (all nodes and nested scripts), or null if unavailable.
        /// </summary>
        public CompiledScript PrecompiledScript { get; set; }

        /// <summary>
        /// Indicates whether this scene can be selected by the player as game start, when creating new save data.
        /// </summary>
        public bool IsGameStart { get; set; }

        /// <summary>
        /// Indicates whether this scene is a patch group, meaning it contains patches that modify a target scene, such as by adding or removing nodes.
        /// </summary>
        public bool IsPatchGroup { get; set; }

        /// <summary>
        /// The Asset ID of the target scene to inject a patch into. Relevant only if IsPatchGroup is set.
        /// </summary>
        public Guid PatchTargetScene { get; set; } = Guid.Empty;

        /// <summary>
        /// User-friendly description of the game start. Relevant only if IsGameStart is set.
        /// </summary>
        public string GameStartDescription { get; set; } = String.Empty;

        public override void Serialize(IFurballContentWriter outstream)
        {
            base.Serialize(outstream);

            // Custom scene scripts
            outstream.WriteScriptProperty(nameof(ScriptCustom), ScriptCustom);
            outstream.WriteScriptProperty(nameof(ScriptEnter), ScriptEnter);
            outstream.WriteScriptProperty(nameof(ScriptLeave), ScriptLeave);

            // Game start settings
            outstream.WriteBooleanProperty(nameof(IsGameStart), IsGameStart);
            if (IsGameStart)
                outstream.WriteStringProperty(nameof(GameStartDescription), GameStartDescription);

            // Patching settings
            outstream.WriteBooleanProperty(nameof(IsPatchGroup), IsPatchGroup);
            if (IsPatchGroup)
                outstream.WriteGuidProperty(nameof(PatchTargetScene), PatchTargetScene);

            // Scene node hierarchy
            outstream.BeginObject(nameof(Root));
            Root.Serialize(outstream);
            outstream.EndObject();
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            base.Deserialize(instream);

            // Read scene scripts
            ScriptCustom = instream.ReadObjectProperty<ScriptData>(nameof(ScriptCustom), EFurballObjectMode.Optional);
            ScriptEnter = instream.ReadObjectProperty<ScriptData>(nameof(ScriptEnter), EFurballObjectMode.Optional);
            ScriptLeave = instream.ReadObjectProperty<ScriptData>(nameof(ScriptLeave), EFurballObjectMode.Optional);

            // Game start settings
            if (instream.GetFormatVersion() >= 20)
            {
                IsGameStart = instream.ReadBooleanProperty(nameof(IsGameStart));
                if (IsGameStart)
                    GameStartDescription = instream.ReadStringProperty(nameof(GameStartDescription));
            }

            // Assign script names
            if (ScriptCustom != null)
                ScriptCustom.Name = Name + "_Custom";
            if (ScriptEnter != null)
                ScriptEnter.Name = Name + "_Enter";
            if (ScriptLeave != null)
                ScriptLeave.Name = Name + "_Leave";

            // Read modding/injection settings
            if (instream.GetFormatVersion() >= 21)
            {
                IsPatchGroup = instream.ReadBooleanProperty(nameof(IsPatchGroup));
                if (IsPatchGroup)
                    PatchTargetScene = instream.ReadGuidProperty(nameof(PatchTargetScene));
            }
            else
            {
                // Because patch configuration has moved to a different object, read the old header out-of-line and store it for later use
                V20ConversionUtil.ReadV20PatchSettings(this, instream);
            }

            // Read the scene node tree recursively
            instream.BeginObject("Root");
            Root = new SceneNode();
            Root.Deserialize(instream);
            instream.EndObject();

            // Upgrade legacy patches to the new V21 format
            if (IsPatchGroup && instream.GetFormatVersion() < 21)
                V20ConversionUtil.WrapV20Patch(this);
        }

        /// <summary>
        /// Finds the node in the scene graph with the specified key.
        /// </summary>
        /// <param name="key">The unique key to find.</param>
        public SceneNode GetNodeByKey(string key)
        {
            // As a special exception, empty keys are valid but anonymous by definition
            if (String.IsNullOrWhiteSpace(key))
                return null;

            // Prepare a stack with the root elements in it
            Stack<SceneNode> stack = new Stack<SceneNode>();
            foreach (var root_child in Root.Children)
                stack.Push(root_child);

            // Flood-fill down the tree until the requested node is found
            while (stack.Count > 0)
            {
                SceneNode node = stack.Pop();

                // Some types of nodes do not have keys, and as such cannot be found with this function
                if (!node.Features.HasFlag(SceneNode.ENodeFeature.Key))
                    continue;

                // Is this the node we're looking for?
                if (node.Key.Equals(key))
                    return node;

                // Recursively examine the node's children
                foreach (var child in node.Children)
                    stack.Push(child);
            }

            return null;
        }

    }

}
