/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting
{

    /// <summary>
    /// Encapsulates a collection of ScriptConditions.
    /// </summary>
    public sealed class ScriptConditionGroup : IFurballSerializable
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
        public List<ScriptCondition> Tests { get; set; } = new List<ScriptCondition>();

        /// <summary>
        /// Describes how multiple conditions should be combined.
        /// </summary>
        public EConditionMode Mode { get; set; } = EConditionMode.All;

        /// <summary>
        /// The right-hand operand of each of the condition comparisons.
        /// </summary>
        public bool Operand { get; set; } = true;

        /// <summary>
        /// Generate Lua code for this group of conditions.
        /// </summary>
        public void EmitLua(StringBuilder output, IContentStore content)
        {
            // Must have a condition configured
            if (Tests.Count == 0)
                throw new InvalidScriptNodeException("Conditional branch has no condition");

            // Emit the branch statement
            for (var i = 0; i < Tests.Count; i++)
            {
                var condition = Tests[i];

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
        }

        public void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteEnumProperty("Mode", Mode);
            outstream.WriteBooleanProperty("Operand", Operand);

            outstream.BeginArray("Tests", Tests.Count);
            foreach (var condition in Tests)
                outstream.WriteNestedObjectProperty(null, condition);
            outstream.EndArray();
        }

        public void Deserialize(IFurballContentReader instream, int version)
        {
            Mode = instream.ReadEnumProperty<EConditionMode>("Mode");
            Operand = instream.ReadBooleanProperty("Operand");

            for (int i = 0, c = instream.BeginArray("Tests"); i < c; i++)
                Tests.Add(instream.ReadNestedObjectProperty<ScriptCondition>(null, version));
            instream.EndArray();
        }

    }

}
