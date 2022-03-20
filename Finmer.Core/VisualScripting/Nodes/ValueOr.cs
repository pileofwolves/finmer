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
    /// Script value transformer that performs a logical OR.
    /// </summary>
    public sealed class ValueOr : ScriptValueDualOperand
    {

        public override string GetEditorDescription()
        {
            string left = Left?.GetEditorDescription() ?? "[ Not configured ]";
            string right = Right?.GetEditorDescription() ?? "[ Not configured ]";
            return $"({left}) Or ({right})";
        }

        public override void EmitLua(StringBuilder output)
        {
            base.EmitLua(output);

            // Emit left-hand operand
            output.Append('(');
            Left.EmitLua(output);

            // Emit operator
            output.Append(") or (");

            // Emit right-hand operand
            Right.EmitLua(output);
            output.Append(')');
        }

    }

}
