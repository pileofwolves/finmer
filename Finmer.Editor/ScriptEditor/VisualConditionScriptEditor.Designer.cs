
namespace Finmer.Editor
{
    partial class VisualConditionScriptEditor
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
            System.Windows.Forms.ToolStrip toolStrip;
            this.tsbConvertExternal = new System.Windows.Forms.ToolStripButton();
            this.tsbConvertInline = new System.Windows.Forms.ToolStripButton();
            this.cgeBranch = new Finmer.Editor.ConditionGroupEditor();
            toolStrip = new System.Windows.Forms.ToolStrip();
            toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbConvertExternal,
            this.tsbConvertInline});
            toolStrip.Location = new System.Drawing.Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new System.Drawing.Size(475, 25);
            toolStrip.TabIndex = 1;
            toolStrip.Text = "toolStrip1";
            // 
            // tsbConvertExternal
            // 
            this.tsbConvertExternal.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbConvertExternal.Image = global::Finmer.Editor.Properties.Resources.script_code;
            this.tsbConvertExternal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConvertExternal.Name = "tsbConvertExternal";
            this.tsbConvertExternal.Size = new System.Drawing.Size(105, 22);
            this.tsbConvertExternal.Text = "Convert to Lua";
            this.tsbConvertExternal.ToolTipText = "Convert to External Lua File";
            this.tsbConvertExternal.Visible = false;
            this.tsbConvertExternal.Click += new System.EventHandler(this.tsbConvertExternal_Click);
            // 
            // tsbConvertInline
            // 
            this.tsbConvertInline.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbConvertInline.Image = global::Finmer.Editor.Properties.Resources.script_code;
            this.tsbConvertInline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConvertInline.Name = "tsbConvertInline";
            this.tsbConvertInline.Size = new System.Drawing.Size(105, 22);
            this.tsbConvertInline.Text = "Convert to Lua";
            this.tsbConvertInline.ToolTipText = "Convert to Inline Lua";
            this.tsbConvertInline.Visible = false;
            this.tsbConvertInline.Click += new System.EventHandler(this.tsbConvertInline_Click);
            // 
            // cgeBranch
            // 
            this.cgeBranch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cgeBranch.Location = new System.Drawing.Point(0, 25);
            this.cgeBranch.Name = "cgeBranch";
            this.cgeBranch.Padding = new System.Windows.Forms.Padding(8);
            this.cgeBranch.Size = new System.Drawing.Size(475, 286);
            this.cgeBranch.TabIndex = 0;
            // 
            // VisualConditionScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cgeBranch);
            this.Controls.Add(toolStrip);
            this.Name = "VisualConditionScriptEditor";
            this.Size = new System.Drawing.Size(475, 311);
            this.Load += new System.EventHandler(this.VisualScriptEditor_Load);
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripButton tsbConvertExternal;
        private System.Windows.Forms.ToolStripButton tsbConvertInline;
        private ConditionGroupEditor cgeBranch;
    }
}
