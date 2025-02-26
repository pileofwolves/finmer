﻿/*
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
    /// Script condition that checks game settings.
    /// </summary>
    public sealed class ConditionIsDisposalEnabled : ScriptCondition
    {

        public override string GetEditorDescription(IContentStore content)
        {
            return "Is Explicit Disposal Enabled";
        }

        public override EColor GetEditorColor()
        {
            return EColor.System;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.Append("Player.IsExplicitDisposalEnabled");
        }

    }

}
