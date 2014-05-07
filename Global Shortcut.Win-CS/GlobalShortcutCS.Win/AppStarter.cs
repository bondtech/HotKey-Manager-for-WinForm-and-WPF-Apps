using System;
using System.Collections;
using System.Collections.Generic;

using System.Windows.Forms;
using BondTech.HotkeyManagement.Win;
using Microsoft.VisualBasic; // Add a reference to Microsoft.VisualBasic.

namespace GlobalShortcutCS.Win
{
    public partial class AppStarter : Form
    {
        internal HotKeyManager MyHotKeyManager;
        private bool LogEvent = true;

        #region **HotKeys
        GlobalHotKey ghkNotepad = new GlobalHotKey("ghkNotepad", Modifiers.Control | Modifiers.Shift, Keys.N);
        GlobalHotKey ghkWordpad = new GlobalHotKey("ghkWordpad", Modifiers.Control | Modifiers.Shift, Keys.W);
        GlobalHotKey ghkCalc = new GlobalHotKey("ghkCalc", Modifiers.Control | Modifiers.Alt, Keys.C);
        GlobalHotKey ghkTaskMan = new GlobalHotKey("ghkTaskMan", Modifiers.Control | Modifiers.Shift, Keys.T);
        GlobalHotKey ghkUninstall = new GlobalHotKey("ghkUninstall", Modifiers.Control | Modifiers.Alt, Keys.U);
        GlobalHotKey ghkCustom = new GlobalHotKey("ghkCustom", Modifiers.Shift | Modifiers.Control | Modifiers.Alt, Keys.T);

        LocalHotKey lhkNewHotkey = new LocalHotKey("lhkNewHotKey", Keys.A);
        LocalHotKey lhkNewLocalKey = new LocalHotKey("lhkNewLocalKey", Modifiers.Control, Keys.A);
        LocalHotKey lhkCopyLog = new LocalHotKey("lhkCopyLog", Keys.C);
        LocalHotKey lhkClearLog = new LocalHotKey("lhkClearLog", Keys.Escape);
        LocalHotKey lhkDisableLog = new LocalHotKey("lhkDisableLog", Modifiers.Alt, Keys.D);

        ChordHotKey chotCmd = new ChordHotKey("chotCmd", Modifiers.Alt, Keys.C, Modifiers.Alt, Keys.P);
        ChordHotKey chotPowerShell = new ChordHotKey("chotPowerShell", Modifiers.Control, Keys.P, Modifiers.None, Keys.D1);
        ChordHotKey chotIExplore = new ChordHotKey("chotIExplore", Modifiers.Control, Keys.I, Modifiers.Control, Keys.E);
        ChordHotKey chotRegEdit = new ChordHotKey("chotRegEdit", Modifiers.Alt | Modifiers.Control, Keys.R, Modifiers.None, Keys.E);
        ChordHotKey chotCharMap = new ChordHotKey("chotCharMap", Modifiers.Shift, Keys.C, Modifiers.None, Keys.M);
        #endregion

        public AppStarter()
        {
            InitializeComponent();
            MyHotKeyManager = new HotKeyManager(this);
            MyHotKeyManager.GlobalHotKeyPressed += new GlobalHotKeyEventHandler(MyHotKeyManager_GlobalHotKeyPressed);
            MyHotKeyManager.LocalHotKeyPressed += new LocalHotKeyEventHandler(MyHotKeyManager_LocalHotKeyPressed);
            MyHotKeyManager.ChordStarted += new PreChordHotkeyEventHandler(MyHotKeyManager_ChordStarted);
            MyHotKeyManager.ChordPressed += new ChordHotKeyEventHandler(MyHotKeyManager_ChordPressed);
            btnAddHotKey.Click += delegate { AddNewHotKey(); };
            RegisterHotKeys();
            MyHotKeyManager.DisableOnManagerFormInactive = true;
        }

        void MyHotKeyManager_ChordStarted(object sender, PreChordHotKeyEventArgs e)
        {
            LogEvents("Chord Started... (" + e.Info() + ") waiting for the second key of chord.");
        }

        void MyHotKeyManager_ChordPressed(object sender, ChordHotKeyEventArgs e)
        {
            System.Diagnostics.Process.Start((e.HotKey.Tag as string));
            LogEvents(e.HotKey);
        }

        void MyHotKeyManager_LocalHotKeyPressed(object sender, LocalHotKeyEventArgs e)
        {
            switch (e.HotKey.Name.ToLower())
            {
                case "lhkcopylog":
                    if (!string.IsNullOrEmpty(txtLog.Text)) { Clipboard.SetText(txtLog.Text); }
                    break;

                case "lhkclearlog":
                    txtLog.Clear();
                    return;

                case "lhkdisablelog":
                    LogEvent = !LogEvent;
                    break;

                case "lhknewlocalkey":
                    NewLocal LocalForm = new NewLocal();
                    LocalForm.MainForm = this;
                    LocalForm.ShowDialog();
                    break;

                default:
                    if (e.HotKey.Tag != null) System.Diagnostics.Process.Start((string)e.HotKey.Tag);
                    break;
            }
            LogEvents(e.HotKey.FullInfo());
        }

        void MyHotKeyManager_GlobalHotKeyPressed(object sender, GlobalHotKeyEventArgs e)
        {
            if (e.HotKey.Name.ToLower() == "ghkcustom") { HandleCustomHotKey(); LogEvents(e.HotKey); return; }
            System.Diagnostics.Process.Start((e.HotKey.Tag as string));
            LogEvents(e.HotKey);
        }

        void AddNewHotKey()
        {
            NewKey NewShortcut = new NewKey();
            NewShortcut.MainForm = this;
            NewShortcut.ShowDialog();
        }

        private void LogEvents(string text)
        {
            if (LogEvent)
            {
                txtLog.Text += text + Environment.NewLine;
                txtLog.Select(txtLog.Text.Length, 0);
                txtLog.ScrollToCaret();
            }
        }

        internal void LogEvents(GlobalHotKey HotKey)
        {
            if (LogEvent)
            {
                txtLog.Text += string.Format("{0} : Hotkey Processed! Name: {1}; {2}",
                                             HotKey.Name, HotKey.FullInfo(), Environment.NewLine);
                txtLog.Select(txtLog.Text.Length, 0);
                txtLog.ScrollToCaret();
            }
        }

        internal void LogEvents(ChordHotKey HotKey)
        {
            if (LogEvent)
            {
                txtLog.Text += string.Format("{0} : Hotkey Processed! Name: {1}; {2}",
                                             HotKey.Name, HotKey.FullInfo(), Environment.NewLine);
                txtLog.Select(txtLog.Text.Length, 0);
                txtLog.ScrollToCaret();
            }
        }

        void RegisterHotKeys()
        {
            ghkNotepad.Enabled = chkNotepad.Checked;
            ghkWordpad.Enabled = chkWordpad.Checked;
            ghkCalc.Enabled = chkCalculator.Checked;
            ghkTaskMan.Enabled = chkTaskManager.Checked;
            ghkUninstall.Enabled = chkUninstall.Checked;
            ghkCustom.Enabled = chkCustomEnabled.Checked;

            lhkNewHotkey.Enabled = chkNewHotKey.Checked;
            lhkNewLocalKey.Enabled = chkNewLocal.Checked;
            lhkCopyLog.Enabled = chkCopyClipboard.Checked;
            lhkClearLog.Enabled = chkCopyClipboard.Checked;
            lhkDisableLog.Enabled = chkDisableLogging.Checked;

            //Store an information in the tag of the hotkeys.
            ghkNotepad.Tag = "Notepad.exe";
            ghkWordpad.Tag = "Wordpad.exe";
            ghkCalc.Tag = "Calc.exe";
            ghkTaskMan.Tag = "Taskmgr.exe";
            ghkUninstall.Tag = "appwiz.cpl";

            chotCmd.Tag = "cmd.exe";
            chotPowerShell.Tag = "powershell.exe";
            chotIExplore.Tag = "iexplore.exe";
            chotRegEdit.Tag = "regedit.exe";
            chotCharMap.Tag = "charmap.exe";

            lhkNewHotkey.HotKeyPressed += delegate { AddNewHotKey(); };

            //Now, we'll add the Keys to the HotKeyManager
            MyHotKeyManager.AddGlobalHotKey(ghkNotepad);
            MyHotKeyManager.AddGlobalHotKey(ghkWordpad);
            MyHotKeyManager.AddGlobalHotKey(ghkCalc);
            MyHotKeyManager.AddGlobalHotKey(ghkTaskMan);
            MyHotKeyManager.AddGlobalHotKey(ghkUninstall);
            MyHotKeyManager.AddGlobalHotKey(ghkCustom);
            //Add the Local HotKeys.
            MyHotKeyManager.AddLocalHotKey(lhkNewHotkey);
            MyHotKeyManager.AddLocalHotKey(lhkNewLocalKey);
            MyHotKeyManager.AddLocalHotKey(lhkCopyLog);
            MyHotKeyManager.AddLocalHotKey(lhkClearLog);
            MyHotKeyManager.AddLocalHotKey(lhkDisableLog);
            //Add the Chord HotKeys.
            MyHotKeyManager.AddChordHotKey(chotCmd);
            MyHotKeyManager.AddChordHotKey(chotPowerShell);
            MyHotKeyManager.AddChordHotKey(chotIExplore);
            MyHotKeyManager.AddChordHotKey(chotRegEdit);
            MyHotKeyManager.AddChordHotKey(chotCharMap);
        }

        void HandleCustomHotKey()
        {
            if (optVisibility.Checked) // Visibility is to be toggled.
            {
                if (this.Visible)   // The form is already visible.
                {
                    this.Hide();
                }
                else //The form is hidden.
                {
                    this.Show();
                }
            }
            else     // A message is to be shown. Since there are only two radio buttons on the form.
            {
                if (MessageBox.Show("You have pressed the global shortcut. :)\nWould you like to visit my page now?",
                    "Global Shortcut Example", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("http://www.codeproject.com/bonded");
                }
            }
        }

        #region **CheckBox Events.
        private void chkNotepad_CheckedChanged(object sender, EventArgs e)
        {
            ghkNotepad.Enabled = chkNotepad.Checked;
            LogEvents(ghkNotepad);
        }

        private void chkWordpad_CheckedChanged(object sender, EventArgs e)
        {
            ghkWordpad.Enabled = chkWordpad.Checked;
            LogEvents(ghkWordpad);
        }

        private void chkCalculator_CheckedChanged(object sender, EventArgs e)
        {
            ghkCalc.Enabled = chkCalculator.Checked;
            LogEvents(ghkCalc);
        }

        private void chkTaskManager_CheckedChanged(object sender, EventArgs e)
        {
            ghkTaskMan.Enabled = chkTaskManager.Checked;
            LogEvents(ghkTaskMan);
        }

        private void chkUninstall_CheckedChanged(object sender, EventArgs e)
        {
            ghkUninstall.Enabled = chkUninstall.Checked;
            LogEvents(ghkUninstall);
        }

        private void chkNewHotKey_CheckedChanged(object sender, EventArgs e)
        {
            lhkNewHotkey.Enabled = (sender as CheckBox).Checked;
            LogEvents(lhkNewHotkey.FullInfo());
        }

        private void chkNewLocal_CheckedChanged(object sender, EventArgs e)
        {
            lhkNewLocalKey.Enabled = (sender as CheckBox).Checked;
            LogEvents(lhkNewLocalKey.FullInfo());
            //Since LocalHotKkeys can be converted to GlobalHotKeys
            LogEvents((GlobalHotKey)lhkNewLocalKey);
        }

        private void chkCopyClipboard_CheckedChanged(object sender, EventArgs e)
        {
            lhkCopyLog.Enabled = chkCopyClipboard.Checked;
            LogEvents(lhkCopyLog.FullInfo());
        }

        private void chkClearLog_CheckedChanged(object sender, EventArgs e)
        {
            lhkClearLog.Enabled = chkClearLog.Checked;
            LogEvents(lhkClearLog.FullInfo());
        }

        private void chkDisableLogging_CheckedChanged(object sender, EventArgs e)
        {
            lhkDisableLog.Enabled = chkDisableLogging.Checked;
            LogEvents(lhkDisableLog.FullInfo());
        }

        private void chkCustomEnabled_CheckedChanged(object sender, EventArgs e)
        {
            ghkCustom.Enabled = chkCustomEnabled.Checked;
            LogEvents(ghkCustom);
        }

        private void chkCmd_CheckedChanged(object sender, EventArgs e)
        {
            chotCmd.Enabled = chkCmd.Checked;
            LogEvents(chotCmd);
        }

        private void chkPowerShell_CheckedChanged(object sender, EventArgs e)
        {
            chotPowerShell.Enabled = chkPowerShell.Checked;
            LogEvents(chotPowerShell);
        }

        private void chkIExplore_CheckedChanged(object sender, EventArgs e)
        {
            chotIExplore.Enabled = chkIExplore.Checked;
            LogEvents(chotIExplore);
        }

        private void chkRegEdit_CheckedChanged(object sender, EventArgs e)
        {
            chotRegEdit.Enabled = chkRegEdit.Checked;
            LogEvents(chotRegEdit);
        }

        private void chkCharMap_CheckedChanged(object sender, EventArgs e)
        {
            chotCharMap.Enabled = chkCharMap.Checked;
            LogEvents(chotCharMap);
        }
        #endregion

        private void btnModify_Click(object sender, EventArgs e)
        {
            lblShortcut.Text = SetKey.ChangeShortcut(lblShortcut.Text);
            UpdateCustomShortcut(lblShortcut.Text);
        }

        void UpdateCustomShortcut(string text)
        {
            LogEvents("Attempting to update custom shortcut to: " + text);

            //Will help determine if the shortcut has any modifier.
            bool HasAlt = false; bool HasControl = false; bool HasShift = false;

            Modifiers Modifier = Modifiers.None;		//Variable to contain modifier.
            Keys key = 0;           //The key to register.

            string[] result;
            string[] separators = new string[] { " + " };
            result = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            //Iterate through the keys and find the modifier.
            foreach (string entry in result)
            {
                //Find the Control Key.
                if (entry.Trim() == Keys.Control.ToString())
                {
                    HasControl = true;
                }
                //Find the Alt key.
                if (entry.Trim() == Keys.Alt.ToString())
                {
                    HasAlt = true;
                }
                //Find the Shift key.
                if (entry.Trim() == Keys.Shift.ToString())
                {
                    HasShift = true;
                }
            }

            if (HasControl) { Modifier |= Modifiers.Control; }
            if (HasAlt) { Modifier |= Modifiers.Alt; }
            if (HasShift) { Modifier |= Modifiers.Shift; }

            //Get the last key in the shortcut
            KeysConverter keyconverter = new KeysConverter();
            key = (Keys)keyconverter.ConvertFrom(result.GetValue(result.Length - 1));

            ghkCustom.Enabled = chkCustomEnabled.Checked;
            ghkCustom.Key = key;
            ghkCustom.Modifier = Modifier;

            LogEvents(string.Format("Custom shortcut updated. \n Key:{0}, Modifier:{1}", ghkCustom.Key, ghkCustom.Modifier));
        }

        private void btnEnumerate_Click(object sender, EventArgs e)
        {
            string message = "Global HotKeys.\n";

            foreach (GlobalHotKey gh in MyHotKeyManager.EnumerateGlobalHotKeys)
            {
                message += string.Format("{0}{1}", Environment.NewLine, gh.FullInfo());
            }

            message += "\n\nLocal HotKeys.\n";

            foreach (LocalHotKey lh in MyHotKeyManager.EnumerateLocalHotKeys)
            {
                message += string.Format("{0}{1}", Environment.NewLine, lh.FullInfo());
            }

            message += "\n\nChord HotKeys.\n";

            foreach (ChordHotKey ch in MyHotKeyManager.EnumerateChordHotKeys)
            {
                message += string.Format("{0}{1}", Environment.NewLine, ch.FullInfo());
            }

            MessageBox.Show(message, "All HotKeys registered by this app.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSimulate_Click(object sender, EventArgs e)
        {
            MyHotKeyManager.SimulateKeyDown(Keys.Control);
            MyHotKeyManager.SimulateKeyPress(Keys.A);
            MyHotKeyManager.SimulateKeyUp(Keys.Control);
        }

        static bool KeysToggle;
        private void btnToggle_Click(object sender, EventArgs e)
        {
            KeysToggle = !KeysToggle;

            //Here we disable the keys A,E,I,O,U and any other key pressed while the Shift Key is held down
            //by hooking our class to listen to all keyboard messages.
            //Then we handle the keys we want by setting Handled to true.

            if (KeysToggle)
            {
                if (!MyHotKeyManager.KeyboardHooked) MyHotKeyManager.KeyBoardHook();

                MyHotKeyManager.KeyBoardKeyDown += keyboardhandler;

                LogEvents("Keys A, E , I , O and U disabled on the keyboard.");
                LogEvents("The Shift key disables all keys.");
                btnToggleKeys.Text = "Enable Keys";
            }
            else
            {
                MyHotKeyManager.KeyBoardKeyDown -= keyboardhandler;
                MyHotKeyManager.KeyBoardUnHook();
                LogEvents("All keys enabled on the keyboard.");
                btnToggleKeys.Text = "Disable Keys";
            }
        }

        KeyboardHookEventHandler keyboardhandler = (sender, handler) =>
        {
            if (handler.Modifier == KeyboardHookEventArgs.modifiers.Shift)
            { handler.Handled = true; }

            switch (handler.Key)
            {
                case Keys.A:
                case Keys.E:
                case Keys.I:
                case Keys.O:
                case Keys.U:
                    handler.Handled = true;
                    return;
            }
        };
    }
}