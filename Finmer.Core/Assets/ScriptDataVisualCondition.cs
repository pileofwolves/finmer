/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Text;
using Finmer.Core.Compilers;
using Finmer.Core.Serialization;
using Finmer.Core.VisualScripting;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Contains a Lua script that evaluates and returns a boolean value.
    /// </summary>
    public sealed class ScriptDataVisualCondition : ScriptData
    {

        /// <summary>
        /// The collection of nodes that make up this script.
        /// </summary>
        public ScriptConditionGroup Condition { get; set; } = new ScriptConditionGroup();

        public override string GetScriptText(IContentStore content)
        {
            var output = new StringBuilder();

            try
            {
                // Write the return statement
                output.Append("return ");
                Condition.EmitLua(output, content);
            }
            catch (InvalidScriptNodeException ex)
            {
                // Add more information to the exception, then rethrow it
                throw new ScriptCompilationException($"In script '{Name}': {ex.Message}", ex);
            }

            return output.ToString();
        }

        public override bool HasContent()
        {
            return Condition.Tests.Count != 0;
        }

        public override void Serialize(IFurballContentWriter outstream)
        {
            Condition.Serialize(outstream);
        }

        public override void Deserialize(IFurballContentReader instream)
        {
            Condition.Deserialize(instream);
        }

    }

}
