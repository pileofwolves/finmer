/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Gameplay;
using Finmer.Models;

namespace Finmer.ViewModels
{

    /// <summary>
    /// Exposes a set of downstream view models that the MainPage can bind to.
    /// </summary>
    internal class MainPageViewModel : BaseProp
    {

        public Player Player => GameController.Session.Player;

        public GameUI UI => GameUI.Instance;

    }

}
