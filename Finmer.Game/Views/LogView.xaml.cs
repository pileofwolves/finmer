/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
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

        public LogView()
        {
            InitializeComponent();
        }

        public ObservableCollection<LogMessageModel> LogSource { get; set; }
        public bool IsCombatLog { get; set; }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // sanity checks: don't crash the XAML editor, and this must be run on UI thread
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            Debug.Assert(Dispatcher != null);
            Dispatcher.VerifyAccess();

            // take the opportunity to cut down on the log size, and readd some old messages
            LogSource.CapAtCount(100);
            foreach (LogMessageModel item in LogSource)
                AddParagraphForMessage(item, false);

            // subscribe to future change events
            // EDIT: use weak event pattern, or else LogSource never loses ref to this view, --> big memory leak
            CollectionChangedEventManager.AddHandler(LogSource, Messages_CollectionChanged);

            // scroll the message log to the bottom, to help avoid confusion
            if (IsCombatLog)
                ScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            var scroll_viewer = ScrollViewer.GetVisualChild<ScrollViewer>();
            scroll_viewer?.ScrollToEnd();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            // unsubscribe from the message log
            CollectionChangedEventManager.RemoveHandler(LogSource, Messages_CollectionChanged);

            // as an extra safety measure, explicitly discard the document (which takes up most memory)
            Document.Blocks.Clear();
        }

        private void Messages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // expect UI thread
            Dispatcher.VerifyAccess();

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object new_item in e.NewItems)
                        AddParagraphForMessage((LogMessageModel)new_item, true);

                    break;

                case NotifyCollectionChangedAction.Reset:
                    Document.Blocks.Clear();
                    break;

                case NotifyCollectionChangedAction.Remove:
                    // ASSUME these are removed from the top down by GameUI!
                    Document.Blocks.Remove(Document.Blocks.FirstBlock);
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        private void AddParagraphForMessage(LogMessageModel vm, bool animate)
        {
            UIElement elem;

            // create a frozen brush, hopefully that helps perf a bit
            var brush = new SolidColorBrush(vm.TextColor);
            brush.Freeze();

            if (vm.IsBar)
            {
                // horizontal splitter
                elem = new Rectangle
                {
                    Margin = new Thickness(6, 10, 6, 10),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Fill = brush,
                    Height = 2
                };
            }
            else
            {
                // text entry
                Debug.Assert(vm.Text != null);
                Debug.Assert(vm.TextStyle != null);

                elem = new TextBlock
                {
                    Text = vm.Text,
                    Style = vm.TextStyle,
                    Foreground = brush,
                    Margin = vm.Margin,
                    TextWrapping = TextWrapping.Wrap
                };
            }

            // add to visual tree
            Document.Blocks.Add(new BlockUIContainer(elem));

            // do animation if not reloading the page (i.e. coming from another page)
            if (animate)
            {
                if (vm.IsBar)
                    LogSplitAnimation(elem);
                else
                    LogEntryAnimation(elem);
            }
        }

        private void LogEntryAnimation(UIElement container)
        {
            var transform_translate = new TranslateTransform { Y = 64 };

            var duration_text = new Duration(TimeSpan.FromSeconds(0.5));
            var duration_scroll = new Duration(TimeSpan.FromSeconds(0.7));

            // apply opacity and slide animations
            container.RenderTransform = transform_translate;
            container.RenderTransformOrigin = new Point(0, 0);
            container.UpdateLayout();
            container.BeginAnimation(OpacityProperty,
                new DoubleAnimation(0.0, 1.0, duration_text)
                {
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                }, HandoffBehavior.SnapshotAndReplace);
            transform_translate.BeginAnimation(TranslateTransform.YProperty,
                new DoubleAnimation(0, duration_text)
                {
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                }, HandoffBehavior.SnapshotAndReplace);

            // need to call this because we just added a text block, and ExtentHeight & ViewportHeight will be outdated
            ScrollViewer.UpdateLayout();

            // get the actual scrollviewer owned by the flowdoc, so we can scroll it
            var underlying_scroll_view = ScrollViewer.GetVisualChild<ScrollViewer>();
            if (underlying_scroll_view == null) return;

            // smoothly scroll to the bottom
            var anim = new DoubleAnimation(underlying_scroll_view.VerticalOffset, underlying_scroll_view.ExtentHeight - underlying_scroll_view.ViewportHeight, duration_scroll)
            {
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            underlying_scroll_view.BeginAnimation(ScrollViewerBehavior.VerticalOffsetProperty, anim, HandoffBehavior.SnapshotAndReplace);
        }

        private void LogSplitAnimation(UIElement container)
        {
            var transform_scale = new ScaleTransform(0, 1);
            container.RenderTransform = transform_scale;

            var duration_scale = new Duration(TimeSpan.FromSeconds(1.0));
            transform_scale.BeginAnimation(ScaleTransform.ScaleXProperty,
                new DoubleAnimation(0.0, 1.0, duration_scale)
                {
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                }, HandoffBehavior.SnapshotAndReplace);
        }

    }

}
