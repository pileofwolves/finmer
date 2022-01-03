/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Diagnostics;
using static Finmer.Gameplay.Scripting.LuaApi;

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// Manages a collection of callbacks to user code. Callbacks can be supplied by script, and then invoked from C# code by name.
    /// </summary>
    public class ScriptCallbackTable : IDisposable
    {

        private const string k_CallbackTable = "ScriptCallbackTables";

        private readonly ScriptContext m_Context;
        private int m_TableRef;

        public ScriptCallbackTable(ScriptContext context)
        {
            m_Context = context;

            // Generate an empty table where we can store the callbacks
            IntPtr state = m_Context.LuaState;
            luaL_newmetatable(state, k_CallbackTable);
            lua_newtable(state);
            m_TableRef = luaL_ref(state, -2);
            lua_pop(state, 1);

            // Ensure this object is freed when the script context is destroyed
            context.RegisterOwnedResource(this);
        }

        ~ScriptCallbackTable()
        {
            ReleaseUnmanagedResources();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Pop and attach the function at the top of the input Lua stack to the specified name.
        /// </summary>
        public void Bind(IntPtr stack, string name)
        {
            Debug.Assert(m_TableRef != -1, "Table used after being freed");
            Debug.Assert(lua_gettop(stack) > 0, "Stack is empty");
            Debug.Assert(lua_isfunction(stack, -1), "Stack top must be a function");

            // Push the callback table below the function object
            PushCallbackTable(stack);
            lua_insert(stack, -2);

            // Bind the name to the function
            lua_setfield(stack, -2, name);

            // Cleanup
            lua_pop(stack, 1);
        }

        /// <summary>
        /// Remove the binding from the specified name to any function it may have been referencing.
        /// </summary>
        public void Unbind(string name)
        {
            Debug.Assert(m_TableRef != -1, "Table used after being freed");
            IntPtr stack = m_Context.LuaState;

            // Assign nil to the name, so the function can be GC'd
            PushCallbackTable(stack);
            lua_pushnil(stack);
            lua_setfield(stack, -2, name);

            // Cleanup
            lua_pop(stack, 1);
        }

        /// <summary>
        /// Remove all bound callbacks.
        /// </summary>
        public void UnbindAll()
        {
            Debug.Assert(m_TableRef != -1, "Table used after being freed");
            IntPtr stack = m_Context.LuaState;

            // Replace our callback table with an empty table, so all contents are effectively wiped
            luaL_newmetatable(stack, k_CallbackTable);
            lua_newtable(stack);
            lua_rawseti(stack, -2, m_TableRef);
            lua_pop(stack, 1);
        }

        /// <summary>
        /// Find the callback associated with the specified name. Returns true if available, false if not.
        /// </summary>
        public bool PrepareCall(IntPtr stack, string name)
        {
            Debug.Assert(m_TableRef != -1, "Table used after being freed");

            // Retrieve the function from the callback table
            PushCallbackTable(stack);
            lua_getfield(stack, -1, name);
            lua_remove(stack, -2); // pop global table

            // If it's not a function (probably nil / unassigned), pop the bad value
            if (lua_type(stack, -1) != ELuaType.Function)
            {
                lua_pop(stack, 1);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Invokes a function prepared by PrepareCall(), passing the specified number of user arguments to the function.
        /// </summary>
        public void Call(IntPtr stack, int numArgs)
        {
            Debug.Assert(m_TableRef != -1, "Table used after being freed");
            Debug.Assert(lua_type(stack, -1 - numArgs) == ELuaType.Function, "Call PrepareCall first, and check that it returned 'true'");

            // The stack has already been prepared, so perform the call
            m_Context.RunProtectedCall(stack, numArgs, 0);
        }

        /// <summary>
        /// Helper for retrieving the local callback table from the registry.
        /// </summary>
        private void PushCallbackTable(IntPtr stack)
        {
            luaL_newmetatable(stack, k_CallbackTable);
            lua_rawgeti(stack, -1, m_TableRef);
            lua_remove(stack, -2);
        }

        private void ReleaseUnmanagedResources()
        {
            // Only do this once
            if (m_TableRef == -1)
                return;

            // Release the internal callback table so it can be collected
            IntPtr stack = m_Context.LuaState;
            luaL_newmetatable(stack, k_CallbackTable);
            luaL_unref(stack, -1, m_TableRef);
            lua_pop(stack, 1);

            // Invalidate the table reference ID
            m_TableRef = -1;
        }

    }

}
