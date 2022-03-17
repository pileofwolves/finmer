namespace Finmer.Editor
{
	partial class FormDocumentScript
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
			if (disposing && (components != null)) {
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
            this.scriptEditorHost = new Finmer.Editor.ScriptEditorHost();
            this.SuspendLayout();
            // 
            // scriptEditorHost
            // 
            this.scriptEditorHost.AllowExternalScript = true;
            this.scriptEditorHost.AllowInlineScript = false;
            this.scriptEditorHost.AllowVisualScript = false;
            this.scriptEditorHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptEditorHost.Location = new System.Drawing.Point(0, 0);
            this.scriptEditorHost.Name = "scriptEditorHost";
            this.scriptEditorHost.Size = new System.Drawing.Size(284, 261);
            this.scriptEditorHost.TabIndex = 0;
            // 
            // FormDocumentScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.scriptEditorHost);
            this.Name = "FormDocumentScript";
            this.Text = "frmDocumentScript";
            this.Load += new System.EventHandler(this.FormDocumentScript_Load);
            this.ResumeLayout(false);

		}

        #endregion

        private ScriptEditorHost scriptEditorHost;
    }
}