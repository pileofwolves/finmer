
namespace Finmer.Editor
{
    partial class FormScriptNodeSingleInt
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
            this.cmdAccept = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.txtLua = new System.Windows.Forms.TextBox();
            this.optModeInlineLua = new System.Windows.Forms.RadioButton();
            this.txtNumberVar = new System.Windows.Forms.TextBox();
            this.optModeNumberVar = new System.Windows.Forms.RadioButton();
            this.optModeLiteral = new System.Windows.Forms.RadioButton();
            this.nudOperand = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudOperand)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdAccept.Location = new System.Drawing.Point(62, 239);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(121, 32);
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
            this.cmdCancel.Location = new System.Drawing.Point(190, 239);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(121, 32);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // txtLua
            // 
            this.txtLua.Enabled = false;
            this.txtLua.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLua.Location = new System.Drawing.Point(24, 168);
            this.txtLua.Name = "txtLua";
            this.txtLua.Size = new System.Drawing.Size(280, 22);
            this.txtLua.TabIndex = 23;
            // 
            // optModeInlineLua
            // 
            this.optModeInlineLua.AutoSize = true;
            this.optModeInlineLua.Location = new System.Drawing.Point(16, 144);
            this.optModeInlineLua.Name = "optModeInlineLua";
            this.optModeInlineLua.Size = new System.Drawing.Size(73, 17);
            this.optModeInlineLua.TabIndex = 22;
            this.optModeInlineLua.TabStop = true;
            this.optModeInlineLua.Text = "Lua Script";
            this.optModeInlineLua.UseVisualStyleBackColor = true;
            this.optModeInlineLua.CheckedChanged += new System.EventHandler(this.optModeInlineLua_CheckedChanged);
            // 
            // txtNumberVar
            // 
            this.txtNumberVar.Enabled = false;
            this.txtNumberVar.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumberVar.Location = new System.Drawing.Point(24, 104);
            this.txtNumberVar.Name = "txtNumberVar";
            this.txtNumberVar.Size = new System.Drawing.Size(280, 22);
            this.txtNumberVar.TabIndex = 21;
            // 
            // optModeNumberVar
            // 
            this.optModeNumberVar.AutoSize = true;
            this.optModeNumberVar.Location = new System.Drawing.Point(16, 80);
            this.optModeNumberVar.Name = "optModeNumberVar";
            this.optModeNumberVar.Size = new System.Drawing.Size(103, 17);
            this.optModeNumberVar.TabIndex = 20;
            this.optModeNumberVar.TabStop = true;
            this.optModeNumberVar.Text = "Number Variable";
            this.optModeNumberVar.UseVisualStyleBackColor = true;
            this.optModeNumberVar.CheckedChanged += new System.EventHandler(this.optModeNumberVar_CheckedChanged);
            // 
            // optModeLiteral
            // 
            this.optModeLiteral.AutoSize = true;
            this.optModeLiteral.Location = new System.Drawing.Point(16, 16);
            this.optModeLiteral.Name = "optModeLiteral";
            this.optModeLiteral.Size = new System.Drawing.Size(82, 17);
            this.optModeLiteral.TabIndex = 19;
            this.optModeLiteral.TabStop = true;
            this.optModeLiteral.Text = "Exact Value";
            this.optModeLiteral.UseVisualStyleBackColor = true;
            this.optModeLiteral.CheckedChanged += new System.EventHandler(this.optModeLiteral_CheckedChanged);
            // 
            // nudOperand
            // 
            this.nudOperand.Enabled = false;
            this.nudOperand.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudOperand.Location = new System.Drawing.Point(24, 40);
            this.nudOperand.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudOperand.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.nudOperand.Name = "nudOperand";
            this.nudOperand.Size = new System.Drawing.Size(216, 23);
            this.nudOperand.TabIndex = 18;
            // 
            // FormScriptNodeSingleInt
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(328, 288);
            this.Controls.Add(this.txtLua);
            this.Controls.Add(this.optModeInlineLua);
            this.Controls.Add(this.txtNumberVar);
            this.Controls.Add(this.optModeNumberVar);
            this.Controls.Add(this.optModeLiteral);
            this.Controls.Add(this.nudOperand);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormScriptNodeSingleInt";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Single Int32";
            this.Load += new System.EventHandler(this.FormScriptNodeSingleInt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudOperand)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.TextBox txtLua;
        private System.Windows.Forms.RadioButton optModeInlineLua;
        private System.Windows.Forms.TextBox txtNumberVar;
        private System.Windows.Forms.RadioButton optModeNumberVar;
        private System.Windows.Forms.RadioButton optModeLiteral;
        private System.Windows.Forms.NumericUpDown nudOperand;
    }
}