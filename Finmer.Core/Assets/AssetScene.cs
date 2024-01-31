/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
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

        /// <summary>
        /// Describes a patch injection point, relative to a State node.
        /// </summary>
        public enum ESceneInjectMode : byte
        {
            BeforeTarget,
            AfterTarget,
            InsideAtStart,
            InsideAtEnd
        }

        /// <summary>
        /// Describes the specialization of a scene node.
        /// </summary>
        public enum ENodeType : byte
        {
            Root,
            State,
            Choice,
            Link,
            Compass
        }

        /// <summary>
        /// Describes which compass direction a compass link will be attached to.
        /// </summary>
        public enum ECompassDirection : byte
        {
            North,
            West,
            South,
            East
        }

        /// <summary>
        /// The root node of the scene node tree.
        /// </summary>
        public SceneNode Root { get; set; }

        /// <summary>
        /// A custom script header that will be inserted at the top of the generated scene script.
        /// </summary>
        public ScriptData ScriptCustom { get; set; }

        /// <summary>
        /// Script that will run when the player enters this scene.
        /// </summary>
        public ScriptData ScriptEnter { get; set; }

        /// <summary>
        /// Script that will run when the player leaves this scene, before entering another scene.
        /// </summary>
        public ScriptData ScriptLeave { get; set; }

        /// <summary>
        /// Precompiled script that represents the entire scene (all nodes and nested scripts), or null if unavailable.
        /// </summary>
        public CompiledScript PrecompiledScript { get; set; }

        /// <summary>
        /// Indicates whether this scene can be selected by the player as game start, when creating new save data.
        /// </summary>
        public bool IsGameStart { get; set; }

        /// <summary>
        /// Indicates whether this scene is a patch, meaning it adds or replaces nodes in another scene.
        /// </summary>
        public bool IsPatch { get; set; }

        /// <summary>
        /// The method with which to inject patch nodes. Relevant only if IsPatch is set.
        /// </summary>
        public ESceneInjectMode InjectMode { get; set; } = ESceneInjectMode.AfterTarget;

        /// <summary>
        /// The Asset ID of the target scene to inject a patch into. Relevant only if IsPatch is set.
        /// </summary>
        public Guid InjectTargetScene { get; set; } = Guid.Empty;

        /// <summary>
        /// The name of the scene node in the target scene into which to inject a patch. Relevant only if IsPatch is set.
        /// </summary>
        public string InjectTargetNode { get; set; } = String.Empty;

        /// <summary>
        /// User-friendly description of the game start.
        /// </summary>
        public string GameStartDescription { get; set; } = String.Empty;

        public override void Serialize(IFurballContentWriter outstream)
        {
            base.Serialize(outstream);

            // Custom scene scripts
            outstream.WriteNestedScriptProperty(nameof(ScriptCustom), ScriptCustom);
            outstream.WriteNestedScriptProperty(nameof(ScriptEnter), ScriptEnter);
            outstream.WriteNestedScriptProperty(nameof(ScriptLeave), ScriptLeave);

            // Game start settings
            outstream.WriteBooleanProperty(nameof(IsGameStart), IsGameStart);
            if (IsGameStart)
                outstream.WriteStringProperty(nameof(GameStartDescription), GameStartDescription);

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
            ScriptCustom = instream.ReadNestedObjectProperty<ScriptData>(nameof(ScriptCustom), version);
            ScriptEnter = instream.ReadNestedObjectProperty<ScriptData>(nameof(ScriptEnter), version);
            ScriptLeave = instream.ReadNestedObjectProperty<ScriptData>(nameof(ScriptLeave), version);

            // Game start settings
            if (version >= 20)
            {
                IsGameStart = instream.ReadBooleanProperty(nameof(IsGameStart));
                if (IsGameStart)
                    GameStartDescription = instream.ReadStringProperty(nameof(GameStartDescription));
            }

            // Assign script names
            if (ScriptCustom != null)
                ScriptCustom.Name = Name + "_Custom";
            if (ScriptEnter != null)
                ScriptEnter.Name = Name + "_Enter";
            if (ScriptLeave != null)
                ScriptLeave.Name = Name + "_Leave";

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
                if (node.NodeType == ENodeType.Link || node.NodeType == ENodeType.Root)
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

        /// <summary>
        /// Represents a node in the scene node tree.
        /// </summary>
        public class SceneNode
        {

            /// <summary>
            /// The specialization / intended purpose of this node.
            /// </summary>
            public ENodeType NodeType { get; set; }

            /// <summary>
            /// The unique identifier of this node. May be an empty string, in which case a key should be auto-generated.
            /// </summary>
            public string Key { get; set; } = String.Empty;

            /// <summary>
            /// Choice nodes: The caption of the choice button.
            /// </summary>
            public string Title { get; set; } = String.Empty;

            /// <summary>
            /// Choice nodes: The tooltip to display when the user hovers over this choice button.
            /// </summary>
            public string Tooltip { get; set; } = String.Empty;

            /// <summary>
            /// Choice nodes: Indicates whether to visually accentuate the choice button.
            /// </summary>
            public bool Highlight { get; set; }

            /// <summary>
            /// Choice nodes: Size multiplier for the choice button's on-screen width, as a factor of its default size.
            /// </summary>
            public float ButtonWidth { get; set; } = 1.0f;

            /// <summary>
            /// Compass nodes: Compass direction to associate with the compass link.
            /// </summary>
            public ECompassDirection CompassLinkDirection { get; set; } = ECompassDirection.North;

            /// <summary>
            /// Compass nodes: Asset ID of the target scene. May be empty if an Actions Taken script is specified instead.
            /// </summary>
            public Guid CompassLinkScene { get; set; } = Guid.Empty;

            /// <summary>
            /// Link nodes: Key of the scene node to resolve this link node to.
            /// </summary>
            public string LinkTarget { get; set; } = String.Empty;

            /// <summary>
            /// State, Choice, Compass nodes: Actions Taken script that will run when the user 'activates' the node.
            /// </summary>
            public ScriptData ScriptAction { get; set; }

            /// <summary>
            /// State, Choice, Compass nodes: Appears When script that determines whether the node may be displayed and/or activated.
            /// </summary>
            public ScriptData ScriptAppear { get; set; }

            /// <summary>
            /// State, Choice nodes: Tree of downstream scene nodes.
            /// </summary>
            public List<SceneNode> Children { get; } = new List<SceneNode>();

            /// <summary>
            /// The parent node of this node. If this node is the tree root, parent is null.
            /// </summary>
            public SceneNode Parent { get; set; }

            /// <summary>
            /// Indicates whether this node is a full member of the scene tree and can have child nodes. If false, the node is a leaf node.
            /// </summary>
            public bool IsFullNode()
            {
                return NodeType == ENodeType.Root || NodeType == ENodeType.Choice || NodeType == ENodeType.State;
            }

            public void Serialize(IFurballContentWriter outstream)
            {
                // Core metadata
                outstream.WriteEnumProperty(nameof(NodeType), NodeType);
                outstream.WriteStringProperty(nameof(Key), Key);

                bool write_scripts = false;
                bool write_children = false;

                // Node-specific metadata
                switch (NodeType)
                {
                    case ENodeType.Choice:
                        // Choice node configuration
                        outstream.WriteStringProperty(nameof(Title), Title);
                        outstream.WriteStringProperty(nameof(Tooltip), Tooltip);
                        outstream.WriteBooleanProperty(nameof(Highlight), Highlight);
                        outstream.WriteFloatProperty(nameof(ButtonWidth), ButtonWidth);
                        write_scripts = true;
                        write_children = true;
                        break;

                    case ENodeType.State:
                        write_scripts = true;
                        write_children = true;
                        break;

                    case ENodeType.Root:
                        write_children = true;
                        break;

                    case ENodeType.Link:
                        // Node link configuration
                        outstream.WriteStringProperty(nameof(LinkTarget), LinkTarget);
                        break;

                    case ENodeType.Compass:
                        // Compass link configuration
                        outstream.WriteEnumProperty(nameof(CompassLinkDirection), CompassLinkDirection);
                        outstream.WriteGuidProperty(nameof(CompassLinkScene), CompassLinkScene);
                        write_scripts = true;
                        break;
                }

                if (write_scripts)
                {
                    // Write node scripts
                    outstream.WriteNestedScriptProperty(nameof(ScriptAction), ScriptAction);
                    outstream.WriteNestedScriptProperty(nameof(ScriptAppear), ScriptAppear);
                }

                if (write_children)
                {
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
                NodeType = instream.ReadEnumProperty<ENodeType>(nameof(NodeType));
                Key = instream.ReadStringProperty(nameof(Key));

                bool read_scripts = false;
                bool read_children = false;

                // Read node-specific settings
                switch (NodeType)
                {
                    case ENodeType.Choice:
                        Title = instream.ReadStringProperty(nameof(Title));
                        Tooltip = instream.ReadStringProperty(nameof(Tooltip));
                        Highlight = instream.ReadBooleanProperty(nameof(Highlight));
                        ButtonWidth = instream.ReadFloatProperty(nameof(ButtonWidth));
                        read_scripts = true;
                        read_children = true;
                        break;

                    case ENodeType.State:
                        read_scripts = true;
                        read_children = true;
                        break;

                    case ENodeType.Root:
                        read_children = true;
                        break;

                    case ENodeType.Link:
                        LinkTarget = instream.ReadStringProperty(nameof(LinkTarget));
                        break;

                    case ENodeType.Compass:
                        CompassLinkDirection = instream.ReadEnumProperty<ECompassDirection>(nameof(CompassLinkDirection));
                        CompassLinkScene = instream.ReadGuidProperty(nameof(CompassLinkScene));
                        read_scripts = true;
                        break;
                }

                if (read_scripts)
                {
                    // Deserialize scripts
                    ScriptAction = instream.ReadNestedObjectProperty<ScriptData>(nameof(ScriptAction), version);
                    ScriptAppear = instream.ReadNestedObjectProperty<ScriptData>(nameof(ScriptAppear), version);

                    // Correct script names
                    if (ScriptAction != null)
                        ScriptAction.Name = Key + "/Actions";
                    if (ScriptAppear != null)
                        ScriptAppear.Name = Key + "/AppearsWhen";
                }

                if (read_children)
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
