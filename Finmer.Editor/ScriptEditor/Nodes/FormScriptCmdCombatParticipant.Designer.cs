
namespace Finmer.Editor
{
    partial class FormScriptCmdCombatParticipant
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
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label1;
            this.txtNpcName = new System.Windows.Forms.TextBox();
            this.chkIsAlly = new System.Windows.Forms.CheckBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.apcNpcAsset = new Finmer.Editor.AssetPickerControl();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 16);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(74, 13);
            label2.TabIndex = 0;
            label2.Text = "Participant ID:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(16, 72);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(79, 13);
            label3.TabIndex = 2;
            label3.Text = "Creature Asset:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 128);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(87, 13);
            label1.TabIndex = 4;
            label1.Text = "Combat Settings:";
            // 
            // txtNpcName
            // 
            this.txtNpcName.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNpcName.Location = new System.Drawing.Point(16, 32);
            this.txtNpcName.MaxLength = 100;
            this.txtNpcName.Name = "txtNpcName";
            this.txtNpcName.Size = new System.Drawing.Size(264, 22);
            this.txtNpcName.TabIndex = 1;
            // 
            // chkIsAlly
            // 
            this.chkIsAlly.AutoSize = true;
            this.chkIsAlly.Location = new System.Drawing.Point(24, 152);
            this.chkIsAlly.Name = "chkIsAlly";
            this.chkIsAlly.Size = new System.Drawing.Size(116, 17);
            this.chkIsAlly.TabIndex = 5;
            this.chkIsAlly.Text = "Is Allied with Player";
            this.chkIsAlly.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Image = global::Finmer.Editor.Properties.Resources.cross;
            this.cmdCancel.Location = new System.Drawing.Point(160, 224);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(119, 32);
            this.cmdCancel.TabIndex = 7;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdAccept.Location = new System.Drawing.Point(32, 224);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(119, 32);
            this.cmdAccept.TabIndex = 6;
            this.cmdAccept.Text = "Accept";
            this.cmdAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // apcNpcAsset
            // 
            this.apcNpcAsset.AssetType = Finmer.Editor.AssetPickerControl.EPickerType.Creature;
            this.apcNpcAsset.Location = new System.Drawing.Point(16, 88);
            this.apcNpcAsset.Name = "apcNpcAsset";
            this.apcNpcAsset.SelectedGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.apcNpcAsset.Size = new System.Drawing.Size(264, 24);
            this.apcNpcAsset.TabIndex = 3;
            // 
            // FormScriptCmdCombatParticipant
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(297, 273);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(label1);
            this.Controls.Add(this.txtNpcName);
            this.Controls.Add(this.chkIsAlly);
            this.Controls.Add(this.apcNpcAsset);
            this.Controls.Add(label2);
            this.Controls.Add(label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormScriptCmdCombatParticipant";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure NPC Participant";
            this.Load += new System.EventHandler(this.FormScriptCmdCombatParticipant_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNpcName;
        private System.Windows.Forms.CheckBox chkIsAlly;
        private AssetPickerControl apcNpcAsset;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdAccept;
    }
}