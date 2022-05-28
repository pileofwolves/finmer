/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core.Serialization;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Represents a Lua script.
    /// </summary>
    public sealed class AssetScript : AssetBase
    {

        /// <summary>
        /// Describes which editor controls are relevant for a script.
        /// </summary>
        [Obsolete]
        public enum EScriptType
        {
            Action,
            Condition,
            ExternalOnly
        }

        /// <summary>
        /// Container for the script data represented by this standalone asset.
        /// </summary>
        public ScriptData Contents { get; set; }

        /// <summary>
        /// Binary precompiled version of the script, or null if unavailable.
        /// </summary>
        public CompiledScript PrecompiledScript { get; set; }

        /// <summary>
        /// The type of editor controls to show for this script.
        /// </summary>
        [Obsolete]
        public EScriptType EditorType { get; set; } = EScriptType.ExternalOnly;

        public override void Serialize(IFurballContentWriter outstream)
        {
            base.Serialize(outstream);

            outstream.WriteNestedScriptProperty(nameof(Contents), Contents);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            base.Deserialize(instream, version);

            if (version >= 16)
            {
                Contents = instream.ReadNestedObjectProperty<ScriptData>(nameof(Contents), version);
                if (Contents != null)
                    Contents.Name = Name;
            }
            else
            {
                // V15 backwards compatibility
                Contents = new ScriptDataExternal
                {
                    Name = Name
                };
                Contents.Deserialize(instream, version);
            }
        }

    }

}
