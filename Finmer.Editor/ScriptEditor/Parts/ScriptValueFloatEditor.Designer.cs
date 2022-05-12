
namespace Finmer.Editor
{
    partial class ScriptValueFloatEditor
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
            this.txtLua = new System.Windows.Forms.TextBox();
            this.optModeInlineLua = new System.Windows.Forms.RadioButton();
            this.txtNumberVar = new System.Windows.Forms.TextBox();
            this.optModeNumberVar = new System.Windows.Forms.RadioButton();
            this.optModeLiteral = new System.Windows.Forms.RadioButton();
            this.nudOperand = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudOperand)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLua
            // 
            this.txtLua.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLua.Enabled = false;
            this.txtLua.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLua.Location = new System.Drawing.Point(8, 152);
            this.txtLua.Name = "txtLua";
            this.txtLua.Size = new System.Drawing.Size(280, 22);
            this.txtLua.TabIndex = 6;
            // 
            // optModeInlineLua
            // 
            this.optModeInlineLua.AutoSize = true;
            this.optModeInlineLua.Location = new System.Drawing.Point(0, 128);
            this.optModeInlineLua.Name = "optModeInlineLua";
            this.optModeInlineLua.Size = new System.Drawing.Size(73, 17);
            this.optModeInlineLua.TabIndex = 5;
            this.optModeInlineLua.TabStop = true;
            this.optModeInlineLua.Text = "Lua Script";
            this.optModeInlineLua.UseVisualStyleBackColor = true;
            this.optModeInlineLua.CheckedChanged += new System.EventHandler(this.optModeInlineLua_CheckedChanged);
            // 
            // txtNumberVar
            // 
            this.txtNumberVar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumberVar.Enabled = false;
            this.txtNumberVar.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumberVar.Location = new System.Drawing.Point(8, 88);
            this.txtNumberVar.Name = "txtNumberVar";
            this.txtNumberVar.Size = new System.Drawing.Size(280, 22);
            this.txtNumberVar.TabIndex = 4;
            // 
            // optModeNumberVar
            // 
            this.optModeNumberVar.AutoSize = true;
            this.optModeNumberVar.Location = new System.Drawing.Point(0, 64);
            this.optModeNumberVar.Name = "optModeNumberVar";
            this.optModeNumberVar.Size = new System.Drawing.Size(103, 17);
            this.optModeNumberVar.TabIndex = 3;
            this.optModeNumberVar.TabStop = true;
            this.optModeNumberVar.Text = "Number Variable";
            this.optModeNumberVar.UseVisualStyleBackColor = true;
            this.optModeNumberVar.CheckedChanged += new System.EventHandler(this.optModeNumberVar_CheckedChanged);
            // 
            // optModeLiteral
            // 
            this.optModeLiteral.AutoSize = true;
            this.optModeLiteral.Location = new System.Drawing.Point(0, 0);
            this.optModeLiteral.Name = "optModeLiteral";
            this.optModeLiteral.Size = new System.Drawing.Size(82, 17);
            this.optModeLiteral.TabIndex = 1;
            this.optModeLiteral.TabStop = true;
            this.optModeLiteral.Text = "Exact Value";
            this.optModeLiteral.UseVisualStyleBackColor = true;
            this.optModeLiteral.CheckedChanged += new System.EventHandler(this.optModeLiteral_CheckedChanged);
            // 
            // nudOperand
            // 
            this.nudOperand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudOperand.DecimalPlaces = 2;
            this.nudOperand.Enabled = false;
            this.nudOperand.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudOperand.Location = new System.Drawing.Point(8, 24);
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
            this.nudOperand.TabIndex = 2;
            // 
            // ScriptValueFloatEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtLua);
            this.Controls.Add(this.optModeInlineLua);
            this.Controls.Add(this.txtNumberVar);
            this.Controls.Add(this.optModeNumberVar);
            this.Controls.Add(this.optModeLiteral);
            this.Controls.Add(this.nudOperand);
            this.Name = "ScriptValueFloatEditor";
            this.Size = new System.Drawing.Size(295, 198);
            ((System.ComponentModel.ISupportInitialize)(this.nudOperand)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLua;
        private System.Windows.Forms.RadioButton optModeInlineLua;
        private System.Windows.Forms.TextBox txtNumberVar;
        private System.Windows.Forms.RadioButton optModeNumberVar;
        private System.Windows.Forms.RadioButton optModeLiteral;
        private System.Windows.Forms.NumericUpDown nudOperand;
    }
}
