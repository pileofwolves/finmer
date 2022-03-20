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
            lsvNodes_SelectedIndexChanged(this, EventArgs.Empty);
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
                AddItemToTree(indent_prefix + node.GetEditorDescription(), node.GetEditorColor(), new TreeItemTag { m_Node = node, m_Parent = nodes });

                // If this node is itself a container for nodes, we need to recurse
                if (node is ScriptCommandContainer container)
                {
                    // Add the first set of nodes
                    RebuildNodeTreeFromCollection(container.Subgroup1, indentLevel + 1);
                    AddItemToTree(indent_prefix + container.GetEditorSubgroup1Suffix(), node.GetEditorColor(), null);

                    // The second subgroup may or may not be present
                    if (container.IsSubgroup2Enabled())
                    {
                        // Add the second set of nodes
                        RebuildNodeTreeFromCollection(container.Subgroup2, indentLevel + 1);
                        AddItemToTree(indent_prefix + container.GetEditorSubgroup2Suffix(), node.GetEditorColor(), null);
                    }
                }
            }

            // Placeholder node that allows for injecting new elements
            AddItemToTree(indent_prefix + "[ Add new... ]", ScriptNode.EColor.System, new TreeItemTag { m_Destination = nodes });
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
                case ScriptNode.EColor.FlowControl:         return Color.RoyalBlue;
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
            var selected_item = lsvNodes.SelectedItems[0];
            var tag = selected_item.Tag as TreeItemTag;
            if (tag != null)
            {
                if (tag.m_Node != null)
                {
                    // This is a node we can edit
                    using (FormScriptNode editor_form = ScriptNodeFormMapper.CreateEditorForm(tag.m_Node))
                    {
                        // If this node cannot be edited, there's nothing more to do
                        if (editor_form == null)
                            return;

                        // Display the node edit dialog
                        if (editor_form.ShowDialog(this) == DialogResult.OK)
                        {
                            RebuildNodeTree();
                            m_Host.MarkDirty();
                        }
                    }
                }
                else if (tag.m_Destination != null)
                {
                    // This is a node placeholder; open the palette so the user can add a new node
                    using (var palette_form = new FormVisualScriptCommandPalette())
                    {
                        // Present the palette. If the user selected a node to add, it will return DialogResult.OK.
                        DialogResult dr = palette_form.ShowDialog(this);
                        if (dr != DialogResult.OK)
                            return;

                        // Append the node to the tree
                        tag.m_Destination.Add(palette_form.NewNode);
                    }

                    // Rebuild the node tree, as it has changed
                    RebuildNodeTree();
                    m_Host.MarkDirty();
                }
            }
        }

        private TreeItemTag GetSelectedTag()
        {
            if (lsvNodes.SelectedItems.Count != 1)
                return null;

            var selected_item = lsvNodes.SelectedItems[0];
            var tag = selected_item.Tag as TreeItemTag;
            return tag;
        }

        private void lsvNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Disable tools by default
            tsbDeleteNode.Enabled = false;
            tsbMoveUp.Enabled = false;
            tsbMoveDown.Enabled = false;

            // Inspect the selection
            var tag = GetSelectedTag();
            if (tag != null)
            {
                // Is this a real node?
                if (tag.m_Node != null)
                {
                    // This node is concrete and can be deleted
                    int node_index = tag.m_Parent.IndexOf(tag.m_Node);
                    tsbDeleteNode.Enabled = tag.m_Node != null;
                    tsbMoveUp.Enabled = node_index > 0;
                    tsbMoveDown.Enabled = node_index < tag.m_Parent.Count - 1;
                }
            }
            else
            {
                // If the node is a dummy - not a script node, nor a new node placeholder - then the user can't select it
                lsvNodes.SelectedItems.Clear();
            }
        }

        private void tsbDeleteNode_Click(object sender, EventArgs e)
        {
            // Find context info for this tree item
            var tag = GetSelectedTag();
            if (tag?.m_Node != null)
            {
                // Erase the selected node from its parent tree
                tag.m_Parent.Remove(tag.m_Node);

                // Rebuild the visual tree as it has changed
                RebuildNodeTree();
                m_Host.MarkDirty();
            }
        }

        private void tsbMoveUp_Click(object sender, EventArgs e)
        {
            // Find context info for this tree item
            var tag = GetSelectedTag();
            if (tag?.m_Node != null)
            {
                // Erase the selected node from its parent tree
                int old_index = tag.m_Parent.IndexOf(tag.m_Node);
                tag.m_Parent.RemoveAt(old_index);
                tag.m_Parent.Insert(old_index - 1, tag.m_Node);

                // Rebuild the visual tree as it has changed
                RebuildNodeTree();
                m_Host.MarkDirty();
            }
        }

        private void tsbMoveDown_Click(object sender, EventArgs e)
        {
            // Find context info for this tree item
            var tag = GetSelectedTag();
            if (tag?.m_Node != null)
            {
                // Erase the selected node from its parent tree
                int old_index = tag.m_Parent.IndexOf(tag.m_Node);
                tag.m_Parent.RemoveAt(old_index);
                tag.m_Parent.Insert(old_index + 1, tag.m_Node);

                // Rebuild the visual tree as it has changed
                RebuildNodeTree();
                m_Host.MarkDirty();
            }
        }

        private void lsvNodes_KeyDown(object sender, KeyEventArgs e)
        {
            // If Delete is pressed, act as if the toolbar button was clicked
            if (e.KeyCode == Keys.Delete && tsbDeleteNode.Enabled)
                tsbDeleteNode_Click(sender, e);
        }

        /// <summary>
        /// Tree item tag that contains metadata for the visual editor UI.
        /// </summary>
        private class TreeItemTag
        {
            public ScriptNode m_Node;
            public List<ScriptNode> m_Parent;
            public List<ScriptNode> m_Destination;
        }

    }

}
