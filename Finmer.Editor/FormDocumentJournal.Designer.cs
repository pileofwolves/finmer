namespace Finmer.Editor
{
	partial class FormDocumentJournal
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
			this.fraGeneral = new System.Windows.Forms.GroupBox();
			this.txtGuid = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.fraEntries = new System.Windows.Forms.GroupBox();
			this.nudEntryKey = new System.Windows.Forms.NumericUpDown();
			this.lblEntryKey = new System.Windows.Forms.Label();
			this.lstEntries = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmdRemove = new System.Windows.Forms.Button();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.lblEntryText = new System.Windows.Forms.Label();
			this.txtEntryText = new System.Windows.Forms.TextBox();
			this.fraGeneral.SuspendLayout();
			this.fraEntries.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudEntryKey)).BeginInit();
			this.SuspendLayout();
			// 
			// fraGeneral
			// 
			this.fraGeneral.Controls.Add(this.txtGuid);
			this.fraGeneral.Controls.Add(this.label10);
			this.fraGeneral.Controls.Add(this.txtName);
			this.fraGeneral.Controls.Add(this.label1);
			this.fraGeneral.Location = new System.Drawing.Point(16, 16);
			this.fraGeneral.Name = "fraGeneral";
			this.fraGeneral.Size = new System.Drawing.Size(296, 128);
			this.fraGeneral.TabIndex = 0;
			this.fraGeneral.TabStop = false;
			this.fraGeneral.Text = "General";
			// 
			// txtGuid
			// 
			this.txtGuid.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtGuid.Location = new System.Drawing.Point(16, 88);
			this.txtGuid.MaxLength = 100;
			this.txtGuid.Name = "txtGuid";
			this.txtGuid.ReadOnly = true;
			this.txtGuid.Size = new System.Drawing.Size(264, 21);
			this.txtGuid.TabIndex = 2;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(16, 72);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(66, 13);
			this.label10.TabIndex = 15;
			this.label10.Text = "Asset GUID:";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(16, 40);
			this.txtName.MaxLength = 100;
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(264, 20);
			this.txtName.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Quest Summary:";
			// 
			// fraEntries
			// 
			this.fraEntries.Controls.Add(this.nudEntryKey);
			this.fraEntries.Controls.Add(this.lblEntryKey);
			this.fraEntries.Controls.Add(this.lstEntries);
			this.fraEntries.Controls.Add(this.cmdRemove);
			this.fraEntries.Controls.Add(this.cmdAdd);
			this.fraEntries.Controls.Add(this.lblEntryText);
			this.fraEntries.Controls.Add(this.txtEntryText);
			this.fraEntries.Location = new System.Drawing.Point(16, 160);
			this.fraEntries.Name = "fraEntries";
			this.fraEntries.Size = new System.Drawing.Size(664, 360);
			this.fraEntries.TabIndex = 1;
			this.fraEntries.TabStop = false;
			this.fraEntries.Text = "Entries";
			// 
			// nudEntryKey
			// 
			this.nudEntryKey.Enabled = false;
			this.nudEntryKey.Location = new System.Drawing.Point(16, 192);
			this.nudEntryKey.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.nudEntryKey.Name = "nudEntryKey";
			this.nudEntryKey.Size = new System.Drawing.Size(104, 20);
			this.nudEntryKey.TabIndex = 22;
			this.nudEntryKey.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// lblEntryKey
			// 
			this.lblEntryKey.AutoSize = true;
			this.lblEntryKey.Enabled = false;
			this.lblEntryKey.Location = new System.Drawing.Point(16, 176);
			this.lblEntryKey.Name = "lblEntryKey";
			this.lblEntryKey.Size = new System.Drawing.Size(48, 13);
			this.lblEntryKey.TabIndex = 21;
			this.lblEntryKey.Text = "Entry ID:";
			// 
			// lstEntries
			// 
			this.lstEntries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.lstEntries.FullRowSelect = true;
			this.lstEntries.GridLines = true;
			this.lstEntries.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lstEntries.Location = new System.Drawing.Point(16, 24);
			this.lstEntries.MultiSelect = false;
			this.lstEntries.Name = "lstEntries";
			this.lstEntries.Size = new System.Drawing.Size(632, 136);
			this.lstEntries.TabIndex = 3;
			this.lstEntries.UseCompatibleStateImageBehavior = false;
			this.lstEntries.View = System.Windows.Forms.View.Details;
			this.lstEntries.SelectedIndexChanged += new System.EventHandler(this.lstEntries_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 600;
			// 
			// cmdRemove
			// 
			this.cmdRemove.Enabled = false;
			this.cmdRemove.Image = global::Finmer.Editor.Properties.Resources.cross_script;
			this.cmdRemove.Location = new System.Drawing.Point(544, 304);
			this.cmdRemove.Name = "cmdRemove";
			this.cmdRemove.Size = new System.Drawing.Size(104, 32);
			this.cmdRemove.TabIndex = 7;
			this.cmdRemove.Text = "Remove";
			this.cmdRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.cmdRemove.UseVisualStyleBackColor = true;
			this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
			// 
			// cmdAdd
			// 
			this.cmdAdd.Image = global::Finmer.Editor.Properties.Resources.plus;
			this.cmdAdd.Location = new System.Drawing.Point(432, 304);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(104, 32);
			this.cmdAdd.TabIndex = 6;
			this.cmdAdd.Text = "Add";
			this.cmdAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.cmdAdd.UseVisualStyleBackColor = true;
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			// 
			// lblEntryText
			// 
			this.lblEntryText.AutoSize = true;
			this.lblEntryText.Enabled = false;
			this.lblEntryText.Location = new System.Drawing.Point(16, 224);
			this.lblEntryText.Name = "lblEntryText";
			this.lblEntryText.Size = new System.Drawing.Size(58, 13);
			this.lblEntryText.TabIndex = 16;
			this.lblEntryText.Text = "Entry Text:";
			// 
			// txtEntryText
			// 
			this.txtEntryText.Enabled = false;
			this.txtEntryText.Location = new System.Drawing.Point(16, 240);
			this.txtEntryText.Multiline = true;
			this.txtEntryText.Name = "txtEntryText";
			this.txtEntryText.Size = new System.Drawing.Size(632, 48);
			this.txtEntryText.TabIndex = 5;
			// 
			// frmDocumentJournal
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(697, 601);
			this.Controls.Add(this.fraEntries);
			this.Controls.Add(this.fraGeneral);
			this.Name = "frmDocumentJournal";
			this.Text = "frmDocumentFeat";
			this.Load += new System.EventHandler(this.FormDocumentJournal_Load);
			this.fraGeneral.ResumeLayout(false);
			this.fraGeneral.PerformLayout();
			this.fraEntries.ResumeLayout(false);
			this.fraEntries.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudEntryKey)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox fraGeneral;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtGuid;
		private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox fraEntries;
        private System.Windows.Forms.Label lblEntryText;
        private System.Windows.Forms.TextBox txtEntryText;
        private System.Windows.Forms.Button cmdRemove;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.ListView lstEntries;
        private System.Windows.Forms.Label lblEntryKey;
        private System.Windows.Forms.NumericUpDown nudEntryKey;
		private System.Windows.Forms.ColumnHeader columnHeader1;
	}
}