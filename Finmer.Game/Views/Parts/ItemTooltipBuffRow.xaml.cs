/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for ItemTooltipBuffRow.xaml
    /// </summary>
    public partial class ItemTooltipBuffRow
    {

        /// <summary>
        /// Dependency property for ItemOptionsTemplate.
        /// </summary>
        public static readonly DependencyProperty RowWidgetProperty = DependencyProperty.Register(
            "RowWidget", typeof(UIElement), typeof(ItemTooltipBuffRow), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property for RowLabel.
        /// </summary>
        public static readonly DependencyProperty RowLabelProperty = DependencyProperty.Register(
            "RowLabel", typeof(string), typeof(ItemTooltipBuffRow), new PropertyMetadata(String.Empty));

        /// <summary>
        /// Arbitrary user control shown besides the row label.
        /// </summary>
        public UIElement RowWidget
        {
            get => (UIElement)GetValue(RowWidgetProperty);
            set => SetValue(RowWidgetProperty, value);
        }

        /// <summary>
        /// Gets or sets the label to prefix to this row.
        /// </summary>
        public string RowLabel
        {
            get => (string)GetValue(RowLabelProperty);
            set => SetValue(RowLabelProperty, value);
        }

        public ItemTooltipBuffRow()
        {
            InitializeComponent();
        }

    }

}
