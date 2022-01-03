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
using System.Threading;
using Finmer.Core.Assets;
using Finmer.Gameplay.Scripting;
using Finmer.Models;
using Finmer.Utility;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Describes an event to trigger in a scene.
    /// </summary>
    public enum ESceneEvent
    {
        Enter,
        Leave,
        Turn,
        Link
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
        /// The name of the default scene asset to load when save data does not provide one (i.e. a new game).
        /// </summary>
        private const string k_DefaultSceneName = @"Scene_Intro";

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

        /// <summary>
        /// The last checkpoint that was captured during this game, or null if unavailable.
        /// </summary>
        public GameSnapshot LastCheckpoint { get; set; }

        /// <summary>
        /// Indicates whether this session has not yet completed a full turn cycle (i.e. the game is being started).
        /// </summary>
        public bool IsRestoringGame { get; private set; } = true;

        private readonly Queue<SceneEvent> m_EventQueue = new Queue<SceneEvent>();
        private readonly object m_EventQueueLock = new object();
        private readonly Stack<Scene> m_SceneStack = new Stack<Scene>();
        private readonly Thread m_ScriptThread;
        private readonly AutoResetEvent m_ScriptWaitEvent;

        private GameSnapshot m_RestoreSnapshot;
        private bool m_ScriptThreadStop;
        private bool m_GameOverRequested;

        public GameSession(GameSnapshot snapshot)
        {
            Debug.Assert(GameController.Session == null, "This class manipulates singletons, so there should be only one instance");

            // Initialize the session
            Compass = new CompassController(this);
            Player = new Player(ScriptContext, snapshot.PlayerData);

            // Allow scripts to access the player object as a global variable
            ScriptContext.PinObjectAsGlobal(Player, "Player");

            // Bind player grammar context
            TextParser.ClearAllContexts();
            TextParser.SetContext("player", Player, true);

            // Restore UI state
            GameUI.Reset();
            GameUI.Instance.Deserialize(snapshot.InterfaceData);

            // Run global scripts
            foreach (var script in GameController.Content.GetAssetsByType<AssetScript>())
                if (ScriptContext.LoadScript(script.PrecompiledScript, script.Name))
                    ScriptContext.RunProtectedCall(0, 0);

            // Check if we have write perms
            if (!Logger.HasWritePermission())
                GameUI.Instance.Log("Warning: It looks like the game does not have permission to write files to the app " +
                    "directory. This means that you cannot save your game.\r\n", Theme.LogColorError);

            // Prepare the script thread (but do not launch it yet)
            m_RestoreSnapshot = snapshot;
            m_ScriptWaitEvent = new AutoResetEvent(false);
            m_ScriptThread = new Thread(ScriptThreadWorker, k_ScriptStackSize)
            {
                IsBackground = true
            };
        }

        /// <summary>
        /// Launch the script thread for this GameSession. Should be called after the Session global has been assigned.
        /// </summary>
        public void Start()
        {
            Debug.Assert(GameController.Session == this, "This class manipulates singletons, so there should be only one instance");
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
            Compass.Dispose();
            ScriptContext.Dispose();
        }

        /// <summary>
        /// Validate that the calling thread is the script thread.
        /// </summary>
        [Conditional("DEBUG")]
        public void VerifyScriptThread()
        {
            Debug.Assert(Thread.CurrentThread == m_ScriptThread, "This function must be executed on the script thread");
        }

        /// <summary>
        /// Creates and returns a snapshot of the GameSession, that can be used to restore the session state later.
        /// </summary>
        public GameSnapshot CaptureSnapshot()
        {
            // We're expecting save data to only be created while regular gameplay scenes are active
            Debug.Assert(m_SceneStack.Count == 1, "Save data does not support stacked scenes");
            SceneScripted current_scene = (SceneScripted)PeekScene();

            // Serialize all different types of data to create a save data object
            return new GameSnapshot(
                Player.SerializeProperties(),
                current_scene.Serialize(),
                GameUI.Instance.Serialize()
            );
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

        /// <summary>
        /// Send the request for the game to end.
        /// </summary>
        public void RequestGameOver()
        {
            m_GameOverRequested = true;
        }

        private bool IsGameOver()
        {
            return Player.IsDead() || m_GameOverRequested;
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
            // Restore saved scene data. Note that we do this on the script thread because restoring may lead to invoking state node
            // functions and the like, which may sleep or perform complicated processing, and the main thread (UI) should not block.
            RestoreScene(m_RestoreSnapshot);
            m_RestoreSnapshot = null;

            // Main loop
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
                    if (item.Type != ESceneEvent.Link)
                        Compass.Reset();

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

                            case ESceneEvent.Link:
                                Compass.ExecuteLink((ECompassDirection)item.Data);
                                break;

                            default:
                                throw new NotSupportedException();
                        }
                    }

                    // We've now completed a full turn
                    IsRestoringGame = false;

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

        private void RestoreScene(GameSnapshot snapshot)
        {
            // Grab the GUID of the scene to restore
            var scene_bytes = snapshot.SceneData.GetBytes(SaveData.k_System_CurrentSceneID);
            if (scene_bytes == null)
            {
                // Save data does not include a scene GUID; fall back to the default initial scene
                var initial_scene = (AssetScene)GameController.Content.GetAssetByName(k_DefaultSceneName);
                PushScene(new SceneScripted(ScriptContext, initial_scene));
            }
            else
            {
                // Find this asset in content
                var asset_id = new Guid(scene_bytes);
                var asset = (AssetScene)GameController.Content.GetAssetByID(asset_id);

                // Add this scene onto the stack.
                // Note that we do not use PushScene() here, since that queues Enter and Turn events which we do not want.
                var restored_scene = new SceneScripted(ScriptContext, asset);
                m_SceneStack.Push(restored_scene);

                // Restore state and run the next state function (as if it were the initial turn)
                restored_scene.Deserialize(snapshot.SceneData);
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
