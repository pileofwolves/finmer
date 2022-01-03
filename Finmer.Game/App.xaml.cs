/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Linq;
using System.Windows;
using Finmer.Gameplay;
using Finmer.Gameplay.Scripting;
using Finmer.Models;
using Finmer.Utility;
using Finmer.Views;
using JetBrains.Annotations;

namespace Finmer
{

    /// <summary>
    /// Contains application startup and shutdown logic.
    /// </summary>
    public partial class App
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if DEBUG
            // Enable development mode by default when running a debug build
            GameController.DebugMode = !e.Args.Contains("-nodebug");
#else
            // Install exception handler, for logging crashes. We only do this in non-Debug builds, to ensure that
            // when debugging the debugger gets the first chance at the exception without having it muddled by the handler.
            AppDomain.CurrentDomain.UnhandledException += Program_UnhandledException;

            // Enable development mode if requested by user (or the editor)
            GameController.DebugMode = e.Args.Contains("-debug");
#endif

            // Load script runtime
            LuaApi.LoadNativeLibrary();

            // Load user settings
            UserConfig.Reload();

            // Launch the game app
            var wnd = new MainWindow();
            GameController.Window = wnd;
            Current.MainWindow = wnd;
            Current.MainWindow.Show();

            // Open the loading screen, which will begin loading game assets
            wnd.Navigate(new LoadingPage(), ENavigatorAnimation.Instant);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Ensure all gameplay state is released while the app framework is still available
            GameController.ExitSession();

            // Persist the user settings to disk
            UserConfig.Save();

            base.OnExit(e);
        }

        [UsedImplicitly]
        private static void Program_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Make sure the object is actually an Exception
            if (!(e.ExceptionObject is Exception ex))
                return;

            // Write a crash report
            Logger.WriteExceptionReport(ex);

            // Let the user know what's happening
            MessageBox.Show("Finmer encountered an internal error. A crash report (two files) should have been saved to the game folder. " +
                "Please send them to nuntiswolf@gmail.com, or 'nuntis' on FurAffinity so it can be investigated! <3" +
                Environment.NewLine + Environment.NewLine + "Exception class: " + ex.GetType().Name,
                "Sorry! I'll do better next time :(",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

    }

}
