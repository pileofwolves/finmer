﻿/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
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
    /// Command that adds text to the game log.
    /// </summary>
    public sealed class CommandLog : ScriptCommand
    {

        /// <summary>
        /// The message text or string table key.
        /// </summary>
        public string Text { get; set; } = String.Empty;

        /// <summary>
        /// Indicates whether this is a raw text message (i.e. not a string table key).
        /// </summary>
        public bool IsRaw { get; set; }

        public override string GetEditorDescription(IContentStore content)
        {
            return "Show Message: " + Text;
        }

        public override EColor GetEditorColor()
        {
            return EColor.Message;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.Append(IsRaw ? "LogRaw(\"" : "Log(\"");
            output.Append(CoreUtility.EscapeLuaString(Text));
            output.Append("\")");
            output.AppendLine();
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteStringProperty("Text", Text);
            outstream.WriteBooleanProperty("IsRaw", IsRaw);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            Text = instream.ReadStringProperty("Text");
            IsRaw = instream.ReadBooleanProperty("IsRaw");
        }

    }

}
