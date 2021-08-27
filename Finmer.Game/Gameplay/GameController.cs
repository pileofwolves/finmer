/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Collections.Generic;
using System.Linq;
using Finmer.Core;
using Finmer.Core.Assets;
using Finmer.Models;
using Finmer.Utility;

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
        /// Gets or sets the central asset repository. All loaded modules have their assets merged into this container module.
        /// </summary>
        public static Furball Content { get; set; }

        /// <summary>
        /// Gets a list of loaded modules. Entries contain no assets, only metadata.
        /// </summary>
        public static List<FurballMetadata> LoadedModules { get; } = new List<FurballMetadata>();

        /// <summary>
        /// Gets state associated with a running game session.
        /// </summary>
        public static GameSession Session { get; private set; }

        public static StringTable MergedStrings { get; } = new StringTable();

        /// <summary>
        /// Gets or sets whether debug mode is enabled, which allows various development tools to be shown and used.
        /// </summary>
        public static bool DebugMode { get; set; }

        /// <summary>
        /// Retrieves a string from the game string table and automatically substitutes text parser tags.
        /// </summary>
        /// <param name="key">The table key to look up.</param>
        public static string GetString(string key)
        {
            return TextParser.Parse(MergedStrings.GetRandomEntry(key));
        }

        /// <summary>
        /// Activate a new GameSession using the provided save data.
        /// </summary>
        /// <param name="savedata">Save data with which to create the player object.</param>
        public static void BeginNewSession(PropertyBag savedata)
        {
            // Reset UI
            GameUI.Instance.Reset();

            // Set up the new gameplay session
            Session?.Dispose();
            Session = new GameSession(savedata);

            // Bind player grammar context
            TextParser.ClearAllContexts();
            TextParser.SetContext("player", Session.Player, true);

            // Run global scripts
            var script_context = Session.ScriptContext;
            foreach (var script in Content.Assets.OfType<AssetScript>())
                if (script_context.LoadScript(script.PrecompiledScript, script.Name))
                    script_context.RunProtectedCall(0, 0);

            // Check if we have write perms
            if (!Logger.HasWritePermission())
                GameUI.Instance.Log("Warning: It looks like the game does not have permission to write files to the app " +
                    "directory. This means that you cannot save your game.\r\n", Theme.LogColorError);

            // Create the initial scene
            Session.PushScene(new SceneScripted(script_context, Session.Player.Location));
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
