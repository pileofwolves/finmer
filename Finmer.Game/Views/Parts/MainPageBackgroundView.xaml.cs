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
using System.Windows.Media;
using System.Windows.Media.Animation;
using Finmer.Gameplay;
using JetBrains.Annotations;

namespace Finmer.Views
{

    public sealed class SkyColorStop : DependencyObject
    {

        /// <summary>
        /// Dependency property for Color.
        /// </summary>
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color", typeof(Color), typeof(SkyColorStop), new PropertyMetadata(default(Color)));

        /// <summary>
        /// Dependency property for TimeOfDay.
        /// </summary>
        public static readonly DependencyProperty TimeOfDayProperty = DependencyProperty.Register(
            "TimeOfDay", typeof(int), typeof(SkyColorStop), new PropertyMetadata(default(int)));

        /// <summary>
        /// .
        /// </summary>
        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        /// <summary>
        /// .
        /// </summary>
        public int TimeOfDay
        {
            get => (int)GetValue(TimeOfDayProperty);
            set => SetValue(TimeOfDayProperty, value);
        }
    }

    public sealed class SkyColorStopCollection : List<SkyColorStop>
    {
        //
    }

    /// <summary>
    /// Interaction logic for MainPageBackgroundView.xaml
    /// </summary>
    public partial class MainPageBackgroundView : INotifyPropertyChanged
    {

        /// <summary>
        /// Dependency property for Timestamps.
        /// </summary>
        public static readonly DependencyProperty TimestampsProperty = DependencyProperty.Register(
            "Timestamps", typeof(SkyColorStopCollection), typeof(MainPageBackgroundView), new PropertyMetadata(new SkyColorStopCollection()));

        /// <summary>
        /// Collection of colored clock timestamps.
        /// </summary>
        public SkyColorStopCollection Timestamps
        {
            get => (SkyColorStopCollection)GetValue(TimestampsProperty);
            set => SetValue(TimestampsProperty, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Color CurrentSkyColor => GetCurrentSkyColor();

        private readonly WeakReference<Player> m_Player;

        public MainPageBackgroundView()
        {
            InitializeComponent();

            // Session may be null in XAML designer
            var session = GameController.Session;
            if (session == null)
                return;

            // Cache the player object
            var player = session.Player;
            m_Player = new WeakReference<Player>(player);

            // Ensure we receive notifications when the game world time changes
            PropertyChangedEventManager.AddHandler(player, OnPlayerTimeChanged, nameof(Player.TimeHour));
        }

        private int GetCurrentHour()
        {
            return m_Player.TryGetTarget(out Player player) ? player.TimeHour : 0;
        }

        private Color GetCurrentSkyColor()
        {
            // If in the XAML designer, return a generic color so the gradient renders properly
            if (DesignerProperties.GetIsInDesignMode(this))
                return Color.FromRgb(119, 117, 210);

            // Cache the collection as a local once
            var stops = Timestamps;

            // Gracefully handle the edge cases where we cannot do any interpolation
            if (stops.Count == 0)
                return new Color();
            if (stops.Count == 1)
                return stops[0].Color;

            // We have colors to interpolate between. Find the stop that the clock has just passed.
            int current_hour = GetCurrentHour();
            int index1 = stops.FindLastIndex(stop => stop.TimeOfDay <= current_hour);
            if (index1 == -1)
                throw new InvalidOperationException($"No sky color stop before time {current_hour}");

            // Then find the stop after that, which is the one we're interpolating towards.
            int index2 = index1 + 1;
            if (index2 >= stops.Count)
                index2 = 0;

            // Calculate the interpolation coefficient between the two colors
            int tn = current_hour;
            int t1 = stops[index1].TimeOfDay;
            int t2 = stops[index2].TimeOfDay;
            if (t1 > t2)
                t2 += 24;
            float color_delta = (tn - t1) / (float)(t2 - t1);

            // Calculate the normalized sRGB color values for both stops
            Color c1 = stops[index1].Color;
            Color c2 = stops[index2].Color;
            float r1 = c1.R / 255.0f;
            float g1 = c1.G / 255.0f;
            float b1 = c1.B / 255.0f;
            float r2 = c2.R / 255.0f;
            float g2 = c2.G / 255.0f;
            float b2 = c2.B / 255.0f;

            // Interpolate between the two sky colors
            float r = r1 + ((r2 - r1) * color_delta);
            float g = g1 + ((g2 - g1) * color_delta);
            float b = b1 + ((b2 - b1) * color_delta);

            // Return the result
            return Color.FromRgb((byte)(r * 255.0f), (byte)(g * 255.0f), (byte)(b * 255.0f));
        }

        private void OnPlayerTimeChanged(object sender, PropertyChangedEventArgs e)
        {
            // If we got a notification for the time changing, then the sky color changes too
            OnPropertyChanged(nameof(CurrentSkyColor));
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
