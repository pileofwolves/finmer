
namespace Finmer.Editor
{
    partial class VisualScriptEditor
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
            System.Windows.Forms.ColumnHeader clhImplicitHeader;
            this.lsvNodes = new System.Windows.Forms.ListView();
            this.tsbConvertExternal = new System.Windows.Forms.ToolStripButton();
            this.tsbConvertInline = new System.Windows.Forms.ToolStripButton();
            toolStrip = new System.Windows.Forms.ToolStrip();
            clhImplicitHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            toolStrip.TabIndex = 0;
            toolStrip.Text = "toolStrip1";
            // 
            // lsvNodes
            // 
            this.lsvNodes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lsvNodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clhImplicitHeader});
            this.lsvNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvNodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvNodes.FullRowSelect = true;
            this.lsvNodes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvNodes.HideSelection = false;
            this.lsvNodes.Location = new System.Drawing.Point(0, 25);
            this.lsvNodes.Name = "lsvNodes";
            this.lsvNodes.Size = new System.Drawing.Size(475, 286);
            this.lsvNodes.TabIndex = 1;
            this.lsvNodes.UseCompatibleStateImageBehavior = false;
            this.lsvNodes.View = System.Windows.Forms.View.Details;
            this.lsvNodes.DoubleClick += new System.EventHandler(this.lsvNodes_DoubleClick);
            // 
            // clhImplicitHeader
            // 
            clhImplicitHeader.Width = 400;
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
            // VisualScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lsvNodes);
            this.Controls.Add(toolStrip);
            this.Name = "VisualScriptEditor";
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
        private System.Windows.Forms.ListView lsvNodes;
    }
}
