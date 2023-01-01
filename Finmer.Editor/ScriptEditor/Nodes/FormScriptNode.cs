/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows.Forms;
using Finmer.Core.VisualScripting;

namespace Finmer.Editor
{

    /// <summary>
    /// Base class for ScriptNode editor forms.
    /// </summary>
    public class FormScriptNode : Form
    {

        /// <summary>
        /// Gets or sets the node that is being edited by this form.
        /// </summary>
        public ScriptNode Node { get; set; }

    }

}
