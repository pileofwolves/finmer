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
using System.IO;
using System.Linq;
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
        private readonly TreeNode m_NodeFeats;
        private readonly TreeNode m_NodeItems;
        private readonly TreeNode m_NodeJournals;
        private readonly TreeNode m_NodeScenes;
        private readonly TreeNode m_NodeScripts;
        private readonly TreeNode m_NodeTexts;

        private readonly Dictionary<Guid, AssetWindow> m_OpenWindows = new Dictionary<Guid, AssetWindow>();

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
            m_NodeFeats = trvAssetList.Nodes.Add("Feats", "Feats", 0);
            m_NodeJournals = trvAssetList.Nodes.Add("Journals", "Journals", 0);
            m_NodeTexts = trvAssetList.Nodes.Add("String Tables", "String Tables", 0);
            m_NodeScripts = trvAssetList.Nodes.Add("Scripts", "Scripts", 0);
        }

        public void OpenAssetEditor(AssetBase asset)
        {
            // TODO: Prevent opening dependencies, since they cannot be edited while a different project is opened

            // If asset is already open, focus the window instead of making a new one
            if (m_OpenWindows.ContainsKey(asset.ID))
            {
                m_OpenWindows[asset.ID].Show();
                return;
            }

            // Create an appropriate editor window for the asset type
            AssetWindow window = CreateAssetWindow(asset);
            window.Asset = asset;
            window.Closed += (o, args) => m_OpenWindows.Remove(window.Asset.ID);

            // Dock and display the window
            window.Show(dockPanel, DockState.Document);
            m_OpenWindows.Add(asset.ID, window);
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

        private AssetWindow CreateAssetWindow(AssetBase asset)
        {
            switch (asset)
            {
                case AssetScene _:          return new FormDocumentScene();
                case AssetItem _:           return new FormDocumentItem();
                case AssetCreature _:       return new FormDocumentCreature();
                case AssetStringTable _:    return new FormDocumentStringTable();
                case AssetScript _:         return new FormDocumentScript();
                case AssetFeat _:           return new FormDocumentFeat();
                case AssetJournal _:        return new FormDocumentJournal();
                default:                    throw new ArgumentException(nameof(asset));
            }
        }

        private void UpdateWindowTitle()
        {
            Text = Program.ActiveFurball.Metadata.Title + " - Finmer Editor";
        }

        private void ClearUI()
        {
            // Close all windows
            List<AssetWindow> window_list_clone = m_OpenWindows.Values.ToList();
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
            m_NodeFeats.Nodes.Clear();
            m_NodeJournals.Nodes.Clear();

            // Disable saving UI
            m_Dirty = false;
            rbtProjSave.Enabled = false;
        }

        private void RegisterNewAsset(AssetBase asset)
        {
            // add asset to treeview
            Program.ActiveFurball.Assets.Add(asset);
            AddAssetToList(asset, true);

            // allow the user to save the project
            MarkDirty();

            // also open the editor window for the new asset, as the user probably
            // wants to make changes in the new asset
            OpenAssetEditor(asset);
        }

        private void AddAssetToList(AssetBase asset, bool autoselect)
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
                case AssetFeat _:
                    node.ImageKey = "feat";
                    m_NodeFeats.Nodes.Add(node);
                    break;
                case AssetJournal _:
                    node.ImageKey = "text";
                    m_NodeJournals.Nodes.Add(node);
                    break;
                default:
                    throw new ArgumentException(nameof(asset));
            }

            // If requested, select it now
            if (autoselect)
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

            // Make sure editor windows commit any changes
            foreach (AssetWindow window in m_OpenWindows.Values)
                window.Flush();

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

        private bool SaveAs()
        {
            // Present a save dialog
            if (dlgSave.ShowDialog() != DialogResult.OK)
                return false;
                
            // Pull the directory we're saving in, see if there's any other finmer projects already there.
            string save_path = Path.GetDirectoryName(dlgSave.FileName);
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
            // Close the tree view to prevent the UI redrawing every change we make, which is extremely slow
            trvAssetList.CollapseAll();

            // Present a dialog for the duration of the load
            using (var loading_window = new FormLoadingProgress())
            {
                loading_window.Show(this);
                Application.DoEvents();

                var device = new FurballFileDeviceText();

                // load new data
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

                        // Load it using a binary file device (since it's suppposed to be in furball form)
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

                loading_window.SetLabel("Preparing editor...");

                // full cleanup
                ClearUI();
                UpdateWindowTitle();

                // show new assets in list
                loading_window.SetupProgress(Program.ActiveFurball.Assets.Count);
                Program.ActiveFurball.Assets.ForEach(asset =>
                {
                    loading_window.AddProgress();
                    AddAssetToList(asset, false);
                    Application.DoEvents();
                });

                loading_window.RequestClose();

                // Allow the module to be saved right from the get-go. This allows users to open a text project (fnproj) and then immediately
                // convert it to a binary module file (furball) without having to awkwardly get the editor to call MarkDirty().
                // This is NOT the same as calling MarkDirty here since we don't want the editor to actually ask "save unchanged changes?" etc.
                m_Filename = filename;
                rbtProjSave.Enabled = true;
            }

            trvAssetList.ExpandAll();
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
            Program.ActiveFurball = new Furball
            {
                Metadata = new FurballMetadata
                {
                    ID = Guid.NewGuid(),
                    Title = "Untitled",
                    Author = "A Snack"
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
            // If settings window is already open, focus the window instead of making a new one
            Guid module_id = Program.ActiveFurball.Metadata.ID;
            if (m_OpenWindows.ContainsKey(module_id))
            {
                m_OpenWindows[module_id].Show();
                return;
            }

            // Otherwise, create a new one
            var window = new FormDocumentProject(Program.ActiveFurball);
            window.Closed += (o, args) => m_OpenWindows.Remove(module_id);
            window.Show(dockPanel, DockState.Document);
            m_OpenWindows.Add(module_id, window);
        }

        private void rbtPlayDev_Click(object sender, EventArgs e)
        {
            var info = new ProcessStartInfo
            {
                FileName = EditorPreferences.ExecutablePath,
                WorkingDirectory = EditorPreferences.ExecutableWorkingDirectory,
                Arguments = "-debug",
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
                    Key = "Root",
                    Title = "Root"
                },
                ScriptCustom = new AssetScript
                {
                    ID = Guid.NewGuid(),
                    Name = asset_guid + "_CustomScript"
                },
                ScriptEnter = new AssetScript
                {
                    ID = Guid.NewGuid(),
                    Name = asset_guid + "_EnterScript"
                },
                ScriptLeave = new AssetScript
                {
                    ID = Guid.NewGuid(),
                    Name = asset_guid + "_LeaveScript"
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
                ID = asset_guid,
                UseScript = new AssetScript
                {
                    ID = Guid.NewGuid(),
                    Name = asset_guid + "_UseScript"
                }
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

        private void rbtAssetAddFeat_Click(object sender, EventArgs e)
        {
            var asset = new AssetFeat
            {
                Name = GetUniqueAssetName("NewFeat"),
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

            // close asset editor windows
            m_OpenWindows
                .Where(pair => pair.Key == asset.ID)
                .ForEach(pair =>
                {
                    pair.Value.Dirty = false; // prevent save popup
                    pair.Value.Close();
                });

            // remove from furball, and tree view
            Program.ActiveFurball.Assets.Remove(asset);
            trvAssetList.SelectedNode.Remove();

            MarkDirty();
        }

        private void rbtHelpDoc_Click(object sender, EventArgs e)
        {
            string doc_path = Path.Combine(GetModulesFolder(), "Docs", "Documentation.html");
            try
            {
                Process.Start(doc_path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the documentation pages: {ex.Message}\n\nThe documentation files are expected to reside under the Modules folder. Check that the game launch settings in the Editor Preferences are correct.",
                    "Finmer Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            OpenAssetEditor(asset);
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
            if (m_OpenWindows.ContainsKey(asset.ID))
                m_OpenWindows[asset.ID].UpdateText();

            // Allow saving
            MarkDirty();
        }

    }

}
