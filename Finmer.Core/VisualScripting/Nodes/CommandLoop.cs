/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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
    /// Command that loops a body of commands.
    /// </summary>
    public sealed class CommandLoop : ScriptCommandContainer
    {

        public override string GetEditorDescription()
        {
            return "Loop:";
        }

        public override EColor GetEditorColor()
        {
            return EColor.FlowControl;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // Emit loop header
            output.AppendLine("while true do");

            // Emit loop body
            foreach (var node in Subgroup1)
                node.EmitLua(output, content);

            // Emit end
            output.AppendLine("end");
        }

        public override string GetEditorSubgroup1Suffix()
        {
            return "Repeat Above";
        }

        public override string GetEditorSubgroup2Suffix()
        {
            throw new NotSupportedException();
        }

        public override bool IsSubgroup2Enabled()
        {
            // The loop command only ever has one loop body
            return false;
        }

    }

}
