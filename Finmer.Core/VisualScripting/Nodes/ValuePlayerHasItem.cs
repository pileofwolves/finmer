/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Script value that returns a boolean indicating whether the player has a specified item.
    /// </summary>
    public sealed class ValuePlayerHasItem : ScriptValue
    {

        /// <summary>
        /// The item to check for.
        /// </summary>
        public Guid Item { get; set; } = Guid.Empty;

        /// <summary>
        /// The name of the item.
        /// </summary>
        public string ItemName { get; set; } = String.Empty;

        public override string GetEditorDescription()
        {
            return "Player Has Item " + ItemName;
        }

        public override void EmitLua(StringBuilder output)
        {
            output.Append("Player:HasItem(\"");
            output.Append(ItemName);
            output.Append("\")");
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteGuidProperty("Item", Item);
            outstream.WriteStringProperty("ItemName", ItemName);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Item = instream.ReadGuidProperty("Item");
            ItemName = instream.ReadStringProperty("ItemName");
        }

    }

}
