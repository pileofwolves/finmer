
namespace Finmer.Editor
{
    partial class FormPreferences
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
            if (disposing && (components != null))
            {
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
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.grpLaunch = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdBrowseExeWorkDir = new System.Windows.Forms.Button();
            this.txtExeWorkDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdBrowseExePath = new System.Windows.Forms.Button();
            this.txtExePath = new System.Windows.Forms.TextBox();
            this.lblExeLocation = new System.Windows.Forms.Label();
            this.dlgBrowseWorkDir = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgBrowseExe = new System.Windows.Forms.OpenFileDialog();
            this.grpLaunch.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Image = global::Finmer.Editor.Properties.Resources.cross;
            this.cmdCancel.Location = new System.Drawing.Point(304, 216);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(112, 32);
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdOK.Location = new System.Drawing.Point(184, 216);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(112, 32);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "Accept";
            this.cmdOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // grpLaunch
            // 
            this.grpLaunch.Controls.Add(this.label3);
            this.grpLaunch.Controls.Add(this.label2);
            this.grpLaunch.Controls.Add(this.cmdBrowseExeWorkDir);
            this.grpLaunch.Controls.Add(this.txtExeWorkDir);
            this.grpLaunch.Controls.Add(this.label1);
            this.grpLaunch.Controls.Add(this.cmdBrowseExePath);
            this.grpLaunch.Controls.Add(this.txtExePath);
            this.grpLaunch.Controls.Add(this.lblExeLocation);
            this.grpLaunch.Location = new System.Drawing.Point(16, 16);
            this.grpLaunch.Name = "grpLaunch";
            this.grpLaunch.Size = new System.Drawing.Size(400, 160);
            this.grpLaunch.TabIndex = 5;
            this.grpLaunch.TabStop = false;
            this.grpLaunch.Text = "Launch Settings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(24, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(332, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Optional. Folder where the game will look for save data and Modules.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(24, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(244, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "The executable to launch when starting the game.";
            // 
            // cmdBrowseExeWorkDir
            // 
            this.cmdBrowseExeWorkDir.Location = new System.Drawing.Point(352, 120);
            this.cmdBrowseExeWorkDir.Name = "cmdBrowseExeWorkDir";
            this.cmdBrowseExeWorkDir.Size = new System.Drawing.Size(32, 20);
            this.cmdBrowseExeWorkDir.TabIndex = 5;
            this.cmdBrowseExeWorkDir.Text = "...";
            this.cmdBrowseExeWorkDir.UseVisualStyleBackColor = true;
            this.cmdBrowseExeWorkDir.Click += new System.EventHandler(this.cmdBrowseExeWorkDir_Click);
            // 
            // txtExeWorkDir
            // 
            this.txtExeWorkDir.Location = new System.Drawing.Point(16, 120);
            this.txtExeWorkDir.Name = "txtExeWorkDir";
            this.txtExeWorkDir.Size = new System.Drawing.Size(336, 20);
            this.txtExeWorkDir.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Working Directory:";
            // 
            // cmdBrowseExePath
            // 
            this.cmdBrowseExePath.Location = new System.Drawing.Point(352, 56);
            this.cmdBrowseExePath.Name = "cmdBrowseExePath";
            this.cmdBrowseExePath.Size = new System.Drawing.Size(32, 20);
            this.cmdBrowseExePath.TabIndex = 2;
            this.cmdBrowseExePath.Text = "...";
            this.cmdBrowseExePath.UseVisualStyleBackColor = true;
            this.cmdBrowseExePath.Click += new System.EventHandler(this.cmdBrowseExePath_Click);
            // 
            // txtExePath
            // 
            this.txtExePath.Location = new System.Drawing.Point(16, 56);
            this.txtExePath.Name = "txtExePath";
            this.txtExePath.Size = new System.Drawing.Size(336, 20);
            this.txtExePath.TabIndex = 1;
            // 
            // lblExeLocation
            // 
            this.lblExeLocation.AutoSize = true;
            this.lblExeLocation.Location = new System.Drawing.Point(16, 24);
            this.lblExeLocation.Name = "lblExeLocation";
            this.lblExeLocation.Size = new System.Drawing.Size(124, 13);
            this.lblExeLocation.TabIndex = 0;
            this.lblExeLocation.Text = "Game Program Location:";
            // 
            // dlgBrowseWorkDir
            // 
            this.dlgBrowseWorkDir.Description = "Select the game installation folder, where the save data files and the Modules fo" +
    "lder can be found.";
            this.dlgBrowseWorkDir.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // dlgBrowseExe
            // 
            this.dlgBrowseExe.Filter = "Applications (*.exe)|*.exe";
            this.dlgBrowseExe.Title = "Set Game Executable";
            // 
            // FormPreferences
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(433, 264);
            this.Controls.Add(this.grpLaunch);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPreferences";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.grpLaunch.ResumeLayout(false);
            this.grpLaunch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.GroupBox grpLaunch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdBrowseExeWorkDir;
        private System.Windows.Forms.TextBox txtExeWorkDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdBrowseExePath;
        private System.Windows.Forms.TextBox txtExePath;
        private System.Windows.Forms.Label lblExeLocation;
        private System.Windows.Forms.FolderBrowserDialog dlgBrowseWorkDir;
        private System.Windows.Forms.OpenFileDialog dlgBrowseExe;
    }
}