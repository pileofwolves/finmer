/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using Finmer.Core.VisualScripting;
using Finmer.Core.VisualScripting.Nodes;

namespace Finmer.Editor
{

    /// <summary>
    /// Provides a mapping from a ScriptNode type to a Form that can be used to edit it.
    /// </summary>
    public static class ScriptNodeFormMapper
    {

        private static readonly Dictionary<Type, Func<FormScriptNode>> s_NodeMap = new Dictionary<Type, Func<FormScriptNode>>
        {
            { typeof(CommandComment),               () => new FormScriptNodeComment() },
            { typeof(CommandSleep),                 () => new FormScriptNodeSingleFloat() },
            { typeof(CommandInlineSnippet),         () => new FormScriptNodeLuaSnippet() },
        };

        /// <summary>
        /// Returns the editor form type to be used for editing the input node type, or null if no form is needed.
        /// </summary>
        public static FormScriptNode CreateEditorForm(ScriptNode node)
        {
            // Find the mapped form type
            if (s_NodeMap.TryGetValue(node.GetType(), out var form_ctor))
            {
                // Create the editor form and assign it the node to edit
                var form = form_ctor();
                form.Node = node;
                return form;
            }

            // If no mapping exists, indicate no form is required
            return null;
        }

    }

}
