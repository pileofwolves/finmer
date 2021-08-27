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
			this.scintilla = new ScintillaNET.Scintilla();
			this.SuspendLayout();
			// 
			// scintilla
			// 
			this.scintilla.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.scintilla.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scintilla.Location = new System.Drawing.Point(0, 0);
			this.scintilla.Name = "scintilla";
			this.scintilla.Size = new System.Drawing.Size(284, 261);
			this.scintilla.TabIndex = 0;
			this.scintilla.TextChanged += new System.EventHandler(this.scintilla_TextChanged);
			// 
			// frmDocumentScript
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.scintilla);
			this.Name = "frmDocumentScript";
			this.Text = "frmDocumentScript";
			this.Load += new System.EventHandler(this.FormDocumentScript_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private ScintillaNET.Scintilla scintilla;
	}
}