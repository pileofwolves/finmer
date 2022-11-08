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
    /// Command that modifies the player's inventory.
    /// </summary>
    public sealed class CommandPlayerSetItem : ScriptCommand
    {

        /// <summary>
        /// The inventory item prototype.
        /// </summary>
        public Guid ItemGuid { get; set; } = Guid.Empty;

        /// <summary>
        /// The cached name of the item (for display purposes).
        /// </summary>
        public string ItemName { get; set; } = String.Empty;

        /// <summary>
        /// Whether to add the item (true) or remove it (false).
        /// </summary>
        public bool Add { get; set; } = true;

        /// <summary>
        /// Whether to suppress the item addition message.
        /// </summary>
        public bool Quiet { get; set; } = false;

        public override string GetEditorDescription()
        {
            return Add
                ? String.Format(CultureInfo.InvariantCulture, "Add {0} to Inventory", ItemName)
                : String.Format(CultureInfo.InvariantCulture, "Remove {0} from Inventory", ItemName);
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

            if (Add)
                output.AppendFormat(CultureInfo.InvariantCulture, "Player:GiveItem(\"{0}\", {1})", item.Name, Quiet ? "true" : "false");
            else
                output.AppendFormat(CultureInfo.InvariantCulture, "Player:TakeItem(\"{0}\")", item.Name);

            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteGuidProperty(nameof(ItemGuid), ItemGuid);
            outstream.WriteStringProperty(nameof(ItemName), ItemName);
            outstream.WriteBooleanProperty(nameof(Add), Add);

            if (Add)
                outstream.WriteBooleanProperty(nameof(Quiet), Quiet);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            ItemGuid = instream.ReadGuidProperty(nameof(ItemGuid));
            ItemName = instream.ReadStringProperty(nameof(ItemName));
            Add = instream.ReadBooleanProperty(nameof(Add));

            if (Add)
                Quiet = instream.ReadBooleanProperty(nameof(Quiet));
        }

    }

}
