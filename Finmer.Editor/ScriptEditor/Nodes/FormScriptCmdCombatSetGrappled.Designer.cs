
namespace Finmer.Editor
{
    partial class FormScriptCmdCombatSetGrappled
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
            System.Windows.Forms.Label label3;
            this.cmdAccept = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.optModeSet = new System.Windows.Forms.RadioButton();
            this.optModeUnset = new System.Windows.Forms.RadioButton();
            this.instigator = new Finmer.Editor.CombatParticipantIDEditor();
            this.target = new Finmer.Editor.CombatParticipantIDEditor();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 16);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(75, 13);
            label2.TabIndex = 0;
            label2.Text = "This instigator:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 168);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(78, 13);
            label1.TabIndex = 5;
            label1.Text = "with this target:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(16, 112);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(49, 13);
            label3.TabIndex = 2;
            label3.Text = "Should...";
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdAccept.Location = new System.Drawing.Point(41, 281);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(119, 32);
            this.cmdAccept.TabIndex = 7;
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
            this.cmdCancel.Location = new System.Drawing.Point(169, 280);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(119, 32);
            this.cmdCancel.TabIndex = 8;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // optModeSet
            // 
            this.optModeSet.AutoSize = true;
            this.optModeSet.Location = new System.Drawing.Point(24, 128);
            this.optModeSet.Name = "optModeSet";
            this.optModeSet.Size = new System.Drawing.Size(62, 17);
            this.optModeSet.TabIndex = 3;
            this.optModeSet.TabStop = true;
            this.optModeSet.Text = "Grapple";
            this.optModeSet.UseVisualStyleBackColor = true;
            // 
            // optModeUnset
            // 
            this.optModeUnset.AutoSize = true;
            this.optModeUnset.Location = new System.Drawing.Point(104, 128);
            this.optModeUnset.Name = "optModeUnset";
            this.optModeUnset.Size = new System.Drawing.Size(93, 17);
            this.optModeUnset.TabIndex = 4;
            this.optModeUnset.TabStop = true;
            this.optModeUnset.Text = "Stop grappling";
            this.optModeUnset.UseVisualStyleBackColor = true;
            // 
            // instigator
            // 
            this.instigator.Location = new System.Drawing.Point(16, 40);
            this.instigator.Name = "instigator";
            this.instigator.Size = new System.Drawing.Size(272, 56);
            this.instigator.TabIndex = 1;
            // 
            // target
            // 
            this.target.Location = new System.Drawing.Point(16, 192);
            this.target.Name = "target";
            this.target.Size = new System.Drawing.Size(272, 56);
            this.target.TabIndex = 6;
            // 
            // FormScriptCmdCombatSetGrappled
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(305, 329);
            this.Controls.Add(this.target);
            this.Controls.Add(this.instigator);
            this.Controls.Add(this.optModeUnset);
            this.Controls.Add(this.optModeSet);
            this.Controls.Add(label3);
            this.Controls.Add(label1);
            this.Controls.Add(label2);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormScriptCmdCombatSetGrappled";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set / Unset Participants as Grappling";
            this.Load += new System.EventHandler(this.FormScriptCmdCombatSetGrappled_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.RadioButton optModeSet;
        private System.Windows.Forms.RadioButton optModeUnset;
        private CombatParticipantIDEditor instigator;
        private CombatParticipantIDEditor target;
    }
}