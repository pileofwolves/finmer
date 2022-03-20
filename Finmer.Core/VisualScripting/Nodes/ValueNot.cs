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
    /// Script value transformer that performs a boolean NOT.
    /// </summary>
    public sealed class ValueNot : ScriptValueSingleOperand
    {

        public override string GetEditorDescription()
        {
            return $"Not {Operand.GetEditorDescription()}";
        }

        public override void EmitLua(StringBuilder output)
        {
            output.Append("not (");
            Operand.EmitLua(output);
            output.Append(')');
        }

    }

}
