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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScriptCmdCombatBegin));
            System.Windows.Forms.Label label1;
            System.Windows.Forms.GroupBox grpCallbacks;
            System.Windows.Forms.ColumnHeader columnHeader3;
            this.cmdNpcEdit = new System.Windows.Forms.Button();
            this.cmdNpcRemove = new System.Windows.Forms.Button();
            this.cmdNpcAdd = new System.Windows.Forms.Button();
            this.lsvNpcs = new System.Windows.Forms.ListView();
            this.chkIncludePlayer = new System.Windows.Forms.CheckBox();
            this.chkCallbackCS = new System.Windows.Forms.CheckBox();
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
            label1 = new System.Windows.Forms.Label();
            grpCallbacks = new System.Windows.Forms.GroupBox();
            columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            grpParticipants.SuspendLayout();
            grpCallbacks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Name";
            columnHeader1.Width = 125;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Creature";
            columnHeader2.Width = 125;
            // 
            // grpParticipants
            // 
            grpParticipants.Controls.Add(this.cmdNpcEdit);
            grpParticipants.Controls.Add(this.cmdNpcRemove);
            grpParticipants.Controls.Add(this.cmdNpcAdd);
            grpParticipants.Controls.Add(this.lsvNpcs);
            grpParticipants.Controls.Add(label1);
            grpParticipants.Controls.Add(this.chkIncludePlayer);
            grpParticipants.Location = new System.Drawing.Point(16, 16);
            grpParticipants.Name = "grpParticipants";
            grpParticipants.Size = new System.Drawing.Size(360, 224);
            grpParticipants.TabIndex = 0;
            grpParticipants.TabStop = false;
            grpParticipants.Text = "Participants";
            // 
            // cmdNpcEdit
            // 
            this.cmdNpcEdit.Enabled = false;
            this.cmdNpcEdit.Image = global::Finmer.Editor.Properties.Resources.pencil;
            this.cmdNpcEdit.Location = new System.Drawing.Point(272, 32);
            this.cmdNpcEdit.Name = "cmdNpcEdit";
            this.cmdNpcEdit.Size = new System.Drawing.Size(32, 32);
            this.cmdNpcEdit.TabIndex = 2;
            this.cmdNpcEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdNpcEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdNpcEdit.UseVisualStyleBackColor = true;
            this.cmdNpcEdit.Click += new System.EventHandler(this.cmdNpcEdit_Click);
            // 
            // cmdNpcRemove
            // 
            this.cmdNpcRemove.Enabled = false;
            this.cmdNpcRemove.Image = global::Finmer.Editor.Properties.Resources.minus;
            this.cmdNpcRemove.Location = new System.Drawing.Point(312, 32);
            this.cmdNpcRemove.Name = "cmdNpcRemove";
            this.cmdNpcRemove.Size = new System.Drawing.Size(32, 32);
            this.cmdNpcRemove.TabIndex = 3;
            this.cmdNpcRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdNpcRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdNpcRemove.UseVisualStyleBackColor = true;
            this.cmdNpcRemove.Click += new System.EventHandler(this.cmdNpcRemove_Click);
            // 
            // cmdNpcAdd
            // 
            this.cmdNpcAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmdNpcAdd.Image")));
            this.cmdNpcAdd.Location = new System.Drawing.Point(232, 32);
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
            columnHeader2,
            columnHeader3});
            this.lsvNpcs.FullRowSelect = true;
            this.lsvNpcs.GridLines = true;
            this.lsvNpcs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvNpcs.HideSelection = false;
            this.lsvNpcs.Location = new System.Drawing.Point(16, 72);
            this.lsvNpcs.MultiSelect = false;
            this.lsvNpcs.Name = "lsvNpcs";
            this.lsvNpcs.Size = new System.Drawing.Size(328, 136);
            this.lsvNpcs.TabIndex = 5;
            this.lsvNpcs.UseCompatibleStateImageBehavior = false;
            this.lsvNpcs.View = System.Windows.Forms.View.Details;
            this.lsvNpcs.SelectedIndexChanged += new System.EventHandler(this.lsvNpcs_SelectedIndexChanged);
            this.lsvNpcs.DoubleClick += new System.EventHandler(this.lsvNpcs_DoubleClick);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 56);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(90, 13);
            label1.TabIndex = 4;
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
            grpCallbacks.Controls.Add(this.chkCallbackCS);
            grpCallbacks.Controls.Add(this.chkCallbackCR);
            grpCallbacks.Controls.Add(this.chkCallbackCV);
            grpCallbacks.Controls.Add(this.chkCallbackCK);
            grpCallbacks.Controls.Add(this.chkCallbackPK);
            grpCallbacks.Controls.Add(this.chkCallbackRE);
            grpCallbacks.Location = new System.Drawing.Point(16, 256);
            grpCallbacks.Name = "grpCallbacks";
            grpCallbacks.Size = new System.Drawing.Size(360, 104);
            grpCallbacks.TabIndex = 1;
            grpCallbacks.TabStop = false;
            grpCallbacks.Text = "Run Custom Scripts When";
            // 
            // chkCallbackCS
            // 
            this.chkCallbackCS.AutoSize = true;
            this.chkCallbackCS.Location = new System.Drawing.Point(16, 24);
            this.chkCallbackCS.Name = "chkCallbackCS";
            this.chkCallbackCS.Size = new System.Drawing.Size(87, 17);
            this.chkCallbackCS.TabIndex = 0;
            this.chkCallbackCS.Text = "Combat Start";
            this.chkCallbackCS.UseVisualStyleBackColor = true;
            // 
            // chkCallbackCR
            // 
            this.chkCallbackCR.AutoSize = true;
            this.chkCallbackCR.Location = new System.Drawing.Point(176, 72);
            this.chkCallbackCR.Name = "chkCallbackCR";
            this.chkCallbackCR.Size = new System.Drawing.Size(145, 17);
            this.chkCallbackCR.TabIndex = 5;
            this.chkCallbackCR.Text = "Any Participant Released";
            this.chkCallbackCR.UseVisualStyleBackColor = true;
            // 
            // chkCallbackCV
            // 
            this.chkCallbackCV.AutoSize = true;
            this.chkCallbackCV.Location = new System.Drawing.Point(176, 48);
            this.chkCallbackCV.Name = "chkCallbackCV";
            this.chkCallbackCV.Size = new System.Drawing.Size(128, 17);
            this.chkCallbackCV.TabIndex = 4;
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
            this.chkCallbackRE.Location = new System.Drawing.Point(176, 24);
            this.chkCallbackRE.Name = "chkCallbackRE";
            this.chkCallbackRE.Size = new System.Drawing.Size(80, 17);
            this.chkCallbackRE.TabIndex = 3;
            this.chkCallbackRE.Text = "Round End";
            this.chkCallbackRE.UseVisualStyleBackColor = true;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdAccept.Location = new System.Drawing.Point(128, 393);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(119, 32);
            this.cmdAccept.TabIndex = 2;
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
            this.cmdCancel.Location = new System.Drawing.Point(256, 392);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(119, 32);
            this.cmdCancel.TabIndex = 3;
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
            // columnHeader3
            // 
            columnHeader3.Text = "Ally?";
            columnHeader3.Width = 45;
            // 
            // FormScriptCmdCombatBegin
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(392, 441);
            this.Controls.Add(grpCallbacks);
            this.Controls.Add(grpParticipants);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
        private System.Windows.Forms.CheckBox chkCallbackCK;
        private System.Windows.Forms.CheckBox chkCallbackPK;
        private System.Windows.Forms.CheckBox chkCallbackRE;
        private System.Windows.Forms.CheckBox chkCallbackCR;
        private System.Windows.Forms.CheckBox chkCallbackCV;
        private System.Windows.Forms.CheckBox chkCallbackCS;
        private System.Windows.Forms.Button cmdNpcEdit;
    }

}
