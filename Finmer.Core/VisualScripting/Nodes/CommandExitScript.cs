﻿/*
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
    /// Command that terminates the running script.
    /// </summary>
    public sealed class CommandExitScript : ScriptCommand
    {

        public override string GetEditorDescription(IContentStore content)
        {
            return "Exit Script";
        }

        public override EColor GetEditorColor()
        {
            return EColor.FlowControl;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // Will likely suffice - doesn't work for nested functions of course, but the visual scripting system
            // doesn't currently allow for creation of those anyway.
            output.AppendLine("return");
        }

    }

}
