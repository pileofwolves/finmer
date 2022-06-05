
namespace Finmer.Editor
{
    partial class NullScriptEditor
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label3;
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.cmdCreateRaw = new System.Windows.Forms.Button();
            this.cmdCreateVisual = new System.Windows.Forms.Button();
            this.lblEmptyNotice = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(16, 176);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(192, 32);
            label2.TabIndex = 3;
            label2.Text = "Write Lua script code directly. Recommended for advanced users.";
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(16, 64);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(192, 32);
            label1.TabIndex = 1;
            label1.Text = "Visually arrange bite-sized commands. Recommended for most users.";
            // 
            // label3
            // 
            label3.ForeColor = System.Drawing.Color.Maroon;
            label3.Location = new System.Drawing.Point(16, 92);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(192, 32);
            label3.TabIndex = 4;
            label3.Text = "Warning: This feature is new and highly experimental. Expect bugs.";
            // 
            // groupBox
            // 
            this.groupBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox.Controls.Add(label3);
            this.groupBox.Controls.Add(label2);
            this.groupBox.Controls.Add(this.cmdCreateRaw);
            this.groupBox.Controls.Add(label1);
            this.groupBox.Controls.Add(this.cmdCreateVisual);
            this.groupBox.Location = new System.Drawing.Point(16, 16);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(224, 224);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Visible = false;
            // 
            // cmdCreateRaw
            // 
            this.cmdCreateRaw.Enabled = false;
            this.cmdCreateRaw.Image = global::Finmer.Editor.Properties.Resources.script_code;
            this.cmdCreateRaw.Location = new System.Drawing.Point(16, 136);
            this.cmdCreateRaw.Name = "cmdCreateRaw";
            this.cmdCreateRaw.Size = new System.Drawing.Size(192, 32);
            this.cmdCreateRaw.TabIndex = 2;
            this.cmdCreateRaw.Text = "Create Raw Script";
            this.cmdCreateRaw.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCreateRaw.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCreateRaw.UseVisualStyleBackColor = true;
            this.cmdCreateRaw.Click += new System.EventHandler(this.cmdCreateRaw_Click);
            // 
            // cmdCreateVisual
            // 
            this.cmdCreateVisual.Enabled = false;
            this.cmdCreateVisual.Image = global::Finmer.Editor.Properties.Resources.box;
            this.cmdCreateVisual.Location = new System.Drawing.Point(16, 24);
            this.cmdCreateVisual.Name = "cmdCreateVisual";
            this.cmdCreateVisual.Size = new System.Drawing.Size(192, 32);
            this.cmdCreateVisual.TabIndex = 0;
            this.cmdCreateVisual.Text = "Create Visual Script";
            this.cmdCreateVisual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCreateVisual.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCreateVisual.UseVisualStyleBackColor = true;
            this.cmdCreateVisual.Click += new System.EventHandler(this.cmdCreateVisual_Click);
            // 
            // lblEmptyNotice
            // 
            this.lblEmptyNotice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEmptyNotice.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblEmptyNotice.Location = new System.Drawing.Point(0, 0);
            this.lblEmptyNotice.Name = "lblEmptyNotice";
            this.lblEmptyNotice.Size = new System.Drawing.Size(256, 256);
            this.lblEmptyNotice.TabIndex = 1;
            this.lblEmptyNotice.Text = "This script is empty.\r\n\r\nMove your mouse here to create one!";
            this.lblEmptyNotice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmptyNotice.MouseEnter += new System.EventHandler(this.lblEmptyNotice_MouseEnter);
            // 
            // NullScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.lblEmptyNotice);
            this.Name = "NullScriptEditor";
            this.Size = new System.Drawing.Size(258, 256);
            this.MouseLeave += new System.EventHandler(this.NullScriptEditor_MouseLeave);
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCreateVisual;
        private System.Windows.Forms.Button cmdCreateRaw;
        private System.Windows.Forms.Label lblEmptyNotice;
        private System.Windows.Forms.GroupBox groupBox;
    }
}
