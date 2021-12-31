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
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Finmer.Models;
using Finmer.Utility;

namespace Finmer.Views
{

    public partial class LogView
    {

        /// <summary>
        /// Dependency property for MessageSource.
        /// </summary>
        public static readonly DependencyProperty MessageSourceProperty = DependencyProperty.Register(
            "MessageSource", typeof(ObservableCollection<LogMessageModel>), typeof(LogView), new PropertyMetadata(null,
                (o, args) =>
                {
                    var self = (LogView)o;
                    self.LogView_OnMessageSourceChanged(args);
                }));

        /// <summary>
        /// The collection of messages to display in this log.
        /// </summary>
        public ObservableCollection<LogMessageModel> MessageSource
        {
            get => (ObservableCollection<LogMessageModel>)GetValue(MessageSourceProperty);
            set => SetValue(MessageSourceProperty, value);
        }

        private ScrollViewer m_FlowDocumentScrollViewer;

        public LogView()
        {
            InitializeComponent();
        }

        private void LogView_OnLoaded(object sender, RoutedEventArgs e)
        {
            // Avoid running this in the XAML designer
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            // Find and cache the ScrollViewer owned by the FlowDocumentScrollViewer so we can talk to it directly
            m_FlowDocumentScrollViewer = this.GetVisualChild<ScrollViewer>();

            // Automatically scroll the message log to the bottom
            m_FlowDocumentScrollViewer.ScrollToEnd();
        }

        private void LogView_OnMessageSourceChanged(DependencyPropertyChangedEventArgs args)
        {
            // Avoid running this in the XAML designer
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            // Unsubscribe the old context from the message log
            if (args.OldValue is INotifyCollectionChanged old_source)
                CollectionChangedEventManager.RemoveHandler(old_source, MessageSource_OnCollectionChanged);

            // Re-add old messages from the source back to the log, for continuity
            Dispatcher.VerifyAccess();
            foreach (LogMessageModel item in MessageSource)
                AddParagraphForMessage(item, false);

            // Subscribe to future change events
            CollectionChangedEventManager.AddHandler(MessageSource, MessageSource_OnCollectionChanged);

            // Automatically scroll the message log to the bottom
            // Note: This callback may occur before the Loaded event fired, so the reference may still be null
            m_FlowDocumentScrollViewer?.ScrollToEnd();
        }

        private void MessageSource_OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // This should run on the UI thread
            Dispatcher.VerifyAccess();

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    // Add new paragraphs
                    foreach (object new_item in e.NewItems)
                        AddParagraphForMessage((LogMessageModel)new_item, true);

                    break;

                case NotifyCollectionChangedAction.Reset:
                    // Wipe the log
                    Document.Blocks.Clear();
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        private void AddParagraphForMessage(LogMessageModel vm, bool animate)
        {
            UIElement element;

            // Use a frozen brush since we won't change it after this point
            var brush = new SolidColorBrush(vm.TextColor);
            brush.Freeze();

            if (vm.IsBar)
            {
                // Create horizontal splitter
                element = new Rectangle
                {
                    Margin = new Thickness(6, 10, 6, 10),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Fill = brush,
                    Height = 2
                };
            }
            else
            {
                // Create text entry
                Debug.Assert(vm.Text != null);
                Debug.Assert(vm.TextStyle != null);
                element = new TextBlock
                {
                    Text = vm.Text,
                    Style = vm.TextStyle,
                    Foreground = brush,
                    Margin = new Thickness(0.0, 0.0, 0.0, 12.0),
                    TextWrapping = TextWrapping.Wrap
                };
            }

            // Add to visual tree
            Document.Blocks.Add(new BlockUIContainer(element));

            // Do animation if that was requested
            if (animate)
            {
                if (vm.IsBar)
                    LogSplitAnimation(element);
                else
                    LogEntryAnimation(element);
            }
        }

        private void LogEntryAnimation(UIElement container)
        {
            // Skip animations if this view is not yet displayed on-screen
            if (m_FlowDocumentScrollViewer == null)
                return;

            // Prepare an opacity and fly-in animation
            var translation = new TranslateTransform { X = 64 };
            var duration = new Duration(TimeSpan.FromSeconds(0.5));
            container.RenderTransform = translation;
            container.RenderTransformOrigin = new Point(0, 0);

            // Play the animations
            container.BeginAnimation(OpacityProperty,
                new DoubleAnimation(0.0, 1.0, duration)
                {
                    EasingFunction = new QuadraticEase
                    {
                        EasingMode = EasingMode.EaseInOut
                    }
                }, HandoffBehavior.SnapshotAndReplace);
            translation.BeginAnimation(TranslateTransform.XProperty,
                new DoubleAnimation(0, duration)
                {
                    EasingFunction = new QuadraticEase
                    {
                        EasingMode = EasingMode.EaseOut
                    }
                }, HandoffBehavior.SnapshotAndReplace);

            // Scroll down
            ScrollToBottomAnimation();
        }

        private void LogSplitAnimation(UIElement container)
        {
            // Skip animations if this view is not yet displayed on-screen
            if (m_FlowDocumentScrollViewer == null)
                return;

            // Prepare an animation for extending the splitter
            var transform_scale = new ScaleTransform(0, 1);
            var duration_scale = new Duration(TimeSpan.FromSeconds(1.0));
            container.RenderTransform = transform_scale;

            // Play the animation
            transform_scale.BeginAnimation(ScaleTransform.ScaleXProperty,
                new DoubleAnimation(0.0, 1.0, duration_scale)
                {
                    EasingFunction = new CubicEase
                    {
                        EasingMode = EasingMode.EaseOut
                    }
                }, HandoffBehavior.SnapshotAndReplace);

            // Scroll down
            ScrollToBottomAnimation();
        }

        private void ScrollToBottomAnimation()
        {
            // Ensure layout is updated, we just added a text block, so ExtentHeight and ViewportHeight will be outdated
            m_FlowDocumentScrollViewer.UpdateLayout();

            // Prepare an animation for smoothly scrolling to the bottom of the view
            double from = m_FlowDocumentScrollViewer.VerticalOffset;
            double to = m_FlowDocumentScrollViewer.ExtentHeight - m_FlowDocumentScrollViewer.ViewportHeight;
            var duration_scroll = new Duration(TimeSpan.FromSeconds(0.65));
            var anim = new DoubleAnimation(from, to, duration_scroll)
            {
                EasingFunction = new CubicEase
                {
                    EasingMode = EasingMode.EaseOut
                }
            };

            // Play the animation
            m_FlowDocumentScrollViewer.BeginAnimation(ScrollViewerBehavior.VerticalOffsetProperty, anim, HandoffBehavior.SnapshotAndReplace);
        }

    }

}
