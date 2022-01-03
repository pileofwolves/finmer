/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using JetBrains.Annotations;

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// Describes how a property may be manipulated from a Lua script.
    /// </summary>
    internal enum EScriptAccess
    {
        None = 0,
        Read = 1 << 0,
        Write = 1 << 1,
        ReadWrite = Read | Write
    }

    /// <summary>
    /// Specifies that a property can be accessed from a Lua script.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    [MeansImplicitUse]
    internal sealed class ScriptablePropertyAttribute : Attribute
    {

        public ScriptablePropertyAttribute(EScriptAccess access)
        {
            Access = access;
        }

        public EScriptAccess Access { get; }

    }

    /// <summary>
    /// Specifies that a function can be called from a Lua script.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    [MeansImplicitUse]
    internal sealed class ScriptableFunctionAttribute : Attribute { }

}
