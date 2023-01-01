/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core.Assets;

namespace Finmer.Editor
{

    /// <summary>
    /// Represents a dockable window that is responsible for editing one asset.
    /// </summary>
    public class AssetWindow : EditorWindow
    {

        /// <summary>
        /// Gets or sets the asset that this window is editing.
        /// </summary>
        public AssetBase Asset { get; set; }

        protected override string GetWindowTitle()
        {
            return Asset?.Name ?? String.Empty;
        }


    }

}
