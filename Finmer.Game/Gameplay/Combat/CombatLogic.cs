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

            // Number of hitpoints subtracted is the attack power minus the defense power
            int damage = Math.Max(0, num_attacks - num_defense);

            // Pick a relevant string key for the amount of damage we're dealing
            string log_key;
            if (damage >= 5)
                log_key = "attack_crit";
            else if (damage == 0)
                log_key = "attack_miss";
            else
                log_key = "attack_hit";

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
            CombatDisplay.ShowRoundResolve(info);

            // Actually take away the health now that the animation has finished
            target.Character.Health -= damage;

            // Script callbacks on death
            if (target.Character.IsDead())
                target.Session.NotifyParticipantKilled(instigator, target);
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
                // Roll dice for this attack
                List<int> attack_dice = RollGenericD6(instigator.Character.NumSwallowDice);
                List<int> defense_dice = RollGenericD6(instigator.Character.NumStruggleDice);
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
            if (vore_successful)
                info.LogKey = target.IsPlayer() ? "vore_win_pov" : "vore_win";
            else
                info.LogKey = target.IsPlayer() ? "vore_lose_pov" : "vore_lose";

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
            // Perform animation
            CombatDisplay.ShowSimpleMessage("turn_wait", instigator);
        }

        /// <summary>
        /// Perform a Prey/Struggle action.
        /// </summary>
        public static void PerformPreyStruggle(Participant instigator)
        {
            if (!instigator.IsPlayer())
                return;

            // Perform animation
            CombatDisplay.ShowSimpleMessage("vore_pov_struggle", instigator);
        }

        /// <summary>
        /// Perform any post-turn automatic actions or countdowns for a participant.
        /// </summary>
        public static void PostTurn(Participant participant)
        {
            Character character = participant.Character;

            // Digest prey, if any
            if (participant.Prey.Count != 0 && character.StomachDigest)
            {
                // Deal digestion damage to all live prey
                var digestion_damage = character.Level + Character.GetAbilityModifier(character.Body);
                foreach (var prey in participant.Prey)
                    if (!prey.DigestionImmunity)
                    {
                        prey.Character.Health -= digestion_damage;

                        // Script callbacks on death
                        if (prey.Character.IsDead())
                            prey.Session.NotifyParticipantKilled(participant, prey);
                    }

                // Print a message indicating such (but of only one random prey, to avoid clutter when many prey are swallowed)
                var random_prey = participant.Prey[CoreUtility.Rng.Next(participant.Prey.Count)];
                CombatDisplay.ShowSimpleMessage("vore_ext_struggle", participant, random_prey);
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
        /// Rolls a set of abstract combat dice (either attack or defense), and returns the set of rolls.
        /// </summary>
        private static List<int> RollCombatDice(int count)
        {
            var ret = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                // Distribution: 3/6 attack, 2/3 miss, 1/3 critical
                int roll = CoreUtility.Rng.Next(6);
                if (roll <= 3)
                    ret.Add(1);
                else if (roll <= 5)
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
            Vore
        }

    }

}
