/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Script condition that tests the world clock.
    /// </summary>
    public sealed class ConditionTimeDay : ScriptConditionNumberComparison
    {

        public override string GetEditorDescription(IContentStore content)
        {
            return "Current Day " + base.GetEditorDescription(content);
        }

        public override EColor GetEditorColor()
        {
            return EColor.Time;
        }

        protected override string GetLeftOperandExpression()
        {
            return "GetTimeDay()";
        }

    }

}
