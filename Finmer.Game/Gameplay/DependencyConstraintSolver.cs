/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2024 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Finmer.Gameplay
{

    /// <summary>
    /// Implements a solver for 'before/after'-style dependency constraints.
    /// </summary>
    public class DependencyConstraintSolver<T>
    {

        /// <summary>
        /// State of a graph node.
        /// </summary>
        private enum ENodeState
        {
            Unvisited,
            Visiting,
            Visited
        }

        /// <summary>
        /// Represents an element in the dependency graph.
        /// </summary>
        private sealed class Node
        {

            /// <summary>
            /// Solve state of this node.
            /// </summary>
            public ENodeState State { get; set; } = ENodeState.Unvisited;

            /// <summary>
            /// User-specified value represented by this node.
            /// </summary>
            public T Value { get; }

            /// <summary>
            /// User-specified debug description.
            /// </summary>
            public string DebugInfo { get; }

            /// <summary>
            /// Collection of nodes that must be ordered after this one.
            /// </summary>
            public List<Node> Dependencies { get; } = new List<Node>();

            public Node(T value, string description)
            {
                Value = value;
                DebugInfo = description;
            }

        }

        private readonly Dictionary<T, Node> m_Graph = new Dictionary<T, Node>();
        private List<T> m_SolvedOutput;
        private Stack<Node> m_SolveStack;

        /// <summary>
        /// Register an element in the graph.
        /// </summary>
        /// <param name="value">Element to add.</param>
        /// <param name="description">Debug description used for exceptions.</param>
        public void AddNode(T value, string description)
        {
            // If node already exists, no need to do anything
            if (m_Graph.ContainsKey(value))
                return;

            // Register this node
            m_Graph.Add(value, new Node(value, description));
        }

        /// <summary>
        /// Adds a dependency (or 'edge') between two elements in the graph.
        /// </summary>
        /// <param name="before">The source element.</param>
        /// <param name="after">The target element.</param>
        public void AddDependency(T before, T after)
        {
            // Make the first node sort before the second node
            if (m_Graph.TryGetValue(before, out Node before_node) && m_Graph.TryGetValue(after, out Node after_node))
            {
                after_node.Dependencies.Add(before_node);
                return;
            }

            // Either of the nodes was not registered with AddNode
            throw new KeyNotFoundException();
        }

        /// <summary>
        /// Solve the dependency graph, returning an iterator that traverses the sorted element list.
        /// </summary>
        /// <exception cref="UnsolvableConstraintException">Throws if the graph contains cyclic links.</exception>
        public IEnumerable<T> Solve()
        {
            // Preallocate memory for bookkeeping structures
            m_SolvedOutput = new List<T>(m_Graph.Count);
            m_SolveStack = new Stack<Node>(m_Graph.Count);

            // Traverse each node in the graph
            foreach (var node in m_Graph)
                VisitNode(node.Value);

            // Return the output
            foreach (var sorted_element in m_SolvedOutput)
                yield return sorted_element;
        }

        private void VisitNode(Node node)
        {
            // Track this node on the resolve path, so we can trace back which route we took to get here
            m_SolveStack.Push(node);

            // Process this node
            switch (node.State)
            {
                case ENodeState.Visited:
                    // Node already visited earlier in a different stack
                    break;

                case ENodeState.Visiting:
                    // Node is being visited in this stack; therefore, there must be a cycle in the graph
                    throw BuildCyclicDependencyException();

                case ENodeState.Unvisited:
                    // Mark node as currently being traversed, so we can detect cycles
                    node.State = ENodeState.Visiting;

                    // Recursively traverse child nodes
                    foreach (var dependency in node.Dependencies)
                        VisitNode(dependency);

                    // Mark node as fully complete
                    node.State = ENodeState.Visited;

                    // Add node to the output graph
                    m_SolvedOutput.Add(node.Value);
                    break;
            }

            // Clean up
            m_SolveStack.Pop();
        }

        private Exception BuildCyclicDependencyException()
        {
            // Trace back the path we took through the graph
            StringBuilder message = new StringBuilder();
            message.Append("Cyclic dependency detected. Items in the cycle: ");
            message.Append(String.Join(", ", m_SolveStack.Select(element => element.DebugInfo)));
            message.Append(" (<-- Cycle here)");

            // Format that path into an exception
            return new UnsolvableConstraintException(message.ToString());
        }

    }

}
