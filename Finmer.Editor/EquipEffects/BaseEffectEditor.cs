/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Windows.Forms;
using Finmer.Core.Buffs;

namespace Finmer.Editor
{

    public class BaseEffectEditor : Form
    {

        private Buff m_SourceBuff;

        public Buff SourceBuff
        {
            get => m_SourceBuff;
            set
            {
                m_SourceBuff = value;
                LoadBuff();
            }
        }

        protected virtual void LoadBuff()
        {
            throw new NotSupportedException();
        }

        public virtual Buff CopyBuff()
        {
            throw new NotSupportedException();
        }

    }

}
