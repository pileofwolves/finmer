/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2023 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Finmer.Core;
using Finmer.Core.Assets;
using Finmer.Core.Serialization;
using WeifenLuo.WinFormsUI.Docking;

namespace Finmer.Editor
{

    public partial class FormMain : RibbonForm
    {

        private readonly TreeNode m_NodeCreatures;
        private readonly TreeNode m_NodeItems;
        private readonly TreeNode m_NodeJournals;
        private readonly TreeNode m_NodeScenes;
        private readonly TreeNode m_NodeScripts;
        private readonly TreeNode m_NodeTexts;

        private readonly Dictionary<int, EditorWindow> m_OpenWindows = new Dictionary<int, EditorWindow>();

        private bool m_Dirty;
        private string m_Filename;

        public FormMain()
        {
            InitializeComponent();

            // Assign singleton reference
            Program.MainForm = this;

            // Dock the ribbon to the top of the window, since this cannot be configured in the Designer for some reason
            ribbon.Dock = DockStyle.Top;

            // Inject our custom color schemes
            ((RibbonProfessionalRenderer)ribbon.Renderer).Theme.RendererColorTable = new RibbonColorTableCustom();
            dockPanel.Theme = new VS2015LightTheme();

            // Generate root tree nodes that we'll use as parents for the different asset categories
            m_NodeScenes = trvAssetList.Nodes.Add("Scenes", "Scenes", 0);
            m_NodeItems = trvAssetList.Nodes.Add("Items", "Items", 0);
            m_NodeCreatures = trvAssetList.Nodes.Add("Creatures", "Creatures", 0);
            m_NodeJournals = trvAssetList.Nodes.Add("Journals", "Journals", 0);
            m_NodeTexts = trvAssetList.Nodes.Add("String Tables", "String Tables", 0);
            m_NodeScripts = trvAssetList.Nodes.Add("Scripts", "Scripts", 0);
        }

        public EditorWindow OpenEditorWindow(IFurballSerializable data)
        {
            // TODO: Prevent opening dependencies, since they cannot be edited while a different project is opened

            // If asset is already open, focus the window instead of making a new one
            int object_key = GetEditorWindowKey(data);
            if (m_OpenWindows.TryGetValue(object_key, out var open_window))
            {
                open_window.Show();
                return open_window;
            }

            // Create an appropriate editor window for the asset type
            EditorWindow window = CreateEditorWindow(data);

            // Ensure the opened-window association is removed when the window is closed
            window.Closed += (o, args) => m_OpenWindows.Remove(object_key);

            // Dock and display the window
            window.Show(dockPanel, DockState.Document);
            m_OpenWindows.Add(object_key, window);

            return window;
        }

        private static int GetEditorWindowKey(IFurballSerializable data)
        {
            // For assets, use the asset GUID, which is guaranteed to not be changed by the editor window.
            // AssetWindows can replace the asset pointer itself, so the GUID is the only property that will always remain the same.
            if (data is AssetBase asset)
                return asset.ID.GetHashCode();

            // Otherwise, hash the pointer to the object - base EditorWindows cannot replace the edited object
            return RuntimeHelpers.GetHashCode(data);
        }

        public void MarkDirty()
        {
            m_Dirty = true;
            rbtProjSave.Enabled = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                if (rbtProjSave.Enabled)
                    rbtProjSave.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.O))
            {
                if (rbtProjOpen.Enabled)
                    rbtProjOpen.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private EditorWindow CreateEditorWindow(IFurballSerializable data)
        {
            switch (data)
            {
                case AssetScene asset:          return new FormDocumentScene                { Asset = asset };
                case AssetItem asset:           return new FormDocumentItem                 { Asset = asset };
                case AssetCreature asset:       return new FormDocumentCreature             { Asset = asset };
                case AssetStringTable asset:    return new FormDocumentStringTable          { Asset = asset };
                case AssetScript asset:         return new FormDocumentScriptExternal       { Asset = asset };
                case AssetJournal asset:        return new FormDocumentJournal              { Asset = asset };
                case ScriptDataWrapper script:  return new FormDocumentScriptNested         { ScriptWrapper = script };
                default:                        throw new ArgumentException(nameof(data));
            }
        }

        private void UpdateWindowTitle()
        {
            Text = Program.ActiveFurball.Metadata.Title + " - Finmer Editor";
        }

        private void ClearUI()
        {
            // Close all windows
            List<EditorWindow> window_list_clone = m_OpenWindows.Values.ToList();
            window_list_clone.ForEach(window =>
            {
                // Prevent save confirmation dialog from showing up
                window.Dirty = false;
                window.Close();
            });
            m_OpenWindows.Clear();

            // Remove all tree nodes for assets
            m_NodeScenes.Nodes.Clear();
            m_NodeItems.Nodes.Clear();
            m_NodeCreatures.Nodes.Clear();
            m_NodeTexts.Nodes.Clear();
            m_NodeScripts.Nodes.Clear();
            m_NodeJournals.Nodes.Clear();

            // Disable saving UI
            m_Dirty = false;
            rbtProjSave.Enabled = false;
        }

        private void RegisterNewAsset(AssetBase asset)
        {
            // Add asset to asset list view
            Program.ActiveFurball.Assets.Add(asset);
            AddAssetToList(asset, true);

            // Adding assets changes the project, so allow saving
            MarkDirty();

            // The user likely wants to begin editing the new asset, so open it immediately
            OpenEditorWindow(asset);
        }

        private void AddAssetToList(AssetBase asset, bool select)
        {
            // Generate a new TreeNode for opening this asset
            var node = new TreeNode(asset.Name);
            switch (asset)
            {
                case AssetScene _:
                    node.ImageKey = "scene";
                    m_NodeScenes.Nodes.Add(node);
                    break;
                case AssetItem _:
                    node.ImageKey = "item";
                    m_NodeItems.Nodes.Add(node);
                    break;
                case AssetCreature _:
                    node.ImageKey = "creature";
                    m_NodeCreatures.Nodes.Add(node);
                    break;
                case AssetStringTable _:
                    node.ImageKey = "text";
                    m_NodeTexts.Nodes.Add(node);
                    break;
                case AssetScript _:
                    node.ImageKey = "script";
                    m_NodeScripts.Nodes.Add(node);
                    break;
                case AssetJournal _:
                    node.ImageKey = "text";
                    m_NodeJournals.Nodes.Add(node);
                    break;
                default:
                    throw new ArgumentException(nameof(asset));
            }

            // If requested, select it now
            if (select)
                trvAssetList.SelectedNode = node;

            // Other settings
            node.Tag = asset;
            node.SelectedImageKey = node.ImageKey;
        }

        /// <summary>
        /// Show an unsaved-changes confirmation prompt, if there are any unsaved changes.
        /// </summary>
        /// <returns>
        /// Returns true if the application can safely continue discarding current state. Returns false if the save was somehow
        /// cancelled, in which case the application must CANCEL any planned state changes.
        /// </returns>
        private bool TrySaveUnsavedChanges()
        {
            // No changes, can always safely continue
            if (!m_Dirty)
                return true;

            // If there are changes, allow the user to cancel
            DialogResult dr = MessageBox.Show("Would you like to save unsaved changes?", "Finmer Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.Cancel)
                return false;
            if (dr == DialogResult.Yes && !Save())
                return false;

            // Otherwise, we're good
            return true;
        }

        private bool Save()
        {
            // Have user pick a filename if none is known
            if (m_Filename == null)
                return SaveAs();

            // Update asset table with latest changes
            foreach (EditorWindow window in m_OpenWindows.Values)
            {
                // Make sure editor windows commit any changes
                window.Flush();
            }

            // Commit the module to disk
            try
            {
                // Save a text version of the module
                var project_file = new FileInfo(m_Filename);
                FurballFileDevice device = new FurballFileDeviceText();
                device.WriteModule(Program.ActiveFurball, project_file);

                // Save a packed binary module as well
                var module_file = new FileInfo(Path.Combine(GetModulesFolder(), Path.ChangeExtension(project_file.Name, ".furball")));
                device = new FurballFileDeviceBinary();
                device.WriteModule(Program.ActiveFurball, module_file);
            }
            catch (FurballException ex)
            {
                MessageBox.Show($"Could not save the project: {ex.Message}\n\nPlease ensure the project file(s) are writable, and check the launch settings in the Editor Preferences (since the editor automatically tries to save a .furball package as well).",
                    "Finmer Editor", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Let upstream code know that saving has failed
                return false;
            }

            // Disable saving UI
            UpdateWindowTitle();
            m_Dirty = false;
            rbtProjSave.Enabled = false;

            return true;
        }

        internal void ReplaceAssetTreeNode(AssetBase old_asset, AssetBase new_asset)
        {
            Debug.Assert(GetEditorWindowKey(old_asset) == GetEditorWindowKey(new_asset), "Window key has changed");

            var tree_node = FindAssetTreeNode(old_asset);
            if (tree_node != null)
                tree_node.Tag = new_asset;
        }

        private bool SaveAs()
        {
            // Present a save dialog
            if (dlgSave.ShowDialog() != DialogResult.OK)
                return false;
                
            // Check if there are any other Finmer project files in the directory we're saving this project to
            string save_path = Path.GetDirectoryName(dlgSave.FileName) ?? String.Empty;
            string[] file_entries = Directory.GetFiles(save_path, "*.fnproj");
            if (file_entries.Any(name => !name.Equals(dlgSave.FileName)))
            {
                // Alert user that saving here will delete unused json files in the directory, have them acknowledge that fact before continuing.
                DialogResult dr = MessageBox.Show("The selected folder contains another Finmer project. This is not supported by the Editor: files from the other project will be overwritten and/or deleted. Continuing anyway may cause unrecoverable data loss. Are you sure you wish to proceed?", "Finmer Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No)
                    return false;
            }

            // Keep the selected filename around so we can save to it in the future
            m_Filename = dlgSave.FileName;

            // Save the file
            return Save();
        }

        private void OpenProject(string filename)
        {
            try
            {
                // Prevent all interaction with the UI, to avoid concurrent requests and the like
                ribbon.Enabled = false;
                splitContainer.Enabled = false;

                // Close the tree view to prevent the UI redrawing every change we make, which is extremely slow
                trvAssetList.SuspendLayout();

                // Present a dialog for the duration of the load
                using (var loading_window = new FormLoadingProgress())
                {
                    loading_window.Show(this);

                    // Reset current loaded module
                    ClearUI();

                    // Configure content store
                    Program.LoadedContent = new EditorContentStore();

                    // Load project from disk
                    var device = new FurballFileDeviceText();
                    try
                    {
                        loading_window.SetLabel("Unpacking module...");
                        Program.ActiveFurball = device.ReadModule(new FileInfo(filename));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Couldn't load the asset package: {ex}", "Finmer Editor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Show upgrade warning, if this version is older but upgradable
                    if (Program.ActiveFurball.Metadata.FormatVersion != FurballFileDevice.k_LatestVersion)
                    {
                        using (var upgrade_prompt = new FormProjectUpgrade())
                        {
                            if (upgrade_prompt.ShowDialog(this) != DialogResult.OK)
                            {
                                // User declined to upgrade; abort the project load and reset main window
                                rbtProjNew_Click(null, EventArgs.Empty);
                                return;
                            }
                        }
                    }

                    loading_window.SetLabel("Loading dependencies...");

                    // Figure out the folder where the module file is located
                    string parent_folder = Path.GetDirectoryName(filename);
                    Debug.Assert(parent_folder != null);

                    // Load all dependencies of this module
                    Program.ActiveDependencies = new Furball();
                    List<string> dep_load_failures = new List<string>();
                    foreach (FurballDependency dependency in Program.ActiveFurball.Dependencies)
                        try
                        {
                            // Find the dependency file based on the file name hint
                            string dep_file_name = FindBinaryModuleFile(parent_folder, dependency.FileNameHint);
                            if (dep_file_name == null)
                            {
                                dep_load_failures.Add($"- {dependency.ID} ({dependency.FileNameHint}) could not be found");
                                continue;
                            }

                            // We expect the dependency as a binary-mode furball, so use a binary device accordingly
                            FurballFileDevice dep_device = new FurballFileDeviceBinary();
                            Furball dep_module = dep_device.ReadModule(new FileInfo(dep_file_name));
                            Program.ActiveDependencies.Merge(dep_module);
                        }
                        catch (Exception ex)
                        {
                            dep_load_failures.Add($"- {dependency.ID} in file {dependency.FileNameHint} (reason: {ex.Message})");
                        }

                    // Display a warning dialog if not all dependencies were loaded
                    if (dep_load_failures.Count > 0)
                        MessageBox.Show("Warning: One or more dependencies could not be loaded. This module can still be edited, but some data from other modules may be missing.\r\n\r\n" +
                            String.Join(Environment.NewLine, dep_load_failures), "Module Dependencies Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Reconfigure editor window
                    loading_window.SetLabel("Preparing editor...");
                    UpdateWindowTitle();

                    // show new assets in list
                    loading_window.SetupProgress(Program.ActiveFurball.Assets.Count);
                    foreach (var asset in Program.ActiveFurball.Assets)
                    {
                        loading_window.AddProgress();
                        AddAssetToList(asset, false);
                    }

                    // Allow the module to be saved right from the get-go. This allows users to open a project and then immediately
                    // convert it to a binary module file (furball) without having to awkwardly get the editor to call MarkDirty().
                    // This is NOT the same as calling MarkDirty here since we don't want the editor to actually ask "save unchanged changes?" etc.
                    m_Filename = filename;
                    rbtProjSave.Enabled = true;
                }
            }
            finally
            {
                // Re-enable main window UI
                splitContainer.Enabled = true;
                ribbon.Enabled = true;

                // Re-enable layout passes in the tree view
                trvAssetList.ExpandAll();
                trvAssetList.ResumeLayout();
            }
        }

        private string GetModulesFolder()
        {
            // Prefer using the user-specified working directory
            string work_dir = EditorPreferences.ExecutableWorkingDirectory;

            // If it is unset, use the folder where the executable resides
            if (String.IsNullOrWhiteSpace(work_dir))
                work_dir = new FileInfo(EditorPreferences.ExecutablePath).DirectoryName;

            // If that also didn't work, just return a path relative to the editor
            if (work_dir == null)
                return "Modules";

            return Path.Combine(work_dir, "Modules");
        }

        /// <summary>
        /// Tries to locate a binary furball file given only a short filename. To be used for loading dependency modules.
        /// </summary>
        /// <param name="parentFolder">Absolute path of the folder where the base project file resides.</param>
        /// <param name="fileNameHint">The file name hint of the dependency, e.g. 'Foo.furball'</param>
        private string FindBinaryModuleFile(string parentFolder, string fileNameHint)
        {
            // Try locating the dependency in the same folder as the project itself
            string path = Path.Combine(parentFolder, fileNameHint);
            if (File.Exists(path))
                return path;

            // Try the editor's output Modules folder
            path = Path.Combine(GetModulesFolder(), fileNameHint);
            if (File.Exists(path))
                return path;

            // Try the Modules folder in the game folder
            path = Path.Combine(EditorPreferences.ExecutablePath, "Modules", fileNameHint);
            if (File.Exists(path))
                return path;

            // Otherwise, don't know
            return null;
        }

        private string GetUniqueAssetName(string prefix)
        {
            // Keep trying increasing number suffixes until we find one that does not match any loaded asset
            var num = 1;
            string name;
            do
            {
                name = $"{prefix}{num}";
                num++;
            } while (!IsAssetNameUnique(name));

            return name;
        }

        private bool IsAssetNameUnique(string name)
        {
            return Program.ActiveFurball.GetAssetByName(name) == null && Program.ActiveDependencies.GetAssetByName(name) == null;
        }

        private TreeNode FindAssetTreeNodeInGroup(TreeNodeCollection collection, AssetBase asset)
        {
            foreach (TreeNode node in collection)
                if (node.Tag == asset)
                    return node;

            return null;
        }

        private TreeNode FindAssetTreeNode(AssetBase asset)
        {
            return FindAssetTreeNodeInGroup(m_NodeCreatures.Nodes, asset)
                ?? FindAssetTreeNodeInGroup(m_NodeItems.Nodes, asset)
                ?? FindAssetTreeNodeInGroup(m_NodeJournals.Nodes, asset)
                ?? FindAssetTreeNodeInGroup(m_NodeScenes.Nodes, asset)
                ?? FindAssetTreeNodeInGroup(m_NodeScripts.Nodes, asset)
                ?? FindAssetTreeNodeInGroup(m_NodeTexts.Nodes, asset);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Prepare an empty project
            rbtProjNew_Click(sender, e);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_Dirty)
                return;

            // Ask the user to save changes, and cancel the close event accordingly
            DialogResult dr = MessageBox.Show("Would you like to save unsaved changes?", "Finmer Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.Cancel)
                e.Cancel = true;
            if (dr == DialogResult.Yes && !Save())
                e.Cancel = true;
        }

        private void rbtProjNew_Click(object sender, EventArgs e)
        {
            if (!TrySaveUnsavedChanges())
                return;

            // Reset the UI state
            ClearUI();

            // Prepare an empty project
            Program.LoadedContent = new EditorContentStore();
            Program.ActiveFurball = new Furball
            {
                Metadata = new FurballMetadata
                {
                    ID = Guid.NewGuid(),
                    Title = "Untitled",
                    Author = "A Snack",
                    FormatVersion = FurballFileDevice.k_LatestVersion
                }
            };
            Program.ActiveDependencies = new Furball();

            m_Filename = null;
            UpdateWindowTitle();
        }

        private void rbtProjOpen_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() != DialogResult.OK)
                return;

            OpenProject(dlgOpen.FileName);
        }

        private void rbtProjSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void rbtProjSaveAs_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void rbtPakPublish_Click(object sender, EventArgs e)
        {
            try
            {
                // Get a file name to save to
                if (dlgSaveBinary.ShowDialog() != DialogResult.OK)
                    return;

                // Write the output furball
                var writer = new FurballFileDeviceBinary();
                writer.WriteModule(Program.ActiveFurball, new FileInfo(dlgSaveBinary.FileName));

                // Show dialog box to confirm success, since there is no other UI feedback
                MessageBox.Show($"Successfully exported furball file '{dlgSaveBinary.FileName}'", "Finmer Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FurballException ex)
            {
                MessageBox.Show("Failed to publish the module: " + ex, "Finmer Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rbtPakExtract_Click(object sender, EventArgs e)
        {
            try
            {
                // We're going to close this project, so confirm
                if (!TrySaveUnsavedChanges())
                    return;

                // Have the user pick both the file to open, and the new project file to save
                if (dlgOpenBinary.ShowDialog() != DialogResult.OK)
                    return;
                if (dlgSave.ShowDialog() != DialogResult.OK)
                    return;

                // Read the binary furball
                var reader = new FurballFileDeviceBinary();
                Furball furball = reader.ReadModule(new FileInfo(dlgOpenBinary.FileName));

                // Write the text project
                var writer = new FurballFileDeviceText();
                writer.WriteModule(furball, new FileInfo(dlgSave.FileName));

                // ... and open it again!
                OpenProject(dlgSave.FileName);
            }
            catch (FurballException ex)
            {
                MessageBox.Show("Failed to extract a project: " + ex, "Finmer Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rbtPakSettings_Click(object sender, EventArgs e)
        {
            const int k_PakSettingsKey = 0;

            // If settings window is already open, focus the window instead of making a new one
            if (m_OpenWindows.TryGetValue(k_PakSettingsKey, out EditorWindow open_window))
            {
                open_window.Show();
                return;
            }

            // Otherwise, create a new one
            var window = new FormDocumentProject(Program.ActiveFurball);
            window.Closed += (o, args) => m_OpenWindows.Remove(k_PakSettingsKey);
            window.Show(dockPanel, DockState.Document);
            m_OpenWindows.Add(k_PakSettingsKey, window);
        }

        private void rbtPlayDev_Click(object sender, EventArgs e)
        {
            var info = new ProcessStartInfo
            {
                FileName = EditorPreferences.ExecutablePath,
                WorkingDirectory = EditorPreferences.ExecutableWorkingDirectory,
                Arguments = "-dev",
                UseShellExecute = false
            };

            try
            {
                Process.Start(info);
            }
            catch (Exception ex)
            {
                // Show a dialog box to the user informing them of the launch failure, but ignore it otherwise
                MessageBox.Show($"Could not launch the game: {ex.Message}\n\nPlease check the launch settings in the Editor Preferences.", "Finmer Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rbtPlayNormal_Click(object sender, EventArgs e)
        {
            var info = new ProcessStartInfo
            {
                FileName = EditorPreferences.ExecutablePath,
                WorkingDirectory = EditorPreferences.ExecutableWorkingDirectory,
                UseShellExecute = false
            };

            try
            {
                Process.Start(info);
            }
            catch (Exception ex)
            {
                // Show a dialog box to the user informing them of the launch failure, but ignore it otherwise
                MessageBox.Show($"Could not launch the game: {ex.Message}\n\nPlease check the launch settings in the Editor Preferences.", "Finmer Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rbtPreferences_Click(object sender, EventArgs e)
        {
            using (var window = new FormPreferences())
                window.ShowDialog();
        }

        private void rbtAssetAddScene_Click(object sender, EventArgs e)
        {
            var asset_guid = Guid.NewGuid();
            var asset = new AssetScene
            {
                Name = GetUniqueAssetName("NewScene"),
                ID = asset_guid,
                Root = new AssetScene.SceneNode
                {
                    NodeType = AssetScene.ENodeType.Root,
                    Key = "Root"
                }
            };
            RegisterNewAsset(asset);
        }

        private void rbtAssetAddItem_Click(object sender, EventArgs e)
        {
            var asset_guid = Guid.NewGuid();
            var asset = new AssetItem
            {
                Name = GetUniqueAssetName("NewItem"),
                ID = asset_guid
            };
            RegisterNewAsset(asset);
        }

        private void rbtAssetAddCreature_Click(object sender, EventArgs e)
        {
            var asset = new AssetCreature
            {
                Name = GetUniqueAssetName("NewCreature"),
                ID = Guid.NewGuid()
            };
            RegisterNewAsset(asset);
        }

        private void rbtAssetAddJournal_Click(object sender, EventArgs e)
        {
            var asset = new AssetJournal
            {
                Name = GetUniqueAssetName("NewJournal"),
                ID = Guid.NewGuid(),
                Title = "Untitled"
            };
            RegisterNewAsset(asset);
        }

        private void rbtAssetAddStringTable_Click(object sender, EventArgs e)
        {
            var asset = new AssetStringTable
            {
                Name = GetUniqueAssetName("NewStringTable"),
                ID = Guid.NewGuid()
            };
            RegisterNewAsset(asset);
        }

        private void rbtAssetAddScript_Click(object sender, EventArgs e)
        {
            var asset = new AssetScript
            {
                Name = GetUniqueAssetName("NewScript"),
                ID = Guid.NewGuid()
            };
            RegisterNewAsset(asset);
        }

        private void rbtAssetRename_Click(object sender, EventArgs e)
        {
            trvAssetList.SelectedNode.BeginEdit();
        }

        private void rbtAssetDelete_Click(object sender, EventArgs e)
        {
            TreeNode node = trvAssetList.SelectedNode;
            if (node?.Parent == null) return;

            var asset = (AssetBase)node.Tag;

            if (MessageBox.Show($"Are you sure you want to remove '{asset.Name}'? This operation cannot be undone.", "Finmer Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;

            // Close editor window for the asset, if it was still open
            if (m_OpenWindows.TryGetValue(GetEditorWindowKey(asset), out var open_window))
            {
                open_window.Dirty = false;
                open_window.Close();
            }

            // Remove asset from furball
            Program.ActiveFurball.Assets.Remove(asset);

            // Remove asset from tree view
            trvAssetList.SelectedNode.Remove();

            MarkDirty();
        }

        private void rbtHelpDoc_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"https://docs.finmer.dev/?utm_source=editor&utm_medium=about_dialog&utm_campaign=general");
            }
            catch (Exception)
            {
                // Ignore errors
            }
        }

        private void rbtHelpAbout_Click(object sender, EventArgs e)
        {
            using (var frm = new FormAbout())
            {
                frm.ShowDialog();
            }
        }

        private void trvAssetList_DoubleClick(object sender, EventArgs e)
        {
            TreeNode node = trvAssetList.SelectedNode;

            // Cannot open a root node, since they are not assets themselves
            if (node?.Parent == null)
                return;

            // Open the editor window for the clicked node
            var asset = (AssetBase)node.Tag;
            OpenEditorWindow(asset);
        }

        private void trvAssetList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Enable asset editing functions if this node actually represents an asset
            bool not_root = trvAssetList.SelectedNode?.Parent != null;
            rbtAssetRename.Enabled = not_root;
            rbtAssetDelete.Enabled = not_root;
        }

        private void trvAssetList_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // Disallow editing of the root nodes
            e.CancelEdit = e.Node.Parent == null;
        }

        private void trvAssetList_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // Discard empty names or names that are not unique
            if (String.IsNullOrWhiteSpace(e.Label) || !IsAssetNameUnique(e.Label))
            {
                e.CancelEdit = true;
                return;
            }

            // Update bookkeeping
            var asset = (AssetBase)e.Node.Tag;
            asset.Name = e.Label;
            e.Node.Text = e.Label;

            // If window was open, update the window title
            if (m_OpenWindows.TryGetValue(GetEditorWindowKey(asset), out var open_window))
                open_window.UpdateText();

            // Allow saving
            MarkDirty();
        }

    }

}
