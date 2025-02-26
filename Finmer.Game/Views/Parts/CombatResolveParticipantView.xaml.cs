﻿/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Finmer.ViewModels;
using JetBrains.Annotations;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for CombatResolveDetailView.xaml
    /// </summary>
    public partial class CombatResolveParticipantView : INotifyPropertyChanged
    {

        /// <summary>
        /// Describes the display context for the participant view.
        /// </summary>
        public enum EParticipantDisplayMode
        {
            Attacker,
            Defender
        }

        /// <summary>
        /// Dependency property for ParticipantDisplayMode.
        /// </summary>
        public static readonly DependencyProperty ParticipantDisplayModeProperty = DependencyProperty.Register(
            nameof(ParticipantDisplayMode), typeof(EParticipantDisplayMode), typeof(CombatResolveParticipantView),
            new PropertyMetadata(EParticipantDisplayMode.Attacker));

        /// <summary>
        /// Specifies the context in which this view is being used.
        /// </summary>
        public EParticipantDisplayMode ParticipantDisplayMode
        {
            get => (EParticipantDisplayMode)GetValue(ParticipantDisplayModeProperty);
            set => SetValue(ParticipantDisplayModeProperty, value);
        }

        /// <summary>
        /// Shorthand for checking ParticipantDisplayMode.
        /// </summary>
        public bool IsAttacker => ParticipantDisplayMode == EParticipantDisplayMode.Attacker;

        /// <summary>
        /// Shorthand for checking ParticipantDisplayMode.
        /// </summary>
        public bool IsDefender => !IsAttacker;

        /// <summary>
        /// Returns a context-appropriate string to display as label for the dice roll total.
        /// </summary>
        public string TotalLabel => GetTotalLabel();

        /// <summary>
        /// The horizontal alignment to apply to various widgets.
        /// </summary>
        public HorizontalAlignment ParticipantHorizontalAlignment => IsAttacker ? HorizontalAlignment.Left : HorizontalAlignment.Right;

        private CombatResolveParticipantViewModel m_ViewModel;

        public CombatResolveParticipantView()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void View_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is INotifyPropertyChanged old_context)
                PropertyChangedEventManager.RemoveHandler(old_context, ViewModel_OnPropertyChanged, nameof(CombatResolveParticipantViewModel.Label));

            if (e.NewValue is INotifyPropertyChanged new_context)
                PropertyChangedEventManager.AddHandler(new_context, ViewModel_OnPropertyChanged, nameof(CombatResolveParticipantViewModel.Label));

            // Ensure the TotalLabel is updated at least once
            m_ViewModel = e.NewValue as CombatResolveParticipantViewModel;
            OnPropertyChanged(nameof(TotalLabel));
        }

        private void ViewModel_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(CombatResolveParticipantViewModel.Label)))
                OnPropertyChanged(nameof(TotalLabel));
        }

        private string GetTotalLabel()
        {
            // This function may be called from the XAML designer, in which case the view-model is not set
            if (m_ViewModel == null)
                return "Total";

            return m_ViewModel.Label;
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
