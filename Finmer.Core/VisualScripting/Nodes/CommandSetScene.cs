/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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

        /// <summary>
        /// The cached name of the item (for display purposes).
        /// </summary>
        public string SceneName { get; set; } = String.Empty;

        public override string GetEditorDescription()
        {
            return String.Format(CultureInfo.InvariantCulture, "Switch to Scene '{0}'", SceneName);
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
            outstream.WriteStringProperty(nameof(SceneName), SceneName);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            SceneGuid = instream.ReadGuidProperty(nameof(SceneGuid));
            SceneName = instream.ReadStringProperty(nameof(SceneName));
        }

    }

}
