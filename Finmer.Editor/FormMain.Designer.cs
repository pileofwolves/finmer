using System.Windows.Forms;

namespace Finmer.Editor
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.assetImages = new System.Windows.Forms.ImageList(this.components);
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.ribbon = new System.Windows.Forms.Ribbon();
            this.rtabHome = new System.Windows.Forms.RibbonTab();
            this.rpnProject = new System.Windows.Forms.RibbonPanel();
            this.rbtProjOpen = new System.Windows.Forms.RibbonButton();
            this.rbtProjSave = new System.Windows.Forms.RibbonButton();
            this.rbtProjNew = new System.Windows.Forms.RibbonButton();
            this.rbtProjSaveAs = new System.Windows.Forms.RibbonButton();
            this.rpnPackage = new System.Windows.Forms.RibbonPanel();
            this.rbtPakPublish = new System.Windows.Forms.RibbonButton();
            this.rbtPakExtract = new System.Windows.Forms.RibbonButton();
            this.rbtPakSettings = new System.Windows.Forms.RibbonButton();
            this.rpnPlay = new System.Windows.Forms.RibbonPanel();
            this.rbtPlayDev = new System.Windows.Forms.RibbonButton();
            this.rbtPlayNormal = new System.Windows.Forms.RibbonButton();
            this.rbtPreferences = new System.Windows.Forms.RibbonButton();
            this.rpnAssets = new System.Windows.Forms.RibbonPanel();
            this.rbtAssetAdd = new System.Windows.Forms.RibbonButton();
            this.rbtAssetAddScene = new System.Windows.Forms.RibbonButton();
            this.rbtAssetAddItem = new System.Windows.Forms.RibbonButton();
            this.rbtAssetAddCreature = new System.Windows.Forms.RibbonButton();
            this.rbtAssetAddJournal = new System.Windows.Forms.RibbonButton();
            this.rbtAssetAddStringTable = new System.Windows.Forms.RibbonButton();
            this.rbtAssetAddScript = new System.Windows.Forms.RibbonButton();
            this.rbtAssetRename = new System.Windows.Forms.RibbonButton();
            this.rbtAssetDelete = new System.Windows.Forms.RibbonButton();
            this.rpnHelp = new System.Windows.Forms.RibbonPanel();
            this.rbtHelpDoc = new System.Windows.Forms.RibbonButton();
            this.rbtHelpAbout = new System.Windows.Forms.RibbonButton();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.trvAssetList = new System.Windows.Forms.TreeView();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.dlgOpenBinary = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveBinary = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // assetImages
            // 
            this.assetImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("assetImages.ImageStream")));
            this.assetImages.TransparentColor = System.Drawing.Color.Transparent;
            this.assetImages.Images.SetKeyName(0, "folder");
            this.assetImages.Images.SetKeyName(1, "folder_open");
            this.assetImages.Images.SetKeyName(2, "scene");
            this.assetImages.Images.SetKeyName(3, "scene_start");
            this.assetImages.Images.SetKeyName(4, "scene_patch");
            this.assetImages.Images.SetKeyName(5, "item");
            this.assetImages.Images.SetKeyName(6, "creature");
            this.assetImages.Images.SetKeyName(7, "text");
            this.assetImages.Images.SetKeyName(8, "script");
            this.assetImages.Images.SetKeyName(9, "feat");
            // 
            // dlgSave
            // 
            this.dlgSave.Filter = "Finmer Modules (*.fnproj)|*.fnproj";
            // 
            // dlgOpen
            // 
            this.dlgOpen.Filter = "Finmer Modules (*.fnproj)|*.fnproj";
            // 
            // ribbon
            // 
            this.ribbon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.Minimized = false;
            this.ribbon.Name = "ribbon";
            // 
            // 
            // 
            this.ribbon.OrbDropDown.BorderRoundness = 8;
            this.ribbon.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon.OrbDropDown.Name = "";
            this.ribbon.OrbDropDown.TabIndex = 0;
            this.ribbon.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2013;
            this.ribbon.OrbText = "Finmer Editor";
            this.ribbon.OrbVisible = false;
            // 
            // 
            // 
            this.ribbon.QuickAccessToolbar.Visible = false;
            this.ribbon.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.ribbon.Size = new System.Drawing.Size(1008, 144);
            this.ribbon.TabIndex = 3;
            this.ribbon.Tabs.Add(this.rtabHome);
            this.ribbon.TabSpacing = 0;
            this.ribbon.Text = "Ribbon";
            this.ribbon.ThemeColor = System.Windows.Forms.RibbonTheme.Black;
            this.ribbon.UseAlwaysStandardTheme = true;
            // 
            // rtabHome
            // 
            this.rtabHome.Name = "rtabHome";
            this.rtabHome.Panels.Add(this.rpnProject);
            this.rtabHome.Panels.Add(this.rpnPackage);
            this.rtabHome.Panels.Add(this.rpnPlay);
            this.rtabHome.Panels.Add(this.rpnAssets);
            this.rtabHome.Panels.Add(this.rpnHelp);
            this.rtabHome.Text = "Home";
            // 
            // rpnProject
            // 
            this.rpnProject.ButtonMoreEnabled = false;
            this.rpnProject.ButtonMoreVisible = false;
            this.rpnProject.Items.Add(this.rbtProjOpen);
            this.rpnProject.Items.Add(this.rbtProjSave);
            this.rpnProject.Items.Add(this.rbtProjNew);
            this.rpnProject.Items.Add(this.rbtProjSaveAs);
            this.rpnProject.Name = "rpnProject";
            this.rpnProject.Text = "Project";
            // 
            // rbtProjOpen
            // 
            this.rbtProjOpen.AltKey = "O";
            this.rbtProjOpen.Image = global::Finmer.Editor.Properties.Resources.folder_horizontal_32;
            this.rbtProjOpen.LargeImage = global::Finmer.Editor.Properties.Resources.folder_horizontal_32;
            this.rbtProjOpen.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.rbtProjOpen.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.rbtProjOpen.Name = "rbtProjOpen";
            this.rbtProjOpen.SmallImage = global::Finmer.Editor.Properties.Resources.folder_horizontal_open;
            this.rbtProjOpen.Text = "&Open Module";
            this.rbtProjOpen.ToolTip = "Open a module from disk.";
            this.rbtProjOpen.ToolTipTitle = "Open Furball (Ctrl+O)";
            this.rbtProjOpen.Click += new System.EventHandler(this.rbtProjOpen_Click);
            // 
            // rbtProjSave
            // 
            this.rbtProjSave.AltKey = "S";
            this.rbtProjSave.Image = global::Finmer.Editor.Properties.Resources.disk_32;
            this.rbtProjSave.LargeImage = global::Finmer.Editor.Properties.Resources.disk_32;
            this.rbtProjSave.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.rbtProjSave.Name = "rbtProjSave";
            this.rbtProjSave.SmallImage = global::Finmer.Editor.Properties.Resources.disk;
            this.rbtProjSave.Text = "&Save Module";
            this.rbtProjSave.ToolTip = "Save pending project changes to disk.";
            this.rbtProjSave.ToolTipTitle = "Save Furball (Ctrl+S)";
            this.rbtProjSave.Click += new System.EventHandler(this.rbtProjSave_Click);
            // 
            // rbtProjNew
            // 
            this.rbtProjNew.AltKey = "N";
            this.rbtProjNew.Image = ((System.Drawing.Image)(resources.GetObject("rbtProjNew.Image")));
            this.rbtProjNew.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtProjNew.LargeImage")));
            this.rbtProjNew.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbtProjNew.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.rbtProjNew.Name = "rbtProjNew";
            this.rbtProjNew.SmallImage = global::Finmer.Editor.Properties.Resources.document;
            this.rbtProjNew.Text = "New Module";
            this.rbtProjNew.ToolTip = "Clears the asset list and opens a new, empty module file.";
            this.rbtProjNew.ToolTipTitle = "New Furball";
            this.rbtProjNew.Click += new System.EventHandler(this.rbtProjNew_Click);
            // 
            // rbtProjSaveAs
            // 
            this.rbtProjSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("rbtProjSaveAs.Image")));
            this.rbtProjSaveAs.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtProjSaveAs.LargeImage")));
            this.rbtProjSaveAs.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbtProjSaveAs.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.rbtProjSaveAs.Name = "rbtProjSaveAs";
            this.rbtProjSaveAs.SmallImage = global::Finmer.Editor.Properties.Resources.disks;
            this.rbtProjSaveAs.Text = "Save As";
            this.rbtProjSaveAs.ToolTip = "Save this module with a different name and/or location.";
            this.rbtProjSaveAs.ToolTipTitle = "Save As";
            this.rbtProjSaveAs.Click += new System.EventHandler(this.rbtProjSaveAs_Click);
            // 
            // rpnPackage
            // 
            this.rpnPackage.ButtonMoreEnabled = false;
            this.rpnPackage.ButtonMoreVisible = false;
            this.rpnPackage.Items.Add(this.rbtPakPublish);
            this.rpnPackage.Items.Add(this.rbtPakExtract);
            this.rpnPackage.Items.Add(this.rbtPakSettings);
            this.rpnPackage.Name = "rpnPackage";
            this.rpnPackage.Text = "Package";
            // 
            // rbtPakPublish
            // 
            this.rbtPakPublish.Image = ((System.Drawing.Image)(resources.GetObject("rbtPakPublish.Image")));
            this.rbtPakPublish.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtPakPublish.LargeImage")));
            this.rbtPakPublish.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbtPakPublish.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.rbtPakPublish.Name = "rbtPakPublish";
            this.rbtPakPublish.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbtPakPublish.SmallImage")));
            this.rbtPakPublish.Text = "Publish Furball";
            this.rbtPakPublish.ToolTip = "Save a compact, standalone module file (furball) that is ready-made for sharing w" +
    "ith others.";
            this.rbtPakPublish.ToolTipTitle = "Publish Furball";
            this.rbtPakPublish.Click += new System.EventHandler(this.rbtPakPublish_Click);
            // 
            // rbtPakExtract
            // 
            this.rbtPakExtract.Image = ((System.Drawing.Image)(resources.GetObject("rbtPakExtract.Image")));
            this.rbtPakExtract.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtPakExtract.LargeImage")));
            this.rbtPakExtract.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbtPakExtract.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.rbtPakExtract.Name = "rbtPakExtract";
            this.rbtPakExtract.SmallImage = global::Finmer.Editor.Properties.Resources.folder_import;
            this.rbtPakExtract.Text = "Extract Furball";
            this.rbtPakExtract.ToolTip = "Convert a compact, standalone module file (furball) into an extracted collection " +
    "of project files, and open the generated project in the Editor.";
            this.rbtPakExtract.ToolTipTitle = "Extract Furball";
            this.rbtPakExtract.Click += new System.EventHandler(this.rbtPakExtract_Click);
            // 
            // rbtPakSettings
            // 
            this.rbtPakSettings.Image = ((System.Drawing.Image)(resources.GetObject("rbtPakSettings.Image")));
            this.rbtPakSettings.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtPakSettings.LargeImage")));
            this.rbtPakSettings.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbtPakSettings.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.rbtPakSettings.Name = "rbtPakSettings";
            this.rbtPakSettings.SmallImage = global::Finmer.Editor.Properties.Resources.tags_label;
            this.rbtPakSettings.Text = "Module Settings";
            this.rbtPakSettings.ToolTip = "Change various metadata settings regarding the module that is currently opened.";
            this.rbtPakSettings.ToolTipTitle = "Edit Module Settings";
            this.rbtPakSettings.Click += new System.EventHandler(this.rbtPakSettings_Click);
            // 
            // rpnPlay
            // 
            this.rpnPlay.ButtonMoreEnabled = false;
            this.rpnPlay.ButtonMoreVisible = false;
            this.rpnPlay.Items.Add(this.rbtPlayDev);
            this.rpnPlay.Items.Add(this.rbtPlayNormal);
            this.rpnPlay.Items.Add(this.rbtPreferences);
            this.rpnPlay.Name = "rpnPlay";
            this.rpnPlay.Text = "Play";
            // 
            // rbtPlayDev
            // 
            this.rbtPlayDev.Image = ((System.Drawing.Image)(resources.GetObject("rbtPlayDev.Image")));
            this.rbtPlayDev.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtPlayDev.LargeImage")));
            this.rbtPlayDev.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbtPlayDev.Name = "rbtPlayDev";
            this.rbtPlayDev.SmallImage = global::Finmer.Editor.Properties.Resources.bug__play;
            this.rbtPlayDev.Text = "Launch Dev Mode";
            this.rbtPlayDev.ToolTip = "Launches Finmer with a development console enabled, where you can input arbitrary" +
    " Lua code to cheat or change things as required.";
            this.rbtPlayDev.ToolTipTitle = "Launch Dev Mode (F5)";
            this.rbtPlayDev.Click += new System.EventHandler(this.rbtPlayDev_Click);
            // 
            // rbtPlayNormal
            // 
            this.rbtPlayNormal.Image = ((System.Drawing.Image)(resources.GetObject("rbtPlayNormal.Image")));
            this.rbtPlayNormal.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtPlayNormal.LargeImage")));
            this.rbtPlayNormal.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbtPlayNormal.Name = "rbtPlayNormal";
            this.rbtPlayNormal.SmallImage = global::Finmer.Editor.Properties.Resources.control;
            this.rbtPlayNormal.Text = "Launch Normally";
            this.rbtPlayNormal.ToolTip = "Launches Finmer.";
            this.rbtPlayNormal.ToolTipTitle = "Launch Normally (Ctrl+F5)";
            this.rbtPlayNormal.Click += new System.EventHandler(this.rbtPlayNormal_Click);
            // 
            // rbtPreferences
            // 
            this.rbtPreferences.Image = ((System.Drawing.Image)(resources.GetObject("rbtPreferences.Image")));
            this.rbtPreferences.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtPreferences.LargeImage")));
            this.rbtPreferences.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbtPreferences.Name = "rbtPreferences";
            this.rbtPreferences.SmallImage = global::Finmer.Editor.Properties.Resources.wrench_screwdriver;
            this.rbtPreferences.Text = "Preferences";
            this.rbtPreferences.ToolTip = "Change editor settings.";
            this.rbtPreferences.ToolTipTitle = "Editor Preferences";
            this.rbtPreferences.Click += new System.EventHandler(this.rbtPreferences_Click);
            // 
            // rpnAssets
            // 
            this.rpnAssets.ButtonMoreEnabled = false;
            this.rpnAssets.ButtonMoreVisible = false;
            this.rpnAssets.Items.Add(this.rbtAssetAdd);
            this.rpnAssets.Items.Add(this.rbtAssetRename);
            this.rpnAssets.Items.Add(this.rbtAssetDelete);
            this.rpnAssets.Name = "rpnAssets";
            this.rpnAssets.Text = "Assets";
            // 
            // rbtAssetAdd
            // 
            this.rbtAssetAdd.DropDownItems.Add(this.rbtAssetAddScene);
            this.rbtAssetAdd.DropDownItems.Add(this.rbtAssetAddItem);
            this.rbtAssetAdd.DropDownItems.Add(this.rbtAssetAddCreature);
            this.rbtAssetAdd.DropDownItems.Add(this.rbtAssetAddJournal);
            this.rbtAssetAdd.DropDownItems.Add(this.rbtAssetAddStringTable);
            this.rbtAssetAdd.DropDownItems.Add(this.rbtAssetAddScript);
            this.rbtAssetAdd.Image = global::Finmer.Editor.Properties.Resources.document__plus_32;
            this.rbtAssetAdd.LargeImage = global::Finmer.Editor.Properties.Resources.document__plus_32;
            this.rbtAssetAdd.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.rbtAssetAdd.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.rbtAssetAdd.Name = "rbtAssetAdd";
            this.rbtAssetAdd.SmallImage = global::Finmer.Editor.Properties.Resources.document__plus;
            this.rbtAssetAdd.Style = System.Windows.Forms.RibbonButtonStyle.DropDown;
            this.rbtAssetAdd.Text = "Add Asset";
            this.rbtAssetAdd.ToolTip = "Create and add a new asset to this furball.";
            this.rbtAssetAdd.ToolTipTitle = "Add Asset";
            // 
            // rbtAssetAddScene
            // 
            this.rbtAssetAddScene.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.rbtAssetAddScene.Image = ((System.Drawing.Image)(resources.GetObject("rbtAssetAddScene.Image")));
            this.rbtAssetAddScene.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtAssetAddScene.LargeImage")));
            this.rbtAssetAddScene.Name = "rbtAssetAddScene";
            this.rbtAssetAddScene.SmallImage = global::Finmer.Editor.Properties.Resources.home;
            this.rbtAssetAddScene.Text = "Scene";
            this.rbtAssetAddScene.ToolTip = "An in-game location or dialogue tree, scripted with Lua code.";
            this.rbtAssetAddScene.ToolTipTitle = "Scene";
            this.rbtAssetAddScene.Click += new System.EventHandler(this.rbtAssetAddScene_Click);
            // 
            // rbtAssetAddItem
            // 
            this.rbtAssetAddItem.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.rbtAssetAddItem.Image = ((System.Drawing.Image)(resources.GetObject("rbtAssetAddItem.Image")));
            this.rbtAssetAddItem.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtAssetAddItem.LargeImage")));
            this.rbtAssetAddItem.Name = "rbtAssetAddItem";
            this.rbtAssetAddItem.SmallImage = global::Finmer.Editor.Properties.Resources.t_shirt_gray;
            this.rbtAssetAddItem.Text = "Item";
            this.rbtAssetAddItem.ToolTip = "An inventory item or equipable item.";
            this.rbtAssetAddItem.ToolTipTitle = "Item";
            this.rbtAssetAddItem.Click += new System.EventHandler(this.rbtAssetAddItem_Click);
            // 
            // rbtAssetAddCreature
            // 
            this.rbtAssetAddCreature.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.rbtAssetAddCreature.Image = ((System.Drawing.Image)(resources.GetObject("rbtAssetAddCreature.Image")));
            this.rbtAssetAddCreature.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtAssetAddCreature.LargeImage")));
            this.rbtAssetAddCreature.Name = "rbtAssetAddCreature";
            this.rbtAssetAddCreature.SmallImage = global::Finmer.Editor.Properties.Resources.animal_dog;
            this.rbtAssetAddCreature.Text = "Creature";
            this.rbtAssetAddCreature.ToolTip = "An object that can participate in the combat system.";
            this.rbtAssetAddCreature.ToolTipTitle = "Creature";
            this.rbtAssetAddCreature.Click += new System.EventHandler(this.rbtAssetAddCreature_Click);
            // 
            // rbtAssetAddJournal
            // 
            this.rbtAssetAddJournal.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.rbtAssetAddJournal.Image = ((System.Drawing.Image)(resources.GetObject("rbtAssetAddJournal.Image")));
            this.rbtAssetAddJournal.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtAssetAddJournal.LargeImage")));
            this.rbtAssetAddJournal.Name = "rbtAssetAddJournal";
            this.rbtAssetAddJournal.SmallImage = global::Finmer.Editor.Properties.Resources.book;
            this.rbtAssetAddJournal.Text = "Journal";
            this.rbtAssetAddJournal.ToolTip = "A chain of journal entries belonging to a single quest.";
            this.rbtAssetAddJournal.ToolTipTitle = "Journal";
            this.rbtAssetAddJournal.Click += new System.EventHandler(this.rbtAssetAddJournal_Click);
            // 
            // rbtAssetAddStringTable
            // 
            this.rbtAssetAddStringTable.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.rbtAssetAddStringTable.Image = ((System.Drawing.Image)(resources.GetObject("rbtAssetAddStringTable.Image")));
            this.rbtAssetAddStringTable.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtAssetAddStringTable.LargeImage")));
            this.rbtAssetAddStringTable.Name = "rbtAssetAddStringTable";
            this.rbtAssetAddStringTable.SmallImage = global::Finmer.Editor.Properties.Resources.document_text;
            this.rbtAssetAddStringTable.Text = "String Table";
            this.rbtAssetAddStringTable.ToolTip = "A table where you can write long, randomizable and parameterized text strings, fo" +
    "r use in Scenes.";
            this.rbtAssetAddStringTable.ToolTipTitle = "String Table";
            this.rbtAssetAddStringTable.Click += new System.EventHandler(this.rbtAssetAddStringTable_Click);
            // 
            // rbtAssetAddScript
            // 
            this.rbtAssetAddScript.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.rbtAssetAddScript.Image = ((System.Drawing.Image)(resources.GetObject("rbtAssetAddScript.Image")));
            this.rbtAssetAddScript.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtAssetAddScript.LargeImage")));
            this.rbtAssetAddScript.Name = "rbtAssetAddScript";
            this.rbtAssetAddScript.SmallImage = global::Finmer.Editor.Properties.Resources.script_code;
            this.rbtAssetAddScript.Text = "Lua Script";
            this.rbtAssetAddScript.ToolTip = "A raw Lua code block, which will be loaded and executed when the game starts.";
            this.rbtAssetAddScript.ToolTipTitle = "Lua Script";
            this.rbtAssetAddScript.Click += new System.EventHandler(this.rbtAssetAddScript_Click);
            // 
            // rbtAssetRename
            // 
            this.rbtAssetRename.Enabled = false;
            this.rbtAssetRename.Image = ((System.Drawing.Image)(resources.GetObject("rbtAssetRename.Image")));
            this.rbtAssetRename.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtAssetRename.LargeImage")));
            this.rbtAssetRename.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbtAssetRename.Name = "rbtAssetRename";
            this.rbtAssetRename.SmallImage = global::Finmer.Editor.Properties.Resources.document_rename;
            this.rbtAssetRename.Text = "Rename";
            this.rbtAssetRename.ToolTip = "Change the name of an asset. Its GUID - used in save data - will not be affected." +
    "";
            this.rbtAssetRename.ToolTipTitle = "Rename Asset (F2)";
            this.rbtAssetRename.Click += new System.EventHandler(this.rbtAssetRename_Click);
            // 
            // rbtAssetDelete
            // 
            this.rbtAssetDelete.Enabled = false;
            this.rbtAssetDelete.Image = ((System.Drawing.Image)(resources.GetObject("rbtAssetDelete.Image")));
            this.rbtAssetDelete.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtAssetDelete.LargeImage")));
            this.rbtAssetDelete.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbtAssetDelete.Name = "rbtAssetDelete";
            this.rbtAssetDelete.SmallImage = global::Finmer.Editor.Properties.Resources.cross_script;
            this.rbtAssetDelete.Text = "Delete";
            this.rbtAssetDelete.ToolTip = "Remove this asset from the project.";
            this.rbtAssetDelete.ToolTipTitle = "Delete Asset (Del)";
            this.rbtAssetDelete.Click += new System.EventHandler(this.rbtAssetDelete_Click);
            // 
            // rpnHelp
            // 
            this.rpnHelp.ButtonMoreEnabled = false;
            this.rpnHelp.ButtonMoreVisible = false;
            this.rpnHelp.Items.Add(this.rbtHelpDoc);
            this.rpnHelp.Items.Add(this.rbtHelpAbout);
            this.rpnHelp.Name = "rpnHelp";
            this.rpnHelp.Text = "Help";
            // 
            // rbtHelpDoc
            // 
            this.rbtHelpDoc.Image = ((System.Drawing.Image)(resources.GetObject("rbtHelpDoc.Image")));
            this.rbtHelpDoc.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtHelpDoc.LargeImage")));
            this.rbtHelpDoc.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbtHelpDoc.Name = "rbtHelpDoc";
            this.rbtHelpDoc.SmallImage = global::Finmer.Editor.Properties.Resources.book;
            this.rbtHelpDoc.Text = "Documentation";
            this.rbtHelpDoc.ToolTip = "Opens the included editor documentation in your web browser.";
            this.rbtHelpDoc.ToolTipTitle = "Open Documentation (F1)";
            this.rbtHelpDoc.Click += new System.EventHandler(this.rbtHelpDoc_Click);
            // 
            // rbtHelpAbout
            // 
            this.rbtHelpAbout.Image = ((System.Drawing.Image)(resources.GetObject("rbtHelpAbout.Image")));
            this.rbtHelpAbout.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbtHelpAbout.LargeImage")));
            this.rbtHelpAbout.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbtHelpAbout.Name = "rbtHelpAbout";
            this.rbtHelpAbout.SmallImage = global::Finmer.Editor.Properties.Resources.information_frame;
            this.rbtHelpAbout.Text = "About Finmer";
            this.rbtHelpAbout.ToolTip = "Show information about the Finmer game, such as a version number, copyrights, and" +
    " contact info.";
            this.rbtHelpAbout.ToolTipTitle = "About Finmer";
            this.rbtHelpAbout.Click += new System.EventHandler(this.rbtHelpAbout_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 144);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer.Panel1.Controls.Add(this.trvAssetList);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.dockPanel);
            this.splitContainer.Size = new System.Drawing.Size(1008, 537);
            this.splitContainer.SplitterDistance = 270;
            this.splitContainer.TabIndex = 4;
            // 
            // trvAssetList
            // 
            this.trvAssetList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvAssetList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvAssetList.FullRowSelect = true;
            this.trvAssetList.HideSelection = false;
            this.trvAssetList.ImageIndex = 0;
            this.trvAssetList.ImageList = this.assetImages;
            this.trvAssetList.LabelEdit = true;
            this.trvAssetList.Location = new System.Drawing.Point(0, 4);
            this.trvAssetList.Name = "trvAssetList";
            this.trvAssetList.SelectedImageIndex = 0;
            this.trvAssetList.Size = new System.Drawing.Size(270, 533);
            this.trvAssetList.TabIndex = 0;
            this.trvAssetList.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.trvAssetList_BeforeLabelEdit);
            this.trvAssetList.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.trvAssetList_AfterLabelEdit);
            this.trvAssetList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvAssetList_AfterSelect);
            this.trvAssetList.DoubleClick += new System.EventHandler(this.trvAssetList_DoubleClick);
            // 
            // dockPanel
            // 
            this.dockPanel.AllowEndUserNestedDocking = false;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel.Location = new System.Drawing.Point(0, 0);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(734, 537);
            this.dockPanel.TabIndex = 0;
            // 
            // dlgOpenBinary
            // 
            this.dlgOpenBinary.Filter = "Furball Packages (*.furball)|*.furball";
            // 
            // dlgSaveBinary
            // 
            this.dlgSaveBinary.Filter = "Furball Packages (*.furball)|*.furball";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 681);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.Text = "Finmer Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ImageList assetImages;
		private System.Windows.Forms.SaveFileDialog dlgSave;
		private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.Ribbon ribbon;
        private System.Windows.Forms.RibbonTab rtabHome;
        private RibbonPanel rpnProject;
        private RibbonButton rbtProjOpen;
        private SplitContainer splitContainer;
        private TreeView trvAssetList;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private RibbonPanel rpnPlay;
        private RibbonPanel rpnAssets;
        private RibbonPanel rpnHelp;
        private RibbonButton rbtProjNew;
        private RibbonButton rbtProjSaveAs;
        private RibbonButton rbtPlayDev;
        private RibbonButton rbtPlayNormal;
        private RibbonButton rbtHelpDoc;
        private RibbonButton rbtHelpAbout;
        private RibbonButton rbtAssetAdd;
        private RibbonButton rbtAssetRename;
        private RibbonButton rbtAssetDelete;
        private RibbonButton rbtProjSave;
        private RibbonButton rbtAssetAddScene;
        private RibbonButton rbtAssetAddItem;
        private RibbonButton rbtAssetAddCreature;
        private RibbonButton rbtAssetAddJournal;
        private RibbonButton rbtAssetAddStringTable;
        private RibbonButton rbtAssetAddScript;
        private OpenFileDialog dlgOpenBinary;
        private RibbonPanel rpnPackage;
        private RibbonButton rbtPakPublish;
        private RibbonButton rbtPakExtract;
        private RibbonButton rbtPakSettings;
        private SaveFileDialog dlgSaveBinary;
        private RibbonButton rbtPreferences;
    }
}

