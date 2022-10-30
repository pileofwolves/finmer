/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows.Forms;
using Finmer.Core.Buffs;

namespace Finmer.Editor
{

    /// <summary>
    /// Base class for equipment effect editor forms.
    /// </summary>
    public class BaseEffectEditor : Form
    {

        /// <summary>
        /// The buff object that is being edited by this form.
        /// </summary>
        public Buff BuffInstance { get; set; }

    }

}
