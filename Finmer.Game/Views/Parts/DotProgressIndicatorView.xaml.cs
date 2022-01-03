/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using JetBrains.Annotations;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for DotProgressIndicatorView.xaml
    /// </summary>
    public partial class DotProgressIndicatorView : INotifyPropertyChanged
    {

        /// <summary>
        /// Dependency property for DotCount.
        /// </summary>
        public static readonly DependencyProperty DotCountProperty = DependencyProperty.Register(
            "DotCount", typeof(int), typeof(DotProgressIndicatorView), new PropertyMetadata(0, (o, args) =>
            {
                var self = (DotProgressIndicatorView)o;
                self.OnPropertyChanged(nameof(DotCount));
                self.OnPropertyChanged(nameof(DotCollection));
            }));

        /// <summary>
        /// Dependency property for FilledCount.
        /// </summary>
        public static readonly DependencyProperty FilledCountProperty = DependencyProperty.Register(
            "FilledCount", typeof(int), typeof(DotProgressIndicatorView), new PropertyMetadata(0, (o, args) =>
            {
                var self = (DotProgressIndicatorView)o;
                self.OnPropertyChanged(nameof(FilledCount));
                self.OnPropertyChanged(nameof(DotCollection));
            }));

        /// <summary>
        /// Gets or sets the total number of dots to display in this progress indicator.
        /// </summary>
        public int DotCount
        {
            get => (int)GetValue(DotCountProperty);
            set => SetValue(DotCountProperty, Math.Max(value, 0));
        }

        /// <summary>
        /// Gets or sets the number of dots to display as filled.
        /// </summary>
        public int FilledCount
        {
            get => (int)GetValue(FilledCountProperty);
            set => SetValue(FilledCountProperty, Math.Max(Math.Min(value, DotCount), 0));
        }

        /// <summary>
        /// Returns a collection that represents the display, where a 'true' value is a filled dot and 'false' is an open dot.
        /// </summary>
        public IEnumerable<bool> DotCollection
        {
            get
            {
                List<bool> result = new List<bool>(DotCount);

                // Add filled dots
                for (var i = 0; i < FilledCount; i++)
                    result.Add(true);

                // Add open dots
                for (var i = 0; i < DotCount - FilledCount; i++)
                    result.Add(false);

                return result;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DotProgressIndicatorView()
        {
            InitializeComponent();
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
