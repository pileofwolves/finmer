namespace Finmer.Editor
{

    partial class FormScriptCmdShop
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.ColumnHeader columnHeader1;
            System.Windows.Forms.ColumnHeader columnHeader2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScriptCmdShop));
            this.lblRestockNote = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.nudRestockInterval = new System.Windows.Forms.NumericUpDown();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.cmdItemRemove = new System.Windows.Forms.Button();
            this.pnlMerchEdit = new System.Windows.Forms.Panel();
            this.nudMerchQty = new System.Windows.Forms.NumericUpDown();
            this.apcMerchAsset = new Finmer.Editor.AssetPickerControl();
            this.lsvMerch = new System.Windows.Forms.ListView();
            this.cmdItemAdd = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRestockInterval)).BeginInit();
            groupBox2.SuspendLayout();
            this.pnlMerchEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMerchQty)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.lblRestockNote);
            groupBox1.Controls.Add(this.txtKey);
            groupBox1.Controls.Add(this.nudRestockInterval);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(this.txtTitle);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new System.Drawing.Point(320, 16);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(288, 176);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Configuration";
            // 
            // lblRestockNote
            // 
            this.lblRestockNote.Location = new System.Drawing.Point(112, 136);
            this.lblRestockNote.Name = "lblRestockNote";
            this.lblRestockNote.Size = new System.Drawing.Size(80, 18);
            this.lblRestockNote.TabIndex = 6;
            this.lblRestockNote.Text = "in-game hours";
            this.lblRestockNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtKey
            // 
            this.txtKey.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKey.Location = new System.Drawing.Point(16, 40);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(256, 22);
            this.txtKey.TabIndex = 1;
            // 
            // nudRestockInterval
            // 
            this.nudRestockInterval.Location = new System.Drawing.Point(16, 136);
            this.nudRestockInterval.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudRestockInterval.Name = "nudRestockInterval";
            this.nudRestockInterval.Size = new System.Drawing.Size(88, 20);
            this.nudRestockInterval.TabIndex = 5;
            this.nudRestockInterval.ValueChanged += new System.EventHandler(this.nudRestockInterval_ValueChanged);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 24);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(58, 13);
            label1.TabIndex = 0;
            label1.Text = "Unique ID:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(16, 120);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(80, 13);
            label3.TabIndex = 4;
            label3.Text = "Restock Every:";
            // 
            // txtTitle
            // 
            this.txtTitle.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(16, 88);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(256, 22);
            this.txtTitle.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 72);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(99, 13);
            label2.TabIndex = 2;
            label2.Text = "Shopkeeper Name:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.cmdItemRemove);
            groupBox2.Controls.Add(this.pnlMerchEdit);
            groupBox2.Controls.Add(this.lsvMerch);
            groupBox2.Controls.Add(this.cmdItemAdd);
            groupBox2.Location = new System.Drawing.Point(16, 16);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(288, 304);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Merchandise";
            // 
            // cmdItemRemove
            // 
            this.cmdItemRemove.Enabled = false;
            this.cmdItemRemove.Image = global::Finmer.Editor.Properties.Resources.minus;
            this.cmdItemRemove.Location = new System.Drawing.Point(120, 264);
            this.cmdItemRemove.Name = "cmdItemRemove";
            this.cmdItemRemove.Size = new System.Drawing.Size(104, 32);
            this.cmdItemRemove.TabIndex = 3;
            this.cmdItemRemove.Text = "Remove Item";
            this.cmdItemRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdItemRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdItemRemove.UseVisualStyleBackColor = true;
            this.cmdItemRemove.Click += new System.EventHandler(this.cmdItemRemove_Click);
            // 
            // pnlMerchEdit
            // 
            this.pnlMerchEdit.Controls.Add(this.nudMerchQty);
            this.pnlMerchEdit.Controls.Add(label6);
            this.pnlMerchEdit.Controls.Add(this.apcMerchAsset);
            this.pnlMerchEdit.Controls.Add(label5);
            this.pnlMerchEdit.Enabled = false;
            this.pnlMerchEdit.Location = new System.Drawing.Point(8, 200);
            this.pnlMerchEdit.Name = "pnlMerchEdit";
            this.pnlMerchEdit.Size = new System.Drawing.Size(272, 56);
            this.pnlMerchEdit.TabIndex = 1;
            // 
            // nudMerchQty
            // 
            this.nudMerchQty.Location = new System.Drawing.Point(208, 24);
            this.nudMerchQty.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudMerchQty.Name = "nudMerchQty";
            this.nudMerchQty.Size = new System.Drawing.Size(56, 20);
            this.nudMerchQty.TabIndex = 3;
            this.nudMerchQty.ValueChanged += new System.EventHandler(this.nudMerchQty_ValueChanged);
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(208, 8);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(49, 13);
            label6.TabIndex = 2;
            label6.Text = "Quantity:";
            // 
            // apcMerchAsset
            // 
            this.apcMerchAsset.AssetType = Finmer.Editor.AssetPickerControl.EPickerType.Item;
            this.apcMerchAsset.Location = new System.Drawing.Point(8, 24);
            this.apcMerchAsset.Name = "apcMerchAsset";
            this.apcMerchAsset.SelectedGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.apcMerchAsset.Size = new System.Drawing.Size(200, 24);
            this.apcMerchAsset.TabIndex = 0;
            this.apcMerchAsset.SelectedAssetChanged += new System.EventHandler(this.apcMerchAsset_SelectedAssetChanged);
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(8, 8);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(30, 13);
            label5.TabIndex = 1;
            label5.Text = "Item:";
            // 
            // lsvMerch
            // 
            this.lsvMerch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2});
            this.lsvMerch.FullRowSelect = true;
            this.lsvMerch.GridLines = true;
            this.lsvMerch.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvMerch.HideSelection = false;
            this.lsvMerch.Location = new System.Drawing.Point(8, 16);
            this.lsvMerch.MultiSelect = false;
            this.lsvMerch.Name = "lsvMerch";
            this.lsvMerch.Size = new System.Drawing.Size(272, 184);
            this.lsvMerch.TabIndex = 0;
            this.lsvMerch.UseCompatibleStateImageBehavior = false;
            this.lsvMerch.View = System.Windows.Forms.View.Details;
            this.lsvMerch.SelectedIndexChanged += new System.EventHandler(this.lsvMerch_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Item";
            columnHeader1.Width = 160;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Qty";
            columnHeader2.Width = 80;
            // 
            // cmdItemAdd
            // 
            this.cmdItemAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmdItemAdd.Image")));
            this.cmdItemAdd.Location = new System.Drawing.Point(8, 264);
            this.cmdItemAdd.Name = "cmdItemAdd";
            this.cmdItemAdd.Size = new System.Drawing.Size(104, 32);
            this.cmdItemAdd.TabIndex = 2;
            this.cmdItemAdd.Text = "Add Item";
            this.cmdItemAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdItemAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdItemAdd.UseVisualStyleBackColor = true;
            this.cmdItemAdd.Click += new System.EventHandler(this.cmdItemAdd_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdAccept.Location = new System.Drawing.Point(360, 288);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(119, 32);
            this.cmdAccept.TabIndex = 2;
            this.cmdAccept.Text = "Accept";
            this.cmdAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Image = global::Finmer.Editor.Properties.Resources.cross;
            this.cmdCancel.Location = new System.Drawing.Point(488, 287);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(119, 32);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // FormScriptCmdShop
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(624, 336);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormScriptCmdShop";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Shopping!";
            this.Load += new System.EventHandler(this.FormScriptCmdPlayerSetItem_Load);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRestockInterval)).EndInit();
            groupBox2.ResumeLayout(false);
            this.pnlMerchEdit.ResumeLayout(false);
            this.pnlMerchEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMerchQty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.NumericUpDown nudRestockInterval;
        private System.Windows.Forms.Panel pnlMerchEdit;
        private System.Windows.Forms.NumericUpDown nudMerchQty;
        private AssetPickerControl apcMerchAsset;
        private System.Windows.Forms.ListView lsvMerch;
        private System.Windows.Forms.Button cmdItemRemove;
        private System.Windows.Forms.Button cmdItemAdd;
        private System.Windows.Forms.Label lblRestockNote;
    }

}
