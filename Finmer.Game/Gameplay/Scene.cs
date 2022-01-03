/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents an interactive environment or game state.
    /// </summary>
    public abstract class Scene
    {

        /// <summary>
        /// Callback fired when the scene becomes the active scene.
        /// </summary>
        public virtual void Enter() {}

        /// <summary>
        /// Callback fired when the scene is no longer the active scene.
        /// </summary>
        public virtual void Leave() {}

        /// <summary>
        /// Callback fired in response to user input.
        /// </summary>
        /// <param name="choice">Choice button number, as set by ChoiceButtonModel.Choice</param>
        public abstract void Turn(int choice);

    }

}
