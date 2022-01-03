/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core;

namespace Finmer.Views
{

    /// <summary>
    /// Interaction logic for JournalListItemView.xaml
    /// </summary>
    public partial class JournalListItemView
    {

        private const double k_RotationMax = 3.0;

        public JournalListItemView()
        {
            InitializeComponent();

            // Randomize the rotation on each item, so it looks less repetitive
            Rotator.Angle = (CoreUtility.Rng.NextDouble() * 2.0 - 1.0) * k_RotationMax;
        }

    }

}
