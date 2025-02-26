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
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that pauses the script for a number of seconds.
    /// </summary>
    public sealed class CommandSleep : ScriptNode
    {

        /// <summary>
        /// The number of seconds to sleep.
        /// </summary>
        public ValueWrapperFloat Seconds { get; set; } = new ValueWrapperFloat();

        public override string GetEditorDescription(IContentStore content)
        {
            return String.Format(CultureInfo.InvariantCulture, "Wait {0} seconds", Seconds.GetOperandDescription());
        }

        public override EColor GetEditorColor()
        {
            return EColor.Sleep;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.AppendFormat(CultureInfo.InvariantCulture, "Sleep({0})", Seconds.GetOperandLuaSnippet());
            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            Seconds.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            Seconds.Deserialize(instream);
        }

    }

}
