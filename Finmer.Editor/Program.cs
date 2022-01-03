/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows.Forms;
using Finmer.Core;

namespace Finmer.Editor
{

    internal static class Program
    {

        /// <summary>
        /// A singleton reference to the main form.
        /// </summary>
        internal static FormMain MainForm { get; set; }

        /// <summary>
        /// Represents the <seealso cref="Furball" /> that is currently being edited in the editor program.
        /// </summary>
        internal static Furball ActiveFurball { get; set; }

        /// <summary>
        /// Contains the collective content of all downstream dependencies of ActiveFurball.
        /// </summary>
        internal static Furball ActiveDependencies { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        internal static void Main()
        {
            // Ensure user preferences are loaded
            EditorPreferences.Reload();

            // Launch the editor
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var window = new FormMain())
                Application.Run(window);
        }

    }

}
