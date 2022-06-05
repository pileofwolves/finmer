
namespace Finmer.Editor
{
    partial class ConditionGroupEditor
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
            System.Windows.Forms.Panel pnlOperand;
            System.Windows.Forms.Panel pnlMode;
            System.Windows.Forms.Label lblOperand;
            this.optOperandTrue = new System.Windows.Forms.RadioButton();
            this.optOperandFalse = new System.Windows.Forms.RadioButton();
            this.optModeAll = new System.Windows.Forms.RadioButton();
            this.optModeAny = new System.Windows.Forms.RadioButton();
            this.cmdConditionRemove = new System.Windows.Forms.Button();
            this.cmdConditionAdd = new System.Windows.Forms.Button();
            this.lsvConditions = new Finmer.Editor.BandedListView();
            pnlOperand = new System.Windows.Forms.Panel();
            pnlMode = new System.Windows.Forms.Panel();
            lblOperand = new System.Windows.Forms.Label();
            pnlOperand.SuspendLayout();
            pnlMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOperand
            // 
            pnlOperand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            pnlOperand.Controls.Add(this.optOperandTrue);
            pnlOperand.Controls.Add(this.optOperandFalse);
            pnlOperand.Location = new System.Drawing.Point(16, 224);
            pnlOperand.Name = "pnlOperand";
            pnlOperand.Size = new System.Drawing.Size(160, 56);
            pnlOperand.TabIndex = 4;
            // 
            // optOperandTrue
            // 
            this.optOperandTrue.AutoSize = true;
            this.optOperandTrue.Location = new System.Drawing.Point(8, 8);
            this.optOperandTrue.Name = "optOperandTrue";
            this.optOperandTrue.Size = new System.Drawing.Size(43, 17);
            this.optOperandTrue.TabIndex = 0;
            this.optOperandTrue.Text = "true";
            this.optOperandTrue.UseVisualStyleBackColor = true;
            this.optOperandTrue.CheckedChanged += new System.EventHandler(this.optOperandTrue_CheckedChanged);
            // 
            // optOperandFalse
            // 
            this.optOperandFalse.AutoSize = true;
            this.optOperandFalse.Location = new System.Drawing.Point(8, 32);
            this.optOperandFalse.Name = "optOperandFalse";
            this.optOperandFalse.Size = new System.Drawing.Size(47, 17);
            this.optOperandFalse.TabIndex = 1;
            this.optOperandFalse.Text = "false";
            this.optOperandFalse.UseVisualStyleBackColor = true;
            // 
            // pnlMode
            // 
            pnlMode.Controls.Add(this.optModeAll);
            pnlMode.Controls.Add(this.optModeAny);
            pnlMode.Location = new System.Drawing.Point(0, 0);
            pnlMode.Name = "pnlMode";
            pnlMode.Size = new System.Drawing.Size(176, 56);
            pnlMode.TabIndex = 0;
            // 
            // optModeAll
            // 
            this.optModeAll.AutoSize = true;
            this.optModeAll.Location = new System.Drawing.Point(8, 8);
            this.optModeAll.Name = "optModeAll";
            this.optModeAll.Size = new System.Drawing.Size(121, 17);
            this.optModeAll.TabIndex = 0;
            this.optModeAll.Text = "ALL of the following:";
            this.optModeAll.UseVisualStyleBackColor = true;
            this.optModeAll.CheckedChanged += new System.EventHandler(this.optModeAll_CheckedChanged);
            // 
            // optModeAny
            // 
            this.optModeAny.AutoSize = true;
            this.optModeAny.Location = new System.Drawing.Point(8, 32);
            this.optModeAny.Name = "optModeAny";
            this.optModeAny.Size = new System.Drawing.Size(124, 17);
            this.optModeAny.TabIndex = 1;
            this.optModeAny.Text = "ANY of the following:";
            this.optModeAny.UseVisualStyleBackColor = true;
            // 
            // lblOperand
            // 
            lblOperand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            lblOperand.AutoSize = true;
            lblOperand.Location = new System.Drawing.Point(0, 232);
            lblOperand.Name = "lblOperand";
            lblOperand.Size = new System.Drawing.Size(14, 13);
            lblOperand.TabIndex = 17;
            lblOperand.Text = "is";
            // 
            // cmdConditionRemove
            // 
            this.cmdConditionRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConditionRemove.Enabled = false;
            this.cmdConditionRemove.Image = global::Finmer.Editor.Properties.Resources.minus;
            this.cmdConditionRemove.Location = new System.Drawing.Point(352, 32);
            this.cmdConditionRemove.Name = "cmdConditionRemove";
            this.cmdConditionRemove.Size = new System.Drawing.Size(88, 28);
            this.cmdConditionRemove.TabIndex = 2;
            this.cmdConditionRemove.Text = "Remove";
            this.cmdConditionRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdConditionRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdConditionRemove.UseVisualStyleBackColor = true;
            this.cmdConditionRemove.Click += new System.EventHandler(this.cmdConditionRemove_Click);
            // 
            // cmdConditionAdd
            // 
            this.cmdConditionAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConditionAdd.Image = global::Finmer.Editor.Properties.Resources.plus;
            this.cmdConditionAdd.Location = new System.Drawing.Point(256, 32);
            this.cmdConditionAdd.Name = "cmdConditionAdd";
            this.cmdConditionAdd.Size = new System.Drawing.Size(88, 28);
            this.cmdConditionAdd.TabIndex = 1;
            this.cmdConditionAdd.Text = "Add";
            this.cmdConditionAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdConditionAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdConditionAdd.UseVisualStyleBackColor = true;
            this.cmdConditionAdd.Click += new System.EventHandler(this.cmdConditionAdd_Click);
            // 
            // lsvConditions
            // 
            this.lsvConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvConditions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lsvConditions.Location = new System.Drawing.Point(0, 64);
            this.lsvConditions.Name = "lsvConditions";
            this.lsvConditions.SelectedIndex = -1;
            this.lsvConditions.Size = new System.Drawing.Size(448, 152);
            this.lsvConditions.TabIndex = 3;
            this.lsvConditions.SelectedIndexChanged += new System.EventHandler(this.lsvConditions_SelectedIndexChanged);
            this.lsvConditions.ItemDoubleClick += new System.EventHandler(this.lsvConditions_ItemDoubleClick);
            // 
            // ConditionGroupEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lsvConditions);
            this.Controls.Add(this.cmdConditionRemove);
            this.Controls.Add(this.cmdConditionAdd);
            this.Controls.Add(pnlOperand);
            this.Controls.Add(pnlMode);
            this.Controls.Add(lblOperand);
            this.Name = "ConditionGroupEditor";
            this.Size = new System.Drawing.Size(448, 280);
            pnlOperand.ResumeLayout(false);
            pnlOperand.PerformLayout();
            pnlMode.ResumeLayout(false);
            pnlMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BandedListView lsvConditions;
        private System.Windows.Forms.Button cmdConditionRemove;
        private System.Windows.Forms.Button cmdConditionAdd;
        private System.Windows.Forms.RadioButton optOperandTrue;
        private System.Windows.Forms.RadioButton optOperandFalse;
        private System.Windows.Forms.RadioButton optModeAll;
        private System.Windows.Forms.RadioButton optModeAny;
    }
}
