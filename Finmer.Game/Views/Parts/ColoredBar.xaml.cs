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
using System.Windows.Media;
using JetBrains.Annotations;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for ColoredBar.xaml.
    /// </summary>
    public partial class ColoredBar : INotifyPropertyChanged
    {

        public static readonly RoutedEvent BarValueChangedEvent = EventManager.RegisterRoutedEvent(
            "BarValueChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ColoredBar));

        public static readonly DependencyProperty InnerColorProperty = DependencyProperty.Register(
            "InnerColor", typeof(Color), typeof(ColoredBar), new PropertyMetadata(Color.FromRgb(255, 255, 255)));

        public static readonly DependencyProperty OuterColorProperty = DependencyProperty.Register(
            "OuterColor", typeof(Color), typeof(ColoredBar), new PropertyMetadata(Color.FromRgb(0, 0, 0)));

        public static readonly DependencyProperty ValueFormatProperty = DependencyProperty.Register(
            "ValueFormat", typeof(string), typeof(ColoredBar), new PropertyMetadata("{0} / {2}",
                (o, args) =>
                {
                    var self = (ColoredBar)o;
                    self.OnPropertyChanged(nameof(ValueFormat));
                    self.OnPropertyChanged(nameof(ValueText));
                }));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(float), typeof(ColoredBar), new FrameworkPropertyMetadata(
                (o, args) =>
                {
                    var self = (ColoredBar)o;
                    self.OnPropertyChanged(nameof(Value));
                    self.OnPropertyChanged(nameof(ValueText));
                    self.OnPropertyChanged(nameof(DesiredBarWidth));
                    self.FillArea.RaiseEvent(new RoutedEventArgs(BarValueChangedEvent));
                }));

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            "Minimum", typeof(float), typeof(ColoredBar), new PropertyMetadata(default(float),
                (o, args) =>
                {
                    var self = (ColoredBar)o;
                    self.OnPropertyChanged(nameof(Minimum));
                    self.OnPropertyChanged(nameof(ValueText));
                    self.OnPropertyChanged(nameof(DesiredBarWidth));
                    self.FillArea.RaiseEvent(new RoutedEventArgs(BarValueChangedEvent));
                }));

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum", typeof(float), typeof(ColoredBar), new PropertyMetadata(default(float),
                (o, args) =>
                {
                    var self = (ColoredBar)o;
                    self.OnPropertyChanged(nameof(Maximum));
                    self.OnPropertyChanged(nameof(ValueText));
                    self.OnPropertyChanged(nameof(DesiredBarWidth));
                    self.FillArea.RaiseEvent(new RoutedEventArgs(BarValueChangedEvent));
                }));

        public ColoredBar()
        {
            InitializeComponent();
            FillArea.DataContext = this;
        }

        public Color InnerColor
        {
            get => (Color)GetValue(InnerColorProperty);
            set => SetValue(InnerColorProperty, value);
        }

        public Color OuterColor
        {
            get => (Color)GetValue(OuterColorProperty);
            set => SetValue(OuterColorProperty, value);
        }

        public string ValueFormat
        {
            get => (string)GetValue(ValueFormatProperty);
            set => SetValue(ValueFormatProperty, value);
        }

        public float Value
        {
            get => (float)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public float Minimum
        {
            get => (float)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public float Maximum
        {
            get => (float)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public double DesiredBarWidth => Math.Abs(Maximum - Minimum) <= Single.Epsilon
            ? 0.0
            : Math.Min((Value - Minimum) / (Maximum - Minimum), 1.0);

        public string ValueText => String.Format(ValueFormat, Value, Minimum, Maximum);

        public event PropertyChangedEventHandler PropertyChanged;

        public event RoutedEventHandler BarValueChanged
        {
            add => AddHandler(BarValueChangedEvent, value);
            remove => RemoveHandler(BarValueChangedEvent, value);
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
