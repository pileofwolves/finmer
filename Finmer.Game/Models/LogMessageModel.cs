/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Windows;
using System.Windows.Media;

namespace Finmer.Models
{

    /// <summary>
    /// Contains settings for a game log entry.
    /// </summary>
    public class LogMessageModel
    {

        /// <summary>
        /// The final text that is to be displayed on-screen.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Color to apply to the text block.
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// Style to apply to the text block.
        /// </summary>
        public Style TextStyle { get; set; }

        /// <summary>
        /// Whether this entry represents a horizontal separator.
        /// </summary>
        public bool IsBar { get; set; }

    }

}
