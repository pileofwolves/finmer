/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows;
using System.Windows.Media.Animation;
using Finmer.Models;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for CombatResolveDiceTrackView.xaml
    /// </summary>
    public partial class CombatResolveDiceTrackView
    {

        private readonly TimelineCollection m_DieAppearTimeline;

        public CombatResolveDiceTrackView()
        {
            InitializeComponent();

            // Cache the animation collection for display
            m_DieAppearTimeline = (TimelineCollection)FindResource("DieAppearTimeline");
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            // Skip playing the dice animation if the animation speed is not set to Full
            if (UserConfig.CombatAnimation != UserConfig.EAnimationLevel.Full)
                return;

            // Make the die invisible to start, so there's no flickering when the delayed animation starts
            ((UIElement)sender).Opacity = 0.0;

            // Clunky :( There is no nice way to let the templated item know its index in the collection, so we'll get it the hard way
            int my_index = DiceList.ItemContainerGenerator.IndexFromContainer(((FrameworkElement)sender).TemplatedParent);

            // Compute animation offset per item. If there are many items, we shorten the offset, to make a large list appear roughly as fast as a short list.
            int num_items = DiceList.ItemContainerGenerator.Items.Count;
            double offset_per_item = 25.0 + (1.0 - Math.Min(num_items, 12) / 12.0) * 125.0;

            // Generate a new Storyboard with the appropriate animations in it.
            // This must be a new Storyboard every time for each die, so that the BeginTime can be different for each of them.
            var storyboard = new Storyboard();
            foreach (var timeline in m_DieAppearTimeline)
                storyboard.Children.Add(timeline);
            storyboard.BeginTime = TimeSpan.FromMilliseconds(offset_per_item * my_index);
            storyboard.Freeze();
            storyboard.Begin((FrameworkElement)sender);
        }

    }

}
