/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Text;
using Finmer.Core.Assets;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Script value that returns a boolean indicating whether the player has a specified item.
    /// </summary>
    public sealed class ConditionPlayerHasItem : ScriptCondition
    {

        /// <summary>
        /// The item to check for.
        /// </summary>
        public Guid ItemGuid { get; set; } = Guid.Empty;

        public override string GetEditorDescription(IContentStore content)
        {
            return "Player Has Item " + content.GetAssetName(ItemGuid);
        }

        public override EColor GetEditorColor()
        {
            return EColor.Player;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            var item = content.GetAssetByID<AssetItem>(ItemGuid);
            if (item == null)
                throw new InvalidScriptNodeException($"Could not find an Item asset with ID {ItemGuid}");

            output.Append("Player:HasItem(\"");
            output.Append(item.Name);
            output.Append("\")");
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteGuidProperty(nameof(ItemGuid), ItemGuid);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            ItemGuid = instream.ReadGuidProperty(nameof(ItemGuid));
        }

    }

}
