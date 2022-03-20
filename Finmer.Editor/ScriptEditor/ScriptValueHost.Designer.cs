
namespace Finmer.Editor
{
    partial class ScriptValueHost
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
            this.lklText = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lklText
            // 
            this.lklText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lklText.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lklText.Location = new System.Drawing.Point(0, 0);
            this.lklText.Name = "lklText";
            this.lklText.Size = new System.Drawing.Size(150, 150);
            this.lklText.TabIndex = 0;
            this.lklText.TabStop = true;
            this.lklText.Text = "Text";
            this.lklText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lklText.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklText_LinkClicked);
            // 
            // ScriptValueHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lklText);
            this.Name = "ScriptValueHost";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel lklText;
    }
}
