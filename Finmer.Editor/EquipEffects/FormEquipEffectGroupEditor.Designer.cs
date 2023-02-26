
namespace Finmer.Editor
{
    partial class FormEquipEffectGroupEditor
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.GroupBox grpBuffs;
            System.Windows.Forms.ColumnHeader clhEquipEffectTitle;
            System.Windows.Forms.Label label5;
            this.cmdBuffEdit = new System.Windows.Forms.Button();
            this.cmdBuffRemove = new System.Windows.Forms.Button();
            this.lsvBuffs = new System.Windows.Forms.ListView();
            this.cmdBuffAdd = new System.Windows.Forms.Button();
            this.cmbProcMode = new System.Windows.Forms.ComboBox();
            this.grpProcSettings = new System.Windows.Forms.GroupBox();
            this.txtProcString = new System.Windows.Forms.TextBox();
            this.nudProcDuration = new System.Windows.Forms.NumericUpDown();
            this.nudProcChance = new System.Windows.Forms.NumericUpDown();
            this.chkIsVolatile = new System.Windows.Forms.CheckBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.mnuBuffAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEffectDiceAttack = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEffectDiceDefense = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEffectDiceGrapple = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEffectDiceSwallow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEffectDiceStruggle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEffectStatHP = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.miscToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEffectCustomText = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbProcTarget = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            grpBuffs = new System.Windows.Forms.GroupBox();
            clhEquipEffectTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            label5 = new System.Windows.Forms.Label();
            grpBuffs.SuspendLayout();
            this.grpProcSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudProcDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudProcChance)).BeginInit();
            this.mnuBuffAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 16);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(114, 13);
            label1.TabIndex = 0;
            label1.Text = "Apply this group when:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 24);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(57, 13);
            label2.TabIndex = 0;
            label2.Text = "% chance:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(120, 24);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(91, 13);
            label3.TabIndex = 2;
            label3.Text = "Duration (rounds):";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(16, 120);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(210, 13);
            label4.TabIndex = 6;
            label4.Text = "Text to print on activation (string table key):";
            // 
            // grpBuffs
            // 
            grpBuffs.Controls.Add(this.cmdBuffEdit);
            grpBuffs.Controls.Add(this.cmdBuffRemove);
            grpBuffs.Controls.Add(this.lsvBuffs);
            grpBuffs.Controls.Add(this.cmdBuffAdd);
            grpBuffs.Location = new System.Drawing.Point(296, 16);
            grpBuffs.Name = "grpBuffs";
            grpBuffs.Size = new System.Drawing.Size(264, 216);
            grpBuffs.TabIndex = 3;
            grpBuffs.TabStop = false;
            grpBuffs.Text = "Buffs Applied By Group";
            // 
            // cmdBuffEdit
            // 
            this.cmdBuffEdit.Enabled = false;
            this.cmdBuffEdit.Image = global::Finmer.Editor.Properties.Resources.pencil;
            this.cmdBuffEdit.Location = new System.Drawing.Point(160, 24);
            this.cmdBuffEdit.Name = "cmdBuffEdit";
            this.cmdBuffEdit.Size = new System.Drawing.Size(32, 32);
            this.cmdBuffEdit.TabIndex = 2;
            this.cmdBuffEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdBuffEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdBuffEdit.UseVisualStyleBackColor = true;
            this.cmdBuffEdit.Click += new System.EventHandler(this.cmdBuffEdit_Click);
            // 
            // cmdBuffRemove
            // 
            this.cmdBuffRemove.Enabled = false;
            this.cmdBuffRemove.Image = global::Finmer.Editor.Properties.Resources.cross_script;
            this.cmdBuffRemove.Location = new System.Drawing.Point(120, 24);
            this.cmdBuffRemove.Name = "cmdBuffRemove";
            this.cmdBuffRemove.Size = new System.Drawing.Size(32, 32);
            this.cmdBuffRemove.TabIndex = 1;
            this.cmdBuffRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdBuffRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdBuffRemove.UseVisualStyleBackColor = true;
            this.cmdBuffRemove.Click += new System.EventHandler(this.cmdBuffRemove_Click);
            // 
            // lsvBuffs
            // 
            this.lsvBuffs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clhEquipEffectTitle});
            this.lsvBuffs.FullRowSelect = true;
            this.lsvBuffs.GridLines = true;
            this.lsvBuffs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvBuffs.HideSelection = false;
            this.lsvBuffs.Location = new System.Drawing.Point(8, 64);
            this.lsvBuffs.Name = "lsvBuffs";
            this.lsvBuffs.Size = new System.Drawing.Size(248, 144);
            this.lsvBuffs.TabIndex = 3;
            this.lsvBuffs.UseCompatibleStateImageBehavior = false;
            this.lsvBuffs.View = System.Windows.Forms.View.Details;
            this.lsvBuffs.SelectedIndexChanged += new System.EventHandler(this.lsvBuffs_SelectedIndexChanged);
            this.lsvBuffs.DoubleClick += new System.EventHandler(this.lsvBuffs_DoubleClick);
            // 
            // clhEquipEffectTitle
            // 
            clhEquipEffectTitle.Text = "Effect";
            clhEquipEffectTitle.Width = 220;
            // 
            // cmdBuffAdd
            // 
            this.cmdBuffAdd.Image = global::Finmer.Editor.Properties.Resources.plus;
            this.cmdBuffAdd.Location = new System.Drawing.Point(16, 24);
            this.cmdBuffAdd.Name = "cmdBuffAdd";
            this.cmdBuffAdd.Size = new System.Drawing.Size(96, 32);
            this.cmdBuffAdd.TabIndex = 0;
            this.cmdBuffAdd.Text = "Add Effect";
            this.cmdBuffAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdBuffAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdBuffAdd.UseVisualStyleBackColor = true;
            this.cmdBuffAdd.Click += new System.EventHandler(this.cmdBuffAdd_Click);
            // 
            // cmbProcMode
            // 
            this.cmbProcMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProcMode.FormattingEnabled = true;
            this.cmbProcMode.Items.AddRange(new object[] {
            "Always (While equipped)",
            "New round starts",
            "Wielder\'s turn starts",
            "Wielder hits an enemy",
            "Wielder misses an enemy",
            "Wielder begins grappling",
            "Wielder is swallowed",
            "Wielder swallows a prey",
            "Enemy hits the wielder",
            "Enemy misses the wielder"});
            this.cmbProcMode.Location = new System.Drawing.Point(16, 32);
            this.cmbProcMode.Name = "cmbProcMode";
            this.cmbProcMode.Size = new System.Drawing.Size(264, 21);
            this.cmbProcMode.TabIndex = 1;
            this.cmbProcMode.SelectedIndexChanged += new System.EventHandler(this.cmbProcMode_SelectedIndexChanged);
            // 
            // grpProcSettings
            // 
            this.grpProcSettings.Controls.Add(this.cmbProcTarget);
            this.grpProcSettings.Controls.Add(label5);
            this.grpProcSettings.Controls.Add(this.txtProcString);
            this.grpProcSettings.Controls.Add(label4);
            this.grpProcSettings.Controls.Add(this.nudProcDuration);
            this.grpProcSettings.Controls.Add(this.nudProcChance);
            this.grpProcSettings.Controls.Add(this.chkIsVolatile);
            this.grpProcSettings.Controls.Add(label3);
            this.grpProcSettings.Controls.Add(label2);
            this.grpProcSettings.Location = new System.Drawing.Point(16, 72);
            this.grpProcSettings.Name = "grpProcSettings";
            this.grpProcSettings.Size = new System.Drawing.Size(264, 208);
            this.grpProcSettings.TabIndex = 2;
            this.grpProcSettings.TabStop = false;
            this.grpProcSettings.Text = "Activation Settings";
            // 
            // txtProcString
            // 
            this.txtProcString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProcString.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProcString.Location = new System.Drawing.Point(16, 136);
            this.txtProcString.Name = "txtProcString";
            this.txtProcString.Size = new System.Drawing.Size(232, 22);
            this.txtProcString.TabIndex = 7;
            // 
            // nudProcDuration
            // 
            this.nudProcDuration.Location = new System.Drawing.Point(120, 40);
            this.nudProcDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudProcDuration.Name = "nudProcDuration";
            this.nudProcDuration.Size = new System.Drawing.Size(88, 20);
            this.nudProcDuration.TabIndex = 3;
            this.nudProcDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudProcChance
            // 
            this.nudProcChance.DecimalPlaces = 1;
            this.nudProcChance.Location = new System.Drawing.Point(16, 40);
            this.nudProcChance.Name = "nudProcChance";
            this.nudProcChance.Size = new System.Drawing.Size(88, 20);
            this.nudProcChance.TabIndex = 1;
            // 
            // chkIsVolatile
            // 
            this.chkIsVolatile.AutoSize = true;
            this.chkIsVolatile.Location = new System.Drawing.Point(16, 176);
            this.chkIsVolatile.Name = "chkIsVolatile";
            this.chkIsVolatile.Size = new System.Drawing.Size(189, 17);
            this.chkIsVolatile.TabIndex = 8;
            this.chkIsVolatile.Text = "Remove effect when combat ends";
            this.chkIsVolatile.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Image = global::Finmer.Editor.Properties.Resources.cross;
            this.cmdCancel.Location = new System.Drawing.Point(448, 264);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(112, 32);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdOK.Location = new System.Drawing.Point(328, 264);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(112, 32);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.Text = "Accept";
            this.cmdOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // mnuBuffAdd
            // 
            this.mnuBuffAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.mnuEffectDiceAttack,
            this.mnuEffectDiceDefense,
            this.mnuEffectDiceGrapple,
            this.mnuEffectDiceSwallow,
            this.mnuEffectDiceStruggle,
            this.toolStripSeparator1,
            this.toolStripMenuItem1,
            this.mnuEffectStatHP,
            this.toolStripSeparator2,
            this.miscToolStripMenuItem,
            this.mnuEffectCustomText});
            this.mnuBuffAdd.Name = "mnuEquipEffectAdd";
            this.mnuBuffAdd.Size = new System.Drawing.Size(145, 236);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Enabled = false;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(144, 22);
            this.toolStripMenuItem2.Text = "Dice:";
            // 
            // mnuEffectDiceAttack
            // 
            this.mnuEffectDiceAttack.Name = "mnuEffectDiceAttack";
            this.mnuEffectDiceAttack.Size = new System.Drawing.Size(144, 22);
            this.mnuEffectDiceAttack.Text = "Attack Dice";
            this.mnuEffectDiceAttack.Click += new System.EventHandler(this.mnuEffectDiceAttack_Click);
            // 
            // mnuEffectDiceDefense
            // 
            this.mnuEffectDiceDefense.Name = "mnuEffectDiceDefense";
            this.mnuEffectDiceDefense.Size = new System.Drawing.Size(144, 22);
            this.mnuEffectDiceDefense.Text = "Defense Dice";
            this.mnuEffectDiceDefense.Click += new System.EventHandler(this.mnuEffectDiceDefense_Click);
            // 
            // mnuEffectDiceGrapple
            // 
            this.mnuEffectDiceGrapple.Name = "mnuEffectDiceGrapple";
            this.mnuEffectDiceGrapple.Size = new System.Drawing.Size(144, 22);
            this.mnuEffectDiceGrapple.Text = "Grapple Dice";
            this.mnuEffectDiceGrapple.Click += new System.EventHandler(this.mnuEffectDiceGrapple_Click);
            // 
            // mnuEffectDiceSwallow
            // 
            this.mnuEffectDiceSwallow.Name = "mnuEffectDiceSwallow";
            this.mnuEffectDiceSwallow.Size = new System.Drawing.Size(144, 22);
            this.mnuEffectDiceSwallow.Text = "Swallow Dice";
            this.mnuEffectDiceSwallow.Click += new System.EventHandler(this.mnuEffectDiceSwallow_Click);
            // 
            // mnuEffectDiceStruggle
            // 
            this.mnuEffectDiceStruggle.Name = "mnuEffectDiceStruggle";
            this.mnuEffectDiceStruggle.Size = new System.Drawing.Size(144, 22);
            this.mnuEffectDiceStruggle.Text = "Struggle Dice";
            this.mnuEffectDiceStruggle.Click += new System.EventHandler(this.mnuEffectDiceStruggle_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(141, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.toolStripMenuItem1.Text = "Stats:";
            // 
            // mnuEffectStatHP
            // 
            this.mnuEffectStatHP.Name = "mnuEffectStatHP";
            this.mnuEffectStatHP.Size = new System.Drawing.Size(144, 22);
            this.mnuEffectStatHP.Text = "Health";
            this.mnuEffectStatHP.Click += new System.EventHandler(this.mnuEffectStatHP_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(141, 6);
            // 
            // miscToolStripMenuItem
            // 
            this.miscToolStripMenuItem.Enabled = false;
            this.miscToolStripMenuItem.Name = "miscToolStripMenuItem";
            this.miscToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.miscToolStripMenuItem.Text = "Misc:";
            // 
            // mnuEffectCustomText
            // 
            this.mnuEffectCustomText.Name = "mnuEffectCustomText";
            this.mnuEffectCustomText.Size = new System.Drawing.Size(144, 22);
            this.mnuEffectCustomText.Text = "Custom Text";
            this.mnuEffectCustomText.Click += new System.EventHandler(this.mnuEffectCustomText_Click);
            // 
            // cmbProcTarget
            // 
            this.cmbProcTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProcTarget.FormattingEnabled = true;
            this.cmbProcTarget.Items.AddRange(new object[] {
            "Wielder",
            "Opponent",
            "All allies",
            "All opponents"});
            this.cmbProcTarget.Location = new System.Drawing.Point(16, 88);
            this.cmbProcTarget.Name = "cmbProcTarget";
            this.cmbProcTarget.Size = new System.Drawing.Size(232, 21);
            this.cmbProcTarget.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(16, 72);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(79, 13);
            label5.TabIndex = 4;
            label5.Text = "Apply to whom:";
            // 
            // FormEquipEffectGroupEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 313);
            this.Controls.Add(grpBuffs);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.grpProcSettings);
            this.Controls.Add(this.cmbProcMode);
            this.Controls.Add(label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEquipEffectGroupEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Equip Effect Group";
            this.Load += new System.EventHandler(this.FormEquipEffectGroupEditor_Load);
            grpBuffs.ResumeLayout(false);
            this.grpProcSettings.ResumeLayout(false);
            this.grpProcSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudProcDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudProcChance)).EndInit();
            this.mnuBuffAdd.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbProcMode;
        private System.Windows.Forms.GroupBox grpProcSettings;
        private System.Windows.Forms.CheckBox chkIsVolatile;
        private System.Windows.Forms.NumericUpDown nudProcDuration;
        private System.Windows.Forms.NumericUpDown nudProcChance;
        private System.Windows.Forms.TextBox txtProcString;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdBuffEdit;
        private System.Windows.Forms.Button cmdBuffRemove;
        private System.Windows.Forms.ListView lsvBuffs;
        private System.Windows.Forms.Button cmdBuffAdd;
        private System.Windows.Forms.ContextMenuStrip mnuBuffAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuEffectDiceAttack;
        private System.Windows.Forms.ToolStripMenuItem mnuEffectDiceDefense;
        private System.Windows.Forms.ToolStripMenuItem mnuEffectDiceGrapple;
        private System.Windows.Forms.ToolStripMenuItem mnuEffectDiceSwallow;
        private System.Windows.Forms.ToolStripMenuItem mnuEffectDiceStruggle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuEffectStatHP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem miscToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuEffectCustomText;
        private System.Windows.Forms.ComboBox cmbProcTarget;
    }
}