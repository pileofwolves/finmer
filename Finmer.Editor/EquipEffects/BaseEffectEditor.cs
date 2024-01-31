/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows.Forms;
using Finmer.Core.Buffs;
using Finmer.Core.Serialization;

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

        /// <summary>
        /// Instantiates an appropriate buff editing form for the specified buff object.
        /// </summary>
        public static BaseEffectEditor CreateBuffEditor(Buff buff)
        {
            // Create a matching editor form
            BaseEffectEditor form;
            switch (buff)
            {
                case SingleDeltaBuff _:             form = new FormEffectEditorSingleDelta();       break;
                case BuffCustomTooltipText _:       form = new FormEffectEditorCustomText();        break;
                default:                            return null;
            }

            // Make a copy of the buff, so the editor form can edit it safely
            form.BuffInstance = AssetSerializer.DuplicateAsset(buff);

            return form;
        }

    }

}
