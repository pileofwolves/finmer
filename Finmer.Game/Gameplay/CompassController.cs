/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2021 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Finmer.Gameplay.Scripting;

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

        public void AddDirectLink(ECompassDirection direction, string scene)
        {
            lock (m_Lock)
            {
                // Associate the direction with the scene name
                m_Links.Add(direction, scene);
            }
        }

        public void AddScriptLink(ECompassDirection direction, IntPtr stack)
        {
            lock (m_Lock)
            {
                // Add a marker link that has no direct scene target, but indicates the link is present
                m_Links.Add(direction, null);

                // Store the script function at the top of the script stack for later reuse
                m_CallbackTable.Bind(stack, DirectionToString(direction));
            }
        }

        public void Reset()
        {
            lock (m_Lock)
            {
                m_Links.Clear();
                m_CallbackTable.UnbindAll();
            }
        }

        public bool HasLink(ECompassDirection direction)
        {
            lock (m_Lock)
            {
                return m_Links.ContainsKey(direction);
            }
        }

        public void ExecuteLink(ECompassDirection direction, IntPtr stack)
        {
            lock (m_Lock)
            {
                Debug.Assert(HasLink(direction), "Executing invalid link");

                string target_scene = m_Links[direction];
                if (target_scene == null)
                {
                    // This is a script link, load the callback function
                    // TODO: Marshal this onto the script thread, since doing this on the main thread opens up data race crashes
                    // TODO: if the script does something that invokes the script thread (such as calling SetScene())
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
                    m_Session.SetScene(new SceneScripted(m_Session.ScriptContext, target_scene));
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
