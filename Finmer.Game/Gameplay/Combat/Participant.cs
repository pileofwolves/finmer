/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using Finmer.ViewModels;

namespace Finmer.Gameplay.Combat
{

    /// <summary>
    /// Contains session-specific state for an individual participant in a combat.
    /// </summary>
    public class Participant : BaseProp
    {

        /// <summary>
        /// Gets the underlying Character data for this participant.
        /// </summary>
        public Character Character { get; }

        /// <summary>
        /// Gets the combat session to which this participant state belongs.
        /// </summary>
        public CombatSession Session { get; }

        /// <summary>
        /// Gets or sets the participant that is grappling with this participant.
        /// </summary>
        public Participant GrapplingWith { get; set; }

        /// <summary>
        /// Gets or sets whether this participant is the one with the upper paw in a grappling session.
        /// </summary>
        public bool GrapplingInitiator { get; set; }

        /// <summary>
        /// Gets or sets the participant that has swallowed this participant.
        /// </summary>
        public Participant Predator { get; set; }

        /// <summary>
        /// Gets a collection of participants that have been swallowed by this participant.
        /// </summary>
        public List<Participant> Prey { get; } = new List<Participant>();

        /// <summary>
        /// Indicates whether the participant should not receive digestion damage this turn.
        /// </summary>
        public bool DigestionImmunity { get; set; } = false;

        public Participant(Character character, CombatSession session)
        {
            Character = character;
            Session = session;
        }

        /// <summary>
        /// Indicates whether this participant can perform an action during the current turn.
        /// </summary>
        public bool CanAct()
        {
            return !Character.IsDead();
        }

        /// <summary>
        /// Shorthand for checking if the underlying character of this participant represents the player character.
        /// </summary>
        public bool IsPlayer()
        {
            return Character is Player;
        }

        /// <summary>
        /// Shorthand for checking if this participant is engaged in a grappling session.
        /// </summary>
        public bool IsGrappling()
        {
            return GrapplingWith != null;
        }

        /// <summary>
        /// Shorthand for checking if a predator is assigned to this participant.
        /// </summary>
        public bool IsSwallowed()
        {
            return Predator != null;
        }

        /// <summary>
        /// Notify the attached viewmodel, if any, that participant state has changed.
        /// </summary>
        public void UpdateDisplay()
        {
            OnPropertyChanged();
        }

    }

}
