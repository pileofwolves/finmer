/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core;
using Finmer.Core.Buffs;
using Finmer.Gameplay.Scripting;

namespace Finmer.Gameplay.Combat
{

    /// <summary>
    /// A temporary buff object that can be created from script, and assigned to a combat participant.
    /// </summary>
    public sealed class PendingBuff : ScriptableObject
    {

        /// <summary>
        /// The applied buff effect encapsulated by this object.
        /// </summary>
        public Buff Effect { get; set; }

        /// <summary>
        /// The lifetime of the buff, in rounds.
        /// </summary>
        public int Duration { get; set; }

        public PendingBuff(ScriptContext context) : base(context) {}

        /// <inheritdoc />
        public override PropertyBag SaveState()
        {
            var output = base.SaveState();

            output.SetSavedAsset(Effect, SaveData.k_PendingBuff_Effect);
            output.SetInt(SaveData.k_PendingBuff_Duration, Duration);

            return output;
        }

        /// <inheritdoc />
        public override void LoadState(PropertyBag input)
        {
            base.LoadState(input);

            Effect = input.GetSavedAsset<Buff>(SaveData.k_PendingBuff_Effect);
            Duration = Math.Max(input.GetInt(SaveData.k_PendingBuff_Duration, 1), 1);
        }

    }

}
