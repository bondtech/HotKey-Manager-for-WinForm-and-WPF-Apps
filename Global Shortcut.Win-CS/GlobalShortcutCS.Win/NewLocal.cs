using System;
using System.Windows.Forms;

using BondTech.HotkeyManagement.Win;

namespace GlobalShortcutCS.Win
{
    public partial class NewLocal : Form
    {
        internal AppStarter MainForm;

        public NewLocal()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(NewLocal_KeyDown);
            this.Shown += new EventHandler(NewLocal_Shown);
            this.FormClosing += new FormClosingEventHandler(NewLocal_FormClosing);
            hotKeyControl1.HotKeyIsSet +=new HotKeyIsSetEventHandler(hotKeyControl1_HotKeyIsSet);
        }

        void hotKeyControl1_HotKeyIsSet(object sender, HotKeyIsSetEventArgs e)
        {
            if (MainForm.MyHotKeyManager.HotKeyExists(e.Shortcut, HotKeyManager.CheckKey.LocalHotKey))
            {
                e.Cancel = true;
                MessageBox.Show("This HotKey has already been registered");
            }
        }

        void NewLocal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && !hotKeyControl1.Focused) { 
                this.Close(); }
        }

        void NewLocal_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.MyHotKeyManager.Enabled = true;
            MainForm = null;
        }

        void NewLocal_Shown(object sender, EventArgs e)
        {
            MainForm.MyHotKeyManager.Enabled = false;
        }

        private void txtProgram_Enter(object sender, EventArgs e)
        {
            if (ProgramPicker.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                txtProgram.Text = ProgramPicker.FileName;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
                if (hotKeyControl1.Text != Keys.None.ToString())
                    if (!string.IsNullOrEmpty(txtProgram.Text) && HotKeyShared.IsValidHotkeyName(txtName.Text))
                    {
                        LocalHotKey NewLocalHotKey = new LocalHotKey(txtName.Text, hotKeyControl1.UserModifier, hotKeyControl1.UserKey);
                        NewLocalHotKey.Tag = txtProgram.Text;
                        MainForm.MyHotKeyManager.AddLocalHotKey(NewLocalHotKey);
                        this.Close();
                    }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}