using System.ComponentModel;
using System.Windows.Forms;

namespace Finmer.Editor
{
    partial class CombatParticipantIDEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.optModePlayer = new System.Windows.Forms.RadioButton();
            this.optModeNPC = new System.Windows.Forms.RadioButton();
            this.txtNPC = new System.Windows.Forms.TextBox();
            this.lblNPC = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // optModePlayer
            // 
            this.optModePlayer.AutoSize = true;
            this.optModePlayer.Location = new System.Drawing.Point(0, 0);
            this.optModePlayer.Name = "optModePlayer";
            this.optModePlayer.Size = new System.Drawing.Size(54, 17);
            this.optModePlayer.TabIndex = 0;
            this.optModePlayer.TabStop = true;
            this.optModePlayer.Text = "Player";
            this.optModePlayer.UseVisualStyleBackColor = true;
            // 
            // optModeNPC
            // 
            this.optModeNPC.AutoSize = true;
            this.optModeNPC.Location = new System.Drawing.Point(72, 0);
            this.optModeNPC.Name = "optModeNPC";
            this.optModeNPC.Size = new System.Drawing.Size(47, 17);
            this.optModeNPC.TabIndex = 1;
            this.optModeNPC.TabStop = true;
            this.optModeNPC.Text = "NPC";
            this.optModeNPC.UseVisualStyleBackColor = true;
            this.optModeNPC.CheckedChanged += new System.EventHandler(this.optModeNPC_CheckedChanged);
            // 
            // txtNPC
            // 
            this.txtNPC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNPC.Enabled = false;
            this.txtNPC.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNPC.Location = new System.Drawing.Point(32, 24);
            this.txtNPC.Name = "txtNPC";
            this.txtNPC.Size = new System.Drawing.Size(256, 22);
            this.txtNPC.TabIndex = 3;
            // 
            // lblNPC
            // 
            this.lblNPC.Location = new System.Drawing.Point(8, 24);
            this.lblNPC.Name = "lblNPC";
            this.lblNPC.Size = new System.Drawing.Size(24, 21);
            this.lblNPC.TabIndex = 2;
            this.lblNPC.Text = "ID:";
            this.lblNPC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CombatParticipantIDEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtNPC);
            this.Controls.Add(this.lblNPC);
            this.Controls.Add(this.optModeNPC);
            this.Controls.Add(this.optModePlayer);
            this.Name = "CombatParticipantIDEditor";
            this.Size = new System.Drawing.Size(291, 49);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RadioButton optModePlayer;
        private RadioButton optModeNPC;
        private TextBox txtNPC;
        private Label lblNPC;
    }
}
