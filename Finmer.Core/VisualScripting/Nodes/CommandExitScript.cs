/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Text;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that terminates the running script.
    /// </summary>
    public sealed class CommandExitScript : ScriptCommand
    {

        public override string GetEditorDescription()
        {
            return "Terminate Script";
        }

        public override EColor GetEditorColor()
        {
            return EColor.FlowControl;
        }

        public override void EmitLua(StringBuilder output)
        {
            // Will likely suffice - doesn't work for nested functions of course, but the visual scripting system
            // doesn't currently allow for creation of those anyway.
            output.AppendLine("return");
        }

    }

}
