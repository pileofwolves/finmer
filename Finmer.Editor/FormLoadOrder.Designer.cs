
namespace Finmer.Editor
{
    partial class FormLoadOrder
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.Label lblOther;
            System.Windows.Forms.ColumnHeader columnHeader1;
            this.grpElement = new System.Windows.Forms.GroupBox();
            this.cmbRelation = new System.Windows.Forms.ComboBox();
            this.lblRelation = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.cmdDepRemove = new System.Windows.Forms.Button();
            this.cmdDepAdd = new System.Windows.Forms.Button();
            this.lsvDependencies = new System.Windows.Forms.ListView();
            this.apcOther = new Finmer.Editor.AssetPickerControl();
            groupBox1 = new System.Windows.Forms.GroupBox();
            lblOther = new System.Windows.Forms.Label();
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            groupBox1.SuspendLayout();
            this.grpElement.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.lsvDependencies);
            groupBox1.Controls.Add(this.cmdDepRemove);
            groupBox1.Controls.Add(this.cmdDepAdd);
            groupBox1.Location = new System.Drawing.Point(16, 16);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(288, 216);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Load Order Dependencies";
            // 
            // grpElement
            // 
            this.grpElement.Controls.Add(this.apcOther);
            this.grpElement.Controls.Add(this.cmbRelation);
            this.grpElement.Controls.Add(this.lblRelation);
            this.grpElement.Controls.Add(lblOther);
            this.grpElement.Enabled = false;
            this.grpElement.Location = new System.Drawing.Point(16, 248);
            this.grpElement.Name = "grpElement";
            this.grpElement.Size = new System.Drawing.Size(288, 136);
            this.grpElement.TabIndex = 1;
            this.grpElement.TabStop = false;
            this.grpElement.Text = "Configuration";
            // 
            // cmbRelation
            // 
            this.cmbRelation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRelation.FormattingEnabled = true;
            this.cmbRelation.Items.AddRange(new object[] {
            "Before",
            "After"});
            this.cmbRelation.Location = new System.Drawing.Point(16, 40);
            this.cmbRelation.Name = "cmbRelation";
            this.cmbRelation.Size = new System.Drawing.Size(256, 21);
            this.cmbRelation.TabIndex = 1;
            // 
            // lblRelation
            // 
            this.lblRelation.AutoSize = true;
            this.lblRelation.Location = new System.Drawing.Point(16, 24);
            this.lblRelation.Name = "lblRelation";
            this.lblRelation.Size = new System.Drawing.Size(49, 13);
            this.lblRelation.TabIndex = 0;
            this.lblRelation.Text = "Relation:";
            // 
            // lblOther
            // 
            lblOther.AutoSize = true;
            lblOther.Location = new System.Drawing.Point(16, 80);
            lblOther.Name = "lblOther";
            lblOther.Size = new System.Drawing.Size(96, 13);
            lblOther.TabIndex = 2;
            lblOther.Text = "... this target asset:";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Image = global::Finmer.Editor.Properties.Resources.cross;
            this.cmdCancel.Location = new System.Drawing.Point(184, 416);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(120, 32);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdAccept.Location = new System.Drawing.Point(56, 416);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(120, 32);
            this.cmdAccept.TabIndex = 2;
            this.cmdAccept.Text = "Accept";
            this.cmdAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdDepRemove
            // 
            this.cmdDepRemove.Enabled = false;
            this.cmdDepRemove.Image = global::Finmer.Editor.Properties.Resources.cross;
            this.cmdDepRemove.Location = new System.Drawing.Point(112, 24);
            this.cmdDepRemove.Name = "cmdDepRemove";
            this.cmdDepRemove.Size = new System.Drawing.Size(88, 32);
            this.cmdDepRemove.TabIndex = 1;
            this.cmdDepRemove.Text = "Remove";
            this.cmdDepRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdDepRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdDepRemove.UseVisualStyleBackColor = true;
            this.cmdDepRemove.Click += new System.EventHandler(this.cmdDepRemove_Click);
            // 
            // cmdDepAdd
            // 
            this.cmdDepAdd.Image = global::Finmer.Editor.Properties.Resources.plus;
            this.cmdDepAdd.Location = new System.Drawing.Point(16, 24);
            this.cmdDepAdd.Name = "cmdDepAdd";
            this.cmdDepAdd.Size = new System.Drawing.Size(88, 32);
            this.cmdDepAdd.TabIndex = 0;
            this.cmdDepAdd.Text = "Add";
            this.cmdDepAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdDepAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdDepAdd.UseVisualStyleBackColor = true;
            this.cmdDepAdd.Click += new System.EventHandler(this.cmdDepAdd_Click);
            // 
            // lsvDependencies
            // 
            this.lsvDependencies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1});
            this.lsvDependencies.FullRowSelect = true;
            this.lsvDependencies.GridLines = true;
            this.lsvDependencies.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvDependencies.HideSelection = false;
            this.lsvDependencies.Location = new System.Drawing.Point(16, 64);
            this.lsvDependencies.Name = "lsvDependencies";
            this.lsvDependencies.Size = new System.Drawing.Size(256, 136);
            this.lsvDependencies.TabIndex = 2;
            this.lsvDependencies.UseCompatibleStateImageBehavior = false;
            this.lsvDependencies.View = System.Windows.Forms.View.Details;
            this.lsvDependencies.SelectedIndexChanged += new System.EventHandler(this.lsvDependencies_SelectedIndexChanged);
            // 
            // apcOther
            // 
            this.apcOther.AssetType = Finmer.Editor.AssetPickerControl.EPickerType.Script;
            this.apcOther.Location = new System.Drawing.Point(16, 96);
            this.apcOther.Name = "apcOther";
            this.apcOther.SelectedGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.apcOther.Size = new System.Drawing.Size(256, 24);
            this.apcOther.TabIndex = 3;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Description";
            columnHeader1.Width = 230;
            // 
            // FormLoadOrder
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(321, 465);
            this.Controls.Add(this.grpElement);
            this.Controls.Add(groupBox1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLoadOrder";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Loading Order";
            this.Load += new System.EventHandler(this.FormLoadOrder_Load);
            groupBox1.ResumeLayout(false);
            this.grpElement.ResumeLayout(false);
            this.grpElement.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.ComboBox cmbRelation;
        private AssetPickerControl apcOther;
        private System.Windows.Forms.Label lblRelation;
        private System.Windows.Forms.Button cmdDepRemove;
        private System.Windows.Forms.Button cmdDepAdd;
        private System.Windows.Forms.ListView lsvDependencies;
        private System.Windows.Forms.GroupBox grpElement;
    }
}