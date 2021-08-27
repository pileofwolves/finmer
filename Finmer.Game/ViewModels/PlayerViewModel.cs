/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using Finmer.Gameplay;

namespace Finmer.ViewModels
{

    /// <summary>
    /// Exposes bindings for the player character.
    /// </summary>
    public sealed class PlayerViewModel : CharacterViewModel
    {

        public Player PlayerModel => (Player)GameObjectModel;

        public PlayerViewModel(Player owner) : base(owner) {}

        public int Level => PlayerModel.Level;

        public int XP => PlayerModel.XP;

        public int RequiredXP => PlayerModel.RequiredXP;

        public int Money => PlayerModel.Money;

        public string Species => PlayerModel.Species;

        public List<Item> Inventory => PlayerModel.Inventory;

        public int TimeHour => PlayerModel.TimeHour;

        public int TimeDay => PlayerModel.TimeDay;

        public int TimeHourCumulative => PlayerModel.TimeHourCumulative;

        public int AbilityPoints => PlayerModel.AbilityPoints;

    }

}
