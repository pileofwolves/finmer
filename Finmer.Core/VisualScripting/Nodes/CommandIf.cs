/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
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
        public bool HasElseBranch { get; set; }

        public List<ScriptNode> MainSubgroup { get; set; } = new List<ScriptNode>();

        public List<ScriptNode> ElseSubgroup { get; set; } = new List<ScriptNode>();

        public override string GetEditorDescription(IContentStore content)
        {
            string join_word = (Condition.Mode == ScriptConditionGroup.EConditionMode.All) ? " And " : " Or ";
            return $"If {(Condition.Operand ? String.Empty : "Not ")}{String.Join(join_word, Condition.Tests.Select(c => c.GetEditorDescription(content)))}:";
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
            foreach (var node in MainSubgroup)
                node.EmitLua(output, content);

            if (HasElseBranch)
            {
                output.AppendLine("else");

                // Emit alternate body
                foreach (var node in ElseSubgroup)
                    node.EmitLua(output, content);
            }

            // Emit end
            output.AppendLine("end");
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            // Configuration
            Condition.Serialize(outstream);
            outstream.WriteBooleanProperty("HasElseBranch", HasElseBranch);

            // Node subgroups
            SerializeSubgroup(outstream, nameof(MainSubgroup), MainSubgroup);
            if (HasElseBranch)
                SerializeSubgroup(outstream, nameof(ElseSubgroup), ElseSubgroup);

            base.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            // Configuration
            Condition.Deserialize(instream);
            HasElseBranch = instream.ReadBooleanProperty("HasElseBranch");

            // Node subgroups
            MainSubgroup = DeserializeSubgroup(instream, nameof(MainSubgroup));
            if (HasElseBranch)
                ElseSubgroup = DeserializeSubgroup(instream, nameof(ElseSubgroup));

            base.Deserialize(instream);
        }

        public override IEnumerable<Subgroup> GetSubgroups()
        {
            yield return new Subgroup
            {
                EditorSuffix = HasElseBranch ? "Else" : "End If",
                Nodes = MainSubgroup
            };

            if (HasElseBranch)
            {
                yield return new Subgroup
                {
                    EditorSuffix = "End If",
                    Nodes = ElseSubgroup
                };
            }
        }

    }

}
