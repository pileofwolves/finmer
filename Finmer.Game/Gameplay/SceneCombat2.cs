/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
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

        private Participant m_Player;

        private EMenuState m_MenuState = EMenuState.Default;
        private CombatAction m_PlayerDecision;

        private ECombatAction m_PendingPlayerDecision;
        private List<Participant> m_PotentialPlayerTargets;
        private List<Item> m_PotentialPlayerItems;
        private bool m_IsFirstTurn = true;
        private bool m_IsEnded = false;

        public SceneCombat2(CombatSession session)
        {
            Session = session;
        }

        public override void Enter()
        {
            // Cache the Player participant, so we can easily reference it during the UI stage
            m_Player = Session.Participants.FirstOrDefault(participant => participant.IsPlayer());

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
            // Process player input to advance to different submenus/states
            bool can_run_round = true;
            if (IsPlayerInputRequired())
            {
                // Skip the first turn, because the combat just begun and we have no player action yet, but we do need to show the buttons
                if (m_IsFirstTurn)
                {
                    Debug.Assert(choice == 0);
                    m_IsFirstTurn = false;
                    PrepareInterface();
                    return;
                }

                HandlePlayerInput(choice);
                can_run_round = m_MenuState == EMenuState.Default && m_PlayerDecision.IsValid();
            }

            // Avoid running combat logic when opening a submenu
            if (can_run_round)
            {
                // If we reach this point in logic, then the player has finalized their choice, and we can run the combat round
                StepRound();

                // If the combat is over, get rid of the combat scene so we return to game scripts
                if (IsCombatEnded())
                {
                    ExitCombat();
                    return;
                }
            }

            // Show UI
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
                    else
                        ShowUIDefault(GameUI.Instance);
                    break;

                case EMenuState.SelectTarget:
                    ShowUITargetSelect(GameUI.Instance);
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
                candidate.Character.IsAlly() != attacker.Character.IsAlly());
        }

        /// <summary>
        /// Returns participants that the input participant can attack on this turn.
        /// </summary>
        private IEnumerable<Participant> GetViableAttackTargets(Participant attacker)
        {
            return GetLiveOpponents(attacker)
                .Where(candidate => !candidate.IsSwallowed());
        }

        /// <summary>
        /// Returns participants that the input participant can attempt to swallow on this turn.
        /// </summary>
        private IEnumerable<Participant> GetViablePreyTargets(Participant predator)
        {
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

            // Try swallowing someone
            var prey = GetViablePreyTargets(ai).ToList();
            if (ai.Character.IsPredator && prey.Any() && CoreUtility.Rng.Next(100) < 60)
            {
                // Gulp an eligible prey
                var selected_prey = prey.FirstOrDefault();
                return new CombatAction(ECombatAction.Swallow, selected_prey);
            }

            // TODO, just attack player for prototype
            var targets = GetViableAttackTargets(ai);
            var target = targets.FirstOrDefault();
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
            }

            // Validate handler output - must either have an action selected, or have a submenu open
            Debug.Assert(m_PlayerDecision.IsValid() || m_MenuState != EMenuState.Default, "Player input did not result in a valid action");
        }

        /// <summary>
        /// Handle player input for the default UI state.
        /// </summary>
        private void HandlePlayerInputDefault(ECombatAction action)
        {
            switch (action)
            {
                case ECombatAction.Attack:
                    // If only one target exists, pick it automatically. Otherwise, show a menu where the player can choose.
                    m_PotentialPlayerTargets = GetViableAttackTargets(m_Player).ToList();
                    if (m_PotentialPlayerTargets.Count == 1)
                    {
                        m_PlayerDecision = new CombatAction(ECombatAction.Attack, m_PotentialPlayerTargets[0]);
                    }
                    else
                    {
                        m_PendingPlayerDecision = ECombatAction.Attack;
                        m_MenuState = EMenuState.SelectTarget;
                    }
                    break;

                case ECombatAction.Swallow:
                    // If only one target exists, pick it automatically. Otherwise, show a menu where the player can choose.
                    m_PotentialPlayerTargets = GetViablePreyTargets(m_Player).ToList();
                    if (m_PotentialPlayerTargets.Count == 1)
                    {
                        m_PlayerDecision = new CombatAction(ECombatAction.Swallow, m_PotentialPlayerTargets[0]);
                    }
                    else
                    {
                        m_PendingPlayerDecision = ECombatAction.Swallow;
                        m_MenuState = EMenuState.SelectTarget;
                    }
                    break;

                default:
                    m_PlayerDecision = new CombatAction(action, null);
                    break;
            }
        }

        /// <summary>
        /// Handle player input for the target selection state.
        /// </summary>
        private void HandlePlayerInputSelectTarget(int choice)
        {
            // Back button
            if (choice == 0)
            {
                m_MenuState = EMenuState.Default;
                return;
            }

            // Otherwise, select target
            m_MenuState = EMenuState.Default;
            m_PlayerDecision = new CombatAction(m_PendingPlayerDecision, m_PotentialPlayerTargets[choice - 1]);
        }

        /// <summary>
        /// Advances rounds until the player can make a turn choice again, or the combat ends.
        /// </summary>
        private void StepRound()
        {
            while (true)
            {
                // Make a copy of the participants list so it can be edited by combat callbacks etc
                Participant[] participants = Session.Participants.ToArray();

                foreach (Participant participant in participants)
                {
                    // Skip downed or immobilized participants
                    if (!participant.CanAct())
                        continue;

                    if (m_Player != null && participant == m_Player)
                    {
                        // Run player action
                        Debug.Assert(m_PlayerDecision.IsValid());
                        StepParticipant(participant, m_PlayerDecision.Action, m_PlayerDecision.Target);

                        // Invalidate the player action so they can select a new one
                        m_PlayerDecision = new CombatAction();
                    }
                    else
                    {
                        // Run AI action
                        CombatAction decision = GetAIAction(participant);
                        StepParticipant(participant, decision.Action, decision.Target);
                    }

                    // Optionally terminate the round at the end of each turn
                    if (IsCombatEnded())
                        return;
                }

                // Post-round cleanup
                CombatLogic.PostRound(Session);

                // At the end of the round, if the player is part of the combat, exit this loop to allow them to pick their action.
                // This construction may look a bit weird, but it enables support for combats involving only NPCs. (Without this,
                // the player would get offered a choice in UI, which is then ignored because the player doesn't actually participate.)
                if (IsPlayerInputRequired())
                    break;
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
                case ECombatAction.Swallow:
                    CombatLogic.PerformVore(instigator, target);
                    break;
                case ECombatAction.SkipTurn:
                    CombatLogic.PerformSkipTurn(instigator);
                    break;
                case ECombatAction.Prey_Struggle:
                    CombatLogic.PerformPreyStruggle(instigator);
                    break;
                case ECombatAction.Prey_Submit:
                    // No action
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action));
            }

            // Post-turn actions
            CombatLogic.PostTurn(instigator);
        }

        private bool IsCombatEnded()
        {
            // The combat ends when script requests it, or there are no more living enemies, or the player died
            return m_IsEnded || m_Player.Character.IsDead() || !GetLiveOpponents(m_Player).Any();
        }

        private bool IsPlayerInputRequired()
        {
            return m_Player != null && !m_Player.Character.IsDead();
        }

        private void ShowUIDefault(GameUI ui)
        {
            ui.Instruction = "What will you do?";

            // Direct attack
            var attack_targets = GetViableAttackTargets(m_Player);
            if (attack_targets.Any())
                ui.AddButton(new ChoiceButtonModel { Choice = (int)ECombatAction.Attack, Label = "Fight" });

            // Vore!
            var vore_targets = GetViablePreyTargets(m_Player);
            if (vore_targets.Any())
                ui.AddButton(new ChoiceButtonModel { Choice = (int)ECombatAction.Swallow, Label = "Swallow" });

            // Inventory items
            //m_PotentialPlayerItems = GetUsableItems().ToList();
            //if (m_PotentialPlayerItems.Count > 0)
            //    ui.AddButton(new ChoiceButtonModel { Choice = (int)ECombatAction.Item, Label = "Item" });

            // Misc options
            if (m_Player.Prey.Count != 0 && m_Player.Character.StomachDigest)
                ui.AddButton(new ChoiceButtonModel { Choice = (int)ECombatAction.SkipTurn, Label = "Digest" });
            else
                ui.AddButton(new ChoiceButtonModel { Choice = (int)ECombatAction.SkipTurn, Label = "Wait" });
        }

        private void ShowUITargetSelect(GameUI ui)
        {
            ui.AddButton(new ChoiceButtonModel { Choice = 0, Label = "(< Back)", Width = 0.5f });

            for (int i = 0; i < m_PotentialPlayerTargets.Count; i++)
                ui.AddButton(new ChoiceButtonModel { Choice = i + 1, Label = m_PotentialPlayerTargets[i].Character.Name });
        }

        private void ShowUISwallowed(GameUI ui)
        {
            ui.AddButton(new ChoiceButtonModel { Choice = (int)ECombatAction.Prey_Struggle, Label = "Struggle", Tooltip = "Resist your predation." });
            ui.AddButton(new ChoiceButtonModel { Choice = (int)ECombatAction.Prey_Submit, Label = "Submit", Tooltip = "Relax in your slimy embrace." });
        }

        /// <summary>
        /// Describes the current UI state, and how to respond to turns (button presses).
        /// </summary>
        private enum EMenuState
        {
            Default,
            SelectTarget
        }

        /// <summary>
        /// Describes what kind of action a character takes.
        /// </summary>
        private enum ECombatAction
        {
            Invalid,
            SkipTurn,
            Attack,
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
