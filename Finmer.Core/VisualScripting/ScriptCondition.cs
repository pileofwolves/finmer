/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Core.VisualScripting
{

    /// <summary>
    /// Represents a value transformer script node.
    /// </summary>
    public abstract class ScriptCondition : ScriptNode
    {

        public override EColor GetEditorColor()
        {
            return EColor.System;
        }

    }

}
