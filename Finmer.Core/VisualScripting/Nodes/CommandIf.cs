/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that conditionally executes a nested body of commands.
    /// </summary>
    public sealed class CommandIf : ScriptCommandContainer
    {

        /// <summary>
        /// The condition that controls whether the block is executed.
        /// </summary>
        public ScriptValue Condition { get; set; }

        /// <summary>
        /// Whether this conditional branch has an else branch.
        /// </summary>
        public bool HasElseBranch { get; set; } = false;

        public override string GetEditorDescription()
        {
            return $"If {Condition?.GetEditorDescription() ?? "[ Not configured ]"}:";
        }

        public override EColor GetEditorColor()
        {
            return EColor.FlowControl;
        }

        public override void EmitLua(StringBuilder output)
        {
            // Must have a condition configured
            if (Condition == null)
                throw new FurballInvalidScriptNodeException("Conditional branch has no condition");

            // Emit condition
            output.Append("if ");
            Condition.EmitLua(output);
            output.AppendLine(" then");

            // Emit conditional body
            foreach (var node in Subgroup1)
                node.EmitLua(output);

            if (HasElseBranch)
            {
                output.AppendLine("else");

                // Emit alternate body
                foreach (var node in Subgroup1)
                    node.EmitLua(output);
            }

            // Emit end
            output.AppendLine("end");
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteNestedObjectProperty("Condition", Condition);
            outstream.WriteBooleanProperty("HasElseBranch", HasElseBranch);

            base.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Condition = instream.ReadNestedObjectProperty<ScriptValue>("Condition", version);
            HasElseBranch = instream.ReadBooleanProperty("HasElseBranch");

            base.Deserialize(instream, version);
        }

        public override string GetEditorSubgroup1Suffix()
        {
            return IsSubgroup2Enabled() ? "Else" : "End If";
        }

        public override string GetEditorSubgroup2Suffix()
        {
            return "End If";
        }

        public override bool IsSubgroup2Enabled()
        {
            return HasElseBranch;
        }

    }

}
