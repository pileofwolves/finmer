/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Finmer.Core;
using Finmer.Gameplay;
using Finmer.Utility;
using Finmer.ViewModels;

namespace Finmer.Models
{

    /// <summary>
    /// Contains global UI state as managed by gameplay code.
    /// </summary>
    public class GameUI : BaseProp
    {

        private static GameUI s_Inst;
        private static Dispatcher Dispatcher => Application.Current.Dispatcher;

        private bool m_EnableControls = true;
        private bool m_EnableInventory = true;
        private bool m_IsGameOver;
        private bool m_IsInCombat;
        private bool m_ConsoleOpen;
        private string m_Location = String.Empty;
        private string m_Instruction = String.Empty;
        private string m_Tooltip = String.Empty;
        private CombatStateViewModel m_CombatStateViewModel;

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static GameUI Instance => s_Inst ?? (s_Inst = new GameUI());

        /// <summary>
        /// Collection of log messages to display in the main window.
        /// </summary>
        public ObservableCollection<LogMessageModel> Messages { get; } = new ObservableCollection<LogMessageModel>();

        /// <summary>
        /// Collection of buttons that the user can choose from to progress gameplay.
        /// </summary>
        public ObservableCollection<ChoiceButtonModel> ChoiceButtons { get; } = new ObservableCollection<ChoiceButtonModel>();

        /// <summary>
        /// UI state for the combat displays.
        /// </summary>
        public CombatStateViewModel CombatStateViewModel
        {
            get => m_CombatStateViewModel;
            set
            {
                m_CombatStateViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets whether this game has been ended.
        /// </summary>
        public bool IsGameOver
        {
            get => m_IsGameOver;
            set
            {
                m_IsGameOver = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets whether gameplay controls should be enabled for the player.
        /// </summary>
        public bool ControlsEnabled
        {
            get => m_EnableControls;
            set
            {
                m_EnableControls = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets whether the player should be able to modify their character at this time.
        /// </summary>
        public bool InventoryEnabled
        {
            get => m_EnableInventory;
            set
            {
                m_EnableInventory = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets whether the script console has been opened.
        /// </summary>
        public bool IsScriptConsoleOpened
        {
            get => m_ConsoleOpen;
            set
            {
                m_ConsoleOpen = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets whether the script console can be opened at all.
        /// </summary>
        public bool IsScriptConsoleEnabled => GameController.DebugMode;

        /// <summary>
        /// Gets or sets whether the combat system is currently active.
        /// </summary>
        public bool IsInCombat
        {
            get => m_IsInCombat;
            set
            {
                m_IsInCombat = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the location label displayed above the compass.
        /// </summary>
        public string Location
        {
            get => m_Location;
            set
            {
                m_Location = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the text displayed above the choice buttons when no other tooltip is active.
        /// </summary>
        public string Instruction
        {
            get => m_Instruction;
            set
            {
                m_Instruction = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the text currently displayed above the choice buttons.
        /// </summary>
        public string Tooltip
        {
            get => m_Tooltip;
            set
            {
                m_Tooltip = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Resets all UI state.
        /// </summary>
        public static void Reset()
        {
            s_Inst = new GameUI();
        }

        /// <summary>
        /// Add a button to the choice panel.
        /// </summary>
        public void AddButton(ChoiceButtonModel settings)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ChoiceButtons.Add(settings);
            }));
        }

        /// <summary>
        /// Add a text message to the game log.
        /// </summary>
        public void Log(string text, Color color)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                var vm = new LogMessageModel
                {
                    Text = text.CapFirst(),
                    TextColor = color,
                    TextStyle = Theme.TextBlockDefault,
                    Margin = new Thickness(0, 0, 0, 10),
                    IsBar = false
                };

                Messages.Add(vm);
            }));
        }

        /// <summary>
        /// Add a horizontal splitter to the game log.
        /// </summary>
        public void LogSplit()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                var vm = new LogMessageModel
                {
                    TextColor = Theme.LogColorDefault,
                    IsBar = true
                };

                Messages.Add(vm);
            }));
        }

        /// <summary>
        /// Erases the collection of choice buttons and directional links.
        /// </summary>
        public void ClearButtons()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ChoiceButtons.Clear();
            }));
        }

        /// <summary>
        /// Erases all elements in the game log.
        /// </summary>
        public void ClearLog()
        {
            Dispatcher.Invoke(() => Messages.Clear());
        }

    }

}
