
namespace Finmer.Editor
{
    partial class AssetPickerBrowser
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Current Project", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Dependencies", System.Windows.Forms.HorizontalAlignment.Left);
            this.lsvAssets = new System.Windows.Forms.ListView();
            this.clhAsset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.clhName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lsvAssets
            // 
            this.lsvAssets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhAsset,
            this.clhName});
            this.lsvAssets.FullRowSelect = true;
            listViewGroup1.Header = "Current Project";
            listViewGroup1.Name = "grpMain";
            listViewGroup2.Header = "Dependencies";
            listViewGroup2.Name = "grpDeps";
            this.lsvAssets.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.lsvAssets.HideSelection = false;
            this.lsvAssets.Location = new System.Drawing.Point(8, 8);
            this.lsvAssets.Name = "lsvAssets";
            this.lsvAssets.Size = new System.Drawing.Size(352, 368);
            this.lsvAssets.TabIndex = 0;
            this.lsvAssets.UseCompatibleStateImageBehavior = false;
            this.lsvAssets.View = System.Windows.Forms.View.Details;
            this.lsvAssets.SelectedIndexChanged += new System.EventHandler(this.lsvAssets_SelectedIndexChanged);
            this.lsvAssets.DoubleClick += new System.EventHandler(this.lsvAssets_DoubleClick);
            // 
            // clhAsset
            // 
            this.clhAsset.Text = "Asset";
            this.clhAsset.Width = 170;
            // 
            // cmdOK
            // 
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdOK.Location = new System.Drawing.Point(112, 384);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(120, 32);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "Accept";
            this.cmdOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdOK.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Image = global::Finmer.Editor.Properties.Resources.cross;
            this.cmdCancel.Location = new System.Drawing.Point(240, 384);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(120, 32);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // clhName
            // 
            this.clhName.Text = "Name";
            this.clhName.Width = 160;
            // 
            // AssetPickerBrowser
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(369, 425);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.lsvAssets);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssetPickerBrowser";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Asset";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvAssets;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ColumnHeader clhAsset;
        private System.Windows.Forms.ColumnHeader clhName;
    }
}