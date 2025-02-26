/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Serialization;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Represents a container for a script.
    /// </summary>
    public abstract class ScriptData : IFurballSerializable
    {

        /// <summary>
        /// The name associated with the script.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns the Lua script text represented by this container.
        /// </summary>
        public abstract string GetScriptText(IContentStore content);

        /// <summary>
        /// Returns true if the script has meaningful content (i.e. not empty) and should be compiled.
        /// </summary>
        public abstract bool HasContent();

        public abstract void Serialize(IFurballContentWriter outstream);

        public abstract void Deserialize(IFurballContentReader instream);

    }

}
