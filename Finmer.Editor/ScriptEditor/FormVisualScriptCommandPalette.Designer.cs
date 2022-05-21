
namespace Finmer.Editor
{
    partial class FormVisualScriptCommandPalette
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
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.GroupBox groupBox3;
            System.Windows.Forms.GroupBox groupBox4;
            System.Windows.Forms.GroupBox groupBox5;
            System.Windows.Forms.GroupBox groupBox6;
            System.Windows.Forms.GroupBox groupBox7;
            System.Windows.Forms.GroupBox groupBox8;
            System.Windows.Forms.GroupBox groupBox9;
            this.cmdFlowSleep = new System.Windows.Forms.Button();
            this.cmdFlowComment = new System.Windows.Forms.Button();
            this.cmdFlowExit = new System.Windows.Forms.Button();
            this.cmdFlowLoopBreak = new System.Windows.Forms.Button();
            this.cmdFlowLoop = new System.Windows.Forms.Button();
            this.cmdFlowIf = new System.Windows.Forms.Button();
            this.cmdSceneSetScene = new System.Windows.Forms.Button();
            this.cmdSceneCombatStop = new System.Windows.Forms.Button();
            this.cmdSceneEndGame = new System.Windows.Forms.Button();
            this.cmdSceneShop = new System.Windows.Forms.Button();
            this.cmdSceneCombat = new System.Windows.Forms.Button();
            this.cmdPlayerRestoreHealth = new System.Windows.Forms.Button();
            this.cmdPlayerSetSpecies = new System.Windows.Forms.Button();
            this.cmdPlayerSetName = new System.Windows.Forms.Button();
            this.cmdPlayerAddAbilityPoints = new System.Windows.Forms.Button();
            this.cmdPlayerAddXP = new System.Windows.Forms.Button();
            this.cmdPlayerSetStat = new System.Windows.Forms.Button();
            this.cmdPlayerAddItem = new System.Windows.Forms.Button();
            this.cmdPlayerSetHealth = new System.Windows.Forms.Button();
            this.cmdPlayerSetEquipment = new System.Windows.Forms.Button();
            this.cmdPlayerSetMoney = new System.Windows.Forms.Button();
            this.cmdJournalUpdate = new System.Windows.Forms.Button();
            this.cmdJournalClose = new System.Windows.Forms.Button();
            this.cmdDataModifyNumber = new System.Windows.Forms.Button();
            this.cmdDataSetString = new System.Windows.Forms.Button();
            this.cmdDataSetNumber = new System.Windows.Forms.Button();
            this.cmdDataSetFlag = new System.Windows.Forms.Button();
            this.cmdAdvLuaScript = new System.Windows.Forms.Button();
            this.cmdUISetInventoryEnabled = new System.Windows.Forms.Button();
            this.cmdUIAddLink = new System.Windows.Forms.Button();
            this.cmdUISetLocation = new System.Windows.Forms.Button();
            this.cmdUISetInstruction = new System.Windows.Forms.Button();
            this.cmdUIClearLog = new System.Windows.Forms.Button();
            this.cmdUILog = new System.Windows.Forms.Button();
            this.cmdUILogSplit = new System.Windows.Forms.Button();
            this.cmdTextSetContext = new System.Windows.Forms.Button();
            this.cmdTextSetVariable = new System.Windows.Forms.Button();
            this.cmdSaveDialog = new System.Windows.Forms.Button();
            this.cmdSaveCheckpoint = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox3 = new System.Windows.Forms.GroupBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            groupBox5 = new System.Windows.Forms.GroupBox();
            groupBox6 = new System.Windows.Forms.GroupBox();
            groupBox7 = new System.Windows.Forms.GroupBox();
            groupBox8 = new System.Windows.Forms.GroupBox();
            groupBox9 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.cmdFlowSleep);
            groupBox1.Controls.Add(this.cmdFlowComment);
            groupBox1.Controls.Add(this.cmdFlowExit);
            groupBox1.Controls.Add(this.cmdFlowLoopBreak);
            groupBox1.Controls.Add(this.cmdFlowLoop);
            groupBox1.Controls.Add(this.cmdFlowIf);
            groupBox1.Location = new System.Drawing.Point(16, 16);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(208, 184);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Flow Control";
            // 
            // cmdFlowSleep
            // 
            this.cmdFlowSleep.Location = new System.Drawing.Point(16, 120);
            this.cmdFlowSleep.Name = "cmdFlowSleep";
            this.cmdFlowSleep.Size = new System.Drawing.Size(176, 24);
            this.cmdFlowSleep.TabIndex = 6;
            this.cmdFlowSleep.Text = "Wait";
            this.cmdFlowSleep.UseVisualStyleBackColor = true;
            this.cmdFlowSleep.Click += new System.EventHandler(this.cmdFlowSleep_Click);
            // 
            // cmdFlowComment
            // 
            this.cmdFlowComment.Location = new System.Drawing.Point(16, 144);
            this.cmdFlowComment.Name = "cmdFlowComment";
            this.cmdFlowComment.Size = new System.Drawing.Size(176, 24);
            this.cmdFlowComment.TabIndex = 5;
            this.cmdFlowComment.Text = "Comment";
            this.cmdFlowComment.UseVisualStyleBackColor = true;
            this.cmdFlowComment.Click += new System.EventHandler(this.cmdFlowComment_Click);
            // 
            // cmdFlowExit
            // 
            this.cmdFlowExit.Location = new System.Drawing.Point(16, 96);
            this.cmdFlowExit.Name = "cmdFlowExit";
            this.cmdFlowExit.Size = new System.Drawing.Size(176, 24);
            this.cmdFlowExit.TabIndex = 4;
            this.cmdFlowExit.Text = "Exit Script";
            this.cmdFlowExit.UseVisualStyleBackColor = true;
            this.cmdFlowExit.Click += new System.EventHandler(this.cmdFlowExit_Click);
            // 
            // cmdFlowLoopBreak
            // 
            this.cmdFlowLoopBreak.Location = new System.Drawing.Point(16, 72);
            this.cmdFlowLoopBreak.Name = "cmdFlowLoopBreak";
            this.cmdFlowLoopBreak.Size = new System.Drawing.Size(176, 24);
            this.cmdFlowLoopBreak.TabIndex = 3;
            this.cmdFlowLoopBreak.Text = "Break Loop";
            this.cmdFlowLoopBreak.UseVisualStyleBackColor = true;
            this.cmdFlowLoopBreak.Click += new System.EventHandler(this.cmdFlowLoopBreak_Click);
            // 
            // cmdFlowLoop
            // 
            this.cmdFlowLoop.Location = new System.Drawing.Point(16, 48);
            this.cmdFlowLoop.Name = "cmdFlowLoop";
            this.cmdFlowLoop.Size = new System.Drawing.Size(176, 24);
            this.cmdFlowLoop.TabIndex = 2;
            this.cmdFlowLoop.Text = "Loop";
            this.cmdFlowLoop.UseVisualStyleBackColor = true;
            this.cmdFlowLoop.Click += new System.EventHandler(this.cmdFlowLoop_Click);
            // 
            // cmdFlowIf
            // 
            this.cmdFlowIf.Location = new System.Drawing.Point(16, 24);
            this.cmdFlowIf.Name = "cmdFlowIf";
            this.cmdFlowIf.Size = new System.Drawing.Size(176, 24);
            this.cmdFlowIf.TabIndex = 1;
            this.cmdFlowIf.Text = "Conditional Branch";
            this.cmdFlowIf.UseVisualStyleBackColor = true;
            this.cmdFlowIf.Click += new System.EventHandler(this.cmdFlowIf_Click);
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.cmdSceneSetScene);
            groupBox2.Controls.Add(this.cmdSceneCombatStop);
            groupBox2.Controls.Add(this.cmdSceneEndGame);
            groupBox2.Controls.Add(this.cmdSceneShop);
            groupBox2.Controls.Add(this.cmdSceneCombat);
            groupBox2.Enabled = false;
            groupBox2.Location = new System.Drawing.Point(240, 240);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(208, 160);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Scene Control";
            // 
            // cmdSceneSetScene
            // 
            this.cmdSceneSetScene.Location = new System.Drawing.Point(16, 24);
            this.cmdSceneSetScene.Name = "cmdSceneSetScene";
            this.cmdSceneSetScene.Size = new System.Drawing.Size(176, 24);
            this.cmdSceneSetScene.TabIndex = 6;
            this.cmdSceneSetScene.Text = "Change Scene";
            this.cmdSceneSetScene.UseVisualStyleBackColor = true;
            // 
            // cmdSceneCombatStop
            // 
            this.cmdSceneCombatStop.Location = new System.Drawing.Point(16, 72);
            this.cmdSceneCombatStop.Name = "cmdSceneCombatStop";
            this.cmdSceneCombatStop.Size = new System.Drawing.Size(176, 24);
            this.cmdSceneCombatStop.TabIndex = 4;
            this.cmdSceneCombatStop.Text = "Stop Combat";
            this.cmdSceneCombatStop.UseVisualStyleBackColor = true;
            // 
            // cmdSceneEndGame
            // 
            this.cmdSceneEndGame.Location = new System.Drawing.Point(16, 120);
            this.cmdSceneEndGame.Name = "cmdSceneEndGame";
            this.cmdSceneEndGame.Size = new System.Drawing.Size(176, 24);
            this.cmdSceneEndGame.TabIndex = 3;
            this.cmdSceneEndGame.Text = "End Game (Game Over)";
            this.cmdSceneEndGame.UseVisualStyleBackColor = true;
            // 
            // cmdSceneShop
            // 
            this.cmdSceneShop.Location = new System.Drawing.Point(16, 96);
            this.cmdSceneShop.Name = "cmdSceneShop";
            this.cmdSceneShop.Size = new System.Drawing.Size(176, 24);
            this.cmdSceneShop.TabIndex = 2;
            this.cmdSceneShop.Text = "Open Shop";
            this.cmdSceneShop.UseVisualStyleBackColor = true;
            // 
            // cmdSceneCombat
            // 
            this.cmdSceneCombat.Location = new System.Drawing.Point(16, 48);
            this.cmdSceneCombat.Name = "cmdSceneCombat";
            this.cmdSceneCombat.Size = new System.Drawing.Size(176, 24);
            this.cmdSceneCombat.TabIndex = 1;
            this.cmdSceneCombat.Text = "Begin Combat";
            this.cmdSceneCombat.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(this.cmdPlayerRestoreHealth);
            groupBox3.Controls.Add(this.cmdPlayerSetSpecies);
            groupBox3.Controls.Add(this.cmdPlayerSetName);
            groupBox3.Controls.Add(this.cmdPlayerAddAbilityPoints);
            groupBox3.Controls.Add(this.cmdPlayerAddXP);
            groupBox3.Controls.Add(this.cmdPlayerSetStat);
            groupBox3.Controls.Add(this.cmdPlayerAddItem);
            groupBox3.Controls.Add(this.cmdPlayerSetHealth);
            groupBox3.Controls.Add(this.cmdPlayerSetEquipment);
            groupBox3.Controls.Add(this.cmdPlayerSetMoney);
            groupBox3.Location = new System.Drawing.Point(16, 208);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(208, 280);
            groupBox3.TabIndex = 8;
            groupBox3.TabStop = false;
            groupBox3.Text = "Player";
            // 
            // cmdPlayerRestoreHealth
            // 
            this.cmdPlayerRestoreHealth.Location = new System.Drawing.Point(16, 120);
            this.cmdPlayerRestoreHealth.Name = "cmdPlayerRestoreHealth";
            this.cmdPlayerRestoreHealth.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerRestoreHealth.TabIndex = 12;
            this.cmdPlayerRestoreHealth.Text = "Restore All Health";
            this.cmdPlayerRestoreHealth.UseVisualStyleBackColor = true;
            this.cmdPlayerRestoreHealth.Click += new System.EventHandler(this.cmdPlayerRestoreHealth_Click);
            // 
            // cmdPlayerSetSpecies
            // 
            this.cmdPlayerSetSpecies.Location = new System.Drawing.Point(16, 48);
            this.cmdPlayerSetSpecies.Name = "cmdPlayerSetSpecies";
            this.cmdPlayerSetSpecies.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerSetSpecies.TabIndex = 11;
            this.cmdPlayerSetSpecies.Text = "Change Species";
            this.cmdPlayerSetSpecies.UseVisualStyleBackColor = true;
            this.cmdPlayerSetSpecies.Click += new System.EventHandler(this.cmdPlayerSetSpecies_Click);
            // 
            // cmdPlayerSetName
            // 
            this.cmdPlayerSetName.Location = new System.Drawing.Point(16, 24);
            this.cmdPlayerSetName.Name = "cmdPlayerSetName";
            this.cmdPlayerSetName.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerSetName.TabIndex = 10;
            this.cmdPlayerSetName.Text = "Change Name";
            this.cmdPlayerSetName.UseVisualStyleBackColor = true;
            this.cmdPlayerSetName.Click += new System.EventHandler(this.cmdPlayerSetName_Click);
            // 
            // cmdPlayerAddAbilityPoints
            // 
            this.cmdPlayerAddAbilityPoints.Location = new System.Drawing.Point(16, 240);
            this.cmdPlayerAddAbilityPoints.Name = "cmdPlayerAddAbilityPoints";
            this.cmdPlayerAddAbilityPoints.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerAddAbilityPoints.TabIndex = 9;
            this.cmdPlayerAddAbilityPoints.Text = "Add Ability Points";
            this.cmdPlayerAddAbilityPoints.UseVisualStyleBackColor = true;
            this.cmdPlayerAddAbilityPoints.Click += new System.EventHandler(this.cmdPlayerAddAbilityPoints_Click);
            // 
            // cmdPlayerAddXP
            // 
            this.cmdPlayerAddXP.Location = new System.Drawing.Point(16, 216);
            this.cmdPlayerAddXP.Name = "cmdPlayerAddXP";
            this.cmdPlayerAddXP.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerAddXP.TabIndex = 8;
            this.cmdPlayerAddXP.Text = "Add XP";
            this.cmdPlayerAddXP.UseVisualStyleBackColor = true;
            this.cmdPlayerAddXP.Click += new System.EventHandler(this.cmdPlayerAddXP_Click);
            // 
            // cmdPlayerSetStat
            // 
            this.cmdPlayerSetStat.Location = new System.Drawing.Point(16, 72);
            this.cmdPlayerSetStat.Name = "cmdPlayerSetStat";
            this.cmdPlayerSetStat.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerSetStat.TabIndex = 6;
            this.cmdPlayerSetStat.Text = "Change Primary Stat";
            this.cmdPlayerSetStat.UseVisualStyleBackColor = true;
            this.cmdPlayerSetStat.Click += new System.EventHandler(this.cmdPlayerSetStat_Click);
            // 
            // cmdPlayerAddItem
            // 
            this.cmdPlayerAddItem.Location = new System.Drawing.Point(16, 192);
            this.cmdPlayerAddItem.Name = "cmdPlayerAddItem";
            this.cmdPlayerAddItem.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerAddItem.TabIndex = 5;
            this.cmdPlayerAddItem.Text = "Add/Remove Item";
            this.cmdPlayerAddItem.UseVisualStyleBackColor = true;
            this.cmdPlayerAddItem.Click += new System.EventHandler(this.cmdPlayerAddItem_Click);
            // 
            // cmdPlayerSetHealth
            // 
            this.cmdPlayerSetHealth.Location = new System.Drawing.Point(16, 96);
            this.cmdPlayerSetHealth.Name = "cmdPlayerSetHealth";
            this.cmdPlayerSetHealth.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerSetHealth.TabIndex = 4;
            this.cmdPlayerSetHealth.Text = "Change Health";
            this.cmdPlayerSetHealth.UseVisualStyleBackColor = true;
            this.cmdPlayerSetHealth.Click += new System.EventHandler(this.cmdPlayerSetHealth_Click);
            // 
            // cmdPlayerSetEquipment
            // 
            this.cmdPlayerSetEquipment.Location = new System.Drawing.Point(16, 168);
            this.cmdPlayerSetEquipment.Name = "cmdPlayerSetEquipment";
            this.cmdPlayerSetEquipment.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerSetEquipment.TabIndex = 3;
            this.cmdPlayerSetEquipment.Text = "Change Equipment";
            this.cmdPlayerSetEquipment.UseVisualStyleBackColor = true;
            this.cmdPlayerSetEquipment.Click += new System.EventHandler(this.cmdPlayerSetEquipment_Click);
            // 
            // cmdPlayerSetMoney
            // 
            this.cmdPlayerSetMoney.Location = new System.Drawing.Point(16, 144);
            this.cmdPlayerSetMoney.Name = "cmdPlayerSetMoney";
            this.cmdPlayerSetMoney.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerSetMoney.TabIndex = 2;
            this.cmdPlayerSetMoney.Text = "Change Money";
            this.cmdPlayerSetMoney.UseVisualStyleBackColor = true;
            this.cmdPlayerSetMoney.Click += new System.EventHandler(this.cmdPlayerSetMoney_Click);
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(this.cmdJournalUpdate);
            groupBox4.Controls.Add(this.cmdJournalClose);
            groupBox4.Enabled = false;
            groupBox4.Location = new System.Drawing.Point(240, 416);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(208, 88);
            groupBox4.TabIndex = 8;
            groupBox4.TabStop = false;
            groupBox4.Text = "Journal";
            // 
            // cmdJournalUpdate
            // 
            this.cmdJournalUpdate.Location = new System.Drawing.Point(16, 24);
            this.cmdJournalUpdate.Name = "cmdJournalUpdate";
            this.cmdJournalUpdate.Size = new System.Drawing.Size(176, 24);
            this.cmdJournalUpdate.TabIndex = 6;
            this.cmdJournalUpdate.Text = "Add/Update Quest";
            this.cmdJournalUpdate.UseVisualStyleBackColor = true;
            // 
            // cmdJournalClose
            // 
            this.cmdJournalClose.Location = new System.Drawing.Point(16, 48);
            this.cmdJournalClose.Name = "cmdJournalClose";
            this.cmdJournalClose.Size = new System.Drawing.Size(176, 24);
            this.cmdJournalClose.TabIndex = 1;
            this.cmdJournalClose.Text = "Remove Quest";
            this.cmdJournalClose.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(this.cmdDataModifyNumber);
            groupBox5.Controls.Add(this.cmdDataSetString);
            groupBox5.Controls.Add(this.cmdDataSetNumber);
            groupBox5.Controls.Add(this.cmdDataSetFlag);
            groupBox5.Enabled = false;
            groupBox5.Location = new System.Drawing.Point(464, 120);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(208, 136);
            groupBox5.TabIndex = 9;
            groupBox5.TabStop = false;
            groupBox5.Text = "Data Storage";
            // 
            // cmdDataModifyNumber
            // 
            this.cmdDataModifyNumber.Location = new System.Drawing.Point(16, 96);
            this.cmdDataModifyNumber.Name = "cmdDataModifyNumber";
            this.cmdDataModifyNumber.Size = new System.Drawing.Size(176, 24);
            this.cmdDataModifyNumber.TabIndex = 9;
            this.cmdDataModifyNumber.Text = "Adjust Number Variable";
            this.cmdDataModifyNumber.UseVisualStyleBackColor = true;
            // 
            // cmdDataSetString
            // 
            this.cmdDataSetString.Location = new System.Drawing.Point(16, 72);
            this.cmdDataSetString.Name = "cmdDataSetString";
            this.cmdDataSetString.Size = new System.Drawing.Size(176, 24);
            this.cmdDataSetString.TabIndex = 7;
            this.cmdDataSetString.Text = "Set String Variable";
            this.cmdDataSetString.UseVisualStyleBackColor = true;
            // 
            // cmdDataSetNumber
            // 
            this.cmdDataSetNumber.Location = new System.Drawing.Point(16, 48);
            this.cmdDataSetNumber.Name = "cmdDataSetNumber";
            this.cmdDataSetNumber.Size = new System.Drawing.Size(176, 24);
            this.cmdDataSetNumber.TabIndex = 6;
            this.cmdDataSetNumber.Text = "Set Number Variable";
            this.cmdDataSetNumber.UseVisualStyleBackColor = true;
            // 
            // cmdDataSetFlag
            // 
            this.cmdDataSetFlag.Location = new System.Drawing.Point(16, 24);
            this.cmdDataSetFlag.Name = "cmdDataSetFlag";
            this.cmdDataSetFlag.Size = new System.Drawing.Size(176, 24);
            this.cmdDataSetFlag.TabIndex = 1;
            this.cmdDataSetFlag.Text = "Set Flag Variable";
            this.cmdDataSetFlag.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(this.cmdAdvLuaScript);
            groupBox6.Location = new System.Drawing.Point(464, 376);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new System.Drawing.Size(208, 64);
            groupBox6.TabIndex = 9;
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
            this.cmdAdvLuaScript.Click += new System.EventHandler(this.cmdAdvLuaScript_Click);
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(this.cmdUISetInventoryEnabled);
            groupBox7.Controls.Add(this.cmdUIAddLink);
            groupBox7.Controls.Add(this.cmdUISetLocation);
            groupBox7.Controls.Add(this.cmdUISetInstruction);
            groupBox7.Controls.Add(this.cmdUIClearLog);
            groupBox7.Controls.Add(this.cmdUILog);
            groupBox7.Controls.Add(this.cmdUILogSplit);
            groupBox7.Location = new System.Drawing.Point(240, 16);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new System.Drawing.Size(208, 208);
            groupBox7.TabIndex = 9;
            groupBox7.TabStop = false;
            groupBox7.Text = "Interface Control";
            // 
            // cmdUISetInventoryEnabled
            // 
            this.cmdUISetInventoryEnabled.Location = new System.Drawing.Point(16, 120);
            this.cmdUISetInventoryEnabled.Name = "cmdUISetInventoryEnabled";
            this.cmdUISetInventoryEnabled.Size = new System.Drawing.Size(176, 24);
            this.cmdUISetInventoryEnabled.TabIndex = 11;
            this.cmdUISetInventoryEnabled.Text = "Set Character Sheet Enabled";
            this.cmdUISetInventoryEnabled.UseVisualStyleBackColor = true;
            this.cmdUISetInventoryEnabled.Click += new System.EventHandler(this.cmdUISetInventoryEnabled_Click);
            // 
            // cmdUIAddLink
            // 
            this.cmdUIAddLink.Enabled = false;
            this.cmdUIAddLink.Location = new System.Drawing.Point(16, 96);
            this.cmdUIAddLink.Name = "cmdUIAddLink";
            this.cmdUIAddLink.Size = new System.Drawing.Size(176, 24);
            this.cmdUIAddLink.TabIndex = 10;
            this.cmdUIAddLink.Text = "Add Compass Link";
            this.cmdUIAddLink.UseVisualStyleBackColor = true;
            this.cmdUIAddLink.Click += new System.EventHandler(this.cmdUIAddLink_Click);
            // 
            // cmdUISetLocation
            // 
            this.cmdUISetLocation.Location = new System.Drawing.Point(16, 168);
            this.cmdUISetLocation.Name = "cmdUISetLocation";
            this.cmdUISetLocation.Size = new System.Drawing.Size(176, 24);
            this.cmdUISetLocation.TabIndex = 9;
            this.cmdUISetLocation.Text = "Set Location Text";
            this.cmdUISetLocation.UseVisualStyleBackColor = true;
            this.cmdUISetLocation.Click += new System.EventHandler(this.cmdUISetLocation_Click);
            // 
            // cmdUISetInstruction
            // 
            this.cmdUISetInstruction.Location = new System.Drawing.Point(16, 144);
            this.cmdUISetInstruction.Name = "cmdUISetInstruction";
            this.cmdUISetInstruction.Size = new System.Drawing.Size(176, 24);
            this.cmdUISetInstruction.TabIndex = 8;
            this.cmdUISetInstruction.Text = "Set Instruction Text";
            this.cmdUISetInstruction.UseVisualStyleBackColor = true;
            this.cmdUISetInstruction.Click += new System.EventHandler(this.cmdUISetInstruction_Click);
            // 
            // cmdUIClearLog
            // 
            this.cmdUIClearLog.Location = new System.Drawing.Point(16, 72);
            this.cmdUIClearLog.Name = "cmdUIClearLog";
            this.cmdUIClearLog.Size = new System.Drawing.Size(176, 24);
            this.cmdUIClearLog.TabIndex = 7;
            this.cmdUIClearLog.Text = "Clear Message Log";
            this.cmdUIClearLog.UseVisualStyleBackColor = true;
            this.cmdUIClearLog.Click += new System.EventHandler(this.cmdUIClearLog_Click);
            // 
            // cmdUILog
            // 
            this.cmdUILog.Location = new System.Drawing.Point(16, 24);
            this.cmdUILog.Name = "cmdUILog";
            this.cmdUILog.Size = new System.Drawing.Size(176, 24);
            this.cmdUILog.TabIndex = 6;
            this.cmdUILog.Text = "Show Message";
            this.cmdUILog.UseVisualStyleBackColor = true;
            this.cmdUILog.Click += new System.EventHandler(this.cmdUILog_Click);
            // 
            // cmdUILogSplit
            // 
            this.cmdUILogSplit.Location = new System.Drawing.Point(16, 48);
            this.cmdUILogSplit.Name = "cmdUILogSplit";
            this.cmdUILogSplit.Size = new System.Drawing.Size(176, 24);
            this.cmdUILogSplit.TabIndex = 1;
            this.cmdUILogSplit.Text = "Show Horizontal Bar";
            this.cmdUILogSplit.UseVisualStyleBackColor = true;
            this.cmdUILogSplit.Click += new System.EventHandler(this.cmdUILogSplit_Click);
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(this.cmdTextSetContext);
            groupBox8.Controls.Add(this.cmdTextSetVariable);
            groupBox8.Enabled = false;
            groupBox8.Location = new System.Drawing.Point(464, 272);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new System.Drawing.Size(208, 88);
            groupBox8.TabIndex = 9;
            groupBox8.TabStop = false;
            groupBox8.Text = "Grammar Engine";
            // 
            // cmdTextSetContext
            // 
            this.cmdTextSetContext.Location = new System.Drawing.Point(16, 24);
            this.cmdTextSetContext.Name = "cmdTextSetContext";
            this.cmdTextSetContext.Size = new System.Drawing.Size(176, 24);
            this.cmdTextSetContext.TabIndex = 6;
            this.cmdTextSetContext.Text = "Set Grammar Context";
            this.cmdTextSetContext.UseVisualStyleBackColor = true;
            // 
            // cmdTextSetVariable
            // 
            this.cmdTextSetVariable.Location = new System.Drawing.Point(16, 48);
            this.cmdTextSetVariable.Name = "cmdTextSetVariable";
            this.cmdTextSetVariable.Size = new System.Drawing.Size(176, 24);
            this.cmdTextSetVariable.TabIndex = 1;
            this.cmdTextSetVariable.Text = "Set Grammar Variable";
            this.cmdTextSetVariable.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(this.cmdSaveDialog);
            groupBox9.Controls.Add(this.cmdSaveCheckpoint);
            groupBox9.Enabled = false;
            groupBox9.Location = new System.Drawing.Point(464, 16);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new System.Drawing.Size(208, 88);
            groupBox9.TabIndex = 10;
            groupBox9.TabStop = false;
            groupBox9.Text = "Save Data";
            // 
            // cmdSaveDialog
            // 
            this.cmdSaveDialog.Location = new System.Drawing.Point(16, 24);
            this.cmdSaveDialog.Name = "cmdSaveDialog";
            this.cmdSaveDialog.Size = new System.Drawing.Size(176, 24);
            this.cmdSaveDialog.TabIndex = 6;
            this.cmdSaveDialog.Text = "Open Save Menu";
            this.cmdSaveDialog.UseVisualStyleBackColor = true;
            // 
            // cmdSaveCheckpoint
            // 
            this.cmdSaveCheckpoint.Location = new System.Drawing.Point(16, 48);
            this.cmdSaveCheckpoint.Name = "cmdSaveCheckpoint";
            this.cmdSaveCheckpoint.Size = new System.Drawing.Size(176, 24);
            this.cmdSaveCheckpoint.TabIndex = 1;
            this.cmdSaveCheckpoint.Text = "Save Checkpoint";
            this.cmdSaveCheckpoint.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Image = global::Finmer.Editor.Properties.Resources.cross;
            this.cmdCancel.Location = new System.Drawing.Point(536, 488);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(128, 32);
            this.cmdCancel.TabIndex = 0;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // FormVisualScriptCommandPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(688, 544);
            this.Controls.Add(groupBox9);
            this.Controls.Add(groupBox8);
            this.Controls.Add(groupBox7);
            this.Controls.Add(groupBox6);
            this.Controls.Add(groupBox5);
            this.Controls.Add(groupBox4);
            this.Controls.Add(groupBox3);
            this.Controls.Add(groupBox2);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormVisualScriptCommandPalette";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Script Command";
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox9.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cmdFlowIf;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdFlowSleep;
        private System.Windows.Forms.Button cmdFlowComment;
        private System.Windows.Forms.Button cmdFlowExit;
        private System.Windows.Forms.Button cmdFlowLoopBreak;
        private System.Windows.Forms.Button cmdFlowLoop;
        private System.Windows.Forms.Button cmdSceneSetScene;
        private System.Windows.Forms.Button cmdSceneCombatStop;
        private System.Windows.Forms.Button cmdSceneEndGame;
        private System.Windows.Forms.Button cmdSceneShop;
        private System.Windows.Forms.Button cmdSceneCombat;
        private System.Windows.Forms.Button cmdPlayerSetStat;
        private System.Windows.Forms.Button cmdPlayerAddItem;
        private System.Windows.Forms.Button cmdPlayerSetHealth;
        private System.Windows.Forms.Button cmdPlayerSetEquipment;
        private System.Windows.Forms.Button cmdPlayerSetMoney;
        private System.Windows.Forms.Button cmdPlayerAddXP;
        private System.Windows.Forms.Button cmdPlayerAddAbilityPoints;
        private System.Windows.Forms.Button cmdPlayerRestoreHealth;
        private System.Windows.Forms.Button cmdPlayerSetSpecies;
        private System.Windows.Forms.Button cmdPlayerSetName;
        private System.Windows.Forms.Button cmdJournalUpdate;
        private System.Windows.Forms.Button cmdJournalClose;
        private System.Windows.Forms.Button cmdDataSetString;
        private System.Windows.Forms.Button cmdDataSetNumber;
        private System.Windows.Forms.Button cmdDataSetFlag;
        private System.Windows.Forms.Button cmdAdvLuaScript;
        private System.Windows.Forms.Button cmdUILog;
        private System.Windows.Forms.Button cmdUILogSplit;
        private System.Windows.Forms.Button cmdUISetInventoryEnabled;
        private System.Windows.Forms.Button cmdUIAddLink;
        private System.Windows.Forms.Button cmdUISetLocation;
        private System.Windows.Forms.Button cmdUISetInstruction;
        private System.Windows.Forms.Button cmdUIClearLog;
        private System.Windows.Forms.Button cmdDataModifyNumber;
        private System.Windows.Forms.Button cmdTextSetContext;
        private System.Windows.Forms.Button cmdTextSetVariable;
        private System.Windows.Forms.Button cmdSaveDialog;
        private System.Windows.Forms.Button cmdSaveCheckpoint;
    }
}