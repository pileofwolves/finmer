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

namespace Finmer.Gameplay.Combat
{

    /// <summary>
    /// Encapsulates the combat game mechanics and wraps the CombatDisplay class.
    /// </summary>
    public static class CombatLogic
    {

        private const int k_NumVoreRoundsToWin = 3;

        /// <summary>
        /// Perform an Attack action.
        /// </summary>
        /// <param name="instigator">The participant that is initiating the attack.</param>
        /// <param name="target">The participant that is being attacked.</param>
        public static void PerformAttack(Participant instigator, Participant target)
        {
            // Roll dice for this attack
            List<int> attack_dice = RollCombatDice(instigator.Character.NumAttackDice);
            List<int> defense_dice = RollCombatDice(target.Character.NumDefenseDice);
            int num_attacks = attack_dice.Sum();
            int num_defense = defense_dice.Sum();

            // Number of hit points subtracted is the attack power minus the defense power
            int damage = Math.Max(0, num_attacks - num_defense);

            // Pick a relevant string key for the amount of damage we're dealing
            string log_key;
            if (damage >= 5)
                log_key = @"attack_crit";
            else if (damage == 0)
                log_key = @"attack_miss";
            else
                log_key = @"attack_hit";

            // Perform animation
            CombatDisplay.ResolveInfo info = new CombatDisplay.ResolveInfo
            {
                Instigator = instigator,
                Target = target,
                Action = CombatDisplay.EResolveType.Attack,
                LogKey = log_key,
                Rounds = new List<CombatDisplay.ResolveRound>
                {
                    new CombatDisplay.ResolveRound
                    {
                        OffenseTotal = num_attacks,
                        DefenseTotal = num_defense,
                        OffenseDice = ConvertRollToDieFaces(attack_dice, ERollType.CombatAttack),
                        DefenseDice = ConvertRollToDieFaces(defense_dice, ERollType.CombatDefense)
                    }
                }
            };
            TextParser.SetVariable("weapon", instigator.Character.EquippedWeapon?.Alias ?? "claws");
            TextParser.SetVariable("damage", $"{damage:##,##0}");
            CombatDisplay.ShowRoundResolve(info);

            // Actually take away the health now that the animation has finished
            target.Character.Health -= damage;

            // Script callbacks on death
            if (target.Character.IsDead())
                target.Session.NotifyParticipantKilled(instigator, target);
        }

        /// <summary>
        /// Helper function that performs a grapple roll between two participants, and returns whether the instigator won.
        /// </summary>
        private static bool GrappleRoll(Participant instigator, Participant target, string logKeyWin, string logKeyLose)
        {
            // Roll dice for this attack
            List<int> attack_dice = RollGenericD6(instigator.Character.NumGrappleDice);
            List<int> defense_dice = RollGenericD6(target.Character.NumGrappleDice);
            int num_attacks = attack_dice.Sum();
            int num_defense = defense_dice.Sum();

            // Instigator wins if they roll more than the target, meaning they were able overpower them.
            bool success = num_attacks > num_defense;

            // Perform animation
            CombatDisplay.ResolveInfo info = new CombatDisplay.ResolveInfo
            {
                Instigator = instigator,
                Target = target,
                Action = CombatDisplay.EResolveType.Grapple,
                LogKey = success ? logKeyWin : logKeyLose,
                Rounds = new List<CombatDisplay.ResolveRound>
                {
                    new CombatDisplay.ResolveRound
                    {
                        OffenseTotal = num_attacks,
                        DefenseTotal = num_defense,
                        OffenseDice = ConvertRollToDieFaces(attack_dice, ERollType.Grapple),
                        DefenseDice = ConvertRollToDieFaces(defense_dice, ERollType.Grapple)
                    }
                }
            };
            CombatDisplay.ShowRoundResolve(info);

            return success;
        }

        /// <summary>
        /// Initiate a grappling session.
        /// </summary>
        /// <param name="instigator">The participant that is initiating the attack.</param>
        /// <param name="target">The participant that is being attacked.</param>
        public static void PerformGrappleInitiate(Participant instigator, Participant target)
        {
            // Set these characters to grappling if the instigator succeeded
            bool success = GrappleRoll(instigator, target, @"grapple_hit", @"grapple_miss");
            if (success)
                instigator.Session.SetGrappling(instigator, target);
        }

        /// <summary>
        /// Attempt to escape a grappling session.
        /// </summary>
        /// <param name="instigator">The participant that is initiating the attack.</param>
        /// <param name="target">The participant that is being attacked.</param>
        public static void PerformGrappleEscape(Participant instigator, Participant target)
        {
            // If the instigator wins, they were able to escape
            bool success = GrappleRoll(instigator, target, @"grapple_escape_hit", @"grapple_escape_miss");
            if (success)
                instigator.Session.UnsetGrappling(instigator, target);
        }

        /// <summary>
        /// Attempt to reverse a grappling session, swapping the grappler and grapplee.
        /// </summary>
        /// <param name="instigator">The participant that is initiating the attack.</param>
        /// <param name="target">The participant that is being attacked.</param>
        public static void PerformGrappleReverse(Participant instigator, Participant target)
        {
            Debug.Assert(!instigator.GrapplingInitiator, "Instigator is attempting reverse grapple, but is already the initiator");
            Debug.Assert(target.GrapplingInitiator, "Target is supposed to be the grapple initiator, but isn't");

            // If the instigator wins, they were able to escape
            bool success = GrappleRoll(instigator, target, @"grapple_escape_hit", @"grapple_escape_miss");
            if (success)
            {
                // Reset the grappling session, and then create a new one where the instigator is on top (which the target used to be).
                instigator.Session.UnsetGrappling(instigator, target);
                instigator.Session.SetGrappling(instigator, target);
            }
        }

        /// <summary>
        /// Perform an action where the grapple initiator releases their victim. This does not require a roll.
        /// </summary>
        /// <param name="instigator">The participant that is initiating the attack.</param>
        /// <param name="target">The participant that is being attacked.</param>
        public static void PerformGrappleRelease(Participant instigator, Participant target)
        {
            Debug.Assert(instigator.GrapplingInitiator, "Instigator is expected to be the initiator");
            Debug.Assert(!target.GrapplingInitiator, "Target is expected to not be the initiator");

            // Reset the grappling session
            instigator.Session.UnsetGrappling(instigator, target);

            // Print a simple message, since there is no further dice animation for this action
            CombatDisplay.ShowSimpleMessage(@"grapple_pin_release", instigator, target);
        }

        /// <summary>
        /// Perform a Vore action.
        /// </summary>
        /// <param name="instigator">The participant that is initiating the attack.</param>
        /// <param name="target">The participant that is being attacked.</param>
        public static void PerformVore(Participant instigator, Participant target)
        {
            CombatDisplay.ResolveInfo info = new CombatDisplay.ResolveInfo
            {
                Instigator = instigator,
                Target = target,
                Action = CombatDisplay.EResolveType.Vore,
                Rounds = new List<CombatDisplay.ResolveRound>(k_NumVoreRoundsToWin)
            };

            // Generally, first participant to win 3 rounds wins the contest. If pred/prey size differs, the pred's required
            // success count changes by 1 for every level of difference (but never less than 1 round).
            int size_diff = target.Character.Size - instigator.Character.Size;
            int pred_rounds_to_win = Math.Max(k_NumVoreRoundsToWin + size_diff, 1);
            int rounds_attack = 0;
            int rounds_defend = 0;

            while (true)
            {
                // Calculate the number of struggle dice for the target
                int num_struggle_dice = target.Character.NumStruggleDice;
                if (target.IsGrappling())
                    // If the target is being grappled, the pred should be more likely to overpower the prey
                    num_struggle_dice = Math.Max(1, num_struggle_dice - 2);

                // Roll dice for this attack
                List<int> attack_dice = RollGenericD6(instigator.Character.NumSwallowDice);
                List<int> defense_dice = RollGenericD6(num_struggle_dice);
                int num_attacks = attack_dice.Sum();
                int num_defense = defense_dice.Sum();

                // Predator wins the round if they roll higher than the prey - so ties are in favor of the prey
                if (num_attacks > num_defense)
                    rounds_attack++;
                else
                    rounds_defend++;

                info.Rounds.Add(new CombatDisplay.ResolveRound
                {
                    OffenseTotal = num_attacks,
                    DefenseTotal = num_defense,
                    OffenseRoundsWon = rounds_attack,
                    DefenseRoundsWon = rounds_defend,
                    OffenseDice = ConvertRollToDieFaces(attack_dice, ERollType.Vore),
                    DefenseDice = ConvertRollToDieFaces(defense_dice, ERollType.Vore)
                });

                // Break loop if either character has won the contest
                if (rounds_attack >= pred_rounds_to_win || rounds_defend >= k_NumVoreRoundsToWin)
                    break;
            }

            // Pick an appropriate logging key
            bool vore_successful = rounds_attack >= pred_rounds_to_win;
            info.LogKey = vore_successful ? @"vore_win" : @"vore_lose";

            // Perform animation
            CombatDisplay.ShowRoundResolve(info);

            // If the predator won the contest, swallow the prey
            if (vore_successful)
            {
                var session = instigator.Session;
                session.SetVored(instigator, target);
            }
        }

        /// <summary>
        /// Perform a Skip Turn action.
        /// </summary>
        public static void PerformSkipTurn(Participant instigator)
        {
            // Skip writing text for NPCs that can't do anything, looks better
            if (!instigator.IsPlayer())
                return;

            // Perform animation
            CombatDisplay.ShowSimpleMessage(instigator.IsGrappling() ? @"turn_wait_grappled" : @"turn_wait", instigator);
        }

        /// <summary>
        /// Perform a Prey/Struggle action.
        /// </summary>
        public static void PerformPreyStruggle(Participant predator, Participant prey)
        {
            // Show text describing the action. Special handling for the player to use some POV messages instead.
            if (prey.IsPlayer())
                CombatDisplay.ShowSimpleMessage(@"vore_pov_struggle", prey);
            else
                CombatDisplay.ShowSimpleMessage(@"vore_ext_struggle", predator, prey);
        }

        /// <summary>
        /// Perform any post-turn automatic actions or countdowns for a participant.
        /// </summary>
        public static void PostTurn(Participant participant)
        {
            Character character = participant.Character;

            // Digest prey, if any
            if (participant.Prey.Count != 0 && character.PredatorDigests)
            {
                // Deal digestion damage to all live prey
                var digestion_damage = character.Level + Character.GetAbilityModifier(character.Body);
                foreach (var prey in participant.Prey)
                    if (!prey.DigestionImmunity && !prey.Character.IsDead())
                    {
                        prey.Character.Health -= digestion_damage;

                        // Script callbacks on death
                        if (prey.Character.IsDead())
                            prey.Session.NotifyParticipantKilled(participant, prey);
                    }
            }

            // TODO: Count down temporary buffs
        }

        /// <summary>
        /// Perform any post-round automatic cleanup.
        /// </summary>
        public static void PostRound(CombatSession session)
        {
            // Remove special one-turn statuses from participants
            foreach (var participant in session.Participants)
            {
                participant.DigestionImmunity = false;
            }

            // Run user callbacks
            session.NotifyRoundEnded();
            session.Round++;
        }

        /// <summary>
        /// Implements auto-vore logic for combat participants.
        /// </summary>
        /// <remarks>
        /// In the Editor, creatures can be configured to automatically swallow or be swallowed when they or other participants are
        /// defeated in combat through a non-vore method, to help reduce logic branches a scene designer needs to take into account.
        /// </remarks>
        /// <param name="killer">The participant who killed the victim.</param>
        /// <param name="victim">The participant who lost all health.</param>
        public static void HandleAutoVore(Participant killer, Participant victim)
        {
            var session = killer.Session;
            var all_participants = session.Participants;

            // NPC prey
            var victim_asset = victim.Character.Asset;
            if (victim_asset != null && !victim.Character.IsAlly && victim_asset.AutoSwallowedByPlayer)
            {
                // If the victim is marked as auto-prey, have the player swallow them now.
                // Find the player participant.
                Participant predator = all_participants.FirstOrDefault(p => p.IsPlayer() && !p.Character.IsDead());
                if (predator == null)
                    return;

                // TODO: Take into account edge cases like predator being occupied (grappling, already swallowed themselves, etc)

                // Proceed with having the player swallow the target
                Debug.Assert(!victim.IsPlayer() && victim != predator);
                session.SetVored(predator, victim);
                CombatDisplay.ShowSimpleMessage(@"kill_generic", predator, victim);
                CombatDisplay.ShowSimpleMessage(@"vore_win", predator, victim);

                return;
            }

            // Player prey
            if (victim.IsPlayer())
            {
                // If the victim is the player, and there is an auto-vore pred, have them swallow the player.
                // Search for the first living predator who is marked as an auto-vore pred - note that this is not necessarily the original killer.
                Participant predator = all_participants.FirstOrDefault(p =>
                    !p.Character.IsDead() && !p.Character.IsAlly && p.Character.Asset != null && p.Character.Asset.AutoSwallowPlayer);
                if (predator == null)
                    return;

                // Proceed with having the predator swallow the player
                Debug.Assert(victim != predator);
                session.SetVored(predator, victim);
                CombatDisplay.ShowSimpleMessage(@"kill_generic", predator, victim);
                CombatDisplay.ShowSimpleMessage(@"vore_win", predator, victim);
            }
        }

        /// <summary>
        /// Calculates and returns the amount of XP that the target character would award when defeated.
        /// </summary>
        public static int CalculateXP(Character victim)
        {
            // Start at 20 XP per kill, multiplying by log(n) for levels above 1
            return (int)Math.Round((Math.Log(victim.Level) + 1) * 20);
        }

        /// <summary>
        /// Rolls a set of abstract combat dice (either attack or defense), and returns the set of rolls.
        /// </summary>
        private static List<int> RollCombatDice(int count)
        {
            var ret = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                // Distribution: 3/6 attack, 2/3 miss, 1/3 critical
                int roll = CoreUtility.Rng.Next(6) + 1;
                if (roll <= 3)
                    ret.Add(1);
                else if (roll <= 4)
                    ret.Add(2);
                else
                    ret.Add(0);
            }

            return ret;
        }

        /// <summary>
        /// Rolls a number of six-sided dice and returns the results.
        /// </summary>
        private static List<int> RollGenericD6(int count)
        {
            var ret = new List<int>(count);

            for (int i = 0; i < count; i++)
                ret.Add(CoreUtility.Rng.Next(6) + 1);

            return ret;
        }

        /// <summary>
        /// Converts an attack die roll value to a user-presentable die face.
        /// </summary>
        private static EDieFace ConvertAttackRollToDieFace(int roll)
        {
            switch (roll)
            {
                case 1:
                    return EDieFace.Attack;
                case 2:
                    return EDieFace.AttackCritical;
                default:
                    return EDieFace.Empty;
            }
        }

        /// <summary>
        /// Converts a defense die roll value to a user-presentable die face.
        /// </summary>
        private static EDieFace ConvertDefenseRollToDieFace(int roll)
        {
            switch (roll)
            {
                case 1:
                    return EDieFace.Defense;
                case 2:
                    return EDieFace.DefenseCritical;
                default:
                    return EDieFace.Empty;
            }
        }

        /// <summary>
        /// Converts a D6 roll value to a user-presentable die face.
        /// </summary>
        private static EDieFace ConvertD6ToDieFace(int roll)
        {
            Debug.Assert(roll >= 1 && roll <= 6);
            return EDieFace.Generic1 + (roll - 1);
        }

        /// <summary>
        /// Given a sequence of raw roll values, returns a set of die faces suitable for presenting to the user.
        /// </summary>
        /// <param name="rolls">The rolls to convert.</param>
        /// <param name="action">The type of action that was performed.</param>
        private static List<EDieFace> ConvertRollToDieFaces(IReadOnlyCollection<int> rolls, ERollType action)
        {
            List<EDieFace> faces = new List<EDieFace>(rolls.Count);

            foreach (int roll in rolls)
            {
                switch (action)
                {
                    case ERollType.CombatAttack:
                        faces.Add(ConvertAttackRollToDieFace(roll));
                        break;

                    case ERollType.CombatDefense:
                        faces.Add(ConvertDefenseRollToDieFace(roll));
                        break;

                    case ERollType.Vore:
                    case ERollType.Grapple:
                        faces.Add(ConvertD6ToDieFace(roll));
                        break;

                    default:
                        // Not yet supported
                        throw new ArgumentOutOfRangeException(nameof(action));
                }
            }

            return faces;
        }

        /// <summary>
        /// Describes the type of action being performed, used for translating a roll value to a set of die faces.
        /// </summary>
        private enum ERollType
        {
            CombatAttack,
            CombatDefense,
            Vore,
            Grapple
        }

    }

}
