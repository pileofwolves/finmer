/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting
{

    /// <summary>
    /// Base class for script value transformers that operate on one nested operand.
    /// </summary>
    public abstract class ScriptValueSingleOperand : ScriptValue
    {

        /// <summary>
        /// The nested operand to transform.
        /// </summary>
        public ScriptValue Operand { get; set; }

        public override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteNestedObjectProperty("Operand", Operand);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Operand = instream.ReadNestedObjectProperty<ScriptValue>("Operand", version);
        }

    }

}
