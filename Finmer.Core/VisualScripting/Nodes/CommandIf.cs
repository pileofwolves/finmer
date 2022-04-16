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
        /// Describes the conjunction mode for combining multiple conditions.
        /// </summary>
        public enum EConditionMode
        {
            All,
            Any
        }

        /// <summary>
        /// The condition that controls whether the block is executed.
        /// </summary>
        public List<ScriptCondition> Conditions { get; set; } = new List<ScriptCondition>();

        /// <summary>
        /// Describes how multiple conditions should be combined.
        /// </summary>
        public EConditionMode Mode { get; set; } = EConditionMode.All;

        /// <summary>
        /// The right-hand operand of each of the condition comparisons.
        /// </summary>
        public bool Operand { get; set; } = true;

        /// <summary>
        /// Whether this conditional branch has an else branch.
        /// </summary>
        public bool HasElseBranch { get; set; } = false;

        public override string GetEditorDescription()
        {
            string join_word = (Mode == EConditionMode.All) ? " And " : " Or ";
            return $"If {(Operand ? String.Empty : "Not ")}{String.Join(join_word, Conditions.Select(c => c.GetEditorDescription()))}:";
        }

        public override EColor GetEditorColor()
        {
            return EColor.FlowControl;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            // Must have a condition configured
            if (Conditions.Count == 0)
                throw new InvalidScriptNodeException("Conditional branch has no condition");

            // Emit the branch statement
            output.Append("if ");
            for (var i = 0; i < Conditions.Count; i++)
            {
                var condition = Conditions[i];

                // Join different conditions with an appropriate operator
                if (i != 0)
                    output.Append(Mode == EConditionMode.All ? " and " : " or ");

                // Emit inverter
                output.Append('(');
                if (!Operand)
                    output.Append("not ");

                // Emit condition
                condition.EmitLua(output, content);
                output.Append(')');
            }
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
            outstream.WriteEnumProperty("Mode", Mode);
            outstream.WriteBooleanProperty("Operand", Operand);
            outstream.WriteBooleanProperty("HasElseBranch", HasElseBranch);

            outstream.BeginArray("Conditions", Conditions.Count);
            foreach (var condition in Conditions)
                outstream.WriteNestedObjectProperty(null, condition);
            outstream.EndArray();

            base.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Mode = instream.ReadEnumProperty<EConditionMode>("Mode");
            Operand = instream.ReadBooleanProperty("Operand");
            HasElseBranch = instream.ReadBooleanProperty("HasElseBranch");

            for (int i = 0, c = instream.BeginArray("Conditions"); i < c; i++)
                Conditions.Add(instream.ReadNestedObjectProperty<ScriptCondition>(null, version));
            instream.EndArray();

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
