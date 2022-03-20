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

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that pauses the script for a number of seconds.
    /// </summary>
    public sealed class CommandSleep : ScriptCommandSingleFloat
    {

        public override string GetEditorDescription()
        {
            return String.Format(CultureInfo.InvariantCulture, "Sleep {0:F1} sec", Value);
        }

        public override EColor GetEditorColor()
        {
            return EColor.FlowControl;
        }

        public override void EmitLua(StringBuilder output)
        {
            output.AppendFormat(CultureInfo.InvariantCulture, "Sleep({0:F5})", Value);
            output.AppendLine();
        }

        public override string GetEditorWindowTitle()
        {
            return "Sleep";
        }

    }

}
