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
using Finmer.Gameplay.Scripting;
using Finmer.Models;

namespace Finmer.Gameplay.Combat
{

    /// <summary>
    /// Contains the state for a V2 combat instance.
    /// </summary>
    public class CombatSession : ScriptableObject
    {

        private const string k_CallbackName_OnRoundEnd = "OnRoundEnd";
        private const string k_CallbackName_OnCombatEnd = "OnCombatEnd";
        private const string k_CallbackName_OnCharacterKilled = "OnCreatureKilled";
        private const string k_CallbackName_OnCharacterVored = "OnCreatureVored";
        private const string k_CallbackName_OnCharacterReleased = "OnCreatureReleased";
        private const string k_CallbackName_OnPlayerKilled = "OnPlayerKilled";

        private readonly ScriptCallbackTable m_Binder;
        private Participant m_WhoseTurn;

        /// <summary>
        /// Collection of participating characters in this combat.
        /// </summary>
        public List<Participant> Participants { get; } = new List<Participant>();

        /// <summary>
        /// The combat turn number. Starts at 1 for the first round.
        /// </summary>
        [ScriptableProperty(EScriptAccess.Read)]
        public int Round { get; set; } = 1;

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
        /// Add a Character to this combat session as a new participant.
        /// </summary>
        /// <param name="character">The character to register. Characters may only be registered once.</param>
        public void AddParticipant(Character character)
        {
            Debug.Assert(Participants.All(p => p.Character != character), "Character is already registered");
            Participants.Add(new Participant(character, this));
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
        /// Notify event listeners that a round has ended.
        /// </summary>
        public void NotifyRoundEnded()
        {
            OnRoundEnd?.Invoke(Round);
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

            // If the victim was not swallowed, there may be auto-vore triggers we need to activate, check now
            if (!victim.IsSwallowed())
                CombatLogic.HandleAutoVore(killer, victim);

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
                Debug.Assert(victim.Predator == killer);
                CombatDisplay.ShowSimpleMessage(@"kill_digested", killer, victim);

                // Optional disposal scene if available and enabled by the user
                var predator_asset = killer.Character.Asset;
                if (predator_asset != null && predator_asset.PredatorDisposal && UserConfig.PreferScat)
                {
                    CombatDisplay.ShowPause();
                    CombatDisplay.ShowSimpleMessage(@"vore_disposal", killer, victim);
                }
            }

            // If the victim was an enemy, award XP to the player
            if (!victim_character.IsAlly)
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

        private void AttachScriptCallbacks()
        {
            OnRoundEnd += round =>
            {
                IntPtr stack = ScriptContext.LuaState;
                if (m_Binder.PrepareCall(stack, k_CallbackName_OnRoundEnd))
                {
                    LuaApi.lua_pushnumber(stack, round);
                    m_Binder.Call(stack, 1);
                }
            };

            OnCombatEnd += () =>
            {
                IntPtr stack = ScriptContext.LuaState;
                if (m_Binder.PrepareCall(stack, k_CallbackName_OnCombatEnd))
                    m_Binder.Call(stack, 0);
            };

            OnCharacterKilled += (killer, victim) =>
            {
                IntPtr stack = ScriptContext.LuaState;
                if (m_Binder.PrepareCall(stack, k_CallbackName_OnCharacterKilled))
                {
                    killer.Character.PushToLua(stack);
                    victim.Character.PushToLua(stack);
                    m_Binder.Call(stack, 2);
                }
            };

            OnPlayerKilled += (killer, victim) =>
            {
                IntPtr stack = ScriptContext.LuaState;
                if (m_Binder.PrepareCall(stack, k_CallbackName_OnPlayerKilled))
                {
                    killer.Character.PushToLua(stack);
                    victim.Character.PushToLua(stack);
                    m_Binder.Call(stack, 2);
                }
            };

            OnCharacterVored += (predator, prey) =>
            {
                IntPtr stack = ScriptContext.LuaState;
                if (m_Binder.PrepareCall(stack, k_CallbackName_OnCharacterVored))
                {
                    predator.Character.PushToLua(stack);
                    prey.Character.PushToLua(stack);
                    m_Binder.Call(stack, 2);
                }
            };

            OnCharacterReleased += (predator, prey) =>
            {
                IntPtr stack = ScriptContext.LuaState;
                if (m_Binder.PrepareCall(stack, k_CallbackName_OnCharacterReleased))
                {
                    predator.Character.PushToLua(stack);
                    prey.Character.PushToLua(stack);
                    m_Binder.Call(stack, 2);
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
        public event RoundEndHandler OnRoundEnd;

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
