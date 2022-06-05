namespace Finmer.Editor
{

    partial class FormScriptCmdPlayerSetStat
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
            System.Windows.Forms.Label label2;
            this.cmdAccept = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.optModeAdd = new System.Windows.Forms.RadioButton();
            this.optModeSet = new System.Windows.Forms.RadioButton();
            this.cmbStat = new System.Windows.Forms.ComboBox();
            this.sveValue = new Finmer.Editor.ScriptValueIntEditor();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 48);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(66, 13);
            label1.TabIndex = 3;
            label1.Text = "Primary Stat:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 104);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(37, 13);
            label2.TabIndex = 4;
            label2.Text = "Value:";
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdAccept.Location = new System.Drawing.Point(56, 336);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(119, 32);
            this.cmdAccept.TabIndex = 6;
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
            this.cmdCancel.Location = new System.Drawing.Point(184, 335);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(119, 32);
            this.cmdCancel.TabIndex = 7;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // optModeAdd
            // 
            this.optModeAdd.AutoSize = true;
            this.optModeAdd.Location = new System.Drawing.Point(16, 16);
            this.optModeAdd.Name = "optModeAdd";
            this.optModeAdd.Size = new System.Drawing.Size(44, 17);
            this.optModeAdd.TabIndex = 1;
            this.optModeAdd.TabStop = true;
            this.optModeAdd.Text = "Add";
            this.optModeAdd.UseVisualStyleBackColor = true;
            // 
            // optModeSet
            // 
            this.optModeSet.AutoSize = true;
            this.optModeSet.Location = new System.Drawing.Point(72, 16);
            this.optModeSet.Name = "optModeSet";
            this.optModeSet.Size = new System.Drawing.Size(41, 17);
            this.optModeSet.TabIndex = 2;
            this.optModeSet.TabStop = true;
            this.optModeSet.Text = "Set";
            this.optModeSet.UseVisualStyleBackColor = true;
            // 
            // cmbStat
            // 
            this.cmbStat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStat.FormattingEnabled = true;
            this.cmbStat.Items.AddRange(new object[] {
            "Strength",
            "Agility",
            "Body",
            "Wits"});
            this.cmbStat.Location = new System.Drawing.Point(16, 64);
            this.cmbStat.Name = "cmbStat";
            this.cmbStat.Size = new System.Drawing.Size(288, 21);
            this.cmbStat.TabIndex = 0;
            // 
            // sveValue
            // 
            this.sveValue.Location = new System.Drawing.Point(16, 128);
            this.sveValue.Name = "sveValue";
            this.sveValue.Size = new System.Drawing.Size(295, 198);
            this.sveValue.TabIndex = 5;
            // 
            // FormScriptCmdPlayerSetStat
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(320, 384);
            this.Controls.Add(this.sveValue);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.cmbStat);
            this.Controls.Add(this.optModeSet);
            this.Controls.Add(this.optModeAdd);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormScriptCmdPlayerSetStat";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set Compass Location";
            this.Load += new System.EventHandler(this.FormScriptCmdSetLocation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.RadioButton optModeAdd;
        private System.Windows.Forms.RadioButton optModeSet;
        private System.Windows.Forms.ComboBox cmbStat;
        private ScriptValueIntEditor sveValue;
    }

}
