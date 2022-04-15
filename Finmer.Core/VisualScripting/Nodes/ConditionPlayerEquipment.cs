/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Text;
using Finmer.Core.Serialization;

namespace Finmer.Core.VisualScripting.Nodes
{

    /// <summary>
    /// Script value that returns an equipped item.
    /// </summary>
    public sealed class ConditionPlayerEquipment : ScriptCondition
    {

        /// <summary>
        /// Identifies an equipment slot.
        /// </summary>
        public enum ESlot
        {
            Weapon,
            Armor,
            Accessory1,
            Accessory2
        }

        /// <summary>
        /// The slot number to inspect.
        /// </summary>
        public ESlot Slot { get; set; }

        public override string GetEditorDescription()
        {
            return "Player " + Slot;
        }

        public override EColor GetEditorColor()
        {
            return EColor.Player;
        }

        public override void EmitLua(StringBuilder output, IContentStore content)
        {
            output.Append("Player.Equipped");
            output.Append(Slot.ToString());
        }

    }

}
