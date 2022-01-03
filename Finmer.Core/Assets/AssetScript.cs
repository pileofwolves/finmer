/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Represents a Lua script.
    /// </summary>
    public class AssetScript : AssetBase
    {

        /// <summary>
        /// Gets or sets the script contents.
        /// </summary>
        public string ScriptText { get; set; } = String.Empty;

        /// <summary>
        /// Binary precompiled version of the script, or null if unavailable.
        /// </summary>
        public CompiledScript PrecompiledScript { get; set; }

        public override void Serialize(IFurballContentWriter outstream)
        {
            base.Serialize(outstream);

            // Write the script file only if it actually has meaningful content. Otherwise, emit a null attachment, which will erase it.
            byte[] source_utf8 = !String.IsNullOrWhiteSpace(ScriptText) ? Encoding.UTF8.GetBytes(ScriptText) : null;

            // Write the attachment
            outstream.WriteAttachment(GetScriptAttachmentName(), source_utf8);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            base.Deserialize(instream, version);

            // Look for an attachment file
            byte[] source_utf8 = instream.ReadAttachment(GetScriptAttachmentName());

            // Convert the UTF-8 bytestream to a string, or fall back on an empty script if the attachment was unavailable
            ScriptText = (source_utf8 != null) ? Encoding.UTF8.GetString(source_utf8) : String.Empty;
        }

        /// <summary>
        /// Returns true if the script contains meaningful text (it is not null, and contains more than just whitespace).
        /// </summary>
        public bool HasContent()
        {
            return !String.IsNullOrWhiteSpace(ScriptText);
        }

        /// <summary>
        /// Returns an attachment key suitable for the script's source text.
        /// </summary>
        private string GetScriptAttachmentName()
        {
            return Name + ".lua";
        }

    }

}
