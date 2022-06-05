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

        public ScriptData ScriptCustom { get; set; }

        public ScriptData ScriptEnter { get; set; }

        public ScriptData ScriptLeave { get; set; }

        /// <summary>
        /// Precompiled script that represents the entire scene (all nodes and nested scripts), or null if unavailable.
        /// </summary>
        public CompiledScript PrecompiledScript { get; set; }

        public bool IsPatch { get; set; }
        public ESceneInjectMode InjectMode { get; set; } = ESceneInjectMode.AfterTarget;
        public Guid InjectTargetScene { get; set; } = Guid.Empty;
        public string InjectTargetNode { get; set; } = String.Empty;

        public override void Serialize(IFurballContentWriter outstream)
        {
            base.Serialize(outstream);

            // Custom scene scripts
            outstream.WriteNestedScriptProperty(nameof(ScriptCustom), ScriptCustom);
            outstream.WriteNestedScriptProperty(nameof(ScriptEnter), ScriptEnter);
            outstream.WriteNestedScriptProperty(nameof(ScriptLeave), ScriptLeave);

            // Patching settings
            outstream.WriteBooleanProperty(nameof(IsPatch), IsPatch);
            if (IsPatch)
            {
                outstream.WriteEnumProperty(nameof(InjectMode), InjectMode);
                outstream.WriteGuidProperty(nameof(InjectTargetScene), InjectTargetScene);
                outstream.WriteStringProperty(nameof(InjectTargetNode), InjectTargetNode);
            }

            // Scene node hierarchy
            outstream.BeginObject(nameof(Root));
            Root.Serialize(outstream);
            outstream.EndObject();
        }

        public override void Deserialize(IFurballContentReader instream, int version)
        {
            base.Deserialize(instream, version);

            // Read scene scripts
            if (version >= 16)
            {
                ScriptCustom = instream.ReadNestedObjectProperty<ScriptData>(nameof(ScriptCustom), version);
                ScriptEnter = instream.ReadNestedObjectProperty<ScriptData>(nameof(ScriptEnter), version);
                ScriptLeave = instream.ReadNestedObjectProperty<ScriptData>(nameof(ScriptLeave), version);

                // Assign script names
                if (ScriptCustom != null)
                    ScriptCustom.Name = Name + "_Custom";
                if (ScriptEnter != null)
                    ScriptEnter.Name = Name + "_Enter";
                if (ScriptLeave != null)
                    ScriptLeave.Name = Name + "_Leave";
            }
            else
            {
                // V15 backwards compatibility
                instream.BeginObject("ScriptCustom");
                {
                    instream.ReadGuidProperty("ID"); // Skip property
                    ScriptCustom = new ScriptDataExternal();
                    ScriptCustom.Deserialize(instream, version);
                }
                instream.EndObject();
                instream.BeginObject("ScriptEnter");
                {
                    instream.ReadGuidProperty("ID"); // Skip property
                    ScriptEnter = new ScriptDataExternal();
                    ScriptEnter.Deserialize(instream, version);
                }
                instream.EndObject();
                instream.BeginObject("ScriptLeave");
                {
                    instream.ReadGuidProperty("ID"); // Skip property
                    ScriptLeave = new ScriptDataExternal();
                    ScriptLeave.Deserialize(instream, version);
                }
                instream.EndObject();

                // Update script names to new standards
                ScriptCustom.Name = Name + "_Custom";
                ScriptEnter.Name = Name + "_Enter";
                ScriptLeave.Name = Name + "_Leave";
            }

            // Read modding/injection settings
            IsPatch = instream.ReadBooleanProperty(nameof(IsPatch));
            if (IsPatch)
            {
                InjectMode = instream.ReadEnumProperty<ESceneInjectMode>(nameof(InjectMode));
                InjectTargetScene = instream.ReadGuidProperty(nameof(InjectTargetScene));
                InjectTargetNode = instream.ReadStringProperty(nameof(InjectTargetNode));
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

            public ScriptData ScriptAction { get; set; }
            public ScriptData ScriptAppear { get; set; }

            public bool Highlight { get; set; }
            public float ButtonWidth { get; set; } = 1.0f;

            public List<SceneNode> Children { get; } = new List<SceneNode>();
            public SceneNode Parent { get; private set; }

            public void Serialize(IFurballContentWriter outstream)
            {
                // Core metadata
                outstream.WriteStringProperty(nameof(Key), Key);
                outstream.WriteBooleanProperty(nameof(IsState), IsState);
                outstream.WriteBooleanProperty(nameof(IsLink), IsLink);

                // Choice node settings
                if (!IsState && !IsLink)
                {
                    outstream.WriteStringProperty(nameof(Title), Title);
                    outstream.WriteStringProperty(nameof(Tooltip), Tooltip);
                    outstream.WriteBooleanProperty(nameof(Highlight), Highlight);
                    outstream.WriteFloatProperty(nameof(ButtonWidth), ButtonWidth);
                }

                // Link node settings
                if (IsLink)
                {
                    outstream.WriteStringProperty(nameof(LinkTarget), LinkTarget);
                }
                // Generic node settings
                else
                {
                    outstream.WriteNestedScriptProperty(nameof(ScriptAction), ScriptAction);
                    outstream.WriteNestedScriptProperty(nameof(ScriptAppear), ScriptAppear);

                    // Recursively serialize child nodes
                    outstream.BeginArray(nameof(Children), Children.Count);
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
                Key = instream.ReadStringProperty(nameof(Key));
                IsState = instream.ReadBooleanProperty(nameof(IsState));
                IsLink = instream.ReadBooleanProperty(nameof(IsLink));

                // Choice node settings
                if (!IsState && !IsLink)
                {
                    Title = instream.ReadStringProperty(nameof(Title));
                    Tooltip = instream.ReadStringProperty(nameof(Tooltip));
                    Highlight = instream.ReadBooleanProperty(nameof(Highlight));
                    ButtonWidth = instream.ReadFloatProperty(nameof(ButtonWidth));
                }

                // Link node settings
                if (IsLink)
                {
                    LinkTarget = instream.ReadStringProperty(nameof(LinkTarget));
                }
                // Generic node settings
                else
                {
                    if (version >= 16)
                    {
                        ScriptAction = instream.ReadNestedObjectProperty<ScriptData>(nameof(ScriptAction), version);
                        ScriptAppear = instream.ReadNestedObjectProperty<ScriptData>(nameof(ScriptAppear), version);

                        // Assign script names
                        if (ScriptAction != null)
                            ScriptAction.Name = Key + "/Actions";
                        if (ScriptAppear != null)
                            ScriptAppear.Name = Key + "/AppearsWhen";
                    }
                    else
                    {
                        // V15 backwards compatibility: convert raw strings to inline script objects
                        var text = instream.ReadStringProperty(nameof(ScriptAction));
                        if (!String.IsNullOrEmpty(text))
                        {
                            ScriptAction = new ScriptDataInline
                            {
                                Name = Key + "/Actions",
                                ScriptText = text
                            };
                        }
                        text = instream.ReadStringProperty(nameof(ScriptAppear));
                        if (!String.IsNullOrEmpty(text))
                        {
                            ScriptAppear = new ScriptDataInline
                            {
                                Name = Key + "/AppearsWhen",
                                ScriptText = text
                            };
                        }
                    }
                }

                // Links do not have child nodes as of version 15
                if (version <= 14 || !IsLink)
                {
                    // Recursively deserialize child nodes
                    Children.Clear();
                    for (int child_count = instream.BeginArray(nameof(Children)); child_count > 0; child_count--)
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
