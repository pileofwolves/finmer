/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Finmer.Utility;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for CarouselView.xaml
    /// </summary>
    public partial class CarouselView
    {

        public CarouselView()
        {
            InitializeComponent();
        }

        public event EventHandler NavigationComplete;

        /// <summary>
        /// Begins a transfer animation to another page.
        /// </summary>
        /// <param name="target">The <seealso cref="Control" /> to use as the new page.</param>
        /// <param name="anim">The animation style to use.</param>
        /// <param name="fade"></param>
        public void Navigate(Control target, ENavigatorAnimation anim, bool fade = false)
        {
            if (anim == ENavigatorAnimation.Instant)
            {
                oldGrid.Content = null;
                oldGrid.IsHitTestVisible = false;
                newGrid.Content = target;
                newGrid.IsHitTestVisible = true;
                NavigationComplete?.Invoke(this, EventArgs.Empty);
                return;
            }

            oldGrid.Content = newGrid.Content;
            oldGrid.IsHitTestVisible = false;
            newGrid.Content = target;
            newGrid.IsHitTestVisible = false;

            var old_transform = new TranslateTransform();
            var new_transform = new TranslateTransform();

            // Attach the transforms to their respective grids
            oldGrid.RenderTransform = old_transform;
            newGrid.RenderTransform = new_transform;
            var anim_length = new Duration(TimeSpan.FromSeconds(0.4));

            // Determine the actual animation to play
            DoubleAnimation old_anim, new_anim;
            switch (anim)
            {
                case ENavigatorAnimation.SlideLeft:
                    new_transform.X = ActualWidth;
                    old_anim = new DoubleAnimation(-ActualWidth, anim_length);
                    new_anim = new DoubleAnimation(0, anim_length);
                    break;

                case ENavigatorAnimation.SlideRight:
                    new_transform.X = -ActualWidth;
                    old_anim = new DoubleAnimation(ActualWidth, anim_length);
                    new_anim = new DoubleAnimation(0, anim_length);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(anim));
            }

            // Apply the easing functions
            IEasingFunction easing = new CubicEase
            {
                EasingMode = EasingMode.EaseOut
            };
            old_anim.EasingFunction = easing;
            new_anim.EasingFunction = easing;

            // When animation is complete, destroy the old page and re-enable input
            new_anim.Completed += (sender, args) =>
            {
                oldGrid.Content = null;
                newGrid.IsHitTestVisible = true;
                NavigationComplete?.Invoke(this, EventArgs.Empty);
            };

            // Fade in/out animations
            if (fade)
            {
                var fade_in = new DoubleAnimation(0.0, 1.0, anim_length);
                var fade_out = new DoubleAnimation(1.0, 0.0, anim_length);
                fade_in.EasingFunction = easing;
                fade_out.EasingFunction = easing;
                oldGrid.BeginAnimation(OpacityProperty, fade_out);
                newGrid.BeginAnimation(OpacityProperty, fade_in);
            }

            // Begin animation
            old_transform.BeginAnimation(TranslateTransform.XProperty, old_anim);
            new_transform.BeginAnimation(TranslateTransform.XProperty, new_anim);
        }

    }

}
