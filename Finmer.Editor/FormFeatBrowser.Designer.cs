namespace Finmer.Editor
{
	partial class FormFeatBrowser
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
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.lsvFeats = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// cmdOK
			// 
			this.cmdOK.Image = global::Finmer.Editor.Properties.Resources.tick;
			this.cmdOK.Location = new System.Drawing.Point(216, 288);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(112, 32);
			this.cmdOK.TabIndex = 0;
			this.cmdOK.Text = "Accept";
			this.cmdOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Image = global::Finmer.Editor.Properties.Resources.cross;
			this.cmdCancel.Location = new System.Drawing.Point(336, 288);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(112, 32);
			this.cmdCancel.TabIndex = 1;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// lsvFeats
			// 
			this.lsvFeats.CheckBoxes = true;
			this.lsvFeats.Location = new System.Drawing.Point(16, 16);
			this.lsvFeats.MultiSelect = false;
			this.lsvFeats.Name = "lsvFeats";
			this.lsvFeats.Size = new System.Drawing.Size(432, 256);
			this.lsvFeats.TabIndex = 2;
			this.lsvFeats.UseCompatibleStateImageBehavior = false;
			this.lsvFeats.View = System.Windows.Forms.View.List;
			// 
			// frmPickFeats
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(465, 337);
			this.Controls.Add(this.lsvFeats);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmPickFeats";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Feats";
			this.Load += new System.EventHandler(this.frmPickFeats_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.ListView lsvFeats;
	}
}