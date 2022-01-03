/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Finmer.Core;
using Finmer.Gameplay.Combat;
using Finmer.Models;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Hosts the second iteration of the combat system.
    /// </summary>
    public sealed class SceneCombat2 : Scene
    {

        public CombatSession Session { get; }

        private EMenuState m_MenuState = EMenuState.Default;
        private Participant[] m_RoundParticipants;
        private int m_RoundStepIndex;
        private bool m_IsEnded;

        private Participant m_Player;
        private CombatAction m_PlayerDecision;

        private ECombatAction m_PendingPlayerDecision;
        private List<Participant> m_PotentialPlayerTargets;
        private List<Item> m_PotentialPlayerItems;

        public SceneCombat2(CombatSession session)
        {
            Session = session;
        }

        public override void Enter()
        {
            // Cache the Player participant, so we can easily reference it during the UI stage
            m_Player = Session.Participants.FirstOrDefault(participant => participant.IsPlayer());

            // Sort the participants by their Wits stat, so the participant with highest Wits goes first
            Session.Participants.Sort((lhs, rhs) => Comparer<int>.Default.Compare(rhs.Character.Wits, lhs.Character.Wits));

            // Open the combat UI on the display
            CombatDisplay.OpenCombatUI(Session);
        }

        public override void Leave()
        {
            // Hide combat UI again now that the combat system is shutting down
            CombatDisplay.CloseCombatUI();
        }

        public override void Turn(int choice)
        {
            // Handle UI and user input if the player needs to choose an action
            if (IsPlayerInputRequired())
            {
                // Process provided input
                HandlePlayerInput(choice);

                // If the player still has no final decision (such as because a submenu was opened) then refresh UI and delay the round
                if (!m_PlayerDecision.IsValid())
                {
                    PrepareInterface();
                    return;
                }
            }

            // Run the combat round
            Debug.Assert(!IsPlayerInputRequired());
            StepRound();

            // If the combat is over, get rid of the combat scene so we return to game scripts
            if (IsCombatEnded())
            {
                ExitCombat();
                return;
            }

            // Show UI so the player can make a new choice. Note: we should not reach this spot if the player is not
            // participating, because then the combat should have ended in one StepRound() call.
            Debug.Assert(IsPlayerInputRequired());
            PrepareInterface();
        }

        /// <summary>
        /// Terminate the combat session and return control to the scene that invoked it.
        /// </summary>
        public void RequestExit()
        {
            // Terminate the combat loop ASAP
            m_IsEnded = true;
        }

        private void ExitCombat()
        {
            GameSession game_session = GameController.Session;
            Debug.Assert(game_session.PeekScene() == this, "Broken game state - closing the wrong Scene");

            // Run user callbacks
            Session.NotifyCombatEnded();

            // Pay out any collected XP
            if (Session.TotalXPAward > 0)
            {
                // Award XP only if the player actually participated themselves
                if (m_Player != null && !m_Player.Character.IsDead())
                    GameController.Session.Player.AwardXP(Session.TotalXPAward);

                // Reset the XP back to zero in case the same session object is reused
                Session.TotalXPAward = 0;
            }

            // Resume the caller script
            game_session.PopScene();
            game_session.ResumeScript();
        }

        private void PrepareInterface()
        {
            // Show UI for the next player choice
            switch (m_MenuState)
            {
                case EMenuState.Default:
                    if (m_Player.IsSwallowed())
                        ShowUISwallowed(GameUI.Instance);
                    else if (m_Player.IsGrappling())
                        ShowUIGrappling(GameUI.Instance);
                    else
                        ShowUIDefault(GameUI.Instance);
                    break;

                case EMenuState.SelectTarget:
                    ShowUITargetSelect(GameUI.Instance);
                    break;

                case EMenuState.SelectItem:
                    ShowUIItemSelect(GameUI.Instance);
                    break;
            }
        }

        /// <summary>
        /// Return all other participants that are not dead, and hostile to the input participant
        /// </summary>
        /// <param name="attacker">The Participant from whose perspective other Participants are evaluated.</param>
        private IEnumerable<Participant> GetLiveOpponents(Participant attacker)
        {
            return Session.Participants.Where(candidate =>
                candidate != attacker &&
                !candidate.Character.IsDead() &&
                candidate.Character.IsAlly != attacker.Character.IsAlly);
        }

        /// <summary>
        /// Returns participants that the input participant can attack on this turn.
        /// </summary>
        private IEnumerable<Participant> GetViableAttackTargets(Participant attacker)
        {
            return GetLiveOpponents(attacker)
                .Where(candidate => !candidate.IsGrappling())
                .Where(candidate => !candidate.IsSwallowed());
        }

        /// <summary>
        /// Returns participants that the input participant can attempt to grapple with this turn.
        /// </summary> 
        private IEnumerable<Participant> GetViableGrappleTargets(Participant initiator)
        {
            return GetViableAttackTargets(initiator)
                .Where(target => initiator.Character.CanGrapple(target.Character));
        }

        /// <summary>
        /// Returns participants that the input participant can attempt to swallow on this turn.
        /// </summary>
        private IEnumerable<Participant> GetViablePreyTargets(Participant predator)
        {
            // If the predator is currently grappling, the only possible prey they can touch now is their grappling opponent
            if (predator.IsGrappling())
                return new List<Participant> { predator.GrapplingWith };

            // Otherwise, anyone will do
            return GetViableAttackTargets(predator)
                .Where(prey => predator.Character.CanSwallow(prey.Character));
        }

        /// <summary>
        /// Returns items from the player's inventory that can be used in combat.
        /// </summary>
        private IEnumerable<Item> GetUsableItems()
        {
            Debug.Assert(m_Player != null, "This function only makes sense if the player participates in combat");
            var player = (Player)m_Player.Character;
            return player.Inventory.Where(item => item.Asset.CanUseInBattle);
        }

        /// <summary>
        /// Returns an action to be taken by the input AI character.
        /// </summary>
        /// <param name="ai">The AI whose action to compute.</param>
        private CombatAction GetAIAction(Participant ai)
        {
            // If swallowed, characters can't do anything
            if (ai.IsSwallowed())
                return new CombatAction(ECombatAction.Prey_Struggle, null);

            // Handling grapples
            if (ai.IsGrappling())
            {
                if (ai.GrapplingInitiator)
                {
                    // Predators will try to swallow their prey at this point
                    if (ai.Character.IsPredator)
                        return new CombatAction(ECombatAction.Swallow, ai.GrapplingWith);

                    // Non-preds shouldn't have reached this since we don't support in-grapple attacks yet
                    Debug.Fail("Non-predator character is the grapple initiator, this should not be possible");
                    return new CombatAction(ECombatAction.SkipTurn, null);
                }
                else
                {
                    // Predators will always try to reverse, so that they can swallow their target.
                    // Everyone else will just try to escape.
                    return new CombatAction(ai.Character.IsPredator
                        ? ECombatAction.GrappleReverse
                        : ECombatAction.GrappleEscape, ai.GrapplingWith);
                }
            }

            // Try swallowing someone
            var prey = GetViablePreyTargets(ai).ToList();
            if (ai.Character.IsPredator && prey.Any() && CoreUtility.Rng.Next(100) < 60)
            {
                // Randomly choose to either pin the prey down, or just begin gulping right away
                var intent = CoreUtility.Rng.Next(100) < 50
                    ? ECombatAction.Swallow
                    : ECombatAction.GrappleInitiate;

                // Gulp an eligible prey
                var selected_prey = prey[CoreUtility.Rng.Next(prey.Count)];
                return new CombatAction(intent, selected_prey);
            }

            // Otherwise, we'll need to use a default attack. Look for targets.
            var targets = GetViableAttackTargets(ai).ToList();

            // No targets = nothing to do at all
            if (!targets.Any())
                return new CombatAction(ECombatAction.SkipTurn, null);

            // Otherwise, select a random target to attack
            var target = targets[CoreUtility.Rng.Next(targets.Count)];
            return new CombatAction(target != null ? ECombatAction.Attack : ECombatAction.SkipTurn, target);
        }

        /// <summary>
        /// Handle player input, given a current menu state and the choice number (the clicked button ID).
        /// </summary>
        /// <param name="choice">The button number that was clicked.</param>
        private void HandlePlayerInput(int choice)
        {
            // No choice to handle if the player is not participating at all
            Debug.Assert(m_Player != null);
            ECombatAction action = (ECombatAction)choice;

            // Handle button tree
            switch (m_MenuState)
            {
                case EMenuState.Default:
                    HandlePlayerInputDefault(action);
                    break;

                case EMenuState.SelectTarget:
                    HandlePlayerInputSelectTarget(choice);
                    break;

                case EMenuState.SelectItem:
                    HandlePlayerInputSelectItem(choice);
                    break;
            }
        }

        /// <summary>
        /// Handles target select checking.
        /// </summary>
        /// <remarks>
        /// If multiple targets are available, changes state to a target select submenu; otherwise, selects the one available target.
        /// </remarks>
        private void BeginPlayerPotentialTargetSelect(ECombatAction intent)
        {
            Debug.Assert(m_PotentialPlayerTargets.Count != 0);

            if (m_PotentialPlayerTargets.Count == 1)
            {
                // If only one target exists, pick it automatically; no need to show a submenu.
                m_PlayerDecision = new CombatAction(intent, m_PotentialPlayerTargets[0]);
            }
            else
            {
                // Otherwise, show a menu where the player can choose.
                m_PendingPlayerDecision = intent;
                m_MenuState = EMenuState.SelectTarget;
            }
        }

        /// <summary>
        /// Handle player input for the default UI state.
        /// </summary>
        private void HandlePlayerInputDefault(ECombatAction action)
        {
            switch (action)
            {
                case ECombatAction.Attack:
                    m_PotentialPlayerTargets = GetViableAttackTargets(m_Player).ToList();
                    BeginPlayerPotentialTargetSelect(ECombatAction.Attack);
                    break;

                case ECombatAction.GrappleInitiate:
                    m_PotentialPlayerTargets = GetViableGrappleTargets(m_Player).ToList();
                    BeginPlayerPotentialTargetSelect(ECombatAction.GrappleInitiate);
                    break;

                case ECombatAction.Swallow:
                    m_PotentialPlayerTargets = GetViablePreyTargets(m_Player).ToList();
                    BeginPlayerPotentialTargetSelect(ECombatAction.Swallow);
                    break;

                case ECombatAction.Item:
                    m_MenuState = EMenuState.SelectItem;
                    break;

                case ECombatAction.GrappleReverse:
                case ECombatAction.GrappleEscape:
                case ECombatAction.GrappleRelease:
                    // Perform the action with the grapple partner
                    m_PlayerDecision = new CombatAction(action, m_Player.GrapplingWith);
                    break;

                default:
                    // No special logic is required, just copy over the intent directly and move on to the round step
                    m_PlayerDecision = new CombatAction(action, null);
                    break;
            }
        }

        /// <summary>
        /// Handle player input for the target selection state.
        /// </summary>
        private void HandlePlayerInputSelectTarget(int choice)
        {
            // Always return to main state
            m_MenuState = EMenuState.Default;

            // Back button
            if (choice == 0)
                return;

            // Select the desired target
            m_PlayerDecision = new CombatAction(m_PendingPlayerDecision, m_PotentialPlayerTargets[choice - 1]);
        }

        /// <summary>
        /// Handle player input for the item selection state.
        /// </summary>
        private void HandlePlayerInputSelectItem(int choice)
        {
            // Always return to main state
            m_MenuState = EMenuState.Default;

            // Back button
            if (choice == 0)
                return;

            // Set a dummy action
            m_PlayerDecision = new CombatAction(ECombatAction.Item, null);

            // Use the item now
            Item item = m_PotentialPlayerItems[choice - 1];
            ItemUtilities.UseItem(GameController.Session, item);
        }

        /// <summary>
        /// Advances rounds until the player can make a turn choice again, or the combat ends.
        /// </summary>
        private void StepRound()
        {
            while (true)
            {
                // Prepare a new participant list if we don't already have one cached. A list may be cached if the round was
                // interrupted because the player needed to select a combat action.
                if (m_RoundParticipants == null)
                {
                    // Make a copy of the participants list so it can be edited by combat callbacks etc
                    m_RoundParticipants = Session.Participants.ToArray();
                    m_RoundStepIndex = 0;
                }

                // Step all participants
                for (int i = m_RoundStepIndex; i < m_RoundParticipants.Length; i++)
                {
                    Participant participant = m_RoundParticipants[i];

                    // Skip downed or immobilized participants
                    if (!participant.CanAct())
                        continue;

                    // Show this participant as having the turn
                    Session.WhoseTurn = participant;

                    if (m_Player != null && participant == m_Player)
                    {
                        // If the player has not yet selected an action, pause the round now and present UI
                        if (!m_PlayerDecision.IsValid())
                        {
                            // Save the step index so we can resume stepping the round once the player has selected an action
                            m_RoundStepIndex = i;
                            return;
                        }

                        // Run player action
                        StepParticipant(participant, m_PlayerDecision.Action, m_PlayerDecision.Target);

                        // Invalidate the player action so they can select a new one next round
                        m_PlayerDecision = new CombatAction();
                    }
                    else
                    {
                        // Run AI action
                        CombatAction decision = GetAIAction(participant);
                        StepParticipant(participant, decision.Action, decision.Target);
                    }

                    // Optionally terminate the round cycle at the end of each turn if needed
                    if (IsCombatEnded())
                        return;
                }

                // Post-round cleanup
                CombatLogic.PostRound(Session);
                m_RoundParticipants = null;
            }
        }

        /// <summary>
        /// Run an individual participant's turn.
        /// </summary>
        /// <param name="instigator">The participant whose turn it is.</param>
        /// <param name="action">The action being taken by the participant.</param>
        /// <param name="target">The target of the participant's action, or null if irrelevant.</param>
        private void StepParticipant(Participant instigator, ECombatAction action, Participant target)
        {
            // Dispatch selected action
            switch (action)
            {
                case ECombatAction.Attack:
                    CombatLogic.PerformAttack(instigator, target);
                    break;
                case ECombatAction.Item:
                    // No action; item has already been used directly from the menu
                    break;
                case ECombatAction.GrappleInitiate:
                    CombatLogic.PerformGrappleInitiate(instigator, target);
                    break;
                case ECombatAction.GrappleEscape:
                    CombatLogic.PerformGrappleEscape(instigator, target);
                    break;
                case ECombatAction.GrappleReverse:
                    CombatLogic.PerformGrappleReverse(instigator, target);
                    break;
                case ECombatAction.GrappleRelease:
                    CombatLogic.PerformGrappleRelease(instigator, target);
                    break;
                case ECombatAction.Swallow:
                    CombatLogic.PerformVore(instigator, target);
                    break;
                case ECombatAction.SkipTurn:
                    CombatLogic.PerformSkipTurn(instigator);
                    break;
                case ECombatAction.Prey_Struggle:
                    CombatLogic.PerformPreyStruggle(instigator.Predator, instigator);
                    break;
                case ECombatAction.Prey_Submit:
                    // No action
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action));
            }

            // Avoid running any further combat logic if the combat has already ended at this point
            if (IsCombatEnded())
                return;

            // Post-turn actions
            CombatLogic.PostTurn(instigator);
        }

        private bool IsCombatEnded()
        {
            // Immediately end combat when script requests it
            if (m_IsEnded)
                return true;

            // If player died, combat always ends
            if (m_Player != null && m_Player.Character.IsDead())
                return true;

            // Find the first living ally. If there are none, then there are no more opposing characters, so combat ends
            Participant ally = Session.Participants.FirstOrDefault(p => !p.Character.IsDead() && p.Character.IsAlly);
            if (ally == null)
                return true;

            // Otherwise, there must also be opponents
            return !GetLiveOpponents(ally).Any();
        }

        private bool IsPlayerInputRequired()
        {
            // Player input is required if the player is participating, and the round was paused because they have
            // not yet selected a valid action to perform this round.
            return m_Player != null && m_RoundParticipants != null && !m_PlayerDecision.IsValid();
        }

        private void ShowUIDefault(GameUI ui)
        {
            ui.Instruction = "What will you do?";

            // Direct attack
            var attack_targets = GetViableAttackTargets(m_Player);
            if (attack_targets.Any())
            {
                ui.AddButton(new ChoiceButtonModel
                {
                    Choice = (int)ECombatAction.Attack,
                    Label = "Fight",
                    Tooltip = "Attack another character to deal damage."
                });
            }

            // Grapple
            var grapple_targets = GetViableGrappleTargets(m_Player);
            if (grapple_targets.Any())
            {
                ui.AddButton(new ChoiceButtonModel
                {
                    Choice = (int)ECombatAction.GrappleInitiate,
                    Label = "Grapple",
                    Tooltip = "Try to restrain another character, to make them more vulnerable."
                });
            }

            // Vore!
            var vore_targets = GetViablePreyTargets(m_Player);
            if (vore_targets.Any())
                ui.AddButton(new ChoiceButtonModel
                {
                    Choice = (int)ECombatAction.Swallow,
                    Label = "Swallow",
                    Tooltip = "Attempt to devour another character."
                });

            // Inventory items
            m_PotentialPlayerItems = GetUsableItems().ToList();
            if (m_PotentialPlayerItems.Count > 0)
            {
                ui.AddButton(new ChoiceButtonModel
                {
                    Choice = (int)ECombatAction.Item,
                    Label = "Item",
                    Tooltip = "Use an item."
                });
            }

            // Misc options
            if (m_Player.Prey.Count != 0 && m_Player.Character.PredatorDigests)
            {
                ui.AddButton(new ChoiceButtonModel
                {
                    Choice = (int)ECombatAction.SkipTurn,
                    Label = "Digest",
                    Tooltip = "End your turn and continue digesting your prey."
                });
            }
            else
            {
                ui.AddButton(new ChoiceButtonModel
                {
                    Choice = (int)ECombatAction.SkipTurn,
                    Label = "Wait",
                    Tooltip = "End your turn."
                });
            }
        }

        private void ShowUITargetSelect(GameUI ui)
        {
            ui.AddButton(new ChoiceButtonModel { Choice = 0, Label = "(< Back)", Width = 0.5f });

            for (int i = 0; i < m_PotentialPlayerTargets.Count; i++)
                ui.AddButton(new ChoiceButtonModel { Choice = i + 1, Label = m_PotentialPlayerTargets[i].Character.Name });
        }

        private void ShowUIItemSelect(GameUI ui)
        {
            ui.AddButton(new ChoiceButtonModel { Choice = 0, Label = "(< Back)", Width = 0.5f });

            for (int i = 0; i < m_PotentialPlayerItems.Count; i++)
                ui.AddButton(new ChoiceButtonModel { Choice = i + 1, Label = m_PotentialPlayerItems[i].Asset.ObjectName });
        }

        private void ShowUISwallowed(GameUI ui)
        {
            ui.AddButton(new ChoiceButtonModel
            {
                Choice = (int)ECombatAction.Prey_Struggle,
                Label = "Struggle",
                Tooltip = "Resist your predation."
            });
            ui.AddButton(new ChoiceButtonModel
            {
                Choice = (int)ECombatAction.Prey_Submit,
                Label = "Submit",
                Tooltip = "Relax in your slimy embrace."
            });

            // Inventory items
            m_PotentialPlayerItems = GetUsableItems().ToList();
            if (m_PotentialPlayerItems.Count > 0)
            {
                ui.AddButton(new ChoiceButtonModel
                {
                    Choice = (int)ECombatAction.Item,
                    Label = "Item",
                    Tooltip = "Use an item."
                });
            }
        }

        private void ShowUIGrappling(GameUI ui)
        {
            // Only the grapple initiator may try swallowing the victim; and only the victim can try reversing it.
            if (m_Player.GrapplingInitiator)
            {
                // Player is on top
                ui.Instruction = $"You're pinning down {m_Player.GrapplingWith.Character.Name}. What will you do?";

                ui.AddButton(new ChoiceButtonModel
                {
                    Choice = (int)ECombatAction.Swallow,
                    Label = "Swallow",
                    Tooltip = "Attempt to devour your pinned prey."
                });
                ui.AddButton(new ChoiceButtonModel
                {
                    Choice = (int)ECombatAction.GrappleRelease,
                    Label = "Release",
                    Tooltip = "Let your victim go."
                });
            }
            else
            {
                // Player is being pinned
                ui.Instruction = $"You're being pinned down by {m_Player.GrapplingWith.Character.Name}! What will you do?";

                // If player cannot start the grapple against larger target, make sure they cannot reverse it either.
                if (m_Player.Character.CanGrapple(m_Player.GrapplingWith.Character))
                {
                    ui.AddButton(new ChoiceButtonModel
                    {
                        Choice = (int)ECombatAction.GrappleReverse,
                        Label = "Reverse",
                        Tooltip = "Attempt to reverse the grapple, so you end up on top."
                    });
                }
                ui.AddButton(new ChoiceButtonModel
                {
                    Choice = (int)ECombatAction.GrappleEscape,
                    Label = "Escape",
                    Tooltip = "Attempt to escape from the grapple."
                });
            }

            // Can always skip turn if desired
            ui.AddButton(new ChoiceButtonModel
            {
                Choice = (int)ECombatAction.SkipTurn,
                Label = "Wait",
                Tooltip = "End your turn."
            });
        }

        /// <summary>
        /// Describes the current UI state, and how to respond to turns (button presses).
        /// </summary>
        private enum EMenuState
        {
            Default,
            SelectTarget,
            SelectItem
        }

        /// <summary>
        /// Describes what kind of action a character takes.
        /// </summary>
        private enum ECombatAction
        {
            Invalid,
            SkipTurn,
            Attack,
            GrappleInitiate,
            GrappleEscape,
            GrappleReverse,
            GrappleRelease,
            Item,
            Swallow,
            Prey_Struggle,
            Prey_Submit
        }

        private readonly struct CombatAction
        {

            public CombatAction(ECombatAction action, Participant target)
            {
                Action = action;
                Target = target;
            }

            public ECombatAction Action { get; }
            public Participant Target { get; }

            public bool IsValid()
            {
                return Action != ECombatAction.Invalid;
            }

        }

    }

}
