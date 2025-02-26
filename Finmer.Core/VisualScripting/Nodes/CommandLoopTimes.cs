/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that loops a body of commands for a set number of times.
    /// </summary>
    public sealed class CommandLoopTimes : ScriptCommandContainer
    {

        /// <summary>
        /// The number of times to repeat the loop body.
        /// </summary>
        public ValueWrapperInt RepeatCount { get; set; } = new ValueWrapperInt();

        /// <summary>
        /// The nested nodes that form the loop body.
        /// </summary>
        public List<ScriptNode> LoopBody { get; set; } = new List<ScriptNode>();

        public CommandLoopTimes()
        {
            // A loop count of one is a sensible default - looks a bit better than zero
            RepeatCount.OperandMode = ValueWrapperInt.EOperandMode.Literal;
            RepeatCount.OperandLiteral = 1;
        }

        public override string GetEditorDescription(IContentStore content)
        {
            return $"Repeat {RepeatCount.GetOperandDescription()} Times:";
        }

        public override EColor GetEditorColor()
        {
            return EColor.FlowControl;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            string iterator_variable = CombatUtilities.GetLocalVariableIdentifier(this, 1);
            string count_variable = CombatUtilities.GetLocalVariableIdentifier(this, 2);

            // Evaluate count variable once, since the expression could have side-effects
            output.Append("do local ");
            output.Append(count_variable);
            output.Append(" = ");
            output.AppendLine(RepeatCount.GetOperandLuaSnippet());

            // Emit loop header
            output.Append("for ");
            output.Append(iterator_variable);
            output.Append(" = 1,");
            output.Append(count_variable);
            output.AppendLine(" do");

            // Emit loop body
            foreach (var node in LoopBody)
                node.EmitLua(output, content);

            // Emit end
            output.AppendLine("end end");
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteObjectProperty(nameof(RepeatCount), RepeatCount, EFurballObjectMode.Required);
            SerializeSubgroup(outstream, nameof(LoopBody), LoopBody);

            base.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            RepeatCount = instream.ReadObjectProperty<ValueWrapperInt>(nameof(RepeatCount), EFurballObjectMode.Required);
            LoopBody = DeserializeSubgroup(instream, nameof(LoopBody));

            base.Deserialize(instream);
        }

        public override IEnumerable<Subgroup> GetSubgroups()
        {
            yield return new Subgroup
            {
                EditorSuffix = "Repeat Above",
                Nodes = LoopBody
            };
        }

    }

}
