/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class GameUI : BaseProp, ISaveable
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

        public PropertyBag Serialize()
        {
            var output = new PropertyBag();

            // Store basic data
            output.SetString(SaveData.k_UI_Location, Location);
            output.SetString(SaveData.k_UI_Instruction, Instruction);
            output.SetBool(SaveData.k_UI_InventoryEnabled, InventoryEnabled);

            // Find the last few log messages (skipping entries that have no text content)
            const int k_LogMessageCount = 16;
            var filtered_messages = Messages.Where(entry => !entry.IsBar).ToArray();
            var desired_message_count = Math.Min(filtered_messages.Length, k_LogMessageCount);
            var last_messages = filtered_messages.Skip(filtered_messages.Length - desired_message_count).ToArray();

            // Store those log messages so they can be displayed when reloading
            output.SetInt(SaveData.k_UI_LogSnippet_Count, last_messages.Length);
            for (int i = 0; i < last_messages.Length; i++)
            {
                var message = last_messages[i];
                output.SetString(SaveData.CombineBase(SaveData.k_UI_LogSnippet_Base, i), message.Text);
            }

            return output;
        }

        public void Deserialize(PropertyBag input)
        {
            Dispatcher.VerifyAccess();

            // Restore basic data
            Location = input.GetString(SaveData.k_UI_Location);
            Instruction = input.GetString(SaveData.k_UI_Instruction);
            InventoryEnabled = input.GetBool(SaveData.k_UI_InventoryEnabled);

            // Restore log messages
            Messages.Clear();
            for (int i = 0, c = input.GetInt(SaveData.k_UI_LogSnippet_Count); i < c; i++)
            {
                var message = input.GetString(SaveData.CombineBase(SaveData.k_UI_LogSnippet_Base, i));
                var model = new LogMessageModel
                {
                    Text = message,
                    TextColor = Theme.LogColorLightGray,
                    TextStyle = Theme.TextBlockDefault,
                    IsBar = false
                };

                Messages.Add(model);
            }
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
