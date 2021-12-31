/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using JetBrains.Annotations;

namespace Finmer.ViewModels
{

    /// <summary>
    /// I think this class sums up my frustration with MVVM quite well.
    /// </summary>
    internal class AbilityPointCollectionViewModel : List<AbilityPointViewModel>
    {

        [UsedImplicitly]
        public AbilityPointCollectionViewModel() { }

    }

}
