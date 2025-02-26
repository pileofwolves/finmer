namespace Finmer.Editor
{
	partial class FormDocumentCreature
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
            System.Windows.Forms.Label lblSize;
            System.Windows.Forms.Label lblWits;
            System.Windows.Forms.Label lblBody;
            System.Windows.Forms.Label lblAgi;
            System.Windows.Forms.Label lblStr;
            System.Windows.Forms.Label lblLevel;
            System.Windows.Forms.Label lblGuid;
            System.Windows.Forms.Label lblName;
            System.Windows.Forms.GroupBox fraCombat;
            System.Windows.Forms.Label lblCombatSummaryGroups;
            System.Windows.Forms.Label lblEquip4;
            System.Windows.Forms.Label lblEquip3;
            System.Windows.Forms.Label lblEquip2;
            System.Windows.Forms.Label lblCombatSummaryHeader;
            System.Windows.Forms.Label lblEquip1;
            System.Windows.Forms.GroupBox fraGeneral;
            System.Windows.Forms.Label lblGender;
            System.Windows.Forms.Label lblAlias;
            System.Windows.Forms.GroupBox fraStringMappings;
            System.Windows.Forms.GroupBox fraBehavior;
            this.lblCombatSummaryValues = new System.Windows.Forms.Label();
            this.assetEquip4 = new Finmer.Editor.AssetPickerControl();
            this.assetEquip3 = new Finmer.Editor.AssetPickerControl();
            this.assetEquip2 = new Finmer.Editor.AssetPickerControl();
            this.assetEquip1 = new Finmer.Editor.AssetPickerControl();
            this.nudWits = new System.Windows.Forms.NumericUpDown();
            this.nudStr = new System.Windows.Forms.NumericUpDown();
            this.nudAgi = new System.Windows.Forms.NumericUpDown();
            this.nudBody = new System.Windows.Forms.NumericUpDown();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.nudLevel = new System.Windows.Forms.NumericUpDown();
            this.txtGuid = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmdStringMappingRemove = new System.Windows.Forms.Button();
            this.cmdStringMappingEdit = new System.Windows.Forms.Button();
            this.cmdStringMappingAdd = new System.Windows.Forms.Button();
            this.lsvStringMappings = new System.Windows.Forms.ListView();
            this.clhKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhRule = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhNewKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkAutoVorePrey = new System.Windows.Forms.CheckBox();
            this.chkAutoVorePred = new System.Windows.Forms.CheckBox();
            this.chkVoreDisposal = new System.Windows.Forms.CheckBox();
            this.chkVoreDigest = new System.Windows.Forms.CheckBox();
            this.chkFlagSkipTurns = new System.Windows.Forms.CheckBox();
            this.chkVorePred = new System.Windows.Forms.CheckBox();
            this.chkFlagNoXP = new System.Windows.Forms.CheckBox();
            this.chkFlagNoFight = new System.Windows.Forms.CheckBox();
            this.chkFlagNoVore = new System.Windows.Forms.CheckBox();
            this.chkFlagNoGrapple = new System.Windows.Forms.CheckBox();
            lblSize = new System.Windows.Forms.Label();
            lblWits = new System.Windows.Forms.Label();
            lblBody = new System.Windows.Forms.Label();
            lblAgi = new System.Windows.Forms.Label();
            lblStr = new System.Windows.Forms.Label();
            lblLevel = new System.Windows.Forms.Label();
            lblGuid = new System.Windows.Forms.Label();
            lblName = new System.Windows.Forms.Label();
            fraCombat = new System.Windows.Forms.GroupBox();
            lblCombatSummaryGroups = new System.Windows.Forms.Label();
            lblEquip4 = new System.Windows.Forms.Label();
            lblEquip3 = new System.Windows.Forms.Label();
            lblEquip2 = new System.Windows.Forms.Label();
            lblCombatSummaryHeader = new System.Windows.Forms.Label();
            lblEquip1 = new System.Windows.Forms.Label();
            fraGeneral = new System.Windows.Forms.GroupBox();
            lblGender = new System.Windows.Forms.Label();
            lblAlias = new System.Windows.Forms.Label();
            fraStringMappings = new System.Windows.Forms.GroupBox();
            fraBehavior = new System.Windows.Forms.GroupBox();
            fraCombat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAgi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBody)).BeginInit();
            fraGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel)).BeginInit();
            fraStringMappings.SuspendLayout();
            fraBehavior.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSize
            // 
            lblSize.AutoSize = true;
            lblSize.Location = new System.Drawing.Point(104, 120);
            lblSize.Name = "lblSize";
            lblSize.Size = new System.Drawing.Size(58, 13);
            lblSize.TabIndex = 8;
            lblSize.Text = "Size Class:";
            // 
            // lblWits
            // 
            lblWits.AutoSize = true;
            lblWits.Location = new System.Drawing.Point(208, 24);
            lblWits.Name = "lblWits";
            lblWits.Size = new System.Drawing.Size(31, 13);
            lblWits.TabIndex = 6;
            lblWits.Text = "Wits:";
            // 
            // lblBody
            // 
            lblBody.AutoSize = true;
            lblBody.Location = new System.Drawing.Point(144, 24);
            lblBody.Name = "lblBody";
            lblBody.Size = new System.Drawing.Size(34, 13);
            lblBody.TabIndex = 4;
            lblBody.Text = "Body:";
            // 
            // lblAgi
            // 
            lblAgi.AutoSize = true;
            lblAgi.Location = new System.Drawing.Point(80, 24);
            lblAgi.Name = "lblAgi";
            lblAgi.Size = new System.Drawing.Size(37, 13);
            lblAgi.TabIndex = 2;
            lblAgi.Text = "Agility:";
            // 
            // lblStr
            // 
            lblStr.AutoSize = true;
            lblStr.Location = new System.Drawing.Point(16, 24);
            lblStr.Name = "lblStr";
            lblStr.Size = new System.Drawing.Size(50, 13);
            lblStr.TabIndex = 0;
            lblStr.Text = "Strength:";
            // 
            // lblLevel
            // 
            lblLevel.AutoSize = true;
            lblLevel.Location = new System.Drawing.Point(16, 120);
            lblLevel.Name = "lblLevel";
            lblLevel.Size = new System.Drawing.Size(36, 13);
            lblLevel.TabIndex = 6;
            lblLevel.Text = "Level:";
            // 
            // lblGuid
            // 
            lblGuid.AutoSize = true;
            lblGuid.Location = new System.Drawing.Point(16, 168);
            lblGuid.Name = "lblGuid";
            lblGuid.Size = new System.Drawing.Size(66, 13);
            lblGuid.TabIndex = 10;
            lblGuid.Text = "Asset GUID:";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new System.Drawing.Point(16, 24);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(38, 13);
            lblName.TabIndex = 0;
            lblName.Text = "Name:";
            // 
            // fraCombat
            // 
            fraCombat.Controls.Add(lblCombatSummaryGroups);
            fraCombat.Controls.Add(this.lblCombatSummaryValues);
            fraCombat.Controls.Add(lblEquip4);
            fraCombat.Controls.Add(lblEquip3);
            fraCombat.Controls.Add(lblEquip2);
            fraCombat.Controls.Add(lblCombatSummaryHeader);
            fraCombat.Controls.Add(lblEquip1);
            fraCombat.Controls.Add(this.assetEquip4);
            fraCombat.Controls.Add(this.assetEquip3);
            fraCombat.Controls.Add(this.assetEquip2);
            fraCombat.Controls.Add(this.assetEquip1);
            fraCombat.Controls.Add(lblWits);
            fraCombat.Controls.Add(lblBody);
            fraCombat.Controls.Add(lblAgi);
            fraCombat.Controls.Add(lblStr);
            fraCombat.Controls.Add(this.nudWits);
            fraCombat.Controls.Add(this.nudStr);
            fraCombat.Controls.Add(this.nudAgi);
            fraCombat.Controls.Add(this.nudBody);
            fraCombat.Location = new System.Drawing.Point(352, 16);
            fraCombat.Name = "fraCombat";
            fraCombat.Size = new System.Drawing.Size(352, 280);
            fraCombat.TabIndex = 2;
            fraCombat.TabStop = false;
            fraCombat.Text = "Combat Settings";
            // 
            // lblCombatSummaryGroups
            // 
            lblCombatSummaryGroups.Location = new System.Drawing.Point(24, 200);
            lblCombatSummaryGroups.Name = "lblCombatSummaryGroups";
            lblCombatSummaryGroups.Size = new System.Drawing.Size(88, 72);
            lblCombatSummaryGroups.TabIndex = 17;
            lblCombatSummaryGroups.Text = "Attack Dice:\r\nDefense Dice:\r\nGrapple Dice:\r\nSwallow Dice:\r\nStruggle Dice:";
            // 
            // lblCombatSummaryValues
            // 
            this.lblCombatSummaryValues.Location = new System.Drawing.Point(112, 200);
            this.lblCombatSummaryValues.Name = "lblCombatSummaryValues";
            this.lblCombatSummaryValues.Size = new System.Drawing.Size(88, 72);
            this.lblCombatSummaryValues.TabIndex = 18;
            this.lblCombatSummaryValues.Text = "...";
            // 
            // lblEquip4
            // 
            lblEquip4.Location = new System.Drawing.Point(16, 144);
            lblEquip4.Name = "lblEquip4";
            lblEquip4.Size = new System.Drawing.Size(64, 24);
            lblEquip4.TabIndex = 14;
            lblEquip4.Text = "Accessory:";
            lblEquip4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEquip3
            // 
            lblEquip3.Location = new System.Drawing.Point(16, 120);
            lblEquip3.Name = "lblEquip3";
            lblEquip3.Size = new System.Drawing.Size(64, 24);
            lblEquip3.TabIndex = 12;
            lblEquip3.Text = "Accessory:";
            lblEquip3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEquip2
            // 
            lblEquip2.Location = new System.Drawing.Point(16, 96);
            lblEquip2.Name = "lblEquip2";
            lblEquip2.Size = new System.Drawing.Size(64, 24);
            lblEquip2.TabIndex = 10;
            lblEquip2.Text = "Armor:";
            lblEquip2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCombatSummaryHeader
            // 
            lblCombatSummaryHeader.AutoSize = true;
            lblCombatSummaryHeader.Location = new System.Drawing.Point(16, 184);
            lblCombatSummaryHeader.Name = "lblCombatSummaryHeader";
            lblCombatSummaryHeader.Size = new System.Drawing.Size(53, 13);
            lblCombatSummaryHeader.TabIndex = 16;
            lblCombatSummaryHeader.Text = "Summary:";
            // 
            // lblEquip1
            // 
            lblEquip1.Location = new System.Drawing.Point(16, 72);
            lblEquip1.Name = "lblEquip1";
            lblEquip1.Size = new System.Drawing.Size(64, 24);
            lblEquip1.TabIndex = 8;
            lblEquip1.Text = "Weapon:";
            lblEquip1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // assetEquip4
            // 
            this.assetEquip4.AssetType = Finmer.Editor.AssetPickerControl.EPickerType.Item;
            this.assetEquip4.Location = new System.Drawing.Point(80, 144);
            this.assetEquip4.Name = "assetEquip4";
            this.assetEquip4.SelectedGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.assetEquip4.SelectorPredicate = null;
            this.assetEquip4.Size = new System.Drawing.Size(264, 24);
            this.assetEquip4.TabIndex = 15;
            // 
            // assetEquip3
            // 
            this.assetEquip3.AssetType = Finmer.Editor.AssetPickerControl.EPickerType.Item;
            this.assetEquip3.Location = new System.Drawing.Point(80, 120);
            this.assetEquip3.Name = "assetEquip3";
            this.assetEquip3.SelectedGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.assetEquip3.SelectorPredicate = null;
            this.assetEquip3.Size = new System.Drawing.Size(264, 24);
            this.assetEquip3.TabIndex = 13;
            // 
            // assetEquip2
            // 
            this.assetEquip2.AssetType = Finmer.Editor.AssetPickerControl.EPickerType.Item;
            this.assetEquip2.Location = new System.Drawing.Point(80, 96);
            this.assetEquip2.Name = "assetEquip2";
            this.assetEquip2.SelectedGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.assetEquip2.SelectorPredicate = null;
            this.assetEquip2.Size = new System.Drawing.Size(264, 24);
            this.assetEquip2.TabIndex = 11;
            // 
            // assetEquip1
            // 
            this.assetEquip1.AssetType = Finmer.Editor.AssetPickerControl.EPickerType.Item;
            this.assetEquip1.Location = new System.Drawing.Point(80, 72);
            this.assetEquip1.Name = "assetEquip1";
            this.assetEquip1.SelectedGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.assetEquip1.SelectorPredicate = null;
            this.assetEquip1.Size = new System.Drawing.Size(264, 24);
            this.assetEquip1.TabIndex = 9;
            // 
            // nudWits
            // 
            this.nudWits.Location = new System.Drawing.Point(208, 40);
            this.nudWits.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWits.Name = "nudWits";
            this.nudWits.Size = new System.Drawing.Size(56, 20);
            this.nudWits.TabIndex = 7;
            this.nudWits.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nudStr
            // 
            this.nudStr.Location = new System.Drawing.Point(16, 40);
            this.nudStr.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudStr.Name = "nudStr";
            this.nudStr.Size = new System.Drawing.Size(56, 20);
            this.nudStr.TabIndex = 1;
            this.nudStr.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nudAgi
            // 
            this.nudAgi.Location = new System.Drawing.Point(80, 40);
            this.nudAgi.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAgi.Name = "nudAgi";
            this.nudAgi.Size = new System.Drawing.Size(56, 20);
            this.nudAgi.TabIndex = 3;
            this.nudAgi.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nudBody
            // 
            this.nudBody.Location = new System.Drawing.Point(144, 40);
            this.nudBody.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBody.Name = "nudBody";
            this.nudBody.Size = new System.Drawing.Size(56, 20);
            this.nudBody.TabIndex = 5;
            this.nudBody.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // fraGeneral
            // 
            fraGeneral.Controls.Add(this.cmbGender);
            fraGeneral.Controls.Add(this.cmbSize);
            fraGeneral.Controls.Add(lblSize);
            fraGeneral.Controls.Add(lblGender);
            fraGeneral.Controls.Add(this.txtAlias);
            fraGeneral.Controls.Add(lblAlias);
            fraGeneral.Controls.Add(lblLevel);
            fraGeneral.Controls.Add(this.nudLevel);
            fraGeneral.Controls.Add(this.txtGuid);
            fraGeneral.Controls.Add(lblGuid);
            fraGeneral.Controls.Add(this.txtName);
            fraGeneral.Controls.Add(lblName);
            fraGeneral.Location = new System.Drawing.Point(16, 16);
            fraGeneral.Name = "fraGeneral";
            fraGeneral.Size = new System.Drawing.Size(320, 224);
            fraGeneral.TabIndex = 0;
            fraGeneral.TabStop = false;
            fraGeneral.Text = "General";
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            "Male (he/him)",
            "Female (she/her)",
            "Neutral (they/them)",
            "Ungendered (it/it)"});
            this.cmbGender.Location = new System.Drawing.Point(184, 88);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(120, 21);
            this.cmbGender.TabIndex = 5;
            // 
            // cmbSize
            // 
            this.cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSize.FormattingEnabled = true;
            this.cmbSize.Items.AddRange(new object[] {
            "Tiny",
            "Small",
            "Medium",
            "Large",
            "Huge"});
            this.cmbSize.Location = new System.Drawing.Point(104, 136);
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new System.Drawing.Size(112, 21);
            this.cmbSize.TabIndex = 9;
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Location = new System.Drawing.Point(184, 72);
            lblGender.Name = "lblGender";
            lblGender.Size = new System.Drawing.Size(45, 13);
            lblGender.TabIndex = 4;
            lblGender.Text = "Gender:";
            // 
            // txtAlias
            // 
            this.txtAlias.Location = new System.Drawing.Point(16, 88);
            this.txtAlias.MaxLength = 100;
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(160, 20);
            this.txtAlias.TabIndex = 3;
            // 
            // lblAlias
            // 
            lblAlias.AutoSize = true;
            lblAlias.Location = new System.Drawing.Point(16, 72);
            lblAlias.Name = "lblAlias";
            lblAlias.Size = new System.Drawing.Size(32, 13);
            lblAlias.TabIndex = 2;
            lblAlias.Text = "Alias:";
            // 
            // nudLevel
            // 
            this.nudLevel.Location = new System.Drawing.Point(16, 136);
            this.nudLevel.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.nudLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLevel.Name = "nudLevel";
            this.nudLevel.Size = new System.Drawing.Size(80, 20);
            this.nudLevel.TabIndex = 7;
            this.nudLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtGuid
            // 
            this.txtGuid.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGuid.Location = new System.Drawing.Point(16, 184);
            this.txtGuid.MaxLength = 100;
            this.txtGuid.Name = "txtGuid";
            this.txtGuid.ReadOnly = true;
            this.txtGuid.Size = new System.Drawing.Size(288, 21);
            this.txtGuid.TabIndex = 11;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(16, 40);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(288, 20);
            this.txtName.TabIndex = 1;
            // 
            // fraStringMappings
            // 
            fraStringMappings.Controls.Add(this.cmdStringMappingRemove);
            fraStringMappings.Controls.Add(this.cmdStringMappingEdit);
            fraStringMappings.Controls.Add(this.cmdStringMappingAdd);
            fraStringMappings.Controls.Add(this.lsvStringMappings);
            fraStringMappings.Location = new System.Drawing.Point(16, 256);
            fraStringMappings.Name = "fraStringMappings";
            fraStringMappings.Size = new System.Drawing.Size(320, 208);
            fraStringMappings.TabIndex = 1;
            fraStringMappings.TabStop = false;
            fraStringMappings.Text = "String Mappings";
            // 
            // cmdStringMappingRemove
            // 
            this.cmdStringMappingRemove.Enabled = false;
            this.cmdStringMappingRemove.Image = global::Finmer.Editor.Properties.Resources.minus;
            this.cmdStringMappingRemove.Location = new System.Drawing.Point(208, 24);
            this.cmdStringMappingRemove.Name = "cmdStringMappingRemove";
            this.cmdStringMappingRemove.Size = new System.Drawing.Size(88, 24);
            this.cmdStringMappingRemove.TabIndex = 2;
            this.cmdStringMappingRemove.Text = "Remove";
            this.cmdStringMappingRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdStringMappingRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdStringMappingRemove.UseVisualStyleBackColor = true;
            this.cmdStringMappingRemove.Click += new System.EventHandler(this.cmdStringMappingRemove_Click);
            // 
            // cmdStringMappingEdit
            // 
            this.cmdStringMappingEdit.Enabled = false;
            this.cmdStringMappingEdit.Image = global::Finmer.Editor.Properties.Resources.pencil;
            this.cmdStringMappingEdit.Location = new System.Drawing.Point(112, 24);
            this.cmdStringMappingEdit.Name = "cmdStringMappingEdit";
            this.cmdStringMappingEdit.Size = new System.Drawing.Size(88, 24);
            this.cmdStringMappingEdit.TabIndex = 1;
            this.cmdStringMappingEdit.Text = "Edit";
            this.cmdStringMappingEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdStringMappingEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdStringMappingEdit.UseVisualStyleBackColor = true;
            this.cmdStringMappingEdit.Click += new System.EventHandler(this.cmdStringMappingEdit_Click);
            // 
            // cmdStringMappingAdd
            // 
            this.cmdStringMappingAdd.Image = global::Finmer.Editor.Properties.Resources.plus;
            this.cmdStringMappingAdd.Location = new System.Drawing.Point(16, 24);
            this.cmdStringMappingAdd.Name = "cmdStringMappingAdd";
            this.cmdStringMappingAdd.Size = new System.Drawing.Size(88, 24);
            this.cmdStringMappingAdd.TabIndex = 0;
            this.cmdStringMappingAdd.Text = "Add";
            this.cmdStringMappingAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdStringMappingAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdStringMappingAdd.UseVisualStyleBackColor = true;
            this.cmdStringMappingAdd.Click += new System.EventHandler(this.cmdStringMappingAdd_Click);
            // 
            // lsvStringMappings
            // 
            this.lsvStringMappings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhKey,
            this.clhRule,
            this.clhNewKey});
            this.lsvStringMappings.FullRowSelect = true;
            this.lsvStringMappings.GridLines = true;
            this.lsvStringMappings.HideSelection = false;
            this.lsvStringMappings.Location = new System.Drawing.Point(8, 56);
            this.lsvStringMappings.MultiSelect = false;
            this.lsvStringMappings.Name = "lsvStringMappings";
            this.lsvStringMappings.Size = new System.Drawing.Size(304, 144);
            this.lsvStringMappings.TabIndex = 3;
            this.lsvStringMappings.UseCompatibleStateImageBehavior = false;
            this.lsvStringMappings.View = System.Windows.Forms.View.Details;
            this.lsvStringMappings.SelectedIndexChanged += new System.EventHandler(this.lsvStringMappings_SelectedIndexChanged);
            this.lsvStringMappings.DoubleClick += new System.EventHandler(this.lsvStringMappings_DoubleClick);
            // 
            // clhKey
            // 
            this.clhKey.Text = "Key";
            this.clhKey.Width = 105;
            // 
            // clhRule
            // 
            this.clhRule.Text = "Rule";
            this.clhRule.Width = 70;
            // 
            // clhNewKey
            // 
            this.clhNewKey.Text = "Replacement Key";
            this.clhNewKey.Width = 105;
            // 
            // chkAutoVorePrey
            // 
            this.chkAutoVorePrey.AutoSize = true;
            this.chkAutoVorePrey.Location = new System.Drawing.Point(168, 120);
            this.chkAutoVorePrey.Name = "chkAutoVorePrey";
            this.chkAutoVorePrey.Size = new System.Drawing.Size(156, 17);
            this.chkAutoVorePrey.TabIndex = 9;
            this.chkAutoVorePrey.Text = "Swallowed When Defeated";
            this.chkAutoVorePrey.UseVisualStyleBackColor = true;
            // 
            // chkAutoVorePred
            // 
            this.chkAutoVorePred.AutoSize = true;
            this.chkAutoVorePred.Location = new System.Drawing.Point(176, 96);
            this.chkAutoVorePred.Name = "chkAutoVorePred";
            this.chkAutoVorePred.Size = new System.Drawing.Size(144, 17);
            this.chkAutoVorePred.TabIndex = 8;
            this.chkAutoVorePred.Text = "Swallow Defeated Player";
            this.chkAutoVorePred.UseVisualStyleBackColor = true;
            // 
            // chkVoreDisposal
            // 
            this.chkVoreDisposal.AutoSize = true;
            this.chkVoreDisposal.Location = new System.Drawing.Point(176, 72);
            this.chkVoreDisposal.Name = "chkVoreDisposal";
            this.chkVoreDisposal.Size = new System.Drawing.Size(122, 17);
            this.chkVoreDisposal.TabIndex = 7;
            this.chkVoreDisposal.Text = "Has Disposal Scene";
            this.chkVoreDisposal.UseVisualStyleBackColor = true;
            // 
            // chkVoreDigest
            // 
            this.chkVoreDigest.AutoSize = true;
            this.chkVoreDigest.Location = new System.Drawing.Point(176, 48);
            this.chkVoreDigest.Name = "chkVoreDigest";
            this.chkVoreDigest.Size = new System.Drawing.Size(85, 17);
            this.chkVoreDigest.TabIndex = 6;
            this.chkVoreDigest.Text = "Digests Prey";
            this.chkVoreDigest.UseVisualStyleBackColor = true;
            // 
            // chkFlagSkipTurns
            // 
            this.chkFlagSkipTurns.AutoSize = true;
            this.chkFlagSkipTurns.Location = new System.Drawing.Point(16, 24);
            this.chkFlagSkipTurns.Name = "chkFlagSkipTurns";
            this.chkFlagSkipTurns.Size = new System.Drawing.Size(91, 17);
            this.chkFlagSkipTurns.TabIndex = 0;
            this.chkFlagSkipTurns.Text = "Skip All Turns";
            this.chkFlagSkipTurns.UseVisualStyleBackColor = true;
            // 
            // chkVorePred
            // 
            this.chkVorePred.AutoSize = true;
            this.chkVorePred.Location = new System.Drawing.Point(168, 24);
            this.chkVorePred.Name = "chkVorePred";
            this.chkVorePred.Size = new System.Drawing.Size(77, 17);
            this.chkVorePred.TabIndex = 5;
            this.chkVorePred.Text = "Is Predator";
            this.chkVorePred.UseVisualStyleBackColor = true;
            this.chkVorePred.CheckedChanged += new System.EventHandler(this.chkVorePred_CheckedChanged);
            // 
            // chkFlagNoXP
            // 
            this.chkFlagNoXP.AutoSize = true;
            this.chkFlagNoXP.Location = new System.Drawing.Point(16, 120);
            this.chkFlagNoXP.Name = "chkFlagNoXP";
            this.chkFlagNoXP.Size = new System.Drawing.Size(113, 17);
            this.chkFlagNoXP.TabIndex = 4;
            this.chkFlagNoXP.Text = "Does Not Give XP";
            this.chkFlagNoXP.UseVisualStyleBackColor = true;
            // 
            // chkFlagNoFight
            // 
            this.chkFlagNoFight.AutoSize = true;
            this.chkFlagNoFight.Location = new System.Drawing.Point(16, 48);
            this.chkFlagNoFight.Name = "chkFlagNoFight";
            this.chkFlagNoFight.Size = new System.Drawing.Size(122, 17);
            this.chkFlagNoFight.TabIndex = 1;
            this.chkFlagNoFight.Text = "Cannot Be Attacked";
            this.chkFlagNoFight.UseVisualStyleBackColor = true;
            // 
            // chkFlagNoVore
            // 
            this.chkFlagNoVore.AutoSize = true;
            this.chkFlagNoVore.Location = new System.Drawing.Point(16, 96);
            this.chkFlagNoVore.Name = "chkFlagNoVore";
            this.chkFlagNoVore.Size = new System.Drawing.Size(107, 17);
            this.chkFlagNoVore.TabIndex = 3;
            this.chkFlagNoVore.Text = "Cannot Be Vored";
            this.chkFlagNoVore.UseVisualStyleBackColor = true;
            // 
            // chkFlagNoGrapple
            // 
            this.chkFlagNoGrapple.AutoSize = true;
            this.chkFlagNoGrapple.Location = new System.Drawing.Point(16, 72);
            this.chkFlagNoGrapple.Name = "chkFlagNoGrapple";
            this.chkFlagNoGrapple.Size = new System.Drawing.Size(100, 17);
            this.chkFlagNoGrapple.TabIndex = 2;
            this.chkFlagNoGrapple.Text = "Cannot Grapple";
            this.chkFlagNoGrapple.UseVisualStyleBackColor = true;
            // 
            // fraBehavior
            // 
            fraBehavior.Controls.Add(this.chkAutoVorePrey);
            fraBehavior.Controls.Add(this.chkVorePred);
            fraBehavior.Controls.Add(this.chkFlagNoXP);
            fraBehavior.Controls.Add(this.chkVoreDigest);
            fraBehavior.Controls.Add(this.chkFlagNoGrapple);
            fraBehavior.Controls.Add(this.chkVoreDisposal);
            fraBehavior.Controls.Add(this.chkFlagNoVore);
            fraBehavior.Controls.Add(this.chkAutoVorePred);
            fraBehavior.Controls.Add(this.chkFlagNoFight);
            fraBehavior.Controls.Add(this.chkFlagSkipTurns);
            fraBehavior.Location = new System.Drawing.Point(352, 312);
            fraBehavior.Name = "fraBehavior";
            fraBehavior.Size = new System.Drawing.Size(352, 152);
            fraBehavior.TabIndex = 3;
            fraBehavior.TabStop = false;
            fraBehavior.Text = "Combat Behavior";
            // 
            // FormDocumentCreature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 481);
            this.Controls.Add(fraBehavior);
            this.Controls.Add(fraStringMappings);
            this.Controls.Add(fraCombat);
            this.Controls.Add(fraGeneral);
            this.Name = "FormDocumentCreature";
            this.Text = "FormDocumentCreature";
            this.Load += new System.EventHandler(this.FormDocumentCreature_Load);
            fraCombat.ResumeLayout(false);
            fraCombat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAgi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBody)).EndInit();
            fraGeneral.ResumeLayout(false);
            fraGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel)).EndInit();
            fraStringMappings.ResumeLayout(false);
            fraBehavior.ResumeLayout(false);
            fraBehavior.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TextBox txtGuid;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.NumericUpDown nudLevel;
		private System.Windows.Forms.NumericUpDown nudWits;
		private System.Windows.Forms.NumericUpDown nudBody;
		private System.Windows.Forms.NumericUpDown nudAgi;
		private System.Windows.Forms.NumericUpDown nudStr;
		private System.Windows.Forms.ComboBox cmbSize;
		private System.Windows.Forms.TextBox txtAlias;
		private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.CheckBox chkAutoVorePred;
        private System.Windows.Forms.CheckBox chkVoreDisposal;
        private System.Windows.Forms.CheckBox chkVoreDigest;
        private System.Windows.Forms.CheckBox chkVorePred;
        private System.Windows.Forms.CheckBox chkFlagSkipTurns;
        private System.Windows.Forms.CheckBox chkFlagNoXP;
        private System.Windows.Forms.CheckBox chkFlagNoFight;
        private System.Windows.Forms.CheckBox chkFlagNoVore;
        private System.Windows.Forms.CheckBox chkFlagNoGrapple;
        private System.Windows.Forms.Label lblCombatSummaryValues;
        private AssetPickerControl assetEquip4;
        private AssetPickerControl assetEquip3;
        private AssetPickerControl assetEquip2;
        private AssetPickerControl assetEquip1;
        private System.Windows.Forms.Button cmdStringMappingRemove;
        private System.Windows.Forms.Button cmdStringMappingEdit;
        private System.Windows.Forms.Button cmdStringMappingAdd;
        private System.Windows.Forms.ListView lsvStringMappings;
        private System.Windows.Forms.ColumnHeader clhKey;
        private System.Windows.Forms.ColumnHeader clhRule;
        private System.Windows.Forms.ColumnHeader clhNewKey;
        private System.Windows.Forms.CheckBox chkAutoVorePrey;
    }
}