/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
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
            List<EDieFace> attack_dice = RollAttackDice(instigator);
            List<EDieFace> defense_dice = RollDefenseDice(target);
            int num_attacks = SumDice(attack_dice);
            int num_defense = SumDice(defense_dice);

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
                        OffenseDice = attack_dice,
                        DefenseDice = defense_dice
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
                List<EDieFace> attack_dice = RollVoreSwallowDice(instigator);
                List<EDieFace> defense_dice = RollVoreStruggleDice(target);
                int num_attacks = SumDice(attack_dice);
                int num_defense = SumDice(defense_dice);

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
                    OffenseDice = attack_dice,
                    DefenseDice = defense_dice
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
        /// Computes the number of attack dice that a participant can use, then generates a list of die faces for those dice.
        /// </summary>
        private static List<EDieFace> RollAttackDice(Participant instigator)
        {
            int num_dice = instigator.Character.NumAttackDice;
            var ret = new List<EDieFace>(num_dice);

            for (int i = 0; i < num_dice; i++)
            {
                // Distribution: 3/6 attack, 2/3 miss, 1/3 critical
                int roll = CoreUtility.Rng.Next(6);
                switch (roll)
                {
                    case 0:
                    case 1:
                    case 2:
                        ret.Add(EDieFace.Attack);
                        break;
                    case 3:
                        ret.Add(EDieFace.AttackCritical);
                        break;
                    default:
                        ret.Add(EDieFace.Empty);
                        break;
                }
            }

            return ret;
        }

        /// <summary>
        /// Computes the number of defense dice that a participant can use, then generates a list of die faces for those dice.
        /// </summary>
        private static List<EDieFace> RollDefenseDice(Participant instigator)
        {
            int num_dice = instigator.Character.NumDefenseDice;
            var ret = new List<EDieFace>(num_dice);

            for (int i = 0; i < num_dice; i++)
            {
                // Distribution: 3/6 attack, 2/3 miss, 1/3 critical
                int roll = CoreUtility.Rng.Next(6);
                switch (roll)
                {
                    case 0:
                    case 1:
                    case 2:
                        ret.Add(EDieFace.Defense);
                        break;
                    case 3:
                        ret.Add(EDieFace.DefenseCritical);
                        break;
                    default:
                        ret.Add(EDieFace.Empty);
                        break;
                }
            }

            return ret;
        }

        /// <summary>
        /// Computes the number of offensive vore dice that a participant can use, then generates a list of die faces for those dice.
        /// </summary>
        private static List<EDieFace> RollVoreSwallowDice(Participant instigator)
        {
            int num_dice = instigator.Character.NumSwallowDice;
            var ret = new List<EDieFace>(num_dice);

            for (int i = 0; i < num_dice; i++)
                // Distribution: 50/50 hit/miss
                ret.Add((CoreUtility.Rng.Next(2) == 0) ? EDieFace.VoreSwallow : EDieFace.Empty);

            return ret;
        }

        /// <summary>
        /// Computes the number of defensive vore dice that a participant can use, then generates a list of die faces for those dice.
        /// </summary>
        private static List<EDieFace> RollVoreStruggleDice(Participant instigator)
        {
            int num_dice = instigator.Character.NumStruggleDice;
            var ret = new List<EDieFace>(num_dice);

            for (int i = 0; i < num_dice; i++)
                // Distribution: 50/50 hit/miss
                ret.Add((CoreUtility.Rng.Next(2) == 0) ? EDieFace.VoreStruggle : EDieFace.Empty);

            return ret;
        }

        /// <summary>
        /// Sum a collection of die faces together, to represent the total roll value.
        /// </summary>
        /// <param name="rolls">The dice to sum. Should contain either only attack die faces, or only defense die faces, not both.</param>
        private static int SumDice(IEnumerable<EDieFace> rolls)
        {
            var total = 0;
            foreach (EDieFace face in rolls)
                switch (face)
                {
                    case EDieFace.Attack:
                    case EDieFace.Defense:
                    case EDieFace.Grapple:
                    case EDieFace.VoreSwallow:
                    case EDieFace.VoreStruggle:
                        total += 1;
                        break;

                    case EDieFace.AttackCritical:
                    case EDieFace.DefenseCritical:
                        total += 2;
                        break;
                }

            return total;
        }

    }

}
