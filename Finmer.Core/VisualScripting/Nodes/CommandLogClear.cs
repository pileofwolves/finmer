/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that wipes all content from the game log.
    /// </summary>
    public sealed class CommandLogClear : ScriptCommand
    {

        public override string GetEditorDescription(IContentStore content)
        {
            return "Clear Message Log";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Message;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.AppendLine("ClearLog()");
        }

    }

}
