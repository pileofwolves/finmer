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
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudLevel = new System.Windows.Forms.NumericUpDown();
            this.nudWis = new System.Windows.Forms.NumericUpDown();
            this.nudCon = new System.Windows.Forms.NumericUpDown();
            this.nudDex = new System.Windows.Forms.NumericUpDown();
            this.nudStr = new System.Windows.Forms.NumericUpDown();
            this.txtGuid = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fraVoreSettings = new System.Windows.Forms.GroupBox();
            this.lblStatsSummary = new System.Windows.Forms.Label();
            this.lblVorePredness = new System.Windows.Forms.Label();
            this.chkVoreDigest = new System.Windows.Forms.CheckBox();
            this.nudVorePredness = new System.Windows.Forms.NumericUpDown();
            this.chkVorePred = new System.Windows.Forms.CheckBox();
            this.fraCombatFlags = new System.Windows.Forms.GroupBox();
            this.chkFlagFailVore = new System.Windows.Forms.CheckBox();
            this.chkFlagFailGrapple = new System.Windows.Forms.CheckBox();
            this.chkFlagSkipAOO = new System.Windows.Forms.CheckBox();
            this.chkFlagSkipTurns = new System.Windows.Forms.CheckBox();
            this.chkFlagNoXP = new System.Windows.Forms.CheckBox();
            this.chkFlagNoFight = new System.Windows.Forms.CheckBox();
            this.chkFlagNoVore = new System.Windows.Forms.CheckBox();
            this.chkFlagNoGrapple = new System.Windows.Forms.CheckBox();
            this.txtTags = new System.Windows.Forms.TextBox();
            this.fraGeneral = new System.Windows.Forms.GroupBox();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.fraTags = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStr)).BeginInit();
            this.fraVoreSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVorePredness)).BeginInit();
            this.fraCombatFlags.SuspendLayout();
            this.fraGeneral.SuspendLayout();
            this.fraTags.SuspendLayout();
            this.SuspendLayout();
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
            this.cmbSize.Location = new System.Drawing.Point(104, 200);
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new System.Drawing.Size(112, 21);
            this.cmbSize.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(104, 184);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Size Class:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(208, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Wits:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(144, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Body:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(80, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Agility:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Strength:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Level:";
            // 
            // nudLevel
            // 
            this.nudLevel.Location = new System.Drawing.Point(16, 200);
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
            this.nudLevel.TabIndex = 15;
            this.nudLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudWis
            // 
            this.nudWis.Location = new System.Drawing.Point(208, 152);
            this.nudWis.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudWis.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.nudWis.Name = "nudWis";
            this.nudWis.Size = new System.Drawing.Size(56, 20);
            this.nudWis.TabIndex = 13;
            // 
            // nudCon
            // 
            this.nudCon.Location = new System.Drawing.Point(144, 152);
            this.nudCon.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudCon.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.nudCon.Name = "nudCon";
            this.nudCon.Size = new System.Drawing.Size(56, 20);
            this.nudCon.TabIndex = 12;
            // 
            // nudDex
            // 
            this.nudDex.Location = new System.Drawing.Point(80, 152);
            this.nudDex.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudDex.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.nudDex.Name = "nudDex";
            this.nudDex.Size = new System.Drawing.Size(56, 20);
            this.nudDex.TabIndex = 11;
            // 
            // nudStr
            // 
            this.nudStr.Location = new System.Drawing.Point(16, 152);
            this.nudStr.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudStr.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.nudStr.Name = "nudStr";
            this.nudStr.Size = new System.Drawing.Size(56, 20);
            this.nudStr.TabIndex = 10;
            // 
            // txtGuid
            // 
            this.txtGuid.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGuid.Location = new System.Drawing.Point(16, 264);
            this.txtGuid.MaxLength = 100;
            this.txtGuid.Name = "txtGuid";
            this.txtGuid.ReadOnly = true;
            this.txtGuid.Size = new System.Drawing.Size(280, 21);
            this.txtGuid.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 248);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Asset GUID:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(16, 40);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(280, 20);
            this.txtName.TabIndex = 0;
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
            // fraVoreSettings
            // 
            this.fraVoreSettings.Controls.Add(this.lblStatsSummary);
            this.fraVoreSettings.Controls.Add(this.lblVorePredness);
            this.fraVoreSettings.Controls.Add(this.chkVoreDigest);
            this.fraVoreSettings.Controls.Add(this.nudVorePredness);
            this.fraVoreSettings.Controls.Add(this.chkVorePred);
            this.fraVoreSettings.Location = new System.Drawing.Point(16, 336);
            this.fraVoreSettings.Name = "fraVoreSettings";
            this.fraVoreSettings.Size = new System.Drawing.Size(312, 128);
            this.fraVoreSettings.TabIndex = 4;
            this.fraVoreSettings.TabStop = false;
            this.fraVoreSettings.Text = "Vore Stats";
            // 
            // lblStatsSummary
            // 
            this.lblStatsSummary.Location = new System.Drawing.Point(152, 24);
            this.lblStatsSummary.Name = "lblStatsSummary";
            this.lblStatsSummary.Size = new System.Drawing.Size(144, 88);
            this.lblStatsSummary.TabIndex = 27;
            this.lblStatsSummary.Text = "label11";
            // 
            // lblVorePredness
            // 
            this.lblVorePredness.AutoSize = true;
            this.lblVorePredness.Location = new System.Drawing.Point(16, 80);
            this.lblVorePredness.Name = "lblVorePredness";
            this.lblVorePredness.Size = new System.Drawing.Size(77, 13);
            this.lblVorePredness.TabIndex = 26;
            this.lblVorePredness.Text = "Predatoryness:";
            // 
            // chkVoreDigest
            // 
            this.chkVoreDigest.AutoSize = true;
            this.chkVoreDigest.Location = new System.Drawing.Point(16, 48);
            this.chkVoreDigest.Name = "chkVoreDigest";
            this.chkVoreDigest.Size = new System.Drawing.Size(85, 17);
            this.chkVoreDigest.TabIndex = 1;
            this.chkVoreDigest.Text = "Digests Prey";
            this.chkVoreDigest.UseVisualStyleBackColor = true;
            // 
            // nudVorePredness
            // 
            this.nudVorePredness.DecimalPlaces = 2;
            this.nudVorePredness.Location = new System.Drawing.Point(16, 96);
            this.nudVorePredness.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudVorePredness.Name = "nudVorePredness";
            this.nudVorePredness.Size = new System.Drawing.Size(88, 20);
            this.nudVorePredness.TabIndex = 25;
            this.nudVorePredness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkVorePred
            // 
            this.chkVorePred.AutoSize = true;
            this.chkVorePred.Location = new System.Drawing.Point(16, 24);
            this.chkVorePred.Name = "chkVorePred";
            this.chkVorePred.Size = new System.Drawing.Size(77, 17);
            this.chkVorePred.TabIndex = 0;
            this.chkVorePred.Text = "Is Predator";
            this.chkVorePred.UseVisualStyleBackColor = true;
            this.chkVorePred.CheckedChanged += new System.EventHandler(this.chkVorePred_CheckedChanged);
            // 
            // fraCombatFlags
            // 
            this.fraCombatFlags.Controls.Add(this.chkFlagFailVore);
            this.fraCombatFlags.Controls.Add(this.chkFlagFailGrapple);
            this.fraCombatFlags.Controls.Add(this.chkFlagSkipAOO);
            this.fraCombatFlags.Controls.Add(this.chkFlagSkipTurns);
            this.fraCombatFlags.Controls.Add(this.chkFlagNoXP);
            this.fraCombatFlags.Controls.Add(this.chkFlagNoFight);
            this.fraCombatFlags.Controls.Add(this.chkFlagNoVore);
            this.fraCombatFlags.Controls.Add(this.chkFlagNoGrapple);
            this.fraCombatFlags.Location = new System.Drawing.Point(344, 16);
            this.fraCombatFlags.Name = "fraCombatFlags";
            this.fraCombatFlags.Size = new System.Drawing.Size(352, 128);
            this.fraCombatFlags.TabIndex = 5;
            this.fraCombatFlags.TabStop = false;
            this.fraCombatFlags.Text = "Combat Flags";
            // 
            // chkFlagFailVore
            // 
            this.chkFlagFailVore.AutoSize = true;
            this.chkFlagFailVore.Location = new System.Drawing.Point(176, 96);
            this.chkFlagFailVore.Name = "chkFlagFailVore";
            this.chkFlagFailVore.Size = new System.Drawing.Size(142, 17);
            this.chkFlagFailVore.TabIndex = 7;
            this.chkFlagFailVore.Text = "Auto-fail All Vore Checks";
            this.chkFlagFailVore.UseVisualStyleBackColor = true;
            // 
            // chkFlagFailGrapple
            // 
            this.chkFlagFailGrapple.AutoSize = true;
            this.chkFlagFailGrapple.Location = new System.Drawing.Point(176, 72);
            this.chkFlagFailGrapple.Name = "chkFlagFailGrapple";
            this.chkFlagFailGrapple.Size = new System.Drawing.Size(157, 17);
            this.chkFlagFailGrapple.TabIndex = 6;
            this.chkFlagFailGrapple.Text = "Auto-fail All Grapple Checks";
            this.chkFlagFailGrapple.UseVisualStyleBackColor = true;
            // 
            // chkFlagSkipAOO
            // 
            this.chkFlagSkipAOO.AutoSize = true;
            this.chkFlagSkipAOO.Location = new System.Drawing.Point(176, 48);
            this.chkFlagSkipAOO.Name = "chkFlagSkipAOO";
            this.chkFlagSkipAOO.Size = new System.Drawing.Size(76, 17);
            this.chkFlagSkipAOO.TabIndex = 5;
            this.chkFlagSkipAOO.Text = "Skip AoOs";
            this.chkFlagSkipAOO.UseVisualStyleBackColor = true;
            // 
            // chkFlagSkipTurns
            // 
            this.chkFlagSkipTurns.AutoSize = true;
            this.chkFlagSkipTurns.Location = new System.Drawing.Point(176, 24);
            this.chkFlagSkipTurns.Name = "chkFlagSkipTurns";
            this.chkFlagSkipTurns.Size = new System.Drawing.Size(91, 17);
            this.chkFlagSkipTurns.TabIndex = 4;
            this.chkFlagSkipTurns.Text = "Skip All Turns";
            this.chkFlagSkipTurns.UseVisualStyleBackColor = true;
            // 
            // chkFlagNoXP
            // 
            this.chkFlagNoXP.AutoSize = true;
            this.chkFlagNoXP.Location = new System.Drawing.Point(16, 96);
            this.chkFlagNoXP.Name = "chkFlagNoXP";
            this.chkFlagNoXP.Size = new System.Drawing.Size(113, 17);
            this.chkFlagNoXP.TabIndex = 3;
            this.chkFlagNoXP.Text = "Does Not Give XP";
            this.chkFlagNoXP.UseVisualStyleBackColor = true;
            // 
            // chkFlagNoFight
            // 
            this.chkFlagNoFight.AutoSize = true;
            this.chkFlagNoFight.Location = new System.Drawing.Point(16, 72);
            this.chkFlagNoFight.Name = "chkFlagNoFight";
            this.chkFlagNoFight.Size = new System.Drawing.Size(122, 17);
            this.chkFlagNoFight.TabIndex = 2;
            this.chkFlagNoFight.Text = "Cannot Be Attacked";
            this.chkFlagNoFight.UseVisualStyleBackColor = true;
            // 
            // chkFlagNoVore
            // 
            this.chkFlagNoVore.AutoSize = true;
            this.chkFlagNoVore.Location = new System.Drawing.Point(16, 48);
            this.chkFlagNoVore.Name = "chkFlagNoVore";
            this.chkFlagNoVore.Size = new System.Drawing.Size(107, 17);
            this.chkFlagNoVore.TabIndex = 1;
            this.chkFlagNoVore.Text = "Cannot Be Vored";
            this.chkFlagNoVore.UseVisualStyleBackColor = true;
            // 
            // chkFlagNoGrapple
            // 
            this.chkFlagNoGrapple.AutoSize = true;
            this.chkFlagNoGrapple.Location = new System.Drawing.Point(16, 24);
            this.chkFlagNoGrapple.Name = "chkFlagNoGrapple";
            this.chkFlagNoGrapple.Size = new System.Drawing.Size(100, 17);
            this.chkFlagNoGrapple.TabIndex = 0;
            this.chkFlagNoGrapple.Text = "Cannot Grapple";
            this.chkFlagNoGrapple.UseVisualStyleBackColor = true;
            // 
            // txtTags
            // 
            this.txtTags.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTags.Location = new System.Drawing.Point(8, 24);
            this.txtTags.Multiline = true;
            this.txtTags.Name = "txtTags";
            this.txtTags.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTags.Size = new System.Drawing.Size(288, 72);
            this.txtTags.TabIndex = 0;
            // 
            // fraGeneral
            // 
            this.fraGeneral.Controls.Add(this.cmbGender);
            this.fraGeneral.Controls.Add(this.cmbSize);
            this.fraGeneral.Controls.Add(this.label11);
            this.fraGeneral.Controls.Add(this.label16);
            this.fraGeneral.Controls.Add(this.txtAlias);
            this.fraGeneral.Controls.Add(this.label9);
            this.fraGeneral.Controls.Add(this.label8);
            this.fraGeneral.Controls.Add(this.label7);
            this.fraGeneral.Controls.Add(this.label6);
            this.fraGeneral.Controls.Add(this.label5);
            this.fraGeneral.Controls.Add(this.label2);
            this.fraGeneral.Controls.Add(this.nudLevel);
            this.fraGeneral.Controls.Add(this.nudWis);
            this.fraGeneral.Controls.Add(this.nudCon);
            this.fraGeneral.Controls.Add(this.nudDex);
            this.fraGeneral.Controls.Add(this.nudStr);
            this.fraGeneral.Controls.Add(this.txtGuid);
            this.fraGeneral.Controls.Add(this.label10);
            this.fraGeneral.Controls.Add(this.txtName);
            this.fraGeneral.Controls.Add(this.label1);
            this.fraGeneral.Location = new System.Drawing.Point(16, 16);
            this.fraGeneral.Name = "fraGeneral";
            this.fraGeneral.Size = new System.Drawing.Size(312, 304);
            this.fraGeneral.TabIndex = 3;
            this.fraGeneral.TabStop = false;
            this.fraGeneral.Text = "General";
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Neuter",
            "Herm"});
            this.cmbGender.Location = new System.Drawing.Point(200, 88);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(96, 21);
            this.cmbGender.TabIndex = 30;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(200, 72);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 13);
            this.label16.TabIndex = 29;
            this.label16.Text = "Gender:";
            // 
            // txtAlias
            // 
            this.txtAlias.Location = new System.Drawing.Point(16, 88);
            this.txtAlias.MaxLength = 100;
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(176, 20);
            this.txtAlias.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "Alias:";
            // 
            // fraTags
            // 
            this.fraTags.Controls.Add(this.txtTags);
            this.fraTags.Location = new System.Drawing.Point(16, 480);
            this.fraTags.Name = "fraTags";
            this.fraTags.Size = new System.Drawing.Size(312, 104);
            this.fraTags.TabIndex = 7;
            this.fraTags.TabStop = false;
            this.fraTags.Text = "Script Tags (one per line)";
            // 
            // FormDocumentCreature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 601);
            this.Controls.Add(this.fraTags);
            this.Controls.Add(this.fraCombatFlags);
            this.Controls.Add(this.fraVoreSettings);
            this.Controls.Add(this.fraGeneral);
            this.Name = "FormDocumentCreature";
            this.Text = "frmDocumentCreature";
            this.Load += new System.EventHandler(this.FormDocumentCreature_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStr)).EndInit();
            this.fraVoreSettings.ResumeLayout(false);
            this.fraVoreSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVorePredness)).EndInit();
            this.fraCombatFlags.ResumeLayout(false);
            this.fraCombatFlags.PerformLayout();
            this.fraGeneral.ResumeLayout(false);
            this.fraGeneral.PerformLayout();
            this.fraTags.ResumeLayout(false);
            this.fraTags.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TextBox txtGuid;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown nudLevel;
		private System.Windows.Forms.NumericUpDown nudWis;
		private System.Windows.Forms.NumericUpDown nudCon;
		private System.Windows.Forms.NumericUpDown nudDex;
		private System.Windows.Forms.NumericUpDown nudStr;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox fraVoreSettings;
		private System.Windows.Forms.CheckBox chkVoreDigest;
		private System.Windows.Forms.CheckBox chkVorePred;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblVorePredness;
		private System.Windows.Forms.NumericUpDown nudVorePredness;
		private System.Windows.Forms.Label lblStatsSummary;
		private System.Windows.Forms.ComboBox cmbSize;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.GroupBox fraCombatFlags;
		private System.Windows.Forms.CheckBox chkFlagNoFight;
		private System.Windows.Forms.CheckBox chkFlagNoVore;
		private System.Windows.Forms.CheckBox chkFlagNoGrapple;
		private System.Windows.Forms.CheckBox chkFlagFailVore;
		private System.Windows.Forms.CheckBox chkFlagFailGrapple;
		private System.Windows.Forms.CheckBox chkFlagSkipAOO;
		private System.Windows.Forms.CheckBox chkFlagSkipTurns;
		private System.Windows.Forms.CheckBox chkFlagNoXP;
		private System.Windows.Forms.TextBox txtTags;
		private System.Windows.Forms.GroupBox fraGeneral;
		private System.Windows.Forms.GroupBox fraTags;
		private System.Windows.Forms.TextBox txtAlias;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox cmbGender;
		private System.Windows.Forms.Label label16;
    }
}