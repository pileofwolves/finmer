
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
            System.Windows.Forms.GroupBox grpLaunch;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.GroupBox grpSceneEditor;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdBrowseExeWorkDir = new System.Windows.Forms.Button();
            this.txtExeWorkDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdBrowseExePath = new System.Windows.Forms.Button();
            this.txtExePath = new System.Windows.Forms.TextBox();
            this.lblExeLocation = new System.Windows.Forms.Label();
            this.dlgBrowseWorkDir = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgBrowseExe = new System.Windows.Forms.OpenFileDialog();
            this.chkSceneAutoGuessKey = new System.Windows.Forms.CheckBox();
            this.chkSceneAllowCustomScript = new System.Windows.Forms.CheckBox();
            grpLaunch = new System.Windows.Forms.GroupBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            grpSceneEditor = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            grpLaunch.SuspendLayout();
            grpSceneEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Image = global::Finmer.Editor.Properties.Resources.cross;
            this.cmdCancel.Location = new System.Drawing.Point(304, 344);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(112, 32);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdOK.Location = new System.Drawing.Point(184, 344);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(112, 32);
            this.cmdOK.TabIndex = 2;
            this.cmdOK.Text = "Accept";
            this.cmdOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // grpLaunch
            // 
            grpLaunch.Controls.Add(label3);
            grpLaunch.Controls.Add(label2);
            grpLaunch.Controls.Add(this.cmdBrowseExeWorkDir);
            grpLaunch.Controls.Add(this.txtExeWorkDir);
            grpLaunch.Controls.Add(this.label1);
            grpLaunch.Controls.Add(this.cmdBrowseExePath);
            grpLaunch.Controls.Add(this.txtExePath);
            grpLaunch.Controls.Add(this.lblExeLocation);
            grpLaunch.Location = new System.Drawing.Point(16, 16);
            grpLaunch.Name = "grpLaunch";
            grpLaunch.Size = new System.Drawing.Size(400, 160);
            grpLaunch.TabIndex = 0;
            grpLaunch.TabStop = false;
            grpLaunch.Text = "Launch Settings";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Enabled = false;
            label3.Location = new System.Drawing.Point(24, 104);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(332, 13);
            label3.TabIndex = 5;
            label3.Text = "Optional. Folder where the game will look for save data and Modules.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Enabled = false;
            label2.Location = new System.Drawing.Point(24, 40);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(244, 13);
            label2.TabIndex = 1;
            label2.Text = "The executable to launch when starting the game.";
            // 
            // cmdBrowseExeWorkDir
            // 
            this.cmdBrowseExeWorkDir.Location = new System.Drawing.Point(352, 120);
            this.cmdBrowseExeWorkDir.Name = "cmdBrowseExeWorkDir";
            this.cmdBrowseExeWorkDir.Size = new System.Drawing.Size(32, 20);
            this.cmdBrowseExeWorkDir.TabIndex = 7;
            this.cmdBrowseExeWorkDir.Text = "...";
            this.cmdBrowseExeWorkDir.UseVisualStyleBackColor = true;
            this.cmdBrowseExeWorkDir.Click += new System.EventHandler(this.cmdBrowseExeWorkDir_Click);
            // 
            // txtExeWorkDir
            // 
            this.txtExeWorkDir.Location = new System.Drawing.Point(16, 120);
            this.txtExeWorkDir.Name = "txtExeWorkDir";
            this.txtExeWorkDir.Size = new System.Drawing.Size(336, 20);
            this.txtExeWorkDir.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Working Directory:";
            // 
            // cmdBrowseExePath
            // 
            this.cmdBrowseExePath.Location = new System.Drawing.Point(352, 56);
            this.cmdBrowseExePath.Name = "cmdBrowseExePath";
            this.cmdBrowseExePath.Size = new System.Drawing.Size(32, 20);
            this.cmdBrowseExePath.TabIndex = 3;
            this.cmdBrowseExePath.Text = "...";
            this.cmdBrowseExePath.UseVisualStyleBackColor = true;
            this.cmdBrowseExePath.Click += new System.EventHandler(this.cmdBrowseExePath_Click);
            // 
            // txtExePath
            // 
            this.txtExePath.Location = new System.Drawing.Point(16, 56);
            this.txtExePath.Name = "txtExePath";
            this.txtExePath.Size = new System.Drawing.Size(336, 20);
            this.txtExePath.TabIndex = 2;
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
            // grpSceneEditor
            // 
            grpSceneEditor.Controls.Add(label5);
            grpSceneEditor.Controls.Add(label4);
            grpSceneEditor.Controls.Add(this.chkSceneAllowCustomScript);
            grpSceneEditor.Controls.Add(this.chkSceneAutoGuessKey);
            grpSceneEditor.Location = new System.Drawing.Point(16, 192);
            grpSceneEditor.Name = "grpSceneEditor";
            grpSceneEditor.Size = new System.Drawing.Size(400, 128);
            grpSceneEditor.TabIndex = 1;
            grpSceneEditor.TabStop = false;
            grpSceneEditor.Text = "Scene Editor";
            // 
            // chkSceneAutoGuessKey
            // 
            this.chkSceneAutoGuessKey.AutoSize = true;
            this.chkSceneAutoGuessKey.Location = new System.Drawing.Point(16, 24);
            this.chkSceneAutoGuessKey.Name = "chkSceneAutoGuessKey";
            this.chkSceneAutoGuessKey.Size = new System.Drawing.Size(218, 17);
            this.chkSceneAutoGuessKey.TabIndex = 0;
            this.chkSceneAutoGuessKey.Text = "Auto-generate node keys where possible";
            this.chkSceneAutoGuessKey.UseVisualStyleBackColor = true;
            // 
            // chkSceneAllowCustomScript
            // 
            this.chkSceneAllowCustomScript.AutoSize = true;
            this.chkSceneAllowCustomScript.Location = new System.Drawing.Point(16, 72);
            this.chkSceneAllowCustomScript.Name = "chkSceneAllowCustomScript";
            this.chkSceneAllowCustomScript.Size = new System.Drawing.Size(215, 17);
            this.chkSceneAllowCustomScript.TabIndex = 2;
            this.chkSceneAllowCustomScript.Text = "Show custom scene script header editor";
            this.chkSceneAllowCustomScript.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Enabled = false;
            label4.Location = new System.Drawing.Point(24, 48);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(315, 13);
            label4.TabIndex = 1;
            label4.Text = "When adding child nodes to States or Choices, guess node keys.";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Enabled = false;
            label5.Location = new System.Drawing.Point(24, 96);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(349, 13);
            label5.TabIndex = 3;
            label5.Text = "For advanced users. Lets you add custom Lua code in the scene scope.";
            // 
            // FormPreferences
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(433, 392);
            this.Controls.Add(grpSceneEditor);
            this.Controls.Add(grpLaunch);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPreferences";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            grpLaunch.ResumeLayout(false);
            grpLaunch.PerformLayout();
            grpSceneEditor.ResumeLayout(false);
            grpSceneEditor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdBrowseExeWorkDir;
        private System.Windows.Forms.TextBox txtExeWorkDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdBrowseExePath;
        private System.Windows.Forms.TextBox txtExePath;
        private System.Windows.Forms.Label lblExeLocation;
        private System.Windows.Forms.FolderBrowserDialog dlgBrowseWorkDir;
        private System.Windows.Forms.OpenFileDialog dlgBrowseExe;
        private System.Windows.Forms.CheckBox chkSceneAllowCustomScript;
        private System.Windows.Forms.CheckBox chkSceneAutoGuessKey;
    }
}