
namespace Finmer.Editor
{
    partial class FormVisualScriptValuePalette
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
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.GroupBox groupBox3;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.GroupBox groupBox6;
            System.Windows.Forms.GroupBox groupBox4;
            this.cmdLogicCompare = new System.Windows.Forms.Button();
            this.cmdLogicOr = new System.Windows.Forms.Button();
            this.cmdLogicAnd = new System.Windows.Forms.Button();
            this.cmdLogicNot = new System.Windows.Forms.Button();
            this.cmdPlayerGetMaxHealth = new System.Windows.Forms.Button();
            this.cmdPlayerGetSpecies = new System.Windows.Forms.Button();
            this.cmdPlayerGetName = new System.Windows.Forms.Button();
            this.cmdPlayerGetLevel = new System.Windows.Forms.Button();
            this.cmdPlayerGetStat = new System.Windows.Forms.Button();
            this.cmdPlayerHasItem = new System.Windows.Forms.Button();
            this.cmdPlayerGetHealth = new System.Windows.Forms.Button();
            this.cmdPlayerGetEquipment = new System.Windows.Forms.Button();
            this.cmdPlayerGetMoney = new System.Windows.Forms.Button();
            this.cmdPlayerGetSize = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.cmdAdvLuaScript = new System.Windows.Forms.Button();
            this.cmdSceneSetScene = new System.Windows.Forms.Button();
            this.cmdSceneCombatStop = new System.Windows.Forms.Button();
            this.cmdSceneShop = new System.Windows.Forms.Button();
            this.cmdSceneCombat = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox3 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox6 = new System.Windows.Forms.GroupBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.cmdLogicCompare);
            groupBox1.Controls.Add(this.cmdLogicOr);
            groupBox1.Controls.Add(this.cmdLogicAnd);
            groupBox1.Controls.Add(this.cmdLogicNot);
            groupBox1.Location = new System.Drawing.Point(16, 16);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(208, 136);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Logic";
            // 
            // cmdLogicCompare
            // 
            this.cmdLogicCompare.Location = new System.Drawing.Point(16, 24);
            this.cmdLogicCompare.Name = "cmdLogicCompare";
            this.cmdLogicCompare.Size = new System.Drawing.Size(176, 24);
            this.cmdLogicCompare.TabIndex = 4;
            this.cmdLogicCompare.Text = "Comparison";
            this.cmdLogicCompare.UseVisualStyleBackColor = true;
            this.cmdLogicCompare.Click += new System.EventHandler(this.cmdLogicCompare_Click);
            // 
            // cmdLogicOr
            // 
            this.cmdLogicOr.Enabled = false;
            this.cmdLogicOr.Location = new System.Drawing.Point(16, 96);
            this.cmdLogicOr.Name = "cmdLogicOr";
            this.cmdLogicOr.Size = new System.Drawing.Size(176, 24);
            this.cmdLogicOr.TabIndex = 3;
            this.cmdLogicOr.Text = "Or";
            this.cmdLogicOr.UseVisualStyleBackColor = true;
            this.cmdLogicOr.Click += new System.EventHandler(this.cmdLogicOr_Click);
            // 
            // cmdLogicAnd
            // 
            this.cmdLogicAnd.Enabled = false;
            this.cmdLogicAnd.Location = new System.Drawing.Point(16, 72);
            this.cmdLogicAnd.Name = "cmdLogicAnd";
            this.cmdLogicAnd.Size = new System.Drawing.Size(176, 24);
            this.cmdLogicAnd.TabIndex = 2;
            this.cmdLogicAnd.Text = "And";
            this.cmdLogicAnd.UseVisualStyleBackColor = true;
            this.cmdLogicAnd.Click += new System.EventHandler(this.cmdLogicAnd_Click);
            // 
            // cmdLogicNot
            // 
            this.cmdLogicNot.Enabled = false;
            this.cmdLogicNot.Location = new System.Drawing.Point(16, 48);
            this.cmdLogicNot.Name = "cmdLogicNot";
            this.cmdLogicNot.Size = new System.Drawing.Size(176, 24);
            this.cmdLogicNot.TabIndex = 1;
            this.cmdLogicNot.Text = "Not";
            this.cmdLogicNot.UseVisualStyleBackColor = true;
            this.cmdLogicNot.Click += new System.EventHandler(this.cmdLogicNot_Click);
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(this.cmdPlayerGetMaxHealth);
            groupBox3.Controls.Add(this.cmdPlayerGetSpecies);
            groupBox3.Controls.Add(this.cmdPlayerGetName);
            groupBox3.Controls.Add(this.cmdPlayerGetLevel);
            groupBox3.Controls.Add(this.cmdPlayerGetStat);
            groupBox3.Controls.Add(this.cmdPlayerHasItem);
            groupBox3.Controls.Add(this.cmdPlayerGetHealth);
            groupBox3.Controls.Add(this.cmdPlayerGetEquipment);
            groupBox3.Controls.Add(this.cmdPlayerGetMoney);
            groupBox3.Controls.Add(this.cmdPlayerGetSize);
            groupBox3.Location = new System.Drawing.Point(16, 168);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(208, 280);
            groupBox3.TabIndex = 8;
            groupBox3.TabStop = false;
            groupBox3.Text = "Player";
            // 
            // cmdPlayerGetMaxHealth
            // 
            this.cmdPlayerGetMaxHealth.Location = new System.Drawing.Point(16, 144);
            this.cmdPlayerGetMaxHealth.Name = "cmdPlayerGetMaxHealth";
            this.cmdPlayerGetMaxHealth.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerGetMaxHealth.TabIndex = 12;
            this.cmdPlayerGetMaxHealth.Text = "Get Max Health";
            this.cmdPlayerGetMaxHealth.UseVisualStyleBackColor = true;
            this.cmdPlayerGetMaxHealth.Click += new System.EventHandler(this.cmdPlayerGetMaxHealth_Click);
            // 
            // cmdPlayerGetSpecies
            // 
            this.cmdPlayerGetSpecies.Location = new System.Drawing.Point(16, 48);
            this.cmdPlayerGetSpecies.Name = "cmdPlayerGetSpecies";
            this.cmdPlayerGetSpecies.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerGetSpecies.TabIndex = 11;
            this.cmdPlayerGetSpecies.Text = "Get Species";
            this.cmdPlayerGetSpecies.UseVisualStyleBackColor = true;
            this.cmdPlayerGetSpecies.Click += new System.EventHandler(this.cmdPlayerGetSpecies_Click);
            // 
            // cmdPlayerGetName
            // 
            this.cmdPlayerGetName.Location = new System.Drawing.Point(16, 24);
            this.cmdPlayerGetName.Name = "cmdPlayerGetName";
            this.cmdPlayerGetName.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerGetName.TabIndex = 10;
            this.cmdPlayerGetName.Text = "Get Name";
            this.cmdPlayerGetName.UseVisualStyleBackColor = true;
            this.cmdPlayerGetName.Click += new System.EventHandler(this.cmdPlayerGetName_Click);
            // 
            // cmdPlayerGetLevel
            // 
            this.cmdPlayerGetLevel.Location = new System.Drawing.Point(16, 240);
            this.cmdPlayerGetLevel.Name = "cmdPlayerGetLevel";
            this.cmdPlayerGetLevel.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerGetLevel.TabIndex = 7;
            this.cmdPlayerGetLevel.Text = "Get Level";
            this.cmdPlayerGetLevel.UseVisualStyleBackColor = true;
            this.cmdPlayerGetLevel.Click += new System.EventHandler(this.cmdPlayerGetLevel_Click);
            // 
            // cmdPlayerGetStat
            // 
            this.cmdPlayerGetStat.Location = new System.Drawing.Point(16, 72);
            this.cmdPlayerGetStat.Name = "cmdPlayerGetStat";
            this.cmdPlayerGetStat.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerGetStat.TabIndex = 6;
            this.cmdPlayerGetStat.Text = "Get Primary Stat";
            this.cmdPlayerGetStat.UseVisualStyleBackColor = true;
            this.cmdPlayerGetStat.Click += new System.EventHandler(this.cmdPlayerGetStat_Click);
            // 
            // cmdPlayerHasItem
            // 
            this.cmdPlayerHasItem.Location = new System.Drawing.Point(16, 216);
            this.cmdPlayerHasItem.Name = "cmdPlayerHasItem";
            this.cmdPlayerHasItem.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerHasItem.TabIndex = 5;
            this.cmdPlayerHasItem.Text = "Has Item";
            this.cmdPlayerHasItem.UseVisualStyleBackColor = true;
            this.cmdPlayerHasItem.Click += new System.EventHandler(this.cmdPlayerHasItem_Click);
            // 
            // cmdPlayerGetHealth
            // 
            this.cmdPlayerGetHealth.Location = new System.Drawing.Point(16, 120);
            this.cmdPlayerGetHealth.Name = "cmdPlayerGetHealth";
            this.cmdPlayerGetHealth.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerGetHealth.TabIndex = 4;
            this.cmdPlayerGetHealth.Text = "Get Health";
            this.cmdPlayerGetHealth.UseVisualStyleBackColor = true;
            this.cmdPlayerGetHealth.Click += new System.EventHandler(this.cmdPlayerGetHealth_Click);
            // 
            // cmdPlayerGetEquipment
            // 
            this.cmdPlayerGetEquipment.Location = new System.Drawing.Point(16, 192);
            this.cmdPlayerGetEquipment.Name = "cmdPlayerGetEquipment";
            this.cmdPlayerGetEquipment.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerGetEquipment.TabIndex = 3;
            this.cmdPlayerGetEquipment.Text = "Get Equipment";
            this.cmdPlayerGetEquipment.UseVisualStyleBackColor = true;
            this.cmdPlayerGetEquipment.Click += new System.EventHandler(this.cmdPlayerGetEquipment_Click);
            // 
            // cmdPlayerGetMoney
            // 
            this.cmdPlayerGetMoney.Location = new System.Drawing.Point(16, 168);
            this.cmdPlayerGetMoney.Name = "cmdPlayerGetMoney";
            this.cmdPlayerGetMoney.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerGetMoney.TabIndex = 2;
            this.cmdPlayerGetMoney.Text = "Get Money";
            this.cmdPlayerGetMoney.UseVisualStyleBackColor = true;
            this.cmdPlayerGetMoney.Click += new System.EventHandler(this.cmdPlayerGetMoney_Click);
            // 
            // cmdPlayerGetSize
            // 
            this.cmdPlayerGetSize.Location = new System.Drawing.Point(16, 96);
            this.cmdPlayerGetSize.Name = "cmdPlayerGetSize";
            this.cmdPlayerGetSize.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerGetSize.TabIndex = 1;
            this.cmdPlayerGetSize.Text = "Get Size";
            this.cmdPlayerGetSize.UseVisualStyleBackColor = true;
            this.cmdPlayerGetSize.Click += new System.EventHandler(this.cmdPlayerGetSize_Click);
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.button1);
            groupBox2.Controls.Add(this.button2);
            groupBox2.Controls.Add(this.button3);
            groupBox2.Controls.Add(this.button4);
            groupBox2.Location = new System.Drawing.Point(240, 16);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(208, 136);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Data Storage";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(176, 24);
            this.button1.TabIndex = 4;
            this.button1.Text = "Get Local Variable";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(16, 96);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(176, 24);
            this.button2.TabIndex = 3;
            this.button2.Text = "Get Persistent String";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(16, 72);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(176, 24);
            this.button3.TabIndex = 2;
            this.button3.Text = "Get Persistent Number";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(16, 48);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(176, 24);
            this.button4.TabIndex = 1;
            this.button4.Text = "Get Persistent Flag";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(this.cmdAdvLuaScript);
            groupBox6.Enabled = false;
            groupBox6.Location = new System.Drawing.Point(240, 320);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new System.Drawing.Size(208, 64);
            groupBox6.TabIndex = 10;
            groupBox6.TabStop = false;
            groupBox6.Text = "Advanced";
            // 
            // cmdAdvLuaScript
            // 
            this.cmdAdvLuaScript.Location = new System.Drawing.Point(16, 24);
            this.cmdAdvLuaScript.Name = "cmdAdvLuaScript";
            this.cmdAdvLuaScript.Size = new System.Drawing.Size(176, 24);
            this.cmdAdvLuaScript.TabIndex = 6;
            this.cmdAdvLuaScript.Text = "Lua Script";
            this.cmdAdvLuaScript.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(this.cmdSceneSetScene);
            groupBox4.Controls.Add(this.cmdSceneCombatStop);
            groupBox4.Controls.Add(this.cmdSceneShop);
            groupBox4.Controls.Add(this.cmdSceneCombat);
            groupBox4.Enabled = false;
            groupBox4.Location = new System.Drawing.Point(240, 168);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(208, 136);
            groupBox4.TabIndex = 11;
            groupBox4.TabStop = false;
            groupBox4.Text = "Active Combat";
            // 
            // cmdSceneSetScene
            // 
            this.cmdSceneSetScene.Location = new System.Drawing.Point(16, 24);
            this.cmdSceneSetScene.Name = "cmdSceneSetScene";
            this.cmdSceneSetScene.Size = new System.Drawing.Size(176, 24);
            this.cmdSceneSetScene.TabIndex = 6;
            this.cmdSceneSetScene.Text = "Is Combat Active";
            this.cmdSceneSetScene.UseVisualStyleBackColor = true;
            // 
            // cmdSceneCombatStop
            // 
            this.cmdSceneCombatStop.Location = new System.Drawing.Point(16, 72);
            this.cmdSceneCombatStop.Name = "cmdSceneCombatStop";
            this.cmdSceneCombatStop.Size = new System.Drawing.Size(176, 24);
            this.cmdSceneCombatStop.TabIndex = 4;
            this.cmdSceneCombatStop.Text = "Is Participant Grappling";
            this.cmdSceneCombatStop.UseVisualStyleBackColor = true;
            // 
            // cmdSceneShop
            // 
            this.cmdSceneShop.Location = new System.Drawing.Point(16, 96);
            this.cmdSceneShop.Name = "cmdSceneShop";
            this.cmdSceneShop.Size = new System.Drawing.Size(176, 24);
            this.cmdSceneShop.TabIndex = 2;
            this.cmdSceneShop.Text = "Is Participant Swallowed";
            this.cmdSceneShop.UseVisualStyleBackColor = true;
            // 
            // cmdSceneCombat
            // 
            this.cmdSceneCombat.Location = new System.Drawing.Point(16, 48);
            this.cmdSceneCombat.Name = "cmdSceneCombat";
            this.cmdSceneCombat.Size = new System.Drawing.Size(176, 24);
            this.cmdSceneCombat.TabIndex = 1;
            this.cmdSceneCombat.Text = "Is Participant Down";
            this.cmdSceneCombat.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Image = global::Finmer.Editor.Properties.Resources.cross;
            this.cmdCancel.Location = new System.Drawing.Point(312, 464);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(128, 32);
            this.cmdCancel.TabIndex = 0;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // FormVisualScriptValuePalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(464, 520);
            this.Controls.Add(groupBox4);
            this.Controls.Add(groupBox6);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox3);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormVisualScriptValuePalette";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Value";
            groupBox1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cmdLogicNot;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdLogicCompare;
        private System.Windows.Forms.Button cmdLogicOr;
        private System.Windows.Forms.Button cmdLogicAnd;
        private System.Windows.Forms.Button cmdPlayerGetSize;
        private System.Windows.Forms.Button cmdPlayerGetMoney;
        private System.Windows.Forms.Button cmdPlayerGetEquipment;
        private System.Windows.Forms.Button cmdPlayerGetHealth;
        private System.Windows.Forms.Button cmdPlayerHasItem;
        private System.Windows.Forms.Button cmdPlayerGetStat;
        private System.Windows.Forms.Button cmdPlayerGetLevel;
        private System.Windows.Forms.Button cmdPlayerGetName;
        private System.Windows.Forms.Button cmdPlayerGetSpecies;
        private System.Windows.Forms.Button cmdPlayerGetMaxHealth;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button cmdAdvLuaScript;
        private System.Windows.Forms.Button cmdSceneSetScene;
        private System.Windows.Forms.Button cmdSceneCombatStop;
        private System.Windows.Forms.Button cmdSceneShop;
        private System.Windows.Forms.Button cmdSceneCombat;
    }
}