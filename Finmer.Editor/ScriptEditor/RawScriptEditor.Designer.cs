
namespace Finmer.Editor
{
    partial class RawScriptEditor
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.scintilla = new ScintillaNET.Scintilla();
            this.tsbUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbRedo = new System.Windows.Forms.ToolStripButton();
            this.tsbConvertVisual = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbUndo,
            this.tsbRedo,
            this.tsbConvertVisual});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(475, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // scintilla
            // 
            this.scintilla.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.scintilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintilla.Location = new System.Drawing.Point(0, 25);
            this.scintilla.Name = "scintilla";
            this.scintilla.Size = new System.Drawing.Size(475, 286);
            this.scintilla.TabIndex = 1;
            // 
            // tsbUndo
            // 
            this.tsbUndo.Enabled = false;
            this.tsbUndo.Image = global::Finmer.Editor.Properties.Resources.arrow_circle_225_left;
            this.tsbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Size = new System.Drawing.Size(56, 22);
            this.tsbUndo.Text = "Undo";
            this.tsbUndo.Click += new System.EventHandler(this.tsbUndo_Click);
            // 
            // tsbRedo
            // 
            this.tsbRedo.Enabled = false;
            this.tsbRedo.Image = global::Finmer.Editor.Properties.Resources.arrow_circle_225;
            this.tsbRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRedo.Name = "tsbRedo";
            this.tsbRedo.Size = new System.Drawing.Size(54, 22);
            this.tsbRedo.Text = "Redo";
            this.tsbRedo.Click += new System.EventHandler(this.tsbRedo_Click);
            // 
            // tsbConvertVisual
            // 
            this.tsbConvertVisual.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbConvertVisual.Image = global::Finmer.Editor.Properties.Resources.script_code;
            this.tsbConvertVisual.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConvertVisual.Name = "tsbConvertVisual";
            this.tsbConvertVisual.Size = new System.Drawing.Size(150, 22);
            this.tsbConvertVisual.Text = "Convert to Visual Script";
            this.tsbConvertVisual.Visible = false;
            this.tsbConvertVisual.Click += new System.EventHandler(this.tsbConvertVisual_Click);
            // 
            // RawScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scintilla);
            this.Controls.Add(this.toolStrip);
            this.Name = "RawScriptEditor";
            this.Size = new System.Drawing.Size(475, 311);
            this.Load += new System.EventHandler(this.RawScriptEditor_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private ScintillaNET.Scintilla scintilla;
        private System.Windows.Forms.ToolStripButton tsbUndo;
        private System.Windows.Forms.ToolStripButton tsbRedo;
        private System.Windows.Forms.ToolStripButton tsbConvertVisual;
    }
}
