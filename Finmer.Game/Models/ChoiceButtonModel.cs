/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Models
{

    /// <summary>
    /// Contains settings for a choice button that is displayed on UI.
    /// </summary>
    public class ChoiceButtonModel
    {

        /// <summary>
        /// The caption of the button.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The tooltip text shown when the user hovers over the button.
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// Multiplier for the button width. 1.0 is default, 0.5 is half size, etc.
        /// </summary>
        public float Width { get; set; } = 1.0f;

        /// <summary>
        /// The choice value that is relayed back to the attached scene script if this button is clicked.
        /// </summary>
        public int Choice { get; set; }

        /// <summary>
        /// Whether this button should be given a special highlight color.
        /// </summary>
        public bool Highlight { get; set; }

    }

}
