/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Script condition that tests the player's health.
    /// </summary>
    public sealed class ConditionPlayerHealth : ScriptConditionNumberComparison
    {

        public override string GetEditorDescription()
        {
            return "Player Health " + base.GetEditorDescription();
        }

        protected override string GetLeftOperandExpression()
        {
            return "Player.Health";
        }

    }

}
