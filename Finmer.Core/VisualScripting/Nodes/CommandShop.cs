/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Finmer.Core.Assets;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that encapsulates the shop system.
    /// </summary>
    public sealed class CommandShop : ScriptCommand
    {

        /// <summary>
        /// Unique key that identifies this shop in save data. Wraps ShopState.Key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Wraps ShopState.Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Wraps ShopState.RestockInterval.
        /// </summary>
        public int RestockInterval { get; set; } = 12;

        /// <summary>
        /// Default shop merchandise, expressed as a mapping of AssetItem GUID to quantity number.
        /// </summary>
        public Dictionary<Guid, int> Merchandise { get; set; } = new Dictionary<Guid, int>();

        public override string GetEditorDescription(IContentStore content)
        {
            return String.Format(CultureInfo.InvariantCulture, "Open Shop '{0}'", Key);
        }

        public override EColor GetEditorColor()
        {
            return EColor.SceneControl;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // Start enclosing scope, so we can define local variables
            output.AppendFormat(CultureInfo.InvariantCulture, "do local _shop = Shop(\"{0}\")", CoreUtility.EscapeLuaString(Key));
            output.AppendLine();

            // Basic configuration
            output.AppendFormat(CultureInfo.InvariantCulture, "_shop.Title = \"{0}\"", CoreUtility.EscapeLuaString(Title));
            output.AppendLine();
            output.AppendFormat(CultureInfo.InvariantCulture, "_shop.RestockInterval = {0}", RestockInterval);
            output.AppendLine();

            // Restock boilerplate
            output.AppendLine("if _shop.RestockRequired then");
            output.AppendLine("_shop:RemoveDefaultStock()");
            output.AppendLine("_shop:MarkRestocked()");

            // Merchandise
            foreach (var pair in Merchandise)
            {
                // Find the asset for this item
                var item = content.GetAssetByID<AssetItem>(pair.Key);
                if (item == null)
                    throw new InvalidScriptNodeException($"Could not find an Item asset with ID {pair.Key}");

                // Add it to the shop
                output.AppendFormat(CultureInfo.InvariantCulture, "_shop:AddItem(Item(\"{0}\"), {1})", item.Name, pair.Value == 0 ? -1 : pair.Value);
                output.AppendLine();
            }

            // Close restock boilerplate
            output.AppendLine("end");

            // Wrap-up boilerplate
            output.AppendLine("_shop:Show() _shop:Save() end");
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            // Basic configuration
            outstream.WriteStringProperty(nameof(Key), Key);
            outstream.WriteStringProperty(nameof(Title), Title);
            outstream.WriteInt32Property(nameof(RestockInterval), RestockInterval);

            // Merchandise list
            outstream.BeginArray(nameof(Merchandise), Merchandise.Count);
            foreach (var stock in Merchandise)
            {
                outstream.BeginObject();
                outstream.WriteGuidProperty(@"Item", stock.Key);
                outstream.WriteInt32Property(@"Quantity", stock.Value);
                outstream.EndObject();
            }
            outstream.EndArray();
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            // Basic configuration
            Key = instream.ReadStringProperty(nameof(Key));
            Title = instream.ReadStringProperty(nameof(Title));
            RestockInterval = instream.ReadInt32Property(nameof(RestockInterval));

            // Merchandise list
            Merchandise.Clear();
            for (int i = 0, c = instream.BeginArray(nameof(Merchandise)); i < c; i++)
            {
                instream.BeginObject();
                Merchandise.Add(instream.ReadGuidProperty(@"Item"), instream.ReadInt32Property(@"Quantity"));
                instream.EndObject();
            }
            instream.EndArray();
        }

    }

}
