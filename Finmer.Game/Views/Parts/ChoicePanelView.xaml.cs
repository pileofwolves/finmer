/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Finmer.Gameplay;
using Finmer.Models;
using Finmer.Utility;

namespace Finmer.Views
{

    public partial class ChoicePanelView
    {

        private const float k_AnimTime_FlyIn = 0.40f;
        private const float k_AnimTime_FlyOut = 0.15f;
        private const float k_AnimTime_DelayInitial = 0.175f;
        private const float k_AnimTime_DelayEach = 0.08f;

        private const float k_Button_DefaultWidth = 175.0f;

        public ChoicePanelView()
        {
            InitializeComponent();

            DataContext = GameUI.Instance;
        }

        public ObservableCollection<Button> Buttons { get; } = new ObservableCollection<Button>();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // make sure we get informed about new/existing buttons
            CollectionChangedEventManager.AddHandler(GameUI.Instance.ChoiceButtons, ChoiceButtons_CollectionChanged);

            // manually add all buttons that are already registered
            GameUI.Instance.ChoiceButtons.ForEach(AddButton);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            // unsubscribe from the message log
            CollectionChangedEventManager.RemoveHandler(GameUI.Instance.ChoiceButtons, ChoiceButtons_CollectionChanged);
        }

        private void ChoiceButtons_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object item in e.NewItems)
                        AddButton((ChoiceButtonModel)item);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    Buttons.ForEach(btn => ButtonFlyAnimation(btn, false, 0.0));
                    Buttons.Clear();
                    break;
            }
        }

        private void AddButton(ChoiceButtonModel model)
        {
            // Create button control
            var btn = new Button
            {
                Width = Math.Round(model.Width * k_Button_DefaultWidth),
                Height = 40,
                Content = model.Label,
                DataContext = model
            };

            // Apply highlight color
            if (model.Highlight)
                btn.Foreground = new SolidColorBrush(Theme.LogColorHighlight);

            // Apply fly-in animation
            ButtonFlyAnimation(btn, true, k_AnimTime_DelayInitial + Buttons.Count * k_AnimTime_DelayEach);

            // Apply callback delegates
            btn.Click += (sender,  args) => GameController.Session.RunSceneEvent(ESceneEvent.Turn, model.Choice);
            btn.MouseEnter += (sender, args) => GameUI.Instance.Tooltip = model.Tooltip;
            btn.MouseLeave += (sender, args) => GameUI.Instance.Tooltip = GameUI.Instance.Instruction;
            Buttons.Add(btn);
        }

        private void txtConsole_KeyDown(object sender, KeyEventArgs e)
        {
            // Safeguard: must be in debug mode to be able to do this
            if (!GameController.DebugMode)
                return;

            // Activate when pressing Enter
            if (e.Key != Key.Enter) 
                return;

            // Run the input script
            var command = ConsoleInput.Text;
            var script_context = GameController.Session.ScriptContext;
            lock (script_context)
            {
                if (script_context.LoadScript(command, "DebugConsole"))
                    script_context.RunProtectedCall(0, 0);
            }

            // Clear UI
            ConsoleInput.Text = String.Empty;
            e.Handled = true;
        }

        private void ButtonFlyAnimation(Button button, bool enter, double delay)
        {
            // Prevent clicking the button while it's animating
            button.IsHitTestVisible = false;

            // Determine length of the animation
            var transform = new TranslateTransform();
            var length = new Duration(TimeSpan.FromSeconds(enter ? k_AnimTime_FlyIn : k_AnimTime_FlyOut));
            DoubleAnimation anim_slide;

            // Speed up animations when debugging
            if (GameController.DebugMode)
            {
                length = new Duration(TimeSpan.FromSeconds(0.05));
                delay = 0.0;
            }

            if (enter)
            {
                // Prepare fly-up animation
                transform.Y = 200;
                anim_slide = new DoubleAnimation(0, length)
                {
                    EasingFunction = new ElasticEase
                    {
                        EasingMode = EasingMode.EaseOut,
                        Oscillations = 1,
                        Springiness = 8f
                    },
                    BeginTime = TimeSpan.FromSeconds(delay)
                };

                // Re-enable interaction once the animation has finished
                anim_slide.Completed += (sender, args) =>
                    button.IsHitTestVisible = true;
            }
            else
            {
                transform.X = 0;
                anim_slide = new DoubleAnimation(100, length);

                // Get absolute position and size
                Point pos = button.TransformToVisual(ButtonExitAnimCanvas).Transform(new Point());

                // Remove from previous parent and readd to the overlay container so it can continue animating after being removed from the collection.
                // Yes, this is super hacky, but it works :3
                Buttons.Remove(button);
                ButtonExitAnimCanvas.Children.Add(button);

                // Offset the button so it appears to be in the exact same position as before
                button.Margin = new Thickness(pos.X, pos.Y, 4, 0);

                // Once animation completes, remove the button from the canvas, which should make it eligible for GC
                anim_slide.Completed += (sender, args) =>
                    ButtonExitAnimCanvas.Children.Remove(button);
            }

            // Run the animation
            button.RenderTransform = transform;
            transform.BeginAnimation(TranslateTransform.YProperty, anim_slide, HandoffBehavior.SnapshotAndReplace);
        }

    }

}
