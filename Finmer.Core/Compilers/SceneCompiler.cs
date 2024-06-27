/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Finmer.Core.Assets;
using Finmer.Core.Serialization;
using Node = Finmer.Core.Assets.SceneNode;

namespace Finmer.Core.Compilers
{

    /// <summary>
    /// Tool for compiling an <seealso cref="AssetScene" /> into a Lua script.
    /// </summary>
    public static class SceneCompiler
    {

        /// <summary>
        /// Compiles the given Scene asset and saves the Lua script representing it.
        /// </summary>
        /// <param name="scene">The scene asset to compile.</param>
        /// <param name="compiler">A Lua script compiler, used for verifying syntax.</param>
        /// <param name="content">Interface for resolving links to assets.</param>
        /// <exception cref="SceneCompilerException">Throws if any part of scene compilation failed.</exception>
        public static void Compile(AssetScene scene, IScriptCompiler compiler, IContentStore content)
        {
            var state = new CompilerState
            {
                Compiler = compiler,
                Content = content,
                Scene = scene
            };

            try
            {
                // Upvalues go first, to ensure references are actually compiled as upvalues and not global lookups.
                // Note: We prefix all identifiers with an underscore to prevent name clashes with user-defined code.
                state.Main.AppendLine("local _state = 0");

                // Custom script goes at the top, so it is available globally
                if (scene.ScriptCustom != null && scene.ScriptCustom.HasContent())
                {
                    var text = scene.ScriptCustom.GetScriptText(content);
                    state.Compiler?.Compile(text, "CustomScript");
                    state.Main.AppendLine(text);
                }

                // Write headers for each subsection
                state.TableStates.AppendLine("local _States = {");
                state.TableChoices.AppendLine("local _Choices = {");
                state.TableStateFns.AppendLine("local _StateFns = {}");
                state.TableChoiceFns.AppendLine("local _ChoiceFns = {}");
                state.TableAppearFns.AppendLine("local _AppearFns = {");

                // Compile all scene nodes recursively, starting with the root. Each node appends code to the various tables in CompilerState.
                // We use a stack instead of call recursion because it's significantly faster and conserves stack space.
                state.Backlog.Push(scene.Root);
                while (state.Backlog.Count > 0)
                {
                    Node node = state.Backlog.Pop();
                    CompileNode(state, node);
                }

                // Close subsections
                state.TableStates.AppendLine("}");
                state.TableChoices.AppendLine("}");
                state.TableAppearFns.AppendLine("}");
                state.TableAppearFns.AppendLine("local function _CanAppear(key) return _AppearFns[key] == nil or _AppearFns[key]() == true end");

                // Merge all subsections into the main script
                state.Main.Append(state.TableStates); // States/Choices tables first, so they become upvalues for the other functions
                state.Main.Append(state.TableChoices);
                state.Main.Append(state.TableAppearFns);
                state.Main.Append(state.TableStateFns);
                state.Main.Append(state.TableChoiceFns);

                // Copy OnEnter script
                if (scene.ScriptEnter != null && scene.ScriptEnter.HasContent())
                {
                    var text = scene.ScriptEnter.GetScriptText(content);
                    state.Compiler?.Compile(text, "EnterScript");
                    state.Main.AppendLine("function OnEnter()");
                    state.Main.AppendLine(text);
                    state.Main.AppendLine("end");
                }

                // Copy OnLeave script
                if (scene.ScriptLeave != null && scene.ScriptLeave.HasContent())
                {
                    var text = scene.ScriptLeave.GetScriptText(content);
                    state.Compiler?.Compile(text, "LeaveScript");
                    state.Main.AppendLine("function OnLeave()");
                    state.Main.AppendLine(text);
                    state.Main.AppendLine("end");
                }

                // Write boilerplate code that drives the scene.
                // In OnTurn, we run the user's chosen ChoiceFn, then run the new StateFn that rolls out of that ChoiceFn.
                state.Main.AppendLine(@"
function _CaptureState()
    for k, v in pairs(_States) do
        if v == _state then return k end
    end
end
function _RestoreState(state_name)
    _state = _States[state_name]
    _StateFns[_state]()
end
function OnTurn(choice)
    _ChoiceFns[choice]()
    _StateFns[_state]()
end"
                );

                // Compile and return the finished script
                var complete_script = state.Main.ToString();
                scene.PrecompiledScript = compiler.Compile(complete_script, scene.Name);
            }
            catch (ScriptCompilationException ex)
            {
                // Add context info and rethrow as a SceneCompilerException, so client has to handle only one type
                throw new SceneCompilerException("Script compilation error: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Finds the node in the scene graph with the specified key.
        /// </summary>
        /// <param name="state">A reference to the scene undergoing compilation.</param>
        /// <param name="key">The unique key to find.</param>
        private static Node FindNodeByKey(CompilerState state, string key)
        {
            return state.Scene.GetNodeByKey(key);
        }

        /// <summary>
        /// Compiles a single <seealso cref="SceneNode" /> into Lua code.
        /// </summary>
        /// <param name="state">The object in which to store the generated code.</param>
        /// <param name="node">The node to generate code for.</param>
        private static void CompileNode(CompilerState state, Node node)
        {
            // Basic validation
            if (node.Key.Length > 32)
                throw new SceneCompilerException($"Node key '{node.Key}' is too long. The limit is 32 characters.");
            if (state.NodeNames.Contains(node.Key))
                throw new SceneCompilerException($"Node key '{node.Key}' is used more than once.");

            // Sanity checks that indicate deserialization errors
            Debug.Assert(node.IsFullNode() || node.Children.Count == 0,
                $"Node '{node.Key}' is a link, but also contains child nodes. This indicates a deserialization bug.");

            // Enqueue all children
            foreach (Node child in node.Children)
            {
                // States and choices must alternate
                if (node.NodeType == child.NodeType)
                    throw new SceneCompilerException($"Node '{node.Key}' contains child '{child.Key}' of same node type as parent ({node.NodeType}). Module file is likely corrupted.");

                // Nested nodes can never be the scene root node
                if (child.NodeType == SceneNode.ENodeType.Root)
                    throw new SceneCompilerException($"Node '{node.Key}' is a Root node, but nested nodes cannot be Root nodes. Module file is likely corrupted.");

                // If the node key is unspecified, assign a default value instead.
                // This must be done before code is emitted for the parent node because the parent may refer to the keys of child nodes.
                if (String.IsNullOrWhiteSpace(child.Key))
                    child.Key = $"_AutoKey{state.NextAutoKey++}";

                // If a Choice node's button caption is unspecified, assign a default value instead.
                if (child.NodeType == SceneNode.ENodeType.Choice && String.IsNullOrWhiteSpace(child.Title))
                    child.Title = "Continue";

                // Queue this child
                state.Backlog.Push(child);
            }

            // Generate an AppearFn for all concrete nodes that have one specified
            if (node.ScriptAppear != null && node.ScriptAppear.HasContent())
            {
                Debug.Assert(node.NodeType != SceneNode.ENodeType.Link, "Node links cannot have appears-when functions. This indicates a deserialization bug.");

                var script_text = node.ScriptAppear.GetScriptText(state.Content);
                state.Compiler?.Compile(script_text, $"{node.Key}/AppearsWhen");
                state.TableAppearFns.Append(node.Key);
                state.TableAppearFns.AppendLine(" = function()");
                state.TableAppearFns.AppendLine(script_text);
                state.TableAppearFns.AppendLine("end,");
            }

            // Track the node name so we can verify its uniqueness
            if (node.IsFullNode())
                state.NodeNames.Add(node.Key);

            // Generate code for the downstream node.
            // Note that code generation for node links and compass links are handled by their parent nodes.
            switch (node.NodeType)
            {
                case SceneNode.ENodeType.State:
                    CompileStateNode(state, node);
                    break;

                case SceneNode.ENodeType.Root:
                case SceneNode.ENodeType.Choice:
                    CompileChoiceNode(state, node);
                    break;
            }
        }

        /// <summary>
        /// Specialized node compiler that generates code for State nodes.
        /// </summary>
        /// <param name="state">The object in which to store the generated code.</param>
        /// <param name="node">The node to generate code for.</param>
        private static void CompileStateNode(CompilerState state, Node node)
        {
            Debug.Assert(node.NodeType == SceneNode.ENodeType.State);

            state.TableStates.AppendLine($"{node.Key} = {state.NextStateID++},");
            state.TableStateFns.AppendLine($"_StateFns[_States.{node.Key}] = function()");

            // Inject the user's 'Actions Taken' script if it's non-empty
            if (node.ScriptAction != null && node.ScriptAction.HasContent())
            {
                var script_text = node.ScriptAction.GetScriptText(state.Content);
                state.Compiler?.Compile(script_text, $"{node.Key}/ActionsTaken");
                state.TableStateFns.AppendLine(script_text);
            }

            // Emit AddButton calls (wrapped in CanAppear tests) for all child Choices of this State
            foreach (Node child in node.Children)
            {
                Node resolved_child = child;

                // Follow links to their respective targets
                if (child.NodeType == SceneNode.ENodeType.Link)
                {
                    // Dereference the link target node
                    string link_target_key = child.LinkTarget;
                    resolved_child = FindNodeByKey(state, link_target_key);

                    // Validate the link
                    if (resolved_child == null)
                        throw new SceneCompilerException($"Node '{node.Key}' contains a link to '{link_target_key}' but no such node exists");
                    if (resolved_child.NodeType == SceneNode.ENodeType.State)
                        throw new SceneCompilerException($"Node '{node.Key}' contains a link to '{link_target_key}' which is a state, but parent '{node.Parent.Key}' is also a state; this is not supported");
                    if (resolved_child.NodeType == SceneNode.ENodeType.Link)
                        throw new SceneCompilerException($"Node '{node.Key}' contains a link to '{link_target_key}' which is also a link. Recursive link resolving is not supported.");
                }

                // Generate code for the resolved child node
                switch (resolved_child.NodeType)
                {
                    case SceneNode.ENodeType.Choice:
                        // Emit a call to the 'Appears When' test, then the AddButton call if it passes.
                        string formatted_button_width = String.Format(CultureInfo.InvariantCulture, "{0:F3}", resolved_child.ButtonWidth);
                        state.TableStateFns.Append($"if _CanAppear(\"{resolved_child.Key}\") then ");
                        state.TableStateFns.Append($"AddButton(_Choices.{resolved_child.Key}, \"{CoreUtility.EscapeLuaString(resolved_child.Title)}\", \"{CoreUtility.EscapeLuaString(resolved_child.Tooltip)}\", {(resolved_child.Highlight ? "true" : "false")}, {formatted_button_width})");
                        state.TableStateFns.AppendLine(" end");

                        break;

                    case SceneNode.ENodeType.Compass:
                        // Emit an 'Appears When' test, followed by an AddLink call
                        state.TableStateFns.Append($"if _CanAppear(\"{resolved_child.Key}\") then ");
                        state.TableStateFns.Append($"AddLink(ECompass.{resolved_child.CompassLinkDirection}, ");

                        // Emit the compass link target
                        if (resolved_child.ScriptAction != null && resolved_child.ScriptAction.HasContent())
                        {
                            // If the compass link has a script, inject that script code here
                            var script_text = resolved_child.ScriptAction.GetScriptText(state.Content);
                            state.Compiler?.Compile(script_text, $"{resolved_child.Key}/ActionsTaken");
                            state.TableStateFns.AppendLine("function()");
                            state.TableStateFns.AppendLine(script_text);
                            state.TableStateFns.AppendLine("end");
                        }
                        else
                        {
                            // Compass must specify either a target scene or a script
                            if (resolved_child.CompassLinkScene == Guid.Empty)
                                throw new SceneCompilerException($"Compass node '{resolved_child.Key}' contains neither a target scene nor an Actions Taken script; one of these is required");

                            // Otherwise, the link should target a scene asset directly
                            var resolved_scene = state.Content.GetAssetByID<AssetScene>(resolved_child.CompassLinkScene);
                            if (resolved_scene == null)
                                throw new SceneCompilerException($"Compass node '{resolved_child.Key}' has target scene '{resolved_child.CompassLinkScene}', but no such Scene exists");

                            state.TableStateFns.Append($"\"{CoreUtility.EscapeLuaString(resolved_scene.Name)}\"");
                        }

                        state.TableStateFns.AppendLine(") end");
                        break;

                    default:
                        // States cannot contain States
                        Debug.Fail($"Node '{node.Key}' resolved to child '{resolved_child.Key}' with unexpected type; this should have been caught earlier");
                        break;
                }
            }

            state.TableStateFns.AppendLine("end");
        }

        /// <summary>
        /// Specialized node compiler that generates code for Choice nodes.
        /// </summary>
        /// <param name="state">The object in which to store the generated code.</param>
        /// <param name="node">The node to generate code for.</param>
        private static void CompileChoiceNode(CompilerState state, Node node)
        {
            Debug.Assert(node.NodeType == SceneNode.ENodeType.Choice || node.NodeType == SceneNode.ENodeType.Root);

            state.TableChoices.AppendLine($"{node.Key} = {state.NextStateID++},");
            state.TableChoiceFns.AppendLine($"_ChoiceFns[_Choices.{node.Key}] = function()");

            // Inject the user's 'Actions Taken' script if it's non-empty
            if (node.ScriptAction != null && node.ScriptAction.HasContent())
            {
                var script_text = node.ScriptAction.GetScriptText(state.Content);
                state.Compiler?.Compile(script_text, $"{node.Key}/ActionsTaken");
                state.TableChoiceFns.AppendLine(script_text);
            }

            // Emit tests for all child States. We go over all child States until we find a passing one, which then becomes the next State.
            Node last = node.Children.LastOrDefault();
            foreach (Node child in node.Children)
            {
                Node resolved_child = child;

                // Resolve the link, if this node is a link
                if (child.NodeType == SceneNode.ENodeType.Link)
                {
                    resolved_child = FindNodeByKey(state, child.LinkTarget);
                    if (resolved_child == null)
                        throw new SceneCompilerException($"Node '{node.Key}' contains a link to '{child.LinkTarget}' but no such node exists");
                }

                // Choices cannot contain Choices or Compass links
                if (resolved_child.NodeType != SceneNode.ENodeType.State)
                    throw new SceneCompilerException($"Node '{node.Key}' contains a link to '{child.LinkTarget}' but the target node is not a state. (Choices can only contain States or links to States.)");

                // Omit the _CanAppear call for the last child, since there must always be one passing State
                state.TableChoiceFns.AppendLine(child == last
                    ? $"_state = _States.{resolved_child.Key}"
                    : $"if _CanAppear(\"{resolved_child.Key}\") then _state = _States.{resolved_child.Key} return end");
            }

            // Close the function
            state.TableChoiceFns.AppendLine("end");
        }

        /// <summary>
        /// Contains state and partial progress for the code generation process.
        /// </summary>
        private sealed class CompilerState
        {

            public AssetScene Scene { get; set; }

            public StringBuilder Main { get; } = new StringBuilder();
            public StringBuilder TableStates { get; } = new StringBuilder();
            public StringBuilder TableStateFns { get; } = new StringBuilder();
            public StringBuilder TableChoices { get; } = new StringBuilder();
            public StringBuilder TableChoiceFns { get; } = new StringBuilder();
            public StringBuilder TableAppearFns { get; } = new StringBuilder();

            public int NextStateID { get; set; }
            public int NextAutoKey { get; set; }
            public Stack<Node> Backlog { get; } = new Stack<Node>();
            public HashSet<string> NodeNames { get; } = new HashSet<string>();

            public IScriptCompiler Compiler { get; set; }
            public IContentStore Content { get; set; }

        }

    }

}
