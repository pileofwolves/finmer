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
    /// Script condition that tests the player's max health.
    /// </summary>
    public sealed class ConditionPlayerHealthMax : ScriptConditionNumberComparison
    {

        public override string GetEditorDescription()
        {
            return "Player Max Health " + base.GetEditorDescription();
        }

        public override EColor GetEditorColor()
        {
            return EColor.Player;
        }

        protected override string GetLeftOperandExpression()
        {
            return "Player.HealthMax";
        }

    }

}
