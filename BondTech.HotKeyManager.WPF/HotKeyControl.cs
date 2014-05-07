using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Interop;
using System.Windows.Data;

namespace BondTech.HotKeyManagement.WPF
{
    [DefaultProperty("ForceModifiers"), DefaultEvent("HotKeyIsSet")]
    public class HotKeyControl : TextBox
    {
        #region **Properties.
        HwndSource hwndSource;
        HwndSourceHook hook;

        /// <summary>Identifies the HotKey control ForceModifiers dependency property.
        /// </summary>
        public static readonly DependencyProperty ForceModifiersProperty =
            DependencyProperty.Register("ForceModifiers", typeof(Boolean), typeof(HotKeyControl), new PropertyMetadata(true));

        /// <summary>Gets or sets the text content of the HotKey control.
        /// </summary>
        [Browsable(false)]
        public new string Text
        {
            get
            { return base.Text; }
            set
            { base.Text = value; }
        }

        /// <summary>Gets or sets a value specifying that the user should be forced to enter modifiers. This is a dependency property.
        /// </summary>
        [Bindable(true), EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        [Description("Gets or sets a value specifying that the user be forced to enter modifiers.")]
        public bool ForceModifiers
        {
            get { return (bool)GetValue(ForceModifiersProperty); }
            set { SetValue(ForceModifiersProperty, value); }
        }

        /// <summary>Returns the key set by the user.
        /// </summary>
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

        /// <summary>Returns the Modifier set by the user.
        /// </summary>
        [Browsable(false)]
        public ModifierKeys UserModifier
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Text) && this.Text != Keys.None.ToString())
                {
                    return (ModifierKeys)HotKeyShared.ParseShortcut(this.Text).GetValue(0);
                }
                return ModifierKeys.None;
            }
        }
        #endregion

        #region **Events
        public static readonly RoutedEvent HotKeyIsSetEvent = EventManager.RegisterRoutedEvent(
            "HotKeyIsSet", RoutingStrategy.Bubble, typeof(HotKeyIsSetEventHandler), typeof(HotKeyControl));

        [Category("Behaviour")]
        public event HotKeyIsSetEventHandler HotKeyIsSet
        {
            add { AddHandler(HotKeyIsSetEvent, value); }
            remove { RemoveHandler(HotKeyIsSetEvent, value); }
        }
        #endregion

        public HotKeyControl()
        {
            this.GotFocus += new RoutedEventHandler(HotKeyControl_GotFocus);
            this.hook = new HwndSourceHook(WndProc); //Hook to to Windows messages.
            this.LostFocus += new RoutedEventHandler(HotKeyControl_LostFocus);
            this.ContextMenu = null; //Disable shortcuts.
            Text = Keys.None.ToString();
            this.IsReadOnly = true;
            this.PreviewKeyDown += new KeyEventHandler(HotKeyControl_PreviewKeyDown);
        }

        void HotKeyControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Microsoft.VisualBasic.Devices.Keyboard UserKeyBoard = new Microsoft.VisualBasic.Devices.Keyboard();
            bool AltPressed = UserKeyBoard.AltKeyDown;
            bool ControlPressed = UserKeyBoard.CtrlKeyDown;
            bool ShiftPressed = UserKeyBoard.ShiftKeyDown;

            ModifierKeys LocalModifier = ModifierKeys.None;
            if (AltPressed) { LocalModifier = ModifierKeys.Alt; }
            if (ControlPressed) { LocalModifier |= ModifierKeys.Control; }
            if (ShiftPressed) { LocalModifier |= ModifierKeys.Shift; }

            switch (e.Key)
            {
                case Key.Back:
                    this.Text = Keys.None.ToString();
                    e.Handled = true;
                    break;

                case Key.Space:
                    this.Text = CheckModifier(LocalModifier) ? HotKeyShared.CombineShortcut(LocalModifier, Keys.Space) : Keys.None.ToString();
                    e.Handled = true;
                    break;

                case Key.Delete:
                    this.Text = CheckModifier(LocalModifier) ? HotKeyShared.CombineShortcut(LocalModifier, Keys.Delete) : Keys.None.ToString();
                    e.Handled = true;
                    break;

                case Key.Home:
                    this.Text = CheckModifier(LocalModifier) ? HotKeyShared.CombineShortcut(LocalModifier, Keys.Home) : Keys.None.ToString();
                    e.Handled = true;
                    break;

                case Key.PageUp:
                    this.Text = CheckModifier(LocalModifier) ? HotKeyShared.CombineShortcut(LocalModifier, Keys.PageUp) : Keys.None.ToString();
                    e.Handled = true;
                    break;

                case Key.Next:
                    this.Text = CheckModifier(LocalModifier) ? HotKeyShared.CombineShortcut(LocalModifier, Keys.Next) : Keys.None.ToString();
                    e.Handled = true;
                    break;

                case Key.End:
                    this.Text = CheckModifier(LocalModifier) ? HotKeyShared.CombineShortcut(LocalModifier, Keys.End) : Keys.None.ToString();
                    break;

                case Key.Up:
                    this.Text = CheckModifier(LocalModifier) ? HotKeyShared.CombineShortcut(LocalModifier, Keys.Up) : Keys.None.ToString();
                    break;

                case Key.Down:
                    this.Text = CheckModifier(LocalModifier) ? HotKeyShared.CombineShortcut(LocalModifier, Keys.Down) : Keys.None.ToString();
                    break;

                case Key.Right:
                    this.Text = CheckModifier(LocalModifier) ? HotKeyShared.CombineShortcut(LocalModifier, Keys.Right) : Keys.None.ToString();
                    break;

                case Key.Left:
                    this.Text = CheckModifier(LocalModifier) ? HotKeyShared.CombineShortcut(LocalModifier, Keys.Left) : Keys.None.ToString();
                    break;
            }
        }

        void HotKeyControl_LostFocus(object sender, RoutedEventArgs e)
        {
            this.hwndSource.RemoveHook(hook);
            this.BorderBrush = null;
        }

        void HotKeyControl_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hwndSource = (HwndSource)HwndSource.FromVisual(this); // new WindowInteropHelper(window).Handle // If the InPtr is needed.
            this.hwndSource.AddHook(hook);
            this.BorderBrush = Brushes.Green;
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            try
            {
                Keys KeyPressed = (Keys)wParam;

                Microsoft.VisualBasic.Devices.Keyboard UserKeyBoard = new Microsoft.VisualBasic.Devices.Keyboard();
                bool AltPressed = UserKeyBoard.AltKeyDown;
                bool ControlPressed = UserKeyBoard.CtrlKeyDown;
                bool ShiftPressed = UserKeyBoard.ShiftKeyDown;

                ModifierKeys LocalModifier = ModifierKeys.None;
                if (AltPressed) { LocalModifier = ModifierKeys.Alt; }
                if (ControlPressed) { LocalModifier |= ModifierKeys.Control; }
                if (ShiftPressed) { LocalModifier |= ModifierKeys.Shift; }

                switch ((KeyboardMessages)msg)
                {
                    case KeyboardMessages.WmSyskeydown:
                    case KeyboardMessages.WmKeydown:
                        switch (KeyPressed)
                        {
                            case Keys.Control:
                            case Keys.ControlKey:
                            case Keys.LControlKey:
                            case Keys.RControlKey:
                            case Keys.Shift:
                            case Keys.ShiftKey:
                            case Keys.LShiftKey:
                            case Keys.RShiftKey:
                            case Keys.Alt:
                            case Keys.Menu:
                            case Keys.LMenu:
                            case Keys.RMenu:
                            case Keys.LWin:
                                return IntPtr.Zero;

                            //case Keys.Back:
                            //    this.Text = Keys.None.ToString();
                            //    return IntPtr.Zero;
                        }

                        if (LocalModifier != ModifierKeys.None)
                        {
                            this.Text = HotKeyShared.CombineShortcut(LocalModifier, KeyPressed);
                        }
                        else
                        {
                            if (ForceModifiers)
                            {
                                this.Text = Keys.None.ToString();
                                MessageBox.Show("You have to specify a modifier like 'Control', 'Alt' or 'Shift'");
                            }
                            else
                            { this.Text = KeyPressed.ToString(); }
                        }
                        return IntPtr.Zero; ;

                    case KeyboardMessages.WmSyskeyup:
                    case KeyboardMessages.WmKeyup:
                        if (!String.IsNullOrEmpty(Text.Trim()) || this.Text != Keys.None.ToString())
                        {
                            if (HotKeyIsSetEvent != null)
                            {
                                var e = new HotKeyIsSetEventArgs(HotKeyIsSetEvent, UserKey, UserModifier);
                                base.RaiseEvent(e);
                                if (e.Cancel)
                                {
                                    this.Text = Keys.None.ToString();
                                }
                            }
                        }
                        return IntPtr.Zero;
                }
            }
            catch (OverflowException) { }

            return IntPtr.Zero;
        }

        private bool CheckModifier(ModifierKeys modifier)
        {
            if (modifier == ModifierKeys.None && ForceModifiers)
            {
                this.Text = Keys.None.ToString();
                MessageBox.Show("You have to specify a modifier like 'Control', 'Alt' or 'Shift'");
                return false;
            }

            return true;
        }
    }

    //public class AnotherControl : ContentControl
    //{
    //    StackPanel container;
    //    TextBox textcontrol;
    //    Button resetcontrol;

    //    HwndSource hwndSource;
    //    HwndSourceHook hook;
    //    public AnotherControl()
    //    {
    //        container = new StackPanel();
    //        container.Orientation = Orientation.Horizontal;

    //        textcontrol = new TextBox();
    //        resetcontrol = new Button();

    //        resetcontrol.ToolTip = "Click here to reset the hotkey";
    //        textcontrol.ContextMenu = null;
    //        container.Children.Add(textcontrol);
    //        container.Children.Add(resetcontrol);
    //        Content = container;

    //        this.hook = new HwndSourceHook(WndProc); //Hook to to Windows messages.
    //        this.GotFocus += new RoutedEventHandler(AnotherControl_GotFocus);
    //        this.LostFocus += new RoutedEventHandler(AnotherControl_LostFocus);
    //    }

    //    void AnotherControl_LostFocus(object sender, RoutedEventArgs e)
    //    {
    //        this.hwndSource.RemoveHook(hook);
    //        this.BorderBrush = null;
    //    }

    //    void AnotherControl_GotFocus(object sender, RoutedEventArgs e)
    //    {
    //        this.hwndSource = (HwndSource)HwndSource.FromVisual(this); // new WindowInteropHelper(window).Handle // If the InPtr is needed.
    //        this.hwndSource.AddHook(hook);
    //        textcontrol.Width = this.Width * .90;
    //    }

    //    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    //    {
    //        textcontrol. Width = this.Width * .90;
    //        switch ((KeyboardMessages)msg)
    //        {
    //            case KeyboardMessages.WmKeydown:
    //                MessageBox.Show(((Keys)(int)wParam).ToString());
    //                break;
    //        }

    //        return IntPtr.Zero;
    //    }
    //}
}
