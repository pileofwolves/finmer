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

        private static readonly Brush k_RowBackgroundBrush1 = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
        private static readonly Brush k_RowBackgroundBrush2 = new SolidBrush(Color.FromArgb(255, 228, 236, 242));
        private static readonly Brush k_RowHoverBrush2 = new SolidBrush(Color.FromArgb(255, 215, 222, 229));
        private static readonly Brush k_RowFocusBrush2 = new SolidBrush(Color.FromArgb(255, 5, 124, 245));

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
            // Emit tree items for all nodes
            foreach (ScriptNode node in nodes)
            {
                // Add node
                AddItemToTree(node.GetEditorDescription(), node.GetEditorColor(), new TreeItemTag { m_Node = node, m_Parent = nodes }, indentLevel);

                // If this node is itself a container for nodes, we need to recurse
                if (node is ScriptCommandContainer container)
                {
                    // Add the first set of nodes
                    RebuildNodeTreeFromCollection(container.Subgroup1, indentLevel + 1);
                    AddItemToTree(container.GetEditorSubgroup1Suffix(), node.GetEditorColor(), null, indentLevel);

                    // The second subgroup may or may not be present
                    if (container.IsSubgroup2Enabled())
                    {
                        // Add the second set of nodes
                        RebuildNodeTreeFromCollection(container.Subgroup2, indentLevel + 1);
                        AddItemToTree(container.GetEditorSubgroup2Suffix(), node.GetEditorColor(), null, indentLevel);
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
                ForeColor = ConvertColor(color),
                Tag = tag,
                IndentCount = indent
            };
            lsvNodes.Items.Add(placeholder);
        }

        private static Color ConvertColor(ScriptNode.EColor color)
        {
            switch (color)
            {
                case ScriptNode.EColor.System:              return Color.FromArgb(255, 128, 128, 128);
                case ScriptNode.EColor.Code:                return Color.FromArgb(255, 64, 64, 64);
                case ScriptNode.EColor.Comment:             return Color.ForestGreen;
                case ScriptNode.EColor.FlowControl:         return Color.FromArgb(255, 40, 67, 204);
                case ScriptNode.EColor.Message:             return Color.FromArgb(255, 109, 14, 155);
                case ScriptNode.EColor.SceneControl:        return Color.FromArgb(255, 67, 145, 255);
                case ScriptNode.EColor.SaveData:            return Color.FromArgb(255, 5, 129, 131);
                case ScriptNode.EColor.Player:              return Color.FromArgb(255, 144, 3, 11);
                case ScriptNode.EColor.Journal:             return Color.FromArgb(255, 165, 139, 57);
                case ScriptNode.EColor.Variable:            return Color.FromArgb(255, 198, 23, 38);
                case ScriptNode.EColor.Sleep:               return Color.FromArgb(255, 230, 24, 81);
                case ScriptNode.EColor.Combat:              return Color.FromArgb(255, 166, 19, 220);
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

        private void lsvNodes_Resize(object sender, EventArgs e)
        {
            clhImplicitHeader.Width = lsvNodes.ClientSize.Width;
        }

        private void lsvNodes_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            // Pick a brush for the background
            Brush back_brush;
            if (e.Item.Selected)
                back_brush = k_RowFocusBrush2;
            else if (e.Item.Tag != null && lsvNodes.RectangleToScreen(e.Bounds).Contains(Cursor.Position))
                back_brush = k_RowHoverBrush2;
            else if (e.ItemIndex % 2 == 0)
                back_brush = k_RowBackgroundBrush1; // Alternating colors for regular rows
            else
                back_brush = k_RowBackgroundBrush2;

            // Draw the background
            e.Graphics.FillRectangle(back_brush, e.Bounds);

            // Offset text slightly to the left, for aesthetics
            var text_offset = e.Item.IndentCount * 28 + 2;
            var text_bounds = e.Bounds;
            text_bounds.X += text_offset;
            text_bounds.Width -= text_offset;

            // Make selected text a different color, for readability against the dark background
            var text_color = e.Item.Selected ? Color.White : e.Item.ForeColor;

            // Draw text
            TextRenderer.DrawText(e.Graphics, e.Item.Text, e.Item.Font, text_bounds, text_color, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
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
