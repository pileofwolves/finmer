/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Diagnostics;
using Finmer.Core.Compilers;
using Finmer.Core.Serialization;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Patch subtype that adds new nodes to the scene tree.
    /// </summary>
    public sealed class PatchTypeAddNodes : PatchTypeTargetNodeBase
    {

        /// <summary>
        /// Describes how to inject the new nodes in the target node.
        /// </summary>
        public enum EInjectMode : byte
        {
            BeforeTarget,
            AfterTarget,
            InsideTargetFirst,
            InsideTargetLast,
            InsideTargetRandom
        }

        /// <summary>
        /// The method with which to inject patch nodes. Relevant only if IsPatch is set.
        /// </summary>
        public EInjectMode Mode { get; set; } = EInjectMode.AfterTarget;

        /// <inheritdoc />
        public override void Serialize(IFurballContentWriter outstream)
        {
            base.Serialize(outstream);
            outstream.WriteEnumProperty(nameof(Mode), Mode);
        }

        /// <inheritdoc />
        public override void Deserialize(IFurballContentReader instream)
        {
            base.Deserialize(instream);
            Mode = instream.ReadEnumProperty<EInjectMode>(nameof(Mode));
        }

        /// <inheritdoc />
        public override string GetEditorDescription(IContentStore content)
        {
            switch (Mode)
            {
                case EInjectMode.BeforeTarget:              return $"Add Nodes: Before {TargetNode}";
                case EInjectMode.AfterTarget:               return $"Add Nodes: After {TargetNode}";
                case EInjectMode.InsideTargetFirst:         return $"Add Nodes: Inside {TargetNode} at the start";
                case EInjectMode.InsideTargetLast:          return $"Add Nodes: Inside {TargetNode} at the end";
                case EInjectMode.InsideTargetRandom:        return $"Add Nodes: Inside {TargetNode} at random";
                default:                                    throw new FurballInvalidAssetException("Invalid patch add node type");
            }
        }

        /// <inheritdoc />
        public override SceneNode.ENodeFeature GetPatchRootFeatures()
        {
            return SceneNode.ENodeFeature.Children;
        }

        /// <inheritdoc />
        public override void Apply(AssetScene target_scene, SceneNode patch, IContentStore content)
        {
            Debug.Assert(patch.NodeType == SceneNode.ENodeType.Patch);
            Debug.Assert(patch.PatchData == this);

            // Find the anchor node the patch should be added to
            SceneNode target_node = target_scene.GetNodeByKey(TargetNode);
            if (target_node == null)
                throw new InvalidScenePatchException($"Target scene '{target_scene.Name}' does not contain a node named '{TargetNode}'.");

            // Find the injection point (target parent node) and the index at which to insert our patch
            SceneNode insert_parent;
            int insert_index;
            switch (Mode)
            {
                case EInjectMode.BeforeTarget:
                    insert_parent = target_node.Parent;
                    insert_index = insert_parent.Children.IndexOf(target_node);
                    break;
                case EInjectMode.AfterTarget:
                    insert_parent = target_node.Parent;
                    insert_index = insert_parent.Children.IndexOf(target_node) + 1;
                    break;
                case EInjectMode.InsideTargetFirst:
                    insert_parent = target_node;
                    insert_index = 0;
                    break;
                case EInjectMode.InsideTargetLast:
                    insert_parent = target_node;
                    insert_index = insert_parent.Children.Count;
                    break;
                case EInjectMode.InsideTargetRandom:
                    insert_parent = target_node;
                    insert_index = CoreUtility.Rng.Next(insert_parent.Children.Count + 1);
                    break;
                default:
                    throw new InvalidScenePatchException($"Unknown patch mode {Mode}. Module file is likely corrupted.");
            }

            // Insert the patch nodes into the target scene
            foreach (SceneNode patch_node in patch.Children)
            {
                // Validate that the new node fits onto the parent
                if (insert_parent.NodeType == patch_node.NodeType)
                    throw new InvalidScenePatchException($"Patch '{patch.Key}' contains new node '{patch_node.Key}' which is the same type as the target node '{insert_parent.Key}' ({patch_node.NodeType}). Module file is likely corrupted.");

                // Inject the new node
                insert_parent.Children.Insert(insert_index++, patch_node);
            }
        }

    }

}
