
namespace Finmer.Editor
{
    partial class AssetPickerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAssetName = new System.Windows.Forms.LinkLabel();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAssetName
            // 
            this.lblAssetName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAssetName.AutoEllipsis = true;
            this.lblAssetName.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lblAssetName.Location = new System.Drawing.Point(24, 0);
            this.lblAssetName.Name = "lblAssetName";
            this.lblAssetName.Size = new System.Drawing.Size(168, 24);
            this.lblAssetName.TabIndex = 1;
            this.lblAssetName.TabStop = true;
            this.lblAssetName.Text = "Asset";
            this.lblAssetName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAssetName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblAssetName_LinkClicked);
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdBrowse.Image = global::Finmer.Editor.Properties.Resources.chain;
            this.cmdBrowse.Location = new System.Drawing.Point(0, 0);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(24, 24);
            this.cmdBrowse.TabIndex = 0;
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // AssetPickerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblAssetName);
            this.Controls.Add(this.cmdBrowse);
            this.Name = "AssetPickerControl";
            this.Size = new System.Drawing.Size(193, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.LinkLabel lblAssetName;
    }
}
