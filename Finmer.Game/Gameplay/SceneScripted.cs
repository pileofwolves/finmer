/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

#if DEBUG
#define LUA_TIMINGS
#endif

using System;
using System.Diagnostics;
using Finmer.Core;
using Finmer.Core.Assets;
using Finmer.Gameplay.Scripting;
using Finmer.Models;
using Finmer.Utility;
using static Finmer.Gameplay.Scripting.LuaApi;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Represents a scene with functionality implemented in Lua scripts.
    /// </summary>
    internal sealed class SceneScripted : Scene, ISaveable
    {

        private const string k_SceneRegistryTable = @"SceneEnvs";

        private const string k_ScriptCallbackEnter = @"OnEnter";
        private const string k_ScriptCallbackLeave = @"OnLeave";
        private const string k_ScriptCallbackTurn = @"OnTurn";
        private const string k_ScriptCallbackCaptureState = @"_CaptureState";
        private const string k_ScriptCallbackRestoreState = @"_RestoreState";

        private readonly ScriptContext m_Context;
        private readonly AssetScene m_SceneAsset;
        private readonly string m_SceneName;
        private readonly int m_SceneRef;

        private IntPtr m_Coroutine = IntPtr.Zero;
        private bool m_HasError = true;

        public SceneScripted(ScriptContext context, string filename)
            : this(context, GameController.Content.GetAssetByName(filename) as AssetScene
                ?? throw new ArgumentException($"Failed to load scene '{filename}': The specified asset does not exist, or is not a Scene.", nameof(filename))) {}

        public SceneScripted(ScriptContext context, AssetScene scene)
        {
            // Set up basic state
            m_Context = context;
            m_SceneAsset = scene ?? throw new ArgumentNullException(nameof(scene));
            m_SceneName = scene.Name;

#if LUA_TIMINGS
            var sw = Stopwatch.StartNew();
#endif

            // Validate that the scene can actually be loaded
            if (scene.Inject)
                throw new ArgumentException($"Failed to load scene '{m_SceneName}': The specified asset is a patch scene and cannot be loaded directly.", nameof(scene));

            // Load the chunk
            // TODO: Use ScriptExceptions here
            CompiledScript script = scene.PrecompiledScript;
            if (!context.LoadScript(script, m_SceneName))
                return;

#if LUA_TIMINGS
            double time_comp = sw.Elapsed.TotalMilliseconds;
            sw.Restart();
#endif

            // Prepare a sandbox environment for the script. We will override the __index metamethod to refer to the real global environment,
            // but not the __newindex metamethod so scripts can modify their environment as they like. Essentially, this makes _G read-only.
            IntPtr state = context.LuaState;
            luaL_newmetatable(state, k_SceneRegistryTable);
            lua_createtable(state, 0, 1);

            // Make the built-in '_G' variable refer to the scene-specific environment, instead of the global environment. Note that users can
            // escape this sandbox by setting _G to nil, at which point they'll go through the metatable and can query the value of the 'real'
            // _G table. A fix would be to wrap it in a double metatable, but from a performance standpoint, this is preferable.
            lua_pushvalue(state, -1);
            lua_setfield(state, -2, "_G");

            // Prepare metatable for the sandbox environment
            lua_createtable(state, 0, 1);
            lua_getglobal(state, "_G"); // set global env as parent lookup env 
            lua_setfield(state, -2, "__index");
            lua_setmetatable(state, -2); // set env metatable

            // Write the environment table to the registry, so it can accessed again later
            lua_pushvalue(state, -1);
            m_SceneRef = luaL_ref(state, -3); // save into scene table (and pop)

            // Sandbox the main chunk
            lua_setfenv(state, -3);
            lua_pop(state, 1); // remove SceneEnvs table

#if LUA_TIMINGS
            double time_sandbox = sw.Elapsed.TotalMilliseconds;
            sw.Restart();
#endif

            // Run it
            if (!context.RunProtectedCall(0, 0))
                return;

#if LUA_TIMINGS
            sw.Stop();
            GameUI.Instance.Log($"Timings for {m_SceneName}: {time_comp + time_sandbox + sw.Elapsed.TotalMilliseconds:F2} ms total: Load = {time_comp:F2} ms, Sandboxing = {time_sandbox:F2} ms, Run = {sw.Elapsed.TotalMilliseconds:F2} ms", Theme.LogColorDarkCyan);
#endif

            // All done, the scene is ready!
            m_HasError = false;
        }

        /// <summary>
        /// Starts or resumes an existing coroutine set to m_Coroutine.
        /// </summary>
        /// <remarks>
        /// For details on passing arguments, see LaunchCoroutine().
        /// </remarks>
        /// <param name="numArgs">Number of arguments to pass to the function.</param>
        private void ResumeCoroutineWithArgs(int numArgs)
        {
            // Run script
            int result = lua_resume(m_Coroutine, numArgs);

            // If the script yielded, we'll resume it later (on a call to ResumeCoroutine()), so there's nothing more to do here.
            if (result == LUA_YIELD)
                return;

            // Error handling
            IntPtr state = m_Context.LuaState;
            if (result != 0)
            {
                // Prevent further callbacks, since the script is now in an undefined state
                m_HasError = true;

                // Note: there's no need to pop the error message or clean the stack at all, because we will destroy the coroutine entirely
                Debug.Assert(lua_isstring(m_Coroutine, -1));
                string error_message = lua_tostring(m_Coroutine, -1);
                GameUI.Instance.Log($"ERROR: Script error in scene '{m_SceneName}': {error_message}", Theme.LogColorError);
            }

            // Remove the thread object from the main thread's stack. This will make it eligible for GC.
            RemoveCoroutine(state);
        }

        /// <summary>
        /// Cleans up the cached coroutine for this scene.
        /// </summary>
        /// <param name="state">Pointer to the Lua state of the main stack.</param>
        private void RemoveCoroutine(IntPtr state)
        {
            Debug.Assert(m_Coroutine != IntPtr.Zero, "No coroutine is active, this function should not be called");

            // Find the coroutine on the main thread stack
            for (int i = 1, c = lua_gettop(state); i <= c; i++)
            {
                if (lua_type(state, i) != ELuaType.Thread || lua_tothread(state, i) != m_Coroutine)
                    continue;

                // Remove the coroutine thread from the main thread stack
                lua_remove(state, i);
                m_Coroutine = IntPtr.Zero;
                break;
            }

            Debug.Assert(m_Coroutine == IntPtr.Zero, "Coroutine thread was not found on main stack");
        }

        /// <summary>
        /// Calls a global function defined by this scene's script.
        /// </summary>
        /// <remarks>
        /// Accepts a number of arguments that must already be passed on the Lua stack, in the same way they would be pushed on the stack for
        /// a native lua_call invocation. The pushed arguments are always consumed, regardless of whether or not the script call succeeds.
        /// </remarks>
        /// <param name="name">Name of the function to invoke.</param>
        /// <param name="numArgs">Number of arguments to pass to the function.</param>
        private void LaunchCoroutine(string name, int numArgs)
        {
            // This function must not be called from the main thread, because scripts expect to be able to sleep or call blocking functions
            GameController.Session.VerifyScriptThread();

            // If the scene is in an invalid state, just get rid of the function arguments and bail
            IntPtr state = m_Context.LuaState;
            if (m_HasError)
            {
                lua_pop(state, numArgs);
                return;
            }

            // Create a new coroutine, which will remain at the top of the main thread stack until it finishes
            Debug.Assert(m_Coroutine == IntPtr.Zero, "Memory leak: there is already another coroutine in progress");
            m_Coroutine = lua_newthread(state);

            // Move the user arguments to the thread
            if (numArgs > 0)
            {
                lua_insert(state, -numArgs - 1); // Move the thread below the args, so the args are at the top
                lua_xmove(state, m_Coroutine, numArgs);
            }

            // Retrieve the scene environment from the registry, so we can access its globals
            luaL_newmetatable(m_Coroutine, k_SceneRegistryTable);
            lua_rawgeti(m_Coroutine, -1, m_SceneRef);
            lua_getfield(m_Coroutine, -1, name);

            // Check that the global we just accessed is in fact a global function
            if (lua_type(m_Coroutine, -1) != ELuaType.Function)
            {
                // If not, destroy the coroutine and bail
                lua_pop(state, 1);
                m_Coroutine = IntPtr.Zero;
                return;
            }

            // Otherwise, insert the function below the arguments (since that is the order that lua_call expects)
            lua_insert(m_Coroutine, -3 - numArgs);
            lua_pop(m_Coroutine, 2); // remove env table and registry table

            // Run it
            Debug.Assert(lua_isfunction(m_Coroutine, 1), "Stack rearrangement for call is broken");
            ResumeCoroutineWithArgs(numArgs);
        }

        public override void Enter()
        {
            // Run callback
            LaunchCoroutine(k_ScriptCallbackEnter, 0);
        }

        public override void Leave()
        {
            // If there still is a coroutine, clean it up.
            // This happens if SetScene() was called, for example, which interrupts this (old) script by yielding.
            IntPtr state = m_Context.LuaState;
            if (m_Coroutine != IntPtr.Zero)
                RemoveCoroutine(state);

            // Run callback
            LaunchCoroutine(k_ScriptCallbackLeave, 0);
            Debug.Assert(m_Coroutine == IntPtr.Zero, "Memory leak: coroutine was not cleaned up after OnLeave");

            // Remove this scene's environment from the environment table, so it can be collected
            luaL_newmetatable(state, k_SceneRegistryTable);
            luaL_unref(state, -1, m_SceneRef);
            lua_pop(state, 1);
        }

        public override void Turn(int choice)
        {
            // If a paused coroutine exists, resume it now
            if (m_Coroutine != IntPtr.Zero)
            {
                ResumeCoroutineWithArgs(0);
                return;
            }

            // Otherwise, start a new one by running
            lua_pushnumber(m_Context.LuaState, choice);
            LaunchCoroutine(k_ScriptCallbackTurn, 1);
        }

        public PropertyBag Serialize()
        {
            IntPtr state = m_Context.LuaState;

            // Retrieve the scene environment from the registry, so we can access its globals
            luaL_newmetatable(state, k_SceneRegistryTable);
            lua_rawgeti(state, -1, m_SceneRef);

            // Run the utility function that returns the state value
            lua_getfield(state, -1, k_ScriptCallbackCaptureState);
            Debug.Assert(lua_isfunction(state, -1), "State capture utility missing");
            lua_call(state, 0, 1);

            // Read the return value
            var output = new PropertyBag();
            output.SetString(SaveData.k_System_CurrentSceneState, lua_tostring(state, -1));

            // Restore the stack (state value + scene table + registry table = 3)
            lua_pop(state, 3);

            // Store the scene asset GUID as well, so the loader knows which scene to load
            output.SetBytes(SaveData.k_System_CurrentSceneID, m_SceneAsset.ID.ToByteArray());

            return output;
        }

        public void Deserialize(PropertyBag input)
        {
            // Push the state value onto the stack
            IntPtr state = m_Context.LuaState;
            lua_pushstring(state, input.GetString(SaveData.k_System_CurrentSceneState));

            // Call the restore utility. Note that this will synchronously invoke the appropriate State function as well.
            LaunchCoroutine(k_ScriptCallbackRestoreState, 1);
        }

    }

}
