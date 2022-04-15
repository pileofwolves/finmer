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
    /// Represents an element in a visual scripting node graph.
    /// </summary>
    public abstract class ScriptNode : IFurballSerializable
    {

        /// <summary>
        /// Describes the node's intended purpose, to be used for highlighting in the editor.
        /// </summary>
        public enum EColor
        {
            System,
            Code,
            Comment,
            Message,
            FlowControl,
            SceneControl,
            Variable,
            Sleep,
            SaveData,
            Combat,
            Player,
            Journal,
        }

        /// <summary>
        /// Returns a human-readable description of this node, including configured settings.
        /// </summary>
        public abstract string GetEditorDescription();

        /// <summary>
        /// Returns the color to apply to the node in the editor.
        /// </summary>
        public abstract EColor GetEditorColor();

        /// <summary>
        /// Generate Lua code for this node.
        /// </summary>
        public abstract void EmitLua(StringBuilder output, IContentStore content);

        public virtual void Serialize(IFurballContentWriter outstream) {}

        public virtual void Deserialize(IFurballContentReader instream, int version) {}

    }

}
