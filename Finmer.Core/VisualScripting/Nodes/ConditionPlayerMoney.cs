/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Script condition that tests the player's money total.
    /// </summary>
    public sealed class ConditionPlayerMoney : ScriptConditionNumberComparison
    {

        public override string GetEditorDescription(IContentStore content)
        {
            return "Money " + base.GetEditorDescription(content);
        }

        public override EColor GetEditorColor()
        {
            return EColor.Player;
        }

        protected override string GetLeftOperandExpression()
        {
            return "Player.Money";
        }

    }

}
