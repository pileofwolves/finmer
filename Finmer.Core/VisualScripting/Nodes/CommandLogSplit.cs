/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that adds a horizontal splitter to the game log.
    /// </summary>
    public sealed class CommandLogSplit : ScriptCommand
    {

        public override string GetEditorDescription(IContentStore content)
        {
            return "Show Horizontal Bar";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Message;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.AppendLine("LogSplit()");
        }

    }

}
