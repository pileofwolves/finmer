/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

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
            Wrapped.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            Wrapped.Deserialize(instream, version);
        }

        public override string GetScriptText()
        {
            return Wrapped.GetScriptText();
        }

        public override bool HasContent()
        {
            return Wrapped.HasContent();
        }

        public static ScriptDataWrapper EnsureWrapped(ScriptData data)
        {
            // If the input is already a wrapper, return it as-is
            if (data is ScriptDataWrapper wrapper)
                return wrapper;

            // Otherwise, create a new wrapper
            return new ScriptDataWrapper(data)
            {
                Name = data.Name
            };
        }

    }

}
