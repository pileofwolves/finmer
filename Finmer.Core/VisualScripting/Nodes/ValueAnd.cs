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
    /// Script value transformer that performs a logical AND.
    /// </summary>
    public sealed class ValueAnd : ScriptValueDualOperand
    {

        public override string GetEditorDescription()
        {
            string left = Left?.GetEditorDescription() ?? "[ Not configured ]";
            string right = Right?.GetEditorDescription() ?? "[ Not configured ]";
            return $"({left}) And ({right})";
        }

        public override void EmitLua(StringBuilder output)
        {
            base.EmitLua(output);

            // Emit left-hand operand
            output.Append('(');
            Left.EmitLua(output);

            // Emit operator
            output.Append(") and (");

            // Emit right-hand operand
            Right.EmitLua(output);
            output.Append(')');
        }

    }

}
