/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
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
    /// Command that advances the world clock.
    /// </summary>
    public sealed class CommandTimeSetHour : ScriptNode
    {

        /// <summary>
        /// The number of hours to advance the clock by.
        /// </summary>
        public ValueWrapperInt Hour { get; set; } = new ValueWrapperInt();

        public override string GetEditorDescription(IContentStore content)
        {
            return String.Format(CultureInfo.InvariantCulture, "Set Current Hour to {0}", Hour.GetOperandDescription());
        }

        public override EColor GetEditorColor()
        {
            return EColor.Time;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.AppendFormat(CultureInfo.InvariantCulture, "SetTimeHour({0})", Hour.GetOperandLuaSnippet());
            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            Hour.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Hour.Deserialize(instream, version);
        }

    }

}
