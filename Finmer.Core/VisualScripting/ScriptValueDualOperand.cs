/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting
{

    /// <summary>
    /// Base class for script value transformers that operate on two operands.
    /// </summary>
    public abstract class ScriptValueDualOperand : ScriptValue
    {

        /// <summary>
        /// The first operand.
        /// </summary>
        public ScriptValue Left { get; set; }

        /// <summary>
        /// The second operand.
        /// </summary>
        public ScriptValue Right { get; set; }

        public override void EmitLua(StringBuilder output)
        {
            // Must have operands configured
            if (Left == null)
                throw new FurballInvalidScriptNodeException("Left operand not configured");
            if (Right == null)
                throw new FurballInvalidScriptNodeException("Right operand not configured");
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteNestedObjectProperty("Left", Left);
            outstream.WriteNestedObjectProperty("Right", Right);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Left = instream.ReadNestedObjectProperty<ScriptValue>("Left", version);
            Right = instream.ReadNestedObjectProperty<ScriptValue>("Right", version);
        }

    }

}
