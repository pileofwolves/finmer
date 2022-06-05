/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Finmer.Core.Assets;
using Finmer.Core.VisualScripting;

namespace Finmer.Editor
{

    /// <summary>
    /// Represents an editor interface for a ScriptDataVisualAction object.
    /// </summary>
    public partial class VisualActionScriptEditor : UserControl, IScriptEditor
    {

        private readonly ScriptEditorHost m_Host;
        private readonly ScriptDataVisualAction m_ScriptData;

        public VisualActionScriptEditor(ScriptEditorHost host, ScriptDataVisualAction script)
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

        private void RebuildNodeTree()
        {
            // Prevent UI from constantly redrawing a partially built tree
            lsvNodes.SuspendLayout();

            // Rebuild the full tree from scratch
            int old_selection = lsvNodes.SelectedIndex;
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
            // Emit tree items for all nodes
            foreach (ScriptNode node in nodes)
            {
                // Add node
                AddItemToTree(node.GetEditorDescription(), node.GetEditorColor(), new TreeItemTag { m_Node = node, m_Parent = nodes }, indentLevel);

                // If this node is itself a container for nodes, we need to recurse
                if (node is ScriptCommandContainer container)
                {
                    foreach (var subgroup in container.GetSubgroups())
                    {
                        // Display the group prefix
                        if (!String.IsNullOrEmpty(subgroup.EditorPrefix))
                            AddItemToTree(subgroup.EditorPrefix, node.GetEditorColor(), null, indentLevel);

                        // Recurse through the node tree
                        RebuildNodeTreeFromCollection(subgroup.Nodes, indentLevel + 1);

                        // Display the group suffix
                        if (!String.IsNullOrEmpty(subgroup.EditorSuffix))
                            AddItemToTree(subgroup.EditorSuffix, node.GetEditorColor(), null, indentLevel);
                    }
                }
            }

            // Placeholder node that allows for injecting new elements
            AddItemToTree("[ Add... ]", ScriptNode.EColor.System, new TreeItemTag { m_Destination = nodes }, indentLevel);
        }

        private void AddItemToTree(string text, ScriptNode.EColor color, object tag, int indent)
        {
            var placeholder = new ListViewItem
            {
                Text = text,
                ForeColor = BandedListView.ConvertColor(color),
                Tag = tag,
                IndentCount = indent
            };
            lsvNodes.Items.Add(placeholder);
        }

        private TreeItemTag GetSelectedTag()
        {
            var selected_item = lsvNodes.SelectedItem;
            var tag = selected_item?.Tag as TreeItemTag;
            return tag;
        }

        private void lsvNodes_DoubleClick(object sender, EventArgs e)
        {
            // Must have a selection to open
            var selected_item = lsvNodes.SelectedItem;
            if (selected_item == null)
                return;

            // Check what actions we can perform with this tree item
            var tag = selected_item.Tag as TreeItemTag;
            if (tag != null)
            {
                if (tag.m_Node != null)
                {
                    // This is a node we can edit
                    var old_node = tag.m_Node;
                    using (FormScriptNode editor_form = ScriptNodeFormMapper.CreateEditorForm(old_node))
                    {
                        // If this node cannot be edited, there's nothing more to do
                        if (editor_form == null)
                            return;

                        // Display the node edit dialog
                        if (editor_form.ShowDialog(this) == DialogResult.OK)
                        {
                            // If the form replaced the node with a different instance, replace the old one in the parent tree
                            if (!Object.ReferenceEquals(old_node, editor_form.Node))
                            {
                                var index = tag.m_Parent.IndexOf(old_node);
                                if (index != -1)
                                {
                                    tag.m_Parent.RemoveAt(index);
                                    tag.m_Parent.Insert(index, editor_form.Node);
                                }
                            }

                            // Refresh the tree visualization
                            RebuildNodeTree();

                            // Allow saving
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
                lsvNodes.SelectedIndex = -1;
            }
        }

        private void lsvNodes_KeyDown(object sender, KeyEventArgs e)
        {
            // If Delete is pressed, act as if the toolbar button was clicked
            if (e.KeyCode == Keys.Delete && tsbDeleteNode.Enabled)
                tsbDeleteNode_Click(sender, e);
        }

        private void tsbConvertInline_Click(object sender, EventArgs e)
        {
            m_Host.ConvertToInline();
        }

        private void tsbConvertExternal_Click(object sender, EventArgs e)
        {
            m_Host.ConvertToExternal();
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
