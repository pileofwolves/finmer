/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Finmer.Core.Serialization;
using Finmer.Core.VisualScripting;

namespace Finmer.Core.Buffs
{

    /// <summary>
    /// Describes a group of buffs that can be conditionally applied by an equipable item.
    /// </summary>
    public class EquipEffectGroup : IFurballSerializable
    {

        /// <summary>
        /// Describes the event in response to which this effect group may trigger.
        /// </summary>
        public enum EProcStyle : byte
        {
            Always,
            RoundStart,
            TurnStart,
            WielderAttackHit,
            WielderAttackMiss,
            WielderGrappled,
            WielderSwallowed,
            WielderSwallowsPrey,
            EnemyAttackHit,
            EnemyAttackMiss,
        }

        /// <summary>
        /// Describes the target to whom buffs are applied.
        /// </summary>
        public enum EProcTarget : byte
        {
            Self,
            Opponent,
            AllAllies,
            AllOpponents
        }

        /// <summary>
        /// Which event this event group listens for.
        /// </summary>
        public EProcStyle ProcStyle { get; set; } = EProcStyle.Always;

        /// <summary>
        /// Which target(s) to apply buffs to when the group triggers.
        /// </summary>
        public EProcTarget ProcTarget { get; set; } = EProcTarget.Self;

        /// <summary>
        /// Normalized chance factor (0-1) for group to trigger.
        /// </summary>
        public float ProcChance { get; set; } = 1.0f;

        /// <summary>
        /// String table key to print in game log when group triggers. If empty, no text is printed.
        /// </summary>
        public string ProcStringTableKey { get; set; } = String.Empty;

        /// <summary>
        /// Duration of applied buffs, in combat rounds.
        /// </summary>
        public int Duration { get; set; } = 1;

        /// <summary>
        /// The collection of buffs that are applied when the group triggers.
        /// </summary>
        public List<Buff> Buffs { get; set; } = new List<Buff>();

        /// <summary>
        /// Returns a human-readable string describing this effect group for editor display.
        /// </summary>
        public string GetEditorDescription()
        {
            string buff_list = Buffs.Any() ? String.Join(", ", Buffs.Select(buff => buff.GetDescription())) : "(Empty)";
            float proc_chance = ProcChance * 100.0f;
            switch (ProcStyle)
            {
                case EProcStyle.Always:                 return "While equipped: " + buff_list;
                case EProcStyle.RoundStart:             return $"On round start ({proc_chance:F0}%): " + buff_list;
                case EProcStyle.TurnStart:              return $"On turn start ({proc_chance:F0}%): " + buff_list;
                case EProcStyle.WielderAttackHit:       return $"On attack hit ({proc_chance:F0}%): " + buff_list;
                case EProcStyle.WielderAttackMiss:      return $"On attack miss ({proc_chance:F0}%): " + buff_list;
                case EProcStyle.WielderGrappled:        return $"When grappling ({proc_chance:F0}%): " + buff_list;
                case EProcStyle.WielderSwallowed:       return $"When swallowed ({proc_chance:F0}%): " + buff_list;
                case EProcStyle.WielderSwallowsPrey:    return $"When swallowing prey ({proc_chance:F0}%): " + buff_list;
                case EProcStyle.EnemyAttackHit:         return $"On hit by enemy ({proc_chance:F0}%): " + buff_list;
                case EProcStyle.EnemyAttackMiss:        return $"On missed by enemy ({proc_chance:F0}%): " + buff_list;
                default:                                throw new InvalidScriptNodeException("Unknown proc style");
            }
        }

        public void Serialize(IFurballContentWriter outstream)
        {
            // Group type
            outstream.WriteEnumProperty(nameof(ProcStyle), ProcStyle);

            // Proc settings, relevant only for conditionally applied buffs
            if (ProcStyle != EProcStyle.Always)
            {
                outstream.WriteEnumProperty(nameof(ProcTarget), ProcTarget);
                outstream.WriteFloatProperty(nameof(ProcChance), ProcChance);
                outstream.WriteStringProperty(nameof(ProcStringTableKey), ProcStringTableKey);
                outstream.WriteCompressedInt32Property(nameof(Duration), Duration);
            }

            // Buff collection
            outstream.BeginArray(nameof(Buffs), Buffs.Count);
            foreach (var buff in Buffs)
                outstream.WriteObjectProperty(null, buff, EFurballObjectMode.Required);
            outstream.EndArray();
        }

        public void Deserialize(IFurballContentReader instream)
        {
            // Group type
            ProcStyle = instream.ReadEnumProperty<EProcStyle>(nameof(ProcStyle));

            // Proc settings, relevant only for conditionally applied buffs
            if (ProcStyle != EProcStyle.Always)
            {
                ProcTarget = instream.ReadEnumProperty<EProcTarget>(nameof(ProcTarget));
                ProcChance = instream.ReadFloatProperty(nameof(ProcChance));
                ProcStringTableKey = instream.ReadStringProperty(nameof(ProcStringTableKey));
                Duration = instream.ReadCompressedInt32Property(nameof(Duration));
            }

            // Buff collection
            for (int i = 0, c = instream.BeginArray(nameof(Buffs)); i < c; i++)
                Buffs.Add(instream.ReadObjectProperty<Buff>(null, EFurballObjectMode.Required));
            instream.EndArray();
        }

    }

}
