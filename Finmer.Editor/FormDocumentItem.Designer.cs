namespace Finmer.Editor
{
	partial class FormDocumentItem
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.fraGeneral = new System.Windows.Forms.GroupBox();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cmdIconExport = new System.Windows.Forms.Button();
            this.cmdIconClear = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.cmdIcon = new System.Windows.Forms.Button();
            this.txtGuid = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFlavor = new System.Windows.Forms.TextBox();
            this.chkQuest = new System.Windows.Forms.CheckBox();
            this.nudValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fraWeapon = new System.Windows.Forms.GroupBox();
            this.nudWeaponCritMult = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.nudWeaponCritRange = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudWeaponAttack = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbWeaponType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtWeaponDamage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fraArmor = new System.Windows.Forms.GroupBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.nudArmorDex = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.nudArmorCheck = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbArmorType = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.nudArmorClass = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.fraUsable = new System.Windows.Forms.GroupBox();
            this.chkItemConsumable = new System.Windows.Forms.CheckBox();
            this.chkUseBattle = new System.Windows.Forms.CheckBox();
            this.chkUseField = new System.Windows.Forms.CheckBox();
            this.txtUseDesc = new System.Windows.Forms.TextBox();
            this.lblUseDesc = new System.Windows.Forms.Label();
            this.cmdEditUseScript = new System.Windows.Forms.Button();
            this.dlgIconOpen = new System.Windows.Forms.OpenFileDialog();
            this.fraGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).BeginInit();
            this.fraWeapon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeaponCritMult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeaponCritRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeaponAttack)).BeginInit();
            this.fraArmor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudArmorDex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudArmorCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudArmorClass)).BeginInit();
            this.fraUsable.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Public Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(16, 40);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(264, 20);
            this.txtName.TabIndex = 0;
            // 
            // fraGeneral
            // 
            this.fraGeneral.Controls.Add(this.txtAlias);
            this.fraGeneral.Controls.Add(this.label18);
            this.fraGeneral.Controls.Add(this.cmdIconExport);
            this.fraGeneral.Controls.Add(this.cmdIconClear);
            this.fraGeneral.Controls.Add(this.label17);
            this.fraGeneral.Controls.Add(this.cmdIcon);
            this.fraGeneral.Controls.Add(this.txtGuid);
            this.fraGeneral.Controls.Add(this.label10);
            this.fraGeneral.Controls.Add(this.label11);
            this.fraGeneral.Controls.Add(this.txtFlavor);
            this.fraGeneral.Controls.Add(this.chkQuest);
            this.fraGeneral.Controls.Add(this.nudValue);
            this.fraGeneral.Controls.Add(this.label3);
            this.fraGeneral.Controls.Add(this.cmbType);
            this.fraGeneral.Controls.Add(this.label2);
            this.fraGeneral.Controls.Add(this.txtName);
            this.fraGeneral.Controls.Add(this.label1);
            this.fraGeneral.Location = new System.Drawing.Point(16, 16);
            this.fraGeneral.Name = "fraGeneral";
            this.fraGeneral.Size = new System.Drawing.Size(296, 424);
            this.fraGeneral.TabIndex = 2;
            this.fraGeneral.TabStop = false;
            this.fraGeneral.Text = "General";
            // 
            // txtAlias
            // 
            this.txtAlias.Location = new System.Drawing.Point(16, 88);
            this.txtAlias.MaxLength = 100;
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(264, 20);
            this.txtAlias.TabIndex = 1;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(16, 72);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(32, 13);
            this.label18.TabIndex = 15;
            this.label18.Text = "Alias:";
            // 
            // cmdIconExport
            // 
            this.cmdIconExport.Image = global::Finmer.Editor.Properties.Resources.folder_export;
            this.cmdIconExport.Location = new System.Drawing.Point(240, 184);
            this.cmdIconExport.Name = "cmdIconExport";
            this.cmdIconExport.Size = new System.Drawing.Size(24, 24);
            this.cmdIconExport.TabIndex = 6;
            this.cmdIconExport.UseVisualStyleBackColor = true;
            // 
            // cmdIconClear
            // 
            this.cmdIconClear.Image = global::Finmer.Editor.Properties.Resources.cross_script;
            this.cmdIconClear.Location = new System.Drawing.Point(264, 184);
            this.cmdIconClear.Name = "cmdIconClear";
            this.cmdIconClear.Size = new System.Drawing.Size(24, 24);
            this.cmdIconClear.TabIndex = 7;
            this.cmdIconClear.UseVisualStyleBackColor = true;
            this.cmdIconClear.Click += new System.EventHandler(this.cmdIconClear_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(240, 120);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(31, 13);
            this.label17.TabIndex = 11;
            this.label17.Text = "Icon:";
            // 
            // cmdIcon
            // 
            this.cmdIcon.Location = new System.Drawing.Point(240, 136);
            this.cmdIcon.Name = "cmdIcon";
            this.cmdIcon.Size = new System.Drawing.Size(48, 48);
            this.cmdIcon.TabIndex = 5;
            this.cmdIcon.UseVisualStyleBackColor = true;
            this.cmdIcon.Click += new System.EventHandler(this.cmdIcon_Click);
            // 
            // txtGuid
            // 
            this.txtGuid.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGuid.Location = new System.Drawing.Point(16, 384);
            this.txtGuid.MaxLength = 100;
            this.txtGuid.Name = "txtGuid";
            this.txtGuid.ReadOnly = true;
            this.txtGuid.Size = new System.Drawing.Size(264, 21);
            this.txtGuid.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 368);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Asset GUID:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 256);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "Flavor Text:";
            // 
            // txtFlavor
            // 
            this.txtFlavor.Location = new System.Drawing.Point(16, 272);
            this.txtFlavor.MaxLength = 1000;
            this.txtFlavor.Multiline = true;
            this.txtFlavor.Name = "txtFlavor";
            this.txtFlavor.Size = new System.Drawing.Size(264, 88);
            this.txtFlavor.TabIndex = 9;
            // 
            // chkQuest
            // 
            this.chkQuest.AutoSize = true;
            this.chkQuest.Location = new System.Drawing.Point(16, 224);
            this.chkQuest.Name = "chkQuest";
            this.chkQuest.Size = new System.Drawing.Size(77, 17);
            this.chkQuest.TabIndex = 8;
            this.chkQuest.Text = "Quest Item";
            this.chkQuest.UseVisualStyleBackColor = true;
            // 
            // nudValue
            // 
            this.nudValue.Location = new System.Drawing.Point(16, 184);
            this.nudValue.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudValue.Name = "nudValue";
            this.nudValue.Size = new System.Drawing.Size(96, 20);
            this.nudValue.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Value:";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Generic",
            "Equipable",
            "Usable"});
            this.cmbType.Location = new System.Drawing.Point(16, 136);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(208, 21);
            this.cmbType.TabIndex = 2;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Type:";
            // 
            // fraWeapon
            // 
            this.fraWeapon.Controls.Add(this.nudWeaponCritMult);
            this.fraWeapon.Controls.Add(this.label9);
            this.fraWeapon.Controls.Add(this.nudWeaponCritRange);
            this.fraWeapon.Controls.Add(this.label8);
            this.fraWeapon.Controls.Add(this.nudWeaponAttack);
            this.fraWeapon.Controls.Add(this.label7);
            this.fraWeapon.Controls.Add(this.cmbWeaponType);
            this.fraWeapon.Controls.Add(this.label6);
            this.fraWeapon.Controls.Add(this.txtWeaponDamage);
            this.fraWeapon.Controls.Add(this.label5);
            this.fraWeapon.Location = new System.Drawing.Point(328, 16);
            this.fraWeapon.Name = "fraWeapon";
            this.fraWeapon.Size = new System.Drawing.Size(296, 176);
            this.fraWeapon.TabIndex = 4;
            this.fraWeapon.TabStop = false;
            this.fraWeapon.Text = "Weapon Stats";
            // 
            // nudWeaponCritMult
            // 
            this.nudWeaponCritMult.Location = new System.Drawing.Point(112, 136);
            this.nudWeaponCritMult.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudWeaponCritMult.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWeaponCritMult.Name = "nudWeaponCritMult";
            this.nudWeaponCritMult.Size = new System.Drawing.Size(88, 20);
            this.nudWeaponCritMult.TabIndex = 11;
            this.nudWeaponCritMult.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(112, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Crit Multiplier:";
            // 
            // nudWeaponCritRange
            // 
            this.nudWeaponCritRange.Location = new System.Drawing.Point(16, 136);
            this.nudWeaponCritRange.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudWeaponCritRange.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWeaponCritRange.Name = "nudWeaponCritRange";
            this.nudWeaponCritRange.Size = new System.Drawing.Size(88, 20);
            this.nudWeaponCritRange.TabIndex = 10;
            this.nudWeaponCritRange.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Crit Range:";
            // 
            // nudWeaponAttack
            // 
            this.nudWeaponAttack.Location = new System.Drawing.Point(184, 88);
            this.nudWeaponAttack.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudWeaponAttack.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.nudWeaponAttack.Name = "nudWeaponAttack";
            this.nudWeaponAttack.Size = new System.Drawing.Size(96, 20);
            this.nudWeaponAttack.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(184, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Attack Bonus:";
            // 
            // cmbWeaponType
            // 
            this.cmbWeaponType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeaponType.FormattingEnabled = true;
            this.cmbWeaponType.Items.AddRange(new object[] {
            "Natural",
            "Light",
            "One-handed",
            "Two-handed"});
            this.cmbWeaponType.Location = new System.Drawing.Point(16, 40);
            this.cmbWeaponType.Name = "cmbWeaponType";
            this.cmbWeaponType.Size = new System.Drawing.Size(264, 21);
            this.cmbWeaponType.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Damage Roll:";
            // 
            // txtWeaponDamage
            // 
            this.txtWeaponDamage.Location = new System.Drawing.Point(16, 88);
            this.txtWeaponDamage.Name = "txtWeaponDamage";
            this.txtWeaponDamage.Size = new System.Drawing.Size(160, 20);
            this.txtWeaponDamage.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Weapon Type:";
            // 
            // fraArmor
            // 
            this.fraArmor.Controls.Add(this.comboBox4);
            this.fraArmor.Controls.Add(this.label16);
            this.fraArmor.Controls.Add(this.nudArmorDex);
            this.fraArmor.Controls.Add(this.label15);
            this.fraArmor.Controls.Add(this.nudArmorCheck);
            this.fraArmor.Controls.Add(this.label14);
            this.fraArmor.Controls.Add(this.cmbArmorType);
            this.fraArmor.Controls.Add(this.label13);
            this.fraArmor.Controls.Add(this.nudArmorClass);
            this.fraArmor.Controls.Add(this.label12);
            this.fraArmor.Location = new System.Drawing.Point(328, 16);
            this.fraArmor.Name = "fraArmor";
            this.fraArmor.Size = new System.Drawing.Size(296, 184);
            this.fraArmor.TabIndex = 14;
            this.fraArmor.TabStop = false;
            this.fraArmor.Text = "Armor Stats";
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.Enabled = false;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "Natural",
            "Enhancement",
            "Deflection",
            "Dodge"});
            this.comboBox4.Location = new System.Drawing.Point(16, 88);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(264, 21);
            this.comboBox4.TabIndex = 13;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 72);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(97, 13);
            this.label16.TabIndex = 17;
            this.label16.Text = "Stacking Category:";
            // 
            // nudArmorDex
            // 
            this.nudArmorDex.Location = new System.Drawing.Point(192, 144);
            this.nudArmorDex.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudArmorDex.Name = "nudArmorDex";
            this.nudArmorDex.Size = new System.Drawing.Size(80, 20);
            this.nudArmorDex.TabIndex = 16;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(192, 128);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 13);
            this.label15.TabIndex = 15;
            this.label15.Text = "Max Dex:";
            // 
            // nudArmorCheck
            // 
            this.nudArmorCheck.Location = new System.Drawing.Point(104, 144);
            this.nudArmorCheck.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudArmorCheck.Name = "nudArmorCheck";
            this.nudArmorCheck.Size = new System.Drawing.Size(80, 20);
            this.nudArmorCheck.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(104, 128);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "Check Penalty:";
            // 
            // cmbArmorType
            // 
            this.cmbArmorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbArmorType.FormattingEnabled = true;
            this.cmbArmorType.Items.AddRange(new object[] {
            "Armor",
            "Charm"});
            this.cmbArmorType.Location = new System.Drawing.Point(16, 40);
            this.cmbArmorType.Name = "cmbArmorType";
            this.cmbArmorType.Size = new System.Drawing.Size(264, 21);
            this.cmbArmorType.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "Armor Type:";
            // 
            // nudArmorClass
            // 
            this.nudArmorClass.Location = new System.Drawing.Point(16, 144);
            this.nudArmorClass.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudArmorClass.Name = "nudArmorClass";
            this.nudArmorClass.Size = new System.Drawing.Size(80, 20);
            this.nudArmorClass.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 128);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "Armor Class:";
            // 
            // fraUsable
            // 
            this.fraUsable.Controls.Add(this.chkItemConsumable);
            this.fraUsable.Controls.Add(this.chkUseBattle);
            this.fraUsable.Controls.Add(this.chkUseField);
            this.fraUsable.Controls.Add(this.txtUseDesc);
            this.fraUsable.Controls.Add(this.lblUseDesc);
            this.fraUsable.Controls.Add(this.cmdEditUseScript);
            this.fraUsable.Location = new System.Drawing.Point(328, 16);
            this.fraUsable.Name = "fraUsable";
            this.fraUsable.Size = new System.Drawing.Size(288, 280);
            this.fraUsable.TabIndex = 17;
            this.fraUsable.TabStop = false;
            this.fraUsable.Text = "Usable";
            // 
            // chkItemConsumable
            // 
            this.chkItemConsumable.AutoSize = true;
            this.chkItemConsumable.Location = new System.Drawing.Point(16, 192);
            this.chkItemConsumable.Name = "chkItemConsumable";
            this.chkItemConsumable.Size = new System.Drawing.Size(190, 17);
            this.chkItemConsumable.TabIndex = 13;
            this.chkItemConsumable.Text = "Consumable (Disappears after use)";
            this.chkItemConsumable.UseVisualStyleBackColor = true;
            // 
            // chkUseBattle
            // 
            this.chkUseBattle.AutoSize = true;
            this.chkUseBattle.Location = new System.Drawing.Point(16, 240);
            this.chkUseBattle.Name = "chkUseBattle";
            this.chkUseBattle.Size = new System.Drawing.Size(114, 17);
            this.chkUseBattle.TabIndex = 15;
            this.chkUseBattle.Text = "Allow Use in Battle";
            this.chkUseBattle.UseVisualStyleBackColor = true;
            // 
            // chkUseField
            // 
            this.chkUseField.AutoSize = true;
            this.chkUseField.Location = new System.Drawing.Point(16, 216);
            this.chkUseField.Name = "chkUseField";
            this.chkUseField.Size = new System.Drawing.Size(109, 17);
            this.chkUseField.TabIndex = 14;
            this.chkUseField.Text = "Allow Use in Field";
            this.chkUseField.UseVisualStyleBackColor = true;
            // 
            // txtUseDesc
            // 
            this.txtUseDesc.Location = new System.Drawing.Point(16, 96);
            this.txtUseDesc.MaxLength = 1000;
            this.txtUseDesc.Multiline = true;
            this.txtUseDesc.Name = "txtUseDesc";
            this.txtUseDesc.Size = new System.Drawing.Size(256, 80);
            this.txtUseDesc.TabIndex = 12;
            // 
            // lblUseDesc
            // 
            this.lblUseDesc.AutoSize = true;
            this.lblUseDesc.Location = new System.Drawing.Point(16, 80);
            this.lblUseDesc.Name = "lblUseDesc";
            this.lblUseDesc.Size = new System.Drawing.Size(85, 13);
            this.lblUseDesc.TabIndex = 18;
            this.lblUseDesc.Text = "Use Description:";
            // 
            // cmdEditUseScript
            // 
            this.cmdEditUseScript.Location = new System.Drawing.Point(24, 32);
            this.cmdEditUseScript.Name = "cmdEditUseScript";
            this.cmdEditUseScript.Size = new System.Drawing.Size(152, 32);
            this.cmdEditUseScript.TabIndex = 11;
            this.cmdEditUseScript.Text = "Edit UseScript...";
            this.cmdEditUseScript.UseVisualStyleBackColor = true;
            this.cmdEditUseScript.Click += new System.EventHandler(this.cmdEditUseScript_Click);
            // 
            // dlgIconOpen
            // 
            this.dlgIconOpen.Filter = "PNG Image (*.png)|*.png";
            // 
            // FormDocumentItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 522);
            this.Controls.Add(this.fraWeapon);
            this.Controls.Add(this.fraGeneral);
            this.Controls.Add(this.fraUsable);
            this.Controls.Add(this.fraArmor);
            this.Name = "FormDocumentItem";
            this.Text = "frmDocumentItem";
            this.Load += new System.EventHandler(this.FormDocumentItem_Load);
            this.fraGeneral.ResumeLayout(false);
            this.fraGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).EndInit();
            this.fraWeapon.ResumeLayout(false);
            this.fraWeapon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeaponCritMult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeaponCritRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeaponAttack)).EndInit();
            this.fraArmor.ResumeLayout(false);
            this.fraArmor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudArmorDex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudArmorCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudArmorClass)).EndInit();
            this.fraUsable.ResumeLayout(false);
            this.fraUsable.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.GroupBox fraGeneral;
		private System.Windows.Forms.ComboBox cmbType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox chkQuest;
		private System.Windows.Forms.NumericUpDown nudValue;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtFlavor;
		private System.Windows.Forms.GroupBox fraWeapon;
		private System.Windows.Forms.NumericUpDown nudWeaponCritMult;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.NumericUpDown nudWeaponCritRange;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown nudWeaponAttack;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox cmbWeaponType;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtWeaponDamage;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox fraArmor;
		private System.Windows.Forms.TextBox txtGuid;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox comboBox4;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.NumericUpDown nudArmorDex;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.NumericUpDown nudArmorCheck;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.ComboBox cmbArmorType;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.NumericUpDown nudArmorClass;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.GroupBox fraUsable;
		private System.Windows.Forms.Button cmdEditUseScript;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Button cmdIcon;
		private System.Windows.Forms.Button cmdIconExport;
		private System.Windows.Forms.Button cmdIconClear;
		private System.Windows.Forms.OpenFileDialog dlgIconOpen;
		private System.Windows.Forms.CheckBox chkUseBattle;
		private System.Windows.Forms.CheckBox chkUseField;
		private System.Windows.Forms.TextBox txtUseDesc;
		private System.Windows.Forms.Label lblUseDesc;
		private System.Windows.Forms.TextBox txtAlias;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.CheckBox chkItemConsumable;
	}
}