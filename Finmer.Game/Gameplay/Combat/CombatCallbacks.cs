/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Gameplay.Combat
{

    /// <summary>
    /// Describes a handler for a combat event where a round has ended.
    /// </summary>
    /// <param name="round">The round number that is ENDING (not starting), where 1 is the first round.</param>
    public delegate void RoundEndHandler(int round);

    /// <summary>
    /// Describes a handler for when the player wins and the combat session ends.
    /// </summary>
    public delegate void CombatEndHandler();

    /// <summary>
    /// Describes a handler for a combat event in which a character is killed in a non-vorish way.
    /// </summary>
    /// <param name="killer">The <seealso cref="Participant" /> responsible for the kill.</param>
    /// <param name="victim">The <seealso cref="Participant" /> that was killed.</param>
    public delegate void CharacterKilledHandler(Participant killer, Participant victim);

    /// <summary>
    /// Describes a handler for a combat event in which a character is devoured.
    /// </summary>
    /// <param name="predator">The <seealso cref="Participant" /> acting as predator.</param>
    /// <param name="prey">The <seealso cref="Participant" /> acting as prey.</param>
    public delegate void CharacterVoredHandler(Participant predator, Participant prey);

    /// <summary>
    /// Describes a handler for a combat event in which a character is released from a stomach.
    /// </summary>
    /// <param name="predator">The <seealso cref="Participant" /> acting as predator.</param>
    /// <param name="prey">The prey <seealso cref="Participant" /> that was released.</param>
    public delegate void CharacterReleasedHandler(Participant predator, Participant prey);

}
