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
            System.Windows.Forms.Label lblTitle;
            System.Windows.Forms.Label lblAuthor;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.GroupBox fraMeta;
            System.Windows.Forms.GroupBox fraStats;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.Label lblDepInfo;
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.cmdRandomGuid = new System.Windows.Forms.Button();
            this.lblStats = new System.Windows.Forms.Label();
            this.txtGUID = new System.Windows.Forms.TextBox();
            this.cmdDepRemove = new System.Windows.Forms.Button();
            this.cmdDepAdd = new System.Windows.Forms.Button();
            this.lsvDependencies = new System.Windows.Forms.ListView();
            this.clhDepFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhDepID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dlgSelectDep = new System.Windows.Forms.OpenFileDialog();
            lblTitle = new System.Windows.Forms.Label();
            lblAuthor = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            fraMeta = new System.Windows.Forms.GroupBox();
            fraStats = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            lblDepInfo = new System.Windows.Forms.Label();
            fraMeta.SuspendLayout();
            fraStats.SuspendLayout();
            groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new System.Drawing.Point(16, 24);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(64, 13);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Furball Title:";
            // 
            // lblAuthor
            // 
            lblAuthor.AutoSize = true;
            lblAuthor.Location = new System.Drawing.Point(16, 80);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new System.Drawing.Size(41, 13);
            lblAuthor.TabIndex = 2;
            lblAuthor.Text = "Author:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 24);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(55, 13);
            label1.TabIndex = 4;
            label1.Text = "Furball ID:";
            // 
            // fraMeta
            // 
            fraMeta.Controls.Add(this.txtAuthor);
            fraMeta.Controls.Add(lblTitle);
            fraMeta.Controls.Add(this.txtTitle);
            fraMeta.Controls.Add(lblAuthor);
            fraMeta.Location = new System.Drawing.Point(16, 16);
            fraMeta.Name = "fraMeta";
            fraMeta.Size = new System.Drawing.Size(352, 136);
            fraMeta.TabIndex = 6;
            fraMeta.TabStop = false;
            fraMeta.Text = "Basic Information";
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(16, 96);
            this.txtAuthor.MaxLength = 100;
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(320, 20);
            this.txtAuthor.TabIndex = 3;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(16, 40);
            this.txtTitle.MaxLength = 100;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(320, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // fraStats
            // 
            fraStats.Controls.Add(this.cmdRandomGuid);
            fraStats.Controls.Add(this.lblStats);
            fraStats.Controls.Add(label2);
            fraStats.Controls.Add(this.txtGUID);
            fraStats.Controls.Add(label1);
            fraStats.Location = new System.Drawing.Point(16, 168);
            fraStats.Name = "fraStats";
            fraStats.Size = new System.Drawing.Size(352, 136);
            fraStats.TabIndex = 7;
            fraStats.TabStop = false;
            fraStats.Text = "Stats for nerds";
            // 
            // cmdRandomGuid
            // 
            this.cmdRandomGuid.Location = new System.Drawing.Point(256, 16);
            this.cmdRandomGuid.Name = "cmdRandomGuid";
            this.cmdRandomGuid.Size = new System.Drawing.Size(80, 24);
            this.cmdRandomGuid.TabIndex = 8;
            this.cmdRandomGuid.Text = "Randomize";
            this.cmdRandomGuid.UseVisualStyleBackColor = true;
            this.cmdRandomGuid.Click += new System.EventHandler(this.cmdRandomGuid_Click);
            // 
            // lblStats
            // 
            this.lblStats.Location = new System.Drawing.Point(152, 80);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(184, 48);
            this.lblStats.TabIndex = 7;
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(16, 80);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(136, 48);
            label2.TabIndex = 6;
            label2.Text = "Number of Assets:\r\nNumber of Scene Nodes:\r\nTotal Word Count:";
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
            // groupBox1
            // 
            groupBox1.Controls.Add(this.cmdDepRemove);
            groupBox1.Controls.Add(this.cmdDepAdd);
            groupBox1.Controls.Add(this.lsvDependencies);
            groupBox1.Controls.Add(lblDepInfo);
            groupBox1.Location = new System.Drawing.Point(384, 16);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(320, 304);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Dependencies";
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
            // lsvDependencies
            // 
            this.lsvDependencies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhDepFile,
            this.clhDepID});
            this.lsvDependencies.FullRowSelect = true;
            this.lsvDependencies.GridLines = true;
            this.lsvDependencies.HideSelection = false;
            this.lsvDependencies.Location = new System.Drawing.Point(16, 88);
            this.lsvDependencies.MultiSelect = false;
            this.lsvDependencies.Name = "lsvDependencies";
            this.lsvDependencies.Size = new System.Drawing.Size(288, 152);
            this.lsvDependencies.TabIndex = 1;
            this.lsvDependencies.UseCompatibleStateImageBehavior = false;
            this.lsvDependencies.View = System.Windows.Forms.View.Details;
            this.lsvDependencies.SelectedIndexChanged += new System.EventHandler(this.lsvDependencies_SelectedIndexChanged);
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
            lblDepInfo.Location = new System.Drawing.Point(16, 24);
            lblDepInfo.Name = "lblDepInfo";
            lblDepInfo.Size = new System.Drawing.Size(288, 56);
            lblDepInfo.TabIndex = 0;
            lblDepInfo.Text = "Specify modules required by this module. They will be additionally loaded by the " +
    "editor for convenience.\r\n\r\nReload this module to make changes take effect.";
            // 
            // dlgSelectDep
            // 
            this.dlgSelectDep.Filter = "Finmer Asset Packages (*.furball)|*.furball";
            // 
            // FormDocumentProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 461);
            this.Controls.Add(groupBox1);
            this.Controls.Add(fraStats);
            this.Controls.Add(fraMeta);
            this.Name = "FormDocumentProject";
            this.Text = "Furball Settings";
            this.Load += new System.EventHandler(this.FormDocumentProject_Load);
            fraMeta.ResumeLayout(false);
            fraMeta.PerformLayout();
            fraStats.ResumeLayout(false);
            fraStats.PerformLayout();
            groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.TextBox txtAuthor;
		private System.Windows.Forms.TextBox txtGUID;
		private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Button cmdRandomGuid;
        private System.Windows.Forms.ListView lsvDependencies;
        private System.Windows.Forms.ColumnHeader clhDepFile;
        private System.Windows.Forms.ColumnHeader clhDepID;
        private System.Windows.Forms.Button cmdDepRemove;
        private System.Windows.Forms.Button cmdDepAdd;
        private System.Windows.Forms.OpenFileDialog dlgSelectDep;
    }
}