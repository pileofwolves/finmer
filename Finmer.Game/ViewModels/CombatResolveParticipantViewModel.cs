/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using Finmer.Gameplay.Combat;

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model for one of the two participants in the combat resolve screen.
    /// </summary>
    public sealed class CombatResolveParticipantViewModel : BaseProp
    {

        public Participant Participant { get; }

        public int DieTotal { get; }

        public List<EDieFace> DieFaces { get; }

        public CombatDisplay.EResolveType ResolveType { get; }

        public CombatResolveParticipantViewModel(Participant participant, CombatDisplay.EResolveType resolveType, int dieTotal, List<EDieFace> dieFaces)
        {
            Participant = participant;
            DieTotal = dieTotal;
            DieFaces = dieFaces;
            ResolveType = resolveType;
        }

    }

}
