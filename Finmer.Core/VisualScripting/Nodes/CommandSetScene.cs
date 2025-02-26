/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Globalization;
using System.Text;
using Finmer.Core.Assets;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that wraps SetScene().
    /// </summary>
    public sealed class CommandSetScene : ScriptCommand
    {

        /// <summary>
        /// The target scene to load.
        /// </summary>
        public Guid SceneGuid { get; set; } = Guid.Empty;

        public override string GetEditorDescription(IContentStore content)
        {
            return $"Change Scene to {content.GetAssetName(SceneGuid)}";
        }

        public override EColor GetEditorColor()
        {
            return EColor.SceneControl;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            var scene = content.GetAssetByID<AssetScene>(SceneGuid);
            if (scene == null)
                throw new InvalidScriptNodeException($"Could not find a Scene asset with ID {SceneGuid}");

            output.AppendFormat(CultureInfo.InvariantCulture, "SetScene(\"{0}\")", scene.Name);
            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteGuidProperty(nameof(SceneGuid), SceneGuid);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            SceneGuid = instream.ReadGuidProperty(nameof(SceneGuid));
        }

    }

}
