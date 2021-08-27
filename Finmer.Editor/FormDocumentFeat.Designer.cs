namespace Finmer.Editor
{
	partial class FormDocumentFeat
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
			this.fraGeneral = new System.Windows.Forms.GroupBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.txtDesc = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtFlavor = new System.Windows.Forms.TextBox();
			this.txtGuid = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.fraReqs = new System.Windows.Forms.GroupBox();
			this.lstPrereqSummary = new System.Windows.Forms.ListBox();
			this.cmdReqRemove = new System.Windows.Forms.Button();
			this.cmdReqEdit = new System.Windows.Forms.Button();
			this.cmdReqAdd = new System.Windows.Forms.Button();
			this.fraGeneral.SuspendLayout();
			this.fraReqs.SuspendLayout();
			this.SuspendLayout();
			// 
			// fraGeneral
			// 
			this.fraGeneral.Controls.Add(this.txtGuid);
			this.fraGeneral.Controls.Add(this.label10);
			this.fraGeneral.Controls.Add(this.label2);
			this.fraGeneral.Controls.Add(this.txtFlavor);
			this.fraGeneral.Controls.Add(this.label11);
			this.fraGeneral.Controls.Add(this.txtDesc);
			this.fraGeneral.Controls.Add(this.txtName);
			this.fraGeneral.Controls.Add(this.label1);
			this.fraGeneral.Location = new System.Drawing.Point(16, 16);
			this.fraGeneral.Name = "fraGeneral";
			this.fraGeneral.Size = new System.Drawing.Size(296, 360);
			this.fraGeneral.TabIndex = 0;
			this.fraGeneral.TabStop = false;
			this.fraGeneral.Text = "General";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(16, 40);
			this.txtName.MaxLength = 100;
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(264, 20);
			this.txtName.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Public Name:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(16, 136);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(63, 13);
			this.label11.TabIndex = 11;
			this.label11.Text = "Description:";
			// 
			// txtDesc
			// 
			this.txtDesc.Location = new System.Drawing.Point(16, 152);
			this.txtDesc.MaxLength = 1000;
			this.txtDesc.Multiline = true;
			this.txtDesc.Name = "txtDesc";
			this.txtDesc.Size = new System.Drawing.Size(264, 80);
			this.txtDesc.TabIndex = 10;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 248);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 13;
			this.label2.Text = "Flavor Text:";
			// 
			// txtFlavor
			// 
			this.txtFlavor.Location = new System.Drawing.Point(16, 264);
			this.txtFlavor.MaxLength = 1000;
			this.txtFlavor.Multiline = true;
			this.txtFlavor.Name = "txtFlavor";
			this.txtFlavor.Size = new System.Drawing.Size(264, 80);
			this.txtFlavor.TabIndex = 12;
			// 
			// txtGuid
			// 
			this.txtGuid.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtGuid.Location = new System.Drawing.Point(16, 96);
			this.txtGuid.MaxLength = 100;
			this.txtGuid.Name = "txtGuid";
			this.txtGuid.ReadOnly = true;
			this.txtGuid.Size = new System.Drawing.Size(264, 21);
			this.txtGuid.TabIndex = 14;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(16, 80);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(66, 13);
			this.label10.TabIndex = 15;
			this.label10.Text = "Asset GUID:";
			// 
			// fraReqs
			// 
			this.fraReqs.Controls.Add(this.cmdReqRemove);
			this.fraReqs.Controls.Add(this.cmdReqEdit);
			this.fraReqs.Controls.Add(this.cmdReqAdd);
			this.fraReqs.Controls.Add(this.lstPrereqSummary);
			this.fraReqs.Enabled = false;
			this.fraReqs.Location = new System.Drawing.Point(328, 16);
			this.fraReqs.Name = "fraReqs";
			this.fraReqs.Size = new System.Drawing.Size(312, 248);
			this.fraReqs.TabIndex = 1;
			this.fraReqs.TabStop = false;
			this.fraReqs.Text = "Prerequisites";
			// 
			// lstPrereqSummary
			// 
			this.lstPrereqSummary.FormattingEnabled = true;
			this.lstPrereqSummary.Location = new System.Drawing.Point(16, 64);
			this.lstPrereqSummary.Name = "lstPrereqSummary";
			this.lstPrereqSummary.Size = new System.Drawing.Size(280, 160);
			this.lstPrereqSummary.TabIndex = 0;
			// 
			// cmdReqRemove
			// 
			this.cmdReqRemove.Image = global::Finmer.Editor.Properties.Resources.cross_script;
			this.cmdReqRemove.Location = new System.Drawing.Point(208, 24);
			this.cmdReqRemove.Name = "cmdReqRemove";
			this.cmdReqRemove.Size = new System.Drawing.Size(88, 32);
			this.cmdReqRemove.TabIndex = 3;
			this.cmdReqRemove.Text = "Remove";
			this.cmdReqRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdReqRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.cmdReqRemove.UseVisualStyleBackColor = true;
			// 
			// cmdReqEdit
			// 
			this.cmdReqEdit.Image = global::Finmer.Editor.Properties.Resources.pencil;
			this.cmdReqEdit.Location = new System.Drawing.Point(112, 24);
			this.cmdReqEdit.Name = "cmdReqEdit";
			this.cmdReqEdit.Size = new System.Drawing.Size(88, 32);
			this.cmdReqEdit.TabIndex = 2;
			this.cmdReqEdit.Text = "Edit...";
			this.cmdReqEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdReqEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.cmdReqEdit.UseVisualStyleBackColor = true;
			// 
			// cmdReqAdd
			// 
			this.cmdReqAdd.Image = global::Finmer.Editor.Properties.Resources.plus;
			this.cmdReqAdd.Location = new System.Drawing.Point(16, 24);
			this.cmdReqAdd.Name = "cmdReqAdd";
			this.cmdReqAdd.Size = new System.Drawing.Size(88, 32);
			this.cmdReqAdd.TabIndex = 1;
			this.cmdReqAdd.Text = "Add";
			this.cmdReqAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdReqAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.cmdReqAdd.UseVisualStyleBackColor = true;
			// 
			// frmDocumentFeat
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(697, 601);
			this.Controls.Add(this.fraReqs);
			this.Controls.Add(this.fraGeneral);
			this.Name = "frmDocumentFeat";
			this.Text = "frmDocumentFeat";
			this.Load += new System.EventHandler(this.FormDocumentFeat_Load);
			this.fraGeneral.ResumeLayout(false);
			this.fraGeneral.PerformLayout();
			this.fraReqs.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox fraGeneral;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtFlavor;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox txtDesc;
		private System.Windows.Forms.TextBox txtGuid;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.GroupBox fraReqs;
		private System.Windows.Forms.Button cmdReqRemove;
		private System.Windows.Forms.Button cmdReqEdit;
		private System.Windows.Forms.Button cmdReqAdd;
		private System.Windows.Forms.ListBox lstPrereqSummary;
	}
}