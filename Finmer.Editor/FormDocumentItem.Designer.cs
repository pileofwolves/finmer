﻿namespace Finmer.Editor
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label18;
            System.Windows.Forms.Label label17;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label13;
            System.Windows.Forms.ColumnHeader clhEquipEffectTitle;
            this.txtName = new System.Windows.Forms.TextBox();
            this.fraGeneral = new System.Windows.Forms.GroupBox();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.cmdIconExport = new System.Windows.Forms.Button();
            this.cmdIconClear = new System.Windows.Forms.Button();
            this.cmdIcon = new System.Windows.Forms.Button();
            this.txtGuid = new System.Windows.Forms.TextBox();
            this.txtFlavor = new System.Windows.Forms.TextBox();
            this.chkQuest = new System.Windows.Forms.CheckBox();
            this.nudValue = new System.Windows.Forms.NumericUpDown();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.fraEquipment = new System.Windows.Forms.GroupBox();
            this.lsvEquipEffectGroups = new System.Windows.Forms.ListView();
            this.cmdEquipEffectEdit = new System.Windows.Forms.Button();
            this.cmdEquipEffectRemove = new System.Windows.Forms.Button();
            this.cmdEquipEffectAdd = new System.Windows.Forms.Button();
            this.cmbEquipSlot = new System.Windows.Forms.ComboBox();
            this.fraUsable = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkItemConsumable = new System.Windows.Forms.CheckBox();
            this.chkUseBattle = new System.Windows.Forms.CheckBox();
            this.chkUseField = new System.Windows.Forms.CheckBox();
            this.txtUseDesc = new System.Windows.Forms.TextBox();
            this.lblUseDesc = new System.Windows.Forms.Label();
            this.cmdEditUseScript = new System.Windows.Forms.Button();
            this.dlgIconOpen = new System.Windows.Forms.OpenFileDialog();
            label1 = new System.Windows.Forms.Label();
            label18 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            clhEquipEffectTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fraGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).BeginInit();
            this.fraEquipment.SuspendLayout();
            this.fraUsable.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 24);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(61, 13);
            label1.TabIndex = 0;
            label1.Text = "Item Name:";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new System.Drawing.Point(16, 72);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(32, 13);
            label18.TabIndex = 2;
            label18.Text = "Alias:";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new System.Drawing.Point(240, 120);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(31, 13);
            label17.TabIndex = 6;
            label17.Text = "Icon:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(16, 368);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(66, 13);
            label10.TabIndex = 15;
            label10.Text = "Asset GUID:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(16, 256);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(63, 13);
            label11.TabIndex = 13;
            label11.Text = "Flavor Text:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(16, 168);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(37, 13);
            label3.TabIndex = 10;
            label3.Text = "Value:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 120);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(34, 13);
            label2.TabIndex = 4;
            label2.Text = "Type:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(16, 24);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(58, 13);
            label13.TabIndex = 0;
            label13.Text = "Equip Slot:";
            // 
            // clhEquipEffectTitle
            // 
            clhEquipEffectTitle.Text = "Effect";
            clhEquipEffectTitle.Width = 250;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(16, 40);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(264, 20);
            this.txtName.TabIndex = 1;
            // 
            // fraGeneral
            // 
            this.fraGeneral.Controls.Add(this.txtAlias);
            this.fraGeneral.Controls.Add(label18);
            this.fraGeneral.Controls.Add(this.cmdIconExport);
            this.fraGeneral.Controls.Add(this.cmdIconClear);
            this.fraGeneral.Controls.Add(label17);
            this.fraGeneral.Controls.Add(this.cmdIcon);
            this.fraGeneral.Controls.Add(this.txtGuid);
            this.fraGeneral.Controls.Add(label10);
            this.fraGeneral.Controls.Add(label11);
            this.fraGeneral.Controls.Add(this.txtFlavor);
            this.fraGeneral.Controls.Add(this.chkQuest);
            this.fraGeneral.Controls.Add(this.nudValue);
            this.fraGeneral.Controls.Add(label3);
            this.fraGeneral.Controls.Add(this.cmbType);
            this.fraGeneral.Controls.Add(label2);
            this.fraGeneral.Controls.Add(this.txtName);
            this.fraGeneral.Controls.Add(label1);
            this.fraGeneral.Location = new System.Drawing.Point(16, 16);
            this.fraGeneral.Name = "fraGeneral";
            this.fraGeneral.Size = new System.Drawing.Size(296, 424);
            this.fraGeneral.TabIndex = 0;
            this.fraGeneral.TabStop = false;
            this.fraGeneral.Text = "General";
            // 
            // txtAlias
            // 
            this.txtAlias.Location = new System.Drawing.Point(16, 88);
            this.txtAlias.MaxLength = 100;
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(264, 20);
            this.txtAlias.TabIndex = 3;
            // 
            // cmdIconExport
            // 
            this.cmdIconExport.Image = global::Finmer.Editor.Properties.Resources.folder_export;
            this.cmdIconExport.Location = new System.Drawing.Point(240, 184);
            this.cmdIconExport.Name = "cmdIconExport";
            this.cmdIconExport.Size = new System.Drawing.Size(24, 24);
            this.cmdIconExport.TabIndex = 8;
            this.cmdIconExport.UseVisualStyleBackColor = true;
            // 
            // cmdIconClear
            // 
            this.cmdIconClear.Image = global::Finmer.Editor.Properties.Resources.cross_script;
            this.cmdIconClear.Location = new System.Drawing.Point(264, 184);
            this.cmdIconClear.Name = "cmdIconClear";
            this.cmdIconClear.Size = new System.Drawing.Size(24, 24);
            this.cmdIconClear.TabIndex = 9;
            this.cmdIconClear.UseVisualStyleBackColor = true;
            this.cmdIconClear.Click += new System.EventHandler(this.cmdIconClear_Click);
            // 
            // cmdIcon
            // 
            this.cmdIcon.Location = new System.Drawing.Point(240, 136);
            this.cmdIcon.Name = "cmdIcon";
            this.cmdIcon.Size = new System.Drawing.Size(48, 48);
            this.cmdIcon.TabIndex = 7;
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
            this.txtGuid.TabIndex = 16;
            // 
            // txtFlavor
            // 
            this.txtFlavor.Location = new System.Drawing.Point(16, 272);
            this.txtFlavor.MaxLength = 1000;
            this.txtFlavor.Multiline = true;
            this.txtFlavor.Name = "txtFlavor";
            this.txtFlavor.Size = new System.Drawing.Size(264, 88);
            this.txtFlavor.TabIndex = 14;
            // 
            // chkQuest
            // 
            this.chkQuest.AutoSize = true;
            this.chkQuest.Location = new System.Drawing.Point(16, 224);
            this.chkQuest.Name = "chkQuest";
            this.chkQuest.Size = new System.Drawing.Size(77, 17);
            this.chkQuest.TabIndex = 12;
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
            this.nudValue.TabIndex = 11;
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
            this.cmbType.TabIndex = 5;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // fraEquipment
            // 
            this.fraEquipment.Controls.Add(this.lsvEquipEffectGroups);
            this.fraEquipment.Controls.Add(this.cmdEquipEffectEdit);
            this.fraEquipment.Controls.Add(this.cmdEquipEffectRemove);
            this.fraEquipment.Controls.Add(this.cmdEquipEffectAdd);
            this.fraEquipment.Controls.Add(this.cmbEquipSlot);
            this.fraEquipment.Controls.Add(label13);
            this.fraEquipment.Location = new System.Drawing.Point(328, 16);
            this.fraEquipment.Name = "fraEquipment";
            this.fraEquipment.Size = new System.Drawing.Size(304, 424);
            this.fraEquipment.TabIndex = 2;
            this.fraEquipment.TabStop = false;
            this.fraEquipment.Text = "Equipment";
            // 
            // lsvEquipEffectGroups
            // 
            this.lsvEquipEffectGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clhEquipEffectTitle});
            this.lsvEquipEffectGroups.FullRowSelect = true;
            this.lsvEquipEffectGroups.GridLines = true;
            this.lsvEquipEffectGroups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvEquipEffectGroups.HideSelection = false;
            this.lsvEquipEffectGroups.Location = new System.Drawing.Point(16, 112);
            this.lsvEquipEffectGroups.Name = "lsvEquipEffectGroups";
            this.lsvEquipEffectGroups.Size = new System.Drawing.Size(272, 296);
            this.lsvEquipEffectGroups.TabIndex = 5;
            this.lsvEquipEffectGroups.UseCompatibleStateImageBehavior = false;
            this.lsvEquipEffectGroups.View = System.Windows.Forms.View.Details;
            this.lsvEquipEffectGroups.SelectedIndexChanged += new System.EventHandler(this.lsvEquipEffectGroups_SelectedIndexChanged);
            this.lsvEquipEffectGroups.DoubleClick += new System.EventHandler(this.lsvEquipEffectGroups_DoubleClick);
            // 
            // cmdEquipEffectEdit
            // 
            this.cmdEquipEffectEdit.Enabled = false;
            this.cmdEquipEffectEdit.Image = global::Finmer.Editor.Properties.Resources.pencil;
            this.cmdEquipEffectEdit.Location = new System.Drawing.Point(208, 72);
            this.cmdEquipEffectEdit.Name = "cmdEquipEffectEdit";
            this.cmdEquipEffectEdit.Size = new System.Drawing.Size(80, 32);
            this.cmdEquipEffectEdit.TabIndex = 4;
            this.cmdEquipEffectEdit.Text = "Edit";
            this.cmdEquipEffectEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdEquipEffectEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdEquipEffectEdit.UseVisualStyleBackColor = true;
            this.cmdEquipEffectEdit.Click += new System.EventHandler(this.cmdEquipEffectEdit_Click);
            // 
            // cmdEquipEffectRemove
            // 
            this.cmdEquipEffectRemove.Enabled = false;
            this.cmdEquipEffectRemove.Image = global::Finmer.Editor.Properties.Resources.cross_script;
            this.cmdEquipEffectRemove.Location = new System.Drawing.Point(120, 72);
            this.cmdEquipEffectRemove.Name = "cmdEquipEffectRemove";
            this.cmdEquipEffectRemove.Size = new System.Drawing.Size(80, 32);
            this.cmdEquipEffectRemove.TabIndex = 3;
            this.cmdEquipEffectRemove.Text = "Remove";
            this.cmdEquipEffectRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdEquipEffectRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdEquipEffectRemove.UseVisualStyleBackColor = true;
            this.cmdEquipEffectRemove.Click += new System.EventHandler(this.cmdEquipEffectRemove_Click);
            // 
            // cmdEquipEffectAdd
            // 
            this.cmdEquipEffectAdd.Image = global::Finmer.Editor.Properties.Resources.plus;
            this.cmdEquipEffectAdd.Location = new System.Drawing.Point(16, 72);
            this.cmdEquipEffectAdd.Name = "cmdEquipEffectAdd";
            this.cmdEquipEffectAdd.Size = new System.Drawing.Size(96, 32);
            this.cmdEquipEffectAdd.TabIndex = 2;
            this.cmdEquipEffectAdd.Text = "Add Group";
            this.cmdEquipEffectAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdEquipEffectAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdEquipEffectAdd.UseVisualStyleBackColor = true;
            this.cmdEquipEffectAdd.Click += new System.EventHandler(this.cmdEquipEffectAdd_Click);
            // 
            // cmbEquipSlot
            // 
            this.cmbEquipSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEquipSlot.FormattingEnabled = true;
            this.cmbEquipSlot.Items.AddRange(new object[] {
            "Weapon",
            "Armor",
            "Accessory"});
            this.cmbEquipSlot.Location = new System.Drawing.Point(16, 40);
            this.cmbEquipSlot.Name = "cmbEquipSlot";
            this.cmbEquipSlot.Size = new System.Drawing.Size(272, 21);
            this.cmbEquipSlot.TabIndex = 1;
            // 
            // fraUsable
            // 
            this.fraUsable.Controls.Add(this.label4);
            this.fraUsable.Controls.Add(this.chkItemConsumable);
            this.fraUsable.Controls.Add(this.chkUseBattle);
            this.fraUsable.Controls.Add(this.chkUseField);
            this.fraUsable.Controls.Add(this.txtUseDesc);
            this.fraUsable.Controls.Add(this.lblUseDesc);
            this.fraUsable.Controls.Add(this.cmdEditUseScript);
            this.fraUsable.Location = new System.Drawing.Point(328, 16);
            this.fraUsable.Name = "fraUsable";
            this.fraUsable.Size = new System.Drawing.Size(288, 280);
            this.fraUsable.TabIndex = 1;
            this.fraUsable.TabStop = false;
            this.fraUsable.Text = "Usable";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Item Script:";
            // 
            // chkItemConsumable
            // 
            this.chkItemConsumable.AutoSize = true;
            this.chkItemConsumable.Location = new System.Drawing.Point(16, 192);
            this.chkItemConsumable.Name = "chkItemConsumable";
            this.chkItemConsumable.Size = new System.Drawing.Size(190, 17);
            this.chkItemConsumable.TabIndex = 4;
            this.chkItemConsumable.Text = "Consumable (Disappears after use)";
            this.chkItemConsumable.UseVisualStyleBackColor = true;
            // 
            // chkUseBattle
            // 
            this.chkUseBattle.AutoSize = true;
            this.chkUseBattle.Location = new System.Drawing.Point(16, 240);
            this.chkUseBattle.Name = "chkUseBattle";
            this.chkUseBattle.Size = new System.Drawing.Size(114, 17);
            this.chkUseBattle.TabIndex = 6;
            this.chkUseBattle.Text = "Allow Use in Battle";
            this.chkUseBattle.UseVisualStyleBackColor = true;
            // 
            // chkUseField
            // 
            this.chkUseField.AutoSize = true;
            this.chkUseField.Location = new System.Drawing.Point(16, 216);
            this.chkUseField.Name = "chkUseField";
            this.chkUseField.Size = new System.Drawing.Size(109, 17);
            this.chkUseField.TabIndex = 5;
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
            this.txtUseDesc.TabIndex = 3;
            // 
            // lblUseDesc
            // 
            this.lblUseDesc.AutoSize = true;
            this.lblUseDesc.Location = new System.Drawing.Point(16, 80);
            this.lblUseDesc.Name = "lblUseDesc";
            this.lblUseDesc.Size = new System.Drawing.Size(85, 13);
            this.lblUseDesc.TabIndex = 2;
            this.lblUseDesc.Text = "Use Description:";
            // 
            // cmdEditUseScript
            // 
            this.cmdEditUseScript.Image = global::Finmer.Editor.Properties.Resources.pencil;
            this.cmdEditUseScript.Location = new System.Drawing.Point(16, 40);
            this.cmdEditUseScript.Name = "cmdEditUseScript";
            this.cmdEditUseScript.Size = new System.Drawing.Size(144, 32);
            this.cmdEditUseScript.TabIndex = 1;
            this.cmdEditUseScript.Text = "Edit UseScript...";
            this.cmdEditUseScript.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdEditUseScript.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
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
            this.ClientSize = new System.Drawing.Size(649, 457);
            this.Controls.Add(this.fraEquipment);
            this.Controls.Add(this.fraGeneral);
            this.Controls.Add(this.fraUsable);
            this.Name = "FormDocumentItem";
            this.Text = "FormDocumentItem";
            this.Load += new System.EventHandler(this.FormDocumentItem_Load);
            this.fraGeneral.ResumeLayout(false);
            this.fraGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).EndInit();
            this.fraEquipment.ResumeLayout(false);
            this.fraEquipment.PerformLayout();
            this.fraUsable.ResumeLayout(false);
            this.fraUsable.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.GroupBox fraGeneral;
		private System.Windows.Forms.ComboBox cmbType;
		private System.Windows.Forms.CheckBox chkQuest;
		private System.Windows.Forms.NumericUpDown nudValue;
		private System.Windows.Forms.TextBox txtFlavor;
		private System.Windows.Forms.GroupBox fraEquipment;
		private System.Windows.Forms.TextBox txtGuid;
		private System.Windows.Forms.ComboBox cmbEquipSlot;
		private System.Windows.Forms.GroupBox fraUsable;
		private System.Windows.Forms.Button cmdEditUseScript;
		private System.Windows.Forms.Button cmdIcon;
		private System.Windows.Forms.Button cmdIconExport;
		private System.Windows.Forms.Button cmdIconClear;
		private System.Windows.Forms.OpenFileDialog dlgIconOpen;
		private System.Windows.Forms.CheckBox chkUseBattle;
		private System.Windows.Forms.CheckBox chkUseField;
		private System.Windows.Forms.TextBox txtUseDesc;
		private System.Windows.Forms.Label lblUseDesc;
		private System.Windows.Forms.TextBox txtAlias;
		private System.Windows.Forms.CheckBox chkItemConsumable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdEquipEffectAdd;
        private System.Windows.Forms.Button cmdEquipEffectEdit;
        private System.Windows.Forms.Button cmdEquipEffectRemove;
        private System.Windows.Forms.ListView lsvEquipEffectGroups;
    }
}