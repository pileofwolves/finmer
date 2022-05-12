namespace Finmer.Editor
{

    partial class ScriptValueStringEditor
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
            this.txtVar = new System.Windows.Forms.TextBox();
            this.optModeVar = new System.Windows.Forms.RadioButton();
            this.optModeLiteral = new System.Windows.Forms.RadioButton();
            this.txtLiteral = new System.Windows.Forms.TextBox();
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
            // txtVar
            // 
            this.txtVar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVar.Enabled = false;
            this.txtVar.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVar.Location = new System.Drawing.Point(8, 88);
            this.txtVar.Name = "txtVar";
            this.txtVar.Size = new System.Drawing.Size(280, 22);
            this.txtVar.TabIndex = 4;
            // 
            // optModeVar
            // 
            this.optModeVar.AutoSize = true;
            this.optModeVar.Location = new System.Drawing.Point(0, 64);
            this.optModeVar.Name = "optModeVar";
            this.optModeVar.Size = new System.Drawing.Size(103, 17);
            this.optModeVar.TabIndex = 3;
            this.optModeVar.TabStop = true;
            this.optModeVar.Text = "Number Variable";
            this.optModeVar.UseVisualStyleBackColor = true;
            this.optModeVar.CheckedChanged += new System.EventHandler(this.optModeNumberVar_CheckedChanged);
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
            // txtLiteral
            // 
            this.txtLiteral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLiteral.Enabled = false;
            this.txtLiteral.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLiteral.Location = new System.Drawing.Point(8, 24);
            this.txtLiteral.Name = "txtLiteral";
            this.txtLiteral.Size = new System.Drawing.Size(280, 22);
            this.txtLiteral.TabIndex = 2;
            // 
            // ScriptValueStringEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtLiteral);
            this.Controls.Add(this.txtLua);
            this.Controls.Add(this.optModeInlineLua);
            this.Controls.Add(this.txtVar);
            this.Controls.Add(this.optModeVar);
            this.Controls.Add(this.optModeLiteral);
            this.Name = "ScriptValueStringEditor";
            this.Size = new System.Drawing.Size(295, 198);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLua;
        private System.Windows.Forms.RadioButton optModeInlineLua;
        private System.Windows.Forms.TextBox txtVar;
        private System.Windows.Forms.RadioButton optModeVar;
        private System.Windows.Forms.RadioButton optModeLiteral;
        private System.Windows.Forms.TextBox txtLiteral;
    }

}
