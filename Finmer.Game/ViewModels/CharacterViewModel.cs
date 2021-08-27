/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Gameplay;

namespace Finmer.ViewModels
{

    /// <summary>
    /// Represents a viewmodel that wraps a Character object.
    /// </summary>
    public class CharacterViewModel : GameObjectViewModel
    {

        public Character CharacterModel => (Character)GameObjectModel;

        public CharacterViewModel(Character owner) : base(owner) {}

        public int Health => CharacterModel.Health;

        public int HealthMax => CharacterModel.HealthMax;

        public int Strength => CharacterModel.Strength;

        public int Agility => CharacterModel.Agility;

        public int Body => CharacterModel.Body;

        public int Wits => CharacterModel.Wits;

        public Item[] Equipment => CharacterModel.Equipment;

    }

}
