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
            { typeof(CommandIf),                    () => new FormScriptCmdIf() },
            { typeof(CommandComment),               () => new FormScriptCmdComment() },
            { typeof(CommandSleep),                 () => new FormScriptCmdSleep() },
            { typeof(CommandLog),                   () => new FormScriptCmdLog() },
            { typeof(CommandInlineSnippet),         () => new FormScriptCmdLuaSnippet() },
            { typeof(CommandSetInventoryEnabled),   () => new FormScriptCmdSetInvEnabled() },
            { typeof(CommandSetInstruction),        () => new FormScriptCmdSetInstruction() },
            { typeof(CommandSetLocation),           () => new FormScriptCmdSetLocation() },
            { typeof(CommandPlayerSetName),         () => new FormScriptCmdPlayerSetName() },
            { typeof(CommandPlayerSetSpecies),      () => new FormScriptCmdPlayerSetSpecies() },
            { typeof(CommandPlayerSetStat),         () => new FormScriptCmdPlayerSetStat() },
            { typeof(CommandPlayerSetHealth),       () => new FormScriptCmdPlayerSetHealth() },
            { typeof(CommandPlayerSetMoney),        () => new FormScriptCmdPlayerSetMoney() },
            { typeof(CommandPlayerSetEquipment),    () => new FormScriptCmdPlayerSetEquipment() },
            { typeof(CommandPlayerSetItem),         () => new FormScriptCmdPlayerSetItem() },
            { typeof(CommandPlayerAddXP),           () => new FormScriptCmdPlayerAddXP() },
            { typeof(CommandPlayerAddAP),           () => new FormScriptCmdPlayerAddAP() },
            { typeof(CommandSetScene),              () => new FormScriptCmdSetScene() },
            { typeof(CommandCombatBegin),           () => new FormScriptCmdCombatBegin() },
            { typeof(CommandShop),                  () => new FormScriptCmdShop() },
            { typeof(CommandJournalClose),          () => new FormScriptCmdJournalClose() },
            { typeof(CommandJournalUpdate),         () => new FormScriptCmdJournalUpdate() },
            { typeof(CommandVarSetFlag),            () => new FormScriptCmdVarSetFlag() },
            { typeof(CommandVarSetNumber),          () => new FormScriptCmdVarSetNum() },
            { typeof(CommandVarSetString),          () => new FormScriptCmdVarSetStr() },
            { typeof(CommandGrammarSetContext),     () => new FormScriptCmdGrammarContext() },
            { typeof(CommandGrammarSetVariable),    () => new FormScriptCmdGrammarVariable() },
            { typeof(ConditionPlayerHealth),        () => new FormScriptCondNumberComp() },
            { typeof(ConditionPlayerHealthMax),     () => new FormScriptCondNumberComp() },
            { typeof(ConditionPlayerLevel),         () => new FormScriptCondNumberComp() },
            { typeof(ConditionPlayerMoney),         () => new FormScriptCondNumberComp() },
            { typeof(ConditionPlayerEquipment),     () => new FormScriptCondPlayerEquip() },
            { typeof(ConditionPlayerHasItem),       () => new FormScriptCondPlayerItem() },
            { typeof(ConditionPlayerStat),          () => new FormScriptCondPlayerStat() },
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
