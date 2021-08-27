/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for ItemBoxView.xaml
    /// </summary>
    public partial class ItemBoxView
    {

        public static readonly DependencyProperty BoxSelectedProperty = DependencyProperty.Register(
            "BoxSelected", typeof(bool), typeof(ItemBoxView), new PropertyMetadata(false));

        public ItemBoxView()
        {
            InitializeComponent();
        }

        public bool BoxSelected
        {
            get => (bool)GetValue(BoxSelectedProperty);
            set => SetValue(BoxSelectedProperty, value);
        }

    }

}
