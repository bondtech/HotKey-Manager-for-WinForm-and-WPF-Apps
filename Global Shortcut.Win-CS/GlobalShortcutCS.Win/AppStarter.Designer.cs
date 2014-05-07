namespace GlobalShortcutCS.Win
{
    partial class AppStarter
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnToggleKeys = new System.Windows.Forms.Button();
            this.btnEnumerate = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkCustomEnabled = new System.Windows.Forms.CheckBox();
            this.btnModify = new System.Windows.Forms.Button();
            this.lblShortcut = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.optmessage = new System.Windows.Forms.RadioButton();
            this.optVisibility = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.chkPowerShell = new System.Windows.Forms.CheckBox();
            this.chkCharMap = new System.Windows.Forms.CheckBox();
            this.chkRegEdit = new System.Windows.Forms.CheckBox();
            this.chkIExplore = new System.Windows.Forms.CheckBox();
            this.chkCmd = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.chkNewLocal = new System.Windows.Forms.CheckBox();
            this.chkDisableLogging = new System.Windows.Forms.CheckBox();
            this.chkClearLog = new System.Windows.Forms.CheckBox();
            this.chkCopyClipboard = new System.Windows.Forms.CheckBox();
            this.chkNewHotKey = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chkUninstall = new System.Windows.Forms.CheckBox();
            this.chkTaskManager = new System.Windows.Forms.CheckBox();
            this.chkCalculator = new System.Windows.Forms.CheckBox();
            this.chkWordpad = new System.Windows.Forms.CheckBox();
            this.chkNotepad = new System.Windows.Forms.CheckBox();
            this.btnAddHotKey = new System.Windows.Forms.Button();
            this.tipMain = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnToggleKeys);
            this.groupBox1.Controls.Add(this.btnEnumerate);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 223);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Custom Shortcut.";
            // 
            // btnToggleKeys
            // 
            this.btnToggleKeys.FlatAppearance.BorderSize = 0;
            this.btnToggleKeys.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleKeys.Location = new System.Drawing.Point(117, 185);
            this.btnToggleKeys.Name = "btnToggleKeys";
            this.btnToggleKeys.Size = new System.Drawing.Size(82, 23);
            this.btnToggleKeys.TabIndex = 5;
            this.btnToggleKeys.Text = "Disable Keys";
            this.tipMain.SetToolTip(this.btnToggleKeys, "Toggles disabling the vowel keys on the keyboard.");
            this.btnToggleKeys.UseVisualStyleBackColor = true;
            this.btnToggleKeys.Click += new System.EventHandler(this.btnToggle_Click);
            // 
            // btnEnumerate
            // 
            this.btnEnumerate.FlatAppearance.BorderSize = 0;
            this.btnEnumerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnumerate.Location = new System.Drawing.Point(9, 185);
            this.btnEnumerate.Name = "btnEnumerate";
            this.btnEnumerate.Size = new System.Drawing.Size(103, 23);
            this.btnEnumerate.TabIndex = 4;
            this.btnEnumerate.Text = "Show all &HotKeys";
            this.tipMain.SetToolTip(this.btnEnumerate, "Shows all hotkeys associated with this form.");
            this.btnEnumerate.UseVisualStyleBackColor = true;
            this.btnEnumerate.Click += new System.EventHandler(this.btnEnumerate_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkCustomEnabled);
            this.groupBox3.Controls.Add(this.btnModify);
            this.groupBox3.Controls.Add(this.lblShortcut);
            this.groupBox3.Location = new System.Drawing.Point(9, 93);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(188, 86);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Custom Shortcut";
            // 
            // chkCustomEnabled
            // 
            this.chkCustomEnabled.AutoSize = true;
            this.chkCustomEnabled.Checked = true;
            this.chkCustomEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCustomEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkCustomEnabled.Location = new System.Drawing.Point(108, 57);
            this.chkCustomEnabled.Name = "chkCustomEnabled";
            this.chkCustomEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkCustomEnabled.TabIndex = 2;
            this.chkCustomEnabled.Text = "Enabled";
            this.tipMain.SetToolTip(this.chkCustomEnabled, "Toggles functionality of the custom shortcut.");
            this.chkCustomEnabled.UseVisualStyleBackColor = true;
            this.chkCustomEnabled.CheckedChanged += new System.EventHandler(this.chkCustomEnabled_CheckedChanged);
            // 
            // btnModify
            // 
            this.btnModify.FlatAppearance.BorderSize = 0;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModify.Location = new System.Drawing.Point(8, 51);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(78, 29);
            this.btnModify.TabIndex = 1;
            this.btnModify.Text = "&Modify";
            this.tipMain.SetToolTip(this.btnModify, "Allows you to modify the custom shortcut above.");
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // lblShortcut
            // 
            this.lblShortcut.AutoSize = true;
            this.lblShortcut.Location = new System.Drawing.Point(36, 26);
            this.lblShortcut.Name = "lblShortcut";
            this.lblShortcut.Size = new System.Drawing.Size(131, 13);
            this.lblShortcut.TabIndex = 0;
            this.lblShortcut.Text = "Shift + Control + Alt + T";
            this.tipMain.SetToolTip(this.lblShortcut, "Pressing this shortcut on the keyboard performs the selected action in the radio " +
        "buttons above.");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.optmessage);
            this.groupBox2.Controls.Add(this.optVisibility);
            this.groupBox2.Location = new System.Drawing.Point(9, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 68);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Shortcut Action";
            // 
            // optmessage
            // 
            this.optmessage.AutoSize = true;
            this.optmessage.Location = new System.Drawing.Point(8, 42);
            this.optmessage.Name = "optmessage";
            this.optmessage.Size = new System.Drawing.Size(121, 17);
            this.optmessage.TabIndex = 1;
            this.optmessage.TabStop = true;
            this.optmessage.Text = "Display a message.";
            this.tipMain.SetToolTip(this.optmessage, "Specifies that a message should be shown when this hotkey is pressed.");
            this.optmessage.UseVisualStyleBackColor = true;
            // 
            // optVisibility
            // 
            this.optVisibility.AutoSize = true;
            this.optVisibility.Location = new System.Drawing.Point(8, 19);
            this.optVisibility.Name = "optVisibility";
            this.optVisibility.Size = new System.Drawing.Size(138, 17);
            this.optVisibility.TabIndex = 0;
            this.optVisibility.TabStop = true;
            this.optVisibility.Text = "Toggle Form Visibility.";
            this.tipMain.SetToolTip(this.optVisibility, "Specifies that the visibility of this form should be toggled when the hotkey is p" +
        "ressed.");
            this.optVisibility.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.txtLog);
            this.groupBox4.Location = new System.Drawing.Point(215, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(554, 223);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Log";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.ForeColor = System.Drawing.Color.White;
            this.txtLog.Location = new System.Drawing.Point(3, 18);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(548, 202);
            this.txtLog.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.groupBox8);
            this.groupBox5.Controls.Add(this.groupBox7);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Location = new System.Drawing.Point(3, 231);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(763, 174);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Application Shortcuts.";
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox8.Controls.Add(this.chkPowerShell);
            this.groupBox8.Controls.Add(this.chkCharMap);
            this.groupBox8.Controls.Add(this.chkRegEdit);
            this.groupBox8.Controls.Add(this.chkIExplore);
            this.groupBox8.Controls.Add(this.chkCmd);
            this.groupBox8.Location = new System.Drawing.Point(479, 19);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(278, 149);
            this.groupBox8.TabIndex = 2;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Chord";
            // 
            // chkPowerShell
            // 
            this.chkPowerShell.AutoSize = true;
            this.chkPowerShell.Checked = true;
            this.chkPowerShell.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPowerShell.FlatAppearance.BorderSize = 0;
            this.chkPowerShell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPowerShell.Location = new System.Drawing.Point(6, 38);
            this.chkPowerShell.Name = "chkPowerShell";
            this.chkPowerShell.Size = new System.Drawing.Size(219, 17);
            this.chkPowerShell.TabIndex = 1;
            this.chkPowerShell.Text = "Start PowerShell - Control + P , Key.D1";
            this.tipMain.SetToolTip(this.chkPowerShell, "Starts a new instance of Powershell");
            this.chkPowerShell.UseVisualStyleBackColor = true;
            this.chkPowerShell.CheckedChanged += new System.EventHandler(this.chkPowerShell_CheckedChanged);
            // 
            // chkCharMap
            // 
            this.chkCharMap.AutoSize = true;
            this.chkCharMap.Checked = true;
            this.chkCharMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCharMap.FlatAppearance.BorderSize = 0;
            this.chkCharMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkCharMap.Location = new System.Drawing.Point(6, 124);
            this.chkCharMap.Name = "chkCharMap";
            this.chkCharMap.Size = new System.Drawing.Size(216, 17);
            this.chkCharMap.TabIndex = 4;
            this.chkCharMap.Text = "Start Character Map - Shift + C, Key.M";
            this.tipMain.SetToolTip(this.chkCharMap, "Starts Character Map");
            this.chkCharMap.UseVisualStyleBackColor = true;
            this.chkCharMap.CheckedChanged += new System.EventHandler(this.chkCharMap_CheckedChanged);
            // 
            // chkRegEdit
            // 
            this.chkRegEdit.AutoSize = true;
            this.chkRegEdit.Checked = true;
            this.chkRegEdit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRegEdit.FlatAppearance.BorderSize = 0;
            this.chkRegEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRegEdit.Location = new System.Drawing.Point(6, 95);
            this.chkRegEdit.Name = "chkRegEdit";
            this.chkRegEdit.Size = new System.Drawing.Size(255, 17);
            this.chkRegEdit.TabIndex = 3;
            this.chkRegEdit.Text = "Start Registry Editor - Control + Alt + R, Key.E";
            this.tipMain.SetToolTip(this.chkRegEdit, "Starts Registry Editor");
            this.chkRegEdit.UseVisualStyleBackColor = true;
            this.chkRegEdit.CheckedChanged += new System.EventHandler(this.chkRegEdit_CheckedChanged);
            // 
            // chkIExplore
            // 
            this.chkIExplore.AutoSize = true;
            this.chkIExplore.Checked = true;
            this.chkIExplore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIExplore.FlatAppearance.BorderSize = 0;
            this.chkIExplore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkIExplore.Location = new System.Drawing.Point(6, 66);
            this.chkIExplore.Name = "chkIExplore";
            this.chkIExplore.Size = new System.Drawing.Size(267, 17);
            this.chkIExplore.TabIndex = 2;
            this.chkIExplore.Text = "Start Internet Explorer - Control + I, Control + E\r\n";
            this.tipMain.SetToolTip(this.chkIExplore, "Starts Internet Explorer");
            this.chkIExplore.UseVisualStyleBackColor = true;
            this.chkIExplore.CheckedChanged += new System.EventHandler(this.chkIExplore_CheckedChanged);
            // 
            // chkCmd
            // 
            this.chkCmd.AutoSize = true;
            this.chkCmd.Checked = true;
            this.chkCmd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCmd.FlatAppearance.BorderSize = 0;
            this.chkCmd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkCmd.Location = new System.Drawing.Point(6, 13);
            this.chkCmd.Name = "chkCmd";
            this.chkCmd.Size = new System.Drawing.Size(224, 17);
            this.chkCmd.TabIndex = 0;
            this.chkCmd.Text = "Start Command Prompt - Alt+ C, Alt + P";
            this.tipMain.SetToolTip(this.chkCmd, "Starts a new instance of Command Prompt");
            this.chkCmd.UseVisualStyleBackColor = true;
            this.chkCmd.CheckedChanged += new System.EventHandler(this.chkCmd_CheckedChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox7.Controls.Add(this.chkNewLocal);
            this.groupBox7.Controls.Add(this.chkDisableLogging);
            this.groupBox7.Controls.Add(this.chkClearLog);
            this.groupBox7.Controls.Add(this.chkCopyClipboard);
            this.groupBox7.Controls.Add(this.chkNewHotKey);
            this.groupBox7.Location = new System.Drawing.Point(260, 19);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(213, 149);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Local";
            // 
            // chkNewLocal
            // 
            this.chkNewLocal.AutoSize = true;
            this.chkNewLocal.Checked = true;
            this.chkNewLocal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNewLocal.FlatAppearance.BorderSize = 0;
            this.chkNewLocal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkNewLocal.Location = new System.Drawing.Point(5, 38);
            this.chkNewLocal.Name = "chkNewLocal";
            this.chkNewLocal.Size = new System.Drawing.Size(202, 17);
            this.chkNewLocal.TabIndex = 1;
            this.chkNewLocal.Text = "Add new Local Hotkey. Key.A + Ctrl";
            this.tipMain.SetToolTip(this.chkNewLocal, "Shows a dialog for adding new LocalHotKeys");
            this.chkNewLocal.UseVisualStyleBackColor = true;
            this.chkNewLocal.CheckedChanged += new System.EventHandler(this.chkNewLocal_CheckedChanged);
            // 
            // chkDisableLogging
            // 
            this.chkDisableLogging.AutoSize = true;
            this.chkDisableLogging.Checked = true;
            this.chkDisableLogging.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisableLogging.FlatAppearance.BorderSize = 0;
            this.chkDisableLogging.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkDisableLogging.Location = new System.Drawing.Point(5, 124);
            this.chkDisableLogging.Name = "chkDisableLogging";
            this.chkDisableLogging.Size = new System.Drawing.Size(193, 17);
            this.chkDisableLogging.TabIndex = 4;
            this.chkDisableLogging.Text = "Disable Logging - Key.D + Alt key";
            this.tipMain.SetToolTip(this.chkDisableLogging, "clears the content of the log textbox");
            this.chkDisableLogging.UseVisualStyleBackColor = true;
            this.chkDisableLogging.CheckedChanged += new System.EventHandler(this.chkDisableLogging_CheckedChanged);
            // 
            // chkClearLog
            // 
            this.chkClearLog.AutoSize = true;
            this.chkClearLog.Checked = true;
            this.chkClearLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClearLog.FlatAppearance.BorderSize = 0;
            this.chkClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkClearLog.Location = new System.Drawing.Point(5, 95);
            this.chkClearLog.Name = "chkClearLog";
            this.chkClearLog.Size = new System.Drawing.Size(136, 17);
            this.chkClearLog.TabIndex = 3;
            this.chkClearLog.Text = "Clear Log - Key.Escape";
            this.tipMain.SetToolTip(this.chkClearLog, "clears the content of the log textbox");
            this.chkClearLog.UseVisualStyleBackColor = true;
            this.chkClearLog.CheckedChanged += new System.EventHandler(this.chkClearLog_CheckedChanged);
            // 
            // chkCopyClipboard
            // 
            this.chkCopyClipboard.AutoSize = true;
            this.chkCopyClipboard.Checked = true;
            this.chkCopyClipboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCopyClipboard.FlatAppearance.BorderSize = 0;
            this.chkCopyClipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkCopyClipboard.Location = new System.Drawing.Point(5, 66);
            this.chkCopyClipboard.Name = "chkCopyClipboard";
            this.chkCopyClipboard.Size = new System.Drawing.Size(176, 17);
            this.chkCopyClipboard.TabIndex = 2;
            this.chkCopyClipboard.Text = "Copy Log to Clipboard - Key.C";
            this.tipMain.SetToolTip(this.chkCopyClipboard, "Copies the content of the log textbox to the clipboard");
            this.chkCopyClipboard.UseVisualStyleBackColor = true;
            this.chkCopyClipboard.CheckedChanged += new System.EventHandler(this.chkCopyClipboard_CheckedChanged);
            // 
            // chkNewHotKey
            // 
            this.chkNewHotKey.AutoSize = true;
            this.chkNewHotKey.Checked = true;
            this.chkNewHotKey.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNewHotKey.FlatAppearance.BorderSize = 0;
            this.chkNewHotKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkNewHotKey.Location = new System.Drawing.Point(5, 13);
            this.chkNewHotKey.Name = "chkNewHotKey";
            this.chkNewHotKey.Size = new System.Drawing.Size(178, 17);
            this.chkNewHotKey.TabIndex = 0;
            this.chkNewHotKey.Text = "Add new Global Hotkey. Key.A";
            this.tipMain.SetToolTip(this.chkNewHotKey, "Shows a dialog for adding new Global Hotkeys");
            this.chkNewHotKey.UseVisualStyleBackColor = true;
            this.chkNewHotKey.CheckedChanged += new System.EventHandler(this.chkNewHotKey_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.Controls.Add(this.chkUninstall);
            this.groupBox6.Controls.Add(this.chkTaskManager);
            this.groupBox6.Controls.Add(this.chkCalculator);
            this.groupBox6.Controls.Add(this.chkWordpad);
            this.groupBox6.Controls.Add(this.chkNotepad);
            this.groupBox6.Controls.Add(this.btnAddHotKey);
            this.groupBox6.Location = new System.Drawing.Point(9, 19);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(245, 149);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Global";
            // 
            // chkUninstall
            // 
            this.chkUninstall.AutoSize = true;
            this.chkUninstall.Checked = true;
            this.chkUninstall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUninstall.FlatAppearance.BorderSize = 0;
            this.chkUninstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkUninstall.Location = new System.Drawing.Point(14, 124);
            this.chkUninstall.Name = "chkUninstall";
            this.chkUninstall.Size = new System.Drawing.Size(224, 17);
            this.chkUninstall.TabIndex = 15;
            this.chkUninstall.Text = "Uninstall a program - Control + Alt + U";
            this.tipMain.SetToolTip(this.chkUninstall, "Allows you uninstall a program");
            this.chkUninstall.UseVisualStyleBackColor = true;
            this.chkUninstall.CheckedChanged += new System.EventHandler(this.chkUninstall_CheckedChanged);
            // 
            // chkTaskManager
            // 
            this.chkTaskManager.AutoSize = true;
            this.chkTaskManager.Checked = true;
            this.chkTaskManager.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTaskManager.FlatAppearance.BorderSize = 0;
            this.chkTaskManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkTaskManager.Location = new System.Drawing.Point(14, 95);
            this.chkTaskManager.Name = "chkTaskManager";
            this.chkTaskManager.Size = new System.Drawing.Size(224, 17);
            this.chkTaskManager.TabIndex = 14;
            this.chkTaskManager.Text = "Start TaskManager - Shift + Control + T";
            this.tipMain.SetToolTip(this.chkTaskManager, "Starts a new instance of Taskmanager.");
            this.chkTaskManager.UseVisualStyleBackColor = true;
            this.chkTaskManager.CheckedChanged += new System.EventHandler(this.chkTaskManager_CheckedChanged);
            // 
            // chkCalculator
            // 
            this.chkCalculator.AutoSize = true;
            this.chkCalculator.Checked = true;
            this.chkCalculator.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCalculator.FlatAppearance.BorderSize = 0;
            this.chkCalculator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkCalculator.Location = new System.Drawing.Point(14, 66);
            this.chkCalculator.Name = "chkCalculator";
            this.chkCalculator.Size = new System.Drawing.Size(203, 17);
            this.chkCalculator.TabIndex = 13;
            this.chkCalculator.Text = "Start Calculator - Control + Alt +  C";
            this.tipMain.SetToolTip(this.chkCalculator, "Starts a new Calculator instance.");
            this.chkCalculator.UseVisualStyleBackColor = true;
            this.chkCalculator.CheckedChanged += new System.EventHandler(this.chkCalculator_CheckedChanged);
            // 
            // chkWordpad
            // 
            this.chkWordpad.AutoSize = true;
            this.chkWordpad.Checked = true;
            this.chkWordpad.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWordpad.FlatAppearance.BorderSize = 0;
            this.chkWordpad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkWordpad.Location = new System.Drawing.Point(14, 38);
            this.chkWordpad.Name = "chkWordpad";
            this.chkWordpad.Size = new System.Drawing.Size(211, 17);
            this.chkWordpad.TabIndex = 12;
            this.chkWordpad.Text = "Start Wordpad - Shift + Control + W";
            this.tipMain.SetToolTip(this.chkWordpad, "Starts a new instance of Wordpad.");
            this.chkWordpad.UseVisualStyleBackColor = true;
            this.chkWordpad.CheckedChanged += new System.EventHandler(this.chkWordpad_CheckedChanged);
            // 
            // chkNotepad
            // 
            this.chkNotepad.AutoSize = true;
            this.chkNotepad.Checked = true;
            this.chkNotepad.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNotepad.FlatAppearance.BorderSize = 0;
            this.chkNotepad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkNotepad.Location = new System.Drawing.Point(14, 13);
            this.chkNotepad.Name = "chkNotepad";
            this.chkNotepad.Size = new System.Drawing.Size(204, 17);
            this.chkNotepad.TabIndex = 11;
            this.chkNotepad.Text = "Start Notepad - Shift + Control + N";
            this.tipMain.SetToolTip(this.chkNotepad, "Starts a new instance of notepad");
            this.chkNotepad.UseVisualStyleBackColor = true;
            this.chkNotepad.CheckedChanged += new System.EventHandler(this.chkNotepad_CheckedChanged);
            // 
            // btnAddHotKey
            // 
            this.btnAddHotKey.FlatAppearance.BorderSize = 0;
            this.btnAddHotKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddHotKey.Location = new System.Drawing.Point(216, 11);
            this.btnAddHotKey.Name = "btnAddHotKey";
            this.btnAddHotKey.Size = new System.Drawing.Size(23, 23);
            this.btnAddHotKey.TabIndex = 4;
            this.btnAddHotKey.Text = "+";
            this.tipMain.SetToolTip(this.btnAddHotKey, "Add a new Global Shortcut.");
            this.btnAddHotKey.UseVisualStyleBackColor = true;
            // 
            // AppStarter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(773, 417);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Name = "AppStarter";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Global Shortcut Example.";
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.Button btnModify;
        internal System.Windows.Forms.Label lblShortcut;
        internal System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.RadioButton optmessage;
        internal System.Windows.Forms.RadioButton optVisibility;
        internal System.Windows.Forms.GroupBox groupBox4;
        internal System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnAddHotKey;
        private System.Windows.Forms.ToolTip tipMain;
        private System.Windows.Forms.CheckBox chkDisableLogging;
        private System.Windows.Forms.CheckBox chkClearLog;
        private System.Windows.Forms.CheckBox chkCopyClipboard;
        private System.Windows.Forms.CheckBox chkNewHotKey;
        private System.Windows.Forms.CheckBox chkUninstall;
        private System.Windows.Forms.CheckBox chkTaskManager;
        private System.Windows.Forms.CheckBox chkCalculator;
        private System.Windows.Forms.CheckBox chkWordpad;
        private System.Windows.Forms.CheckBox chkNotepad;
        private System.Windows.Forms.CheckBox chkCustomEnabled;
        private System.Windows.Forms.CheckBox chkNewLocal;
        private System.Windows.Forms.Button btnEnumerate;
        private System.Windows.Forms.Button btnToggleKeys;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox chkPowerShell;
        private System.Windows.Forms.CheckBox chkCharMap;
        private System.Windows.Forms.CheckBox chkRegEdit;
        private System.Windows.Forms.CheckBox chkIExplore;
        private System.Windows.Forms.CheckBox chkCmd;


    }
}

