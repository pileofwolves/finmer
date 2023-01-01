/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Editor
{

    /// <summary>
    /// Provides an interface for a specialized script editor control to communicate with the host form.
    /// </summary>
    public interface IScriptEditor
    {

        /// <summary>
        /// Instruct the script editor control to flush any pending changes.
        /// </summary>
        void Flush();

    }

}
