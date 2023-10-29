
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.GroupBox groupBox3;
            System.Windows.Forms.GroupBox groupBox5;
            System.Windows.Forms.GroupBox groupBox6;
            System.Windows.Forms.GroupBox groupBox7;
            System.Windows.Forms.GroupBox groupBox8;
            System.Windows.Forms.GroupBox groupBox9;
            System.Windows.Forms.GroupBox groupBox10;
            System.Windows.Forms.GroupBox groupBox4;
            this.cmdFlowSleep = new System.Windows.Forms.Button();
            this.cmdFlowComment = new System.Windows.Forms.Button();
            this.cmdFlowExit = new System.Windows.Forms.Button();
            this.cmdFlowLoopBreak = new System.Windows.Forms.Button();
            this.cmdFlowLoop = new System.Windows.Forms.Button();
            this.cmdFlowIf = new System.Windows.Forms.Button();
            this.cmdSceneSetScene = new System.Windows.Forms.Button();
            this.cmdSceneEndGame = new System.Windows.Forms.Button();
            this.cmdSceneShop = new System.Windows.Forms.Button();
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
            this.cmdDataSetString = new System.Windows.Forms.Button();
            this.cmdDataSetNumber = new System.Windows.Forms.Button();
            this.cmdDataSetFlag = new System.Windows.Forms.Button();
            this.cmdAdvLuaScript = new System.Windows.Forms.Button();
            this.cmdUISetInventoryEnabled = new System.Windows.Forms.Button();
            this.cmdUISetLocation = new System.Windows.Forms.Button();
            this.cmdUISetInstruction = new System.Windows.Forms.Button();
            this.cmdUIClearLog = new System.Windows.Forms.Button();
            this.cmdUILog = new System.Windows.Forms.Button();
            this.cmdUILogSplit = new System.Windows.Forms.Button();
            this.cmdTextSetContext = new System.Windows.Forms.Button();
            this.cmdTextSetVariable = new System.Windows.Forms.Button();
            this.cmdSaveDialog = new System.Windows.Forms.Button();
            this.cmdSaveCheckpoint = new System.Windows.Forms.Button();
            this.cmdCombatApplyBuff = new System.Windows.Forms.Button();
            this.cmdCombatPreysense = new System.Windows.Forms.Button();
            this.cmdCombatSetGrappled = new System.Windows.Forms.Button();
            this.cmdCombatSetVored = new System.Windows.Forms.Button();
            this.cmdCombatStop = new System.Windows.Forms.Button();
            this.cmdCombatBegin = new System.Windows.Forms.Button();
            this.cmdJournalUpdate = new System.Windows.Forms.Button();
            this.cmdJournalClose = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.ttpCommand = new System.Windows.Forms.ToolTip(this.components);
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox3 = new System.Windows.Forms.GroupBox();
            groupBox5 = new System.Windows.Forms.GroupBox();
            groupBox6 = new System.Windows.Forms.GroupBox();
            groupBox7 = new System.Windows.Forms.GroupBox();
            groupBox8 = new System.Windows.Forms.GroupBox();
            groupBox9 = new System.Windows.Forms.GroupBox();
            groupBox10 = new System.Windows.Forms.GroupBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox9.SuspendLayout();
            groupBox10.SuspendLayout();
            groupBox4.SuspendLayout();
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
            this.cmdFlowSleep.TabIndex = 4;
            this.cmdFlowSleep.Text = "Wait";
            this.ttpCommand.SetToolTip(this.cmdFlowSleep, "Pause the script for a number of seconds.\r\nDramatic pause, oh my!");
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
            this.ttpCommand.SetToolTip(this.cmdFlowComment, "Add comments to the script. These don\'t do anything in-game,\r\nbut may be useful f" +
        "or your own notekeeping purposes.");
            this.cmdFlowComment.UseVisualStyleBackColor = true;
            this.cmdFlowComment.Click += new System.EventHandler(this.cmdFlowComment_Click);
            // 
            // cmdFlowExit
            // 
            this.cmdFlowExit.Location = new System.Drawing.Point(16, 96);
            this.cmdFlowExit.Name = "cmdFlowExit";
            this.cmdFlowExit.Size = new System.Drawing.Size(176, 24);
            this.cmdFlowExit.TabIndex = 3;
            this.cmdFlowExit.Text = "Exit Script";
            this.ttpCommand.SetToolTip(this.cmdFlowExit, "Stop running the current script.");
            this.cmdFlowExit.UseVisualStyleBackColor = true;
            this.cmdFlowExit.Click += new System.EventHandler(this.cmdFlowExit_Click);
            // 
            // cmdFlowLoopBreak
            // 
            this.cmdFlowLoopBreak.Location = new System.Drawing.Point(16, 72);
            this.cmdFlowLoopBreak.Name = "cmdFlowLoopBreak";
            this.cmdFlowLoopBreak.Size = new System.Drawing.Size(176, 24);
            this.cmdFlowLoopBreak.TabIndex = 2;
            this.cmdFlowLoopBreak.Text = "Break Loop";
            this.ttpCommand.SetToolTip(this.cmdFlowLoopBreak, "Exit a Loop command.");
            this.cmdFlowLoopBreak.UseVisualStyleBackColor = true;
            this.cmdFlowLoopBreak.Click += new System.EventHandler(this.cmdFlowLoopBreak_Click);
            // 
            // cmdFlowLoop
            // 
            this.cmdFlowLoop.Location = new System.Drawing.Point(16, 48);
            this.cmdFlowLoop.Name = "cmdFlowLoop";
            this.cmdFlowLoop.Size = new System.Drawing.Size(176, 24);
            this.cmdFlowLoop.TabIndex = 1;
            this.cmdFlowLoop.Text = "Loop";
            this.ttpCommand.SetToolTip(this.cmdFlowLoop, "Repeat a group of commands.");
            this.cmdFlowLoop.UseVisualStyleBackColor = true;
            this.cmdFlowLoop.Click += new System.EventHandler(this.cmdFlowLoop_Click);
            // 
            // cmdFlowIf
            // 
            this.cmdFlowIf.Location = new System.Drawing.Point(16, 24);
            this.cmdFlowIf.Name = "cmdFlowIf";
            this.cmdFlowIf.Size = new System.Drawing.Size(176, 24);
            this.cmdFlowIf.TabIndex = 0;
            this.cmdFlowIf.Text = "Conditional Branch";
            this.ttpCommand.SetToolTip(this.cmdFlowIf, "Run a group of commands only if some condition is met.");
            this.cmdFlowIf.UseVisualStyleBackColor = true;
            this.cmdFlowIf.Click += new System.EventHandler(this.cmdFlowIf_Click);
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.cmdSceneSetScene);
            groupBox2.Controls.Add(this.cmdSceneEndGame);
            groupBox2.Controls.Add(this.cmdSceneShop);
            groupBox2.Location = new System.Drawing.Point(240, 216);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(208, 112);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Scene Control";
            // 
            // cmdSceneSetScene
            // 
            this.cmdSceneSetScene.Location = new System.Drawing.Point(16, 24);
            this.cmdSceneSetScene.Name = "cmdSceneSetScene";
            this.cmdSceneSetScene.Size = new System.Drawing.Size(176, 24);
            this.cmdSceneSetScene.TabIndex = 0;
            this.cmdSceneSetScene.Text = "Change Scene";
            this.ttpCommand.SetToolTip(this.cmdSceneSetScene, "Stop the current script and switch to another Scene.");
            this.cmdSceneSetScene.UseVisualStyleBackColor = true;
            this.cmdSceneSetScene.Click += new System.EventHandler(this.cmdSceneSetScene_Click);
            // 
            // cmdSceneEndGame
            // 
            this.cmdSceneEndGame.Location = new System.Drawing.Point(16, 72);
            this.cmdSceneEndGame.Name = "cmdSceneEndGame";
            this.cmdSceneEndGame.Size = new System.Drawing.Size(176, 24);
            this.cmdSceneEndGame.TabIndex = 2;
            this.cmdSceneEndGame.Text = "End Game (Game Over)";
            this.ttpCommand.SetToolTip(this.cmdSceneEndGame, "End the current game and show reload/exit buttons.");
            this.cmdSceneEndGame.UseVisualStyleBackColor = true;
            this.cmdSceneEndGame.Click += new System.EventHandler(this.cmdSceneEndGame_Click);
            // 
            // cmdSceneShop
            // 
            this.cmdSceneShop.Location = new System.Drawing.Point(16, 48);
            this.cmdSceneShop.Name = "cmdSceneShop";
            this.cmdSceneShop.Size = new System.Drawing.Size(176, 24);
            this.cmdSceneShop.TabIndex = 1;
            this.cmdSceneShop.Text = "Open Shop";
            this.ttpCommand.SetToolTip(this.cmdSceneShop, "Open the shop menu with the configured merchandise.");
            this.cmdSceneShop.UseVisualStyleBackColor = true;
            this.cmdSceneShop.Click += new System.EventHandler(this.cmdSceneShop_Click);
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
            groupBox3.Location = new System.Drawing.Point(16, 216);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(208, 280);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            groupBox3.Text = "Player";
            // 
            // cmdPlayerRestoreHealth
            // 
            this.cmdPlayerRestoreHealth.Location = new System.Drawing.Point(16, 120);
            this.cmdPlayerRestoreHealth.Name = "cmdPlayerRestoreHealth";
            this.cmdPlayerRestoreHealth.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerRestoreHealth.TabIndex = 4;
            this.cmdPlayerRestoreHealth.Text = "Restore All Health";
            this.ttpCommand.SetToolTip(this.cmdPlayerRestoreHealth, "Set the player character\'s Health to the Max Health.");
            this.cmdPlayerRestoreHealth.UseVisualStyleBackColor = true;
            this.cmdPlayerRestoreHealth.Click += new System.EventHandler(this.cmdPlayerRestoreHealth_Click);
            // 
            // cmdPlayerSetSpecies
            // 
            this.cmdPlayerSetSpecies.Location = new System.Drawing.Point(16, 48);
            this.cmdPlayerSetSpecies.Name = "cmdPlayerSetSpecies";
            this.cmdPlayerSetSpecies.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerSetSpecies.TabIndex = 1;
            this.cmdPlayerSetSpecies.Text = "Change Species";
            this.ttpCommand.SetToolTip(this.cmdPlayerSetSpecies, "Change the nouns and adjectives for the player\'s species.");
            this.cmdPlayerSetSpecies.UseVisualStyleBackColor = true;
            this.cmdPlayerSetSpecies.Click += new System.EventHandler(this.cmdPlayerSetSpecies_Click);
            // 
            // cmdPlayerSetName
            // 
            this.cmdPlayerSetName.Location = new System.Drawing.Point(16, 24);
            this.cmdPlayerSetName.Name = "cmdPlayerSetName";
            this.cmdPlayerSetName.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerSetName.TabIndex = 0;
            this.cmdPlayerSetName.Text = "Change Name";
            this.ttpCommand.SetToolTip(this.cmdPlayerSetName, "Change the player character\'s name.");
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
            this.ttpCommand.SetToolTip(this.cmdPlayerAddAbilityPoints, "Grant the player ability points.");
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
            this.ttpCommand.SetToolTip(this.cmdPlayerAddXP, "Grant the player experience points.");
            this.cmdPlayerAddXP.UseVisualStyleBackColor = true;
            this.cmdPlayerAddXP.Click += new System.EventHandler(this.cmdPlayerAddXP_Click);
            // 
            // cmdPlayerSetStat
            // 
            this.cmdPlayerSetStat.Location = new System.Drawing.Point(16, 72);
            this.cmdPlayerSetStat.Name = "cmdPlayerSetStat";
            this.cmdPlayerSetStat.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerSetStat.TabIndex = 2;
            this.cmdPlayerSetStat.Text = "Change Ability Score";
            this.ttpCommand.SetToolTip(this.cmdPlayerSetStat, "Modify one of the player\'s ability scores (Strength, Agility,\r\nBody, Wits).");
            this.cmdPlayerSetStat.UseVisualStyleBackColor = true;
            this.cmdPlayerSetStat.Click += new System.EventHandler(this.cmdPlayerSetStat_Click);
            // 
            // cmdPlayerAddItem
            // 
            this.cmdPlayerAddItem.Location = new System.Drawing.Point(16, 192);
            this.cmdPlayerAddItem.Name = "cmdPlayerAddItem";
            this.cmdPlayerAddItem.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerAddItem.TabIndex = 7;
            this.cmdPlayerAddItem.Text = "Add/Remove Item";
            this.ttpCommand.SetToolTip(this.cmdPlayerAddItem, "Add or remove items from the player\'s backpack.");
            this.cmdPlayerAddItem.UseVisualStyleBackColor = true;
            this.cmdPlayerAddItem.Click += new System.EventHandler(this.cmdPlayerAddItem_Click);
            // 
            // cmdPlayerSetHealth
            // 
            this.cmdPlayerSetHealth.Location = new System.Drawing.Point(16, 96);
            this.cmdPlayerSetHealth.Name = "cmdPlayerSetHealth";
            this.cmdPlayerSetHealth.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerSetHealth.TabIndex = 3;
            this.cmdPlayerSetHealth.Text = "Change Health";
            this.ttpCommand.SetToolTip(this.cmdPlayerSetHealth, "Change the player character\'s current Health.");
            this.cmdPlayerSetHealth.UseVisualStyleBackColor = true;
            this.cmdPlayerSetHealth.Click += new System.EventHandler(this.cmdPlayerSetHealth_Click);
            // 
            // cmdPlayerSetEquipment
            // 
            this.cmdPlayerSetEquipment.Location = new System.Drawing.Point(16, 168);
            this.cmdPlayerSetEquipment.Name = "cmdPlayerSetEquipment";
            this.cmdPlayerSetEquipment.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerSetEquipment.TabIndex = 6;
            this.cmdPlayerSetEquipment.Text = "Change Equipment";
            this.ttpCommand.SetToolTip(this.cmdPlayerSetEquipment, "Change the contents of the player character\'s equip slots.");
            this.cmdPlayerSetEquipment.UseVisualStyleBackColor = true;
            this.cmdPlayerSetEquipment.Click += new System.EventHandler(this.cmdPlayerSetEquipment_Click);
            // 
            // cmdPlayerSetMoney
            // 
            this.cmdPlayerSetMoney.Location = new System.Drawing.Point(16, 144);
            this.cmdPlayerSetMoney.Name = "cmdPlayerSetMoney";
            this.cmdPlayerSetMoney.Size = new System.Drawing.Size(176, 24);
            this.cmdPlayerSetMoney.TabIndex = 5;
            this.cmdPlayerSetMoney.Text = "Change Money";
            this.ttpCommand.SetToolTip(this.cmdPlayerSetMoney, "Add to or subtract from the player\'s money total.");
            this.cmdPlayerSetMoney.UseVisualStyleBackColor = true;
            this.cmdPlayerSetMoney.Click += new System.EventHandler(this.cmdPlayerSetMoney_Click);
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(this.cmdDataSetString);
            groupBox5.Controls.Add(this.cmdDataSetNumber);
            groupBox5.Controls.Add(this.cmdDataSetFlag);
            groupBox5.Location = new System.Drawing.Point(464, 120);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(208, 112);
            groupBox5.TabIndex = 6;
            groupBox5.TabStop = false;
            groupBox5.Text = "Variables";
            // 
            // cmdDataSetString
            // 
            this.cmdDataSetString.Location = new System.Drawing.Point(16, 72);
            this.cmdDataSetString.Name = "cmdDataSetString";
            this.cmdDataSetString.Size = new System.Drawing.Size(176, 24);
            this.cmdDataSetString.TabIndex = 2;
            this.cmdDataSetString.Text = "Set String Variable";
            this.ttpCommand.SetToolTip(this.cmdDataSetString, "Set a text variable in the player\'s save game. Variables can be\r\nchecked later, s" +
        "uch as in a Conditional Branch command.\r\n\r\n");
            this.cmdDataSetString.UseVisualStyleBackColor = true;
            this.cmdDataSetString.Click += new System.EventHandler(this.cmdDataSetString_Click);
            // 
            // cmdDataSetNumber
            // 
            this.cmdDataSetNumber.Location = new System.Drawing.Point(16, 48);
            this.cmdDataSetNumber.Name = "cmdDataSetNumber";
            this.cmdDataSetNumber.Size = new System.Drawing.Size(176, 24);
            this.cmdDataSetNumber.TabIndex = 1;
            this.cmdDataSetNumber.Text = "Set Number Variable";
            this.ttpCommand.SetToolTip(this.cmdDataSetNumber, "Set a number variable in the player\'s save game. Variables can be\r\nchecked later," +
        " such as in a Conditional Branch command.\r\n");
            this.cmdDataSetNumber.UseVisualStyleBackColor = true;
            this.cmdDataSetNumber.Click += new System.EventHandler(this.cmdDataSetNumber_Click);
            // 
            // cmdDataSetFlag
            // 
            this.cmdDataSetFlag.Location = new System.Drawing.Point(16, 24);
            this.cmdDataSetFlag.Name = "cmdDataSetFlag";
            this.cmdDataSetFlag.Size = new System.Drawing.Size(176, 24);
            this.cmdDataSetFlag.TabIndex = 0;
            this.cmdDataSetFlag.Text = "Set Flag Variable";
            this.ttpCommand.SetToolTip(this.cmdDataSetFlag, "Set a flag variable (\'yes/no\') in the player\'s save game. Variables\r\ncan be check" +
        "ed later, such as in a Conditional Branch command.");
            this.cmdDataSetFlag.UseVisualStyleBackColor = true;
            this.cmdDataSetFlag.Click += new System.EventHandler(this.cmdDataSetFlag_Click);
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(this.cmdAdvLuaScript);
            groupBox6.Location = new System.Drawing.Point(464, 456);
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
            this.cmdAdvLuaScript.TabIndex = 0;
            this.cmdAdvLuaScript.Text = "Lua Script";
            this.ttpCommand.SetToolTip(this.cmdAdvLuaScript, "Run a block of raw Lua code. For advanced usage.");
            this.cmdAdvLuaScript.UseVisualStyleBackColor = true;
            this.cmdAdvLuaScript.Click += new System.EventHandler(this.cmdAdvLuaScript_Click);
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(this.cmdUISetInventoryEnabled);
            groupBox7.Controls.Add(this.cmdUISetLocation);
            groupBox7.Controls.Add(this.cmdUISetInstruction);
            groupBox7.Controls.Add(this.cmdUIClearLog);
            groupBox7.Controls.Add(this.cmdUILog);
            groupBox7.Controls.Add(this.cmdUILogSplit);
            groupBox7.Location = new System.Drawing.Point(240, 16);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new System.Drawing.Size(208, 184);
            groupBox7.TabIndex = 2;
            groupBox7.TabStop = false;
            groupBox7.Text = "Interface Control";
            // 
            // cmdUISetInventoryEnabled
            // 
            this.cmdUISetInventoryEnabled.Location = new System.Drawing.Point(16, 96);
            this.cmdUISetInventoryEnabled.Name = "cmdUISetInventoryEnabled";
            this.cmdUISetInventoryEnabled.Size = new System.Drawing.Size(176, 24);
            this.cmdUISetInventoryEnabled.TabIndex = 4;
            this.cmdUISetInventoryEnabled.Text = "Set Character Sheet Enabled";
            this.ttpCommand.SetToolTip(this.cmdUISetInventoryEnabled, "Change whether the player can access their inventory.");
            this.cmdUISetInventoryEnabled.UseVisualStyleBackColor = true;
            this.cmdUISetInventoryEnabled.Click += new System.EventHandler(this.cmdUISetInventoryEnabled_Click);
            // 
            // cmdUISetLocation
            // 
            this.cmdUISetLocation.Location = new System.Drawing.Point(16, 144);
            this.cmdUISetLocation.Name = "cmdUISetLocation";
            this.cmdUISetLocation.Size = new System.Drawing.Size(176, 24);
            this.cmdUISetLocation.TabIndex = 6;
            this.cmdUISetLocation.Text = "Set Location Text";
            this.ttpCommand.SetToolTip(this.cmdUISetLocation, "Set the text that appears above the compass.");
            this.cmdUISetLocation.UseVisualStyleBackColor = true;
            this.cmdUISetLocation.Click += new System.EventHandler(this.cmdUISetLocation_Click);
            // 
            // cmdUISetInstruction
            // 
            this.cmdUISetInstruction.Location = new System.Drawing.Point(16, 120);
            this.cmdUISetInstruction.Name = "cmdUISetInstruction";
            this.cmdUISetInstruction.Size = new System.Drawing.Size(176, 24);
            this.cmdUISetInstruction.TabIndex = 5;
            this.cmdUISetInstruction.Text = "Set Instruction Text";
            this.ttpCommand.SetToolTip(this.cmdUISetInstruction, "Set the text that appears above Choice buttons.");
            this.cmdUISetInstruction.UseVisualStyleBackColor = true;
            this.cmdUISetInstruction.Click += new System.EventHandler(this.cmdUISetInstruction_Click);
            // 
            // cmdUIClearLog
            // 
            this.cmdUIClearLog.Location = new System.Drawing.Point(16, 72);
            this.cmdUIClearLog.Name = "cmdUIClearLog";
            this.cmdUIClearLog.Size = new System.Drawing.Size(176, 24);
            this.cmdUIClearLog.TabIndex = 2;
            this.cmdUIClearLog.Text = "Clear Message Log";
            this.ttpCommand.SetToolTip(this.cmdUIClearLog, "Erase all contents of the game log.");
            this.cmdUIClearLog.UseVisualStyleBackColor = true;
            this.cmdUIClearLog.Click += new System.EventHandler(this.cmdUIClearLog_Click);
            // 
            // cmdUILog
            // 
            this.cmdUILog.Location = new System.Drawing.Point(16, 24);
            this.cmdUILog.Name = "cmdUILog";
            this.cmdUILog.Size = new System.Drawing.Size(176, 24);
            this.cmdUILog.TabIndex = 0;
            this.cmdUILog.Text = "Show Message";
            this.ttpCommand.SetToolTip(this.cmdUILog, "Add text to the game log.");
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
            this.ttpCommand.SetToolTip(this.cmdUILogSplit, "Add a horizontal bar to the game log, to split text.");
            this.cmdUILogSplit.UseVisualStyleBackColor = true;
            this.cmdUILogSplit.Click += new System.EventHandler(this.cmdUILogSplit_Click);
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(this.cmdTextSetContext);
            groupBox8.Controls.Add(this.cmdTextSetVariable);
            groupBox8.Location = new System.Drawing.Point(464, 248);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new System.Drawing.Size(208, 88);
            groupBox8.TabIndex = 7;
            groupBox8.TabStop = false;
            groupBox8.Text = "Grammar Engine";
            // 
            // cmdTextSetContext
            // 
            this.cmdTextSetContext.Location = new System.Drawing.Point(16, 24);
            this.cmdTextSetContext.Name = "cmdTextSetContext";
            this.cmdTextSetContext.Size = new System.Drawing.Size(176, 24);
            this.cmdTextSetContext.TabIndex = 0;
            this.cmdTextSetContext.Text = "Set Grammar Context";
            this.ttpCommand.SetToolTip(this.cmdTextSetContext, "Bind a Creature object to a name, for use in grammar tags. Check\r\nthe documentati" +
        "on pages on the grammar engine for details.");
            this.cmdTextSetContext.UseVisualStyleBackColor = true;
            this.cmdTextSetContext.Click += new System.EventHandler(this.cmdTextSetContext_Click);
            // 
            // cmdTextSetVariable
            // 
            this.cmdTextSetVariable.Location = new System.Drawing.Point(16, 48);
            this.cmdTextSetVariable.Name = "cmdTextSetVariable";
            this.cmdTextSetVariable.Size = new System.Drawing.Size(176, 24);
            this.cmdTextSetVariable.TabIndex = 1;
            this.cmdTextSetVariable.Text = "Set Grammar Variable";
            this.ttpCommand.SetToolTip(this.cmdTextSetVariable, "Bind some text to a variable, allowing it to be replaced in game text.\r\nCheck the" +
        " documentation pages on the grammar engine for details.");
            this.cmdTextSetVariable.UseVisualStyleBackColor = true;
            this.cmdTextSetVariable.Click += new System.EventHandler(this.cmdTextSetVariable_Click);
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(this.cmdSaveDialog);
            groupBox9.Controls.Add(this.cmdSaveCheckpoint);
            groupBox9.Location = new System.Drawing.Point(464, 16);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new System.Drawing.Size(208, 88);
            groupBox9.TabIndex = 5;
            groupBox9.TabStop = false;
            groupBox9.Text = "Save Data";
            // 
            // cmdSaveDialog
            // 
            this.cmdSaveDialog.Location = new System.Drawing.Point(16, 24);
            this.cmdSaveDialog.Name = "cmdSaveDialog";
            this.cmdSaveDialog.Size = new System.Drawing.Size(176, 24);
            this.cmdSaveDialog.TabIndex = 0;
            this.cmdSaveDialog.Text = "Open Save Menu";
            this.ttpCommand.SetToolTip(this.cmdSaveDialog, "Display the save menu, allowing players to save their game if\r\nthey wish to. Note" +
        " that if the player makes a manual save, the\r\nlast checkpoint (if any) is delete" +
        "d.");
            this.cmdSaveDialog.UseVisualStyleBackColor = true;
            this.cmdSaveDialog.Click += new System.EventHandler(this.cmdSaveDialog_Click);
            // 
            // cmdSaveCheckpoint
            // 
            this.cmdSaveCheckpoint.Location = new System.Drawing.Point(16, 48);
            this.cmdSaveCheckpoint.Name = "cmdSaveCheckpoint";
            this.cmdSaveCheckpoint.Size = new System.Drawing.Size(176, 24);
            this.cmdSaveCheckpoint.TabIndex = 1;
            this.cmdSaveCheckpoint.Text = "Save Checkpoint";
            this.ttpCommand.SetToolTip(this.cmdSaveCheckpoint, "Take a checkpoint (auto-save), allowing the player to quickly\r\nreturn to the curr" +
        "ent point in the scene if they exit the game.");
            this.cmdSaveCheckpoint.UseVisualStyleBackColor = true;
            this.cmdSaveCheckpoint.Click += new System.EventHandler(this.cmdSaveCheckpoint_Click);
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(this.cmdCombatApplyBuff);
            groupBox10.Controls.Add(this.cmdCombatPreysense);
            groupBox10.Controls.Add(this.cmdCombatSetGrappled);
            groupBox10.Controls.Add(this.cmdCombatSetVored);
            groupBox10.Controls.Add(this.cmdCombatStop);
            groupBox10.Controls.Add(this.cmdCombatBegin);
            groupBox10.Location = new System.Drawing.Point(240, 344);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new System.Drawing.Size(208, 184);
            groupBox10.TabIndex = 4;
            groupBox10.TabStop = false;
            groupBox10.Text = "Combat";
            // 
            // cmdCombatApplyBuff
            // 
            this.cmdCombatApplyBuff.Location = new System.Drawing.Point(16, 72);
            this.cmdCombatApplyBuff.Name = "cmdCombatApplyBuff";
            this.cmdCombatApplyBuff.Size = new System.Drawing.Size(176, 24);
            this.cmdCombatApplyBuff.TabIndex = 2;
            this.cmdCombatApplyBuff.Text = "Apply Buff";
            this.ttpCommand.SetToolTip(this.cmdCombatApplyBuff, "Apply a (de)buff to the player character, or a combat participant.\r\nIf the player" +
        " is not currently in combat, buffs will be queued up\r\nfor later and applied when" +
        " they next enter combat.");
            this.cmdCombatApplyBuff.UseVisualStyleBackColor = true;
            this.cmdCombatApplyBuff.Click += new System.EventHandler(this.cmdCombatApplyBuff_Click);
            // 
            // cmdCombatPreysense
            // 
            this.cmdCombatPreysense.Location = new System.Drawing.Point(16, 144);
            this.cmdCombatPreysense.Name = "cmdCombatPreysense";
            this.cmdCombatPreysense.Size = new System.Drawing.Size(176, 24);
            this.cmdCombatPreysense.TabIndex = 5;
            this.cmdCombatPreysense.Text = "Show Preysense";
            this.ttpCommand.SetToolTip(this.cmdCombatPreysense, "Show a Preysense warning (a content warning) describing some\r\nupcoming vore-relat" +
        "ed content. Note that Preysense warnings are\r\nonly shown if the relevant option " +
        "in the Options menu is enabled.");
            this.cmdCombatPreysense.UseVisualStyleBackColor = true;
            this.cmdCombatPreysense.Click += new System.EventHandler(this.cmdCombatPreysense_Click);
            // 
            // cmdCombatSetGrappled
            // 
            this.cmdCombatSetGrappled.Location = new System.Drawing.Point(16, 120);
            this.cmdCombatSetGrappled.Name = "cmdCombatSetGrappled";
            this.cmdCombatSetGrappled.Size = new System.Drawing.Size(176, 24);
            this.cmdCombatSetGrappled.TabIndex = 4;
            this.cmdCombatSetGrappled.Text = "Set Participants Grappling";
            this.ttpCommand.SetToolTip(this.cmdCombatSetGrappled, "Force two characters to start grappling with each other, or cancel\r\nan ongoing gr" +
        "apple.");
            this.cmdCombatSetGrappled.UseVisualStyleBackColor = true;
            this.cmdCombatSetGrappled.Click += new System.EventHandler(this.cmdCombatSetGrappled_Click);
            // 
            // cmdCombatSetVored
            // 
            this.cmdCombatSetVored.Location = new System.Drawing.Point(16, 96);
            this.cmdCombatSetVored.Name = "cmdCombatSetVored";
            this.cmdCombatSetVored.Size = new System.Drawing.Size(176, 24);
            this.cmdCombatSetVored.TabIndex = 3;
            this.cmdCombatSetVored.Text = "Set Participants Swallowed";
            this.ttpCommand.SetToolTip(this.cmdCombatSetVored, "Force a character to have swallowed another character, or undo\r\nsuch a situation." +
        "");
            this.cmdCombatSetVored.UseVisualStyleBackColor = true;
            this.cmdCombatSetVored.Click += new System.EventHandler(this.cmdCombatSetVored_Click);
            // 
            // cmdCombatStop
            // 
            this.cmdCombatStop.Location = new System.Drawing.Point(16, 48);
            this.cmdCombatStop.Name = "cmdCombatStop";
            this.cmdCombatStop.Size = new System.Drawing.Size(176, 24);
            this.cmdCombatStop.TabIndex = 1;
            this.cmdCombatStop.Text = "Stop Combat";
            this.ttpCommand.SetToolTip(this.cmdCombatStop, "Exit the currently active combat session. Meant to be used from\r\nwithin a combat " +
        "event.");
            this.cmdCombatStop.UseVisualStyleBackColor = true;
            this.cmdCombatStop.Click += new System.EventHandler(this.cmdCombatStop_Click);
            // 
            // cmdCombatBegin
            // 
            this.cmdCombatBegin.Location = new System.Drawing.Point(16, 24);
            this.cmdCombatBegin.Name = "cmdCombatBegin";
            this.cmdCombatBegin.Size = new System.Drawing.Size(176, 24);
            this.cmdCombatBegin.TabIndex = 0;
            this.cmdCombatBegin.Text = "Begin Combat";
            this.ttpCommand.SetToolTip(this.cmdCombatBegin, "Start a combat session with configured participants. You may\r\nadd scripts to cert" +
        "ain events, to customize combat behavior.");
            this.cmdCombatBegin.UseVisualStyleBackColor = true;
            this.cmdCombatBegin.Click += new System.EventHandler(this.cmdCombatBegin_Click);
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(this.cmdJournalUpdate);
            groupBox4.Controls.Add(this.cmdJournalClose);
            groupBox4.Location = new System.Drawing.Point(464, 352);
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
            this.cmdJournalUpdate.TabIndex = 0;
            this.cmdJournalUpdate.Text = "Add/Update Quest";
            this.ttpCommand.SetToolTip(this.cmdJournalUpdate, "Change the stage of a quest in the player\'s journal. If the quest is\r\nnot yet in " +
        "the journal, it will be added.");
            this.cmdJournalUpdate.UseVisualStyleBackColor = true;
            this.cmdJournalUpdate.Click += new System.EventHandler(this.cmdJournalUpdate_Click);
            // 
            // cmdJournalClose
            // 
            this.cmdJournalClose.Location = new System.Drawing.Point(16, 48);
            this.cmdJournalClose.Name = "cmdJournalClose";
            this.cmdJournalClose.Size = new System.Drawing.Size(176, 24);
            this.cmdJournalClose.TabIndex = 1;
            this.cmdJournalClose.Text = "Remove Quest";
            this.ttpCommand.SetToolTip(this.cmdJournalClose, "Remove a quest from the player\'s journal, if it is there.");
            this.cmdJournalClose.UseVisualStyleBackColor = true;
            this.cmdJournalClose.Click += new System.EventHandler(this.cmdJournalClose_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Image = global::Finmer.Editor.Properties.Resources.cross;
            this.cmdCancel.Location = new System.Drawing.Point(536, 544);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(136, 32);
            this.cmdCancel.TabIndex = 10;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // ttpCommand
            // 
            this.ttpCommand.AutomaticDelay = 250;
            this.ttpCommand.AutoPopDelay = 60000;
            this.ttpCommand.InitialDelay = 250;
            this.ttpCommand.ReshowDelay = 50;
            this.ttpCommand.Popup += new System.Windows.Forms.PopupEventHandler(this.ttpCommand_Popup);
            // 
            // FormVisualScriptCommandPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(689, 593);
            this.Controls.Add(groupBox10);
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
            groupBox5.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox9.ResumeLayout(false);
            groupBox10.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
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
        private System.Windows.Forms.Button cmdCombatStop;
        private System.Windows.Forms.Button cmdSceneEndGame;
        private System.Windows.Forms.Button cmdSceneShop;
        private System.Windows.Forms.Button cmdCombatBegin;
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
        private System.Windows.Forms.Button cmdUISetLocation;
        private System.Windows.Forms.Button cmdUISetInstruction;
        private System.Windows.Forms.Button cmdUIClearLog;
        private System.Windows.Forms.Button cmdTextSetContext;
        private System.Windows.Forms.Button cmdTextSetVariable;
        private System.Windows.Forms.Button cmdSaveDialog;
        private System.Windows.Forms.Button cmdSaveCheckpoint;
        private System.Windows.Forms.Button cmdCombatSetGrappled;
        private System.Windows.Forms.Button cmdCombatSetVored;
        private System.Windows.Forms.Button cmdCombatPreysense;
        private System.Windows.Forms.Button cmdCombatApplyBuff;
        private System.Windows.Forms.ToolTip ttpCommand;
    }
}