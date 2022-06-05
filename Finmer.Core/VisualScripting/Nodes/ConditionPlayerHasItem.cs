/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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

        /// <summary>
        /// The name of the item.
        /// </summary>
        public string ItemDisplayName { get; set; } = String.Empty;

        public override string GetEditorDescription()
        {
            return "Player Has Item " + ItemDisplayName;
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
            outstream.WriteStringProperty(nameof(ItemDisplayName), ItemDisplayName);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            ItemGuid = instream.ReadGuidProperty(nameof(ItemGuid));
            ItemDisplayName = instream.ReadStringProperty(nameof(ItemDisplayName));
        }

    }

}
