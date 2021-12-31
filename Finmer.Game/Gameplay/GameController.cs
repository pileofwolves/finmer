/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using Finmer.Core;
using Finmer.Models;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Contains global game state.
    /// </summary>
    public static class GameController
    {

        /// <summary>
        /// Reference to the game window, to be used for accessing UI directly.
        /// </summary>
        public static MainWindow Window { get; set; }

        /// <summary>
        /// Gets the accelerated asset repository. This repository will contain the content of all loaded modules.
        /// </summary>
        public static ContentStore Content { get; } = new ContentStore();

        /// <summary>
        /// Gets a list of loaded modules. Entries contain no assets, only metadata.
        /// </summary>
        public static List<FurballMetadata> LoadedModules { get; } = new List<FurballMetadata>();

        /// <summary>
        /// Gets state associated with a running game session.
        /// </summary>
        public static GameSession Session { get; private set; }

        /// <summary>
        /// Gets or sets whether debug mode is enabled, which allows various development tools to be shown and used.
        /// </summary>
        public static bool DebugMode { get; set; }

        /// <summary>
        /// Activate a new GameSession using the provided save data.
        /// </summary>
        /// <param name="snapshot">Save data with which to reconstruct the game.</param>
        public static void BeginNewSession(GameSnapshot snapshot)
        {
            // Set up the new gameplay session
            Session?.Dispose();
            Session = new GameSession(snapshot);
            Session.Start();
        }

        /// <summary>
        /// Terminate the currently running GameSession.
        /// </summary>
        public static void ExitSession()
        {
            Session?.Dispose();
            Session = null;
        }

    }

}
