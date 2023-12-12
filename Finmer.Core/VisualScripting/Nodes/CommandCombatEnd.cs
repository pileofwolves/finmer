/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Command that terminates the active combat session.
    /// </summary>
    public sealed class CommandCombatEnd : ScriptCommand
    {

        public override string GetEditorDescription(IContentStore content)
        {
            return "Stop Combat";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Combat;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.AppendLine("GetActiveCombat():End()");
        }

    }

}
