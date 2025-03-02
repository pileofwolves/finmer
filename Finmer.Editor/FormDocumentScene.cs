﻿/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2025 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
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
        private SceneNode m_SelectedNode;
        private TreeNode m_SelectedTree, m_SelectedTreeParent;

        private Dictionary<int, SceneNode> m_PatchTargetCache = new Dictionary<int, SceneNode>();

        private int m_SelectedTreeIndex;
        private int m_SelectedTreeMaxIndex;

        private bool m_SkipDirtyUpdates = true;
        private bool m_SkipTreeSelect;

        private static readonly Clipboard<SceneNode> s_Clipboard = new Clipboard<SceneNode>();
        private static readonly Regex s_StateKeyRegex = new Regex(@"^(\w+)_R_([A-Za-z_]+)(\d*)[A-Z]?$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private static readonly Regex s_ChoiceKeyRegex = new Regex(@"^(\w+)_C_(.+)[A-Z]?$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public FormDocumentScene()
        {
            InitializeComponent();
        }

        private void FormDocumentScene_Load(object sender, EventArgs e)
        {
            // Duplicate the asset we're going to be editing, so that we can safely modify the copy in-place while still allowing changes to be discarded
            m_Scene = AssetSerializer.DuplicateAsset((AssetScene)Asset);
            m_PatchTargetScene = Program.LoadedContent.GetAssetByID<AssetScene>(m_Scene.PatchTargetScene);

            // Hide node edit panel by default
            tbcNode.TabPages.Clear();

            // Set up toolbar
            UpdateScriptButtonIcons();

            // Build the visual node tree
            trvNodes.SuspendLayout();
            AddNodeToTreeView(trvNodes.Nodes, m_Scene.Root);
            trvNodes.ExpandAll();
            trvNodes.ResumeLayout();

            // Set up scene root settings panel
            chkRootInject.Checked = m_Scene.IsPatchGroup;
            chkRootGameStart.Checked = m_Scene.IsGameStart;
            assetInjectTargetScene.SelectedGuid = m_Scene.PatchTargetScene;
            txtGameStartDesc.Text = m_Scene.GameStartDescription;

            // Other scene editor forms may change the shared clipboard; ensure this form updates its toolbar if the user copies from another scene
            s_Clipboard.ContentChanged += Clipboard_ContentChanged;

            // Mark the asset as dirty when the user changes node scripts
            scriptAction.Dirty += (o, arg) => Dirty = true;
            scriptAppear.Dirty += (o, arg) => Dirty = true;

            // Compass nodes and patch groups cannot target the scene they're contained in
            assetCompassTarget.SelectorPredicate = candidate => candidate.ID != m_Scene.ID;
            assetInjectTargetScene.SelectorPredicate = candidate => candidate.ID != m_Scene.ID;
        }

        private void FormDocumentScene_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Clipboard.ContentChanged -= Clipboard_ContentChanged;
        }

        private void Clipboard_ContentChanged()
        {
            // When the user switches to this form, they may have copied something from another scene asset
            tsbClipboardPaste.Enabled = CanPasteInSelectedNode();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Do not eat keypresses if focus lies elsewhere; all the below keyboard shortcuts relate to the node tree.
            // Particularly the cut/copy/paste shortcuts should remain functional while typing in the script editor views.
            if (!trvNodes.ContainsFocus)
                return base.ProcessCmdKey(ref msg, keyData);

            switch (keyData)
            {
                case Keys.Delete:               if (tsbRemoveNode.Enabled)      tsbRemoveNode_Click(this, EventArgs.Empty);         return true;
                case Keys.Alt | Keys.Up:        if (tsbMoveUp.Enabled)          tsbMoveUp_Click(this, EventArgs.Empty);             return true;
                case Keys.Alt | Keys.Down:      if (tsbMoveDown.Enabled)        tsbMoveDown_Click(this, EventArgs.Empty);           return true;
                case Keys.Control | Keys.N:     if (tsbAddNode.Enabled)         tsbAddNode_Click(this, EventArgs.Empty);            return true;
                case Keys.Control | Keys.L:     if (tsbAddLink.Enabled)         tsbAddLink_Click(this, EventArgs.Empty);            return true;
                case Keys.Control | Keys.B:     if (tsbAddCompass.Enabled)      tsbAddCompass_Click(this, EventArgs.Empty);         return true;
                case Keys.Control | Keys.X:     if (tsbClipboardCut.Enabled)    tsbClipboardCut_Click(this, EventArgs.Empty);       return true;
                case Keys.Control | Keys.C:     if (tsbClipboardCopy.Enabled)   tsbClipboardCopy_Click(this, EventArgs.Empty);      return true;
                case Keys.Control | Keys.V:     if (tsbClipboardPaste.Enabled)  tsbClipboardPaste_Click(this, EventArgs.Empty);     return true;
                default:                        return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        public override void Flush()
        {
            // Ensure any previous script editor changes are committed
            scriptAction.Flush();
            scriptAppear.Flush();

            // If a node was already selected, update its node text to "commit" pending changes
            if (m_SelectedNode != null)
                UpdateNodeText(m_SelectedTree, m_SelectedNode);

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

            // If a node was already selected, update its node text to "commit" pending changes
            if (m_SelectedNode != null)
                UpdateNodeText(m_SelectedTree, m_SelectedNode);

            // Determine which visual tree node was selected
            m_SelectedTree = trvNodes.SelectedNode;
            m_SelectedTreeParent = m_SelectedTree?.Parent;
            m_SelectedTreeIndex = m_SelectedTreeParent?.Nodes.IndexOf(m_SelectedTree) ?? -1;
            m_SelectedTreeMaxIndex = m_SelectedTreeParent?.Nodes.Count ?? -1;
            UpdateMoveButtons();

            // Determine which logical node was selected
            m_SelectedNode = trvNodes.SelectedNode?.Tag as SceneNode;
            Debug.Assert(m_SelectedNode != null);

            // We're about to programmatically update UI elements, so prevent them from reacting to changes
            m_SkipDirtyUpdates = true;
            splitNodeSettings.SuspendLayout();
            splitNodeSettings.Visible = true;

            // Remove all tabs so that we can add only the specific tab we need for this node
            tbcNode.SuspendLayout();
            tbcNode.TabPages.Clear();

            // Configure toolbar for this node
            UpdateToolbar();

            // Display specific settings for the node
            switch (m_SelectedNode.NodeType)
            {
                case SceneNode.ENodeType.Root:
                    // Root node doesn't have settings of its own, but represents the scene itself
                    Debug.Assert(m_SelectedNode.Parent == null);
                    Debug.Assert(m_SelectedNode == m_Scene.Root);
                    tbcNode.TabPages.Add(tbpNodeRoot);
                    break;

                case SceneNode.ENodeType.State:
                    // Populate state-specific settings
                    tbcNode.TabPages.Add(tbpNodeState);
                    txtNodeStateKey.Text = m_SelectedNode.Key;

                    break;

                case SceneNode.ENodeType.Choice:
                    // Populate choice-specific settings
                    tbcNode.TabPages.Add(tbpNodeChoice);
                    txtNodeChoiceKey.Text = m_SelectedNode.Key;
                    txtNodeChoiceTitle.Text = m_SelectedNode.Title;
                    txtNodeChoiceTooltip.Text = m_SelectedNode.Tooltip;
                    chkChoiceHighlight.Checked = m_SelectedNode.Highlight;
                    chkChoiceCustomWidth.Checked = Math.Abs(m_SelectedNode.ButtonWidth - 1.0f) > 0.01f;
                    nudChoiceCustomWidth.Value = chkChoiceCustomWidth.Checked
                        ? (decimal)Math.Max(Math.Min(m_SelectedNode.ButtonWidth, k_Button_Max_Width), k_Button_Min_Width)
                        : (decimal)k_Button_Default_Width;

                    break;

                case SceneNode.ENodeType.Link:
                    // Populate link-specific settings
                    tbcNode.TabPages.Add(tbpNodeLink);
                    UpdateLinkTargetList();
                    cmbLinkTarget.Text = m_SelectedNode.LinkTarget;

                    break;

                case SceneNode.ENodeType.Compass:
                    // Populate compass-specific settings
                    tbcNode.TabPages.Add(tbpNodeCompass);
                    txtNodeCompassKey.Text = m_SelectedNode.Key;
                    cmbCompassDirection.SelectedIndex = (int)m_SelectedNode.CompassLinkDirection;
                    assetCompassTarget.SelectedGuid = m_SelectedNode.CompassLinkScene;
                    break;

                case SceneNode.ENodeType.Patch when m_SelectedNode.Patch is ScenePatchAddNodes patch_add:
                    // Patch (Add Nodes)
                    tbcNode.TabPages.Add(tbpNodePatchAdd);
                    UpdatePatchTargetNodeList(cmbPatchAddTargetNode);
                    txtNodePatchAddKey.Text = m_SelectedNode.Key;
                    cmbPatchAddTargetNode.Text = patch_add.TargetNode;
                    cmbPatchAddMode.SelectedIndex = (int)patch_add.Mode;
                    break;

                case SceneNode.ENodeType.Patch when m_SelectedNode.Patch is ScenePatchReplaceNode patch_replace:
                    // Patch (Replace Nodes)
                    tbcNode.TabPages.Add(tbpNodePatchReplace);
                    UpdatePatchTargetNodeList(cmbPatchReplaceTargetNode);
                    txtNodePatchReplaceKey.Text = m_SelectedNode.Key;
                    cmbPatchReplaceTargetNode.Text = patch_replace.TargetNode;
                    chkPatchReplaceKeepChildren.Checked = patch_replace.KeepChildren;
                    break;

                case SceneNode.ENodeType.Patch when m_SelectedNode.Patch is ScenePatchRemoveNode patch_remove:
                    // Patch (Remove Nodes)
                    tbcNode.TabPages.Add(tbpNodePatchRemove);
                    UpdatePatchTargetNodeList(cmbPatchRemoveTargetNode);
                    txtNodePatchRemoveKey.Text = m_SelectedNode.Key;
                    cmbPatchRemoveTargetNode.Text = patch_remove.TargetNode;
                    break;

                default:
                    throw new InvalidOperationException();
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
            tbcNode.ResumeLayout();

            // Ensure input focus remains on the tree view - for some reason tbcNode claims focus when tabs are added
            trvNodes.Focus();
        }

        private void UpdateNodeImage(TreeNode tree_node, SceneNode scene_node)
        {
            // Select the appropriate image for this node
            switch (scene_node.NodeType)
            {
                case SceneNode.ENodeType.Root:          tree_node.ImageKey = "node_root";           break;
                case SceneNode.ENodeType.Link:          tree_node.ImageKey = "node_link";           break;
                case SceneNode.ENodeType.Compass:       tree_node.ImageKey = "node_compass";        break;
                case SceneNode.ENodeType.State:         tree_node.ImageKey = "node_state";          break;
                case SceneNode.ENodeType.Choice:        tree_node.ImageKey = "node_choice";         break;
                case SceneNode.ENodeType.Patch:
                {
                    // For Patches that target external nodes, give a clear indicator if the target is invalid
                    bool patch_is_resolved = IsPatchNodeResolved(scene_node);
                    tree_node.ImageKey = patch_is_resolved ? "node_patch" : "node_patch_error";
                    break;
                }
            }

            tree_node.SelectedImageKey = tree_node.ImageKey;
        }

        private void UpdateMoveButtons()
        {
            tsbMoveUp.Enabled = m_SelectedTreeIndex > 0;
            tsbMoveDown.Enabled = m_SelectedTreeIndex < m_SelectedTreeMaxIndex - 1;
        }

        private void UpdateLinkTargetList()
        {
            cmbLinkTarget.Items.Clear();

            var all_nodes = m_Scene.Root.Children.Traverse(node => node.Children);
            if (m_PatchTargetScene != null)
                all_nodes = all_nodes.Concat(m_PatchTargetScene.Root.Children.Traverse(node => node.Children));

            foreach (var node in all_nodes)
            {
                // Cannot link to nodes that have no key (links, root) or nodes that are not actual parts of the scene tree
                if (!node.Features.HasFlag(SceneNode.ENodeFeature.Key) || node.NodeType == SceneNode.ENodeType.Patch)
                    return;

                // Cannot link to nodes that have no key
                if (String.IsNullOrWhiteSpace(node.Key))
                    return;

                // To be a valid link target, the candidate must either be of the same type or be a compass link where compass links are accepted.
                bool can_accept_compass = GetEffectiveNodeType(m_SelectedNode) == SceneNode.ENodeType.State;
                bool is_compass = node.NodeType == SceneNode.ENodeType.Compass;
                bool is_same_type = node.NodeType == GetAcceptableChildType(m_SelectedNode.Parent.NodeType);
                if ((is_compass && can_accept_compass) || is_same_type)
                    cmbLinkTarget.Items.Add(node.Key);
            }
        }

        private void UpdateScriptButtonIcons()
        {
            // To simplify the UI for newcomer module authors, we hide the Custom Script button by default, since it is only useful for
            // Lua scripting. Show it if the Custom Script is already present or the user indicated they want to see it.
            bool has_custom_script  = m_Scene.ScriptCustom != null && m_Scene.ScriptCustom.HasContent();
            tsbScriptCustom.Image   = has_custom_script ? Resources.pencil : Resources.plus;
            tsbScriptCustom.Visible = has_custom_script || EditorPreferences.SceneEditorAllowCustomScript;

            tsbScriptEnter.Image    = m_Scene.ScriptEnter != null && m_Scene.ScriptEnter.HasContent() ? Resources.pencil : Resources.plus;
            tsbScriptLeave.Image    = m_Scene.ScriptLeave != null && m_Scene.ScriptLeave.HasContent() ? Resources.pencil : Resources.plus;
        }

        private void UpdatePatchTargetNodeList(ComboBox box)
        {
            box.Items.Clear();

            // If the target scene is unknown, we can't populate the patch list
            if (m_PatchTargetScene == null)
                return;

            var all_keys = m_PatchTargetScene.Root.Children
                .Traverse(node => node.Children)
                .Where(node => !String.IsNullOrWhiteSpace(node.Key))
                .Select(node => node.Key);

            foreach (var key in all_keys)
                box.Items.Add(key);
        }

        private void UpdateToolbar()
        {
            bool is_root = m_SelectedNode == m_Scene.Root;
            bool is_patch_group = is_root && m_Scene.IsPatchGroup;

            // If a patch targeting a node cannot be resolved, its effective type falls back to its actual underlying type of Patch
            bool is_patch_valid = m_SelectedNode.NodeType != SceneNode.ENodeType.Patch || IsPatchNodeResolved(m_SelectedNode);

            tsbAddPatch.Visible = is_patch_group;
            tsbAddNode.Visible = !is_patch_group;
            tsbAddCompass.Visible = !is_patch_group;
            tsbAddLink.Visible = !is_patch_group;
            tbcScripts.Visible = m_SelectedNode.Features.HasFlag(SceneNode.ENodeFeature.Scripts);
            tsbAddNode.Enabled = m_SelectedNode.Features.HasFlag(SceneNode.ENodeFeature.Children) && is_patch_valid;
            tsbAddLink.Enabled = tsbAddNode.Enabled;
            tsbRemoveNode.Enabled = !is_root;
            tsbClipboardCut.Enabled = !is_root;
            tsbClipboardCopy.Enabled = !is_root;
            tsbClipboardPaste.Enabled = CanPasteInSelectedNode();

            // Node can have a compass child if it's a State, or a Patch node that targets a State
            bool can_host_compass = GetEffectiveNodeType(m_SelectedNode) == SceneNode.ENodeType.State;
            tsbAddCompass.Enabled = tsbAddNode.Enabled && can_host_compass;
        }

        private void AddNodeToTreeView(TreeNodeCollection collection, SceneNode scene_node, bool select = false)
        {
            // create and add a node
            var tree_node = new TreeNode
            {
                Tag = scene_node
            };
            collection.Add(tree_node);
            UpdateNodeText(tree_node, scene_node);
            UpdateNodeImage(tree_node, scene_node);

            // add the SceneNode's children to the tree also
            scene_node.Children.ForEach(child => AddNodeToTreeView(tree_node.Nodes, child));

            if (select)
                trvNodes.SelectedNode = tree_node;
        }

        private void UpdateNodeText(TreeNode tree_node, SceneNode scene_node)
        {
            // Generate the node name based on its configuration
            switch (scene_node.NodeType)
            {
                case SceneNode.ENodeType.Root:
                    tree_node.ForeColor = Color.Black;
                    tree_node.Text = "Root";
                    break;

                case SceneNode.ENodeType.State:
                case SceneNode.ENodeType.Choice:
                {
                    // For generic nodes, concatenate the node key and any prefixes
                    bool is_state = scene_node.NodeType == SceneNode.ENodeType.State;
                    bool has_action_script = scene_node.ScriptAction != null && scene_node.ScriptAction.HasContent();
                    bool has_appear_script = scene_node.ScriptAppear != null && scene_node.ScriptAppear.HasContent();
                    string choice_caption = String.IsNullOrWhiteSpace(scene_node.Title) ? "Continue" : scene_node.Title;
                    string[] elements =
                    {
                        String.IsNullOrWhiteSpace(scene_node.Key) ? String.Empty : $"[{scene_node.Key}]",
                        !is_state ? choice_caption : String.Empty,
                        has_action_script ? "(!)" : String.Empty,
                        has_appear_script ? "(?)" : String.Empty,
                    };
                    tree_node.ForeColor = is_state ? Color.DarkRed : Color.DarkBlue;
                    tree_node.Text = String.Join(" ", elements.Where(str => !String.IsNullOrEmpty(str)));
                    break;
                }

                case SceneNode.ENodeType.Link:
                {
                    // Link target
                    tree_node.ForeColor = Color.DarkSlateGray;
                    tree_node.Text = "to " + scene_node.LinkTarget;
                    break;
                }

                case SceneNode.ENodeType.Compass:
                {
                    // Compass link
                    bool has_action_script = scene_node.ScriptAction != null && scene_node.ScriptAction.HasContent();
                    bool has_appear_script = scene_node.ScriptAppear != null && scene_node.ScriptAppear.HasContent();
                    var compass_target = Program.LoadedContent.GetAssetByID<AssetScene>(scene_node.CompassLinkScene);
                    string[] elements =
                    {
                        String.IsNullOrWhiteSpace(scene_node.Key) ? String.Empty : $"[{scene_node.Key}]",
                        scene_node.CompassLinkDirection.ToString(),
                        (compass_target != null && !has_action_script) ? "to " + compass_target.Name : String.Empty,
                        has_action_script ? "(custom script)" : String.Empty,
                        has_appear_script ? "(?)" : String.Empty,
                    };
                    tree_node.ForeColor = Color.DarkSlateGray;
                    tree_node.Text = String.Join(" ", elements.Where(str => !String.IsNullOrEmpty(str)));
                    break;
                }

                case SceneNode.ENodeType.Patch:
                {
                    // Patch root
                    bool patch_is_resolved = IsPatchNodeResolved(scene_node);
                    string[] elements =
                    {
                        patch_is_resolved ? "Patch:" : "<Patch Target Not Found>",
                        String.IsNullOrWhiteSpace(scene_node.Key) ? String.Empty : $"[{scene_node.Key}]",
                        scene_node.Patch.GetEditorDescription(Program.LoadedContent)
                    };
                    tree_node.ForeColor = Color.Indigo;
                    tree_node.Text = String.Join(" ", elements.Where(str => !String.IsNullOrEmpty(str)));
                    break;
                }
            }
        }

        private void tsbAddNode_Click(object sender, EventArgs e)
        {
            // Safeguard: prevent adding children to leaf nodes
            if (!m_SelectedNode.Features.HasFlag(SceneNode.ENodeFeature.Children))
                return;

            // Generate a new node
            var node = new SceneNode
            {
                NodeType = GetAcceptableChildType(GetEffectiveNodeType(m_SelectedNode)),
                Parent = m_SelectedNode
            };

            // If enabled, try auto-generating a sensible key for this node
            if (EditorPreferences.SceneEditorGuessChildKeys)
                node.Key = GenerateChildNodeKey(m_SelectedNode);

            // Append it to the internal representation and the UI tree
            Debug.Assert(m_SelectedNode != null);
            m_SelectedNode.Children.Add(node);
            AddNodeToTreeView(m_SelectedTree.Nodes, node, true);

            Dirty = true;
        }

        private void tsbAddLink_Click(object sender, EventArgs e)
        {
            // Safeguard: prevent adding children to leaf nodes
            if (!m_SelectedNode.Features.HasFlag(SceneNode.ENodeFeature.Children))
                return;

            // Generate a new node
            var node = new SceneNode
            {
                NodeType = SceneNode.ENodeType.Link,
                Parent = m_SelectedNode
            };

            // Append it to the internal representation and the UI tree
            m_SelectedNode.Children.Add(node);
            AddNodeToTreeView(m_SelectedTree.Nodes, node, true);

            Dirty = true;
        }

        private void tsbAddCompass_Click(object sender, EventArgs e)
        {
            // Safeguard: prevent adding children to leaf nodes
            if (!m_SelectedNode.Features.HasFlag(SceneNode.ENodeFeature.Children))
                return;

            // Generate a new node
            var node = new SceneNode
            {
                NodeType = SceneNode.ENodeType.Compass,
                Parent = m_SelectedNode
            };

            // Append it to the internal representation and the UI tree
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

        private void mnuAddPatchNodeAdd_Click(object sender, EventArgs e)
        {
            CreatePatch(new ScenePatchAddNodes());
        }

        private void mnuAddPatchNodeReplace_Click(object sender, EventArgs e)
        {
            CreatePatch(new ScenePatchReplaceNode());
        }

        private void mnuAddPatchNodeRemove_Click(object sender, EventArgs e)
        {
            CreatePatch(new ScenePatchRemoveNode());
        }

        private void CreatePatch(ScenePatch patch)
        {
            Debug.Assert(m_SelectedNode.NodeType == SceneNode.ENodeType.Root);

            // Generate a new Patch
            var node = new SceneNode
            {
                NodeType = SceneNode.ENodeType.Patch,
                Parent = m_Scene.Root,
                Patch = patch
            };

            // Append it to the scene tree
            m_SelectedNode.Children.Add(node);
            AddNodeToTreeView(m_SelectedTree.Nodes, node, true);
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
            RegisterNestedWindow(Program.MainForm.OpenEditorWindow(m_Scene.ScriptCustom));
        }

        private void tsbScriptEnter_Click(object sender, EventArgs e)
        {
            m_Scene.ScriptEnter = ScriptDataWrapper.EnsureWrapped(m_Scene.ScriptEnter);
            RegisterNestedWindow(Program.MainForm.OpenEditorWindow(m_Scene.ScriptEnter));
        }

        private void tsbScriptLeave_Click(object sender, EventArgs e)
        {
            m_Scene.ScriptLeave = ScriptDataWrapper.EnsureWrapped(m_Scene.ScriptLeave);
            RegisterNestedWindow(Program.MainForm.OpenEditorWindow(m_Scene.ScriptLeave));
        }

        private void tsbMoveUp_Click(object sender, EventArgs e)
        {
            m_SkipTreeSelect = true;

            // Move the scene node in the asset
            m_SelectedNode.Parent.Children[m_SelectedTreeIndex] = m_SelectedNode.Parent.Children[m_SelectedTreeIndex - 1];
            m_SelectedNode.Parent.Children[m_SelectedTreeIndex - 1] = m_SelectedNode;

            // Move tree node in UI
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

        private void cmbCompassDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_SkipDirtyUpdates) return;
            Dirty = true;

            m_SelectedNode.CompassLinkDirection = (SceneNode.ECompassDirection)cmbCompassDirection.SelectedIndex;
        }

        private void assetCompassTarget_SelectedAssetChanged(object sender, EventArgs e)
        {
            if (m_SkipDirtyUpdates) return;
            Dirty = true;

            m_SelectedNode.CompassLinkScene = assetCompassTarget.SelectedGuid;
        }

        private void chkPatchReplaceKeepChildren_CheckedChanged(object sender, EventArgs e)
        {
            if (m_SkipDirtyUpdates) return;
            Dirty = true;

            ((ScenePatchReplaceNode)m_SelectedNode.Patch).KeepChildren = chkPatchReplaceKeepChildren.Checked;
            UpdateNodeText(m_SelectedTree, m_SelectedNode);
        }

        private void trvNodes_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Can only drag/drop tree nodes
            if (!(e.Item is TreeNode tree_node))
                return;

            // Cannot change the scene root
            if (tree_node.Parent == null)
                return;

            // Must be left or right mouse
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right)
                return;

            // If doing a move, collapse the node, to prevent dragging the source onto one of its children (which would lead to a cycle)
            if (e.Button == MouseButtons.Left)
                tree_node.Collapse();

            // Permit the drag operation
            DoDragDrop(e.Item, e.Button == MouseButtons.Left ? DragDropEffects.Move :  DragDropEffects.Link);
        }

        private void trvNodes_DragDrop(object sender, DragEventArgs e)
        {
            // Get the node that is being dragged, and the node it is being dropped on
            var source = (TreeNode)e.Data.GetData(typeof(TreeNode));
            var target = trvNodes.GetNodeAt(trvNodes.PointToClient(new Point(e.X, e.Y)));

            // Get the SceneNode of those node representations
            var source_sn = (SceneNode)source.Tag;
            var target_sn = (SceneNode)target.Tag;

            // Get the SceneNode of the source node's parent, so we can remove it from there
            var source_parent = source.Parent;
            var source_parent_sn = (SceneNode)source_parent.Tag;

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
                    var link = new SceneNode
                    {
                        NodeType = SceneNode.ENodeType.Link,
                        LinkTarget = source_sn.Key,
                        Parent = target_sn
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
                var node_sn = (SceneNode)node.Tag;

                // Links do not have keys
                if (!node_sn.Features.HasFlag(SceneNode.ENodeFeature.Key))
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
            var link_node_sn = (SceneNode)link_node.Tag;
            if (link_node_sn.NodeType == SceneNode.ENodeType.Link)
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
            // Unless we determine otherwise, the drop action is invalid by default
            e.Effect = DragDropEffects.None;

            // Find the particular node the mouse is hovering over
            var source = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            Point mouse = trvNodes.PointToClient(new Point(e.X, e.Y));
            TreeNode target = trvNodes.GetNodeAt(mouse);
            if (target == null || source == null)
                return;

            // Retrieve the SceneNodes represented by these tree items
            var source_sn = (SceneNode)source.Tag;
            var target_sn = (SceneNode)target.Tag;
            if (CanReparentNode(source_sn, target_sn))
            {
                // Notify the system which actions are allowed now
                e.Effect = e.AllowedEffect;
            }
        }

        private void chkRootInject_CheckedChanged(object sender, EventArgs e)
        {
            // Skip interaction checks when the UI is being set up on startup
            if (!m_SkipDirtyUpdates)
            {
                // If the patch mode isn't actually being changed, no need to do anything (relevant for the checkbox being reverted, see below)
                if (chkRootInject.Checked == m_Scene.IsPatchGroup)
                    return;

                // If the scene already has nodes in it, they must be deleted
                if (m_Scene.Root.Children.Count != 0)
                {
                    if (MessageBox.Show("To change the patch group setting, all nodes in the scene must be removed. If you wish to keep them, Copy (Ctrl+C) or Cut (Ctrl+X) them onto the clipboard first.\r\n\r\nWould you like to continue and erase the scene contents?",
                            "Finmer Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        // Set the checkbox back to its previous value
                        chkRootInject.Checked = m_Scene.IsPatchGroup;
                        return;
                    }

                    // Erase the scene contents
                    m_Scene.Root.Children.Clear();
                    trvNodes.Nodes.Clear();
                    AddNodeToTreeView(trvNodes.Nodes, m_Scene.Root, true);
                }
            }

            // Update UI configuration
            bool is_patch_group = chkRootInject.Checked;
            pnlInjectionSettings.Visible = is_patch_group;
            tsbScriptCustom.Enabled = !is_patch_group;
            tsbScriptEnter.Enabled = !is_patch_group;
            tsbScriptLeave.Enabled = !is_patch_group;

            // Do not mark asset as dirty if UI is being set up
            if (m_SkipDirtyUpdates)
                return;

            // Uncheck game start if checking patch, since they are mutually exclusive
            if (is_patch_group)
                chkRootGameStart.Checked = false;

            // Update scene data
            m_Scene.IsPatchGroup = is_patch_group;

            // Update UI with changed settings
            Dirty = true;
            UpdateToolbar();
            Program.MainForm.UpdateAssetIcon(m_Scene);
        }

        private void chkRootGameStart_CheckedChanged(object sender, EventArgs e)
        {
            bool mode = chkRootGameStart.Checked;
            pnlGameStartSettings.Visible = mode;

            // Do not mark asset as dirty if UI is being set up
            if (m_SkipDirtyUpdates)
                return;

            // Uncheck game start if checking patch
            if (mode)
                chkRootInject.Checked = false;

            // Update scene data
            m_Scene.IsGameStart = mode;
            Dirty = true;

            // Update icon on main form
            Program.MainForm.UpdateAssetIcon(m_Scene);
        }

        private void txtGameStartDesc_TextChanged(object sender, EventArgs e)
        {
            if (m_SkipDirtyUpdates)
                return;

            m_Scene.GameStartDescription = txtGameStartDesc.Text;
            Dirty = true;
        }

        private void assetInjectTargetScene_SelectedAssetChanged(object sender, EventArgs e)
        {
            if (m_SkipDirtyUpdates)
                return;

            m_PatchTargetScene = Program.LoadedContent.GetAssetByID<AssetScene>(assetInjectTargetScene.SelectedGuid);
            m_Scene.PatchTargetScene = assetInjectTargetScene.SelectedGuid;
        }

        private void cmbPatchTargetNode_TextChanged(object sender, EventArgs e)
        {
            if (m_SkipDirtyUpdates)
                return;

            ComboBox box = (ComboBox)sender;
            ((ScenePatchTargetNodeBase)m_SelectedNode.Patch).TargetNode = box.Text;
            Dirty = true;

            // Re-resolve the patch target, and reflect in the icon whether the target node is known
            UpdateToolbar();
            UpdateNodeText(m_SelectedTree, m_SelectedNode);
            UpdateNodeImage(m_SelectedTree, m_SelectedNode);
        }

        private void cmbPatchAddMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_SkipDirtyUpdates)
                return;

            ((ScenePatchAddNodes)m_SelectedNode.Patch).Mode = (ScenePatchAddNodes.EInjectMode)cmbPatchAddMode.SelectedIndex;
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

        private void tsbClipboardCut_Click(object sender, EventArgs e)
        {
            // Store the selected node in the clipboard
            s_Clipboard.Set(m_SelectedNode);

            // Remove the node from the scene tree, to emulate cutting behavior. This will reselect another node, and update the toolbar accordingly.
            m_SelectedNode.Parent.Children.Remove(m_SelectedNode);
            m_SelectedTree.Remove();

            // This asset has been modified
            Dirty = true;
        }

        private void tsbClipboardCopy_Click(object sender, EventArgs e)
        {
            // Store the selected node in the clipboard, without erasing it from the scene tree
            s_Clipboard.Set(m_SelectedNode);

            // Update UI
            tsbClipboardPaste.Enabled = CanPasteInSelectedNode();
        }

        private void tsbClipboardPaste_Click(object sender, EventArgs e)
        {
            // Sanity check; prevent linking up broken content just in case the toolbar button is enabled when it shouldn't
            if (!CanPasteInSelectedNode())
                return;

            // Attach a copy of the clipboard contents to the selected node
            var copy = s_Clipboard.CopyBuffer();
            copy.Parent = m_SelectedNode;
            m_SelectedNode.Children.Add(copy);
            AddNodeToTreeView(m_SelectedTree.Nodes, copy, true);

            // This asset has been modified
            Dirty = true;
        }

        private SceneNode.ENodeType GetEffectiveNodeType(SceneNode node)
        {
            // Patch nodes represent a remote point in another scene tree; try to resolve the patch to find the actual type of the targeted node
            if (node.NodeType == SceneNode.ENodeType.Patch && node.Patch is ScenePatchTargetNodeBase patch_tree)
            {
                Debug.Assert(m_PatchTargetScene != m_Scene && m_PatchTargetScene != Asset);

                // If the user has not specified a patch target, then we cannot evaluate whether the patch matches the target scene tree
                if (m_PatchTargetScene == null)
                    return SceneNode.ENodeType.Patch;

                // Look for the target node in the cache
                int cache_key = GetSceneNodeCacheKey(m_PatchTargetScene, patch_tree.TargetNode);
                if (!m_PatchTargetCache.TryGetValue(cache_key, out var target_node))
                {
                    // Search for the target node the hard way, using a depth-first search through the target scene tree
                    target_node = m_PatchTargetScene?.GetNodeByKey(patch_tree.TargetNode);

                    // If we found it, cache it for future use
                    if (target_node != null)
                        m_PatchTargetCache.Add(cache_key, target_node);
                }

                // If we found the node in the target scene, use it to determine the real type of this patch node
                if (target_node != null)
                {
                    // Some types of patches implicitly break the alternating order of States and Choices, which we must account for here
                    if ((node.Patch is ScenePatchReplaceNode) ||
                        (node.Patch is ScenePatchAddNodes patch_add && patch_add.Mode <= ScenePatchAddNodes.EInjectMode.AfterTarget))
                        return GetAcceptableParentType(target_node);

                    return target_node.NodeType;
                }

                // If the patch target cannot be resolved, return the Patch node type unchanged
            }

            // Note: this function does not resolve Links, because there are no current use cases where this function is called for Links
            // and the output distinction between represented State/Choice nodes would be relevant.

            // All other node types are local to this scene tree
            return node.NodeType;
        }

        private bool IsPatchNodeResolved(SceneNode scene_node)
        {
            Debug.Assert(scene_node.NodeType == SceneNode.ENodeType.Patch);
            Debug.Assert(scene_node.Patch != null);

            // If the patch node's effective type cannot be deduced from the target scene, then it is unresolved
            return GetEffectiveNodeType(scene_node) != SceneNode.ENodeType.Patch;
        }

        private static SceneNode.ENodeType GetAcceptableParentType(SceneNode child)
        {
            switch (child.NodeType)
            {
                case SceneNode.ENodeType.Choice:
                case SceneNode.ENodeType.Compass:
                    // Choices and Compasses can be parented to States
                    return SceneNode.ENodeType.State;

                case SceneNode.ENodeType.State:
                    // States go on Choices
                    return SceneNode.ENodeType.Choice;

                case SceneNode.ENodeType.Link:
                    // Links can be on either States or Choices - make sure we maintain the current node alternation
                    return child.Parent.NodeType;

                case SceneNode.ENodeType.Patch:
                    // Patches only ever go on the root
                    return SceneNode.ENodeType.Root;

                default:
                    throw new ArgumentException(nameof(child));
            }
        }

        private SceneNode.ENodeType GetAcceptableChildType(SceneNode.ENodeType type)
        {
            // The inverse of the root node is whichever type can be parented to the root (given the patch mode)
            if (type == SceneNode.ENodeType.Root)
                return GetAcceptableRootType();

            // Otherwise, States and Choices must alternate
            Debug.Assert(type == SceneNode.ENodeType.State || type == SceneNode.ENodeType.Choice);
            return type == SceneNode.ENodeType.State ? SceneNode.ENodeType.Choice : SceneNode.ENodeType.State;
        }

        private SceneNode.ENodeType GetAcceptableRootType()
        {
            // Top-level nodes are States, except in patch groups, in which case they must be Patches
            return m_Scene.IsPatchGroup ? SceneNode.ENodeType.Patch : SceneNode.ENodeType.State;
        }

        private bool CanReparentNode(SceneNode source, SceneNode target)
        {
            // Cannot parent nodes to leaf nodes, or to themselves
            if (source == target || !target.Features.HasFlag(SceneNode.ENodeFeature.Children))
                return false;

            // If the source is a Link and has no parent (e.g. a Link copied to the clipboard), we have no context for its State/Choice status and must accept the move
            if (source.NodeType == SceneNode.ENodeType.Link && source.Parent == null)
                return true;

            // Nodes must follow the State/Choice alternation. Some extra logic is required to handle the Root node correctly.
            var target_type = GetEffectiveNodeType(target);
            bool can_parent_generic = target_type == GetAcceptableParentType(source);
            bool can_parent_root_node = target_type == SceneNode.ENodeType.Root && source.NodeType == GetAcceptableRootType();
            return can_parent_generic || can_parent_root_node;
        }

        private bool CanPasteInSelectedNode()
        {
            return s_Clipboard.HasContent()
                && m_SelectedNode != null
                && CanReparentNode(s_Clipboard.PeekBuffer(), m_SelectedNode);
        }

        /// <summary>
        /// Auto-generate a unique key for a child node of the specified parent, using the parent's type and key as input.
        /// </summary>
        private static string GenerateChildNodeKey(SceneNode parent)
        {
            string part_group, part_topic, part_type;

            switch (parent.NodeType)
            {
                case SceneNode.ENodeType.State:
                {
                    // Parse the key of the parent State. If unsuccessful, we cannot auto-generate a key.
                    var match = s_StateKeyRegex.Match(parent.Key);
                    if (!match.Success)
                        return String.Empty;

                    // Child node should be a Choice ('C' for 'call') inheriting the State's namespace label
                    part_group = match.Groups[1].Value;
                    part_topic = match.Groups[2].Value;
                    part_type = "C";

                    // Append a sequence number
                    if (match.Groups.Count == 4 &&
                        Int32.TryParse(match.Groups[3].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int sequence))
                    {
                        // The parent node includes a sequence number - increment it (since the choice represents the next sequence)
                        sequence++;

                        // Append it to the topic, making sure we use the same number of leading zeroes
                        part_topic += sequence.ToString("D" + match.Groups[3].Length, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        // Since this Choice must be the second sequence item, append a default sequence number
                        part_topic += "02";
                    }

                    break;
                }

                case SceneNode.ENodeType.Choice:
                {
                    // Parse the key of the parent Choice. If unsuccessful, we cannot auto-generate a key.
                    var match = s_ChoiceKeyRegex.Match(parent.Key);
                    if (!match.Success)
                        return String.Empty;

                    // Child node should be a State ('R' for 'response') inheriting the Choice's namespace label
                    part_group = match.Groups[1].Value;
                    part_topic = match.Groups[2].Value;
                    part_type = "R";
                    break;
                }

                default:
                    // Cannot auto-generate key for other node types
                    return String.Empty;
            }

            // We may also need to append an additional letter suffix, if there are siblings with the same key
            for (int i = 0; i < 26; i++)
            {
                // First node has no letter suffix, then start with B, C, and so on
                string alphabet_suffix = (i == 0) ? String.Empty : ((char)('A' + i)).ToString();

                // Combine all the extracted parts and computed suffixes into one key
                string combined = String.Format(CultureInfo.InvariantCulture, "{0}_{1}_{2}{3}", part_group, part_type, part_topic, alphabet_suffix);

                // If this key is not yet in use, apply it
                if (!parent.Children.Exists(node => node.Key.Equals(combined, StringComparison.InvariantCultureIgnoreCase)))
                    return combined;
            }

            // Otherwise, if we can't find a usable suffix, abort the auto-generation
            return String.Empty;
        }

        /// <summary>
        /// Returns a hash (cache key) for an unresolved scene node.
        /// </summary>
        private static int GetSceneNodeCacheKey(AssetScene scene, string key)
        {
            unchecked
            {
                // Combine the hashes of the scene and a node key (which is unresolved at this point, that's what the cache is for)
                int hash = 17;
                hash = hash * 31 + scene.GetHashCode();
                hash = hash * 31 + key.GetHashCode();
                return hash;
            }
        }

    }

}
