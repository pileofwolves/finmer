namespace Finmer.Editor
{
	partial class FormDocumentStringTable
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbRemove = new System.Windows.Forms.ToolStripButton();
            this.tslStats = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAddIncrement = new System.Windows.Forms.ToolStripButton();
            this.tsbAddTopic = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstKeys = new System.Windows.Forms.ListView();
            this.clhDummy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.scintilla = new ScintillaNET.Scintilla();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbRemove,
            this.tslStats,
            this.toolStripSeparator1,
            this.tsbAddIncrement,
            this.tsbAddTopic});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(784, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.Image = global::Finmer.Editor.Properties.Resources.plus;
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(68, 22);
            this.tsbAdd.Text = "Add Set";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbRemove
            // 
            this.tsbRemove.Enabled = false;
            this.tsbRemove.Image = global::Finmer.Editor.Properties.Resources.minus;
            this.tsbRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemove.Name = "tsbRemove";
            this.tsbRemove.Size = new System.Drawing.Size(89, 22);
            this.tsbRemove.Text = "Remove Set";
            this.tsbRemove.Click += new System.EventHandler(this.tsbRemove_Click);
            // 
            // tslStats
            // 
            this.tslStats.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslStats.Name = "tslStats";
            this.tslStats.Size = new System.Drawing.Size(32, 22);
            this.tslStats.Text = "Stats";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbAddIncrement
            // 
            this.tsbAddIncrement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddIncrement.Enabled = false;
            this.tsbAddIncrement.Image = global::Finmer.Editor.Properties.Resources.document_number;
            this.tsbAddIncrement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddIncrement.Name = "tsbAddIncrement";
            this.tsbAddIncrement.Size = new System.Drawing.Size(23, 22);
            this.tsbAddIncrement.Text = "Add Next Number in Set (Ctrl+D)";
            this.tsbAddIncrement.Click += new System.EventHandler(this.tsbAddIncrement_Click);
            // 
            // tsbAddTopic
            // 
            this.tsbAddTopic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddTopic.Enabled = false;
            this.tsbAddTopic.Image = global::Finmer.Editor.Properties.Resources.document_copy;
            this.tsbAddTopic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddTopic.Name = "tsbAddTopic";
            this.tsbAddTopic.Size = new System.Drawing.Size(23, 22);
            this.tsbAddTopic.Text = "Add Set with Same Topic (Ctrl+T)";
            this.tsbAddTopic.Click += new System.EventHandler(this.tsbAddTopic_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstKeys);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.scintilla);
            this.splitContainer1.Size = new System.Drawing.Size(784, 468);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 1;
            // 
            // lstKeys
            // 
            this.lstKeys.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhDummy});
            this.lstKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstKeys.FullRowSelect = true;
            this.lstKeys.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstKeys.HideSelection = false;
            this.lstKeys.LabelEdit = true;
            this.lstKeys.Location = new System.Drawing.Point(0, 0);
            this.lstKeys.MultiSelect = false;
            this.lstKeys.Name = "lstKeys";
            this.lstKeys.Size = new System.Drawing.Size(250, 468);
            this.lstKeys.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstKeys.TabIndex = 0;
            this.lstKeys.UseCompatibleStateImageBehavior = false;
            this.lstKeys.View = System.Windows.Forms.View.Details;
            this.lstKeys.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lstKeys_AfterLabelEdit);
            this.lstKeys.SelectedIndexChanged += new System.EventHandler(this.lstKeys_SelectedIndexChanged);
            this.lstKeys.Resize += new System.EventHandler(this.lstKeys_Resize);
            // 
            // clhDummy
            // 
            this.clhDummy.Width = 246;
            // 
            // scintilla
            // 
            this.scintilla.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scintilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintilla.Location = new System.Drawing.Point(0, 0);
            this.scintilla.Name = "scintilla";
            this.scintilla.Size = new System.Drawing.Size(530, 468);
            this.scintilla.TabIndex = 0;
            this.scintilla.Visible = false;
            this.scintilla.TextChanged += new System.EventHandler(this.scintilla_TextChanged);
            // 
            // FormDocumentStringTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 493);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip);
            this.Name = "FormDocumentStringTable";
            this.Text = "FormDocumentStringTable";
            this.Load += new System.EventHandler(this.FormDocumentStringTable_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private ScintillaNET.Scintilla scintilla;
		private System.Windows.Forms.ToolStripButton tsbAdd;
		private System.Windows.Forms.ToolStripButton tsbRemove;
		private System.Windows.Forms.ListView lstKeys;
		private System.Windows.Forms.ColumnHeader clhDummy;
        private System.Windows.Forms.ToolStripLabel tslStats;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbAddIncrement;
        private System.Windows.Forms.ToolStripButton tsbAddTopic;
    }
}