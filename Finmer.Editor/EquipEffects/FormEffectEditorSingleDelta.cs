/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using Finmer.Core.Buffs;

namespace Finmer.Editor
{

    public partial class FormEffectEditorSingleDelta : BaseEffectEditor
    {

        public FormEffectEditorSingleDelta()
        {
            InitializeComponent();
        }

        private void FormEffectEditorSingleDelta_Load(object sender, System.EventArgs e)
        {
            var inst = (SingleDeltaBuff)BuffInstance;
            nudDelta.Value = inst.Delta;
        }

        private void cmdOK_Click(object sender, System.EventArgs e)
        {
            var inst = (SingleDeltaBuff)BuffInstance;
            inst.Delta = (int)nudDelta.Value;
        }

    }

}
