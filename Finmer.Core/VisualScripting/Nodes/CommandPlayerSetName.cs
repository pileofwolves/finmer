/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Globalization;
using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that assigns to Player.Name.
    /// </summary>
    public sealed class CommandPlayerSetName : ScriptCommand
    {

        /// <summary>
        /// Gets or sets the wrapped parameter for this command.
        /// </summary>
        public ValueWrapperString Value { get; set; } = new ValueWrapperString();

        public override string GetEditorDescription(IContentStore content)
        {
            return String.Format(CultureInfo.InvariantCulture, "Change Player Name to {0}", Value.GetOperandDescription());
        }

        public override EColor GetEditorColor()
        {
            return EColor.Player;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.AppendFormat(CultureInfo.InvariantCulture, "Player.Name = {0}", Value.GetOperandLuaSnippet());
            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            Value.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            Value.Deserialize(instream);
        }

    }

}
