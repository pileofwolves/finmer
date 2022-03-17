/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Diagnostics;
using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Contains a Lua script that is stored as an external file attachment.
    /// </summary>
    public sealed class ScriptDataExternal : ScriptDataInline
    {

        public override void Serialize(IFurballContentWriter outstream)
        {
            // Write the script file only if it actually has meaningful content. Otherwise, emit a null attachment, which will erase it.
            byte[] source_utf8 = !String.IsNullOrWhiteSpace(ScriptText) ? Encoding.UTF8.GetBytes(ScriptText) : null;

            // Write the attachment
            outstream.WriteStringProperty("Name", Name);
            outstream.WriteAttachment(GetScriptAttachmentName(), source_utf8);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            // Look for an attachment file
            Name = instream.ReadStringProperty("Name");
            byte[] source_utf8 = instream.ReadAttachment(GetScriptAttachmentName());

            // Convert the UTF-8 bytestream to a string, or fall back on an empty script if the attachment was unavailable
            ScriptText = (source_utf8 != null) ? Encoding.UTF8.GetString(source_utf8) : String.Empty;
        }

        /// <summary>
        /// Returns an attachment key suitable for the script's source text.
        /// </summary>
        private string GetScriptAttachmentName()
        {
            Debug.Assert(!String.IsNullOrEmpty(Name));
            return Name + ".lua";
        }

    }

}
