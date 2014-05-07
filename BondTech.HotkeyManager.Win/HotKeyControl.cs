using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

namespace BondTech.HotkeyManagement.Win
{
    /// <summary>Allows adding custom hotkeys.
    /// </summary>
    [DefaultProperty("ForceModifiers"), DefaultEvent("HotKeyIsSet"), ToolboxBitmap(typeof(HotKeyControl), "HotKeyControl.png")]
    public partial class HotKeyControl : UserControl
    {
        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        #region **Properties.
        bool KeyisSet; //Would help us to know if the user has set a shortcut.
        bool forcemodifier = true;
        string tooltip; //The hotKey control tooltip cannot be set outside of here, hence the need for a tooltip property.

        /// <summary>Specifies that the control should force the user to use a modifier.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(true)]
        [Description("Specifies that the control should force the user to use a modifier.")]
        public bool ForceModifiers { get { return forcemodifier; } set { forcemodifier = value; } }

        ///// <summary>The value of this property can never be true, even if set.
        ///// </summary>
        //[EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public override bool ShortcutsEnabled { get { return TextBox.ShortcutsEnabled; } set { base.ShortcutsEnabled = false; } }

        /// <summary>Gets or sets the current text in the HotKeyControl
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(false)]
        public override string Text
        {
            get
            {
                return TextBox.Text;
            }
            set
            {
                TextBox.Text = value;
            }
        }

        [Browsable(false)]
        public Keys UserKey
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Text) && this.Text != Keys.None.ToString())
                {
                    return (Keys)HotKeyShared.ParseShortcut(this.Text).GetValue(1);
                }
                return Keys.None;
            }
        }

        [Browsable(false)]
        public Modifiers UserModifier
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Text) && this.Text != Keys.None.ToString())
                {
                    return (Modifiers)HotKeyShared.ParseShortcut(this.Text).GetValue(0);
                }
                return Modifiers.None;
            }
        }

        public string ToolTip
        {
            get { return tooltip; }
            set
            {
                ToolTipProvider.SetToolTip(TextBox, value);
                tooltip = value;
            }


        }
        #endregion

        #region **Events
        /// <summary>Raised after a valid key is set.
        /// </summary>
        [Description("Raised when a valid key is set")]
        public event HotKeyIsSetEventHandler HotKeyIsSet;
        #endregion

        #region **Constructor
        public HotKeyControl()
        {
            InitializeComponent();
        }
        #endregion

        #region **Helpers
        void updateWatermark()
        {
            if (!IsHandleCreated)
                return;

            IntPtr mem = Marshal.StringToHGlobalUni("Enter HotKey here");
            SendMessage(TextBox.Handle, 0x1501, (IntPtr)1, mem);
            Marshal.FreeHGlobal(mem);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            updateWatermark();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (Height != TextBox.Height)
                Height = TextBox.Height;
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (this.Text.Trim().EndsWith("+")) { this.Text = String.Empty; }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            //On KeyUp if KeyisSet is False then clear the textbox.
            if (KeyisSet == false)
            {
                this.Text = String.Empty;
            }
            else
            {
                if (HotKeyIsSet != null)
                {
                    var ex = new HotKeyIsSetEventArgs(UserKey, UserModifier);
                    HotKeyIsSet(this, ex);
                    if (ex.Cancel)
                    {
                        KeyisSet = false;
                        this.Text = String.Empty;
                    }
                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;  //Suppress the key from being processed by the underlying control.
            this.Text = string.Empty;  //Empty the content of the textbox
            KeyisSet = false; //At this point the user has not specified a shortcut.

            //Make the user specify a modifier. Control, Alt or Shift.
            //If a modifier is not present then clear the textbox.
            if (e.Modifiers == Keys.None && forcemodifier)
            {
                MessageBox.Show("You have to specify a modifier like 'Control', 'Alt' or 'Shift'");
                this.Text = String.Empty;
                return;
            }

            //A modifier is present. Process each modifier.
            //Modifiers are separated by a ",". So we'll split them and write each one to the textbox.
            foreach (string modifier in e.Modifiers.ToString().Split(new Char[] { ',' }))
            {
                if (modifier != Keys.None.ToString())
                    this.Text += modifier + " + ";
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
                this.Text += e.KeyCode.ToString();
                KeyisSet = true;
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.Text == string.Empty)
                ResetButton.Visible = false;
            else
                ResetButton.Visible = true;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            this.Text = string.Empty;
        }
        #endregion
    }
}
