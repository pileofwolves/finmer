namespace Finmer.Editor
{

    partial class FormScriptCondCombatParGrp
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
            System.Windows.Forms.Label label1;
            this.cmdAccept = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.participant = new Finmer.Editor.CombatParticipantIDEditor();
            this.partner = new Finmer.Editor.CombatParticipantIDEditor();
            this.chkWithPartner = new System.Windows.Forms.CheckBox();
            label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 16);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(176, 13);
            label1.TabIndex = 0;
            label1.Text = "Check if this participant is grappling:";
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdAccept.Location = new System.Drawing.Point(57, 233);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(119, 32);
            this.cmdAccept.TabIndex = 4;
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
            this.cmdCancel.Location = new System.Drawing.Point(185, 232);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(119, 32);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // participant
            // 
            this.participant.Location = new System.Drawing.Point(16, 40);
            this.participant.Name = "participant";
            this.participant.Size = new System.Drawing.Size(291, 56);
            this.participant.TabIndex = 1;
            // 
            // partner
            // 
            this.partner.Location = new System.Drawing.Point(16, 136);
            this.partner.Name = "partner";
            this.partner.Size = new System.Drawing.Size(291, 73);
            this.partner.TabIndex = 3;
            // 
            // chkWithPartner
            // 
            this.chkWithPartner.AutoSize = true;
            this.chkWithPartner.Location = new System.Drawing.Point(16, 112);
            this.chkWithPartner.Name = "chkWithPartner";
            this.chkWithPartner.Size = new System.Drawing.Size(126, 17);
            this.chkWithPartner.TabIndex = 2;
            this.chkWithPartner.Text = "With specific partner:";
            this.chkWithPartner.UseVisualStyleBackColor = true;
            this.chkWithPartner.CheckedChanged += new System.EventHandler(this.chkWithPartner_CheckedChanged);
            // 
            // FormScriptCondCombatParGrp
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(321, 281);
            this.Controls.Add(this.chkWithPartner);
            this.Controls.Add(this.partner);
            this.Controls.Add(this.participant);
            this.Controls.Add(label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormScriptCondCombatParGrp";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Is Participant Grappling?";
            this.Load += new System.EventHandler(this.FormScriptCondCombatParGrp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Button cmdCancel;
        private CombatParticipantIDEditor participant;
        private CombatParticipantIDEditor partner;
        private System.Windows.Forms.CheckBox chkWithPartner;
    }

}
