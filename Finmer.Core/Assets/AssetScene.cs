/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using Finmer.Core.Serialization;

namespace Finmer.Core.Assets
{

    /// <summary>
    /// Represents a node-driven interactive dialogue.
    /// </summary>
    public class AssetScene : AssetBase
    {

        public enum ESceneInjectMode : byte
        {
            BeforeTarget,
            AfterTarget,
            InsideAtStart,
            InsideAtEnd
        }

        public SceneNode Root { get; set; }

        public AssetScript ScriptCustom { get; set; }
        public AssetScript ScriptEnter { get; set; }
        public AssetScript ScriptLeave { get; set; }

        /// <summary>
        /// Precompiled script that represents the entire scene (all nodes and nested scripts), or null if unavailable.
        /// </summary>
        public CompiledScript PrecompiledScript { get; set; }

        public bool Inject { get; set; }
        public ESceneInjectMode InjectMode { get; set; } = ESceneInjectMode.AfterTarget;
        public Guid InjectScene { get; set; } = Guid.Empty;
        public string InjectNode { get; set; } = String.Empty;

        public override void Serialize(IFurballContentWriter outstream)
        {
            base.Serialize(outstream);

            // Custom scene scripts
            outstream.BeginObject("ScriptCustom");
            ScriptCustom.Serialize(outstream);
            outstream.EndObject();
            outstream.BeginObject("ScriptEnter");
            ScriptEnter.Serialize(outstream);
            outstream.EndObject();
            outstream.BeginObject("ScriptLeave");
            ScriptLeave.Serialize(outstream);
            outstream.EndObject();

            // Patching settings
            outstream.WriteBooleanProperty("IsPatch", Inject);
            if (Inject)
            {
                outstream.WriteEnumProperty("InjectMode", InjectMode);
                outstream.WriteGuidProperty("InjectTargetScene", InjectScene);
                outstream.WriteStringProperty("InjectTargetNode", InjectNode);
            }

            // Scene node hierarchy
            outstream.BeginObject("Root");
            Root.Serialize(outstream);
            outstream.EndObject();
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            base.Deserialize(instream, version);

            // Read scene scripts
            instream.BeginObject("ScriptCustom");
            {
                ScriptCustom = new AssetScript();
                ScriptCustom.Deserialize(instream, version);
            }
            instream.EndObject();
            instream.BeginObject("ScriptEnter");
            {
                ScriptEnter = new AssetScript();
                ScriptEnter.Deserialize(instream, version);
            }
            instream.EndObject();
            instream.BeginObject("ScriptLeave");
            {
                ScriptLeave = new AssetScript();
                ScriptLeave.Deserialize(instream, version);
            }
            instream.EndObject();

            // Read modding/injection settings
            Inject = instream.ReadBooleanProperty("IsPatch");
            if (Inject)
            {
                InjectMode = instream.ReadEnumProperty<ESceneInjectMode>("InjectMode");
                InjectScene = instream.ReadGuidProperty("InjectTargetScene");
                InjectNode = instream.ReadStringProperty("InjectTargetNode");
            }

            // Read the scene node tree recursively
            instream.BeginObject("Root");
            Root = new SceneNode();
            Root.Deserialize(instream, version);
            instream.EndObject();
        }

        /// <summary>
        /// Finds the node in the scene graph with the specified key.
        /// </summary>
        /// <param name="key">The unique key to find.</param>
        public SceneNode GetNodeByKey(string key)
        {
            // Prepare a stack with the root elements in it
            Stack<SceneNode> stack = new Stack<SceneNode>();
            foreach (var root_child in Root.Children)
                stack.Push(root_child);

            // Flood-fill down the tree until the requested node is found
            while (stack.Count > 0)
            {
                SceneNode node = stack.Pop();

                // Links do not have keys
                if (node.IsLink)
                    continue;

                // Is this the node we're looking for?
                if (node.Key.Equals(key))
                    return node;

                // Recursively examine the node's children
                foreach (var child in node.Children)
                    stack.Push(child);
            }

            return null;
        }

        public class SceneNode
        {

            public string Key { get; set; } = String.Empty;
            public string Title { get; set; } = String.Empty;
            public string Tooltip { get; set; } = String.Empty;

            public bool IsState { get; set; }
            public bool IsLink { get; set; }
            public string LinkTarget { get; set; } = String.Empty;

            public string ScriptAction { get; set; } = String.Empty;
            public string ScriptAppear { get; set; } = String.Empty;

            public bool Highlight { get; set; }
            public float ButtonWidth { get; set; } = 1.0f;

            public List<SceneNode> Children { get; } = new List<SceneNode>();
            public SceneNode Parent { get; private set; }

            public void Serialize(IFurballContentWriter outstream)
            {
                // Core metadata
                outstream.WriteStringProperty("Key", Key);
                outstream.WriteBooleanProperty("IsState", IsState);
                outstream.WriteBooleanProperty("IsLink", IsLink);

                // Choice node settings
                if (!IsState && !IsLink)
                {
                    outstream.WriteStringProperty("Title", Title);
                    outstream.WriteStringProperty("Tooltip", Tooltip);
                    outstream.WriteBooleanProperty("Highlight", Highlight);
                    outstream.WriteFloatProperty("ButtonWidth", ButtonWidth);
                }

                // Link node settings
                if (IsLink)
                {
                    outstream.WriteStringProperty("LinkTarget", LinkTarget);
                }
                // Generic node settings
                else
                {
                    outstream.WriteStringProperty("ScriptAction", ScriptAction);
                    outstream.WriteStringProperty("ScriptAppear", ScriptAppear);

                    // Recursively serialize child nodes
                    outstream.BeginArray("Children", Children.Count);
                    foreach (SceneNode child in Children)
                    {
                        outstream.BeginObject();
                        child.Serialize(outstream);
                        outstream.EndObject();
                    }
                    outstream.EndArray();
                }
            }

            public void Deserialize(IFurballContentReader instream, int version)
            {
                // Core metadata
                Key = instream.ReadStringProperty("Key");
                IsState = instream.ReadBooleanProperty("IsState");
                IsLink = instream.ReadBooleanProperty("IsLink");

                // Choice node settings
                if (!IsState && !IsLink)
                {
                    Title = instream.ReadStringProperty("Title");
                    Tooltip = instream.ReadStringProperty("Tooltip");
                    Highlight = instream.ReadBooleanProperty("Highlight");
                    ButtonWidth = instream.ReadFloatProperty("ButtonWidth");
                }

                // Link node settings
                if (IsLink)
                {
                    LinkTarget = instream.ReadStringProperty("LinkTarget");
                }
                // Generic node settings
                else
                {
                    ScriptAction = instream.ReadStringProperty("ScriptAction");
                    ScriptAppear = instream.ReadStringProperty("ScriptAppear");
                }

                // Links do not have child nodes as of version 15
                if (version <= 14 || !IsLink)
                {
                    // Recursively deserialize child nodes
                    Children.Clear();
                    for (int child_count = instream.BeginArray("Children"); child_count > 0; child_count--)
                    {
                        instream.BeginObject();
                        {
                            // Read this child node
                            var child = new SceneNode();
                            child.Deserialize(instream, version);
                            child.Parent = this;
                            Children.Add(child);
                        }
                        instream.EndObject();
                    }
                    instream.EndArray();
                }
            }

        }

    }

}
