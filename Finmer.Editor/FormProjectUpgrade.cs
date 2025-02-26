/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Drawing;
using System.Windows.Forms;
using Finmer.Core.Serialization;

namespace Finmer.Editor
{

    public partial class FormProjectUpgrade : Form
    {

        public FormProjectUpgrade()
        {
            InitializeComponent();

            // Generate the default warning icon
            picWarning.Image = SystemIcons.Exclamation.ToBitmap();

            // Show the actual version numbers, for clarity
            lblVersion.Text = $"Project version {Program.ActiveFurball.Metadata.FormatVersion} - Latest version {FurballFileDevice.k_LatestVersion}";
        }

    }

}
