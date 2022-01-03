/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using Finmer.Core;
using Finmer.Core.Compilers;

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// Provides a wrapper around a Lua context to precompile a script.
    /// </summary>
    /// <inheritdoc cref="IScriptCompiler" />
    internal sealed class ScriptCompiler : IScriptCompiler, IDisposable
    {

        private readonly ScriptContext m_Context = new ScriptContext();

        public CompiledScript Compile(string body, string name)
        {
            lock (m_Context)
            {
                byte[] chunk = m_Context.Precompile(body, name);
                return new CompiledScript
                {
                    Data = chunk,
#if DEBUG
                    Source = body
#endif
                };
            }
        }

        public void Dispose()
        {
            m_Context.Dispose();
        }

    }

}
