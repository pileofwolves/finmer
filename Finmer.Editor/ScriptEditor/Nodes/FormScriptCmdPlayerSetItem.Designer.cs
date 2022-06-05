namespace Finmer.Editor
{

    partial class FormScriptCmdPlayerSetItem
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
            this.cmdAccept = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.optModeAdd = new System.Windows.Forms.RadioButton();
            this.optModeRemove = new System.Windows.Forms.RadioButton();
            this.apcItem = new Finmer.Editor.AssetPickerControl();
            label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 48);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(91, 13);
            label2.TabIndex = 5;
            label2.Text = "the following item:";
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdAccept.Location = new System.Drawing.Point(56, 128);
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
            this.cmdCancel.Location = new System.Drawing.Point(184, 127);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(119, 32);
            this.cmdCancel.TabIndex = 4;
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
            // optModeRemove
            // 
            this.optModeRemove.AutoSize = true;
            this.optModeRemove.Location = new System.Drawing.Point(72, 16);
            this.optModeRemove.Name = "optModeRemove";
            this.optModeRemove.Size = new System.Drawing.Size(65, 17);
            this.optModeRemove.TabIndex = 2;
            this.optModeRemove.TabStop = true;
            this.optModeRemove.Text = "Remove";
            this.optModeRemove.UseVisualStyleBackColor = true;
            // 
            // apcItem
            // 
            this.apcItem.AssetType = Finmer.Editor.AssetPickerControl.EPickerType.Item;
            this.apcItem.Location = new System.Drawing.Point(16, 64);
            this.apcItem.Name = "apcItem";
            this.apcItem.SelectedGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.apcItem.Size = new System.Drawing.Size(288, 24);
            this.apcItem.TabIndex = 0;
            // 
            // FormScriptCmdPlayerSetItem
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(320, 176);
            this.Controls.Add(this.apcItem);
            this.Controls.Add(label2);
            this.Controls.Add(this.optModeRemove);
            this.Controls.Add(this.optModeAdd);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormScriptCmdPlayerSetItem";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add/Remove Item";
            this.Load += new System.EventHandler(this.FormScriptCmdPlayerSetItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.RadioButton optModeAdd;
        private System.Windows.Forms.RadioButton optModeRemove;
        private AssetPickerControl apcItem;
    }

}
