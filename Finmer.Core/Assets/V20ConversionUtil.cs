/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Serialization;
using System;
using System.Diagnostics;
using System.Linq;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Cache of settings used for deserialization of furball format 20 and lower formats.
    /// </summary>
    internal static class V20ConversionUtil
    {

        /// <summary>
        /// Describes a patch injection point, relative to a State node.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "UnusedMember.Local")]
        private enum ELegacyInjectMode : byte
        {
            // Order should match PatchTypeAddNodes.EInjectMode, so that we can simply cast to/from integer to convert to the new enum
            BeforeTarget,
            AfterTarget,
            InsideAtStart,
            InsideAtEnd
        }

        /// <summary>
        /// Cached patch injection mode.
        /// </summary>
        private static ELegacyInjectMode PatchMode { get; set; }

        /// <summary>
        /// Cached target node for the patch.
        /// </summary>
        private static string PatchTargetNode { get; set; }

        /// <summary>
        /// Deserialize and cache V20-format patch settings from a Scene asset.
        /// </summary>
        /// <param name="scene">The scene to read data for.</param>
        /// <param name="instream">The stream to read from.</param>
        public static void ReadV20PatchSettings(AssetScene scene, IFurballContentReader instream)
        {
            scene.IsPatchGroup = instream.ReadBooleanProperty(@"IsPatch");
            if (scene.IsPatchGroup)
            {
                PatchMode = instream.ReadEnumProperty<ELegacyInjectMode>(@"InjectMode");
                scene.PatchTargetScene = instream.ReadGuidProperty(@"InjectTargetScene");
                PatchTargetNode = instream.ReadStringProperty(@"InjectTargetNode");
            }
        }

        /// <summary>
        /// Use cached settings read by ReadV20PatchSettings to convert a V20-format patch scene tree to a Patch node with PatchTypeAddNodes.
        /// </summary>
        /// <param name="scene">The scene to patch.</param>
        public static void WrapV20Patch(AssetScene scene)
        {
            // Sanity checks: we should be upgrading a legacy-format patch node tree that has no Patch nodes in it
            Debug.Assert(PatchTargetNode != null, "ReadV20PatchSettings was not called first");
            Debug.Assert(scene.IsPatchGroup);
            Debug.Assert(scene.Root.NodeType == SceneNode.ENodeType.Root);
            Debug.Assert(scene.Root.Children.All(node => node.NodeType != SceneNode.ENodeType.Patch));

            // Convert the Root node into a Patch of type PatchTypeAddNodes
            // Note that this uses settings configured by ReadV20PatchSettings().
            var patch_root = scene.Root;
            patch_root.Key = String.Empty; // Remove 'Root' key that was the default with V20 scenes
            patch_root.NodeType = SceneNode.ENodeType.Patch;
            patch_root.Patch = new ScenePatchAddNodes
            {
                Mode = (ScenePatchAddNodes.EInjectMode)PatchMode, // Order of elements is the same
                TargetNode = PatchTargetNode
            };

            // Reparent the converted Patch under a new Root
            scene.Root = new SceneNode
            {
                NodeType = SceneNode.ENodeType.Root,
                Children =
                {
                    patch_root
                }
            };

            // Fix up tree hierarchy
            patch_root.Parent = scene.Root;

#if DEBUG
            // Reset state so the assertions at the top of this function are reset
            PatchTargetNode = null;
#endif
        }

    }

}
