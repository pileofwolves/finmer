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
using System.Globalization;
using System.Linq;
using System.Text;
using Finmer.Core.Assets;
using Node = Finmer.Core.Assets.AssetScene.SceneNode;

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
        /// <param name="compiler">A Lua script compiler, used for verifying syntax.</param>
        /// <param name="scene">The scene asset to compile.</param>
        public static void Compile(IScriptCompiler compiler, AssetScene scene)
        {
            var state = new CompilerState
            {
                Compiler = compiler,
                Scene = scene
            };

            // Upvalues go first, to ensure references are actually compiled as upvalues and not global lookups.
            // Note: We prefix all identifiers with an underscore to prevent name clashes with user-defined code.
            state.Main.AppendLine("local _state = 0");

            // Custom script goes at the top, so it is available globally
            if (scene.ScriptCustom.HasContent())
            {
                state.Compiler?.Compile(scene.ScriptCustom.ScriptText, "CustomScript");
                state.Main.AppendLine(scene.ScriptCustom.ScriptText);
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
            if (scene.ScriptEnter.HasContent())
            {
                state.Compiler?.Compile(scene.ScriptEnter.ScriptText, "EnterScript");
                state.Main.AppendLine("function OnEnter()");
                state.Main.AppendLine(scene.ScriptEnter.ScriptText);
                state.Main.AppendLine("end");
            }

            // Copy OnLeave script
            if (scene.ScriptLeave.HasContent())
            {
                state.Compiler?.Compile(scene.ScriptLeave.ScriptText, "LeaveScript");
                state.Main.AppendLine("function OnLeave()");
                state.Main.AppendLine(scene.ScriptLeave.ScriptText);
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

        /// <summary>
        /// Escapes a string so that it can be safely inserted into Lua code.
        /// </summary>
        private static string EscapeLuaString(string unescaped)
        {
            return unescaped.Replace(@"\", @"\\").Replace(@"""", @"\""");
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
        /// Compiles a single <seealso cref="AssetScene.SceneNode" /> into Lua code.
        /// </summary>
        /// <param name="state">The object in which to store the generated code.</param>
        /// <param name="node">The node to generate code for.</param>
        private static void CompileNode(CompilerState state, Node node)
        {
            // Basic validation
            if (node.Key.Length > 32)
                throw new SceneCompilerException("A node has an invalid key (empty or too long). This likely indicates that the module file is corrupt.");

            if (state.NodeNames.Contains(node.Key))
                throw new SceneCompilerException($"Node key '{node.Key}' is used more than once.");

            if (node.IsLink && node.Children.Count > 0)
                throw new SceneCompilerException($"Node '{node.Key}' is a link, but also contains children. This likely indicates that the module file is corrupt.");

            // Enqueue all children
            foreach (Node child in node.Children)
            {
                // States and choices must alternate
                if (node.IsState == child.IsState)
                    throw new SceneCompilerException($"Node '{node.Key}' contains child '{child.Key}' of same node type as parent. Module file is likely corrupted. (IsState = {node.IsState})");

                // If the node key is unspecified, assign a default value instead.
                // This must be done before code is emitted for the parent node because the parent may refer to the keys of child nodes.
                if (String.IsNullOrWhiteSpace(child.Key))
                    child.Key = $"_AutoKey{state.NextAutoKey++}";

                // Queue this child
                state.Backlog.Push(child);
            }

            // Generate an AppearFn for all concrete nodes that have one specified
            if (!String.IsNullOrWhiteSpace(node.ScriptAppear) && !node.IsLink)
            {
                state.Compiler?.Compile(node.ScriptAppear, $"{node.Key}/AppearsWhen");
                state.TableAppearFns.Append(node.Key);
                state.TableAppearFns.AppendLine(" = function()");
                state.TableAppearFns.AppendLine(node.ScriptAppear);
                state.TableAppearFns.AppendLine("end,");
            }

            // Keep the node name in the hashset so we can check for uniqueness
            if (!node.IsLink)
                state.NodeNames.Add(node.Key);

            // Generate code
            if (node.IsState)
                CompileStateNode(state, node);
            else
                CompileChoiceNode(state, node);
        }

        /// <summary>
        /// Specialized node compiler that generates code for State nodes.
        /// </summary>
        /// <param name="state">The object in which to store the generated code.</param>
        /// <param name="node">The node to generate code for.</param>
        private static void CompileStateNode(CompilerState state, Node node)
        {
            Debug.Assert(node.IsState);

            // Links are resolved by their parent, no need to add new code
            if (node.IsLink)
                return;

            state.TableStates.AppendLine($"{node.Key} = {state.NextStateID++},");
            state.TableStateFns.AppendLine($"_StateFns[_States.{node.Key}] = function()");

            // Inject the user's 'Actions Taken' script if it's non-empty
            if (!String.IsNullOrWhiteSpace(node.ScriptAction))
            {
                state.Compiler?.Compile(node.ScriptAction, $"{node.Key}/ActionsTaken");
                state.TableStateFns.AppendLine(node.ScriptAction);
            }

            // Emit AddButton calls (wrapped in CanAppear tests) for all child Choices of this State
            foreach (Node child in node.Children)
            {
                Debug.Assert(!child.IsState);

                // Follow Links to their respective targets
                Node link_target = child;
                if (child.IsLink)
                {
                    // Dereference the link target node
                    string link_target_key = child.LinkTarget;
                    link_target = FindNodeByKey(state, link_target_key);

                    if (link_target == null)
                        throw new SceneCompilerException($"Node '{node.Key}' contains a link to '{link_target_key}' but no such node exists");

                    if (link_target.IsState)
                        throw new SceneCompilerException($"Node '{node.Key}' contains a link to '{link_target_key}' but the link target type does not match (You cannot link to a state from another state.)");
                }

                // Emit a call to the 'Appears When' test, then the AddButton call if it passes.
                string formatted_button_width = String.Format(CultureInfo.InvariantCulture, "{0:F3}", link_target.ButtonWidth);
                state.TableStateFns.Append($"if _CanAppear(\"{link_target.Key}\") then ");
                state.TableStateFns.Append($"AddButton(_Choices.{link_target.Key}, \"{EscapeLuaString(link_target.Title)}\", \"{EscapeLuaString(link_target.Tooltip)}\", {(link_target.Highlight ? "true" : "false")}, {formatted_button_width})");
                state.TableStateFns.AppendLine(" end");
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
            Debug.Assert(!node.IsState);

            // Links are resolved by their parent, no need to add new code
            if (node.IsLink)
                return;

            state.TableChoices.AppendLine($"{node.Key} = {state.NextStateID++},");
            state.TableChoiceFns.AppendLine($"_ChoiceFns[_Choices.{node.Key}] = function()");

            // Inject the user's 'Actions Taken' script if it's non-empty
            if (!String.IsNullOrWhiteSpace(node.ScriptAction))
            {
                state.Compiler?.Compile(node.ScriptAction, $"{node.Key}/ActionsTaken");
                state.TableChoiceFns.AppendLine(node.ScriptAction);
            }

            // Emit tests for all child States. We go over all child States until we find a passing one, which then becomes the next State.
            Node last = node.Children.LastOrDefault();
            foreach (Node child in node.Children)
            {
                Debug.Assert(child.IsState);

                // For link nodes, verify that the target link is a state and not a choice
                if (child.IsLink)
                {
                    Node link_target = FindNodeByKey(state, child.LinkTarget);

                    if (link_target == null)
                        throw new SceneCompilerException($"Node '{node.Key}' contains a link to '{child.LinkTarget}' but no such node exists");

                    if (!link_target.IsState)
                        throw new SceneCompilerException($"Node '{node.Key}' contains a link to '{child.LinkTarget}' but the link target type does not match (You cannot link to a choice from another choice.)");
                }

                string child_key = child.IsLink ? child.LinkTarget : child.Key;

                // Omit the _CanAppear call for the last child, since there must always be one passing State
                state.TableChoiceFns.AppendLine(child == last 
                    ? $"_state = _States.{child_key}" 
                    : $"if _CanAppear(\"{child_key}\") then _state = _States.{child_key} return end");
            }

            // Close the function
            state.TableChoiceFns.AppendLine("end");
        }

        /// <summary>
        /// Contains state and partial progress for the code generation process.
        /// </summary>
        private class CompilerState
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

        }

    }

}
