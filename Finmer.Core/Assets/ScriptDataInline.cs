/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core.Serialization;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Contains a Lua script that is stored as an inline code snippet.
    /// </summary>
    public class ScriptDataInline : ScriptData
    {

        private const string k_DefaultInlineScriptName = "InlineScript";

        /// <summary>
        /// Gets or sets the script contents.
        /// </summary>
        public string ScriptText { get; set; } = String.Empty;

        public ScriptDataInline()
        {
            Name = k_DefaultInlineScriptName;
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            // Note: Name is implicit for inline scripts so it does not need to be read or written

            // Write the script contents
            outstream.WriteStringProperty("Script", ScriptText);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            // Note: Name is implicit for inline scripts so it does not need to be read or written

            // Read the script contents
            Name = k_DefaultInlineScriptName;
            ScriptText = instream.ReadStringProperty("Script");
        }

        public override string GetScriptText(IContentStore content)
        {
            return ScriptText;
        }

        public override bool HasContent()
        {
            return !String.IsNullOrWhiteSpace(ScriptText);
        }

    }

}
