/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows;
using System.Windows.Controls;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for ItemListView.xaml
    /// </summary>
    public partial class ItemListView
    {

        /// <summary>
        /// Dependency property for ItemOptionsTemplate.
        /// </summary>
        public static readonly DependencyProperty ItemOptionsTemplateProperty = DependencyProperty.Register(
            "ItemOptionsTemplate", typeof(ControlTemplate), typeof(ItemListView), new PropertyMetadata(null));

        /// <summary>
        /// Template shown at the bottom of the item widget when selected by the user.
        /// </summary>
        public ControlTemplate ItemOptionsTemplate
        {
            get => (ControlTemplate)GetValue(ItemOptionsTemplateProperty);
            set => SetValue(ItemOptionsTemplateProperty, value);
        }

        public ItemListView()
        {
            InitializeComponent();
        }

    }

}
