/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using Finmer.Gameplay;
using Finmer.Models;
using Finmer.Utility;
using JetBrains.Annotations;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for LoadingPage.xaml
    /// </summary>
    public partial class LoadingPage : INotifyPropertyChanged
    {

        public static readonly RoutedEvent GameContentLoadedEvent =
            EventManager.RegisterRoutedEvent("GameContentLoaded", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(LoadingPage));

        /// <summary>
        /// Whether the error dialog should be shown.
        /// </summary>
        public bool ShowError
        {
            get => m_ShowError;
            private set
            {
                m_ShowError = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ShowSpinner));
            }
        }

        /// <summary>
        /// Text message shown on the error dialog.
        /// </summary>
        public string ErrorText
        {
            get => m_ErrorText;
            private set
            {
                m_ErrorText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Whether the loading spinner should be shown.
        /// </summary>
        public bool ShowSpinner => !ShowError;

        private string m_ErrorText;

        private Thread m_LoadingThread;

        private bool m_ShowError;

        public LoadingPage()
        {
            InitializeComponent();

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event RoutedEventHandler GameContentLoaded
        {
            add => AddHandler(GameContentLoadedEvent, value);
            remove => RemoveHandler(GameContentLoadedEvent, value);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow?.Close();
        }

        private void DisplayError(string message)
        {
            ErrorText = message;
            ShowError = true;
        }

        private void DisplayFinish()
        {
            // Ensure the thread is destroyed
            m_LoadingThread.Join();

            // Close the loading screen and go to the main menu
            RaiseEvent(new RoutedEventArgs(GameContentLoadedEvent, this));
        }

        private void DisplayFadeComplete(object sender, EventArgs e)
        {
            // Animation is done
            if (UserConfig.FirstStart)
            {
                // On the first boot, navigate to a special page that introduces the game
                GameController.Window.Navigate(new WelcomePage(), ENavigatorAnimation.Instant);
            }
            else
            {
                // Otherwise, go straight to the title screen
                GameController.Window.Navigate(new TitlePage(), ENavigatorAnimation.Instant);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Launch a thread to load all game content, so we can continue animating the main window
            m_LoadingThread = new Thread(new ThreadStart(delegate
            {
                // Load content
                try
                {
                    ContentLoader.LoadAll();
                }
                catch (LoaderException ex)
                {
                    // Pass error to main thread
                    Dispatcher.InvokeAsync(() => DisplayError(ex.Message));
                    return;
                }

                // All done
                Dispatcher.InvokeAsync(DisplayFinish);
            }))
            {
                Name = "Content Loading Thread",
                IsBackground = true
            };
            m_LoadingThread.Start();
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
