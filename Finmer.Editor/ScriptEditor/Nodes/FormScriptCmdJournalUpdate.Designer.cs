namespace Finmer.Editor
{

    partial class FormScriptCmdJournalUpdate
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            this.cmdAccept = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.apcJournal = new Finmer.Editor.AssetPickerControl();
            this.nudStage = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudStage)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 16);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(76, 13);
            label2.TabIndex = 10;
            label2.Text = "Set this Quest:";
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdAccept.Location = new System.Drawing.Point(56, 152);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(119, 32);
            this.cmdAccept.TabIndex = 3;
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
            this.cmdCancel.Location = new System.Drawing.Point(184, 151);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(119, 32);
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // apcJournal
            // 
            this.apcJournal.AssetType = Finmer.Editor.AssetPickerControl.EPickerType.Journal;
            this.apcJournal.Location = new System.Drawing.Point(16, 32);
            this.apcJournal.Name = "apcJournal";
            this.apcJournal.SelectedGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.apcJournal.Size = new System.Drawing.Size(288, 24);
            this.apcJournal.TabIndex = 11;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 72);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(67, 13);
            label1.TabIndex = 12;
            label1.Text = "to this stage:";
            // 
            // nudStage
            // 
            this.nudStage.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudStage.Location = new System.Drawing.Point(16, 88);
            this.nudStage.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudStage.Name = "nudStage";
            this.nudStage.Size = new System.Drawing.Size(152, 23);
            this.nudStage.TabIndex = 13;
            // 
            // FormScriptCmdJournalUpdate
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(320, 200);
            this.Controls.Add(this.nudStage);
            this.Controls.Add(label1);
            this.Controls.Add(this.apcJournal);
            this.Controls.Add(label2);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormScriptCmdJournalUpdate";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update Quest";
            this.Load += new System.EventHandler(this.FormScriptCmdPlayerSetItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudStage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Button cmdCancel;
        private AssetPickerControl apcJournal;
        private System.Windows.Forms.NumericUpDown nudStage;
    }

}
