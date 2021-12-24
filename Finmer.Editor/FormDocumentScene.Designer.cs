namespace Finmer.Editor
{
	partial class FormDocumentScene
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDocumentScene));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbAddNode = new System.Windows.Forms.ToolStripButton();
            this.tsbAddLink = new System.Windows.Forms.ToolStripButton();
            this.tsbRemoveNode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tsbMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbScriptCustom = new System.Windows.Forms.ToolStripButton();
            this.tsbScriptEnter = new System.Windows.Forms.ToolStripButton();
            this.tsbScriptLeave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExpand = new System.Windows.Forms.ToolStripButton();
            this.tsbCollapse = new System.Windows.Forms.ToolStripButton();
            this.splitNodeList = new System.Windows.Forms.SplitContainer();
            this.trvNodes = new System.Windows.Forms.TreeView();
            this.imlNodeIcons = new System.Windows.Forms.ImageList(this.components);
            this.splitNodeSettings = new System.Windows.Forms.SplitContainer();
            this.pnlRootNodeSettings = new System.Windows.Forms.Panel();
            this.pnlInjectionSettings = new System.Windows.Forms.Panel();
            this.cmbInjectTargetMode = new System.Windows.Forms.ComboBox();
            this.cmbInjectTargetNode = new System.Windows.Forms.ComboBox();
            this.lblInjectTargetNode = new System.Windows.Forms.Label();
            this.lblInjectTargetScene = new System.Windows.Forms.Label();
            this.chkRootInject = new System.Windows.Forms.CheckBox();
            this.lblRootInfo = new System.Windows.Forms.Label();
            this.pnlGeneralNodeSettings = new System.Windows.Forms.Panel();
            this.pnlChoiceNodeSettings = new System.Windows.Forms.Panel();
            this.nudCustomWidth = new System.Windows.Forms.NumericUpDown();
            this.chkChoiceHighlight = new System.Windows.Forms.CheckBox();
            this.chkCustomWidth = new System.Windows.Forms.CheckBox();
            this.txtNodeTitle = new System.Windows.Forms.TextBox();
            this.lblNodeTooltip = new System.Windows.Forms.Label();
            this.txtNodeTooltip = new System.Windows.Forms.TextBox();
            this.lblNodeTitle = new System.Windows.Forms.Label();
            this.optTypeNode = new System.Windows.Forms.RadioButton();
            this.txtNodeKey = new System.Windows.Forms.TextBox();
            this.optTypeLink = new System.Windows.Forms.RadioButton();
            this.lblNodeKey = new System.Windows.Forms.Label();
            this.lblLinkTarget = new System.Windows.Forms.Label();
            this.cmbLinkTarget = new System.Windows.Forms.ComboBox();
            this.scriptTabs = new System.Windows.Forms.TabControl();
            this.tbpScriptAction = new System.Windows.Forms.TabPage();
            this.scriptAction = new ScintillaNET.Scintilla();
            this.tbpScriptAppear = new System.Windows.Forms.TabPage();
            this.scriptAppear = new ScintillaNET.Scintilla();
            this.assetInjectTargetScene = new Finmer.Editor.AssetPickerControl();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitNodeList)).BeginInit();
            this.splitNodeList.Panel1.SuspendLayout();
            this.splitNodeList.Panel2.SuspendLayout();
            this.splitNodeList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitNodeSettings)).BeginInit();
            this.splitNodeSettings.Panel1.SuspendLayout();
            this.splitNodeSettings.Panel2.SuspendLayout();
            this.splitNodeSettings.SuspendLayout();
            this.pnlRootNodeSettings.SuspendLayout();
            this.pnlInjectionSettings.SuspendLayout();
            this.pnlGeneralNodeSettings.SuspendLayout();
            this.pnlChoiceNodeSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCustomWidth)).BeginInit();
            this.scriptTabs.SuspendLayout();
            this.tbpScriptAction.SuspendLayout();
            this.tbpScriptAppear.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddNode,
            this.tsbAddLink,
            this.tsbRemoveNode,
            this.toolStripSeparator1,
            this.tsbMoveUp,
            this.tsbMoveDown,
            this.toolStripSeparator3,
            this.tsbScriptCustom,
            this.tsbScriptEnter,
            this.tsbScriptLeave,
            this.toolStripSeparator2,
            this.tsbExpand,
            this.tsbCollapse});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(688, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsbAddNode
            // 
            this.tsbAddNode.Image = global::Finmer.Editor.Properties.Resources.plus;
            this.tsbAddNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddNode.Name = "tsbAddNode";
            this.tsbAddNode.Size = new System.Drawing.Size(81, 22);
            this.tsbAddNode.Text = "Add Node";
            this.tsbAddNode.Click += new System.EventHandler(this.tsbAddNode_Click);
            // 
            // tsbAddLink
            // 
            this.tsbAddLink.Image = global::Finmer.Editor.Properties.Resources.chain;
            this.tsbAddLink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddLink.Name = "tsbAddLink";
            this.tsbAddLink.Size = new System.Drawing.Size(74, 22);
            this.tsbAddLink.Text = "Add Link";
            this.tsbAddLink.Click += new System.EventHandler(this.tsbAddLink_Click);
            // 
            // tsbRemoveNode
            // 
            this.tsbRemoveNode.Image = global::Finmer.Editor.Properties.Resources.minus;
            this.tsbRemoveNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemoveNode.Name = "tsbRemoveNode";
            this.tsbRemoveNode.Size = new System.Drawing.Size(102, 22);
            this.tsbRemoveNode.Text = "Remove Node";
            this.tsbRemoveNode.Click += new System.EventHandler(this.tsbRemoveNode_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbMoveUp
            // 
            this.tsbMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMoveUp.Image = global::Finmer.Editor.Properties.Resources.arrow_090;
            this.tsbMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveUp.Name = "tsbMoveUp";
            this.tsbMoveUp.Size = new System.Drawing.Size(23, 22);
            this.tsbMoveUp.Text = "Move Up";
            this.tsbMoveUp.Click += new System.EventHandler(this.tsbMoveUp_Click);
            // 
            // tsbMoveDown
            // 
            this.tsbMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMoveDown.Image = global::Finmer.Editor.Properties.Resources.arrow_270;
            this.tsbMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveDown.Name = "tsbMoveDown";
            this.tsbMoveDown.Size = new System.Drawing.Size(23, 22);
            this.tsbMoveDown.Text = "Move Down";
            this.tsbMoveDown.Click += new System.EventHandler(this.tsbMoveDown_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbScriptCustom
            // 
            this.tsbScriptCustom.AutoToolTip = false;
            this.tsbScriptCustom.Image = global::Finmer.Editor.Properties.Resources.script_code;
            this.tsbScriptCustom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbScriptCustom.Name = "tsbScriptCustom";
            this.tsbScriptCustom.Size = new System.Drawing.Size(99, 22);
            this.tsbScriptCustom.Text = "CustomScript";
            this.tsbScriptCustom.ToolTipText = "Edit Custom Script";
            this.tsbScriptCustom.Click += new System.EventHandler(this.tsbScriptCustom_Click);
            // 
            // tsbScriptEnter
            // 
            this.tsbScriptEnter.AutoToolTip = false;
            this.tsbScriptEnter.Image = global::Finmer.Editor.Properties.Resources.script_code;
            this.tsbScriptEnter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbScriptEnter.Name = "tsbScriptEnter";
            this.tsbScriptEnter.Size = new System.Drawing.Size(84, 22);
            this.tsbScriptEnter.Text = "EnterScript";
            this.tsbScriptEnter.ToolTipText = "Edit OnEnter Script";
            this.tsbScriptEnter.Click += new System.EventHandler(this.tsbScriptEnter_Click);
            // 
            // tsbScriptLeave
            // 
            this.tsbScriptLeave.AutoToolTip = false;
            this.tsbScriptLeave.Image = global::Finmer.Editor.Properties.Resources.script_code;
            this.tsbScriptLeave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbScriptLeave.Name = "tsbScriptLeave";
            this.tsbScriptLeave.Size = new System.Drawing.Size(87, 22);
            this.tsbScriptLeave.Text = "LeaveScript";
            this.tsbScriptLeave.ToolTipText = "Edit OnLeave Script";
            this.tsbScriptLeave.Click += new System.EventHandler(this.tsbScriptLeave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbExpand
            // 
            this.tsbExpand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExpand.Image = global::Finmer.Editor.Properties.Resources.arrow_out;
            this.tsbExpand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExpand.Name = "tsbExpand";
            this.tsbExpand.Size = new System.Drawing.Size(23, 22);
            this.tsbExpand.Text = "Expand All";
            this.tsbExpand.Click += new System.EventHandler(this.tsbExpand_Click);
            // 
            // tsbCollapse
            // 
            this.tsbCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCollapse.Image = global::Finmer.Editor.Properties.Resources.arrow_in;
            this.tsbCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCollapse.Name = "tsbCollapse";
            this.tsbCollapse.Size = new System.Drawing.Size(23, 22);
            this.tsbCollapse.Text = "Collapse All";
            this.tsbCollapse.Click += new System.EventHandler(this.tsbCollapse_Click);
            // 
            // splitNodeList
            // 
            this.splitNodeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitNodeList.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitNodeList.Location = new System.Drawing.Point(0, 25);
            this.splitNodeList.Name = "splitNodeList";
            this.splitNodeList.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitNodeList.Panel1
            // 
            this.splitNodeList.Panel1.Controls.Add(this.trvNodes);
            // 
            // splitNodeList.Panel2
            // 
            this.splitNodeList.Panel2.Controls.Add(this.splitNodeSettings);
            this.splitNodeList.Size = new System.Drawing.Size(688, 526);
            this.splitNodeList.SplitterDistance = 254;
            this.splitNodeList.TabIndex = 1;
            // 
            // trvNodes
            // 
            this.trvNodes.AllowDrop = true;
            this.trvNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvNodes.HideSelection = false;
            this.trvNodes.ImageIndex = 0;
            this.trvNodes.ImageList = this.imlNodeIcons;
            this.trvNodes.Location = new System.Drawing.Point(0, 0);
            this.trvNodes.Name = "trvNodes";
            this.trvNodes.SelectedImageIndex = 0;
            this.trvNodes.Size = new System.Drawing.Size(688, 254);
            this.trvNodes.TabIndex = 0;
            this.trvNodes.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.trvNodes_ItemDrag);
            this.trvNodes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvNodes_AfterSelect);
            this.trvNodes.DragDrop += new System.Windows.Forms.DragEventHandler(this.trvNodes_DragDrop);
            this.trvNodes.DragOver += new System.Windows.Forms.DragEventHandler(this.trvNodes_DragOver);
            this.trvNodes.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvNodes_NodeMouseDoubleClick);
            // 
            // imlNodeIcons
            // 
            this.imlNodeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlNodeIcons.ImageStream")));
            this.imlNodeIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imlNodeIcons.Images.SetKeyName(0, "node-link");
            this.imlNodeIcons.Images.SetKeyName(1, "node-option");
            this.imlNodeIcons.Images.SetKeyName(2, "node-option-alt");
            this.imlNodeIcons.Images.SetKeyName(3, "node-root");
            this.imlNodeIcons.Images.SetKeyName(4, "node-state");
            this.imlNodeIcons.Images.SetKeyName(5, "node-state-alt");
            // 
            // splitNodeSettings
            // 
            this.splitNodeSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitNodeSettings.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitNodeSettings.IsSplitterFixed = true;
            this.splitNodeSettings.Location = new System.Drawing.Point(0, 0);
            this.splitNodeSettings.Name = "splitNodeSettings";
            // 
            // splitNodeSettings.Panel1
            // 
            this.splitNodeSettings.Panel1.Controls.Add(this.pnlRootNodeSettings);
            this.splitNodeSettings.Panel1.Controls.Add(this.pnlGeneralNodeSettings);
            // 
            // splitNodeSettings.Panel2
            // 
            this.splitNodeSettings.Panel2.Controls.Add(this.scriptTabs);
            this.splitNodeSettings.Size = new System.Drawing.Size(688, 268);
            this.splitNodeSettings.SplitterDistance = 218;
            this.splitNodeSettings.TabIndex = 0;
            this.splitNodeSettings.Visible = false;
            // 
            // pnlRootNodeSettings
            // 
            this.pnlRootNodeSettings.Controls.Add(this.pnlInjectionSettings);
            this.pnlRootNodeSettings.Controls.Add(this.chkRootInject);
            this.pnlRootNodeSettings.Controls.Add(this.lblRootInfo);
            this.pnlRootNodeSettings.Location = new System.Drawing.Point(8, 6);
            this.pnlRootNodeSettings.Name = "pnlRootNodeSettings";
            this.pnlRootNodeSettings.Size = new System.Drawing.Size(200, 258);
            this.pnlRootNodeSettings.TabIndex = 10;
            this.pnlRootNodeSettings.Visible = false;
            // 
            // pnlInjectionSettings
            // 
            this.pnlInjectionSettings.Controls.Add(this.assetInjectTargetScene);
            this.pnlInjectionSettings.Controls.Add(this.cmbInjectTargetMode);
            this.pnlInjectionSettings.Controls.Add(this.cmbInjectTargetNode);
            this.pnlInjectionSettings.Controls.Add(this.lblInjectTargetNode);
            this.pnlInjectionSettings.Controls.Add(this.lblInjectTargetScene);
            this.pnlInjectionSettings.Location = new System.Drawing.Point(8, 72);
            this.pnlInjectionSettings.Name = "pnlInjectionSettings";
            this.pnlInjectionSettings.Size = new System.Drawing.Size(184, 152);
            this.pnlInjectionSettings.TabIndex = 3;
            this.pnlInjectionSettings.Visible = false;
            // 
            // cmbInjectTargetMode
            // 
            this.cmbInjectTargetMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInjectTargetMode.FormattingEnabled = true;
            this.cmbInjectTargetMode.Items.AddRange(new object[] {
            "before (as States)",
            "after (as States)",
            "inside, at the start (as Choices)",
            "inside, at the end (as Choices)"});
            this.cmbInjectTargetMode.Location = new System.Drawing.Point(0, 72);
            this.cmbInjectTargetMode.Name = "cmbInjectTargetMode";
            this.cmbInjectTargetMode.Size = new System.Drawing.Size(184, 21);
            this.cmbInjectTargetMode.TabIndex = 4;
            this.cmbInjectTargetMode.SelectedIndexChanged += new System.EventHandler(this.cmbInjectTargetMode_SelectedIndexChanged);
            // 
            // cmbInjectTargetNode
            // 
            this.cmbInjectTargetNode.FormattingEnabled = true;
            this.cmbInjectTargetNode.Location = new System.Drawing.Point(0, 96);
            this.cmbInjectTargetNode.Name = "cmbInjectTargetNode";
            this.cmbInjectTargetNode.Size = new System.Drawing.Size(184, 21);
            this.cmbInjectTargetNode.TabIndex = 5;
            this.cmbInjectTargetNode.TextChanged += new System.EventHandler(this.cmbInjectTargetNode_TextChanged);
            // 
            // lblInjectTargetNode
            // 
            this.lblInjectTargetNode.AutoSize = true;
            this.lblInjectTargetNode.Location = new System.Drawing.Point(0, 56);
            this.lblInjectTargetNode.Name = "lblInjectTargetNode";
            this.lblInjectTargetNode.Size = new System.Drawing.Size(77, 13);
            this.lblInjectTargetNode.TabIndex = 4;
            this.lblInjectTargetNode.Text = "Injection Point:";
            // 
            // lblInjectTargetScene
            // 
            this.lblInjectTargetScene.AutoSize = true;
            this.lblInjectTargetScene.Location = new System.Drawing.Point(0, 8);
            this.lblInjectTargetScene.Name = "lblInjectTargetScene";
            this.lblInjectTargetScene.Size = new System.Drawing.Size(75, 13);
            this.lblInjectTargetScene.TabIndex = 2;
            this.lblInjectTargetScene.Text = "Target Scene:";
            // 
            // chkRootInject
            // 
            this.chkRootInject.AutoSize = true;
            this.chkRootInject.Location = new System.Drawing.Point(8, 48);
            this.chkRootInject.Name = "chkRootInject";
            this.chkRootInject.Size = new System.Drawing.Size(145, 17);
            this.chkRootInject.TabIndex = 1;
            this.chkRootInject.Text = "Inject into another Scene";
            this.chkRootInject.UseVisualStyleBackColor = true;
            this.chkRootInject.CheckedChanged += new System.EventHandler(this.chkRootInject_CheckedChanged);
            // 
            // lblRootInfo
            // 
            this.lblRootInfo.Enabled = false;
            this.lblRootInfo.Location = new System.Drawing.Point(8, 8);
            this.lblRootInfo.Name = "lblRootInfo";
            this.lblRootInfo.Size = new System.Drawing.Size(184, 40);
            this.lblRootInfo.TabIndex = 0;
            this.lblRootInfo.Text = "The Root node is the starting point of the scene and can\'t be edited.";
            // 
            // pnlGeneralNodeSettings
            // 
            this.pnlGeneralNodeSettings.Controls.Add(this.pnlChoiceNodeSettings);
            this.pnlGeneralNodeSettings.Controls.Add(this.optTypeNode);
            this.pnlGeneralNodeSettings.Controls.Add(this.txtNodeKey);
            this.pnlGeneralNodeSettings.Controls.Add(this.optTypeLink);
            this.pnlGeneralNodeSettings.Controls.Add(this.lblNodeKey);
            this.pnlGeneralNodeSettings.Controls.Add(this.lblLinkTarget);
            this.pnlGeneralNodeSettings.Controls.Add(this.cmbLinkTarget);
            this.pnlGeneralNodeSettings.Location = new System.Drawing.Point(8, 8);
            this.pnlGeneralNodeSettings.Name = "pnlGeneralNodeSettings";
            this.pnlGeneralNodeSettings.Size = new System.Drawing.Size(200, 256);
            this.pnlGeneralNodeSettings.TabIndex = 11;
            this.pnlGeneralNodeSettings.Visible = false;
            // 
            // pnlChoiceNodeSettings
            // 
            this.pnlChoiceNodeSettings.Controls.Add(this.nudCustomWidth);
            this.pnlChoiceNodeSettings.Controls.Add(this.chkChoiceHighlight);
            this.pnlChoiceNodeSettings.Controls.Add(this.chkCustomWidth);
            this.pnlChoiceNodeSettings.Controls.Add(this.txtNodeTitle);
            this.pnlChoiceNodeSettings.Controls.Add(this.lblNodeTooltip);
            this.pnlChoiceNodeSettings.Controls.Add(this.txtNodeTooltip);
            this.pnlChoiceNodeSettings.Controls.Add(this.lblNodeTitle);
            this.pnlChoiceNodeSettings.Location = new System.Drawing.Point(0, 80);
            this.pnlChoiceNodeSettings.Name = "pnlChoiceNodeSettings";
            this.pnlChoiceNodeSettings.Size = new System.Drawing.Size(208, 168);
            this.pnlChoiceNodeSettings.TabIndex = 9;
            this.pnlChoiceNodeSettings.Visible = false;
            // 
            // nudCustomWidth
            // 
            this.nudCustomWidth.DecimalPlaces = 2;
            this.nudCustomWidth.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.nudCustomWidth.Location = new System.Drawing.Point(104, 136);
            this.nudCustomWidth.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            65536});
            this.nudCustomWidth.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.nudCustomWidth.Name = "nudCustomWidth";
            this.nudCustomWidth.Size = new System.Drawing.Size(72, 20);
            this.nudCustomWidth.TabIndex = 11;
            this.nudCustomWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.nudCustomWidth.Visible = false;
            this.nudCustomWidth.ValueChanged += new System.EventHandler(this.nudCustomWidth_ValueChanged);
            // 
            // chkChoiceHighlight
            // 
            this.chkChoiceHighlight.AutoSize = true;
            this.chkChoiceHighlight.Location = new System.Drawing.Point(8, 114);
            this.chkChoiceHighlight.Name = "chkChoiceHighlight";
            this.chkChoiceHighlight.Size = new System.Drawing.Size(67, 17);
            this.chkChoiceHighlight.TabIndex = 9;
            this.chkChoiceHighlight.Text = "Highlight";
            this.chkChoiceHighlight.UseVisualStyleBackColor = true;
            this.chkChoiceHighlight.CheckedChanged += new System.EventHandler(this.chkChoiceHighlight_CheckedChanged);
            // 
            // chkCustomWidth
            // 
            this.chkCustomWidth.AutoSize = true;
            this.chkCustomWidth.Location = new System.Drawing.Point(8, 138);
            this.chkCustomWidth.Name = "chkCustomWidth";
            this.chkCustomWidth.Size = new System.Drawing.Size(92, 17);
            this.chkCustomWidth.TabIndex = 10;
            this.chkCustomWidth.Text = "Custom Width";
            this.chkCustomWidth.UseVisualStyleBackColor = true;
            this.chkCustomWidth.CheckedChanged += new System.EventHandler(this.chkCustomWidth_CheckedChanged);
            // 
            // txtNodeTitle
            // 
            this.txtNodeTitle.Location = new System.Drawing.Point(8, 24);
            this.txtNodeTitle.MaxLength = 50;
            this.txtNodeTitle.Name = "txtNodeTitle";
            this.txtNodeTitle.Size = new System.Drawing.Size(192, 20);
            this.txtNodeTitle.TabIndex = 3;
            this.txtNodeTitle.TextChanged += new System.EventHandler(this.txtNodeTitle_TextChanged);
            // 
            // lblNodeTooltip
            // 
            this.lblNodeTooltip.AutoSize = true;
            this.lblNodeTooltip.Location = new System.Drawing.Point(8, 56);
            this.lblNodeTooltip.Name = "lblNodeTooltip";
            this.lblNodeTooltip.Size = new System.Drawing.Size(42, 13);
            this.lblNodeTooltip.TabIndex = 5;
            this.lblNodeTooltip.Text = "Tooltip:";
            // 
            // txtNodeTooltip
            // 
            this.txtNodeTooltip.Location = new System.Drawing.Point(8, 72);
            this.txtNodeTooltip.MaxLength = 200;
            this.txtNodeTooltip.Name = "txtNodeTooltip";
            this.txtNodeTooltip.Size = new System.Drawing.Size(192, 20);
            this.txtNodeTooltip.TabIndex = 4;
            this.txtNodeTooltip.TextChanged += new System.EventHandler(this.txtNodeTooltip_TextChanged);
            // 
            // lblNodeTitle
            // 
            this.lblNodeTitle.AutoSize = true;
            this.lblNodeTitle.Location = new System.Drawing.Point(8, 8);
            this.lblNodeTitle.Name = "lblNodeTitle";
            this.lblNodeTitle.Size = new System.Drawing.Size(65, 13);
            this.lblNodeTitle.TabIndex = 8;
            this.lblNodeTitle.Text = "Button Text:";
            // 
            // optTypeNode
            // 
            this.optTypeNode.AutoSize = true;
            this.optTypeNode.Location = new System.Drawing.Point(8, 8);
            this.optTypeNode.Name = "optTypeNode";
            this.optTypeNode.Size = new System.Drawing.Size(51, 17);
            this.optTypeNode.TabIndex = 0;
            this.optTypeNode.Text = "Node";
            this.optTypeNode.UseVisualStyleBackColor = true;
            this.optTypeNode.CheckedChanged += new System.EventHandler(this.optTypeNode_CheckedChanged);
            // 
            // txtNodeKey
            // 
            this.txtNodeKey.Location = new System.Drawing.Point(8, 56);
            this.txtNodeKey.MaxLength = 20;
            this.txtNodeKey.Name = "txtNodeKey";
            this.txtNodeKey.Size = new System.Drawing.Size(192, 20);
            this.txtNodeKey.TabIndex = 2;
            this.txtNodeKey.TextChanged += new System.EventHandler(this.txtNodeKey_TextChanged);
            this.txtNodeKey.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNodeKey_KeyPress);
            // 
            // optTypeLink
            // 
            this.optTypeLink.AutoSize = true;
            this.optTypeLink.Location = new System.Drawing.Point(104, 8);
            this.optTypeLink.Name = "optTypeLink";
            this.optTypeLink.Size = new System.Drawing.Size(45, 17);
            this.optTypeLink.TabIndex = 1;
            this.optTypeLink.Text = "Link";
            this.optTypeLink.UseVisualStyleBackColor = true;
            // 
            // lblNodeKey
            // 
            this.lblNodeKey.AutoSize = true;
            this.lblNodeKey.Location = new System.Drawing.Point(8, 40);
            this.lblNodeKey.Name = "lblNodeKey";
            this.lblNodeKey.Size = new System.Drawing.Size(65, 13);
            this.lblNodeKey.TabIndex = 7;
            this.lblNodeKey.Text = "Unique Key:";
            this.lblNodeKey.Visible = false;
            // 
            // lblLinkTarget
            // 
            this.lblLinkTarget.AutoSize = true;
            this.lblLinkTarget.Location = new System.Drawing.Point(8, 40);
            this.lblLinkTarget.Name = "lblLinkTarget";
            this.lblLinkTarget.Size = new System.Drawing.Size(93, 13);
            this.lblLinkTarget.TabIndex = 2;
            this.lblLinkTarget.Text = "Link Target Node:";
            this.lblLinkTarget.Visible = false;
            // 
            // cmbLinkTarget
            // 
            this.cmbLinkTarget.FormattingEnabled = true;
            this.cmbLinkTarget.Location = new System.Drawing.Point(8, 56);
            this.cmbLinkTarget.Name = "cmbLinkTarget";
            this.cmbLinkTarget.Size = new System.Drawing.Size(192, 21);
            this.cmbLinkTarget.TabIndex = 2;
            this.cmbLinkTarget.Visible = false;
            this.cmbLinkTarget.TextChanged += new System.EventHandler(this.cmbLinkTarget_TextChanged);
            // 
            // scriptTabs
            // 
            this.scriptTabs.Controls.Add(this.tbpScriptAction);
            this.scriptTabs.Controls.Add(this.tbpScriptAppear);
            this.scriptTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptTabs.Location = new System.Drawing.Point(0, 0);
            this.scriptTabs.Name = "scriptTabs";
            this.scriptTabs.SelectedIndex = 0;
            this.scriptTabs.Size = new System.Drawing.Size(466, 268);
            this.scriptTabs.TabIndex = 0;
            // 
            // tbpScriptAction
            // 
            this.tbpScriptAction.Controls.Add(this.scriptAction);
            this.tbpScriptAction.Location = new System.Drawing.Point(4, 22);
            this.tbpScriptAction.Name = "tbpScriptAction";
            this.tbpScriptAction.Padding = new System.Windows.Forms.Padding(3);
            this.tbpScriptAction.Size = new System.Drawing.Size(458, 242);
            this.tbpScriptAction.TabIndex = 0;
            this.tbpScriptAction.Text = "Actions Taken";
            this.tbpScriptAction.UseVisualStyleBackColor = true;
            // 
            // scriptAction
            // 
            this.scriptAction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scriptAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptAction.Location = new System.Drawing.Point(3, 3);
            this.scriptAction.Name = "scriptAction";
            this.scriptAction.Size = new System.Drawing.Size(452, 236);
            this.scriptAction.TabIndex = 1;
            this.scriptAction.TextChanged += new System.EventHandler(this.scriptAction_TextChanged);
            // 
            // tbpScriptAppear
            // 
            this.tbpScriptAppear.Controls.Add(this.scriptAppear);
            this.tbpScriptAppear.Location = new System.Drawing.Point(4, 22);
            this.tbpScriptAppear.Name = "tbpScriptAppear";
            this.tbpScriptAppear.Padding = new System.Windows.Forms.Padding(3);
            this.tbpScriptAppear.Size = new System.Drawing.Size(458, 242);
            this.tbpScriptAppear.TabIndex = 1;
            this.tbpScriptAppear.Text = "Node Appears When";
            this.tbpScriptAppear.UseVisualStyleBackColor = true;
            // 
            // scriptAppear
            // 
            this.scriptAppear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scriptAppear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptAppear.Location = new System.Drawing.Point(3, 3);
            this.scriptAppear.Name = "scriptAppear";
            this.scriptAppear.Size = new System.Drawing.Size(452, 236);
            this.scriptAppear.TabIndex = 1;
            this.scriptAppear.TextChanged += new System.EventHandler(this.scriptAppear_TextChanged);
            // 
            // assetInjectTargetScene
            // 
            this.assetInjectTargetScene.AssetType = Finmer.Editor.AssetPickerControl.EPickerType.Scene;
            this.assetInjectTargetScene.Location = new System.Drawing.Point(0, 24);
            this.assetInjectTargetScene.Name = "assetInjectTargetScene";
            this.assetInjectTargetScene.SelectedGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.assetInjectTargetScene.Size = new System.Drawing.Size(184, 24);
            this.assetInjectTargetScene.TabIndex = 3;
            this.assetInjectTargetScene.SelectedAssetChanged += new System.EventHandler(this.assetInjectTargetScene_SelectedAssetChanged);
            // 
            // FormDocumentScene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 551);
            this.Controls.Add(this.splitNodeList);
            this.Controls.Add(this.toolStrip);
            this.Name = "FormDocumentScene";
            this.Text = "Scene";
            this.Load += new System.EventHandler(this.FormDocumentScene_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitNodeList.Panel1.ResumeLayout(false);
            this.splitNodeList.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitNodeList)).EndInit();
            this.splitNodeList.ResumeLayout(false);
            this.splitNodeSettings.Panel1.ResumeLayout(false);
            this.splitNodeSettings.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitNodeSettings)).EndInit();
            this.splitNodeSettings.ResumeLayout(false);
            this.pnlRootNodeSettings.ResumeLayout(false);
            this.pnlRootNodeSettings.PerformLayout();
            this.pnlInjectionSettings.ResumeLayout(false);
            this.pnlInjectionSettings.PerformLayout();
            this.pnlGeneralNodeSettings.ResumeLayout(false);
            this.pnlGeneralNodeSettings.PerformLayout();
            this.pnlChoiceNodeSettings.ResumeLayout(false);
            this.pnlChoiceNodeSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCustomWidth)).EndInit();
            this.scriptTabs.ResumeLayout(false);
            this.tbpScriptAction.ResumeLayout(false);
            this.tbpScriptAppear.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.SplitContainer splitNodeList;
		private System.Windows.Forms.TreeView trvNodes;
		private System.Windows.Forms.SplitContainer splitNodeSettings;
		private System.Windows.Forms.RadioButton optTypeLink;
		private System.Windows.Forms.RadioButton optTypeNode;
		private System.Windows.Forms.ComboBox cmbLinkTarget;
		private System.Windows.Forms.Label lblLinkTarget;
		private System.Windows.Forms.ToolStripButton tsbAddNode;
		private System.Windows.Forms.ToolStripButton tsbRemoveNode;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbScriptEnter;
		private System.Windows.Forms.ToolStripButton tsbScriptLeave;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsbExpand;
		private System.Windows.Forms.ToolStripButton tsbCollapse;
		private System.Windows.Forms.TabControl scriptTabs;
		private System.Windows.Forms.TabPage tbpScriptAction;
		private ScintillaNET.Scintilla scriptAction;
		private System.Windows.Forms.TabPage tbpScriptAppear;
		private ScintillaNET.Scintilla scriptAppear;
		private System.Windows.Forms.TextBox txtNodeTooltip;
		private System.Windows.Forms.Label lblNodeTooltip;
		private System.Windows.Forms.Label lblNodeKey;
		private System.Windows.Forms.ImageList imlNodeIcons;
		private System.Windows.Forms.TextBox txtNodeKey;
		private System.Windows.Forms.ToolStripButton tsbScriptCustom;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton tsbMoveUp;
		private System.Windows.Forms.ToolStripButton tsbMoveDown;
		private System.Windows.Forms.TextBox txtNodeTitle;
		private System.Windows.Forms.Label lblNodeTitle;
		private System.Windows.Forms.ToolStripButton tsbAddLink;
		private System.Windows.Forms.Panel pnlChoiceNodeSettings;
		private System.Windows.Forms.NumericUpDown nudCustomWidth;
		private System.Windows.Forms.CheckBox chkChoiceHighlight;
		private System.Windows.Forms.CheckBox chkCustomWidth;
        private System.Windows.Forms.Panel pnlRootNodeSettings;
        private System.Windows.Forms.CheckBox chkRootInject;
        private System.Windows.Forms.Label lblRootInfo;
        private System.Windows.Forms.Panel pnlGeneralNodeSettings;
        private System.Windows.Forms.Panel pnlInjectionSettings;
        private System.Windows.Forms.ComboBox cmbInjectTargetNode;
        private System.Windows.Forms.Label lblInjectTargetNode;
        private System.Windows.Forms.Label lblInjectTargetScene;
        private System.Windows.Forms.ComboBox cmbInjectTargetMode;
        private AssetPickerControl assetInjectTargetScene;
    }
}