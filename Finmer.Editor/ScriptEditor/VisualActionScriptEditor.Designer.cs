
namespace Finmer.Editor
{
    partial class VisualActionScriptEditor
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
            this.tsbDeleteNode = new System.Windows.Forms.ToolStripButton();
            this.tsbMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tsbMoveDown = new System.Windows.Forms.ToolStripButton();
            this.lsvNodes = new Finmer.Editor.BandedListView();
            toolStrip = new System.Windows.Forms.ToolStrip();
            toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbConvertExternal,
            this.tsbConvertInline,
            this.tsbDeleteNode,
            this.tsbMoveUp,
            this.tsbMoveDown});
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
            // tsbDeleteNode
            // 
            this.tsbDeleteNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDeleteNode.Enabled = false;
            this.tsbDeleteNode.Image = global::Finmer.Editor.Properties.Resources.cross_script;
            this.tsbDeleteNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteNode.Name = "tsbDeleteNode";
            this.tsbDeleteNode.Size = new System.Drawing.Size(23, 22);
            this.tsbDeleteNode.Text = "Delete Command";
            this.tsbDeleteNode.Click += new System.EventHandler(this.tsbDeleteNode_Click);
            // 
            // tsbMoveUp
            // 
            this.tsbMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMoveUp.Enabled = false;
            this.tsbMoveUp.Image = global::Finmer.Editor.Properties.Resources.arrow_090;
            this.tsbMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveUp.Name = "tsbMoveUp";
            this.tsbMoveUp.Size = new System.Drawing.Size(23, 22);
            this.tsbMoveUp.Text = "Move Up";
            this.tsbMoveUp.Click += new System.EventHandler(this.tsbMoveUp_Click);
            // 
            // tsbMoveDown
            // 
            this.tsbMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMoveDown.Enabled = false;
            this.tsbMoveDown.Image = global::Finmer.Editor.Properties.Resources.arrow_270;
            this.tsbMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveDown.Name = "tsbMoveDown";
            this.tsbMoveDown.Size = new System.Drawing.Size(23, 22);
            this.tsbMoveDown.Text = "Move Down";
            this.tsbMoveDown.Click += new System.EventHandler(this.tsbMoveDown_Click);
            // 
            // lsvNodes
            // 
            this.lsvNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvNodes.Location = new System.Drawing.Point(0, 25);
            this.lsvNodes.Name = "lsvNodes";
            this.lsvNodes.SelectedIndex = -1;
            this.lsvNodes.Size = new System.Drawing.Size(475, 286);
            this.lsvNodes.TabIndex = 0;
            this.lsvNodes.SelectedIndexChanged += new System.EventHandler(this.lsvNodes_SelectedIndexChanged);
            this.lsvNodes.ItemDoubleClick += new System.EventHandler(this.lsvNodes_DoubleClick);
            this.lsvNodes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsvNodes_KeyDown);
            // 
            // VisualActionScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lsvNodes);
            this.Controls.Add(toolStrip);
            this.Name = "VisualActionScriptEditor";
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
        private BandedListView lsvNodes;
        private System.Windows.Forms.ToolStripButton tsbDeleteNode;
        private System.Windows.Forms.ToolStripButton tsbMoveUp;
        private System.Windows.Forms.ToolStripButton tsbMoveDown;
    }
}
