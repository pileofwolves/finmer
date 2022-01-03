/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.ViewModels
{

    /// <summary>
    /// View model that encapsulates a preset species configuration for the character creator.
    /// </summary>
    public sealed class SpeciesPresetViewModel : BaseProp
    {

        /// <summary>
        /// The singular noun that describes the species.
        /// </summary>
        public string SingularNoun { get; set; }

        /// <summary>
        /// The plural noun that describes the species.
        /// </summary>
        public string PluralNoun { get; set; }

        /// <summary>
        /// The noun that describes the skin or coat of a member of this species.
        /// </summary>
        public string CoatNoun { get; set; }

        /// <summary>
        /// The adjective that describes the skin or coat of a member of this species.
        /// </summary>
        public string CoatAdjective { get; set; }

    }

}
