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
    /// Represents an executable command script node with a single integer parameter.
    /// </summary>
    public abstract class ScriptCommandSingleInt : ScriptNode
    {

        /// <summary>
        /// The integer value configured for this node.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Returns the editor window title appropriate for this node.
        /// </summary>
        public abstract string GetEditorWindowTitle();

        public sealed override void Serialize(IFurballContentWriter outstream)
        {
            outstream.WriteInt32Property("Value", Value);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Value = instream.ReadInt32Property("Value");
        }

    }

}
