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
    /// Patch subtype that replaces a specific target node in-place in the scene tree.
    /// </summary>
    public sealed class PatchTypeReplaceNode : PatchTypeTargetNodeBase
    {

        /// <summary>
        /// Whether to keep existing children (if true), or discard them (if false).
        /// </summary>
        public bool KeepChildren { get; set; } = true;

        /// <inheritdoc />
        public override void Serialize(IFurballContentWriter outstream)
        {
            base.Serialize(outstream);
            outstream.WriteBooleanProperty(nameof(KeepChildren), KeepChildren);
        }

        /// <inheritdoc />
        public override void Deserialize(IFurballContentReader instream)
        {
            base.Deserialize(instream);
            KeepChildren = instream.ReadBooleanProperty(nameof(KeepChildren));
        }

        /// <inheritdoc />
        public override string GetEditorDescription(IContentStore content)
        {
            return $"Replace Node {TargetNode}, {(KeepChildren ? "Keep" : "Discard")} Children";
        }

        /// <inheritdoc />
        public override SceneNode.ENodeFeature GetPatchRootFeatures()
        {
            return SceneNode.ENodeFeature.Children | SceneNode.ENodeFeature.Scripts;
        }

        /// <inheritdoc />
        public override void Apply(AssetScene target_scene, SceneNode patch, IContentStore content)
        {
            Debug.Assert(patch.NodeType == SceneNode.ENodeType.Patch);
            Debug.Assert(patch.PatchData == this);

            // Replacing a node with nothing does not make sense; for this purpose the Remove patch should be used instead
            if (patch.Children.Count == 0)
                throw new InvalidScenePatchException("Replace patch does not have any children to replace the target with. Did you intend to use a Remove Node patch instead?");

            // Find the anchor node the patch should be added to
            SceneNode target_node = target_scene.GetNodeByKey(TargetNode);
            if (target_node == null)
                throw new InvalidScenePatchException($"Target scene '{target_scene.Name}' does not contain a node named '{TargetNode}'.");

            // Editing the root of a scene does not make sense, and is hence not supported
            if (target_node.NodeType == SceneNode.ENodeType.Root)
                throw new InvalidScenePatchException("Cannot replace the root node.");

            // Remove this node from its parent
            var parent_children = target_node.Parent.Children;
            int index_of_target = parent_children.IndexOf(target_node);
            parent_children.RemoveAt(index_of_target);

            // If child nodes of the target node should be kept, insert them onto the first replacement
            if (KeepChildren)
            {
                var first_replacement = patch.Children[0];
                foreach (var target_child in target_node.Children)
                {
                    Debug.Assert(target_child.NodeType != first_replacement.NodeType);
                    first_replacement.Children.Add(target_child);
                }
            }

            // Insert the replacement node at the same index
            foreach (var replacement in patch.Children)
                parent_children.Insert(index_of_target++, replacement);
        }

    }

}
