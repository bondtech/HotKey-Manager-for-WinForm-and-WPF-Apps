using System;
using System.Windows.Forms;
using BondTech.HotkeyManagement.Win;

namespace GlobalShortcutCS.Win
{
    public partial class NewKey : Form
    {
        bool KeyisSet;
        internal AppStarter MainForm;

        public NewKey()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.Shown += new EventHandler(NewKey_Shown);
            this.FormClosing += new FormClosingEventHandler(NewKey_FormClosing);
            this.KeyDown += new KeyEventHandler(NewKey_KeyDown);
        }

        void NewKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape & !txtButton.Focused)
            {
                this.Close();
            }
        }

        void NewKey_Shown(object sender, EventArgs e)
        {
            MainForm.MyHotKeyManager.Enabled = false;
        }

        void NewKey_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.MyHotKeyManager.Enabled = true;
            MainForm = null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtButton.Text != Keys.None.ToString() && !txtButton.Text.Trim().EndsWith("+"))
                    if (!string.IsNullOrEmpty(txtProgram.Text) && HotKeyShared.IsValidHotkeyName(txtName.Text))
                        MainForm.MyHotKeyManager.AddGlobalHotKey(CreateHotKey(txtName.Text, txtButton.Text, txtProgram.Text));

                this.Close();

            }
            catch (HotKeyAlreadyRegisteredException)
            {
                MessageBox.Show("A hotkey with the same name or shortcut has already been registered.");
            }
        }

        private void txtProgram_Enter(object sender, EventArgs e)
        {
            if (ProgramPicker.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                txtProgram.Text = ProgramPicker.FileName;
            }
        }

        private void txtButton_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;  //Suppress the key from being processed by the underlying control.
            txtButton.Text = string.Empty;  //Empty the content of the textbox
            KeyisSet = false; //At this point the user has not specified a shortcut.

            //Set the backspace button to specify that the user does not want to use a shortcut.
            if (e.KeyData == Keys.Back)
            {
                txtButton.Text = Keys.None.ToString();
                return;
            }

            //Make the user specify a modifier. Control, Alt or Shift.
            //If a modifier is not present then clear the textbox.
            if (e.Modifiers == Keys.None)
            {
                MessageBox.Show("You have to specify a modifier like 'Control', 'Alt' or 'Shift'");
                txtButton.Text = Keys.None.ToString();
                return;
            }

            //A modifier is present. Process each modifier.
            //Modifiers are separated by a ",". So we'll split them and write each one to the textbox.
            foreach (string modifier in e.Modifiers.ToString().Split(new Char[] { ',' }))
            {
                txtButton.Text += modifier + " + ";
            }

            //KEYCODE contains the last key pressed by the user.
            //If KEYCODE contains a modifier, then the user has not entered a shortcut. hence, KeyisSet is false
            //But if not, KeyisSet is true.
            if (e.KeyCode == Keys.ShiftKey | e.KeyCode == Keys.ControlKey | e.KeyCode == Keys.Menu)
            {
                KeyisSet = false;
            }
            else
            {
                txtButton.Text += e.KeyCode.ToString();
                KeyisSet = true;
            }

        }

        private void txtButton_KeyUp(object sender, KeyEventArgs e)
        {
            //On KeyUp if KeyisSet is False then clear the textbox.
            if (KeyisSet == false)
            {
                txtButton.Text = Keys.None.ToString();
            }
        }

        GlobalHotKey CreateHotKey(string name, string shortcut, object tag)
        {
            object[] Parsed = HotKeyShared.ParseShortcut(shortcut);
            Modifiers mod = (Modifiers)Parsed.GetValue(0);
            Keys key = (Keys)Parsed.GetValue(1);

            GlobalHotKey toReturn = new GlobalHotKey(name, mod, key);
            toReturn.Tag = tag;

            return toReturn;
        }
    }
}
