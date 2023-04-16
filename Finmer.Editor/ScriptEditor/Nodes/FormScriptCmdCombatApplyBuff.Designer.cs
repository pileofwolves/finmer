
namespace Finmer.Editor
{
    partial class FormScriptCmdCombatApplyBuff
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
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label5;
            this.lblTargetNPC = new System.Windows.Forms.Label();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.txtTargetNPC = new System.Windows.Forms.TextBox();
            this.optTargetPlayer = new System.Windows.Forms.RadioButton();
            this.optTargetNPC = new System.Windows.Forms.RadioButton();
            this.cmdBuffDelete = new System.Windows.Forms.Button();
            this.cmdBuffEdit = new System.Windows.Forms.Button();
            this.lblBuffText = new System.Windows.Forms.Label();
            this.nudDuration = new System.Windows.Forms.NumericUpDown();
            this.mnuBuffAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEffectDiceAttack = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEffectDiceDefense = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEffectDiceGrapple = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEffectDiceSwallow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEffectDiceStruggle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEffectStatHPOverTime = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEffectStun = new System.Windows.Forms.ToolStripMenuItem();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).BeginInit();
            this.mnuBuffAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(16, 16);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(48, 13);
            label3.TabIndex = 0;
            label3.Text = "Apply to:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 120);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(79, 13);
            label2.TabIndex = 5;
            label2.Text = "Effect to Apply:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(16, 176);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(91, 13);
            label5.TabIndex = 9;
            label5.Text = "Duration (rounds):";
            // 
            // lblTargetNPC
            // 
            this.lblTargetNPC.AutoSize = true;
            this.lblTargetNPC.Location = new System.Drawing.Point(16, 64);
            this.lblTargetNPC.Name = "lblTargetNPC";
            this.lblTargetNPC.Size = new System.Drawing.Size(108, 13);
            this.lblTargetNPC.TabIndex = 3;
            this.lblTargetNPC.Text = "Target Participant ID:";
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Image = global::Finmer.Editor.Properties.Resources.tick;
            this.cmdAccept.Location = new System.Drawing.Point(41, 256);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(119, 32);
            this.cmdAccept.TabIndex = 11;
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
            this.cmdCancel.Location = new System.Drawing.Point(169, 255);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(119, 32);
            this.cmdCancel.TabIndex = 12;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // txtTargetNPC
            // 
            this.txtTargetNPC.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTargetNPC.Location = new System.Drawing.Point(16, 80);
            this.txtTargetNPC.MaxLength = 100;
            this.txtTargetNPC.Name = "txtTargetNPC";
            this.txtTargetNPC.Size = new System.Drawing.Size(272, 22);
            this.txtTargetNPC.TabIndex = 4;
            // 
            // optTargetPlayer
            // 
            this.optTargetPlayer.AutoSize = true;
            this.optTargetPlayer.Location = new System.Drawing.Point(24, 32);
            this.optTargetPlayer.Name = "optTargetPlayer";
            this.optTargetPlayer.Size = new System.Drawing.Size(54, 17);
            this.optTargetPlayer.TabIndex = 1;
            this.optTargetPlayer.TabStop = true;
            this.optTargetPlayer.Text = "Player";
            this.optTargetPlayer.UseVisualStyleBackColor = true;
            this.optTargetPlayer.CheckedChanged += new System.EventHandler(this.optTargetPlayer_CheckedChanged);
            // 
            // optTargetNPC
            // 
            this.optTargetNPC.AutoSize = true;
            this.optTargetNPC.Location = new System.Drawing.Point(104, 32);
            this.optTargetNPC.Name = "optTargetNPC";
            this.optTargetNPC.Size = new System.Drawing.Size(100, 17);
            this.optTargetNPC.TabIndex = 2;
            this.optTargetNPC.TabStop = true;
            this.optTargetNPC.Text = "NPC Participant";
            this.optTargetNPC.UseVisualStyleBackColor = true;
            // 
            // cmdBuffDelete
            // 
            this.cmdBuffDelete.Image = global::Finmer.Editor.Properties.Resources.cross_script;
            this.cmdBuffDelete.Location = new System.Drawing.Point(16, 136);
            this.cmdBuffDelete.Name = "cmdBuffDelete";
            this.cmdBuffDelete.Size = new System.Drawing.Size(24, 24);
            this.cmdBuffDelete.TabIndex = 6;
            this.cmdBuffDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdBuffDelete.UseVisualStyleBackColor = true;
            this.cmdBuffDelete.Click += new System.EventHandler(this.cmdBuffDelete_Click);
            // 
            // cmdBuffEdit
            // 
            this.cmdBuffEdit.Image = global::Finmer.Editor.Properties.Resources.pencil;
            this.cmdBuffEdit.Location = new System.Drawing.Point(40, 136);
            this.cmdBuffEdit.Name = "cmdBuffEdit";
            this.cmdBuffEdit.Size = new System.Drawing.Size(24, 24);
            this.cmdBuffEdit.TabIndex = 7;
            this.cmdBuffEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdBuffEdit.UseVisualStyleBackColor = true;
            this.cmdBuffEdit.Click += new System.EventHandler(this.cmdBuffEdit_Click);
            // 
            // lblBuffText
            // 
            this.lblBuffText.Location = new System.Drawing.Point(64, 136);
            this.lblBuffText.Name = "lblBuffText";
            this.lblBuffText.Size = new System.Drawing.Size(224, 24);
            this.lblBuffText.TabIndex = 8;
            this.lblBuffText.Text = "Buff title";
            this.lblBuffText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudDuration
            // 
            this.nudDuration.Location = new System.Drawing.Point(16, 192);
            this.nudDuration.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDuration.Name = "nudDuration";
            this.nudDuration.Size = new System.Drawing.Size(96, 20);
            this.nudDuration.TabIndex = 10;
            this.nudDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
            this.mnuEffectStatHPOverTime,
            this.toolStripSeparator2,
            this.toolStripMenuItem3,
            this.mnuEffectStun});
            this.mnuBuffAdd.Name = "mnuEquipEffectAdd";
            this.mnuBuffAdd.Size = new System.Drawing.Size(165, 236);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Enabled = false;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem2.Text = "Dice:";
            // 
            // mnuEffectDiceAttack
            // 
            this.mnuEffectDiceAttack.Name = "mnuEffectDiceAttack";
            this.mnuEffectDiceAttack.Size = new System.Drawing.Size(164, 22);
            this.mnuEffectDiceAttack.Text = "Attack Dice";
            this.mnuEffectDiceAttack.Click += new System.EventHandler(this.mnuEffectDiceAttack_Click);
            // 
            // mnuEffectDiceDefense
            // 
            this.mnuEffectDiceDefense.Name = "mnuEffectDiceDefense";
            this.mnuEffectDiceDefense.Size = new System.Drawing.Size(164, 22);
            this.mnuEffectDiceDefense.Text = "Defense Dice";
            this.mnuEffectDiceDefense.Click += new System.EventHandler(this.mnuEffectDiceDefense_Click);
            // 
            // mnuEffectDiceGrapple
            // 
            this.mnuEffectDiceGrapple.Name = "mnuEffectDiceGrapple";
            this.mnuEffectDiceGrapple.Size = new System.Drawing.Size(164, 22);
            this.mnuEffectDiceGrapple.Text = "Grapple Dice";
            this.mnuEffectDiceGrapple.Click += new System.EventHandler(this.mnuEffectDiceGrapple_Click);
            // 
            // mnuEffectDiceSwallow
            // 
            this.mnuEffectDiceSwallow.Name = "mnuEffectDiceSwallow";
            this.mnuEffectDiceSwallow.Size = new System.Drawing.Size(164, 22);
            this.mnuEffectDiceSwallow.Text = "Swallow Dice";
            this.mnuEffectDiceSwallow.Click += new System.EventHandler(this.mnuEffectDiceSwallow_Click);
            // 
            // mnuEffectDiceStruggle
            // 
            this.mnuEffectDiceStruggle.Name = "mnuEffectDiceStruggle";
            this.mnuEffectDiceStruggle.Size = new System.Drawing.Size(164, 22);
            this.mnuEffectDiceStruggle.Text = "Struggle Dice";
            this.mnuEffectDiceStruggle.Click += new System.EventHandler(this.mnuEffectDiceStruggle_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem1.Text = "Stats:";
            // 
            // mnuEffectStatHPOverTime
            // 
            this.mnuEffectStatHPOverTime.Name = "mnuEffectStatHPOverTime";
            this.mnuEffectStatHPOverTime.Size = new System.Drawing.Size(164, 22);
            this.mnuEffectStatHPOverTime.Text = "Health over Time";
            this.mnuEffectStatHPOverTime.Click += new System.EventHandler(this.mnuEffectStatHPOverTime_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Enabled = false;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem3.Text = "Misc:";
            // 
            // mnuEffectStun
            // 
            this.mnuEffectStun.Name = "mnuEffectStun";
            this.mnuEffectStun.Size = new System.Drawing.Size(164, 22);
            this.mnuEffectStun.Text = "Stun (Skip Turn)";
            this.mnuEffectStun.Click += new System.EventHandler(this.mnuEffectStun_Click);
            // 
            // FormScriptCmdCombatApplyBuff
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(305, 304);
            this.Controls.Add(this.nudDuration);
            this.Controls.Add(label5);
            this.Controls.Add(this.lblBuffText);
            this.Controls.Add(this.cmdBuffEdit);
            this.Controls.Add(this.cmdBuffDelete);
            this.Controls.Add(label2);
            this.Controls.Add(this.optTargetNPC);
            this.Controls.Add(this.optTargetPlayer);
            this.Controls.Add(label3);
            this.Controls.Add(this.txtTargetNPC);
            this.Controls.Add(this.lblTargetNPC);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormScriptCmdCombatApplyBuff";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Apply Buff";
            this.Load += new System.EventHandler(this.FormScriptCmdCombatApplyBuff_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).EndInit();
            this.mnuBuffAdd.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.TextBox txtTargetNPC;
        private System.Windows.Forms.RadioButton optTargetPlayer;
        private System.Windows.Forms.RadioButton optTargetNPC;
        private System.Windows.Forms.Button cmdBuffDelete;
        private System.Windows.Forms.Button cmdBuffEdit;
        private System.Windows.Forms.NumericUpDown nudDuration;
        private System.Windows.Forms.Label lblBuffText;
        private System.Windows.Forms.ContextMenuStrip mnuBuffAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuEffectDiceAttack;
        private System.Windows.Forms.ToolStripMenuItem mnuEffectDiceDefense;
        private System.Windows.Forms.ToolStripMenuItem mnuEffectDiceGrapple;
        private System.Windows.Forms.ToolStripMenuItem mnuEffectDiceSwallow;
        private System.Windows.Forms.ToolStripMenuItem mnuEffectDiceStruggle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuEffectStatHPOverTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuEffectStun;
        private System.Windows.Forms.Label lblTargetNPC;
    }
}