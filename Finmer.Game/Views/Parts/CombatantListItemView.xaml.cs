/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using Finmer.ViewModels;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for CombatantListItemView.xaml
    /// </summary>
    public partial class CombatantListItemView
    {

        public static readonly RoutedEvent HealthLostEvent =
            EventManager.RegisterRoutedEvent("HealthLost", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(CombatantListItemView));

        public static readonly RoutedEvent HealthGainedEvent =
            EventManager.RegisterRoutedEvent("HealthGained", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(CombatantListItemView));

        private int m_LastKnownHealth;
        private WeakReference<CombatParticipantViewModel> m_CurrentViewModel;

        public CombatantListItemView()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler HealthLost
        {
            add => AddHandler(HealthLostEvent, value);
            remove => RemoveHandler(HealthLostEvent, value);
        }

        public event RoutedEventHandler HealthGained
        {
            add => AddHandler(HealthGainedEvent, value);
            remove => RemoveHandler(HealthGainedEvent, value);
        }

        private void CombatantListItemView_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            // Make sure the new data context is actually a participant viewmodel, since that's all we're interested in
            CombatParticipantViewModel vm = DataContext as CombatParticipantViewModel;
            Debug.Assert(vm != null);

            // Unsubscribe from the previous DataContext's PropertyChanged event
            if (m_CurrentViewModel != null && m_CurrentViewModel.TryGetTarget(out CombatParticipantViewModel old_vm))
                PropertyChangedEventManager.RemoveHandler(old_vm, ViewModel_PropertyChanged, String.Empty);
            
            // Subscribe to property change events from the new CombatParticipantViewModel, so that we can be notified
            // if health changes and play the appropriate animation.
            m_CurrentViewModel = new WeakReference<CombatParticipantViewModel>(vm);
            m_LastKnownHealth = vm.Health;
            PropertyChangedEventManager.AddHandler(vm, ViewModel_PropertyChanged, String.Empty);
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!m_CurrentViewModel.TryGetTarget(out CombatParticipantViewModel vm))
                return;

            if (!e.PropertyName.Equals("Health"))
                return;

            // dispatch RoutedEvent to play animation on main thread if we just lost or gained health
            if (m_LastKnownHealth > vm.Health)
                Dispatcher.BeginInvoke(new Action(() => RaiseEvent(new RoutedEventArgs(HealthLostEvent, this))));
            if (m_LastKnownHealth < vm.Health)
                Dispatcher.BeginInvoke(new Action(() => RaiseEvent(new RoutedEventArgs(HealthGainedEvent, this))));

            m_LastKnownHealth = vm.Health;
        }

    }

}
