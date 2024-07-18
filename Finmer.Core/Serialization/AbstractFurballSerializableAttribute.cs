/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;

namespace Finmer.Core.Serialization
{

    /// <summary>
    /// Indicates that the type annotated with this attribute cannot be serialized into a Furball asset stream directly. This attribute is not inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal sealed class AbstractFurballSerializableAttribute : Attribute {}

}
