/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Represents the test for this branch.
        /// </summary>
        public ScriptConditionGroup Condition { get; set; } = new ScriptConditionGroup();

        /// <summary>
        /// Whether this conditional branch has an else branch.
        /// </summary>
        public bool HasElseBranch { get; set; } = false;

        public override string GetEditorDescription()
        {
            string join_word = (Condition.Mode == ScriptConditionGroup.EConditionMode.All) ? " And " : " Or ";
            return $"If {(Condition.Operand ? String.Empty : "Not ")}{String.Join(join_word, Condition.Tests.Select(c => c.GetEditorDescription()))}:";
        }

        public override EColor GetEditorColor()
        {
            return EColor.FlowControl;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // Emit the branch statement
            output.Append("if ");
            Condition.EmitLua(output, content);
            output.AppendLine(" then");

            // Emit conditional body
            foreach (var node in Subgroup1)
                node.EmitLua(output, content);

            if (HasElseBranch)
            {
                output.AppendLine("else");

                // Emit alternate body
                foreach (var node in Subgroup2)
                    node.EmitLua(output, content);
            }

            // Emit end
            output.AppendLine("end");
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteBooleanProperty("HasElseBranch", HasElseBranch);
            Condition.Serialize(outstream);

            base.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            HasElseBranch = instream.ReadBooleanProperty("HasElseBranch");
            Condition.Deserialize(instream, version);

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
