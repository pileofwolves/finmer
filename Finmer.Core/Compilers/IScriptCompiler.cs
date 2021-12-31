/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

namespace Finmer.Core.Compilers
{

    /// <summary>
    /// Implements a Lua script compiler.
    /// </summary>
    public interface IScriptCompiler
    {

        /// <summary>
        /// Precompile a script and returns the raw binary data that represents the compiled script.
        /// </summary>
        /// <param name="body">The contents of the script.</param>
        /// <param name="name">A name used to identify this script for debugging.</param>
        CompiledScript Compile( string body, string name);

    }

}
