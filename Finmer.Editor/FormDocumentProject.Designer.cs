namespace Finmer.Editor
{
	partial class FormDocumentProject
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.txtGUID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fraMeta = new System.Windows.Forms.GroupBox();
            this.fraStats = new System.Windows.Forms.GroupBox();
            this.cmdRandomGuid = new System.Windows.Forms.Button();
            this.lblStats = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdDepRemove = new System.Windows.Forms.Button();
            this.cmdDepAdd = new System.Windows.Forms.Button();
            this.lsvDeps = new System.Windows.Forms.ListView();
            this.clhDepFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhDepID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblDepInfo = new System.Windows.Forms.Label();
            this.dlgSelectDep = new System.Windows.Forms.OpenFileDialog();
            this.fraMeta.SuspendLayout();
            this.fraStats.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(16, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(64, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Furball Title:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(16, 40);
            this.txtTitle.MaxLength = 100;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(320, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(16, 96);
            this.txtAuthor.MaxLength = 100;
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(320, 20);
            this.txtAuthor.TabIndex = 3;
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(16, 80);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(41, 13);
            this.lblAuthor.TabIndex = 2;
            this.lblAuthor.Text = "Author:";
            // 
            // txtGUID
            // 
            this.txtGUID.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGUID.Location = new System.Drawing.Point(16, 40);
            this.txtGUID.MaxLength = 100;
            this.txtGUID.Name = "txtGUID";
            this.txtGUID.ReadOnly = true;
            this.txtGUID.Size = new System.Drawing.Size(320, 21);
            this.txtGUID.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Furball ID:";
            // 
            // fraMeta
            // 
            this.fraMeta.Controls.Add(this.txtAuthor);
            this.fraMeta.Controls.Add(this.lblTitle);
            this.fraMeta.Controls.Add(this.txtTitle);
            this.fraMeta.Controls.Add(this.lblAuthor);
            this.fraMeta.Location = new System.Drawing.Point(16, 16);
            this.fraMeta.Name = "fraMeta";
            this.fraMeta.Size = new System.Drawing.Size(352, 136);
            this.fraMeta.TabIndex = 6;
            this.fraMeta.TabStop = false;
            this.fraMeta.Text = "Basic Information";
            // 
            // fraStats
            // 
            this.fraStats.Controls.Add(this.cmdRandomGuid);
            this.fraStats.Controls.Add(this.lblStats);
            this.fraStats.Controls.Add(this.label2);
            this.fraStats.Controls.Add(this.txtGUID);
            this.fraStats.Controls.Add(this.label1);
            this.fraStats.Location = new System.Drawing.Point(16, 168);
            this.fraStats.Name = "fraStats";
            this.fraStats.Size = new System.Drawing.Size(352, 176);
            this.fraStats.TabIndex = 7;
            this.fraStats.TabStop = false;
            this.fraStats.Text = "Stats for nerds";
            // 
            // cmdRandomGuid
            // 
            this.cmdRandomGuid.Location = new System.Drawing.Point(256, 16);
            this.cmdRandomGuid.Name = "cmdRandomGuid";
            this.cmdRandomGuid.Size = new System.Drawing.Size(80, 24);
            this.cmdRandomGuid.TabIndex = 8;
            this.cmdRandomGuid.Text = "Randomize";
            this.cmdRandomGuid.UseVisualStyleBackColor = true;
            this.cmdRandomGuid.Visible = false;
            this.cmdRandomGuid.Click += new System.EventHandler(this.cmdRandomGuid_Click);
            // 
            // lblStats
            // 
            this.lblStats.Location = new System.Drawing.Point(152, 80);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(184, 80);
            this.lblStats.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 80);
            this.label2.TabIndex = 6;
            this.label2.Text = "Number of Assets:\r\nNumber of Scene Nodes:\r\nNumber of String Sets:\r\nTotal Word Cou" +
    "nt:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdDepRemove);
            this.groupBox1.Controls.Add(this.cmdDepAdd);
            this.groupBox1.Controls.Add(this.lsvDeps);
            this.groupBox1.Controls.Add(this.lblDepInfo);
            this.groupBox1.Location = new System.Drawing.Point(384, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 304);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dependencies";
            // 
            // cmdDepRemove
            // 
            this.cmdDepRemove.Enabled = false;
            this.cmdDepRemove.Image = global::Finmer.Editor.Properties.Resources.cross_script;
            this.cmdDepRemove.Location = new System.Drawing.Point(200, 256);
            this.cmdDepRemove.Name = "cmdDepRemove";
            this.cmdDepRemove.Size = new System.Drawing.Size(104, 32);
            this.cmdDepRemove.TabIndex = 3;
            this.cmdDepRemove.Text = "Remove";
            this.cmdDepRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdDepRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdDepRemove.UseVisualStyleBackColor = true;
            this.cmdDepRemove.Click += new System.EventHandler(this.cmdDepRemove_Click);
            // 
            // cmdDepAdd
            // 
            this.cmdDepAdd.Image = global::Finmer.Editor.Properties.Resources.plus;
            this.cmdDepAdd.Location = new System.Drawing.Point(88, 256);
            this.cmdDepAdd.Name = "cmdDepAdd";
            this.cmdDepAdd.Size = new System.Drawing.Size(104, 32);
            this.cmdDepAdd.TabIndex = 2;
            this.cmdDepAdd.Text = "Add...";
            this.cmdDepAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdDepAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdDepAdd.UseVisualStyleBackColor = true;
            this.cmdDepAdd.Click += new System.EventHandler(this.cmdDepAdd_Click);
            // 
            // lsvDeps
            // 
            this.lsvDeps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhDepFile,
            this.clhDepID});
            this.lsvDeps.FullRowSelect = true;
            this.lsvDeps.GridLines = true;
            this.lsvDeps.Location = new System.Drawing.Point(16, 88);
            this.lsvDeps.MultiSelect = false;
            this.lsvDeps.Name = "lsvDeps";
            this.lsvDeps.Size = new System.Drawing.Size(288, 152);
            this.lsvDeps.TabIndex = 1;
            this.lsvDeps.UseCompatibleStateImageBehavior = false;
            this.lsvDeps.View = System.Windows.Forms.View.Details;
            this.lsvDeps.SelectedIndexChanged += new System.EventHandler(this.lsvDeps_SelectedIndexChanged);
            // 
            // clhDepFile
            // 
            this.clhDepFile.Text = "File Name";
            this.clhDepFile.Width = 110;
            // 
            // clhDepID
            // 
            this.clhDepID.Text = "Furball ID";
            this.clhDepID.Width = 150;
            // 
            // lblDepInfo
            // 
            this.lblDepInfo.Location = new System.Drawing.Point(16, 24);
            this.lblDepInfo.Name = "lblDepInfo";
            this.lblDepInfo.Size = new System.Drawing.Size(288, 56);
            this.lblDepInfo.TabIndex = 0;
            this.lblDepInfo.Text = "Specify modules required by this module. They will be additionally loaded by the " +
    "editor for convenience.\r\n\r\nReload this module to make changes take effect.";
            // 
            // dlgSelectDep
            // 
            this.dlgSelectDep.Filter = "Finmer Asset Packages (*.furball)|*.furball";
            // 
            // frmDocumentProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 461);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.fraStats);
            this.Controls.Add(this.fraMeta);
            this.Name = "frmDocumentProject";
            this.Text = "Furball Settings";
            this.Load += new System.EventHandler(this.FormDocumentProject_Load);
            this.fraMeta.ResumeLayout(false);
            this.fraMeta.PerformLayout();
            this.fraStats.ResumeLayout(false);
            this.fraStats.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.TextBox txtAuthor;
		private System.Windows.Forms.Label lblAuthor;
		private System.Windows.Forms.TextBox txtGUID;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox fraMeta;
		private System.Windows.Forms.GroupBox fraStats;
		private System.Windows.Forms.Label lblStats;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdRandomGuid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDepInfo;
        private System.Windows.Forms.ListView lsvDeps;
        private System.Windows.Forms.ColumnHeader clhDepFile;
        private System.Windows.Forms.ColumnHeader clhDepID;
        private System.Windows.Forms.Button cmdDepRemove;
        private System.Windows.Forms.Button cmdDepAdd;
        private System.Windows.Forms.OpenFileDialog dlgSelectDep;
    }
}