/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;
using Finmer.Gameplay;
using Finmer.Gameplay.Combat;
using Finmer.Utility;

namespace Finmer.ViewModels
{

    /// <summary>
    /// Represents a combatant to be viewed in the main window's combat sidebar.
    /// </summary>
    public sealed class CombatParticipantViewModel : BaseProp
    {

        private const float k_CriticalHealthFraction = 0.3f;

        private readonly Character m_Upstream;
        private readonly Participant m_Participant;

        public CombatParticipantViewModel(Participant owner)
        {
            m_Upstream = owner.Character;
            m_Participant = owner;

            // Subscribe to change notifications for the participant itself
            PropertyChangedEventManager.AddHandler(m_Upstream, Character_PropertyChanged, String.Empty);
            PropertyChangedEventManager.AddHandler(m_Participant, Participant_PropertyChanged, String.Empty);

            // Subscribe to change notifications for the turn indicator
            PropertyChangedEventManager.AddHandler(m_Participant.Session, Session_PropertyChanged, nameof(CombatSession.WhoseTurn));
        }

        public string Text => m_Upstream.Name;

        public string Subtext => GetLatestSubtext();

        public bool IsAlly => m_Upstream.IsAlly;

        public bool IsDead => m_Upstream.IsDead();

        public bool IsMyTurn => m_Participant.Session.WhoseTurn == m_Participant;

        private bool Danger => Health / (float)HealthMax < k_CriticalHealthFraction;

        public int Health => m_Upstream.Health;

        public int HealthMax => m_Upstream.HealthMax;

        public Color InnerColor => IsAlly
            ? Color.FromRgb(90, 90, 176)
            : Color.FromRgb(154, 32, 32);

        public Color OuterColor => IsAlly
            ? Color.FromRgb(32, 32, 48)
            : Color.FromRgb(64, 32, 32);

        public Brush TextColor => Danger
            ? new SolidColorBrush(Theme.LogColorNegative)
            : new SolidColorBrush(Theme.LogColorDefault);

        private void Character_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(m_Upstream.Health)))
            {
                OnPropertyChanged(nameof(TextColor));
                OnPropertyChanged(nameof(IsDead)); // actually nameof(Danger)
            }

            // Any change in the combat state should get us to reevaluate the display state as well
            OnPropertyChanged(nameof(Subtext));

            // Forward to attached view
            OnPropertyChanged(e.PropertyName);
        }

        private void Participant_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Any change in the combat state should get us to reevaluate the display state as well
            OnPropertyChanged(nameof(Subtext));
        }

        private void Session_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // We only subscribed to change events for the WhoseTurn property, so we shouldn't receive any others
            Debug.Assert(e.PropertyName.Equals(nameof(CombatSession.WhoseTurn)));

            // Re-evaluate whether to display the turn indicator
            OnPropertyChanged(nameof(IsMyTurn));
        }

        private string GetLatestSubtext()
        {
            var text = new List<string>();

            // Downed state
            if (IsDead)
                text.Add(m_Participant.IsSwallowed() ? "Digested" : "Down");
            else if (Danger)
                text.Add("Badly injured");

            // Predator link
            if (m_Participant.IsSwallowed())
            {
                var predator = m_Participant.Predator;
                text.Add($"Swallowed by {predator.Character.Name}");
            }

            // Grapple link
            if (m_Participant.IsGrappling())
            {
                var other = m_Participant.GrapplingWith;
                text.Add(m_Participant.GrapplingInitiator ? $"Grappling with {other.Character.Name}" : $"Pinned by {other.Character.Name}");
            }

            // Prey links
            int prey_count = m_Participant.Prey.Count;
            if (prey_count != 0)
            {
                var prey = m_Participant.Prey.First();
                text.Add(m_Participant.Character.PredatorDigests ? $"Digesting {prey.Character.Name}" : $"Swallowed {prey.Character.Name}");

                // Extra suffix text for several prey, don't list all of them to avoid clutter
                if (prey_count > 1)
                    text.Add($"(... and {prey_count - 1} more)");
            }

            // If any tags were found, show them
            if (text.Count != 0)
                return String.Join(Environment.NewLine, text);

            // Default value
            return "Ready";
        }

    }

}
