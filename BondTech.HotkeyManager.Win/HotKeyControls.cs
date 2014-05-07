using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace BondTech.HotkeyManagement.Win
{
    /// <summary>Allows adding custom hotkeys.
    /// </summary>
    [DefaultProperty("ForceModifiers"), DefaultEvent("HotKeyIsSet"), ToolboxBitmap(typeof(HotKeyControl), "HotKeyControl.png")]
    public class HotKeyControl : TextBox
    {
        #region **Properties.
        bool KeyisSet; //Would help us to know if the user has set a shortcut.
        bool forcemodifier = true;

        /// <summary>Specifies that the control should force the user to use a modifier.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(true)]
        [Description("Specifies that the control should force the user to use a modifier.")]
        public bool ForceModifiers { get { return forcemodifier; } set { forcemodifier = value; } }

        /// <summary>The value of this property can never be true, even if set.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool ShortcutsEnabled { get { return base.ShortcutsEnabled; } set { base.ShortcutsEnabled = false; } }

        /// <summary>Gets or sets the current text in the HotKeyControl
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(false)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
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
            this.KeyDown += new KeyEventHandler(HotKeyControl_KeyDown);
            this.KeyUp += new KeyEventHandler(HotKeyControl_KeyUp);
            this.Leave += new EventHandler(HotKeyControl_Leave);
            this.Text = Keys.None.ToString();
            this.Font = new Font("Tahoma", 9.75f);
            base.ShortcutsEnabled = false;
        }
        #endregion

        #region **Helpers
        void HotKeyControl_Leave(object sender, EventArgs e)
        {
            if (this.Text.Trim().EndsWith("+")) { this.Text = Keys.None.ToString(); }
        }

        void HotKeyControl_KeyUp(object sender, KeyEventArgs e)
        {
            //On KeyUp if KeyisSet is False then clear the textbox.
            if (KeyisSet == false)
            {
                this.Text = Keys.None.ToString();
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
                        this.Text = Keys.None.ToString();
                    }
                }
            }
        }

        void HotKeyControl_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;  //Suppress the key from being processed by the underlying control.
            this.Text = string.Empty;  //Empty the content of the textbox
            KeyisSet = false; //At this point the user has not specified a shortcut.

            //Set the backspace button to specify that the user does not want to use a shortcut.
            if (e.KeyData == Keys.Back)
            {
                this.Text = Keys.None.ToString();
                return;
            }

            //Make the user specify a modifier. Control, Alt or Shift.
            //If a modifier is not present then clear the textbox.
            if (e.Modifiers == Keys.None && forcemodifier)
            {
                MessageBox.Show("You have to specify a modifier like 'Control', 'Alt' or 'Shift'");
                this.Text = Keys.None.ToString();
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
        #endregion

        //ToDo: Make the control able to set chords.

        //public enum Types:int
        //{
        //    Normal=0,
        //    Chord=1
        //}

        //[Browsable(true)]
        //[EditorBrowsable(EditorBrowsableState.Always)]
        //public Types HotKeyType { get; set; }
    }
}