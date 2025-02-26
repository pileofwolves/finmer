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
    /// Command that fully restores the player's health.
    /// </summary>
    public sealed class CommandPlayerHealAll : ScriptCommand
    {

        public override string GetEditorDescription(IContentStore content)
        {
            return "Restore All Player Health";
        }

        public override EColor GetEditorColor()
        {
            return EColor.Player;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.AppendLine("Player.Health = Player.HealthMax");
        }

    }

}
