/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Finmer.Core;
using Finmer.Models;
using Finmer.Utility;
using static Finmer.Gameplay.Scripting.LuaApi;

namespace Finmer.Gameplay.Scripting
{

    /// <summary>
    /// Represents a Lua context.
    /// </summary>
    public class ScriptContext : IDisposable
    {

        /// <summary>
        /// Pointer to the unmanaged Lua state object (in C: struct lua_State*).
        /// </summary>
        public IntPtr LuaState { get ; private set; }

        private readonly List<WeakReference<IDisposable>> m_OwnedResources = new List<WeakReference<IDisposable>>();
        private readonly List<GCHandle> m_PinnedDelegates = new List<GCHandle>();
        private readonly Dictionary<Guid, PinnedScriptableObject> m_PinnedObjects = new Dictionary<Guid, PinnedScriptableObject>();

        private GCHandle m_GCPin;

        public ScriptContext()
        {
            // Instantiate a new Lua state, with the default memory allocator (C malloc)
            LuaState = luaL_newstate();

            // Register the panic function. Note that we pin a reference to the delegate to prevent it from being collected.
            lua_CFunction new_panic_func = ExportedPanic;
            PinDelegate(new_panic_func);
            lua_atpanic(LuaState, new_panic_func);

            // Write a global pointer to this managed object to the Lua registry, enabling users to obtain the ScriptContext given only a lua_State pointer
            m_GCPin = GCHandle.Alloc(this, GCHandleType.Normal);
            lua_pushlightuserdata(LuaState, GCHandle.ToIntPtr(m_GCPin));
            lua_setfield(LuaState, LUA_REGISTRYINDEX, "ScriptContext");

            // Open standard libraries
            OpenStandardLibrary(luaopen_base);
            OpenStandardLibrary(luaopen_math);
            OpenStandardLibrary(luaopen_string);
            OpenStandardLibrary(luaopen_table);
            Debug.Assert(lua_gettop(LuaState) == 0);

            // Register global libraries
            GlobalScriptLibrary.Inject(this);
            GameplayScriptLibrary.Inject(this);
            UIScriptLibrary.Inject(this);
            GrammarScriptLibrary.Inject(this);
            SaveDataScriptLibrary.Inject(this);
            Combat2ScriptLibrary.Inject(this);
            Debug.Assert(lua_gettop(LuaState) == 0);
        }

        ~ScriptContext()
        {
            ReleaseUnmanagedResources();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Registers a disposable resources as being owned by the ScriptContext, such that it will be disposed of before the Lua state is released.
        /// </summary>
        public void RegisterOwnedResource(IDisposable resource)
        {
            m_OwnedResources.Add(new WeakReference<IDisposable>(resource));
        }

        /// <summary>
        /// Permanently pins a delegate, so that it cannot be garbage-collected for the lifetime of this ScriptContext.
        /// This is required to allow Lua to hold the only remaining reference to the function pointer.
        /// </summary>
        /// <param name="func">The delegate to pin.</param>
        public void PinDelegate(lua_CFunction func)
        {
            // Create a GCHandle to prevent the delegate from being collected while Lua holds the only remaining reference.
            // Note that a Pinned handle isn't necessary here; the CLR creates a wrapper for us that does not require a fixed address.
            m_PinnedDelegates.Add(GCHandle.Alloc(func, GCHandleType.Normal));
        }

        /// <summary>
        /// Helper function that calls PinDelegate(), then pushes the closure to Lua. Meant for avoiding having to store
        /// delegates in local variables just to be able to both pin and push them in one go.
        /// </summary>
        /// <param name="func"></param>
        public void PushDelegate(lua_CFunction func)
        {
            PinDelegate(func);
            lua_pushcfunction(LuaState, func);
        }

        /// <summary>
        /// Keeps the input object alive (same as PinObject()) and stores it as a global variable for scripts to use.
        /// </summary>
        /// <param name="obj">The object to expose.</param>
        /// <param name="name">The name of the global variable to assign the object to.</param>
        public void PinObjectAsGlobal(ScriptableObject obj, string name)
        {
            // Store it in a global variable
            obj.PushToLua(LuaState);
            lua_setglobal(LuaState, name);
        }

        /// <summary>
        /// Keeps the input object alive, preventing it from being garbage-collected, until it is unpinned.
        /// This method maintains a reference count, meaning you must call UnpinObject() the same number of times as you called
        /// PinObject().
        /// </summary>
        /// <param name="obj">The object to pin.</param>
        public void PinObject(ScriptableObject obj)
        {
            // Get or create the pinned object entry
            if (!m_PinnedObjects.TryGetValue(obj.ID, out PinnedScriptableObject pin))
            {
                pin = new PinnedScriptableObject { m_Object = obj };
                m_PinnedObjects.Add(obj.ID, pin);
            }

            // Increase its refcount
            pin.m_RefCount++;
        }

        /// <summary>
        /// Removes a pinned reference made with PinObject(). Call this the same number of times as you called PinObject().
        /// </summary>
        /// <param name="obj">The object to unpin.</param>
        public void UnpinObject(ScriptableObject obj)
        {
            // Remove one reference
            PinnedScriptableObject pin = m_PinnedObjects[obj.ID];
            if (--pin.m_RefCount == 0)
            {
                // Delete the entry if the last reference is released
                m_PinnedObjects.Remove(obj.ID);

                // Remove pinned delegates that belonged to this object
                for (int i = m_PinnedDelegates.Count - 1; i >= 0; i--)
                {
                    var del = (lua_CFunction)m_PinnedDelegates[i].Target;
                    if (del.Target != obj)
                        continue;

                    m_PinnedDelegates[i].Free();
                    m_PinnedDelegates.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Obtains the ScriptContext object given only a Lua state pointer.
        /// </summary>
        public static ScriptContext FromLua(IntPtr state)
        {
            // Retrieve the pointer from the Lua registry
            lua_getfield(state, LUA_REGISTRYINDEX, "ScriptContext");
            IntPtr pointer = lua_touserdata(state, -1);
            lua_pop(state, 1);

            // Convert the pointer to a ScriptContext reference
            GCHandle handle = GCHandle.FromIntPtr(pointer);
            return (ScriptContext)handle.Target;
        }

        public ScriptableObject GetPinnedObject(Guid guid)
        {
            return m_PinnedObjects.TryGetValue(guid, out PinnedScriptableObject pin) ? pin.m_Object : null;
        }

        public void RegisterFunction(string name, lua_CFunction func)
        {
            PinDelegate(func);
            lua_pushcfunction(LuaState, func);
            lua_setfield(LuaState, -2, name);
        }

        public void RegisterGlobalFunction(string name, lua_CFunction func)
        {
            PinDelegate(func);
            lua_pushcfunction(LuaState, func);
            lua_setglobal(LuaState, name);
        }

        public bool LoadScript(CompiledScript precompiled, string chunkname)
        {
            // Try to compile the script
            if (luaL_loadbuffer_binary(LuaState, precompiled.Data, new UIntPtr(unchecked((ulong)precompiled.Data.Length)), chunkname) != 0)
            {
                // An error occurred; write the message to the game log
                GameUI.Instance.Log($"Failed to compile Lua script {chunkname}: {lua_tostring(LuaState, -1)}", Theme.LogColorError);

                // Pop the error message
                lua_pop(LuaState, 1);

                return false;
            }

            return true;
        }

        public bool LoadScript(string body, string chunkname)
        {
            // Try to compile the script
            if (luaL_loadbuffer(LuaState, body, new UIntPtr(unchecked((ulong)body.Length)), chunkname) != 0)
            {
                // An error occurred; write the message to the game log
                GameUI.Instance.Log($"Failed to compile Lua script {chunkname}: {lua_tostring(LuaState, -1)}", Theme.LogColorError);

                // Pop the error message
                lua_pop(LuaState, 1);

                return false;
            }

            return true;
        }

        public bool RunProtectedCall(IntPtr stack, int numArgs, int numResults)
        {
            // TODO: Shared error handler

            // Call the function in protected mode
            if (lua_pcall(stack, numArgs, numResults, 0) == 0)
                return true;

            // An error occurred; get the error message
            string errormsg = lua_tostring(stack, -1);
            GameUI.Instance.Log($"ERROR: Script error in protected call: {errormsg}", Theme.LogColorError);
            lua_pop(stack, 1);

            return false;
        }

        public bool RunProtectedCall(int numArgs, int numResults)
        {
            return RunProtectedCall(LuaState, numArgs, numResults);
        }

        public byte[] Precompile(string body, string chunkname)
        {
            // Try to compile the script
            if (luaL_loadbuffer(LuaState, body, new UIntPtr(unchecked((ulong)body.Length)), chunkname) != 0)
            {
                // An error occurred
                string message = $"Failed to compile Lua script {chunkname}: {lua_tostring(LuaState, -1)}";
                lua_pop(LuaState, 1);

                throw new InvalidDataException(message);
            }

            int WriteChunk(IntPtr _, IntPtr chunkPtr, UIntPtr chunkLen, IntPtr userdata)
            {
                // Marshal the data behind the unmanaged pointer to a managed byte array
                int count = (int)chunkLen.ToUInt32();
                var chunk_bytes = new byte[count];
                Marshal.Copy(chunkPtr, chunk_bytes, 0, count);

                // Write the bytes to the output stream
                var outstream = (MemoryStream)GCHandle.FromIntPtr(userdata).Target;
                outstream.Write(chunk_bytes, 0, count);

                return 0;
            }

            using (var ms = new MemoryStream())
            {
                // Get a handle referring to the MemoryStream, that we can pass to unmanaged code
                var handle = GCHandle.Alloc(ms);

                // Write the Lua function to the stream
                lua_Writer writer = WriteChunk; // Keep delegate alive
                lua_dump(LuaState, writer, GCHandle.ToIntPtr(handle), false);
                lua_pop(LuaState, 1);

                // Discard the handle
                handle.Free();

                // And we're done
                return ms.ToArray();
            }
        }

        private static int ExportedPanic(IntPtr state)
        {
            // We throw an exception so that the stack is unwound and we hit our own unhandled-exception-handler.
            // If this function returns normally, Lua calls std::exit(), and we get no opportunity to do logging.
            string errmsg = lua_tostring(state, -1);
            throw new ScriptException("Critical error in Lua runtime: " + errmsg);
        }

        private void OpenStandardLibrary(lua_CFunction func)
        {
            // The purpose of this function is to ensure that Lua can finish calling the input function pointer, while we keep the
            // managed delegate alive until the function completes. Not doing this risks GCing the function pointer while it's running.
            lua_pushcfunction(LuaState, func);
            lua_call(LuaState, 0, 0);

            GC.KeepAlive(func);
        }

        private void ReleaseUnmanagedResources()
        {
            // Release owned resources
            foreach (var resource_ref in m_OwnedResources)
                if (resource_ref.TryGetTarget(out var resource))
                    resource.Dispose();

            // Destroy the Lua state
            lua_close(LuaState);
            LuaState = IntPtr.Zero;

            // Release the pinned GC handle
            m_GCPin.Free();

            // Since all userdata has been released, we do not need the pinned ScriptableObjects anymore
            m_PinnedDelegates.ForEach(handle => handle.Free());
            m_PinnedDelegates.Clear();
            m_PinnedObjects.Clear();
        }

        /// <summary>
        /// Helper class that maintains a reference count for ScriptableObjects.
        /// </summary>
        private sealed class PinnedScriptableObject
        {

            public ScriptableObject m_Object;
            public int m_RefCount;

        }

    }

}
