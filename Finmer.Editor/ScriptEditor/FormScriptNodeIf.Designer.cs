
namespace Finmer.Editor
{
    partial class FormScriptNodeIf
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Panel pnlMode;
            System.Windows.Forms.Panel pnlOperand;
            System.Windows.Forms.Label label3;
            this.optModeAll = new System.Windows.Forms.RadioButton();
            this.optModeAny = new System.Windows.Forms.RadioButton();
            this.optOperandTrue = new System.Windows.Forms.RadioButton();
            this.optOperandFalse = new System.Windows.Forms.RadioButton();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.chkElse = new System.Windows.Forms.CheckBox();
            this.cmdConditionAdd = new System.Windows.Forms.Button();
            this.cmdConditionRemove = new System.Windows.Forms.Button();
            this.lsvConditions = new Finmer.Editor.BandedListView();
            label2 = new System.Windows.Forms.Label();
            pnlMode = new System.Windows.Forms.Panel();
            pnlOperand = new System.Windows.Forms.Panel();
            label3 = new System.Windows.Forms.Label();
            pnlMode.SuspendLayout();
            pnlOperand.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 248);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(14, 13);
            label2.TabIndex = 9;
            label2.Text = "is";
            // 
            // pnlMode
            // 
            pnlMode.Controls.Add(this.optModeAll);
            pnlMode.Controls.Add(this.optModeAny);
            pnlMode.Location = new System.Drawing.Point(16, 32);
            pnlMode.Name = "pnlMode";
            pnlMode.Size = new System.Drawing.Size(176, 56);
            pnlMode.TabIndex = 11;
            // 
            // optModeAll
            // 
            this.optModeAll.AutoSize = true;
            this.optModeAll.Location = new System.Drawing.Point(8, 8);
            this.optModeAll.Name = "optModeAll";
            this.optModeAll.Size = new System.Drawing.Size(121, 17);
            this.optModeAll.TabIndex = 7;
            this.optModeAll.Text = "ALL of the following:";
            this.optModeAll.UseVisualStyleBackColor = true;
            // 
            // optModeAny
            // 
            this.optModeAny.AutoSize = true;
            this.optModeAny.Location = new System.Drawing.Point(8, 32);
            this.optModeAny.Name = "optModeAny";
            this.optModeAny.Size = new System.Drawing.Size(124, 17);
            this.optModeAny.TabIndex = 8;
            this.optModeAny.Text = "ANY of the following:";
            this.optModeAny.UseVisualStyleBackColor = true;
            // 
            // pnlOperand
            // 
            pnlOperand.Controls.Add(this.optOperandTrue);
            pnlOperand.Controls.Add(this.optOperandFalse);
            pnlOperand.Location = new System.Drawing.Point(32, 240);
            pnlOperand.Name = "pnlOperand";
            pnlOperand.Size = new System.Drawing.Size(160, 56);
            pnlOperand.TabIndex = 12;
            // 
            // optOperandTrue
            // 
            this.optOperandTrue.AutoSize = true;
            this.optOperandTrue.Location = new System.Drawing.Point(8, 8);
            this.optOperandTrue.Name = "optOperandTrue";
            this.optOperandTrue.Size = new System.Drawing.Size(43, 17);
            this.optOperandTrue.TabIndex = 7;
            this.optOperandTrue.Text = "true";
            this.optOperandTrue.UseVisualStyleBackColor = true;
            // 
            // optOperandFalse
            // 
            this.optOperandFalse.AutoSize = true;
            this.optOperandFalse.Location = new System.Drawing.Point(8, 32);
            this.optOperandFalse.Name = "optOperandFalse";
            this.optOperandFalse.Size = new System.Drawing.Size(47, 17);
            this.optOperandFalse.TabIndex = 8;
            this.optOperandFalse.Text = "false";
            this.optOperandFalse.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(16, 16);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(140, 13);
            label3.TabIndex = 13;
            label3.Text = "Run the contained actions if";
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Enabled = false;
            this.cmdAccept.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdAccept.Location = new System.Drawing.Point(200, 312);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(119, 32);
            this.cmdAccept.TabIndex = 1;
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
            this.cmdCancel.Location = new System.Drawing.Point(328, 311);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(119, 32);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // chkElse
            // 
            this.chkElse.AutoSize = true;
            this.chkElse.Location = new System.Drawing.Point(24, 320);
            this.chkElse.Name = "chkElse";
            this.chkElse.Size = new System.Drawing.Size(109, 17);
            this.chkElse.TabIndex = 5;
            this.chkElse.Text = "Has \'Else\' Branch";
            this.chkElse.UseVisualStyleBackColor = true;
            // 
            // cmdConditionAdd
            // 
            this.cmdConditionAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConditionAdd.Image = global::Finmer.Editor.Properties.Resources.plus;
            this.cmdConditionAdd.Location = new System.Drawing.Point(264, 56);
            this.cmdConditionAdd.Name = "cmdConditionAdd";
            this.cmdConditionAdd.Size = new System.Drawing.Size(88, 28);
            this.cmdConditionAdd.TabIndex = 14;
            this.cmdConditionAdd.Text = "Add";
            this.cmdConditionAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdConditionAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdConditionAdd.UseVisualStyleBackColor = true;
            this.cmdConditionAdd.Click += new System.EventHandler(this.cmdConditionAdd_Click);
            // 
            // cmdConditionRemove
            // 
            this.cmdConditionRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConditionRemove.Enabled = false;
            this.cmdConditionRemove.Image = global::Finmer.Editor.Properties.Resources.minus;
            this.cmdConditionRemove.Location = new System.Drawing.Point(360, 56);
            this.cmdConditionRemove.Name = "cmdConditionRemove";
            this.cmdConditionRemove.Size = new System.Drawing.Size(88, 28);
            this.cmdConditionRemove.TabIndex = 15;
            this.cmdConditionRemove.Text = "Remove";
            this.cmdConditionRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdConditionRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdConditionRemove.UseVisualStyleBackColor = true;
            this.cmdConditionRemove.Click += new System.EventHandler(this.cmdConditionRemove_Click);
            // 
            // lsvConditions
            // 
            this.lsvConditions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lsvConditions.Location = new System.Drawing.Point(16, 96);
            this.lsvConditions.Name = "lsvConditions";
            this.lsvConditions.SelectedIndex = -1;
            this.lsvConditions.Size = new System.Drawing.Size(432, 136);
            this.lsvConditions.TabIndex = 16;
            this.lsvConditions.SelectedIndexChanged += new System.EventHandler(this.lsvConditions_SelectedIndexChanged);
            this.lsvConditions.ItemDoubleClick += new System.EventHandler(this.lsvConditions_DoubleClick);
            // 
            // FormScriptNodeIf
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(464, 360);
            this.Controls.Add(this.lsvConditions);
            this.Controls.Add(this.cmdConditionRemove);
            this.Controls.Add(this.cmdConditionAdd);
            this.Controls.Add(label3);
            this.Controls.Add(pnlOperand);
            this.Controls.Add(pnlMode);
            this.Controls.Add(label2);
            this.Controls.Add(this.chkElse);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormScriptNodeIf";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Conditional Branch";
            this.Load += new System.EventHandler(this.FormScriptNodeIf_Load);
            pnlMode.ResumeLayout(false);
            pnlMode.PerformLayout();
            pnlOperand.ResumeLayout(false);
            pnlOperand.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.CheckBox chkElse;
        private System.Windows.Forms.RadioButton optModeAll;
        private System.Windows.Forms.RadioButton optModeAny;
        private System.Windows.Forms.RadioButton optOperandTrue;
        private System.Windows.Forms.RadioButton optOperandFalse;
        private System.Windows.Forms.Button cmdConditionAdd;
        private System.Windows.Forms.Button cmdConditionRemove;
        private BandedListView lsvConditions;
    }
}