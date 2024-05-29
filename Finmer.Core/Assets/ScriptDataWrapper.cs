/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core.Serialization;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Contains a downstream ScriptData, transparently wrapped in order to enable type replacement in the editor.
    /// </summary>
    public sealed class ScriptDataWrapper : ScriptData
    {

        /// <summary>
        /// The ScriptData that is wrapped by this helper.
        /// </summary>
        public ScriptData Wrapped { get; set; }

        private ScriptDataWrapper(ScriptData wrapped)
        {
            Wrapped = wrapped;
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            // Ensure the wrapped script has its name assigned properly
            Wrapped.Name = this.Name;

            // Forward the serialization call
            Wrapped.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            // Forward deserialization call
            Wrapped.Deserialize(instream);
        }

        public override string GetScriptText(IContentStore content)
        {
            return Wrapped?.GetScriptText(content) ?? String.Empty;
        }

        public override bool HasContent()
        {
            return Wrapped != null && Wrapped.HasContent();
        }

        public static ScriptDataWrapper EnsureWrapped(ScriptData data)
        {
            // If the input is already a wrapper, return it as-is
            if (data is ScriptDataWrapper wrapper)
                return wrapper;

            // Otherwise, create a new wrapper
            return new ScriptDataWrapper(data)
            {
                Name = data?.Name ?? String.Empty
            };
        }


    }

    /// <summary>
    /// Provides extension methods related to script serialization.
    /// </summary>
    public static class ScriptDataExtensionMethods
    {

        /// <summary>
        /// Helper for writing a nested script object property only if the script is not empty.
        /// </summary>
        public static void WriteNestedScriptProperty(this IFurballContentWriter outstream, string key, ScriptData data)
        {
            // If the script is empty, then don't bother serializing it at all
            if (data != null && !data.HasContent())
                data = null;

            outstream.WriteNestedObjectProperty(key, data);
        }

    }

}
