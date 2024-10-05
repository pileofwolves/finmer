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
    /// Patch subtype that removes a specific target node, and its children, from the scene tree.
    /// </summary>
    public sealed class PatchTypeRemoveNode : PatchTypeTargetNodeBase
    {

        /// <inheritdoc />
        public override string GetEditorDescription(IContentStore content)
        {
            return $"Remove Node {TargetNode}";
        }

        /// <inheritdoc />
        public override SceneNode.ENodeFeature GetPatchRootFeatures()
        {
            return SceneNode.ENodeFeature.None;
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

            // Erasing the root of a scene does not make sense, and is hence not supported
            if (target_node.NodeType == SceneNode.ENodeType.Root)
                throw new InvalidScenePatchException("Cannot remove the root node.");

            // Remove this node from its parent
            target_node.Parent.Children.Remove(target_node);
        }

    }

}
