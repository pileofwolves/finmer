/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Script condition that tests the world clock.
    /// </summary>
    public sealed class ConditionTimeIsNight : ScriptCondition
    {

        public override string GetEditorDescription(IContentStore content)
        {
            return "Current Time Is Night";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Time;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.Append("GetIsNight()");
        }

    }

}
