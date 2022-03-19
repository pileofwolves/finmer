/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Finmer.Core.Assets;
using Finmer.Core.VisualScripting;

namespace Finmer.Editor
{

    /// <summary>
    /// Represents an editor interface for a raw (text-based) Lua script.
    /// </summary>
    public partial class VisualScriptEditor : UserControl, IScriptEditor
    {

        private readonly ScriptEditorHost m_Host;
        private readonly ScriptDataVisual m_ScriptData;

        public VisualScriptEditor(ScriptEditorHost host, ScriptDataVisual script)
        {
            InitializeComponent();

            m_Host = host;
            m_ScriptData = script;
        }

        public void Flush()
        {
            // TODO
        }

        private void VisualScriptEditor_Load(object sender, EventArgs e)
        {
            // The host determines which conversions are permissible
            tsbConvertInline.Visible = m_Host.AllowInlineScript;
            tsbConvertExternal.Visible = m_Host.AllowExternalScript;

            // Populate the node tree
            RebuildNodeTree();
        }

        private void tsbConvertInline_Click(object sender, EventArgs e)
        {
            m_Host.ConvertToInline();
        }

        private void tsbConvertExternal_Click(object sender, EventArgs e)
        {
            m_Host.ConvertToExternal();
        }

        private void RebuildNodeTree()
        {
            // Prevent UI from constantly redrawing a partially built tree
            lsvNodes.SuspendLayout();

            // Rebuild the full tree from scratch
            int old_selection = lsvNodes.SelectedIndices.Count == 1 ? lsvNodes.SelectedIndices[0] : -1;
            lsvNodes.Items.Clear();
            RebuildNodeTreeFromCollection(m_ScriptData.Nodes, 0);

            // Try to restore the user's previous selection
            if (old_selection != -1 && lsvNodes.Items.Count != 0)
            {
                // Cap the selection index at the tree length, so we don't try to select a node outside the tree bounds
                old_selection = Math.Min(old_selection, lsvNodes.Items.Count - 1);
                lsvNodes.Items[old_selection].Selected = true;
            }

            // Draw the tree
            lsvNodes.ResumeLayout();
        }

        private void RebuildNodeTreeFromCollection(List<ScriptNode> nodes, int indentLevel)
        {
            // Prepare a string to prefix to all nodes, to represent the nesting level
            string indent_prefix = GetIndentString(indentLevel);

            // Emit tree items for all nodes
            foreach (ScriptNode node in nodes)
            {
                // Add node
                AddItemToTree(node.GetEditorDescription(), node.GetEditorColor(), node);

                // If this node is itself a container for nodes, we need to recurse
                if (node is ScriptCommandContainer container)
                {
                    // Add the first set of nodes
                    RebuildNodeTreeFromCollection(container.Subgroup1, indentLevel + 1);
                    AddItemToTree(container.GetEditorSubgroup1Suffix(), node.GetEditorColor(), null);

                    // The second subgroup may or may not be present
                    if (container.IsSubgroup2Enabled())
                    {
                        // Add the second set of nodes
                        RebuildNodeTreeFromCollection(container.Subgroup2, indentLevel + 1);
                        AddItemToTree(container.GetEditorSubgroup2Suffix(), node.GetEditorColor(), null);
                    }
                }
            }

            // Placeholder node that allows for injecting new elements
            AddItemToTree(indent_prefix + "[ Add new... ]", ScriptNode.EColor.System, new NodeCreationPlaceholder { m_Destination = nodes });
        }

        private void AddItemToTree(string text, ScriptNode.EColor color, object tag)
        {
            var placeholder = new ListViewItem
            {
                Text = text,
                ForeColor = ConvertColor(color),
                Tag = tag
            };
            lsvNodes.Items.Add(placeholder);
        }

        private static string GetIndentString(int indentLevel)
        {
            return indentLevel == 0 ? String.Empty : new string(' ', indentLevel * 5);
        }

        private static Color ConvertColor(ScriptNode.EColor color)
        {
            switch (color)
            {
                case ScriptNode.EColor.System:              return Color.DimGray;
                case ScriptNode.EColor.Code:                return Color.Gray;
                case ScriptNode.EColor.Comment:             return Color.ForestGreen;
                case ScriptNode.EColor.FlowControl:         return Color.DodgerBlue;
                case ScriptNode.EColor.SceneControl:        return Color.DarkRed;
                case ScriptNode.EColor.Global:              return Color.DarkSlateBlue;
                case ScriptNode.EColor.SaveData:            return Color.DarkSlateGray;
                case ScriptNode.EColor.Player:              return Color.DarkGoldenrod;
                case ScriptNode.EColor.Journal:             return Color.SaddleBrown;
                default:                                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }
        }

        private void lsvNodes_DoubleClick(object sender, EventArgs e)
        {
            // Must have a selection to open
            if (lsvNodes.SelectedItems.Count != 1)
                return;

            // Check what actions we can perform with this tree item
            ListViewItem selected_item = lsvNodes.SelectedItems[0];
            switch (selected_item.Tag)
            {
                case ScriptNode node:
                {
                    // This is a node we can edit
                    using (FormScriptNode editor_form = ScriptNodeFormMapper.CreateEditorForm(node))
                    {
                        if (editor_form.ShowDialog(this) == DialogResult.OK)
                            // Update the node
                            selected_item.Text = node.GetEditorDescription();
                    }
                    break;
                }

                case NodeCreationPlaceholder placeholder:
                {
                    // This is a node placeholder; open the palette so the user can add a new node
                    using (var palette_form = new FormVisualScriptCommandPalette())
                    {
                        // Present the palette. If the user selected a node to add, it will return DialogResult.OK.
                        DialogResult dr = palette_form.ShowDialog(this);
                        if (dr != DialogResult.OK)
                            return;

                        // Append the node to the tree
                        placeholder.m_Destination.Add(palette_form.NewNode);
                    }

                    // Rebuild the node tree, as it has changed
                    RebuildNodeTree();

                    break;
                }
            }
        }

        /// <summary>
        /// Tree item tag that indicates the node represents a spot for new nodes to be added.
        /// </summary>
        private class NodeCreationPlaceholder
        {
            public List<ScriptNode> m_Destination;
        }

    }

}
