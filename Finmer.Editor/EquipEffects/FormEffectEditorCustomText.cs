/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Buffs;

namespace Finmer.Editor
{

    public partial class FormEffectEditorCustomText : BaseEffectEditor
    {

        public FormEffectEditorCustomText()
        {
            InitializeComponent();
        }

        private void FormEffectEditorCustomText_Load(object sender, System.EventArgs e)
        {
            var inst = (BuffCustomTooltipText)BuffInstance;
            txtTooltip.Text = inst.TooltipText;
        }

        private void cmdOK_Click(object sender, System.EventArgs e)
        {
            var inst = (BuffCustomTooltipText)BuffInstance;
            inst.TooltipText = txtTooltip.Text;
        }

    }

}
