/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Finmer.Core;
using Finmer.Gameplay;
using Finmer.Utility;
using Finmer.ViewModels;

namespace Finmer.Models
{

    public class GameUI : BaseProp
    {

        private static GameUI s_Inst;

        private bool m_EnableControls = true;
        private bool m_EnableInventory = true;
        private bool m_IsGameOver;
        private bool m_IsInCombat;
        private bool m_ConsoleOpen;

        private string m_Location = String.Empty;
        private string m_Instruction = String.Empty;
        private string m_Tooltip = String.Empty;

        private CombatStateViewModel m_CombatStateViewModel;

        public static GameUI Instance => s_Inst ?? (s_Inst = new GameUI());

        public ObservableCollection<LogMessageModel> Messages { get; } = new ObservableCollection<LogMessageModel>();
        public ObservableCollection<ChoiceButtonModel> ChoiceButtons { get; } = new ObservableCollection<ChoiceButtonModel>();
        public Dictionary<CompassDirection, string> DirectionalLinks { get; } = new Dictionary<CompassDirection, string>();

        public Dispatcher Dispatcher => Application.Current.Dispatcher;

        public CombatStateViewModel CombatStateViewModel
        {
            get => m_CombatStateViewModel;
            set
            {
                m_CombatStateViewModel = value;
                OnPropertyChanged();
            }
        }

        public bool IsGameOver
        {
            get => m_IsGameOver;
            set
            {
                m_IsGameOver = value;
                OnPropertyChanged();
            }
        }

        public bool ControlsEnabled
        {
            get => m_EnableControls;
            set
            {
                m_EnableControls = value;
                OnPropertyChanged();
            }
        }

        public bool InventoryEnabled
        {
            get => m_EnableInventory;
            set
            {
                m_EnableInventory = value;
                OnPropertyChanged();
            }
        }

        public bool IsScriptConsoleOpened
        {
            get => m_ConsoleOpen;
            set
            {
                m_ConsoleOpen = value;
                OnPropertyChanged();
            }
        }

        public bool IsScriptConsoleEnabled => GameController.DebugMode;

        public bool IsInCombat
        {
            get => m_IsInCombat;
            set
            {
                m_IsInCombat = value;
                OnPropertyChanged();
            }
        }

        public string Location
        {
            get => m_Location;
            set
            {
                m_Location = value;
                OnPropertyChanged();
            }
        }

        public string Instruction
        {
            get => m_Instruction;
            set
            {
                m_Instruction = value;
                OnPropertyChanged();
            }
        }

        public string Tooltip
        {
            get => m_Tooltip;
            set
            {
                m_Tooltip = value;
                OnPropertyChanged();
            }
        }

        public void Reset()
        {

            ClearButtons();
            ClearLog();

            Tooltip = String.Empty;
            Location = String.Empty;
            Instruction = String.Empty;
            IsGameOver = false;
            IsScriptConsoleOpened = false;
            ControlsEnabled = false;
            InventoryEnabled = false;
            //m_CombatStateViewModel = null;
            s_Inst = new GameUI();
            

        }

        public void AddButton(ChoiceButtonModel settings)
        {
            // the first time a script shows a highlighted button, show a tip about it
            if (settings.Highlight && !GameController.Session.Player.AdditionalSaveData.GetBool("tip_shown_highlight"))
            {
                GameController.Session.Player.AdditionalSaveData.SetBool("tip_shown_highlight", true);
                Log(GameController.GetString("tip_highlight_button"), Theme.LogColorHighlight);
            }

            Dispatcher.BeginInvoke(new Action(() =>
            {
                ChoiceButtons.Add(settings);
            }));
        }

        public void AddLink(CompassDirection dir, string target)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (DirectionalLinks.ContainsKey(dir))
                    DirectionalLinks.Remove(dir);
                DirectionalLinks.Add(dir, target);
            }));
        }

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

        public void PerformDirectionalLink(CompassDirection dir)
        {
            Debug.Assert(DirectionalLinks.ContainsKey(dir), "Missing CompassDirection when executing directional link");

            string target = DirectionalLinks[dir];
            if (target == null) // a callback was linked up instead of a scene name, so invoke the callback
                Debugger.Break(); // TODO
            else // travel to the new scene
                GameController.Session.SetScene(new SceneScripted(GameController.Session.ScriptContext, target));
        }

        public void ClearButtons()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ChoiceButtons.Clear();
                DirectionalLinks.Clear();
            }));
        }

        public void ClearLog()
        {
            Dispatcher.Invoke(() => Messages.Clear());
        }

    }

}
