﻿/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Finmer.Core;
using Finmer.Gameplay.Scripting;
using Finmer.Models;
using Finmer.Utility;

namespace Finmer.Gameplay.Combat
{

    /// <summary>
    /// Contains the state for a V2 combat instance.
    /// </summary>
    public class CombatSession : ScriptableObject
    {

        private const string k_CallbackName_OnRoundStart = "OnRoundStart";
        private const string k_CallbackName_OnRoundEnd = "OnRoundEnd";
        private const string k_CallbackName_OnCombatEnd = "OnCombatEnd";
        private const string k_CallbackName_OnCharacterKilled = "OnCreatureKilled";
        private const string k_CallbackName_OnCharacterVored = "OnCreatureVored";
        private const string k_CallbackName_OnCharacterReleased = "OnCreatureReleased";
        private const string k_CallbackName_OnPlayerKilled = "OnPlayerKilled";

        private readonly ScriptCallbackTable m_Binder;
        private Participant m_WhoseTurn;
        private int m_Round;

        /// <summary>
        /// Collection of participating characters in this combat.
        /// </summary>
        public List<Participant> Participants { get; } = new List<Participant>();

        /// <summary>
        /// The combat turn number. Starts at 1 for the first round.
        /// </summary>
        [ScriptableProperty(EScriptAccess.Read)]
        public int Round
        {
            get => m_Round;
            set
            {
                if (m_Round != 0)
                    OnRoundEnd?.Invoke(m_Round);

                m_Round = value;
                OnRoundStart?.Invoke(m_Round);
            }
        }

        /// <summary>
        /// The amount of XP to award to the player at the end of combat.
        /// </summary>
        public int TotalXPAward { get; set; }

        /// <summary>
        /// Gets or sets the participant who currently has the active turn in the combat system.
        /// Used for display purposes, not combat logic.
        /// </summary>
        public Participant WhoseTurn
        {
            get => m_WhoseTurn;
            set
            {
                m_WhoseTurn = value;
                OnPropertyChanged();
            }
        }

        public CombatSession(ScriptContext context) : base(context)
        {
            m_Binder = new ScriptCallbackTable(context);

            AttachScriptCallbacks();
        }

        /// <summary>
        /// Returns the CombatSession instance of the currently active combat, or null if unavailable.
        /// </summary>
        public static CombatSession GetActiveSession()
        {
            return (GameController.Session.PeekScene() as SceneCombat2)?.Session;
        }

        /// <summary>
        /// Add a Character to this combat session as a new participant.
        /// </summary>
        /// <param name="character">The character to register. Characters may only be registered once.</param>
        public void AddParticipant(Character character)
        {
            // Register the character as a new participant
            Debug.Assert(Participants.All(p => p.Character != character), "Character is already registered");
            var participant = new Participant(character, this);
            Participants.Add(participant);

            // If this is the player, transfer any pending buffs to this combat session
            if (character is Player player)
            {
                foreach (var pending in player.PendingBuffs)
                    participant.ApplyPendingBuff(pending);
                player.PendingBuffs.Clear();
            }
        }

        public void SetVored(Participant predator, Participant prey)
        {
            Debug.Assert(!predator.Prey.Contains(prey), "Predator already has prey marked as swallowed");
            Debug.Assert(prey.Predator == null, "Prey already has a predator set");

            // Automatically remove grapple status since the characters cannot be in both states
            if (predator.IsGrappling())
                UnsetGrappling(predator, prey);

            // Link the predator and prey
            predator.Prey.Add(prey);
            prey.Predator = predator;

            // Give one turn of damage immunity, since that looks/plays better
            prey.DigestionImmunity = true;

            // Update display state
            predator.UpdateDisplay();
            prey.UpdateDisplay();

            // Increment Swallow count if the player ate victim
            if (predator.Character is Player player)
            {
                player.TotalPreySwallowed++;
            }

            // Run script callback
            OnCharacterVored?.Invoke(predator, prey);
        }

        public void UnsetVored(Participant predator, Participant prey)
        {
            Debug.Assert(predator.Prey.Contains(prey), "Predator does not have prey marked as swallowed");
            Debug.Assert(prey.Predator == predator, "Prey was not swallowed by expected predator");

            // Unlink the predator and prey
            predator.Prey.Remove(prey);
            prey.Predator = null;

            // Update display state
            predator.UpdateDisplay();
            prey.UpdateDisplay();

            // Run script callback
            OnCharacterReleased?.Invoke(predator, prey);
        }

        public void SetGrappling(Participant instigator, Participant target)
        {
            Debug.Assert(instigator.GrapplingWith == null, "Grappler is already grappling someone");
            Debug.Assert(target.GrapplingWith == null, "Grapplee is already grappling someone");

            // Link the two participants together
            instigator.GrapplingWith = target;
            target.GrapplingWith = instigator;
            instigator.GrapplingInitiator = true;
            target.GrapplingInitiator = false;

            // Update display state
            instigator.UpdateDisplay();
            target.UpdateDisplay();
        }

        public void UnsetGrappling(Participant a, Participant b)
        {
            Debug.Assert(a.GrapplingWith == b, "Grappler is not grappling the expected participant");
            Debug.Assert(b.GrapplingWith == a, "Grapplee is not grappling the expected participant");

            // Unlink the participants
            a.GrapplingWith = null;
            b.GrapplingWith = null;

            // Update display state
            a.UpdateDisplay();
            b.UpdateDisplay();
        }

        /// <summary>
        /// Notify event listeners that this combat session has terminated.
        /// </summary>
        public void NotifyCombatEnded()
        {
            OnCombatEnd?.Invoke();
        }

        /// <summary>
        /// Notify event listeners that a participant lost all HP.
        /// </summary>
        public void NotifyParticipantKilled(Participant killer, Participant victim)
        {
            var victim_character = victim.Character;
            Debug.Assert(victim_character.IsDead());

            // Invoke the appropriate callback
            if (victim.IsPlayer())
                OnPlayerKilled?.Invoke(killer, victim);
            else
                OnCharacterKilled?.Invoke(killer, victim);

            // Check again whether the character is still dead - we allow the above callback to revive them
            if (!victim_character.IsDead())
                return;

            // Remove any buffs from the victim so they do not have special logic ticking every turn
            victim.LocalBuffs.Clear();

            // If the victim was grappling with someone, remove those states
            var grapple_partner = victim.GrapplingWith;
            if (grapple_partner != null)
                victim.Session.UnsetGrappling(victim, grapple_partner);

            // Regurgitate prey that hasn't been killed yet
            if (!victim.IsPlayer())
            {
                foreach (var prey in victim.Prey)
                {
                    // Chyme stays chyme, sorry
                    if (prey.Character.IsDead())
                        continue;

                    // Release victim
                    victim.Session.UnsetVored(victim, prey);
                    CombatDisplay.ShowSimpleMessage(victim.Character.PredatorDigests ? @"vore_release_kill_digest" : @"vore_release_kill_endo", victim, prey);
                }
            }

            // If the victim was not swallowed, there may be auto-vore triggers we need to activate, check now
            if (!victim.IsSwallowed())
                CombatLogic.HandleAutoVore(victim);

            // Now that the killer/victim relation is finalized, handle it.
            // Note that we must check swallowed state again here because any auto-vore triggers may have changed it.
            if (!victim.IsSwallowed())
            {
                // This was a regular kill
                CombatDisplay.ShowSimpleMessage(@"kill_generic", killer, victim);
            }
            else
            {
                // This was a vore kill
                // Note that the killer could differ from the predator if an auto-vore path was used earlier.
                killer = victim.Predator;
                CombatDisplay.ShowSimpleMessage(@"kill_digested", killer, victim);

                // Increment vore stats if the player was the predator
                if (killer.Character is Player player)
                    player.TotalPreyDigested++; 
                
                // Optional disposal scene if available and enabled by the user
                var predator_asset = killer.Character.Asset;
                if (predator_asset != null && predator_asset.PredatorDisposal && UserConfig.AllowExplicitDisposal)
                {
                    CombatDisplay.ShowPause();
                    CombatDisplay.ShowSimpleMessage(@"vore_disposal", killer, victim);
                }
            }

            // If the victim was an enemy, award XP to the player
            if (!victim_character.IsAlly && !victim_character.Flags.HasFlag(ECharacterFlags.NoXP))
            {
                // Store the XP for later, so we can award it all at once at the end of the combat. This prevents the player from
                // leveling up mid-combat, which looks odd and doesn't help the player since they can't spend points yet anyway.
                int xp = CombatLogic.CalculateXP(victim_character);
                TotalXPAward += xp;
            }
        }

        [ScriptableFunction]
        protected static int ExportedAddParticipant(IntPtr state)
        {
            var self = FromLuaNonOptional<CombatSession>(state, 1);
            var character = FromLuaNonOptional<Character>(state, 2);
            self.AddParticipant(character);
            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedBegin(IntPtr state)
        {
            var self = FromLuaNonOptional<CombatSession>(state, 1);
            var session = GameController.Session;

            // Start combat
            session.PushScene(new SceneCombat2(self));

            // Pause script until combat exits
            return LuaApi.lua_yield(state, 0);
        }

        [ScriptableFunction]
        protected static int ExportedEnd(IntPtr state)
        {
            var self = FromLuaNonOptional<CombatSession>(state, 1);
            var game_session = GameController.Session;

            // Validate that a combat session is actually running
            var scene = game_session.PeekScene() as SceneCombat2;
            if (scene == null)
                return LuaApi.luaL_error(state, "no active combat to end");

            // Validate that this is the same combat
            if (scene.Session != self)
                return LuaApi.luaL_error(state, "another combat is active, call End() on the correct instance instead");

            // Terminate the session
            scene.RequestExit();

            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedIsSwallowed(IntPtr state)
        {
            // Retrieve and validate the participant from the stack
            var participant = GetValidatedParticipant(state);

            // Check the Participant's state
            LuaApi.lua_pushboolean(state, participant.IsSwallowed());
            return 1;
        }

        [ScriptableFunction]
        protected static int ExportedIsGrappling(IntPtr state)
        {
            // Retrieve and validate the participant from the stack
            var participant = GetValidatedParticipant(state);

            // Check the Participant's state
            LuaApi.lua_pushboolean(state, participant.IsGrappling());
            return 1;
        }

        [ScriptableFunction]
        protected static int ExportedIsGrappleInitiator(IntPtr state)
        {
            // Retrieve and validate the participant from the stack
            var participant = GetValidatedParticipant(state);

            // Check the Participant's state
            LuaApi.lua_pushboolean(state, participant.GrapplingInitiator);
            return 1;
        }

        [ScriptableFunction]
        protected static int ExportedGetPredator(IntPtr state)
        {
            // Retrieve and validate the participant from the stack
            var participant = GetValidatedParticipant(state);

            // Push the Predator field
            var predator = participant.Predator;
            if (predator == null)
                LuaApi.lua_pushnil(state);
            else
                predator.Character.PushToLua(state);

            return 1;
        }

        [ScriptableFunction]
        protected static int ExportedGetGrapplingWith(IntPtr state)
        {
            // Retrieve and validate the participant from the stack
            var participant = GetValidatedParticipant(state);

            // Push the GrapplingWith field
            var grapple_partner = participant.GrapplingWith;
            if (grapple_partner == null)
                LuaApi.lua_pushnil(state);
            else
                grapple_partner.Character.PushToLua(state);

            return 1;
        }

        [ScriptableFunction]
        protected static int ExportedApplyBuff(IntPtr state)
        {
            // Retrieve and validate the participant and desired buff from the stack
            var participant = GetValidatedParticipant(state);
            var buff = FromLuaNonOptional<PendingBuff>(state, 3);

            // Add the buff to the participant
            participant.ApplyPendingBuff( buff);

            return 1;
        }

        [ScriptableFunction]
        protected static int ExportedOnRoundStart(IntPtr state)
        {
            // Keep the function around so the callback can find it
            var self = FromLuaNonOptional<CombatSession>(state, 1);
            self.m_Binder.Bind(state, k_CallbackName_OnRoundStart);

            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedOnRoundEnd(IntPtr state)
        {
            // Keep the function around so the callback can find it
            var self = FromLuaNonOptional<CombatSession>(state, 1);
            self.m_Binder.Bind(state, k_CallbackName_OnRoundEnd);

            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedOnCombatEnd(IntPtr state)
        {
            // Keep the function around so the callback can find it
            var self = FromLuaNonOptional<CombatSession>(state, 1);
            self.m_Binder.Bind(state, k_CallbackName_OnCombatEnd);

            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedOnCreatureKilled(IntPtr state)
        {
            // Keep the function around so the callback can find it
            var self = FromLuaNonOptional<CombatSession>(state, 1);
            self.m_Binder.Bind(state, k_CallbackName_OnCharacterKilled);

            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedOnPlayerKilled(IntPtr state)
        {
            // Keep the function around so the callback can find it
            var self = FromLuaNonOptional<CombatSession>(state, 1);
            self.m_Binder.Bind(state, k_CallbackName_OnPlayerKilled);

            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedOnCreatureVored(IntPtr state)
        {
            // Keep the function around so the callback can find it
            var self = FromLuaNonOptional<CombatSession>(state, 1);
            self.m_Binder.Bind(state, k_CallbackName_OnCharacterVored);

            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedOnCreatureReleased(IntPtr state)
        {
            // Keep the function around so the callback can find it
            var self = FromLuaNonOptional<CombatSession>(state, 1);
            self.m_Binder.Bind(state, k_CallbackName_OnCharacterReleased);

            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedSetGrappling(IntPtr state)
        {
            var self = FromLuaNonOptional<CombatSession>(state, 1);
            var lhs = self.GetParticipantForCharacter(FromLuaNonOptional<Character>(state, 2));
            var rhs = self.GetParticipantForCharacter(FromLuaNonOptional<Character>(state, 3));
            self.SetGrappling(lhs, rhs);
            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedSetVored(IntPtr state)
        {
            var self = FromLuaNonOptional<CombatSession>(state, 1);
            var lhs = self.GetParticipantForCharacter(FromLuaNonOptional<Character>(state, 2));
            var rhs = self.GetParticipantForCharacter(FromLuaNonOptional<Character>(state, 3));
            self.SetVored(lhs, rhs);
            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedUnsetGrappling(IntPtr state)
        {
            var self = FromLuaNonOptional<CombatSession>(state, 1);
            var lhs = self.GetParticipantForCharacter(FromLuaNonOptional<Character>(state, 2));
            var rhs = self.GetParticipantForCharacter(FromLuaNonOptional<Character>(state, 3));
            self.UnsetGrappling(lhs, rhs);
            return 0;
        }

        [ScriptableFunction]
        protected static int ExportedUnsetVored(IntPtr state)
        {
            var self = FromLuaNonOptional<CombatSession>(state, 1);
            var lhs = self.GetParticipantForCharacter(FromLuaNonOptional<Character>(state, 2));
            var rhs = self.GetParticipantForCharacter(FromLuaNonOptional<Character>(state, 3));
            self.UnsetVored(lhs, rhs);
            return 0;
        }

        private static Participant GetValidatedParticipant(IntPtr state)
        {
            var self = FromLuaNonOptional<CombatSession>(state, 1);
            var character = FromLuaNonOptional<Character>(state, 2);

            // Obtain the Participant associated with a particular Character
            var participant = self.GetParticipantForCharacter(character);
            if (participant == null)
                LuaApi.luaL_error(state, "creature is not a participant of this combat");

            return participant;
        }

        private void InternalProtectedCallback(IntPtr stack, IntPtr coroutine, int num_args)
        {
            try
            {
                // Run callback in protected mode
                m_Binder.Call(stack, coroutine, num_args);
            }
            catch (ScriptException ex)
            {
                // Write any errors to game log; upstream caller has no concept of the script callback so cannot handle them better
                GameUI.Instance.Log("ERROR: Script error in combat callback: " + ex.Message, Theme.LogColorError);
            }
        }

        private void AttachScriptCallbacks()
        {
            OnRoundStart += round =>
            {
                IntPtr stack = ScriptContext.LuaState;
                if (m_Binder.PrepareCall(stack, k_CallbackName_OnRoundStart, out var coroutine))
                {
                    LuaApi.lua_pushnumber(coroutine, round);
                    InternalProtectedCallback(stack, coroutine, 1);
                }
            };

            OnRoundEnd += round =>
            {
                IntPtr stack = ScriptContext.LuaState;
                if (m_Binder.PrepareCall(stack, k_CallbackName_OnRoundEnd, out var coroutine))
                {
                    LuaApi.lua_pushnumber(coroutine, round);
                    InternalProtectedCallback(stack, coroutine, 1);
                }
            };

            OnCombatEnd += () =>
            {
                IntPtr stack = ScriptContext.LuaState;
                if (m_Binder.PrepareCall(stack, k_CallbackName_OnCombatEnd, out var coroutine))
                    InternalProtectedCallback(stack, coroutine, 0);
            };

            OnCharacterKilled += (killer, victim) =>
            {
                IntPtr stack = ScriptContext.LuaState;
                if (m_Binder.PrepareCall(stack, k_CallbackName_OnCharacterKilled, out var coroutine))
                {
                    killer.Character.PushToLua(coroutine);
                    victim.Character.PushToLua(coroutine);
                    InternalProtectedCallback(stack, coroutine, 2);
                }
            };

            OnPlayerKilled += (killer, victim) =>
            {
                IntPtr stack = ScriptContext.LuaState;
                if (m_Binder.PrepareCall(stack, k_CallbackName_OnPlayerKilled, out var coroutine))
                {
                    killer.Character.PushToLua(coroutine);
                    victim.Character.PushToLua(coroutine);
                    InternalProtectedCallback(stack, coroutine, 2);
                }
            };

            OnCharacterVored += (predator, prey) =>
            {
                IntPtr stack = ScriptContext.LuaState;
                if (m_Binder.PrepareCall(stack, k_CallbackName_OnCharacterVored, out var coroutine))
                {
                    predator.Character.PushToLua(coroutine);
                    prey.Character.PushToLua(coroutine);
                    InternalProtectedCallback(stack, coroutine, 2);
                }
            };

            OnCharacterReleased += (predator, prey) =>
            {
                IntPtr stack = ScriptContext.LuaState;
                if (m_Binder.PrepareCall(stack, k_CallbackName_OnCharacterReleased, out var coroutine))
                {
                    predator.Character.PushToLua(coroutine);
                    prey.Character.PushToLua(coroutine);
                    InternalProtectedCallback(stack, coroutine, 2);
                }
            };
        }

        /// <summary>
        /// Returns the Participant associated with a registered Character, or null if none exists.
        /// </summary>
        private Participant GetParticipantForCharacter(Character character)
        {
            return Participants.FirstOrDefault(participant => participant.Character == character);
        }

        /// <summary>
        /// Called when a round ends.
        /// </summary>
        public event RoundUpdateHandler OnRoundStart;

        /// <summary>
        /// Called when a round ends.
        /// </summary>
        public event RoundUpdateHandler OnRoundEnd;

        /// <summary>
        /// Called when combat ends.
        /// </summary>
        public event CombatEndHandler OnCombatEnd;

        /// <summary>
        /// Called when a character is killed.
        /// </summary>
        public event CharacterKilledHandler OnCharacterKilled;

        /// <summary>
        /// Called when the player character is killed.
        /// </summary>
        public event CharacterKilledHandler OnPlayerKilled;

        /// <summary>
        /// Called when a character is devoured by another character.
        /// </summary>
        public event CharacterVoredHandler OnCharacterVored;

        /// <summary>
        /// Called when a character is released from a vore situation.
        /// </summary>
        public event CharacterReleasedHandler OnCharacterReleased;

    }

}
