/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Serialization;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Represents a configuration for the application of a scene patch.
    /// </summary>
    public abstract class ScenePatch : IFurballSerializable
    {

        /// <inheritdoc />
        public abstract void Serialize(IFurballContentWriter outstream);

        /// <inheritdoc />
        public abstract void Deserialize(IFurballContentReader instream);

        /// <summary>
        /// Returns a human-readable description of this patch configuration, for display in the Editor.
        /// </summary>
        /// <param name="content">Content repository to use for resolving links.</param>
        public abstract string GetEditorDescription(IContentStore content);

        /// <summary>
        /// Returns the set of features required on the Patch node represented by the PatchType.
        /// </summary>
        public abstract SceneNode.ENodeFeature GetPatchRootFeatures();

        /// <summary>
        /// Apply this patch onto a scene.
        /// </summary>
        /// <param name="target_scene">The scene to apply the patch on.</param>
        /// <param name="patch">The root node of the patch. Must be the node whose PatchData is this object.</param>
        /// <param name="content">Content repository to use for resolving links.</param>
        public abstract void Apply(AssetScene target_scene, SceneNode patch, IContentStore content);

    }

}
