/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents an object that can be captured into and restored from save data.
    /// </summary>
    public interface ISaveable
    {

        /// <summary>
        /// Write data representing this instance to an output save data object.
        /// </summary>
        PropertyBag SaveState();

        /// <summary>
        /// Overwrite state of this instance with data previously captured by SaveState().
        /// </summary>
        /// <exception cref="InvalidSaveDataException">Throws if save data cannot be parsed.</exception>
        void LoadState(PropertyBag input);

    }

}
