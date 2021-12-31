/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;

namespace Finmer.Editor
{

    public partial class FormDocumentFeat : AssetWindow
    {

        public FormDocumentFeat()
        {
            InitializeComponent();
        }

        private void FormDocumentFeat_Load(object sender, EventArgs e)
        {
            // Mark the asset as dirty if a control on the form is changed
            MakeControlsDirty(this);
        }

    }

}
