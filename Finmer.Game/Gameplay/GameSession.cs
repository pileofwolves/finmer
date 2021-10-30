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
using System.Threading;
using Finmer.Core;
using Finmer.Gameplay.Scripting;
using Finmer.Models;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Describes an event to trigger in a scene.
    /// </summary>
    public enum ESceneEvent
    {
        Enter,
        Leave,
        Turn
    }

    /// <summary>
    /// Encompasses gameplay state associated with one running session, that can be discarded when returning to the menu screen.
    /// </summary>
    public class GameSession : IDisposable
    {

        /// <summary>
        /// Stack size in bytes of the script thread.
        /// </summary>
        private const int k_ScriptStackSize = 128 * 1024;

        /// <summary>
        /// The Player object associated with this session.
        /// </summary>
        public Player Player { get; }

        /// <summary>
        /// The script context containing script state for this session.
        /// </summary>
        public ScriptContext ScriptContext { get; } = new ScriptContext();

        /// <summary>
        /// The directional link manager for this session.
        /// </summary>
        public CompassController Compass { get; }

        private readonly Queue<SceneEvent> m_EventQueue = new Queue<SceneEvent>();
        private readonly object m_EventQueueLock = new object();

        private readonly Stack<Scene> m_SceneStack = new Stack<Scene>();
        private readonly Thread m_ScriptThread;
        private readonly AutoResetEvent m_ScriptWaitEvent;
        private bool m_ScriptThreadStop;

        public GameSession(PropertyBag savedata)
        {
            // Initialize the session
            Compass = new CompassController(this);
            Player = new Player(ScriptContext, savedata);

            // Allow scripts to access the player object as a global variable
            ScriptContext.PinObjectAsGlobal(Player, "Player");

            // Launch the script thread
            m_ScriptWaitEvent = new AutoResetEvent(false);
            m_ScriptThread = new Thread(ScriptThreadWorker, k_ScriptStackSize)
            {
                IsBackground = true
            };
            m_ScriptThread.Start();
        }

        public void Dispose()
        {
            // Stop the script thread
            m_ScriptThreadStop = true;
            m_ScriptWaitEvent.Set();
            m_ScriptThread.Join();

            // Clean up thread
            m_ScriptWaitEvent.Dispose();

            // Clean up runtime
            m_SceneStack.Clear();
            ScriptContext.Dispose();
        }

        /// <summary>
        /// Replaces the topmost scene on the scene stack with another one.
        /// </summary>
        /// <param name="scene"></param>
        public void SetScene(Scene scene)
        {
            PopScene();
            PushScene(scene);
        }

        /// <summary>
        /// Pushes a scene onto the scene stack.
        /// </summary>
        public void PushScene(Scene scene)
        {
            Debug.Assert(!IsGameOver());

            // Reset various state
            GameUI.Instance.LogSplit();
            GameUI.Instance.ClearButtons();
            GameUI.Instance.Instruction = String.Empty;
            TextParser.ClearNonPersistentContexts();
            Compass.Reset();

            // Add this scene onto the stack
            m_SceneStack.Push(scene);

            // Queue new scene events to give the scene its first turn
            RunSceneEvent(ESceneEvent.Enter);
            RunSceneEvent(ESceneEvent.Turn);
        }

        /// <summary>
        /// Pops the topmost scene off the scene stack.
        /// </summary>
        public void PopScene()
        {
            // Run the scene's Leave callback
            RunSceneEvent(ESceneEvent.Leave);

            // Remove the topmost scene from the stack
            m_SceneStack.Pop();
        }

        /// <summary>
        /// Returns the topmost scene on the scene stack.
        /// </summary>
        public Scene PeekScene()
        {
            return m_SceneStack.Peek();
        }

        /// <summary>
        /// Wake the paused scene script.
        /// </summary>
        public void ResumeScript()
        {
            // Wake the script thread by posting a new Turn event, which will resume any paused script
            RunSceneEvent(ESceneEvent.Turn, -1);
        }

        /// <summary>
        /// Enqueue a scene event for execution on the script thread.
        /// </summary>
        /// <param name="type">The type of event to perform.</param>
        /// <param name="data">Parameter to pass to the event.</param>
        public void RunSceneEvent(ESceneEvent type, int data = 0)
        {
            lock (m_EventQueueLock)
            {
                m_EventQueue.Enqueue(new SceneEvent { Scene = m_SceneStack.Peek(), Type = type, Data = data });
            }

            // Wake the script thread
            m_ScriptWaitEvent.Set();
        }

        /// <summary>
        /// Advance the game clock by a number of hours.
        /// </summary>
        public void AdvanceTime(int hours)
        {
            // Nothing to do?
            if (hours < 1)
                return;

            // Advance the clock
            Player.TimeHour += hours % 24;
            Player.TimeDay += hours / 24;
            if (Player.TimeHour >= 24)
            {
                // Wrap around
                Player.TimeHour -= 24;
                Player.TimeDay++;
            }
        }

        private bool IsGameOver()
        {
            return Player.IsDead();
        }

        private void RunGameOver()
        {
            var ui = GameUI.Instance;
            ui.Instruction = String.Empty;
            ui.ControlsEnabled = false;
            ui.InventoryEnabled = false;
            ui.Location = String.Empty;
            ui.IsGameOver = true;
        }

        private void ScriptThreadWorker()
        {
            while (true)
            {
                // Wait for a signal to resume work
                m_ScriptWaitEvent.WaitOne();

                // Exit thread if the session is being destroyed
                if (m_ScriptThreadStop)
                    return;

                // Keep processing scene events until the queue runs out
                while (true)
                {
                    // Retrieve the next scene event
                    SceneEvent item;
                    lock (m_EventQueueLock)
                    {
                        // Go to sleep if no work is queued
                        if (m_EventQueue.Count == 0)
                            break;

                        // Get the next work item
                        item = m_EventQueue.Dequeue();
                        Debug.Assert(item.Type != ESceneEvent.Turn || m_EventQueue.Count == 0,
                            "No other actions may come after a Turn, because the player must provide input");
                    }

                    // Remove UI controls
                    GameUI.Instance.ClearButtons();
                    GameUI.Instance.Instruction = String.Empty;
                    GameUI.Instance.ControlsEnabled = false;

                    // Run event. Note that this must be synchronized with the main thread, because the main thread may use the Lua state
                    // directly (such as for the dev console, or when closing sessions).
                    lock (ScriptContext)
                    {
                        switch (item.Type)
                        {
                            case ESceneEvent.Enter:
                                item.Scene.Enter();
                                break;
                            case ESceneEvent.Leave:
                                item.Scene.Leave();
                                break;
                            case ESceneEvent.Turn:
                                item.Scene.Turn(item.Data);
                                break;
                            default:
                                throw new NotSupportedException();
                        }
                    }

                    // Skip all further scene events if the game has ended
                    if (IsGameOver())
                    {
                        RunGameOver();
                        return;
                    }

                    GameUI.Instance.ControlsEnabled = true;
                }
            }
        }

        /// <summary>
        /// Describes a queued script callback.
        /// </summary>
        private struct SceneEvent
        {

            public Scene Scene { get; set; }
            public ESceneEvent Type { get; set; }
            public int Data { get; set; }

        }

    }

}
