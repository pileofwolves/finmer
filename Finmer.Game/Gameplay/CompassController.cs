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
using Finmer.Gameplay.Scripting;
using Finmer.Models;
using Finmer.Utility;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Helper class that manages state for the compass' directional links.
    /// </summary>
    public class CompassController : IDisposable
    {

        private readonly GameSession m_Session;
        private readonly ScriptCallbackTable m_CallbackTable;
        private readonly Dictionary<ECompassDirection, string> m_Links;
        private readonly object m_Lock = new object();

        public CompassController(GameSession session)
        {
            m_Session = session;
            m_CallbackTable = new ScriptCallbackTable(session.ScriptContext);
            m_Links = new Dictionary<ECompassDirection, string>(4);
        }

        /// <summary>
        /// Add a link to another scene.
        /// </summary>
        /// <param name="direction">The compass direction of the target link.</param>
        /// <param name="scene">The asset name of the linked scene.</param>
        public void AddDirectLink(ECompassDirection direction, string scene)
        {
            lock (m_Lock)
            {
                // Associate the direction with the scene name
                m_Session.VerifyScriptThread();
                m_Links[direction] = scene;
            }
        }

        /// <summary>
        /// Add a link to a script callback.
        /// </summary>
        /// <param name="direction">The compass direction of the target link.</param>
        /// <param name="stack">The Lua stack pointer to retrieve the function from. The function must be at the top of the stack.</param>
        public void AddScriptLink(ECompassDirection direction, IntPtr stack)
        {
            lock (m_Lock)
            {
                // Add a marker link that has no direct scene target, but indicates the link is present
                m_Links[direction] = null;

                // Store the script function at the top of the script stack for later reuse
                m_Session.VerifyScriptThread();
                m_CallbackTable.Bind(stack, DirectionToString(direction));
            }
        }

        /// <summary>
        /// Erases all registered links.
        /// </summary>
        public void Reset()
        {
            lock (m_Session.ScriptContext)
            {
                lock (m_Lock)
                {
                    m_Links.Clear();
                    m_CallbackTable.UnbindAll();
                }
            }
        }

        /// <summary>
        /// Indicates whether a link in the specified compass direction has been registered.
        /// </summary>
        public bool HasLink(ECompassDirection direction)
        {
            lock (m_Lock)
            {
                return m_Links.ContainsKey(direction);
            }
        }

        /// <summary>
        /// Marshal a request to follow a link onto the script thread. Should be called from the main thread.
        /// </summary>
        public void QueueExecuteLink(ECompassDirection direction)
        {
            m_Session.RunSceneEvent(ESceneEvent.Link, (int)direction);
        }

        /// <summary>
        /// Perform a queued link follow request. Must be called from the script thread.
        /// </summary>
        public void ExecuteLink(ECompassDirection direction)
        {
            lock (m_Lock)
            {
                // Sanity checks
                Debug.Assert(HasLink(direction), "Executing invalid link");
                m_Session.VerifyScriptThread();

                // Follow the link
                string target_scene = m_Links[direction];
                if (target_scene == null)
                {
                    // This is a script link, load the callback function
                    var stack = m_Session.ScriptContext.LuaState;
                    if (!m_CallbackTable.PrepareCall(stack, DirectionToString(direction)))
                    {
                        Debug.Fail("Could not load script link");
                        return;
                    }

                    // Call the user callback function
                    m_CallbackTable.Call(stack, 0);
                }
                else
                {
                    // This is a direct scene link, transition to the target scene
                    try
                    {
                        m_Session.SetScene(new SceneScripted(m_Session.ScriptContext, target_scene));
                    }
                    catch (ArgumentException ex)
                    {
                        GameUI.Instance.Log($"ERROR: {ex.Message}", Theme.LogColorError);
                    }
                }
            }
        }

        private static string DirectionToString(ECompassDirection direction)
        {
            switch (direction)
            {
                case ECompassDirection.North:
                    return "N";
                case ECompassDirection.West:
                    return "W";
                case ECompassDirection.South:
                    return "S";
                case ECompassDirection.East:
                    return "E";
                default:
                    throw new ArgumentException(nameof(direction));
            }
        }

        public void Dispose()
        {
            m_CallbackTable.Dispose();
        }

    }

}
