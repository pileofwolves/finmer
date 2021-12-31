/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using JetBrains.Annotations;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Specifies that a string property can be injected into game text as part of a grammar tag substitution.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    [MeansImplicitUse]
    public class TextPropertyAttribute : Attribute
    {

        /// <summary>
        /// The identifier to be used for the property in the grammar tag.
        /// </summary>
        public string Key { get; }

        public TextPropertyAttribute(string key)
        {
            Key = key;
        }

    }

}
