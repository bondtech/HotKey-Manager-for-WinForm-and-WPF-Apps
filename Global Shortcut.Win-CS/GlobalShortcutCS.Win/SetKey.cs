using System;
using System.Windows.Forms;

namespace GlobalShortcutCS.Win
{
    public partial class SetKey : Form
    {
        bool KeyisSet; //Would help us to know if the user has set a shortcut.

        public SetKey()
        {
            InitializeComponent();
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

        //This Method is used to change the shortcut. It will show the form and allow the user enter a shortcut.
        //However, if you want to do something else when the user saves the form.
        //Change the DialogResult of btnSave to none and write your code in the Click event.
        public static string ChangeShortcut(string current)
        {
            //Since this is a shared function, we'll create an instance of the form and show it to the user.
            SetKey ThisForm = new SetKey();
            ThisForm.txtButton.Text = current;         //Set the textbox text to the current global shortcut.

            DialogResult UserResponce = ThisForm.ShowDialog();   //display the form as a dialog.

            if (UserResponce != DialogResult.Cancel)// The user did not press cancel.
            {
                if (ThisForm.txtButton.Text == Keys.None.ToString())    //The user did not enter a shortcut.
                {
                    DialogResult MessageResult = MessageBox.Show(
                        "You have not specified a global shortcut." +
                        "\nPress Yes to keep it this way." +
                        "\nPress No to specify a shortcut." +
                        "\nPress Cancel to use the default shortcut.",
                        "Global Shortcut Example.", MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);  //The dialog to display to the user.

                    if (MessageResult == DialogResult.Yes) //The user does not want to specify a shortcut.
                    {
                        return Keys.None.ToString();
                    }
                    else if (MessageResult == DialogResult.No)// The user wants to go back and specify a shortcut.
                    {
                        ChangeShortcut(current);  // Call the method again.
                    }
                    else // The user wants to use the default global shortcut.
                    {
                        return "Shift + Control + Alt + T";
                    }
                }
                else //The user entered a shortcut.
                {
                    return ThisForm.txtButton.Text;
                }
            }
            else
            {
                //Enter code here for if the user pressed cancel. That is if in case you do not want that.
                //You could show the form again by uncommenting the line below. or display a message.
                //ChangeShortCut()
            }
            return current; ;  //Returns the current shortcut.
        }
    }
}
