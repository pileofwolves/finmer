/*
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
using Finmer.Core.Buffs;
using JetBrains.Annotations;

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
            List<int> attack_dice = RollCombatDice(GetNumAttackDice(instigator.Character, instigator.CumulativeBuffs));
            List<int> defense_dice = RollCombatDice(GetNumDefenseDice(target.Character, target.CumulativeBuffs));
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
                ActionLabelInstigator = "Attack",
                ActionLabelTarget = "Defense",
                LogKey = log_key,
                Rounds = new List<CombatDisplay.ResolveRound>
                {
                    new CombatDisplay.ResolveRound
                    {
                        OffenseTotal = num_attacks,
                        DefenseTotal = num_defense,
                        OffenseDice = ConvertRollToDieFaces(attack_dice, ERollType.CombatAttack, instigator.Character.IsAlly),
                        DefenseDice = ConvertRollToDieFaces(defense_dice, ERollType.CombatDefense, target.Character.IsAlly)
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

            // Apply equipment effects
            if (damage > 0)
            {
                ProcEffectGroups(instigator, target, EquipEffectGroup.EProcStyle.WielderAttackHit);
                ProcEffectGroups(target, instigator, EquipEffectGroup.EProcStyle.EnemyAttackHit);
            }
            else
            {
                ProcEffectGroups(instigator, target, EquipEffectGroup.EProcStyle.WielderAttackMiss);
                ProcEffectGroups(target, instigator, EquipEffectGroup.EProcStyle.EnemyAttackMiss);
            }
        }

        /// <summary>
        /// Helper function that performs a grapple roll between two participants, and returns whether the instigator won.
        /// </summary>
        private static bool GrappleRoll(Participant instigator, Participant target, string logKeyWin, string logKeyLose)
        {
            // Roll dice for this attack
            List<int> attack_dice = RollGenericD6(GetNumGrappleDice(instigator.Character, instigator.CumulativeBuffs));
            List<int> defense_dice = RollGenericD6(GetNumGrappleDice(target.Character, target.CumulativeBuffs));
            int num_attacks = attack_dice.Sum();
            int num_defense = defense_dice.Sum();

            // Instigator wins if they roll more than the target, meaning they were able overpower them.
            bool success = num_attacks > num_defense;

            // Perform animation
            CombatDisplay.ResolveInfo info = new CombatDisplay.ResolveInfo
            {
                Instigator = instigator,
                Target = target,
                ActionLabelInstigator = "Grapple",
                ActionLabelTarget = "Grapple",
                LogKey = success ? logKeyWin : logKeyLose,
                Rounds = new List<CombatDisplay.ResolveRound>
                {
                    new CombatDisplay.ResolveRound
                    {
                        OffenseTotal = num_attacks,
                        DefenseTotal = num_defense,
                        OffenseDice = ConvertRollToDieFaces(attack_dice, ERollType.Grapple, instigator.Character.IsAlly),
                        DefenseDice = ConvertRollToDieFaces(defense_dice, ERollType.Grapple, target.Character.IsAlly)
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
            {
                instigator.Session.SetGrappling(instigator, target);

                // Apply equipment effects
                ProcEffectGroups(instigator, target, EquipEffectGroup.EProcStyle.WielderGrappled);
                ProcEffectGroups(target, instigator, EquipEffectGroup.EProcStyle.WielderGrappled);
            }
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
        /// Attempt to reverse a grappling session, swapping the grapple initiator and grappled target.
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
                ActionLabelInstigator = "Swallow",
                ActionLabelTarget = "Struggle",
                Rounds = new List<CombatDisplay.ResolveRound>(k_NumVoreRoundsToWin)
            };

            // Generally, first participant to win 3 rounds wins the contest. If pred/prey size differs, the pred's required
            // success count changes by 1 for every level of difference (but never less than 1 round).
            int size_diff = (int)target.Character.Size - (int)instigator.Character.Size;
            int pred_rounds_to_win = Math.Max(k_NumVoreRoundsToWin + size_diff, 1);
            int rounds_attack = 0;
            int rounds_defend = 0;

            while (true)
            {
                // Calculate the number of struggle dice for the target
                int num_struggle_dice = GetNumStruggleDice(target.Character, target.CumulativeBuffs);
                if (target.IsGrappling())
                    // If the target is being grappled, the pred should be more likely to overpower the prey
                    num_struggle_dice = Math.Max(1, num_struggle_dice - 2);

                // Roll dice for this attack
                List<int> attack_dice = RollGenericD6(GetNumSwallowDice(instigator.Character, instigator.CumulativeBuffs));
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
                    OffenseDice = ConvertRollToDieFaces(attack_dice, ERollType.Vore, instigator.Character.IsAlly),
                    DefenseDice = ConvertRollToDieFaces(defense_dice, ERollType.Vore, target.Character.IsAlly)
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

                // Apply equipment effects
                ProcEffectGroups(instigator, target, EquipEffectGroup.EProcStyle.WielderSwallowsPrey);
                ProcEffectGroups(target, instigator, EquipEffectGroup.EProcStyle.WielderSwallowed);
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
            CombatDisplay.ShowSimpleMessage(prey.IsPlayer() ? @"vore_pov_struggle" : @"vore_ext_struggle", predator, prey);
        }

        /// <summary>
        /// Perform turn end for an otherwise active participant whose turn is being skipped.
        /// </summary>
        public static void PostStunTurn(Participant instigator)
        {
            // Show a quick message
            CombatDisplay.ShowSimpleMessage(@"turn_stunned", instigator);

            // Perform normal post-turn processing
            PostTurn(instigator);
        }

        /// <summary>
        /// Perform any post-turn automatic actions or countdowns for a participant.
        /// </summary>
        public static void PostTurn(Participant participant)
        {
            Character character = participant.Character;

            // Show POV prey messages
            if (participant.IsSwallowed() && participant.IsPlayer())
            {
                Participant predator = participant.Predator;
                CombatDisplay.ShowSimpleMessage(predator.Character.PredatorDigests ? @"vore_pov_digest" : @"vore_pov_endo", predator, participant);
            }

            // Digest prey, if any
            if (participant.Prey.Count != 0 && character.PredatorDigests)
            {
                // Deal digestion damage to all live prey
                var digestion_damage = Math.Max(character.Level + Character.GetAbilityModifier(character.Body), 1);
                foreach (var prey in participant.Prey)
                    if (!prey.DigestionImmunity && !prey.Character.IsDead())
                    {
                        prey.Character.Health -= digestion_damage;

                        // Script callbacks on death
                        if (prey.Character.IsDead())
                            prey.Session.NotifyParticipantKilled(participant, prey);
                    }
            }

            // Apply health-over-time buffs now
            int health_diff = participant.CumulativeBuffs.OfType<BuffHealthOverTime>().Sum(buff => buff.Delta);
            participant.Character.Health += health_diff;
            if (participant.Character.IsDead())
                participant.Session.NotifyParticipantKilled(participant, participant);

            // Count down temporary buffs, iterating in reverse order so we can easily pop items from the end
            bool has_buffs = participant.LocalBuffs.Any();
            for (int i = participant.LocalBuffs.Count - 1; i >= 0; i--)
            {
                var container = participant.LocalBuffs[i];

                // Decrease duration, and remove the buff if it expired
                if (--container.RoundsLeft == 0)
                    participant.LocalBuffs.RemoveAt(i);
            }

            // If the participant had any displayed buffs, they will have changed/expired now, so update UI
            if (has_buffs)
                participant.UpdateDisplay();
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

            // Increment round counter, which will also run round-related user callbacks
            session.Round++;
        }

        /// <summary>
        /// Implements auto-vore logic for combat participants.
        /// </summary>
        /// <remarks>
        /// In the Editor, creatures can be configured to automatically swallow or be swallowed when they or other participants are
        /// defeated in combat through a non-vore method, to help reduce logic branches a scene designer needs to take into account.
        /// </remarks>
        /// <param name="victim">The participant who lost all health.</param>
        public static void HandleAutoVore(Participant victim)
        {
            var session = victim.Session;
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
        /// Evaluate and trigger all equipment effect groups of a particular type on the specified participant.
        /// </summary>
        /// <param name="owner">The character whose effect groups to evaluate.</param>
        /// <param name="opponent">Last opponent to attack <paramref name="owner"/>. May be null. Used as target for some effects.</param>
        /// <param name="proc">The category of effect groups to evaluate.</param>
        public static void ProcEffectGroups([NotNull] Participant owner, [CanBeNull] Participant opponent, EquipEffectGroup.EProcStyle proc)
        {
            // Find all effect groups with a matching style
            var eligible_groups = owner.Character.Equipment
                .Where(item => item != null)
                .SelectMany(item => item.Asset.EquipEffects);

            // Evaluate each group
            foreach (var group in eligible_groups)
            {
                // Check that the proc style matches the input trigger
                if (group.ProcStyle != proc)
                    continue;

                // If the group has a random chance specified, roll for it
                if (group.ProcChance < 1.0f && group.ProcChance < CoreUtility.Rng.NextDouble())
                    continue;

                // Find the target(s) of the group
                var targets = new List<Participant>();
                switch (group.ProcTarget)
                {
                    case EquipEffectGroup.EProcTarget.Self:
                        targets.Add(owner);
                        break;
                    case EquipEffectGroup.EProcTarget.Opponent:
                        if (opponent != null)
                            targets.Add(opponent);
                        break;
                    case EquipEffectGroup.EProcTarget.AllAllies:
                        targets.AddRange(owner.Session.Participants.Where(p => p.Character.IsAlly == owner.Character.IsAlly));
                        break;
                    case EquipEffectGroup.EProcTarget.AllOpponents:
                        targets.AddRange(owner.Session.Participants.Where(p => p.Character.IsAlly != owner.Character.IsAlly));
                        break;
                }

                // Apply all included buffs to all targets
                foreach (var target in targets)
                {
                    foreach (var buff in group.Buffs)
                        target.ApplyBuff(buff, group.Duration);

                    target.UpdateDisplay();
                }

                // If a message is included, display it now
                if (!String.IsNullOrWhiteSpace(group.ProcStringTableKey))
                {
                    // Pick overload depending on whether or not we have a relevant opponent
                    if (opponent == null)
                        CombatDisplay.ShowSimpleMessage(group.ProcStringTableKey, owner);
                    else
                        CombatDisplay.ShowSimpleMessage(group.ProcStringTableKey, owner, opponent);
                }
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
        /// Return the total number of Attack Dice a character has in combat given the specified buff set.
        /// </summary>
        public static int GetNumAttackDice(Character target, IEnumerable<Buff> buffs)
        {
            return Math.Max(target.Strength + buffs.OfType<BuffAttackDice>().Sum(buff => buff.Delta), 1);
        }

        /// <summary>
        /// Return the total number of Defense Dice a character has in combat given the specified buff set.
        /// </summary>
        public static int GetNumDefenseDice(Character target, IEnumerable<Buff> buffs)
        {
            return Math.Max(target.Agility + buffs.OfType<BuffDefenseDice>().Sum(buff => buff.Delta), 1);
        }

        /// <summary>
        /// Return the total number of Grapple Dice a character has in combat given the specified buff set.
        /// </summary>
        public static int GetNumGrappleDice(Character target, IEnumerable<Buff> buffs)
        {
            return Math.Max(target.Strength + buffs.OfType<BuffGrappleDice>().Sum(buff => buff.Delta), 1);
        }

        /// <summary>
        /// Return the total number of Swallow Dice a character has in combat given the specified buff set.
        /// </summary>
        public static int GetNumSwallowDice(Character target, IEnumerable<Buff> buffs)
        {
            return Math.Max(target.Body + buffs.OfType<BuffSwallowDice>().Sum(buff => buff.Delta), 1);
        }

        /// <summary>
        /// Return the total number of Struggle Dice a character has in combat given the specified buff set.
        /// </summary>
        public static int GetNumStruggleDice(Character target, IEnumerable<Buff> buffs)
        {
            return Math.Max(target.Agility + buffs.OfType<BuffStruggleDice>().Sum(buff => buff.Delta), 1);
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
                    return EDieFace.AlliedAttack;
                case 2:
                    return EDieFace.AlliedAttackCritical;
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
                    return EDieFace.AlliedDefense;
                case 2:
                    return EDieFace.AlliedDefenseCritical;
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
            return EDieFace.AlliedGeneric1 + (roll - 1);
        }

        /// <summary>
        /// Given a sequence of raw roll values, returns a set of die faces suitable for presenting to the user.
        /// </summary>
        /// <param name="rolls">The rolls to convert.</param>
        /// <param name="action">The type of action that was performed.</param>
        /// <param name="allied">Whether the dice should represent an allied participant, or, if false, a hostile participant.</param>
        private static List<EDieFace> ConvertRollToDieFaces(IReadOnlyCollection<int> rolls, ERollType action, bool allied)
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

            // If the dice represent a hostile participant, shift the selected icons to match
            if (!allied)
                for (int i = 0; i < faces.Count; i++)
                    if (faces[i] != EDieFace.Empty)
                        faces[i] += (int)EDieFace.HostileAttack - 1;

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
