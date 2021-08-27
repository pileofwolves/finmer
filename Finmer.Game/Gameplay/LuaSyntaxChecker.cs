using System;
using Finmer.Core.Compilers;
using Finmer.Gameplay.Scripting;
using static Finmer.Gameplay.Scripting.LuaAPI;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Provides a wrapper around a Lua context to verify script syntax.
    /// </summary>
    /// <inheritdoc cref="IScriptVerifier" />
    internal sealed class LuaSyntaxChecker : IScriptVerifier, IDisposable
    {

        private readonly ScriptContext m_Context = new ScriptContext();

        public void Verify(string body, string name)
        {
            lock (m_Context)
            {
                // Try to compile the script body
                if (luaL_loadbuffer(m_Context.LuaState, body, new UIntPtr(unchecked((ulong)body.Length)), name) != 0)
                {
                    // return the error message
                    string errormsg = lua_tostring(m_Context.LuaState, -1);
                    lua_pop(m_Context.LuaState, 1);
                    throw new LoaderException(errormsg);
                }

                // Pop the compiled script, we don't actually need it
                lua_pop(m_Context.LuaState, 1);
            }
        }

        public void Dispose()
        {
            m_Context.Dispose();
        }

    }

}
