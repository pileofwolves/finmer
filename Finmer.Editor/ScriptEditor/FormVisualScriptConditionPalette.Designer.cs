﻿
namespace Finmer.Editor
{
    partial class FormVisualScriptConditionPalette
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
            System.Windows.Forms.GroupBox groupBox3;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.GroupBox groupBox6;
            System.Windows.Forms.GroupBox groupBox4;
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.GroupBox groupBox5;
            this.cmdPlayerGetMaxHealth = new System.Windows.Forms.Button();
            this.cmdPlayerGetLevel = new System.Windows.Forms.Button();
            this.cmdPlayerGetStat = new System.Windows.Forms.Button();
            this.cmdPlayerHasItem = new System.Windows.Forms.Button();
            this.cmdPlayerGetHealth = new System.Windows.Forms.Button();
            this.cmdPlayerGetEquipment = new System.Windows.Forms.Button();
            this.cmdPlayerGetMoney = new System.Windows.Forms.Button();
            this.cmdVarStr = new System.Windows.Forms.Button();
            this.cmdVarNum = new System.Windows.Forms.Button();
            this.cmdVarFlag = new System.Windows.Forms.Button();
            this.cmdAdvLuaScript = new System.Windows.Forms.Button();
            this.cmdCombatActive = new System.Windows.Forms.Button();
            this.cmdCombatGrappling = new System.Windows.Forms.Button();
            this.cmdCombatSwallowed = new System.Windows.Forms.Button();
            this.cmdCombatDead = new System.Windows.Forms.Button();
            this.cmdSystemDevMode = new System.Windows.Forms.Button();
            this.cmdSystemDisposal = new System.Windows.Forms.Button();
            this.cmdSystemExplorer = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdTimeHourTotal = new System.Windows.Forms.Button();
            this.cmdTimeHour = new System.Windows.Forms.Button();
            this.cmdTimeDay = new System.Windows.Forms.Button();
            this.cmdTimeNight = new System.Windows.Forms.Button();
            groupBox3 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox6 = new System.Windows.Forms.GroupBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox5 = new System.Windows.Forms.GroupBox();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(this.cmdPlayerGetMaxHealth);
            groupBox3.Controls.Add(this.cmdPlayerGetLevel);
            groupBox3.Controls.Add(this.cmdPlayerGetStat);
            groupBox3.Controls.Add(this.cmdPlayerHasItem);
            groupBox3.Controls.Add(this.cmdPlayerGetHealth);
            groupBox3.Controls.Add(this.cmdPlayerGetEquipment);
            groupBox3.Controls.Add(this.cmdPlayerGetMoney);
            groupBox3.Location = new System.Drawing.Point(16, 16);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(208, 208);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Player";
            // 
            // cmdPlayerGetMaxHealth
            // 
            this.cmdPlayerGetMaxHealth.Location = new System.Drawing.Point(16, 72);
            this.cmdPlayerGetMaxHealth.Name = "cmdPlayerGetMaxHealth";
            this.cmdPlayerGetMaxHealth.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerGetMaxHealth.TabIndex = 2;
            this.cmdPlayerGetMaxHealth.Text = "Max Health";
            this.cmdPlayerGetMaxHealth.UseVisualStyleBackColor = true;
            this.cmdPlayerGetMaxHealth.Click += new System.EventHandler(this.cmdPlayerGetMaxHealth_Click);
            // 
            // cmdPlayerGetLevel
            // 
            this.cmdPlayerGetLevel.Location = new System.Drawing.Point(16, 168);
            this.cmdPlayerGetLevel.Name = "cmdPlayerGetLevel";
            this.cmdPlayerGetLevel.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerGetLevel.TabIndex = 6;
            this.cmdPlayerGetLevel.Text = "Level";
            this.cmdPlayerGetLevel.UseVisualStyleBackColor = true;
            this.cmdPlayerGetLevel.Click += new System.EventHandler(this.cmdPlayerGetLevel_Click);
            // 
            // cmdPlayerGetStat
            // 
            this.cmdPlayerGetStat.Location = new System.Drawing.Point(16, 24);
            this.cmdPlayerGetStat.Name = "cmdPlayerGetStat";
            this.cmdPlayerGetStat.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerGetStat.TabIndex = 0;
            this.cmdPlayerGetStat.Text = "Primary Stat";
            this.cmdPlayerGetStat.UseVisualStyleBackColor = true;
            this.cmdPlayerGetStat.Click += new System.EventHandler(this.cmdPlayerGetStat_Click);
            // 
            // cmdPlayerHasItem
            // 
            this.cmdPlayerHasItem.Location = new System.Drawing.Point(16, 144);
            this.cmdPlayerHasItem.Name = "cmdPlayerHasItem";
            this.cmdPlayerHasItem.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerHasItem.TabIndex = 5;
            this.cmdPlayerHasItem.Text = "Item";
            this.cmdPlayerHasItem.UseVisualStyleBackColor = true;
            this.cmdPlayerHasItem.Click += new System.EventHandler(this.cmdPlayerHasItem_Click);
            // 
            // cmdPlayerGetHealth
            // 
            this.cmdPlayerGetHealth.Location = new System.Drawing.Point(16, 48);
            this.cmdPlayerGetHealth.Name = "cmdPlayerGetHealth";
            this.cmdPlayerGetHealth.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerGetHealth.TabIndex = 1;
            this.cmdPlayerGetHealth.Text = "Health";
            this.cmdPlayerGetHealth.UseVisualStyleBackColor = true;
            this.cmdPlayerGetHealth.Click += new System.EventHandler(this.cmdPlayerGetHealth_Click);
            // 
            // cmdPlayerGetEquipment
            // 
            this.cmdPlayerGetEquipment.Location = new System.Drawing.Point(16, 120);
            this.cmdPlayerGetEquipment.Name = "cmdPlayerGetEquipment";
            this.cmdPlayerGetEquipment.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerGetEquipment.TabIndex = 4;
            this.cmdPlayerGetEquipment.Text = "Equipment";
            this.cmdPlayerGetEquipment.UseVisualStyleBackColor = true;
            this.cmdPlayerGetEquipment.Click += new System.EventHandler(this.cmdPlayerGetEquipment_Click);
            // 
            // cmdPlayerGetMoney
            // 
            this.cmdPlayerGetMoney.Location = new System.Drawing.Point(16, 96);
            this.cmdPlayerGetMoney.Name = "cmdPlayerGetMoney";
            this.cmdPlayerGetMoney.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerGetMoney.TabIndex = 3;
            this.cmdPlayerGetMoney.Text = "Money";
            this.cmdPlayerGetMoney.UseVisualStyleBackColor = true;
            this.cmdPlayerGetMoney.Click += new System.EventHandler(this.cmdPlayerGetMoney_Click);
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.cmdVarStr);
            groupBox2.Controls.Add(this.cmdVarNum);
            groupBox2.Controls.Add(this.cmdVarFlag);
            groupBox2.Location = new System.Drawing.Point(240, 16);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(208, 112);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Variables";
            // 
            // cmdVarStr
            // 
            this.cmdVarStr.Location = new System.Drawing.Point(16, 72);
            this.cmdVarStr.Name = "cmdVarStr";
            this.cmdVarStr.Size = new System.Drawing.Size(176, 24);
            this.cmdVarStr.TabIndex = 2;
            this.cmdVarStr.Text = "String Variable";
            this.cmdVarStr.UseVisualStyleBackColor = true;
            this.cmdVarStr.Click += new System.EventHandler(this.cmdVarStr_Click);
            // 
            // cmdVarNum
            // 
            this.cmdVarNum.Location = new System.Drawing.Point(16, 48);
            this.cmdVarNum.Name = "cmdVarNum";
            this.cmdVarNum.Size = new System.Drawing.Size(176, 24);
            this.cmdVarNum.TabIndex = 1;
            this.cmdVarNum.Text = "Number Variable";
            this.cmdVarNum.UseVisualStyleBackColor = true;
            this.cmdVarNum.Click += new System.EventHandler(this.cmdVarNum_Click);
            // 
            // cmdVarFlag
            // 
            this.cmdVarFlag.Location = new System.Drawing.Point(16, 24);
            this.cmdVarFlag.Name = "cmdVarFlag";
            this.cmdVarFlag.Size = new System.Drawing.Size(176, 24);
            this.cmdVarFlag.TabIndex = 0;
            this.cmdVarFlag.Text = "Flag Variable";
            this.cmdVarFlag.UseVisualStyleBackColor = true;
            this.cmdVarFlag.Click += new System.EventHandler(this.cmdVarFlag_Click);
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(this.cmdAdvLuaScript);
            groupBox6.Location = new System.Drawing.Point(16, 392);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new System.Drawing.Size(208, 64);
            groupBox6.TabIndex = 2;
            groupBox6.TabStop = false;
            groupBox6.Text = "Advanced";
            // 
            // cmdAdvLuaScript
            // 
            this.cmdAdvLuaScript.Location = new System.Drawing.Point(16, 24);
            this.cmdAdvLuaScript.Name = "cmdAdvLuaScript";
            this.cmdAdvLuaScript.Size = new System.Drawing.Size(176, 24);
            this.cmdAdvLuaScript.TabIndex = 0;
            this.cmdAdvLuaScript.Text = "Lua Script";
            this.cmdAdvLuaScript.UseVisualStyleBackColor = true;
            this.cmdAdvLuaScript.Click += new System.EventHandler(this.cmdAdvLuaScript_Click);
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(this.cmdCombatActive);
            groupBox4.Controls.Add(this.cmdCombatGrappling);
            groupBox4.Controls.Add(this.cmdCombatSwallowed);
            groupBox4.Controls.Add(this.cmdCombatDead);
            groupBox4.Location = new System.Drawing.Point(16, 240);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(208, 136);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            groupBox4.Text = "Combat";
            // 
            // cmdCombatActive
            // 
            this.cmdCombatActive.Location = new System.Drawing.Point(16, 24);
            this.cmdCombatActive.Name = "cmdCombatActive";
            this.cmdCombatActive.Size = new System.Drawing.Size(176, 24);
            this.cmdCombatActive.TabIndex = 0;
            this.cmdCombatActive.Text = "Is Combat Active";
            this.cmdCombatActive.UseVisualStyleBackColor = true;
            this.cmdCombatActive.Click += new System.EventHandler(this.cmdCombatActive_Click);
            // 
            // cmdCombatGrappling
            // 
            this.cmdCombatGrappling.Location = new System.Drawing.Point(16, 72);
            this.cmdCombatGrappling.Name = "cmdCombatGrappling";
            this.cmdCombatGrappling.Size = new System.Drawing.Size(176, 24);
            this.cmdCombatGrappling.TabIndex = 2;
            this.cmdCombatGrappling.Text = "Is Participant Grappling";
            this.cmdCombatGrappling.UseVisualStyleBackColor = true;
            this.cmdCombatGrappling.Click += new System.EventHandler(this.cmdCombatGrappling_Click);
            // 
            // cmdCombatSwallowed
            // 
            this.cmdCombatSwallowed.Location = new System.Drawing.Point(16, 96);
            this.cmdCombatSwallowed.Name = "cmdCombatSwallowed";
            this.cmdCombatSwallowed.Size = new System.Drawing.Size(176, 24);
            this.cmdCombatSwallowed.TabIndex = 3;
            this.cmdCombatSwallowed.Text = "Is Participant Swallowed";
            this.cmdCombatSwallowed.UseVisualStyleBackColor = true;
            this.cmdCombatSwallowed.Click += new System.EventHandler(this.cmdCombatSwallowed_Click);
            // 
            // cmdCombatDead
            // 
            this.cmdCombatDead.Location = new System.Drawing.Point(16, 48);
            this.cmdCombatDead.Name = "cmdCombatDead";
            this.cmdCombatDead.Size = new System.Drawing.Size(176, 24);
            this.cmdCombatDead.TabIndex = 1;
            this.cmdCombatDead.Text = "Is Participant Down";
            this.cmdCombatDead.UseVisualStyleBackColor = true;
            this.cmdCombatDead.Click += new System.EventHandler(this.cmdCombatDead_Click);
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.cmdSystemDevMode);
            groupBox1.Controls.Add(this.cmdSystemDisposal);
            groupBox1.Controls.Add(this.cmdSystemExplorer);
            groupBox1.Location = new System.Drawing.Point(240, 296);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(208, 112);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "System";
            // 
            // cmdSystemDevMode
            // 
            this.cmdSystemDevMode.Location = new System.Drawing.Point(16, 72);
            this.cmdSystemDevMode.Name = "cmdSystemDevMode";
            this.cmdSystemDevMode.Size = new System.Drawing.Size(176, 24);
            this.cmdSystemDevMode.TabIndex = 2;
            this.cmdSystemDevMode.Text = "Is Dev Mode Enabled";
            this.cmdSystemDevMode.UseVisualStyleBackColor = true;
            this.cmdSystemDevMode.Click += new System.EventHandler(this.cmdSystemDevMode_Click);
            // 
            // cmdSystemDisposal
            // 
            this.cmdSystemDisposal.Location = new System.Drawing.Point(16, 48);
            this.cmdSystemDisposal.Name = "cmdSystemDisposal";
            this.cmdSystemDisposal.Size = new System.Drawing.Size(176, 24);
            this.cmdSystemDisposal.TabIndex = 1;
            this.cmdSystemDisposal.Text = "Is Explicit Disposal Enabled";
            this.cmdSystemDisposal.UseVisualStyleBackColor = true;
            this.cmdSystemDisposal.Click += new System.EventHandler(this.cmdSystemDisposal_Click);
            // 
            // cmdSystemExplorer
            // 
            this.cmdSystemExplorer.Location = new System.Drawing.Point(16, 24);
            this.cmdSystemExplorer.Name = "cmdSystemExplorer";
            this.cmdSystemExplorer.Size = new System.Drawing.Size(176, 24);
            this.cmdSystemExplorer.TabIndex = 0;
            this.cmdSystemExplorer.Text = "Is Explorer Mode Enabled";
            this.cmdSystemExplorer.UseVisualStyleBackColor = true;
            this.cmdSystemExplorer.Click += new System.EventHandler(this.cmdSystemExplorer_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Image = global::Finmer.Editor.Properties.Resources.cross;
            this.cmdCancel.Location = new System.Drawing.Point(320, 440);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(128, 32);
            this.cmdCancel.TabIndex = 6;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(this.cmdTimeNight);
            groupBox5.Controls.Add(this.cmdTimeHourTotal);
            groupBox5.Controls.Add(this.cmdTimeHour);
            groupBox5.Controls.Add(this.cmdTimeDay);
            groupBox5.Location = new System.Drawing.Point(240, 144);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(208, 136);
            groupBox5.TabIndex = 4;
            groupBox5.TabStop = false;
            groupBox5.Text = "Time";
            // 
            // cmdTimeHourTotal
            // 
            this.cmdTimeHourTotal.Location = new System.Drawing.Point(16, 72);
            this.cmdTimeHourTotal.Name = "cmdTimeHourTotal";
            this.cmdTimeHourTotal.Size = new System.Drawing.Size(176, 24);
            this.cmdTimeHourTotal.TabIndex = 2;
            this.cmdTimeHourTotal.Text = "Total Hours Passed";
            this.cmdTimeHourTotal.UseVisualStyleBackColor = true;
            this.cmdTimeHourTotal.Click += new System.EventHandler(this.cmdTimeHourTotal_Click);
            // 
            // cmdTimeHour
            // 
            this.cmdTimeHour.Location = new System.Drawing.Point(16, 48);
            this.cmdTimeHour.Name = "cmdTimeHour";
            this.cmdTimeHour.Size = new System.Drawing.Size(176, 24);
            this.cmdTimeHour.TabIndex = 1;
            this.cmdTimeHour.Text = "Current Hour (0-23)";
            this.cmdTimeHour.UseVisualStyleBackColor = true;
            this.cmdTimeHour.Click += new System.EventHandler(this.cmdTimeHour_Click);
            // 
            // cmdTimeDay
            // 
            this.cmdTimeDay.Location = new System.Drawing.Point(16, 24);
            this.cmdTimeDay.Name = "cmdTimeDay";
            this.cmdTimeDay.Size = new System.Drawing.Size(176, 24);
            this.cmdTimeDay.TabIndex = 0;
            this.cmdTimeDay.Text = "Current Day";
            this.cmdTimeDay.UseVisualStyleBackColor = true;
            this.cmdTimeDay.Click += new System.EventHandler(this.cmdTimeDay_Click);
            // 
            // cmdTimeNight
            // 
            this.cmdTimeNight.Location = new System.Drawing.Point(16, 96);
            this.cmdTimeNight.Name = "cmdTimeNight";
            this.cmdTimeNight.Size = new System.Drawing.Size(176, 24);
            this.cmdTimeNight.TabIndex = 3;
            this.cmdTimeNight.Text = "Is Night";
            this.cmdTimeNight.UseVisualStyleBackColor = true;
            this.cmdTimeNight.Click += new System.EventHandler(this.cmdTimeNight_Click);
            // 
            // FormVisualScriptConditionPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(465, 489);
            this.Controls.Add(groupBox5);
            this.Controls.Add(groupBox1);
            this.Controls.Add(groupBox4);
            this.Controls.Add(groupBox6);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox3);
            this.Controls.Add(this.cmdCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormVisualScriptConditionPalette";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Test Condition";
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdPlayerGetMoney;
        private System.Windows.Forms.Button cmdPlayerGetEquipment;
        private System.Windows.Forms.Button cmdPlayerGetHealth;
        private System.Windows.Forms.Button cmdPlayerHasItem;
        private System.Windows.Forms.Button cmdPlayerGetStat;
        private System.Windows.Forms.Button cmdPlayerGetLevel;
        private System.Windows.Forms.Button cmdPlayerGetMaxHealth;
        private System.Windows.Forms.Button cmdVarStr;
        private System.Windows.Forms.Button cmdVarNum;
        private System.Windows.Forms.Button cmdVarFlag;
        private System.Windows.Forms.Button cmdAdvLuaScript;
        private System.Windows.Forms.Button cmdCombatActive;
        private System.Windows.Forms.Button cmdCombatGrappling;
        private System.Windows.Forms.Button cmdCombatSwallowed;
        private System.Windows.Forms.Button cmdCombatDead;
        private System.Windows.Forms.Button cmdSystemDevMode;
        private System.Windows.Forms.Button cmdSystemDisposal;
        private System.Windows.Forms.Button cmdSystemExplorer;
        private System.Windows.Forms.Button cmdTimeNight;
        private System.Windows.Forms.Button cmdTimeHourTotal;
        private System.Windows.Forms.Button cmdTimeHour;
        private System.Windows.Forms.Button cmdTimeDay;
    }
}