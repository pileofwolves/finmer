
namespace Finmer.Editor
{
    partial class FormScriptValueLiteral
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
            this.cmdAccept = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.optTypeNum = new System.Windows.Forms.RadioButton();
            this.optTypeBool = new System.Windows.Forms.RadioButton();
            this.optTypeString = new System.Windows.Forms.RadioButton();
            this.optTypeNil = new System.Windows.Forms.RadioButton();
            this.nudValue = new System.Windows.Forms.NumericUpDown();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.chkValue = new System.Windows.Forms.CheckBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdAccept.Location = new System.Drawing.Point(217, 143);
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
            this.cmdCancel.Location = new System.Drawing.Point(345, 142);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(119, 32);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 16);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(34, 13);
            label1.TabIndex = 3;
            label1.Text = "Type:";
            // 
            // optTypeNum
            // 
            this.optTypeNum.AutoSize = true;
            this.optTypeNum.Location = new System.Drawing.Point(16, 40);
            this.optTypeNum.Name = "optTypeNum";
            this.optTypeNum.Size = new System.Drawing.Size(62, 17);
            this.optTypeNum.TabIndex = 4;
            this.optTypeNum.TabStop = true;
            this.optTypeNum.Text = "Number";
            this.optTypeNum.UseVisualStyleBackColor = true;
            this.optTypeNum.CheckedChanged += new System.EventHandler(this.optTypeNum_CheckedChanged);
            // 
            // optTypeBool
            // 
            this.optTypeBool.AutoSize = true;
            this.optTypeBool.Location = new System.Drawing.Point(16, 64);
            this.optTypeBool.Name = "optTypeBool";
            this.optTypeBool.Size = new System.Drawing.Size(131, 17);
            this.optTypeBool.TabIndex = 5;
            this.optTypeBool.TabStop = true;
            this.optTypeBool.Text = "Boolean (True / False)";
            this.optTypeBool.UseVisualStyleBackColor = true;
            this.optTypeBool.CheckedChanged += new System.EventHandler(this.optTypeBool_CheckedChanged);
            // 
            // optTypeString
            // 
            this.optTypeString.AutoSize = true;
            this.optTypeString.Location = new System.Drawing.Point(16, 88);
            this.optTypeString.Name = "optTypeString";
            this.optTypeString.Size = new System.Drawing.Size(82, 17);
            this.optTypeString.TabIndex = 6;
            this.optTypeString.TabStop = true;
            this.optTypeString.Text = "String (Text)";
            this.optTypeString.UseVisualStyleBackColor = true;
            this.optTypeString.CheckedChanged += new System.EventHandler(this.optTypeString_CheckedChanged);
            // 
            // optTypeNil
            // 
            this.optTypeNil.AutoSize = true;
            this.optTypeNil.Location = new System.Drawing.Point(16, 112);
            this.optTypeNil.Name = "optTypeNil";
            this.optTypeNil.Size = new System.Drawing.Size(89, 17);
            this.optTypeNil.TabIndex = 7;
            this.optTypeNil.TabStop = true;
            this.optTypeNil.Text = "Nil (No value)";
            this.optTypeNil.UseVisualStyleBackColor = true;
            this.optTypeNil.CheckedChanged += new System.EventHandler(this.optTypeNil_CheckedChanged);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(192, 16);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(37, 13);
            label2.TabIndex = 8;
            label2.Text = "Value:";
            // 
            // nudValue
            // 
            this.nudValue.DecimalPlaces = 1;
            this.nudValue.Location = new System.Drawing.Point(192, 32);
            this.nudValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudValue.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudValue.Name = "nudValue";
            this.nudValue.Size = new System.Drawing.Size(128, 20);
            this.nudValue.TabIndex = 9;
            this.nudValue.Visible = false;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(192, 32);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(272, 20);
            this.txtValue.TabIndex = 10;
            this.txtValue.Visible = false;
            // 
            // chkValue
            // 
            this.chkValue.AutoSize = true;
            this.chkValue.Location = new System.Drawing.Point(200, 40);
            this.chkValue.Name = "chkValue";
            this.chkValue.Size = new System.Drawing.Size(15, 14);
            this.chkValue.TabIndex = 11;
            this.chkValue.UseVisualStyleBackColor = true;
            this.chkValue.Visible = false;
            // 
            // FormScriptValueLiteral
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(481, 191);
            this.Controls.Add(this.chkValue);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.nudValue);
            this.Controls.Add(label2);
            this.Controls.Add(this.optTypeNil);
            this.Controls.Add(this.optTypeString);
            this.Controls.Add(this.optTypeBool);
            this.Controls.Add(this.optTypeNum);
            this.Controls.Add(label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormScriptValueLiteral";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Exact Value";
            this.Load += new System.EventHandler(this.FormScriptValueLiteral_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.RadioButton optTypeNum;
        private System.Windows.Forms.RadioButton optTypeBool;
        private System.Windows.Forms.RadioButton optTypeString;
        private System.Windows.Forms.RadioButton optTypeNil;
        private System.Windows.Forms.NumericUpDown nudValue;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.CheckBox chkValue;
    }
}