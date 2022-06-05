namespace Finmer.Editor
{

    partial class FormScriptCmdCombatBegin
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
            System.Windows.Forms.ColumnHeader columnHeader1;
            System.Windows.Forms.ColumnHeader columnHeader2;
            System.Windows.Forms.GroupBox grpParticipants;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScriptCmdCombatBegin));
            System.Windows.Forms.Label label1;
            System.Windows.Forms.GroupBox grpCallbacks;
            this.grpParticipantSettings = new System.Windows.Forms.GroupBox();
            this.txtNpcName = new System.Windows.Forms.TextBox();
            this.apcNpcAsset = new Finmer.Editor.AssetPickerControl();
            this.cmdNpcRemove = new System.Windows.Forms.Button();
            this.cmdNpcAdd = new System.Windows.Forms.Button();
            this.lsvNpcs = new System.Windows.Forms.ListView();
            this.chkIncludePlayer = new System.Windows.Forms.CheckBox();
            this.chkCallbackCR = new System.Windows.Forms.CheckBox();
            this.chkCallbackCV = new System.Windows.Forms.CheckBox();
            this.chkCallbackCK = new System.Windows.Forms.CheckBox();
            this.chkCallbackPK = new System.Windows.Forms.CheckBox();
            this.chkCallbackRE = new System.Windows.Forms.CheckBox();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.eventLog1 = new System.Diagnostics.EventLog();
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            grpParticipants = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            grpCallbacks = new System.Windows.Forms.GroupBox();
            grpParticipants.SuspendLayout();
            this.grpParticipantSettings.SuspendLayout();
            grpCallbacks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Name";
            columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Creature";
            columnHeader2.Width = 150;
            // 
            // grpParticipants
            // 
            grpParticipants.Controls.Add(this.grpParticipantSettings);
            grpParticipants.Controls.Add(this.cmdNpcRemove);
            grpParticipants.Controls.Add(this.cmdNpcAdd);
            grpParticipants.Controls.Add(this.lsvNpcs);
            grpParticipants.Controls.Add(label1);
            grpParticipants.Controls.Add(this.chkIncludePlayer);
            grpParticipants.Location = new System.Drawing.Point(16, 16);
            grpParticipants.Name = "grpParticipants";
            grpParticipants.Size = new System.Drawing.Size(360, 352);
            grpParticipants.TabIndex = 5;
            grpParticipants.TabStop = false;
            grpParticipants.Text = "Participants";
            // 
            // grpParticipantSettings
            // 
            this.grpParticipantSettings.Controls.Add(this.txtNpcName);
            this.grpParticipantSettings.Controls.Add(this.apcNpcAsset);
            this.grpParticipantSettings.Controls.Add(label2);
            this.grpParticipantSettings.Controls.Add(label3);
            this.grpParticipantSettings.Enabled = false;
            this.grpParticipantSettings.Location = new System.Drawing.Point(16, 216);
            this.grpParticipantSettings.Name = "grpParticipantSettings";
            this.grpParticipantSettings.Size = new System.Drawing.Size(280, 128);
            this.grpParticipantSettings.TabIndex = 9;
            this.grpParticipantSettings.TabStop = false;
            this.grpParticipantSettings.Text = "Participant Settings";
            // 
            // txtNpcName
            // 
            this.txtNpcName.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNpcName.Location = new System.Drawing.Point(16, 40);
            this.txtNpcName.Name = "txtNpcName";
            this.txtNpcName.Size = new System.Drawing.Size(248, 22);
            this.txtNpcName.TabIndex = 0;
            this.txtNpcName.TextChanged += new System.EventHandler(this.txtNpcName_TextChanged);
            // 
            // apcNpcAsset
            // 
            this.apcNpcAsset.AssetType = Finmer.Editor.AssetPickerControl.EPickerType.Creature;
            this.apcNpcAsset.Location = new System.Drawing.Point(16, 88);
            this.apcNpcAsset.Name = "apcNpcAsset";
            this.apcNpcAsset.SelectedGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.apcNpcAsset.Size = new System.Drawing.Size(248, 24);
            this.apcNpcAsset.TabIndex = 1;
            this.apcNpcAsset.SelectedAssetChanged += new System.EventHandler(this.apcNpcAsset_SelectedAssetChanged);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 24);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(38, 13);
            label2.TabIndex = 5;
            label2.Text = "Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(16, 72);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(79, 13);
            label3.TabIndex = 7;
            label3.Text = "Creature Asset:";
            // 
            // cmdNpcRemove
            // 
            this.cmdNpcRemove.Enabled = false;
            this.cmdNpcRemove.Image = global::Finmer.Editor.Properties.Resources.minus;
            this.cmdNpcRemove.Location = new System.Drawing.Point(312, 32);
            this.cmdNpcRemove.Name = "cmdNpcRemove";
            this.cmdNpcRemove.Size = new System.Drawing.Size(32, 32);
            this.cmdNpcRemove.TabIndex = 2;
            this.cmdNpcRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdNpcRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdNpcRemove.UseVisualStyleBackColor = true;
            this.cmdNpcRemove.Click += new System.EventHandler(this.cmdNpcRemove_Click);
            // 
            // cmdNpcAdd
            // 
            this.cmdNpcAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmdNpcAdd.Image")));
            this.cmdNpcAdd.Location = new System.Drawing.Point(272, 32);
            this.cmdNpcAdd.Name = "cmdNpcAdd";
            this.cmdNpcAdd.Size = new System.Drawing.Size(32, 32);
            this.cmdNpcAdd.TabIndex = 1;
            this.cmdNpcAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdNpcAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdNpcAdd.UseVisualStyleBackColor = true;
            this.cmdNpcAdd.Click += new System.EventHandler(this.cmdNpcAdd_Click);
            // 
            // lsvNpcs
            // 
            this.lsvNpcs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2});
            this.lsvNpcs.FullRowSelect = true;
            this.lsvNpcs.GridLines = true;
            this.lsvNpcs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvNpcs.HideSelection = false;
            this.lsvNpcs.Location = new System.Drawing.Point(16, 72);
            this.lsvNpcs.MultiSelect = false;
            this.lsvNpcs.Name = "lsvNpcs";
            this.lsvNpcs.Size = new System.Drawing.Size(328, 136);
            this.lsvNpcs.TabIndex = 0;
            this.lsvNpcs.UseCompatibleStateImageBehavior = false;
            this.lsvNpcs.View = System.Windows.Forms.View.Details;
            this.lsvNpcs.SelectedIndexChanged += new System.EventHandler(this.lsvNpcs_SelectedIndexChanged);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 56);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(90, 13);
            label1.TabIndex = 1;
            label1.Text = "NPC Participants:";
            // 
            // chkIncludePlayer
            // 
            this.chkIncludePlayer.AutoSize = true;
            this.chkIncludePlayer.Location = new System.Drawing.Point(16, 24);
            this.chkIncludePlayer.Name = "chkIncludePlayer";
            this.chkIncludePlayer.Size = new System.Drawing.Size(93, 17);
            this.chkIncludePlayer.TabIndex = 0;
            this.chkIncludePlayer.Text = "Include Player";
            this.chkIncludePlayer.UseVisualStyleBackColor = true;
            // 
            // grpCallbacks
            // 
            grpCallbacks.Controls.Add(this.chkCallbackCR);
            grpCallbacks.Controls.Add(this.chkCallbackCV);
            grpCallbacks.Controls.Add(this.chkCallbackCK);
            grpCallbacks.Controls.Add(this.chkCallbackPK);
            grpCallbacks.Controls.Add(this.chkCallbackRE);
            grpCallbacks.Location = new System.Drawing.Point(16, 384);
            grpCallbacks.Name = "grpCallbacks";
            grpCallbacks.Size = new System.Drawing.Size(360, 104);
            grpCallbacks.TabIndex = 6;
            grpCallbacks.TabStop = false;
            grpCallbacks.Text = "Run Custom Scripts When";
            // 
            // chkCallbackCR
            // 
            this.chkCallbackCR.AutoSize = true;
            this.chkCallbackCR.Location = new System.Drawing.Point(176, 48);
            this.chkCallbackCR.Name = "chkCallbackCR";
            this.chkCallbackCR.Size = new System.Drawing.Size(145, 17);
            this.chkCallbackCR.TabIndex = 4;
            this.chkCallbackCR.Text = "Any Participant Released";
            this.chkCallbackCR.UseVisualStyleBackColor = true;
            // 
            // chkCallbackCV
            // 
            this.chkCallbackCV.AutoSize = true;
            this.chkCallbackCV.Location = new System.Drawing.Point(176, 24);
            this.chkCallbackCV.Name = "chkCallbackCV";
            this.chkCallbackCV.Size = new System.Drawing.Size(128, 17);
            this.chkCallbackCV.TabIndex = 3;
            this.chkCallbackCV.Text = "Any Participant Vored";
            this.chkCallbackCV.UseVisualStyleBackColor = true;
            // 
            // chkCallbackCK
            // 
            this.chkCallbackCK.AutoSize = true;
            this.chkCallbackCK.Location = new System.Drawing.Point(16, 72);
            this.chkCallbackCK.Name = "chkCallbackCK";
            this.chkCallbackCK.Size = new System.Drawing.Size(76, 17);
            this.chkCallbackCK.TabIndex = 2;
            this.chkCallbackCK.Text = "NPC Killed";
            this.chkCallbackCK.UseVisualStyleBackColor = true;
            // 
            // chkCallbackPK
            // 
            this.chkCallbackPK.AutoSize = true;
            this.chkCallbackPK.Location = new System.Drawing.Point(16, 48);
            this.chkCallbackPK.Name = "chkCallbackPK";
            this.chkCallbackPK.Size = new System.Drawing.Size(83, 17);
            this.chkCallbackPK.TabIndex = 1;
            this.chkCallbackPK.Text = "Player Killed";
            this.chkCallbackPK.UseVisualStyleBackColor = true;
            // 
            // chkCallbackRE
            // 
            this.chkCallbackRE.AutoSize = true;
            this.chkCallbackRE.Location = new System.Drawing.Point(16, 24);
            this.chkCallbackRE.Name = "chkCallbackRE";
            this.chkCallbackRE.Size = new System.Drawing.Size(80, 17);
            this.chkCallbackRE.TabIndex = 0;
            this.chkCallbackRE.Text = "Round End";
            this.chkCallbackRE.UseVisualStyleBackColor = true;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdAccept.Location = new System.Drawing.Point(128, 504);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(119, 32);
            this.cmdAccept.TabIndex = 0;
            this.cmdAccept.Text = "Accept";
            this.cmdAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Image = global::Finmer.Editor.Properties.Resources.cross;
            this.cmdCancel.Location = new System.Drawing.Point(256, 503);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(119, 32);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // eventLog1
            // 
            this.eventLog1.Log = "gbjlkjk";
            this.eventLog1.Source = "gjhgjhm";
            this.eventLog1.SynchronizingObject = this;
            // 
            // FormScriptCmdCombatBegin
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(392, 552);
            this.Controls.Add(grpCallbacks);
            this.Controls.Add(grpParticipants);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormScriptCmdCombatBegin";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Begin Combat";
            this.Load += new System.EventHandler(this.FormScriptCmdCombatBegin_Load);
            grpParticipants.ResumeLayout(false);
            grpParticipants.PerformLayout();
            this.grpParticipantSettings.ResumeLayout(false);
            this.grpParticipantSettings.PerformLayout();
            grpCallbacks.ResumeLayout(false);
            grpCallbacks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ListView lsvNpcs;
        private System.Windows.Forms.CheckBox chkIncludePlayer;
        private System.Windows.Forms.Button cmdNpcRemove;
        private System.Windows.Forms.Button cmdNpcAdd;
        private System.Diagnostics.EventLog eventLog1;
        private AssetPickerControl apcNpcAsset;
        private System.Windows.Forms.TextBox txtNpcName;
        private System.Windows.Forms.CheckBox chkCallbackCK;
        private System.Windows.Forms.CheckBox chkCallbackPK;
        private System.Windows.Forms.CheckBox chkCallbackRE;
        private System.Windows.Forms.CheckBox chkCallbackCR;
        private System.Windows.Forms.CheckBox chkCallbackCV;
        private System.Windows.Forms.GroupBox grpParticipantSettings;
    }

}
