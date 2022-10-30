﻿namespace Finmer.Editor
{

    partial class FormScriptCmdVarSetNum
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.cmdAccept = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.txtContextName = new System.Windows.Forms.TextBox();
            this.optModeMul = new System.Windows.Forms.RadioButton();
            this.optModeAdd = new System.Windows.Forms.RadioButton();
            this.sveValue = new Finmer.Editor.ScriptValueFloatEditor();
            this.optModeDiv = new System.Windows.Forms.RadioButton();
            this.optModeSet = new System.Windows.Forms.RadioButton();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 72);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(56, 13);
            label1.TabIndex = 2;
            label1.Text = "Operation:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 16);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(79, 13);
            label2.TabIndex = 0;
            label2.Text = "Variable Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(16, 136);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(97, 13);
            label3.TabIndex = 7;
            label3.Text = "with / to this value:";
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdAccept.Location = new System.Drawing.Point(56, 376);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(119, 32);
            this.cmdAccept.TabIndex = 9;
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
            this.cmdCancel.Location = new System.Drawing.Point(184, 375);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(119, 32);
            this.cmdCancel.TabIndex = 10;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // txtContextName
            // 
            this.txtContextName.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContextName.Location = new System.Drawing.Point(16, 32);
            this.txtContextName.Name = "txtContextName";
            this.txtContextName.Size = new System.Drawing.Size(288, 22);
            this.txtContextName.TabIndex = 1;
            // 
            // optModeMul
            // 
            this.optModeMul.AutoSize = true;
            this.optModeMul.Location = new System.Drawing.Point(80, 96);
            this.optModeMul.Name = "optModeMul";
            this.optModeMul.Size = new System.Drawing.Size(60, 17);
            this.optModeMul.TabIndex = 4;
            this.optModeMul.TabStop = true;
            this.optModeMul.Text = "Multiply";
            this.optModeMul.UseVisualStyleBackColor = true;
            // 
            // optModeAdd
            // 
            this.optModeAdd.AutoSize = true;
            this.optModeAdd.Location = new System.Drawing.Point(24, 96);
            this.optModeAdd.Name = "optModeAdd";
            this.optModeAdd.Size = new System.Drawing.Size(44, 17);
            this.optModeAdd.TabIndex = 3;
            this.optModeAdd.TabStop = true;
            this.optModeAdd.Text = "Add";
            this.optModeAdd.UseVisualStyleBackColor = true;
            // 
            // sveValue
            // 
            this.sveValue.Location = new System.Drawing.Point(16, 160);
            this.sveValue.Name = "sveValue";
            this.sveValue.Size = new System.Drawing.Size(295, 198);
            this.sveValue.TabIndex = 8;
            // 
            // optModeDiv
            // 
            this.optModeDiv.AutoSize = true;
            this.optModeDiv.Location = new System.Drawing.Point(144, 96);
            this.optModeDiv.Name = "optModeDiv";
            this.optModeDiv.Size = new System.Drawing.Size(55, 17);
            this.optModeDiv.TabIndex = 5;
            this.optModeDiv.TabStop = true;
            this.optModeDiv.Text = "Divide";
            this.optModeDiv.UseVisualStyleBackColor = true;
            // 
            // optModeSet
            // 
            this.optModeSet.AutoSize = true;
            this.optModeSet.Location = new System.Drawing.Point(208, 96);
            this.optModeSet.Name = "optModeSet";
            this.optModeSet.Size = new System.Drawing.Size(41, 17);
            this.optModeSet.TabIndex = 6;
            this.optModeSet.TabStop = true;
            this.optModeSet.Text = "Set";
            this.optModeSet.UseVisualStyleBackColor = true;
            // 
            // FormScriptCmdVarSetNum
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(320, 424);
            this.Controls.Add(label3);
            this.Controls.Add(this.optModeSet);
            this.Controls.Add(this.optModeDiv);
            this.Controls.Add(this.sveValue);
            this.Controls.Add(this.optModeMul);
            this.Controls.Add(this.optModeAdd);
            this.Controls.Add(label1);
            this.Controls.Add(this.txtContextName);
            this.Controls.Add(label2);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormScriptCmdVarSetNum";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set Number Variable";
            this.Load += new System.EventHandler(this.FormScriptCmdSetPlayerName_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.TextBox txtContextName;
        private System.Windows.Forms.RadioButton optModeMul;
        private System.Windows.Forms.RadioButton optModeAdd;
        private ScriptValueFloatEditor sveValue;
        private System.Windows.Forms.RadioButton optModeDiv;
        private System.Windows.Forms.RadioButton optModeSet;
    }

}