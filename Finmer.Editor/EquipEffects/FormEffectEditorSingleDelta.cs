/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
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

        protected override void LoadBuff()
        {
            var source = (SingleDeltaBuff)SourceBuff;
            nudDelta.Value = source.Delta;
        }

        public override Buff CopyBuff()
        {
            var copy = (SingleDeltaBuff)SourceBuff.Clone();
            copy.Delta = (int)nudDelta.Value;

            return copy;
        }

    }

}
