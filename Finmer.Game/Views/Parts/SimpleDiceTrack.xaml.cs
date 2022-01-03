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
using System.Windows;
using Finmer.Gameplay.Combat;
using JetBrains.Annotations;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for SimpleDiceTrack.xaml
    /// </summary>
    public partial class SimpleDiceTrack : INotifyPropertyChanged
    {

        /// <summary>
        /// Describes the type of die to show on a track.
        /// </summary>
        public enum EDiceStyle
        {
            Attack,
            Defense
        }

        /// <summary>
        /// Dependency property for DiceStyle.
        /// </summary>
        public static readonly DependencyProperty DiceStyleProperty = DependencyProperty.Register(
            "DiceStyle", typeof(EDiceStyle), typeof(SimpleDiceTrack), new PropertyMetadata(EDiceStyle.Attack, (o, args) =>
            {
                // Notify property system that downstream properties have also changed
                var self = (SimpleDiceTrack)o;
                self.OnPropertyChanged(nameof(CombatDieFace));
            }));

        /// <summary>
        /// Dependency property for DiceCount.
        /// </summary>
        public static readonly DependencyProperty DiceCountProperty = DependencyProperty.Register(
            "DiceCount", typeof(int), typeof(SimpleDiceTrack), new PropertyMetadata(0, (o, args) =>
            {
                // Notify property system that downstream properties have also changed
                var self = (SimpleDiceTrack)o;
                self.OnPropertyChanged(nameof(EffectiveDiceCount));
                self.OnPropertyChanged(nameof(IsDiceOverflowing));
            }));

        /// <summary>
        /// Gets or sets the type of dice to show on this track.
        /// </summary>
        public EDiceStyle DiceStyle
        {
            get => (EDiceStyle)GetValue(DiceStyleProperty);
            set => SetValue(DiceStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the number of dice to show on this track.
        /// </summary>
        public int DiceCount
        {
            get => (int)GetValue(DiceCountProperty);
            set => SetValue(DiceCountProperty, value);
        }

        /// <summary>
        /// Returns DiceStyle as an appropriate combat die face, for display purposes.
        /// </summary>
        public EDieFace CombatDieFace => DieStyleToCombatDieFace(DiceStyle);

        /// <summary>
        /// Returns the actual number of dice to display on the track.
        /// </summary>
        public int EffectiveDiceCount => IsDiceOverflowing ? 1 : Math.Abs(DiceCount);

        /// <summary>
        /// Indicates whether there are too many dice to be displayed and the display should be simplified instead.
        /// </summary>
        public bool IsDiceOverflowing => Math.Abs(DiceCount) >= 5;

        /// <summary>
        /// Indicates whether the dice total is negative.
        /// </summary>
        public bool IsNegative => DiceCount < 0;

        public SimpleDiceTrack()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Converts the given track die style to an equivalent combat die face.
        /// </summary>
        private static EDieFace DieStyleToCombatDieFace(EDiceStyle style)
        {
            switch (style)
            {
                case EDiceStyle.Attack:
                    return EDieFace.Attack;

                case EDiceStyle.Defense:
                    return EDieFace.Defense;

                default:
                    throw new ArgumentException(nameof(style));
            }
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
