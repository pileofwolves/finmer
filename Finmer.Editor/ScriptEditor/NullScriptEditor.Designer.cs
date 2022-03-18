
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
            System.Windows.Forms.GroupBox groupBox;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            this.cmdCreateRaw = new System.Windows.Forms.Button();
            this.cmdCreateVisual = new System.Windows.Forms.Button();
            groupBox = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            groupBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            groupBox.Controls.Add(label2);
            groupBox.Controls.Add(this.cmdCreateRaw);
            groupBox.Controls.Add(label1);
            groupBox.Controls.Add(this.cmdCreateVisual);
            groupBox.Location = new System.Drawing.Point(16, 16);
            groupBox.Name = "groupBox";
            groupBox.Size = new System.Drawing.Size(224, 200);
            groupBox.TabIndex = 0;
            groupBox.TabStop = false;
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(16, 152);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(192, 32);
            label2.TabIndex = 3;
            label2.Text = "Write Lua script code directly. Recommended for advanced users.";
            // 
            // cmdCreateRaw
            // 
            this.cmdCreateRaw.Enabled = false;
            this.cmdCreateRaw.Image = global::Finmer.Editor.Properties.Resources.script_code;
            this.cmdCreateRaw.Location = new System.Drawing.Point(16, 112);
            this.cmdCreateRaw.Name = "cmdCreateRaw";
            this.cmdCreateRaw.Size = new System.Drawing.Size(192, 32);
            this.cmdCreateRaw.TabIndex = 2;
            this.cmdCreateRaw.Text = "Create Raw Script";
            this.cmdCreateRaw.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCreateRaw.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCreateRaw.UseVisualStyleBackColor = true;
            this.cmdCreateRaw.Click += new System.EventHandler(this.cmdCreateRaw_Click);
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(16, 64);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(192, 32);
            label1.TabIndex = 1;
            label1.Text = "Visually arrange bite-sized commands. Recommended for most users.";
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
            // NullScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(groupBox);
            this.Name = "NullScriptEditor";
            this.Size = new System.Drawing.Size(258, 233);
            groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCreateVisual;
        private System.Windows.Forms.Button cmdCreateRaw;
    }
}
