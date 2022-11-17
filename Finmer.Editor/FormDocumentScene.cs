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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Finmer.Core.Assets;
using Finmer.Core.Serialization;
using Finmer.Editor.Properties;

namespace Finmer.Editor
{

    public partial class FormDocumentScene : AssetWindow
    {

        private const float k_Button_Default_Width = 1.0f;
        private const float k_Button_Min_Width = 0.25f;
        private const float k_Button_Max_Width = 3.0f;

        private AssetScene m_Scene;
        private AssetScene m_PatchTargetScene;
        private AssetScene.SceneNode m_SelectedNode;

        private TreeNode m_SelectedTree, m_SelectedTreeParent;

        private int m_SelectedTreeIndex;
        private int m_SelectedTreeMaxIndex;

        private WeakReference<EditorWindow> m_ScriptEditorEnter;
        private WeakReference<EditorWindow> m_ScriptEditorLeave;
        private WeakReference<EditorWindow> m_ScriptEditorCustom;

        private readonly TabPage m_TabPageNodeRoot;
        private readonly TabPage m_TabPageNodeState;
        private readonly TabPage m_TabPageNodeChoice;
        private readonly TabPage m_TabPageNodeLink;
        private readonly TabPage m_TabPageNodeCompass;

        private bool m_SkipDirtyUpdates = true;
        private bool m_SkipTreeSelect;

        public FormDocumentScene()
        {
            InitializeComponent();

            // Cache node tabs, so we can hide tabs we don't need
            m_TabPageNodeRoot = tbpNodeRoot;
            m_TabPageNodeState = tbpNodeState;
            m_TabPageNodeChoice = tbpNodeChoice;
            m_TabPageNodeLink = tbpNodeLink;
            m_TabPageNodeCompass = tbpNodeCompass;
        }

        private void FormDocumentScene_Load(object sender, EventArgs e)
        {
            // Duplicate the asset we're going to be editing, so that we can safely modify the copy in-place while still allowing changes to be discarded
            m_Scene = AssetSerializer.DuplicateAsset((AssetScene)Asset);

            // Hide node edit panel by default
            tbcNode.TabPages.Clear();

            // Set up toolbar
            UpdateScriptButtonIcons();

            // Build the visual node tree
            trvNodes.SuspendLayout();
            AddNodeToTreeView(trvNodes.Nodes, m_Scene.Root);
            trvNodes.ExpandAll();
            trvNodes.ResumeLayout();

            // Set up patch settings panel
            chkRootInject.Checked = m_Scene.IsPatch;
            cmbInjectTargetMode.SelectedIndex = (int)m_Scene.InjectMode;
            assetInjectTargetScene.SelectedGuid = m_Scene.InjectTargetScene;
            cmbInjectTargetNode.Text = m_Scene.InjectTargetNode;
            UpdateInjectionNodeList();

            // Mark the asset as dirty when the user changes node scripts
            scriptAction.Dirty += (o, arg) => Dirty = true;
            scriptAppear.Dirty += (o, arg) => Dirty = true;
        }

        public override void Flush()
        {
            // Ensure any previous script editor changes are committed
            scriptAction.Flush();
            scriptAppear.Flush();

            // Ensure scene scripts are committed, since the serialization of the scene asset depends on it
            // These are weak references so that this scene editor doesn't keep the script editors alive after the user closes them
            if (m_ScriptEditorEnter != null && m_ScriptEditorEnter.TryGetTarget(out var enter_script_window) && !enter_script_window.IsDisposed)
                enter_script_window.Flush();
            if (m_ScriptEditorLeave != null && m_ScriptEditorLeave.TryGetTarget(out var leave_script_window) && !leave_script_window.IsDisposed)
                leave_script_window.Flush();
            if (m_ScriptEditorCustom != null && m_ScriptEditorCustom.TryGetTarget(out var custom_script_window) && !custom_script_window.IsDisposed)
                custom_script_window.Flush();

            // Force the new asset to take on the old asset's name - this can only be changed outside the editor window, so if the user
            // changed this after the editor window made a copy of the source asset, the name would be overwritten with the old one.
            m_Scene.Name = Asset.Name;

            // Ensure script names are up-to-date with the asset name
            if (m_Scene.ScriptCustom != null)
                m_Scene.ScriptCustom.Name = m_Scene.Name + "_Custom";
            if (m_Scene.ScriptEnter != null)
                m_Scene.ScriptEnter.Name = m_Scene.Name + "_Enter";
            if (m_Scene.ScriptLeave != null)
                m_Scene.ScriptLeave.Name = m_Scene.Name + "_Leave";

            UpdateScriptButtonIcons();

            // Commit the changes by updating the asset represented by this editor window with a new snapshot
            Asset = AssetSerializer.DuplicateAsset(m_Scene);

            base.Flush();
        }

        private void trvNodes_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (m_SkipTreeSelect) return;

            // Ensure any previous script editor changes are committed
            scriptAction.Flush();
            scriptAppear.Flush();

            // Determine which visual tree node was selected
            m_SelectedTree = trvNodes.SelectedNode;
            m_SelectedTreeParent = m_SelectedTree?.Parent;
            m_SelectedTreeIndex = m_SelectedTreeParent?.Nodes.IndexOf(m_SelectedTree) ?? -1;
            m_SelectedTreeMaxIndex = m_SelectedTreeParent?.Nodes.Count ?? -1;
            UpdateMoveButtons();

            // Determine which logical node was selected
            m_SelectedNode = trvNodes.SelectedNode?.Tag as AssetScene.SceneNode;
            Debug.Assert(m_SelectedNode != null);

            // We're about to programmatically update UI elements, so prevent them from reacting to changes
            m_SkipDirtyUpdates = true;
            splitNodeSettings.SuspendLayout();
            splitNodeSettings.Visible = true;

            // Remove all tabs so that we can add only the specific tab we need for this node
            tbcNode.TabPages.Clear();

            // Configure toolbar for this node
            bool is_root = m_SelectedNode == m_Scene.Root;
            tbcScripts.Visible = !is_root && m_SelectedNode.NodeType != AssetScene.ENodeType.Link;
            tsbAddNode.Enabled = trvNodes.SelectedNode != null && m_SelectedNode.IsFullNode();
            tsbAddLink.Enabled = tsbAddNode.Enabled;
            tsbRemoveNode.Enabled = !is_root;

            // Display specific settings for the node
            switch (m_SelectedNode.NodeType)
            {
                case AssetScene.ENodeType.Root:
                    // Root node has a separate collection of settings
                    Debug.Assert(is_root);
                    tbcNode.TabPages.Add(m_TabPageNodeRoot);
                    break;

                case AssetScene.ENodeType.State:
                    // Populate state-specific settings
                    tbcNode.TabPages.Add(m_TabPageNodeState);
                    txtNodeStateKey.Text = m_SelectedNode.Key;

                    break;

                case AssetScene.ENodeType.Choice:
                    // Populate choice-specific settings
                    tbcNode.TabPages.Add(m_TabPageNodeChoice);
                    txtNodeChoiceKey.Text = m_SelectedNode.Key;
                    txtNodeChoiceTitle.Text = m_SelectedNode.Title;
                    txtNodeChoiceTooltip.Text = m_SelectedNode.Tooltip;
                    chkChoiceHighlight.Checked = m_SelectedNode.Highlight;
                    chkChoiceCustomWidth.Checked = Math.Abs(m_SelectedNode.ButtonWidth - 1.0f) > 0.01f;
                    nudChoiceCustomWidth.Value = chkChoiceCustomWidth.Checked
                        ? (decimal)Math.Max(Math.Min(m_SelectedNode.ButtonWidth, k_Button_Max_Width), k_Button_Min_Width)
                        : (decimal)k_Button_Default_Width;

                    break;

                case AssetScene.ENodeType.Link:
                    // Populate link-specific settings
                    tbcNode.TabPages.Add(m_TabPageNodeLink);
                    UpdateLinkTargetList();
                    cmbLinkTarget.Text = m_SelectedNode.LinkTarget;

                    break;

                case AssetScene.ENodeType.Compass:
                    // Populate compass-specific settings
                    tbcNode.TabPages.Add(m_TabPageNodeCompass);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Set up the script editor tabs
            var wrapper = ScriptDataWrapper.EnsureWrapped(m_SelectedNode.ScriptAction);
            m_SelectedNode.ScriptAction = wrapper;
            scriptAction.SetScript(wrapper);
            wrapper = ScriptDataWrapper.EnsureWrapped(m_SelectedNode.ScriptAppear);
            m_SelectedNode.ScriptAppear = wrapper;
            scriptAppear.SetScript(wrapper);

            // Allow update handlers again
            m_SkipDirtyUpdates = false;
            splitNodeSettings.ResumeLayout();
        }

        private void UpdateNodeImage(TreeNode treeNode, AssetScene.SceneNode sceneNode)
        {
            bool has_appear_script = sceneNode.ScriptAppear != null && sceneNode.ScriptAppear.HasContent();

            if (treeNode.Parent == null)
                treeNode.ImageKey = "node-root";
            else if (sceneNode.NodeType == AssetScene.ENodeType.Link)
                treeNode.ImageKey = "node-link";
            else if (sceneNode.NodeType == AssetScene.ENodeType.State)
                treeNode.ImageKey = has_appear_script ? "node-state-alt" : "node-state";
            else
                treeNode.ImageKey = has_appear_script ? "node-option-alt" : "node-option";

            treeNode.SelectedImageKey = treeNode.ImageKey;
        }

        private void UpdateMoveButtons()
        {
            tsbMoveUp.Enabled = m_SelectedTreeIndex > 0;
            tsbMoveDown.Enabled = m_SelectedTreeIndex < m_SelectedTreeMaxIndex - 1;
        }

        private void UpdateLinkTargetList()
        {
            void AddToLinkTargetList(AssetScene.SceneNode node)
            {
                // To be a valid link target, the node must have a key, and either be of the same type or be a compass link where compass links are accepted.
                bool can_accept_compass = m_SelectedNode.NodeType == AssetScene.ENodeType.State;
                bool is_compass = node.NodeType == AssetScene.ENodeType.Compass;
                bool is_same_type = node.NodeType == GetInverseNodeType(m_SelectedNode.Parent.NodeType);
                if (((is_compass && can_accept_compass) || is_same_type) && !String.IsNullOrWhiteSpace(node.Key))
                    cmbLinkTarget.Items.Add(node.Key);
            }

            cmbLinkTarget.Items.Clear();
            m_Scene.Root.Children.Traverse(node => node.Children).ForEach(AddToLinkTargetList);
            m_PatchTargetScene?.Root.Children.Traverse(node => node.Children).ForEach(AddToLinkTargetList);
        }

        private void UpdateScriptButtonIcons()
        {
            tsbScriptCustom.Image   = m_Scene.ScriptCustom != null && m_Scene.ScriptCustom.HasContent() ? Resources.pencil : Resources.plus;
            tsbScriptEnter.Image    = m_Scene.ScriptEnter != null && m_Scene.ScriptEnter.HasContent() ? Resources.pencil : Resources.plus;
            tsbScriptLeave.Image    = m_Scene.ScriptLeave != null && m_Scene.ScriptLeave.HasContent() ? Resources.pencil : Resources.plus;
        }

        private void UpdateInjectionNodeList()
        {
            cmbInjectTargetNode.Items.Clear();

            AssetScene target_scene = assetInjectTargetScene.SelectedAsset as AssetScene;
            if (target_scene == null)
                return;

            // populate the node combobox with the nodes of the selected target scene
            m_PatchTargetScene = target_scene;
            target_scene.Root.Children
                .Traverse(node => node.Children)
                .Where(node => node.NodeType == AssetScene.ENodeType.State && !String.IsNullOrEmpty(node.Key))
                .Select(node => node.Key)
                .ForEach(name => cmbInjectTargetNode.Items.Add(name));

            // update the scene data
            if (m_SkipDirtyUpdates) return;
            m_Scene.InjectTargetScene = target_scene.ID;
            Dirty = true;
        }

        private void AddNodeToTreeView(TreeNodeCollection collection, AssetScene.SceneNode sceneNode, bool select = false)
        {
            // create and add a node
            var tree_node = new TreeNode
            {
                Tag = sceneNode
            };
            collection.Add(tree_node);
            UpdateNodeText(tree_node, sceneNode);
            UpdateNodeImage(tree_node, sceneNode);

            // add the SceneNode's children to the tree also
            sceneNode.Children.ForEach(child => AddNodeToTreeView(tree_node.Nodes, child));

            if (select)
                trvNodes.SelectedNode = tree_node;
        }

        private void UpdateNodeText(TreeNode treeNode, AssetScene.SceneNode sceneNode)
        {
            // The root node has a fixed name
            if (treeNode.Parent == null)
            {
                treeNode.ForeColor = Color.DarkBlue;
                treeNode.Text = "Root";
                return;
            }

            // Otherwise, generate the node name based on its configuration
            switch (sceneNode.NodeType)
            {
                case AssetScene.ENodeType.State:
                case AssetScene.ENodeType.Choice:
                {
                    // For all other nodes, concatenate the node key and any prefixes
                    string[] elements =
                    {
                        String.IsNullOrWhiteSpace(sceneNode.Key) ? String.Empty : $"[{sceneNode.Key}]",
                        sceneNode.NodeType == AssetScene.ENodeType.Choice ? sceneNode.Title : String.Empty,
                        (sceneNode.ScriptAction != null && sceneNode.ScriptAction.HasContent()) ? "(!)" : String.Empty,
                        (sceneNode.ScriptAppear != null && sceneNode.ScriptAppear.HasContent()) ? "(?)" : String.Empty,
                    };
                    treeNode.ForeColor = sceneNode.NodeType == AssetScene.ENodeType.State ? Color.DarkRed : Color.DarkBlue;
                    treeNode.Text = String.Join(" ", elements.Where(str => !String.IsNullOrEmpty(str)));
                    break;
                }

                case AssetScene.ENodeType.Link:
                {
                    // Link target
                    treeNode.ForeColor = Color.DarkSlateBlue;
                    treeNode.Text = "--> " + sceneNode.LinkTarget;
                    break;
                }

                case AssetScene.ENodeType.Compass:
                {
                    // Compass link
                    bool has_script = sceneNode.ScriptAction != null && sceneNode.ScriptAction.HasContent();
                    string[] elements =
                    {
                        "Compass:",
                        sceneNode.CompassLinkDirection.ToString(),
                        has_script ? "(!)" : String.Empty,
                        (sceneNode.ScriptAppear != null && sceneNode.ScriptAppear.HasContent()) ? "(?)" : String.Empty,
                    };
                    treeNode.ForeColor = Color.DarkSlateBlue;
                    treeNode.Text = String.Join(" ", elements.Where(str => !String.IsNullOrEmpty(str)));
                    break;
                }
            }
        }

        private void tsbAddNode_Click(object sender, EventArgs e)
        {
            // Safeguard: prevent adding children to links
            if (!m_SelectedNode.IsFullNode())
                return;

            // Generate a new node
            var node = new AssetScene.SceneNode
            {
                NodeType = m_SelectedNode.Parent == null ? GetAcceptableRootType() : GetInverseNodeType(m_SelectedNode.NodeType),
                Key = String.Empty,
                Parent = m_SelectedNode
            };

            // Append it to the internal representation and the UI tree
            Debug.Assert(m_SelectedNode != null);
            m_SelectedNode.Children.Add(node);
            AddNodeToTreeView(m_SelectedTree.Nodes, node, true);

            Dirty = true;
        }

        private void tsbAddLink_Click(object sender, EventArgs e)
        {
            // Safeguard: prevent adding children to links
            if (!m_SelectedNode.IsFullNode())
                return;

            // Generate a new node
            var node = new AssetScene.SceneNode
            {
                NodeType = AssetScene.ENodeType.Link,
                Key = String.Empty
            };

            // Append it to the internal representation and the UI tree
            Debug.Assert(m_SelectedNode != null);
            m_SelectedNode.Children.Add(node);
            AddNodeToTreeView(m_SelectedTree.Nodes, node, true);

            Dirty = true;
        }

        private void tsbRemoveNode_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure you want to remove '{m_SelectedNode.Key}' and ALL its children? This operation cannot be undone.", "Finmer Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;

            // remove from scene graph, and tree view
            m_SelectedNode.Parent.Children.Remove(m_SelectedNode);
            m_SelectedTree.Remove();

            Dirty = true;
        }

        private void tsbExpand_Click(object sender, EventArgs e)
        {
            trvNodes.ExpandAll();
        }

        private void tsbCollapse_Click(object sender, EventArgs e)
        {
            trvNodes.CollapseAll();
        }

        private void txtNodeKey_TextChanged(object sender, EventArgs e)
        {
            if (m_SkipDirtyUpdates) return;
            Dirty = true;

            var text_box = (TextBox)sender;
            m_SelectedNode.Key = text_box.Text;
            UpdateLinkTargetList();
            UpdateNodeText(m_SelectedTree, m_SelectedNode);
        }

        private void txtNodeTitle_TextChanged(object sender, EventArgs e)
        {
            if (m_SkipDirtyUpdates) return;
            Dirty = true;

            m_SelectedNode.Title = txtNodeChoiceTitle.Text;
            UpdateNodeText(m_SelectedTree, m_SelectedNode);
        }

        private void txtNodeTooltip_TextChanged(object sender, EventArgs e)
        {
            if (m_SkipDirtyUpdates) return;
            Dirty = true;

            m_SelectedNode.Tooltip = txtNodeChoiceTooltip.Text;
        }

        private void cmbLinkTarget_TextChanged(object sender, EventArgs e)
        {
            if (m_SkipDirtyUpdates) return;
            Dirty = true;

            m_SelectedNode.LinkTarget = cmbLinkTarget.Text;
            UpdateNodeText(m_SelectedTree, m_SelectedNode);
        }

        private void tsbScriptCustom_Click(object sender, EventArgs e)
        {
            m_Scene.ScriptCustom = ScriptDataWrapper.EnsureWrapped(m_Scene.ScriptCustom);
            m_ScriptEditorCustom = new WeakReference<EditorWindow>(Program.MainForm.OpenAssetEditor(m_Scene.ScriptCustom));
        }

        private void tsbScriptEnter_Click(object sender, EventArgs e)
        {
            m_Scene.ScriptEnter = ScriptDataWrapper.EnsureWrapped(m_Scene.ScriptEnter);
            m_ScriptEditorEnter = new WeakReference<EditorWindow>(Program.MainForm.OpenAssetEditor(m_Scene.ScriptEnter));
        }

        private void tsbScriptLeave_Click(object sender, EventArgs e)
        {
            m_Scene.ScriptLeave = ScriptDataWrapper.EnsureWrapped(m_Scene.ScriptLeave);
            m_ScriptEditorLeave = new WeakReference<EditorWindow>(Program.MainForm.OpenAssetEditor(m_Scene.ScriptLeave));
        }

        private void tsbMoveUp_Click(object sender, EventArgs e)
        {
            m_SkipTreeSelect = true;

            // move scene noed
            m_SelectedNode.Parent.Children[m_SelectedTreeIndex] = m_SelectedNode.Parent.Children[m_SelectedTreeIndex - 1];
            m_SelectedNode.Parent.Children[m_SelectedTreeIndex - 1] = m_SelectedNode;

            // move tree node
            m_SelectedTreeParent.Nodes.RemoveAt(m_SelectedTreeIndex);
            m_SelectedTreeParent.Nodes.Insert(m_SelectedTreeIndex - 1, m_SelectedTree);
            m_SelectedTree.TreeView.SelectedNode = m_SelectedTree;

            m_SelectedTreeIndex--;
            UpdateMoveButtons();

            m_SkipTreeSelect = false;
            Dirty = true;
        }

        private void chkCustomWidth_CheckedChanged(object sender, EventArgs e)
        {
            // Width widget appears only if custom width is enabled
            nudChoiceCustomWidth.Visible = chkChoiceCustomWidth.Checked;

            // Mark as dirty
            if (m_SkipDirtyUpdates) return;
            Dirty = true;

            // If custom width is disabled, reset the width widget back to default value
            if (!chkChoiceCustomWidth.Checked)
                nudChoiceCustomWidth.Value = (decimal)k_Button_Default_Width;

            // Either way, reset the stored value as well
            m_SelectedNode.ButtonWidth = k_Button_Default_Width;
        }

        private void chkChoiceHighlight_CheckedChanged(object sender, EventArgs e)
        {
            if (m_SkipDirtyUpdates) return;

            Dirty = true;
            m_SelectedNode.Highlight = chkChoiceHighlight.Checked;
        }

        private void nudCustomWidth_ValueChanged(object sender, EventArgs e)
        {
            if (m_SkipDirtyUpdates) return;

            Dirty = true;
            m_SelectedNode.ButtonWidth = (float)nudChoiceCustomWidth.Value;
        }

        private void trvNodes_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // can only drag/drop treenodes
            if (!(e.Item is TreeNode treenode)) return;
            // cannot change the scene root
            if (treenode.Parent == null) return;
            // must be left or right mouse
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;

            // if doing a move, collapse the node, so that it becomes impossible to drag the source onto one of its children (which makes no sense)
            if (e.Button == MouseButtons.Left)
                treenode.Collapse();

            DoDragDrop(e.Item, e.Button == MouseButtons.Left ? DragDropEffects.Move :  DragDropEffects.Link);
        }

        private void trvNodes_DragDrop(object sender, DragEventArgs e)
        {
            // Get the node that is being dragged, and the node it is being dropped on
            var source = (TreeNode)e.Data.GetData(typeof(TreeNode));
            var target = trvNodes.GetNodeAt(trvNodes.PointToClient(new Point(e.X, e.Y)));

            // Get the SceneNode of those node representations
            var source_sn = (AssetScene.SceneNode)source.Tag;
            var target_sn = (AssetScene.SceneNode)target.Tag;

            // Get the SceneNode of the source node's parent, so we can remove it from there
            var source_parent = source.Parent;
            var source_parent_sn = (AssetScene.SceneNode)source_parent.Tag;

            // Perform the drag/drop operation
            switch (e.Effect)
            {
                case DragDropEffects.Move:
                    // Unlink the node from the parent
                    source_parent.Nodes.Remove(source);
                    source_parent_sn.Children.Remove(source_sn);

                    // Add to target
                    target.Nodes.Add(source);
                    target_sn.Children.Add(source_sn);
                    source_sn.Parent = target_sn;

                    // Select it to enable immediate editing
                    trvNodes.SelectedNode = source;

                    break;

                case DragDropEffects.Link:
                    // Create a new link node that points to the target
                    var link = new AssetScene.SceneNode
                    {
                        NodeType = AssetScene.ENodeType.Link,
                        Key = String.Empty,
                        LinkTarget = source_sn.Key
                    };
                    target_sn.Children.Add(link);
                    AddNodeToTreeView(target.Nodes, link, true);

                    break;

                default:
                    throw new InvalidOperationException("Unsupported DragEventArgs.Effect in trvNodes_DragDrop");
            }

            // Mark scene as dirty
            Dirty = true;
        }

        private TreeNode GetTreeNodeByKey(string key)
        {
            // Prepare stack with root TreeNodes in it
            Stack<TreeNode> stack = new Stack<TreeNode>();
            foreach (TreeNode root_node in trvNodes.Nodes)
                stack.Push(root_node);

            // Flood-fill down the tree until the requested node is found
            while (stack.Count > 0)
            {
                TreeNode node = stack.Pop();
                var node_sn = (AssetScene.SceneNode)node.Tag;

                // Links do not have keys
                if (!node_sn.IsFullNode())
                    continue;

                // Is this the node we're looking for?
                if (node_sn.Key.Equals(key))
                    return node;

                // Recursively examine the node's children
                foreach (TreeNode child in node.Nodes)
                    stack.Push(child);
            }

            return null;
        }

        private void trvNodes_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Find the type of node that is double-clicked on, and see if it's a link node
            var link_node = e.Node;
            var link_node_sn = (AssetScene.SceneNode)link_node.Tag;
            if (link_node_sn.NodeType == AssetScene.ENodeType.Link)
            {
                // If node is a link node, jump to the node it's linked to.
                var search_key = link_node_sn.LinkTarget;
                var matching_node = GetTreeNodeByKey(search_key);
                if (matching_node != null)
                    trvNodes.SelectedNode = matching_node;
            }
        }

        private void trvNodes_DragOver(object sender, DragEventArgs e)
        {
            var source = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            Point mouse = trvNodes.PointToClient(new Point(e.X, e.Y));
            TreeNode target = trvNodes.GetNodeAt(mouse);

            // Check if the user is hovering over any particular node at all
            if (target == null || source == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            // Retrieve the SceneNodes represented by these tree items
            var source_sn = (AssetScene.SceneNode)source.Tag;
            var target_sn = (AssetScene.SceneNode)target.Tag;

            // Nodes cannot be re-parented to links, or break alternation between States/Choices
            if (target_sn.NodeType != GetAcceptableParentType(source_sn))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            // Notify the system which actions are allowed now
            e.Effect = e.AllowedEffect;
        }

        private void chkRootInject_CheckedChanged(object sender, EventArgs e)
        {
            bool inject = chkRootInject.Checked;
            pnlInjectionSettings.Visible = inject;
            tsbScriptCustom.Enabled = !inject;
            tsbScriptEnter.Enabled = !inject;
            tsbScriptLeave.Enabled = !inject;

            if (m_SkipDirtyUpdates) return;

            m_Scene.IsPatch = inject;
            Dirty = true;
        }

        private void assetInjectTargetScene_SelectedAssetChanged(object sender, EventArgs e)
        {
            if (m_SkipDirtyUpdates)
                return;

            // Update the list of possible injection target nodes
            if (assetInjectTargetScene.SelectedGuid == Guid.Empty)
                // If no scene is selected, we have no node list either
                cmbInjectTargetNode.Items.Clear();
            else
                // Otherwise, refresh the node list
                UpdateInjectionNodeList();
        }

        private void cmbInjectTargetNode_TextChanged(object sender, EventArgs e)
        {
            if (m_SkipDirtyUpdates) return;

            m_Scene.InjectTargetNode = cmbInjectTargetNode.Text;
            Dirty = true;
        }

        private void cmbInjectTargetMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_SkipDirtyUpdates) return;

            var mode_old = (int)m_Scene.InjectMode;
            int mode_new = cmbInjectTargetMode.SelectedIndex;
            if (mode_old == mode_new) return;

            if (mode_old < 2 && mode_new >= 2 || mode_old >= 2 && mode_new < 2)
            {
                // require deleting the scene contents if the root node has to change type
                if (MessageBox.Show("Changing injection mode between inside and outside requires the contents of this Scene asset to be deleted. Are you sure you wish to continue?", "Finmer Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    cmbInjectTargetMode.SelectedIndex = mode_old;
                    return;
                }

                // destroy all scene contents
                m_Scene.Root.Children.Clear();
                trvNodes.Nodes.Clear();
                AddNodeToTreeView(trvNodes.Nodes, m_Scene.Root);
            }

            m_Scene.InjectMode = (AssetScene.ESceneInjectMode)cmbInjectTargetMode.SelectedIndex;
            Dirty = true;
        }

        private void tsbMoveDown_Click(object sender, EventArgs e)
        {
            m_SkipTreeSelect = true;

            // Swap the two nodes in the scene tree
            var node_parent = m_SelectedNode.Parent;
            node_parent.Children[m_SelectedTreeIndex] = node_parent.Children[m_SelectedTreeIndex + 1];
            node_parent.Children[m_SelectedTreeIndex + 1] = m_SelectedNode;

            // Remove and reinsert the node in the visual tree to match
            m_SelectedTreeParent.Nodes.RemoveAt(m_SelectedTreeIndex);
            m_SelectedTreeParent.Nodes.Insert(m_SelectedTreeIndex + 1, m_SelectedTree);
            m_SelectedTree.TreeView.SelectedNode = m_SelectedTree;

            m_SelectedTreeIndex++;
            UpdateMoveButtons();

            m_SkipTreeSelect = false;
            Dirty = true;
        }

        private void txtNodeKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            // backspace and underscores are fine
            if (e.KeyChar == 8) return;
            if (e.KeyChar == 95) return;

            // replace space with underscore
            if (e.KeyChar == 32)
            {
                e.KeyChar = '_';
                return;
            }

            // numbers are ok if they're not the first character
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                if (((TextBox)sender).TextLength == 0)
                    e.Handled = true;
            }
            // skip everything except [a-zA-Z]
            else if ((e.KeyChar < 65 || e.KeyChar > 90) && (e.KeyChar < 97 || e.KeyChar > 122))
            {
                e.Handled = true;
            }
        }

        private static AssetScene.ENodeType GetInverseNodeType(AssetScene.ENodeType type)
        {
            Debug.Assert(type == AssetScene.ENodeType.State || type == AssetScene.ENodeType.Choice);
            return type == AssetScene.ENodeType.State ? AssetScene.ENodeType.Choice : AssetScene.ENodeType.State;
        }

        private AssetScene.ENodeType GetAcceptableRootType()
        {
            return m_Scene.InjectMode >= AssetScene.ESceneInjectMode.InsideAtStart ? AssetScene.ENodeType.Choice : AssetScene.ENodeType.State;
        }

        private static AssetScene.ENodeType GetAcceptableParentType(AssetScene.SceneNode child)
        {
            switch (child.NodeType)
            {
                case AssetScene.ENodeType.Choice:
                case AssetScene.ENodeType.Compass:
                    // Choices and Compasses can be parented to States
                    return AssetScene.ENodeType.State;

                case AssetScene.ENodeType.State:
                    // States go on Choices
                    return AssetScene.ENodeType.Choice;

                case AssetScene.ENodeType.Link:
                    // Links can be on either States or Choices - make sure we maintain the current node alternation
                    return child.Parent.NodeType;

                default:
                    throw new ArgumentException(nameof(child));
            }
        }

    }

}
