/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Finmer.Core.Assets;

namespace Finmer.Editor
{

    public partial class FormFeatBrowser : Form
    {

        private readonly List<Guid> m_Feats;

        public FormFeatBrowser(List<Guid> list)
        {
            InitializeComponent();
            m_Feats = list;
        }

        private void frmPickFeats_Load(object sender, EventArgs e)
        {
            Program.ActiveFurball.Assets.OfType<AssetFeat>().ForEach(feat =>
            {
                var item = new ListViewItem();
                item.Text = feat.Name;
                item.Tag = feat;
                item.Checked = m_Feats.Contains(feat.ID);
                lsvFeats.Items.Add(item);
            });
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            m_Feats.Clear();
            foreach (ListViewItem item in lsvFeats.Items)
                if (item.Checked)
                    m_Feats.Add(((AssetFeat)item.Tag).ID);

            DialogResult = DialogResult.OK;
            Close();
        }

    }

}
